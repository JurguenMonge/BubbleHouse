﻿using BackEnd.domain;
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
        public ResFactura ingresar(ReqFactura req)
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
    }
}