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
    public class RolController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/rol/obtener")]
        public ResObtenerRol obtenerRoles()
        {
            return new LogRol().obtenerRoles();
        }
    }
}
