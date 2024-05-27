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
    public class LogIngrediente
    {
        //Insertar Ingrediente
        public ResIngrediente ingresarIngrediente(ReqIngrediente req)
        {
            ResIngrediente res = new ResIngrediente();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req != null)
                {
                    ValidacionesIngrediente.ValidarCategoria(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarNombre(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarCategoria(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarDescripcion(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarUrlImagen(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarPrecio(req.Ingrediente, res, ref tipoRegistro);

                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";


                        linq.Insertar_Ingrediente(req.Ingrediente.idCategoriaIngrediente, req.Ingrediente.dscNombre, req.Ingrediente.dscDescripcion, req.Ingrediente.dscURLImagen, req.Ingrediente.numPrecio, ref idReturn, ref idError, ref errorBD);
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
                res.ListaDeErrores.Add("Ocurrió un error al insertar el ingrediente");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Obtener la lista de ingedientes de producto
        public ResObtenerIngredientes obtenerIngrediente()
        {
            ResObtenerIngredientes res = new ResObtenerIngredientes();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error inesperado
            try
            {
                ConexionDataContext linq = new ConexionDataContext();
                var linqIngrediente = linq.Obtener_Ingredientes_Activos().ToList();

                foreach (var item in linqIngrediente)
                {
                    Ingrediente ingrediente = factoryArmarIngrediente(item);
                    if (ingrediente != null)
                    {
                        res.Resultado = true;
                        res.ListaIngredientes.Add(ingrediente);
                    }
                }

            }
            catch (Exception)
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

        //Modificar subcategoria producto
        public ResIngrediente modificarIngrediente(ReqIngrediente req)
        {
            ResIngrediente res = new ResIngrediente();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req != null)
                {

                    ValidacionesIngrediente.ValidarCategoria(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarNombre(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarCategoria(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarDescripcion(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarUrlImagen(req.Ingrediente, res, ref tipoRegistro);
                    ValidacionesIngrediente.ValidarPrecio(req.Ingrediente, res, ref tipoRegistro);

                    if (!res.ListaDeErrores.Any())
                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Modificar_Ingrediente(req.Ingrediente.idIngrediente, req.Ingrediente.idCategoriaIngrediente, req.Ingrediente.dscNombre, req.Ingrediente.dscDescripcion, req.Ingrediente.dscURLImagen, req.Ingrediente.numPrecio, ref idReturn, ref idError, ref errorBD);
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
                res.ListaDeErrores.Add("Ocurrió un error al modificar el ingrediente");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }


        //Eliminar una subcategoría
        public ResIngrediente eliminarIngrediente(ReqIngrediente req)
        {
            ResIngrediente res = new ResIngrediente();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (req.Ingrediente.idIngrediente != 0)
                {

                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Eliminar_Ingrediente(req.Ingrediente.idIngrediente, ref idReturn, ref idError, ref errorBD);
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
                res.ListaDeErrores.Add("Ocurrió un error al eliminar el ingrediente");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        //Armar la categoria para obtener la lista
        private Ingrediente factoryArmarIngrediente(Obtener_Ingredientes_ActivosResult ingredienteLink)
        {
            Ingrediente ingrediente = new Ingrediente();
            ingrediente.idIngrediente = ingredienteLink.ID_INGREDIENTE;
            ingrediente.idCategoriaIngrediente = ingredienteLink.ID_CATE_INGREDIENTE;
            ingrediente.dscNombre = ingredienteLink.DSC_NOMBRE_INGREDIENTE;
            ingrediente.dscNombreCategoriaIngrediente = ingredienteLink.DSC_NOMBRE_CATEGORIA;
            ingrediente.dscDescripcion = ingredienteLink.DSC_DESCRIPCION;
            ingrediente.dscURLImagen = ingredienteLink.DSC_URL_IMAGEN;
            ingrediente.numPrecio = (decimal)ingredienteLink.NUM_PRECIO;
            

            return ingrediente;
        }
    }
}