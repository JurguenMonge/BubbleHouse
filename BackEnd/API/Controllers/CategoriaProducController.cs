using BackEnd.domain;
using BackEnd.logic;
using BackEnd.utils;
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
            ValidacionesSesion vali = new ValidacionesSesion();
            ResCategoriaProducto res = new ResCategoriaProducto();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogCategoriaProducto().ingresarCategoria(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
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
            ValidacionesSesion vali = new ValidacionesSesion();
            ResCategoriaProducto res = new ResCategoriaProducto();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogCategoriaProducto().modificarCategoria(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/categoriaProducto/eliminar")]
        public ResCategoriaProducto eliminarCategoria(ReqCategoriaProducto req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResCategoriaProducto res = new ResCategoriaProducto();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogCategoriaProducto().eliminarCategoria(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

    }
}
