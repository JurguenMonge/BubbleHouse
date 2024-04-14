using BackEnd.data;
using BackEnd.domain;
using System;
using System.Linq;
using BCrypt.Net;
using Newtonsoft.Json;
using System.Reflection;

namespace BackEnd.logic
{
    public class LogUsuario
    {

        private string EncriptarPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        private bool VerificarPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public  ResIngresarUsuario ingresarUsuario(ReqIngresarUsuario req)
        {
            ResIngresarUsuario res = new ResIngresarUsuario();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req != null)
                {
                    
                    Validaciones.ValidarNombre(req.Usuario, res, ref tipoRegistro);
                    Validaciones.ValidarPrimerApellido(req.Usuario, res, ref tipoRegistro);
                    Validaciones.ValidarSegundoApellido(req.Usuario, res, ref tipoRegistro);
                    Validaciones.ValidarPassword(req.Usuario, res, ref tipoRegistro);
                    req.Usuario.Password = EncriptarPassword(req.Usuario.Password);
                    Validaciones.ValidarTelefono(req.Usuario, res, ref tipoRegistro);
                    Validaciones.ValidarCorreo(req.Usuario, res, ref tipoRegistro);
                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Insertar_Usuario(req.Usuario.Nombre, req.Usuario.PrimerApellido,
                            req.Usuario.SegundoApellido, req.Usuario.CorreoElectronico, req.Usuario.Password,
                            req.Usuario.NumeroTelefono, ref idReturn, ref idError, ref errorBD);
                        if (idError == 0)
                        {
                            res.Resultado = true;
                            tipoRegistro = 1;
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add(errorBD);
                            tipoRegistro = 2;
                        }
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("No se enviaron los datos correctamente");
                    tipoRegistro = 2;
                }
            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al insertar el usuario");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }

    }
}
