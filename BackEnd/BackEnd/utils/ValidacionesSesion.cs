using BackEnd.data;
using BackEnd.domain;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.utils
{
    public class ValidacionesSesion
    {
        public bool validarSesionyRolSuperAdmin(String idSesion) ///True valido, false invalido
        {
            bool valido = false; 
            if (idSesion == " ")
            {
                valido = false;
            }
            else
            {
                try
                {
                    ConexionDataContext linq = new ConexionDataContext();
                    var sesiones = linq.Obtener_Sesion(idSesion);
                    Sesion sesion = new Sesion();
                    sesion.Id_Sesion = "vacio";
                    foreach (var item in sesiones)
                    {
                        sesion = factoryArmarSesion(item); ;
                    }
                    JwtManager jwt = new JwtManager();
                    ClaimsPrincipal principal = jwt.GetPrincipal(sesion.Token_Sesion);
                    bool rolvalido = false;
                    if(sesion.Usuario.rol.tipoRol.Equals("Super Administrador"))
                    {
                        rolvalido = true;
                    }
                    if (principal != null && rolvalido == true)
                    {
                        valido = true;
                    }
                }
                catch
                {
                    valido = false;
                }
            }
            return valido;
        }

        public bool validarSesionyRolAdmin(String idSesion)
        {
            bool valido = false;
            if (idSesion.IsNullOrEmpty())
            {
                valido = false;
            }
            else
            {
                try
                {
                    ConexionDataContext linq = new ConexionDataContext();
                    var sesiones = linq.Obtener_Sesion(idSesion);
                    Sesion sesion = new Sesion();
                    sesion.Id_Sesion = "vacio";
                    foreach (var item in sesiones)
                    {
                        sesion = factoryArmarSesion(item); ;
                    }
                    JwtManager jwt = new JwtManager();
                    ClaimsPrincipal principal = jwt.GetPrincipal(sesion.Token_Sesion);
                    bool rolvalido = false;
                    if (sesion.Usuario.rol.tipoRol.Equals("Super Administrador") || sesion.Usuario.rol.tipoRol.Equals("Administrador"))
                    {
                        rolvalido = true;
                    }
                    if (principal != null && rolvalido == true)
                    {
                        valido = true;
                    }
                }
                catch
                {
                    valido = false;
                }
            }
            return valido;
        }

        private Sesion factoryArmarSesion(Obtener_SesionResult sesionLinq)
        {
            Sesion sesion = new Sesion();
            sesion.Id_Sesion = sesionLinq.ID_SESION;
            sesion.Token_Sesion = sesionLinq.DSC_SESION;
            Usuario usuario = new Usuario();
            usuario.IdUsuario = (int)sesionLinq.ID_USUARIO;
            Rol rol = new Rol();
            rol.tipoRol = sesionLinq.DSC_TIPO_ROL;
            usuario.rol = rol;
            sesion.Usuario = usuario;
            return sesion;
        }
    }
}
