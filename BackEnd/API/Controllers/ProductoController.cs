using BackEnd.domain;
using BackEnd.logic;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace API.Controllers
{
    public class ProductoController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/producto/ingresar")]
        public ResProducto ingresarProducto(ReqIngresarProducto req)
        {
            return new LogProducto().ingresarProducto(req);
        }
    }
}