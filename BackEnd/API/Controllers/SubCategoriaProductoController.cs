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
    public class SubCategoriaProductoController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/subCategoriaProducto/ingresar")]
        public ResSubCategoriaProducto ingresarSubCategoria(ReqSubCategoriaProducto req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogSubCategoriaProducto().ingresarSubCategoria(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
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
            ValidacionesSesion vali = new ValidacionesSesion();
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogSubCategoriaProducto().modificarSubCategoria(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/subCategoriaProducto/eliminar")]
        public ResSubCategoriaProducto eliminarSubCategoria(ReqSubCategoriaProducto req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogSubCategoriaProducto().eliminarSubCategoria(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }
    }
}