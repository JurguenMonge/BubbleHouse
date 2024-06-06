using BackEnd.data;
using BackEnd.domain;
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
    public class LogReceta
    {
        public ResReceta validaciones(ReqReceta req)
        {
            ResReceta res = new ResReceta();
            
            ValidacionesGenericas validacionesGenericas = new ValidacionesGenericas();
            var result = validacionesGenericas.validarString(req.Producto.receta.dscNombre);
            if (result == 1)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Nombre faltante");
            }
            else if (result == 2)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("El Nombre no debe contener caracteres especiales");
            }
            return res;
        }


        public ResReceta ingresarReceta(ReqReceta req)
        {
            short tipoRegistro = 0;
            ResReceta res = new ResReceta();
            
            try
            {
                if (req != null)
                {
                    ConexionDataContext linq = new ConexionDataContext();
                    int? idReturn = 0;
                    int? idError = 0;
                    String errorBD = "";
                    var permisos = true;
                    if(permisos)
                    {
                        res = validaciones(req);
                        if (!res.ListaDeErrores.Any())
                        {
                            idReturn = 0;
                            idError = 0;
                            errorBD = "";
                            linq.Insertar_Receta(req.Producto.receta.dscNombre,ref idReturn, ref idError, ref errorBD);
                            if (idError == 0)
                            {
                                int? idReturnIng = 0;
                                int? idErrorIng = 0;
                                String errorBDIng = "";
                                ResIngrediente resInge = new ResIngrediente();
                                foreach (Ingrediente ing in req.Producto.receta.listaIngrediente)
                                {
                                    ValidacionesIngrediente.ValidarCategoria(ing,resInge,ref tipoRegistro);
                                    ValidacionesIngrediente.ValidarNombre(ing, resInge, ref tipoRegistro);
                                    ValidacionesIngrediente.ValidarDescripcion(ing, resInge, ref tipoRegistro);
                                    ValidacionesIngrediente.ValidarUrlImagen(ing, resInge, ref tipoRegistro);
                                    ValidacionesIngrediente.ValidarPrecio(ing, resInge, ref tipoRegistro);
                                    if (!res.ListaDeErrores.Any())
                                    {
                                        linq.Insertar_Receta_Ingrediente(idReturn,ing.idIngrediente, 
                                            ref idReturnIng, ref idErrorIng, ref errorBDIng);
                                        if (idErrorIng != 0)
                                        {
                                            res.Resultado = false;
                                            res.ListaDeErrores.Add(errorBDIng);
                                            tipoRegistro = 2;
                                            break;
                                        }
                                    }
                                }
                                if (idErrorIng == 0)
                                {
                                    ReqIngresarProducto reqPro = new ReqIngresarProducto();
                                    reqPro.Producto = req.Producto;
                                    reqPro.Producto.receta.idReceta = (int)idReturn;
                                    ResProducto resPro = new LogProducto().ingresarProducto(reqPro);
                                    if(resPro.Resultado == true)
                                    {
                                        res.Resultado = true;
                                    }
                                    else
                                    {
                                        res.Resultado = false;
                                        res.ListaDeErrores.Add("Sucedio un error al crear el producto");
                                    }
                                }
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
                        res.ListaDeErrores.Add("Permisos insuficientes");
                        tipoRegistro = 2;
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("No se enviaron los datos correctamente");
                    tipoRegistro = 2;
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Error interno, intente mas tarde" + ex);
                tipoRegistro = 2;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            
            return res; 
        }

        public ResReceta eliminarReceta(int id)
        {
            ResReceta res = new ResReceta();
            
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error no controlado
            try
            {
                if (id != 0)
                {

                    {
                        ConexionDataContext linq = new ConexionDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        String errorBD = "";
                        linq.Eliminar_Receta(id, ref idReturn, ref idError, ref errorBD);
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
                    res.ListaDeErrores.Add("No se envió una receta valida");
                    tipoRegistro = 2;
                }
            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al eliminar la receta");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            
            return res;
        }

        public ResObtenerRecetas obtenerRecetas()
        {
            ResObtenerRecetas res = new ResObtenerRecetas();
            
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error inesperado
            try
            {
                ConexionDataContext linq = new ConexionDataContext();
                int? idError = 0;
                String errorBD = "";
                var linqReceta = linq.Obtener_Receta(ref idError, ref errorBD);
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


        private Rol factoryArmarRol(Obtener_SesionResult usuarioLinq)
        {
            Rol rol = new Rol();
            rol.tipoRol = usuarioLinq.DSC_TIPO_ROL;
            return rol;
        }
        private RecetaCompleta factoryArmarReceta(Obtener_RecetaResult recetaLinq)
        {
            RecetaCompleta receta = new RecetaCompleta();
            receta.recetaId = recetaLinq.ID_RECETA;
            receta.nombreReceta = recetaLinq.DSC_NOMBRE;
            receta.fecha = (DateTime)recetaLinq.FECHA;
            if (!string.IsNullOrEmpty(recetaLinq.Ingredientes))
            {
                receta.ingredientes = recetaLinq.Ingredientes.Split(',').Select(i => i.Trim()).ToList();
            }
            return receta;
        }

        private Ingrediente factoryArmarIngredientes(Obtener_Ingrediente_ByIdResult usuarioLinq)
        {
            Ingrediente ingrediente = new Ingrediente();
            ingrediente.idIngrediente = (int)usuarioLinq.ID_INGREDIENTE;
            ingrediente.dscDescripcion = usuarioLinq.DSC_DESCRIPCION;
            ingrediente.idCategoriaIngrediente = (int)usuarioLinq.ID_CATE_INGREDIENTE;
            ingrediente.dscURLImagen = usuarioLinq.DSC_URL_IMAGEN;
            ingrediente.numPrecio = (decimal)usuarioLinq.NUM_PRECIO;
            ingrediente.estado = true;
            return ingrediente;
        }


    }
}
