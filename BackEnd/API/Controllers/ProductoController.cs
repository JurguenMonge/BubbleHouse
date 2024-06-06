using BackEnd.domain;
using BackEnd.domain.response;
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/producto/obtener")]
        public ResObtenerProducto obtenerProductos()
        {
            return new LogProducto().obtenerProductos();
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/producto/eliminar/{id}")]
        public ResProducto eliminarReceta(int id)
        {
            return new LogProducto().eliminarProducto(id);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/producto/modificar")]
        public ResProducto modificarUsuario(ReqIngresarProducto req)
        {
            return new LogProducto().modificarProducto(req);
        }
    }
}