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
            var result = validacionesGenericas.validarString(req.Receta.dscNombre);
            if (result == 1)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Nombre faltante");
            }
            else if (result == 2)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("el Nombre no debe contener caracteres especiales");
            }
            result = validacionesGenericas.validarString(req.Receta.dscTamano);
            if (result == 1)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Tamano faltante");
            }
            else if (result == 2)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("el Tamano no debe contener caracteres especiales");
            }
            if (validacionesGenericas.validarInt(req.Receta.idIngLacteo) == 1)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("lacteo faltante");
            }
            if (validacionesGenericas.validarInt(req.Receta.idIngSabor) == 1)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("sabor faltante");
            }
            if (validacionesGenericas.validarInt(req.Receta.producto.idProducto) == 1)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("producto faltante");
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
                    var linqSesion = linq.Obtener_Sesion(req.id_Sesion);
                    var permisos = false;
                    foreach (var item in linqSesion)
                    {
                        Rol rol = factoryArmarRol(item);
                        if (rol != null && rol.tipoRol == "Administrador") 
                        {
                            permisos = true;
                        }
                    }
                    if(permisos)
                    {
                        res = validaciones(req);
                        if (!res.ListaDeErrores.Any())
                        {
                            linq.Insertar_Receta(req.Receta.producto.idProducto,req.Receta.idIngLacteo,req.Receta.idIngSabor,
                                req.Receta.idIngAzucar,req.Receta.idIngTopping,req.Receta.idIngBordeado,req.Receta.idIngBubbles,
                                req.Receta.dscNombre,req.Receta.dscTamano,ref idReturn, ref idError, ref errorBD);
                            if (idError == 0)
                            {
                                res.Resultado = true;
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
                var linqRoles = linq.Obtener_Receta();
                if (idError == 0)
                {
                    res.Resultado = true;
                    tipoRegistro = 1;
                    foreach (var item in linqRoles)
                    {
                        Receta receta = factoryArmarReceta(item);
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
        private Receta factoryArmarReceta(Obtener_RecetaResult usuarioLinq)
        {
            Receta receta = new Receta();
            receta.idReceta = usuarioLinq.ID_RECETA;
            receta.dscNombre = usuarioLinq.DSC_NOMBRE;
            receta.dscTamano = usuarioLinq.DSC_TAMANO;
            receta.idIngLacteo = (int)usuarioLinq.ID_ING_LACTEO;
            receta.idIngSabor = (int)usuarioLinq.ID_ING_SABOR;
            receta.idIngAzucar = (int)usuarioLinq.ID_ING_AZUCAR;
            receta.idIngBordeado = (int)usuarioLinq.ID_ING_BORDEADO;
            receta.idIngBubbles = (int)usuarioLinq.ID_ING_BUBBLES;
            receta.idIngTopping = (int)usuarioLinq.ID_ING_TOPPING;

                try
                {
                   ConexionDataContext linq = new ConexionDataContext();
                    var linqingredientes = linq.Obtener_Ingrediente_ById((int)usuarioLinq.ID_ING_LACTEO);
                    foreach (var item in linqingredientes)
                    {
                            Ingrediente ingrediente = factoryArmarIngredientes(item);
                            if (ingrediente != null)
                            {
                                receta.listaIngrediente.Add(ingrediente);
                            }
                    }
                    linqingredientes = linq.Obtener_Ingrediente_ById((int)usuarioLinq.ID_ING_SABOR);
                    foreach (var item in linqingredientes)
                    {
                        Ingrediente ingrediente = factoryArmarIngredientes(item);
                        if (ingrediente != null)
                        {
                            receta.listaIngrediente.Add(ingrediente);
                        }
                    }
                    linqingredientes = linq.Obtener_Ingrediente_ById((int)usuarioLinq.ID_ING_AZUCAR);
                    foreach (var item in linqingredientes)
                    {
                        Ingrediente ingrediente = factoryArmarIngredientes(item);
                        if (ingrediente != null)
                        {
                            receta.listaIngrediente.Add(ingrediente);
                        }
                    }
                    linqingredientes = linq.Obtener_Ingrediente_ById((int)usuarioLinq.ID_ING_BORDEADO);
                    foreach (var item in linqingredientes)
                    {
                        Ingrediente ingrediente = factoryArmarIngredientes(item);
                        if (ingrediente != null)
                        {
                            receta.listaIngrediente.Add(ingrediente);
                        }
                    }
                    linqingredientes = linq.Obtener_Ingrediente_ById((int)usuarioLinq.ID_ING_BUBBLES);
                    foreach (var item in linqingredientes)
                    {
                        Ingrediente ingrediente = factoryArmarIngredientes(item);
                        if (ingrediente != null)
                        {
                            receta.listaIngrediente.Add(ingrediente);
                        }
                    }
                    linqingredientes = linq.Obtener_Ingrediente_ById((int)usuarioLinq.ID_ING_TOPPING);
                    foreach (var item in linqingredientes)
                    {
                        Ingrediente ingrediente = factoryArmarIngredientes(item);
                        if (ingrediente != null)
                        {
                            receta.listaIngrediente.Add(ingrediente);
                        }
                    }
            }
                catch
                {

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
