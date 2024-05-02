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

        private string EncriptarPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
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
            var cont = 0;
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
                    cont++;
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
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al crear la sesion");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;

        }

        public ResLogOut LogOut(ReqLogOut req)
        {
            ResLogOut res = new ResLogOut();
            short tipoRegistro = 0;
            try
            {
                if (String.IsNullOrEmpty(req.id_Sesion))
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("Id de la sesion faltante");
                    tipoRegistro = 2;
                }
                if (!res.ListaDeErrores.Any())
                {
                    ConexionDataContext linq = new ConexionDataContext();
                    int? idReturn = 0;
                    int? idError = 0;
                    String errorBD = "";
                    var linqUsuario = linq.Eliminar_Sesion(req.id_Sesion, req.dsc_cierre, ref idReturn, ref idError, ref errorBD);
                    if (idError != 1)
                    {
                        res.Resultado = true;
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaDeErrores.Add("Ocurrió un error al eliminar la sesion");
                        tipoRegistro = 3;
                    }
                }
                }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al eliminar la sesion");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }

            return res;
        }

        public ResSolicitarPassword solicitarPassword(ReqSolicitarPassword req)
        {
            ResSolicitarPassword res = new ResSolicitarPassword();
            short tipoRegistro = 0;
            try
            {
                if (String.IsNullOrEmpty(req.correo))
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("correo faltante");
                    tipoRegistro = 2;
                }
                if (!res.ListaDeErrores.Any())
                {
                    ConexionDataContext linq = new ConexionDataContext();
                    int? idReturn = 0;
                    int? idError = 0;
                    String errorBD = "";
                    var jwtManager = new JwtManager();
                    string token = jwtManager.GenerateToken(req.correo);
                    var linqUsuario = linq.Insertar_Recuperacion_Password(token, req.correo, ref idReturn, ref idError, ref errorBD);
                    if (idError != 1)
                    {
                        string fromAddress = "guapilesbubblehouse@hotmail.com";
                        string password = "miopwfayoljnozhj";
                        string toAddress = req.correo;
                        string subject = "Solicitud de cambio de contraseña";
                        string body = "De click en el siguiente enlance https://localhost:44311/api/changePassword/{" + token +"}";
                        EmailSender emailSender = new EmailSender(fromAddress, password);
                        res.Resultado = emailSender.SendEmail(toAddress, subject, body);
                    }
                    else
                    {
                        res.Resultado = false;

                        res.ListaDeErrores.Add("Ocurrió un error al insertar la solicitud de recuperacion");
                        tipoRegistro = 3;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("error al solicitar nueva contraseña");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res; 
        }

        public ResChangePassword changePassword(ReqChangePassword req)
        {
            ResChangePassword res = new ResChangePassword();
            short tipoRegistro = 0;
            try
            {
                if (String.IsNullOrEmpty(req.token))
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("token faltante");
                    tipoRegistro = 2;
                }
                if (String.IsNullOrEmpty(req.password))
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("contraseña faltante");
                    tipoRegistro = 2;
                }
                if (!res.ListaDeErrores.Any())
                {
                    ConexionDataContext linq = new ConexionDataContext();
                    int? idReturn = 0;
                    int? idError = 0;
                    String errorBD = "";
                    var jwtManager = new JwtManager();
                    req.password = EncriptarPassword(req.password);
                    var linqUsuario = linq.Modificar_Password(req.token, req.password, ref idReturn, ref idError, ref errorBD);
                    if (idError != 1)
                    {
                        res.Resultado = true;
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaDeErrores.Add("Ocurrió un error al modificar la contraseña");
                        tipoRegistro = 3;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("error al modificar la contraseña");
                tipoRegistro = 3;
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
