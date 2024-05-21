using BackEnd.domain.request;
using BackEnd.domain.response;
using BackEnd.domain;
using BackEnd.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class SubCategoriaProductoController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/subCategoriaProducto/ingresar")]
        public ResSubCategoriaProducto ingresarSubCategoria(ReqSubCategoriaProducto req)
        {
            return new LogSubCategoriaProducto().ingresarSubCategoria(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/subCategoriaProducto/obtener")]
        public ResObtenerSubCategoriaProducto obtenerSubCategoria()
        {
            return new LogSubCategoriaProducto().obtenerSubCategoriaProducto();
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/subCategoriaProducto/modificar")]
        public ResSubCategoriaProducto modificarSubCategoria(ReqSubCategoriaProducto req)
        {
            return new LogSubCategoriaProducto().modificarSubCategoria(req);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/subCategoriaProducto/eliminar/{id}")]
        public ResSubCategoriaProducto eliminarSubCategoria(int id)
        {
            return new LogSubCategoriaProducto().eliminarSubCategoria(id);
        }
    }
}