using BackEnd.domain;
using BackEnd.domain.response;
using BackEnd.logic;
using BackEnd.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        //Ingresar un usuario
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/ingresar")]
        public ResUsuario ingresarUsuario(ReqIngresarUsuario req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResUsuario res = new ResUsuario();
            if (vali.validarSesionyRolSuperAdmin(req.idSesion))
            {
                res = new LogUsuario().ingresarUsuario(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/usuario/obtener")]
        public ResObtenerUsuario obtenerUsuarios()
        {
            return new LogUsuario().obtenerUsuarios();
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/usuario/modificar")]
        public ResUsuario modificarUsuario(ReqIngresarUsuario req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResUsuario res = new ResUsuario();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogUsuario().modificarUsuario(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/usuario/modificarSuperAdmin")]
        public ResUsuario modificarUsuarioSuperAdmin(ReqIngresarUsuario req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResUsuario res = new ResUsuario();
            if (vali.validarSesionyRolSuperAdmin(req.idSesion))
            {
                res = new LogUsuario().modificarUsuarioSuperAdmin(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/usuario/eliminar")]
        public ResUsuario eliminarUsuario(ReqIngresarUsuario req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResUsuario res = new ResUsuario();
            if (vali.validarSesionyRolSuperAdmin(req.idSesion))
            {
                res = new LogUsuario().eliminarUsuario(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

    }
}
