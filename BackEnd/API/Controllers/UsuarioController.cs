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
        public ResIngresarUsuario ingresarPublicacion(ReqIngresarUsuario req)
        {
            return new LogUsuario().ingresarUsuario(req);
        }
    }
}
