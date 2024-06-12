using BackEnd.data;
using BackEnd.domain;
using BackEnd.domain.domain;
using BackEnd.domain.request;
using BackEnd.domain.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BackEnd.logic
{
    public class LogFactura
    {
        public ResObtenerFactura obtenerFacturasCompletadas(int estado)
        {
            ResObtenerFactura res = new ResObtenerFactura();
            short tipoRegistro = 0;
            try
            {
                using (ConexionDataContext linq = new ConexionDataContext())
                {
                    var facturaslinq = linq.Obtener_Facturas((byte?)estado);
                    foreach (var item in facturaslinq)
                    {
                        Factura factura = res.listaFacturas.FirstOrDefault(f => f.idFactura == item.ID_FACTURA);
                        if (factura == null)
                        {
                            factura = factoryArmarFactura(item); // Crea una nueva instancia de factura
                            res.listaFacturas.Add(factura);
                        }
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

        public ResObtenerProductosFactura obtenerProductosFactura(Factura fac)
        {
            ResObtenerProductosFactura res = new ResObtenerProductosFactura();
            short tipoRegistro = 0;
            try
            {
                using (ConexionDataContext linq = new ConexionDataContext())
                {
                    var facturaslinq = linq.Obtener_Productos_Facturas((byte?)fac.idFactura);
                    foreach (var item in facturaslinq)
                    {
                        ContenedorProducto contenedor = factoryArmarProducto(item);
                        res.Contenedores.Add(contenedor);
                    }
                }
                res.Resultado = true;
            }
            catch (Exception ex)
            {
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
                            else
                            {
                                res.Resultado = false;
                                res.ListaDeErrores.Add("Ocurrió un problema al registrar un producto");
                                tipoRegistro = 3;
                            }
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add("Ocurrió un error al modificar la factura");
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

        public ResFactura modificarEstadoFactura(ReqFactura req, int estado)
        {
            ResFactura res = new ResFactura();
            short tipoRegistro = 0;
            try
            {
                ValidacionesFactura.ValidarFactura(req.Factura, res, ref tipoRegistro);
                ValidacionesFactura.ValidarFecha(req.Factura, res, ref tipoRegistro);
                ValidacionesFactura.ValidarTotal(req.Factura, res, ref tipoRegistro);
                if (!res.ListaDeErrores.Any())
                {
                    using (ConexionDataContext linq = new ConexionDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        bool fallo = false;
                        String errorBD = "";
                        linq.Modificar_Estado_Factura(req.Factura.idFactura, (decimal?)req.Factura.numTotal, (byte?)estado, ref idReturn, ref idError, ref errorBD);
                        if (idError == 0)
                        {
                            res.Resultado = true;
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add("Ocurrió un error al modificar la factura");
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

        public ResFactura eliminarFactura(ReqFactura req)
        {
            ResFactura res = new ResFactura();
            short tipoRegistro = 0;
            try
            {
                ValidacionesFactura.ValidarFactura(req.Factura, res, ref tipoRegistro);
                if (!res.ListaDeErrores.Any())
                {
                    using (ConexionDataContext linq = new ConexionDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        bool fallo = false;
                        linq.Eliminar_Factura(req.Factura.idFactura, ref idReturn, ref idError, ref errorBD);
                        if (idError == 0)
                        {
                            foreach (ContenedorProductoFactura cont in req.Factura.productosList)
                            {
                                linq.Eliminar_Producto_Factura(cont.IdRFacturaProducto, ref idReturn, ref idError, ref errorBD);
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
                            else
                            {
                                res.Resultado = false;
                                res.ListaDeErrores.Add("Ocurrió un problema al eliminar un producto");
                                tipoRegistro = 3;
                            }
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add("Ocurrió un error al eliminar la factura");
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
        public ResFactura ingresarProductoaFactura(ReqContenedorProducto req)
        {
            ResFactura res = new ResFactura();
            short tipoRegistro = 0;
            try
            {
                    ValidacionesFactura.ValidarNumSubTotal(req.contenedor, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarDescuento(req.contenedor, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarCantidad(req.contenedor, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarPrecio(req.contenedor.producto, res, ref tipoRegistro);
                    ValidacionesFactura.ValidarProducto(req.contenedor.producto, res, ref tipoRegistro);
                if (!res.ListaDeErrores.Any())
                {
                    using (ConexionDataContext linq = new ConexionDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                                linq.Insertar_Productos_Factura(req.contenedor.producto.idProducto, req.contenedor.idFactura, req.contenedor.numSubtotal, req.contenedor.numCantidad, req.contenedor.descuento,
                                    ref idReturn, ref idError, ref errorBD);
                            if (idError == 0)
                            {
                                res.Resultado = true;
                            }
                            else
                            {
                                res.Resultado = false;
                                res.ListaDeErrores.Add("Ocurrió un error al ingresar el producto");
                                tipoRegistro = 3;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener ingresar el producto");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }
        public ResFactura modificarProductoaFactura(ReqContenedorProducto req)
        {
            ResFactura res = new ResFactura();
            short tipoRegistro = 0;
            try
            {
                ValidacionesFactura.ValidarNumSubTotal(req.contenedor, res, ref tipoRegistro);
                ValidacionesFactura.ValidarDescuento(req.contenedor, res, ref tipoRegistro);
                ValidacionesFactura.ValidarCantidad(req.contenedor, res, ref tipoRegistro);
                ValidacionesFactura.ValidarPrecio(req.contenedor.producto, res, ref tipoRegistro);
                ValidacionesFactura.ValidarProducto(req.contenedor.producto, res, ref tipoRegistro);
                if (!res.ListaDeErrores.Any())
                {
                    using (ConexionDataContext linq = new ConexionDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Modificar_Productos_Factura(req.contenedor.IdRFacturaProducto, req.contenedor.idFactura, req.contenedor.numSubtotal, req.contenedor.numCantidad, req.contenedor.descuento,
                            ref idReturn, ref idError, ref errorBD);
                        if (idError == 0)
                        {
                            res.Resultado = true;
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add("Ocurrió un error al modificar el producto");
                            tipoRegistro = 3;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener modificar un producto");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }
        public ResFactura EliminarProductoaFactura(ReqContenedorProducto req)
        {
            ResFactura res = new ResFactura();
            short tipoRegistro = 0;
            try
            {
                ValidacionesFactura.ValidarContenedor(req.contenedor, res, ref tipoRegistro);
                if (!res.ListaDeErrores.Any())
                {
                    using (ConexionDataContext linq = new ConexionDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Eliminar_Producto_Factura(req.contenedor.IdRFacturaProducto, ref idReturn, ref idError, ref errorBD);
                        if (idError == 0)
                        {
                            res.Resultado = true;
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add("Ocurrió un error al eliminar el producto");
                            tipoRegistro = 3;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener eliminar el producto");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }


        private Factura factoryArmarFactura(Obtener_FacturasResult facturasLinq)
        {
            TimeZoneInfo zonaHorariaServidor = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            Factura factura = new Factura();
           
            factura.idFactura = facturasLinq.ID_FACTURA;
            factura.fecha = (DateTime)facturasLinq.FECHA;
            DateTime fechaServidor = TimeZoneInfo.ConvertTimeFromUtc(factura.fecha, zonaHorariaServidor);
            factura.fecha = fechaServidor;
            factura.numTotal = (float)facturasLinq.NUM_TOTAL;
            factura.estado = (byte)facturasLinq.ESTADO;
            return factura;
        }
        private ContenedorProducto factoryArmarProducto(Obtener_Productos_FacturasResult facturasLinq)
        {
            ContenedorProducto contenedor = new ContenedorProducto();

            contenedor.idProducto = facturasLinq.ID_PRODUCTO;
            contenedor.nombreProducto = facturasLinq.DSC_NOMBRE_PRODUCTO;
            contenedor.descripcion = facturasLinq.DSC_DESCRIPCION;
            contenedor.urlImgen = facturasLinq.DSC_URL_IMAGEN;
            contenedor.precio = (float)facturasLinq.NUM_PRECIO;
            contenedor.estado = true;
            if(facturasLinq.ID_RECETA != 0)
            {///////Codigo de receta faltante
                try
                {
                    ConexionDataContext linq = new ConexionDataContext();
                    int? idErrorRece = 0;
                    String errorBDRece = "";
                    var linqReceta = linq.Obtener_Receta_ById(facturasLinq.ID_RECETA, ref idErrorRece, ref errorBDRece);
                    if (idErrorRece == 0)
                    {
                        foreach (var item in linqReceta)
                        {
                            Receta receta = factoryArmarRecetaByid(item);
                            if (receta != null)
                            {
                                contenedor.informacionReceta = receta.dscNombre;
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
            contenedor.IdRFacturaProducto = facturasLinq.ID_R_FACTURA_PRODUCTO;
            contenedor.idFactura = facturasLinq.ID_FACTURA;
            contenedor.numSubtotal = (decimal)facturasLinq.NUM_SUBTOTAL;
            contenedor.descuento = (decimal)facturasLinq.NUM_DESCUENTO;
            contenedor.numCantidad = (int)facturasLinq.NUM_CANTIDAD;
            return contenedor;
        }
        private Receta factoryArmarRecetaByid(Obtener_Receta_ByIdResult recetaLinq)
        {
            Receta receta = new Receta();
            receta.idReceta = recetaLinq.ID_RECETA;
            receta.dscNombre = recetaLinq.Ingredientes;
            receta.dscNombre = receta.dscNombre.Replace(", ", Environment.NewLine);
            return receta;
        }
    }
}
