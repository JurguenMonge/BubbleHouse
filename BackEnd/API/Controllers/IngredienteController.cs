using BackEnd.domain.request;
using BackEnd.domain.response;
using BackEnd.logic;
using BackEnd.utils;
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
            ValidacionesSesion vali = new ValidacionesSesion();
            ResIngrediente res = new ResIngrediente();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogIngrediente().ingresarIngrediente(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
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
            ValidacionesSesion vali = new ValidacionesSesion();
            ResIngrediente res = new ResIngrediente();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogIngrediente().modificarIngrediente(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/ingrediente/eliminar")]
        public ResIngrediente eliminarCategoria(ReqIngrediente req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResIngrediente res = new ResIngrediente();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogIngrediente().eliminarIngrediente(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }
    }
}