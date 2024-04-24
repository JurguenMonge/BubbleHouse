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
    //Controller producto
    public class ProductoController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/producto/ingresar")]
        public ResProducto ingresarProducto(ReqIngresarProducto req)
        {
            return new LogProducto().ingresarProducto(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/producto/obtener")]
        public ResProducto obtenerProductos()
        {
            return null;
        }
    }
}