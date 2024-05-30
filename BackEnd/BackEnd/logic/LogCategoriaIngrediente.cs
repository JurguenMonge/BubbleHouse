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
    public class LogCategoriaIngrediente
    {
        //Insertar Categoria ingrediente
        public ResCategoriaIngrediente ingresarCategoria(ReqCategoriaIngrediente req)
        {
            ResCategoriaIngrediente res = new ResCategoriaIngrediente();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req != null)
                {

                    ValidacionesCategoriaIngrediente.ValidarNombreCategoria(req.CategoriaIngrediente, res, ref tipoRegistro);

                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";


                        linq.Insertar_Categoria_Ingrediente(req.CategoriaIngrediente.dscNombreCategoria, ref idReturn, ref idError, ref errorBD);
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

        //Obtener la lista de categorias de ingredientes
        public ResObtenerCategoriaIngrediente obtenerCategoriaIngrediente()
        {
            ResObtenerCategoriaIngrediente res = new ResObtenerCategoriaIngrediente();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error inesperado
            try
            {
                ConexionDataContext linq = new ConexionDataContext();
                var linqRoles = linq.Obtener_Cate_Ingredientes_Activos().ToList();

                foreach (var item in linqRoles)
                {
                    CategoriaIngrediente categoriaIngrediente = factoryArmarCategoriaIngrediente(item);
                    if (categoriaIngrediente != null)
                    {
                        res.Resultado = true;
                        res.listaCategoriaIngrediente.Add(categoriaIngrediente);
                    }
                }

            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener la lista de categorías");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Modificar categoria ingrediente
        public ResCategoriaIngrediente modificarCategoria(ReqCategoriaIngrediente req)
        {
            ResCategoriaIngrediente res = new ResCategoriaIngrediente();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req != null)
                {

                    ValidacionesCategoriaIngrediente.ValidarNombreCategoria(req.CategoriaIngrediente, res, ref tipoRegistro);

                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Modificar_Categoria_Ingrediente(req.CategoriaIngrediente.idCateIngrediente, req.CategoriaIngrediente.dscNombreCategoria, ref idReturn, ref idError, ref errorBD);
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
                res.ListaDeErrores.Add("Ocurrió un error al modificar la categoría");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }


        //Eliminar una categoría de ingrediente
        public ResCategoriaIngrediente eliminarCategoria(ReqCategoriaIngrediente req)
        {
            ResCategoriaIngrediente res = new ResCategoriaIngrediente();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req.CategoriaIngrediente.idCateIngrediente != 0)
                {

                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Eliminar_Categoria_Ingrediente(req.CategoriaIngrediente.idCateIngrediente, ref idReturn, ref idError, ref errorBD);
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
                res.ListaDeErrores.Add("Ocurrió un error al eliminar la categoría");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Armar la categoria para obtener la lista
        private CategoriaIngrediente factoryArmarCategoriaIngrediente(Obtener_Cate_Ingredientes_ActivosResult categoriaLinq)
        {
            CategoriaIngrediente categoriaIngrediente = new CategoriaIngrediente();
            categoriaIngrediente.idCateIngrediente = categoriaLinq.ID_CATE_INGREDIENTE;
            categoriaIngrediente.dscNombreCategoria = categoriaLinq.DSC_NOMBRE_CATEGORIA;
            categoriaIngrediente.estadoCate = true;
            return categoriaIngrediente;
        }
    }
}
