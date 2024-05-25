using BackEnd.data;
using BackEnd.domain;
using BackEnd.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public class LogProducto
    {

        public ResProducto ingresarProducto(ReqIngresarProducto req)
        {
            ResProducto res = new ResProducto();
            short tipoRegistro = 0;
            try
            {
                if (req != null)
                {
                    ValidacionProducto.ValidarSubCategoria(req.Producto, res,ref tipoRegistro);
                    ValidacionProducto.ValidarNombre(req.Producto, res, ref tipoRegistro);
                    ValidacionProducto.ValidarDescripcion(req.Producto, res, ref tipoRegistro);
                    ValidacionProducto.ValidarUrlImagen(req.Producto, res, ref tipoRegistro);
                    ValidacionProducto.ValidarPrecio(req.Producto, res, ref tipoRegistro);
                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Insertar_Producto(req.Producto.subcategoriaProducto.idSubcategoriaProducto,req.Producto.nombreProducto,
                            req.Producto.descripcion,req.Producto.urlImgen,req.Producto.precio,ref idReturn, ref idError, ref errorBD);
                        if (idError == 0)
                        {
                            res.Resultado = true;
                            tipoRegistro = 1;
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaDeErrores.Add(errorBD);
                            tipoRegistro = 2;
                        }
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("No se enviaron correctamente los datos");
                    tipoRegistro = 2;
                }
            }catch (Exception) {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al insertar el producto");
                tipoRegistro = 3;
            }
            finally {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }
        /*
        public ResObtenerProducto obtenerProductos()
        {
            ResObtenerProducto res = new ResObtenerProducto();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error inesperado
            try
            {
                ConexionDataContext linq = new ConexionDataContext();
                int? idError = 0;
                String errorBD = "";
                var linqReceta = linq.Obtener_Productos_Activos();
                if (idError == 0)
                {
                    res.Resultado = true;
                    tipoRegistro = 1;
                    foreach (var item in linqReceta)
                    {
                        RecetaCompleta receta = factoryArmarReceta(item);
                        if (receta != null)
                        {
                            res.listaRecetas.Add(receta);
                        }
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("Ocurrió un error en la base de datos, intentalo más tarde");
                    tipoRegistro = 2;
                }
            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener la lista de recetas");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }


            return res;
        }
        */
    }
}
