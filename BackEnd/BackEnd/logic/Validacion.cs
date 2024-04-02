using BackEnd.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public static class Validaciones
    {
        public static void ValidarNombre(Usuario usuario, ResIngresarUsuario res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(usuario.Nombre))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Nombre faltante");
                tipoRegistro = 2;
            }
        }

        public static void ValidarPrimerApellido(Usuario usuario, ResIngresarUsuario res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(usuario.PrimerApellido))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Primer apellido faltante");
                tipoRegistro = 2;
            }
        }

        public static void ValidarSegundoApellido(Usuario usuario, ResIngresarUsuario res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(usuario.SegundoApellido))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Segundo apellido faltante");
                tipoRegistro = 2;
            }
        }
        public static void ValidarCorreo(Usuario usuario, ResIngresarUsuario res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(usuario.CorreoElectronico))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Correo electronico faltante");
                tipoRegistro = 2;
            }
        }
        public static void ValidarPassword(Usuario usuario, ResIngresarUsuario res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(usuario.Password))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Contraseña faltante");
                tipoRegistro = 2;
            }
        }

        public static void ValidarTelefono(Usuario usuario, ResIngresarUsuario res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(usuario.NumeroTelefono))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Número de teléfono faltante");
                tipoRegistro = 2;
            }
        }

    }

}
