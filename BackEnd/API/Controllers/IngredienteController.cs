using BackEnd.domain.request;
using BackEnd.domain.response;
using BackEnd.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class IngredienteController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ingrediente/ingresar")]
        public ResIngrediente ingresarIngrediente(ReqIngrediente req)
        {
            return new LogIngrediente().ingresarIngrediente(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/ingrediente/obtener")]
        public ResObtenerIngredientes obtenerCategoria()
        {
            return new LogIngrediente().obtenerIngrediente();
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/ingrediente/modificar")]
        public ResIngrediente modificarCategoria(ReqIngrediente req)
        {
            return new LogIngrediente().modificarIngrediente(req);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/ingrediente/eliminar/{id}")]
        public ResIngrediente eliminarCategoria(int id)
        {
            return new LogIngrediente().eliminarIngrediente(id);
        }
    }
}