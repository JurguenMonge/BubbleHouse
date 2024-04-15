using BackEnd.data;
using BackEnd.domain;
using BackEnd.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public class LogSesion
    {
        private bool VerificarPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private static string GenerateHexId()
        {
            int length = 50;
            int byteLength = length / 2;
            byte[] randomBytes = new byte[byteLength];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            string hexId = BitConverter.ToString(randomBytes).Replace("-", string.Empty);
            if (length % 2 != 0)
            {
                hexId = hexId.Insert(0, randomBytes[0].ToString("X2")[0].ToString());
            }
            return hexId;
        }

        public ResIngresarSesion Login(ReqIngresarSesion req)
        {
            ResIngresarSesion res = new ResIngresarSesion();
            short tipoRegistro = 0;
            try
            {
                if (String.IsNullOrEmpty(req.correo))
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("Correo electronico faltante");
                    tipoRegistro = 2;
                }
                if (String.IsNullOrEmpty(req.password))
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("Contraseña faltante");
                    tipoRegistro = 2;
                }
                if (!res.ListaDeErrores.Any())
                {
                    ConexionDataContext linq = new ConexionDataContext();
                    String actualPassword;
                    int? idReturn = 0;
                    int? idError = 0;
                    String errorBD = "";
                    var result = linq.Solicitar_Login(req.correo, ref idReturn, ref idError, ref errorBD);
                    if (result != null)
                    {
                        res.Sesion.Usuario.Nombre = result.FirstOrDefault()?.DSC_NOMBRE;
                        res.Sesion.Usuario.PrimerApellido = result.FirstOrDefault()?.DSC_PRIMER_APELLIDO;
                        res.Sesion.Usuario.SegundoApellido = result.FirstOrDefault()?.DSC_SEGUNDO_APELLIDO;
                        res.Sesion.Usuario.CorreoElectronico = result.FirstOrDefault()?.DSC_CORREO;
                        res.Sesion.Usuario.NumeroTelefono = result.FirstOrDefault()?.DSC_TELEFONO;
                        var IdUsuario = result.FirstOrDefault()?.ID_USUARIO;
                        res.Sesion.Usuario.IdUsuario = IdUsuario.Value;

                        if (idError == 0)
                        {
                            actualPassword = result.FirstOrDefault()?.DSC_PASSWORD;
                            if (VerificarPassword(req.password, actualPassword))
                            {
                                res.Resultado = true;
                                var jwtManager = new JwtManager("BubbleHouseSecretKey2024jfuur46ag49sad64");
                                bool valido = false;
                                var resultSesion = linq.Obtener_Sesion_Activa_By_IdUsuario(res.Sesion.Usuario.IdUsuario);
                                if (resultSesion != null)
                                {
                                    ClaimsPrincipal principal = jwtManager.GetPrincipal(resultSesion.FirstOrDefault()?.DSC_SESION);
                                    if (principal != null)
                                    {
                                        //valido
                                        res.Sesion.Id_Sesion = resultSesion.FirstOrDefault()?.ID_SESION;
                                        valido = true;
                                    }
                                }
                                if (valido == false)
                                {
                                    String id_Sesion = GenerateHexId();
                                    string token = jwtManager.GenerateToken(req.correo);
                                    linq.Insertar_Sesion(id_Sesion, res.Sesion.Usuario.IdUsuario, token, req.origen, ref idReturn, ref idError, ref errorBD);
                                    res.Sesion.Id_Sesion = id_Sesion;
                                }
                            }
                            else
                            {
                                res.Resultado = false;
                                res.ListaDeErrores.Add("Contraseña incorrecta");
                                tipoRegistro = 2;
                            }
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add(errorBD);
                            tipoRegistro = 2;
                        }
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaDeErrores.Add("Usuario no encontrado");
                        tipoRegistro = 2;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;

        }
    }
}
