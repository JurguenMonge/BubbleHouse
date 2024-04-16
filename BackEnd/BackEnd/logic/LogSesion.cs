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
                    var linqUsuario = linq.Solicitar_Login(req.correo, ref idReturn, ref idError, ref errorBD);    
                        if (idError == 0)
                        {
                            Usuario usuario = new Usuario();
                            foreach (var item in linqUsuario)
                            {
                                usuario = factoryArmarUsuario(item); ;
                            }
                            actualPassword = usuario.Password;
                            if (VerificarPassword(req.password, actualPassword))
                            {
                                res.Resultado = true;
                                var jwtManager = new JwtManager();
                                bool valido = false;
                                var linqSesion = linq.Obtener_Sesion_Activa_By_IdUsuario(usuario.IdUsuario);
                                if (idError != null)
                                {
                                    
                                    Sesion sesion  = new Sesion();
                                    sesion.Id_Sesion = "vacio";
                                    foreach (var item in linqSesion)
                                    {
                                        sesion = factoryArmarSesion(item); ;
                                    }
                                    if(sesion.Id_Sesion != "vacio")
                                    {
                                        ClaimsPrincipal principal = jwtManager.GetPrincipal(sesion.Token_Sesion);
                                        if (principal != null)
                                        {
                                        //valido
                                            valido = true;
                                        }
                                    }
                                    
                                    if (valido == false)
                                    {
                                        String id_Sesion = GenerateHexId();
                                        string token = jwtManager.GenerateToken(req.correo);
                                        linq.Insertar_Sesion(id_Sesion, usuario.IdUsuario , token, req.origen, ref idReturn, ref idError, ref errorBD);
                                        sesion.Id_Sesion = id_Sesion;
                                    }
                                    usuario.Password = "";
                                    sesion.Usuario = usuario;
                                    res.Sesion = sesion;
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
            catch
            {
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;

        }
        private Usuario factoryArmarUsuario(Solicitar_LoginResult usuarioLinq)
        {
            Usuario usuario = new Usuario();
            usuario.IdUsuario = usuarioLinq.ID_USUARIO;
            usuario.Nombre = usuarioLinq.DSC_NOMBRE;
            usuario.PrimerApellido = usuarioLinq.DSC_PRIMER_APELLIDO;
            usuario.SegundoApellido = usuarioLinq.DSC_SEGUNDO_APELLIDO;
            usuario.Password = usuarioLinq.DSC_PASSWORD;
            usuario.NumeroTelefono = usuarioLinq.DSC_TELEFONO;
            return usuario;
        }
        private Sesion factoryArmarSesion(Obtener_Sesion_Activa_By_IdUsuarioResult sesionLinq)
        {
            Sesion sesion = new Sesion();
            sesion.Id_Sesion = sesionLinq.ID_SESION;
            sesion.Token_Sesion = sesionLinq.DSC_SESION;
            return sesion;
        }
    }

}
