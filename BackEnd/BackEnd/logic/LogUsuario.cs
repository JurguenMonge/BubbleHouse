using BackEnd.data;
using BackEnd.domain;
using System;
using System.Linq;
using BCrypt.Net;

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
                    Validaciones.ValidarTelefono(req.Usuario, res, ref tipoRegistro);
                    Validaciones.ValidarCorreo(req.Usuario, res, ref tipoRegistro);
                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.SP_CREAR_USUARIO(req.Usuario.Nombre, req.Usuario.PrimerApellido,
                            req.Usuario.SegundoApellido, req.Usuario.CorreoElectronico, EncriptarPassword(req.Usuario.Password),
                            req.Usuario.NumeroTelefono,1, ref idReturn, ref idError, ref errorBD);
                        if (idError == 0)
                        {
                            res.Resultado = true;
                            tipoRegistro = 1;
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add("Ocurrió un error en la base de datos, intentalo más tarde");
                            tipoRegistro = 2;
                        }
                    }

                }
                else
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("El request viene null");
                    tipoRegistro = 2;
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Error interno" + ex.Message);
                tipoRegistro = 3;
            }
            finally
            {
                //Hacer una bitacora
            }
            return res;
        }

    }
}
