using BackEnd.data;
using BackEnd.domain;
using BackEnd.domain.domain;
using BackEnd.domain.request;
using BackEnd.domain.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public class LogFactura
    {
        public ResObtenerFactura obtenerFacturasTodas()
        {
            ResObtenerFactura res = new ResObtenerFactura();
            short tipoRegistro = 0;
            try
            {
                using (ConexionDataContext linq = new ConexionDataContext())
                {
                    var facturaslinq = linq.Obtener_Facturas_Completadas();
                    foreach (var item in facturaslinq)
                    {
                        Factura factura = res.listaFacturas.FirstOrDefault(f => f.idFactura == item.ID_FACTURA);
                        if (factura == null)
                        {
                            factura = factoryArmarFactura(item); // Crea una nueva instancia de factura
                            res.listaFacturas.Add(factura);
                        }

                        ContenedorProductoFactura contenedor = factoryArmarProducto(item); // Crea el contenedor de producto
                        factura.productosList.Add(contenedor); // Agrega el contenedor a la lista de productos de la factura
                    }
                }
                res.Resultado = true;
            }
            catch (Exception ex) { 
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener la lista de ingredientes");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        public ResFactura ingresarFactura(ReqFactura req)
        {
            ResFactura res = new ResFactura();
            short tipoRegistro = 0;
            try
            {
                //Validaciones
                ValidacionesFactura.ValidarFecha(req.Factura, res, ref tipoRegistro);
                ValidacionesFactura.ValidarTotal(req.Factura, res, ref tipoRegistro);
                foreach (ContenedorProductoFactura cont in req.Factura.productosList)
                {
                    ValidacionesFactura.ValidarNumSubTotal(cont, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarDescuento(cont, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarCantidad(cont, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarPrecio(cont.producto, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarProducto(cont.producto, res, ref tipoRegistro);
                }
                if (!res.ListaDeErrores.Any())
                {
                    using (ConexionDataContext linq = new ConexionDataContext())
                    {
                        int? idReturnFactura = 0;
                        int? idReturn = 0;
                        int? idError = 0;
                        bool fallo = false;
                        String errorBD = "";
                        linq.Insertar_Factura((decimal?)req.Factura.numTotal,ref idReturnFactura, ref idError, ref errorBD);
                        if(idError == 0)
                        {
                            foreach (ContenedorProductoFactura cont in req.Factura.productosList)
                            {
                                linq.Insertar_Productos_Factura(cont.producto.idProducto, idReturnFactura, cont.numSubtotal, cont.numCantidad, cont.descuento,
                                    ref idReturn, ref idError, ref errorBD);
                                if(idError != 0)
                                {
                                    fallo = true;
                                    break;
                                }
                            }
                            if(fallo == false)
                            {
                                res.Resultado = true;
                            }
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add("Ocurrió un error al insertar la factura");
                            tipoRegistro = 3;
                        }

                    }
                }
            }
            catch (Exception ex) { 
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener ingresar una factura");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        public ResFactura modificarFactura(ReqFactura req)
        {
            ResFactura res = new ResFactura();
            short tipoRegistro = 0;
            try
            {
                ValidacionesFactura.ValidarFactura(req.Factura, res, ref tipoRegistro);
                ValidacionesFactura.ValidarFecha(req.Factura, res, ref tipoRegistro);
                ValidacionesFactura.ValidarTotal(req.Factura, res, ref tipoRegistro);
                foreach (ContenedorProductoFactura cont in req.Factura.productosList)
                {
                    ValidacionesFactura.ValidarContenedor(cont, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarNumSubTotal(cont, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarDescuento(cont, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarCantidad(cont, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarPrecio(cont.producto, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarProducto(cont.producto, res, ref tipoRegistro);
                }
                if (!res.ListaDeErrores.Any())
                {
                    using (ConexionDataContext linq = new ConexionDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        bool fallo = false;
                        String errorBD = "";
                        linq.Modificar_Factura(req.Factura.idFactura,(decimal?)req.Factura.numTotal, ref idReturn, ref idError, ref errorBD);
                        if (idError == 0)
                        {
                            foreach (ContenedorProductoFactura cont in req.Factura.productosList)
                            {
                                linq.Modificar_Productos_Factura(cont.IdRFacturaProducto, req.Factura.idFactura, cont.numSubtotal, cont.numCantidad, cont.descuento,
                                    ref idReturn, ref idError, ref errorBD);
                                if (idError != 0)
                                {
                                    fallo = true;
                                    break;
                                }
                            }
                            if (fallo == false)
                            {
                                res.Resultado = true;
                            }
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add("Ocurrió un error al insertar la factura");
                            tipoRegistro = 3;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener modificar una factura");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }


        private Factura factoryArmarFactura(Obtener_Facturas_CompletadasResult facturasLinq)
        {
            Factura factura = new Factura();
           
            factura.idFactura = facturasLinq.ID_FACTURA;
            factura.fecha = (DateTime)facturasLinq.FECHA;
            factura.numTotal = (float)facturasLinq.NUM_TOTAL;
            factura.estado = (byte)facturasLinq.ESTADO;
            return factura;
        }
        private ContenedorProductoFactura factoryArmarProducto(Obtener_Facturas_CompletadasResult facturasLinq)
        {
            Producto producto = new Producto();
            ContenedorProductoFactura contenedor = new ContenedorProductoFactura();
            producto.idProducto = facturasLinq.ID_PRODUCTO;
            producto.nombreProducto = facturasLinq.DSC_NOMBRE_PRODUCTO;
            producto.descripcion = facturasLinq.DSC_DESCRIPCION;
            producto.urlImgen = facturasLinq.DSC_URL_IMAGEN;
            producto.precio = (float)facturasLinq.NUM_PRECIO;
            producto.estado = true;
            if(facturasLinq.ID_RECETA != 0)
            {///////Codigo de receta faltante
                Receta receta = new Receta();
                producto.receta = receta;
            }
            contenedor.producto = producto;
            contenedor.IdRFacturaProducto = facturasLinq.ID_R_FACTURA_PRODUCTO;
            contenedor.idFactura = facturasLinq.ID_FACTURA;
            contenedor.numSubtotal = (decimal)facturasLinq.NUM_SUBTOTAL;
            contenedor.descuento = (decimal)facturasLinq.NUM_DESCUENTO;
            contenedor.numCantidad = (int)facturasLinq.NUM_CANTIDAD;
            return contenedor;
        }
    }
}
