using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Request;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Controller
{
    public class RecetaController
    {
        public async Task<ResReceta> IngresarReceta(List<Ingrediente> ingredientes)
        {
            ResReceta res = new ResReceta();
            try
            {
                float total = 0;
                bool tamanio = false;
                bool lacteo = false;
                bool sabor = false;
                foreach(Ingrediente ingre in ingredientes)
                {
                    if(ingre.idCategoriaIngrediente == 7)
                    {
                        tamanio = true;
                    }
                    if(ingre.idCategoriaIngrediente == 1)
                    {
                        lacteo = true;
                    }
                    if(ingre.idCategoriaIngrediente == 2)
                    {
                        sabor = true;
                    }
                    total = total + (float)ingre.numPrecio;
                }
                if(!tamanio || !lacteo || !sabor)
                {
                    res.ListaDeErrores.Add("Un ingrediente primordial no fue seleccionado");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    Receta receta = new Receta();
                    receta.dscNombre = "Bubble te Personalizado";
                    receta.listaIngrediente = ingredientes; 
                    ReqReceta req = new ReqReceta();
                    Producto producto = new Producto();
                    producto.subcategoriaProducto = new Entidades.SubcategoriaProducto();
                    producto.receta = receta;
                    producto.nombreProducto = "Bubble te Personalizado";
                    producto.subcategoriaProducto.idSubcategoriaProducto = 8;
                    producto.receta.idReceta = 0;
                    producto.descripcion = "Producto Generico Generado a la hora de crear un bubble te personalizado";
                    producto.urlImgen = "Sin imagen";
                    producto.precio = total;
                    producto.estado = true;
                    req.Producto = producto;
                    //req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync("https://localhost:44311/api/receta/ingresar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResReceta>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar insertar un producto");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                res.ListaDeErrores.Add("Error interno");
            }
            return res;
        }

        public async Task<ResReceta> EliminarReceta(int id)
        {
            ResReceta res = new ResReceta();
            try
            {
                if (id != 0)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        // Crear la solicitud HttpRequestMessage
                        var response = await client.DeleteAsync("https://localhost:44311/api/receta/eliminar/" + id);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResReceta>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar eliminar la receta");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                res.ListaDeErrores.Add("Error interno");
            }
            return res;
        }
    }
}
