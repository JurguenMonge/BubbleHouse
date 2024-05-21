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
    public class CategoriaIngredienteController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/categoriaIngrediente/ingresar")]
        public ResCategoriaIngrediente ingresarCategoria(ReqCategoriaIngrediente req)
        {
            return new LogCategoriaIngrediente().ingresarCategoria(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/categoriaIngrediente/obtener")]
        public ResObtenerCategoriaIngrediente obtenerCategoria()
        {
            return new LogCategoriaIngrediente().obtenerCategoriaIngrediente();
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/categoriaIngrediente/modificar")]
        public ResCategoriaIngrediente modificarCategoria(ReqCategoriaIngrediente req)
        {
            return new LogCategoriaIngrediente().modificarCategoria(req);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/categoriaIngrediente/eliminar/{id}")]
        public ResCategoriaIngrediente eliminarCategoria(int id)
        {
            return new LogCategoriaIngrediente().eliminarCategoria(id);
        }
    }
}