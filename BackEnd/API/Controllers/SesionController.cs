using BackEnd.domain;
using BackEnd.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace API.Controllers
{
    public class SesionController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/login")]
        public ResIngresarSesion Login(ReqIngresarSesion req)
        {
            return new LogSesion().Login(req);
        }
    }
}