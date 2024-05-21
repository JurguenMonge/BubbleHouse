using BackEnd.domain.request;
using BackEnd.domain.response;
using BackEnd.domain;
using BackEnd.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BackEnd.utils;

namespace API.Controllers
{
    public class CategoriaIngredienteController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/categoriaIngrediente/ingresar")]
        public ResCategoriaIngrediente ingresarCategoria(ReqCategoriaIngrediente req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResCategoriaIngrediente res = new ResCategoriaIngrediente();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogCategoriaIngrediente().ingresarCategoria(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
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
            ValidacionesSesion vali = new ValidacionesSesion();
            ResCategoriaIngrediente res = new ResCategoriaIngrediente();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogCategoriaIngrediente().modificarCategoria(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/categoriaIngrediente/eliminar")]
        public ResCategoriaIngrediente eliminarCategoria(ReqCategoriaIngrediente req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResCategoriaIngrediente res = new ResCategoriaIngrediente();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogCategoriaIngrediente().eliminarCategoria(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }
    }
}