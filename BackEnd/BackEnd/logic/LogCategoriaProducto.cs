using BackEnd.data;
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
    public class LogCategoriaProducto
    {
        //Insertar Categoria producto
        public ResCategoriaProducto ingresarCategoria(ReqCategoriaProducto req)
        {
            ResCategoriaProducto res = new ResCategoriaProducto();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req != null)
                {

                    ValidacionesCategoriaProducto.ValidarNombreCategoria(req.CategoriaProducto, res, ref tipoRegistro);

                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                       

                        linq.Insertar_Categoria_Producto(req.CategoriaProducto.dscNombreCategoria, ref idReturn, ref idError, ref errorBD);
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
                res.ListaDeErrores.Add("Ocurrió un error al insertar la categoria");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Obtener la lista de categorias de producto
        public ResObtenerCategoriaProducto obtenerCategoriaProducto()
        {
            ResObtenerCategoriaProducto res = new ResObtenerCategoriaProducto();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error inesperado
            try
            {
                ConexionDataContext linq = new ConexionDataContext();
                var linqRoles = linq.Obtener_Cate_Productos_Activos().ToList();
                
                foreach (var item in linqRoles)
                {
                    CategoriaProducto categoriaProducto = factoryArmarCategoriaProducto(item);
                    if (categoriaProducto != null)
                    {
                        res.listaCategoriaProducto.Add(categoriaProducto);
                    }
                }
                res.Resultado = true;
                
            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener la lista de categorias");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Modificar categoria producto
        public ResCategoriaProducto modificarCategoria(ReqCategoriaProducto req)
        {
            ResCategoriaProducto res = new ResCategoriaProducto();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req != null)
                {

                    ValidacionesCategoriaProducto.ValidarNombreCategoria(req.CategoriaProducto, res, ref tipoRegistro);

                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Modificar_Categoria_Producto(req.CategoriaProducto.idCategoriaProducto, req.CategoriaProducto.dscNombreCategoria, ref idReturn, ref idError, ref errorBD);
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
                res.ListaDeErrores.Add("Ocurrió un error al modificar la categoria");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }


        //Eliminar un usuario
        public ResCategoriaProducto eliminarCategoria(ReqCategoriaProducto req)
        {
            ResCategoriaProducto res = new ResCategoriaProducto();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req.CategoriaProducto.idCategoriaProducto != 0)
                {

                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Eliminar_Categoria_Producto(req.CategoriaProducto.idCategoriaProducto, ref idReturn, ref idError, ref errorBD);
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
                    res.ListaDeErrores.Add("No se envió una categoria valida");
                    tipoRegistro = 2;
                }
            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al eliminar la categoria");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Armar la categoria para obtener la lista
        private CategoriaProducto factoryArmarCategoriaProducto(Obtener_Cate_Productos_ActivosResult categoriaLinq)
        {
            CategoriaProducto categoriaProduccto = new CategoriaProducto();
            categoriaProduccto.idCategoriaProducto = categoriaLinq.ID_CATE_PRODUCTO;
            categoriaProduccto.dscNombreCategoria = categoriaLinq.DSC_NOMBRE_CATEGORIA;
            categoriaProduccto.estado = true;
            return categoriaProduccto;
        }

    }
}
