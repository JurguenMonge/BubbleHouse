using BackEnd.domain;
using BackEnd.domain.request;
using BackEnd.domain.response;
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
    public class FacturaController : ApiController
    {
        // GET: Factura
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/factura/ingresar")]
        public ResFactura ingresarFactura(ReqFactura req)
        {
            return new LogFactura().ingresarFactura(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/factura/obtenerCompletadas")]
        public ResObtenerFactura obtenerFacturasCompletas(ReqFactura req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResObtenerFactura res = new ResObtenerFactura();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogFactura().obtenerFacturasTodas();
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/factura/modificar")]
        public ResFactura modificarFactura(ReqFactura req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResFactura res = new ResFactura();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogFactura().modificarFactura(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/factura/eliminar")]
        public ResFactura eliminarFactura(ReqFactura req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResFactura res = new ResFactura();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogFactura().eliminarFactura(req) ;
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/productofactura/ingresar")]
        public ResFactura ingresarProducto(ReqContenedorProducto req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResFactura res = new ResFactura();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogFactura().ingresarProductoaFactura(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/productofactura/modificar")]
        public ResFactura modificarProducto(ReqContenedorProducto req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResFactura res = new ResFactura();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogFactura().modificarProductoaFactura(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/productofactura/eliminar")]
        public ResFactura eliminarProducto(ReqContenedorProducto req)
        {
            ValidacionesSesion vali = new ValidacionesSesion();
            ResFactura res = new ResFactura();
            if (vali.validarSesionyRolAdmin(req.idSesion))
            {
                res = new LogFactura().EliminarProductoaFactura(req);
            }
            else
            {
                res.ListaDeErrores.Add("Sesion o rol invalido");
            }
            return res;
        }
    }
}