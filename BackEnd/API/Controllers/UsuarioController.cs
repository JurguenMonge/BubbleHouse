using BackEnd.domain;
using BackEnd.logic;
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
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/ingresar")]
        public ResUsuario ingresarUsuario(ReqIngresarUsuario req)
        {
            return new LogUsuario().ingresarUsuario(req);
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
            return new LogUsuario().modificarUsuario(req);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/usuario/eliminar/{id}")]
        public ResUsuario eliminarUsuario(int id)
        {
            return new LogUsuario().eliminarUsuario(id);
        }

    }
}
