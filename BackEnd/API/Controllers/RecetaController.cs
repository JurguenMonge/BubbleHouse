using BackEnd.domain;
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
    public class RecetaController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/receta/ingresar")]
        public ResReceta ingresarProducto(ReqReceta req)
        {
            return new LogReceta().ingresarReceta(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/receta/obtener")]
        public ResObtenerRecetas obtenerUsuarios()
        {
            return new LogReceta().obtenerRecetas();
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/receta/eliminar/{id}")]
        public ResReceta eliminarReceta(int id)
        {
            return new LogReceta().eliminarReceta(id);
        }
    }
}