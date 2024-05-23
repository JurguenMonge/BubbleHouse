using BackEnd.data;
using BackEnd.domain.request;
using BackEnd.domain.response;
using BackEnd.domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public class LogSubCategoriaProducto
    {

        //Insertar SubCategoria producto
        public ResSubCategoriaProducto ingresarSubCategoria(ReqSubCategoriaProducto req)
        {
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req != null)
                {

                    ValidacionesSubCategoriaProducto.ValidarNombreSubCategoria(req.SubCategoriaProducto, res, ref tipoRegistro);

                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";


                        linq.Insertar_SubCategoria_Producto(req.SubCategoriaProducto.cateProductoId, req.SubCategoriaProducto.dscNombreSubCategoria, ref idReturn, ref idError, ref errorBD);
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
                    res.ListaDeErrores.Add("No se enviaron los datos correctamente");
                    tipoRegistro = 2;
                }
            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al insertar la subcategoría");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Obtener la lista de subcategorias de producto
        public ResObtenerSubCategoriaProducto obtenerSubCategoriaProducto()
        {
            ResObtenerSubCategoriaProducto res = new ResObtenerSubCategoriaProducto();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error inesperado
            try
            {
                ConexionDataContext linq = new ConexionDataContext();
                var linqSubCategoriasProducto = linq.Obtener_SubCate_Productos_Activos().ToList();

                foreach (var item in linqSubCategoriasProducto)
                {
                    SubcategoriaProducto subCategoriaProducto = factoryArmarSubCategoriaProducto(item);
                    if (subCategoriaProducto != null)
                    {
                        res.Resultado = true;
                        res.listaSubCategoriaProducto.Add(subCategoriaProducto);
                    }
                }

            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener la lista de subcategorías");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Modificar subcategoria producto
        public ResSubCategoriaProducto modificarSubCategoria(ReqSubCategoriaProducto req)
        {
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req != null)
                {

                    ValidacionesSubCategoriaProducto.ValidarNombreSubCategoria(req.SubCategoriaProducto, res, ref tipoRegistro);

                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Modificar_SubCategoria_Producto(req.SubCategoriaProducto.idSubcategoriaProducto, req.SubCategoriaProducto.cateProductoId, req.SubCategoriaProducto.dscNombreSubCategoria, ref idReturn, ref idError, ref errorBD);
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
                    res.ListaDeErrores.Add("No se enviaron los datos correctamente");
                    tipoRegistro = 2;
                }
            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al modificar la subcategoría");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }


        //Eliminar una subcategoría
        public ResSubCategoriaProducto eliminarSubCategoria(ReqSubCategoriaProducto req)
        {
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req.SubCategoriaProducto.idSubcategoriaProducto != 0)
                {

                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Eliminar_SubCategoria_Producto(req.SubCategoriaProducto.idSubcategoriaProducto, ref idReturn, ref idError, ref errorBD);
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
                    res.ListaDeErrores.Add("No se envió una categoría valida");
                    tipoRegistro = 2;
                }
            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al eliminar la subcategoría");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Armar la categoria para obtener la lista
        private SubcategoriaProducto factoryArmarSubCategoriaProducto(Obtener_SubCate_Productos_ActivosResult subCategoriaLinq)
        {
            SubcategoriaProducto subCategoriaProduccto = new SubcategoriaProducto();
            subCategoriaProduccto.idSubcategoriaProducto = subCategoriaLinq.ID_SUBCATE_PRODUCTO;
            subCategoriaProduccto.dscNombreSubCategoria = subCategoriaLinq.DSC_NOMBRE_SUBCATEGORIA;
            subCategoriaProduccto.cateProductoId = (int)subCategoriaLinq.ID_CATE_PRODUCTO_ID;
            subCategoriaProduccto.dscNombreCategoria = subCategoriaLinq.DSC_NOMBRE_CATEGORIA;
            subCategoriaProduccto.estado = true;
            return subCategoriaProduccto;
        }
    }
}
