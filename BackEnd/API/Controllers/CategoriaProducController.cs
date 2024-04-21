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
    public class CategoriaProducController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/categoriaProducto/ingresar")]
        public ResCategoriaProducto ingresar(ReqCategoriaProducto req)
        {
            return new LogCategoriaProducto().ingresarCategoria(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/categoriaProducto/obtener")]
        public ResObtenerCategoriaProducto obtenerCategoria()
        {
            return new LogCategoriaProducto().obtenerCategoriaProducto();
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/categoriaProducto/modificar")]
        public ResCategoriaProducto modificarCategoria(ReqCategoriaProducto req)
        {
            return new LogCategoriaProducto().modificarCategoria(req);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/categoriaProducto/eliminar/{id}")]
        public ResCategoriaProducto eliminarCategoria(int id)
        {
            return new LogCategoriaProducto().eliminarCategoria(id);
        }

    }
}
