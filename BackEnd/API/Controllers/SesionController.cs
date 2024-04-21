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

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/logout")]
        public ResLogOut LogOut(ReqLogOut req)
        {
            return new LogSesion().LogOut(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/solicitarPassword")]
        public ResSolicitarPassword SolicitarPassword(ReqSolicitarPassword req)
        {
            return new LogSesion().solicitarPassword(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/changePassword")]
        public ResChangePassword changePassword(ReqChangePassword req)
        {
            return new LogSesion().changePassword(req);
        }
    }
}