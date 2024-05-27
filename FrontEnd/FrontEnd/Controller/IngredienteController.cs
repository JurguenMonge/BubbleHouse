using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Request;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrontEnd.Controller
{
    public class IngredienteController
    {
        public async Task<ResIngrediente> IngresarIngrediente(int idCategoriaIngrediente, string nombreIngrediente, string nombreCategoriaIngrediente, string descripcion, string urlImagen, decimal precio)
        {
            ResIngrediente res = new ResIngrediente();
            try
            {
                if (String.IsNullOrEmpty(nombreIngrediente))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre del ingrediente");
                }
                if (String.IsNullOrEmpty(nombreCategoriaIngrediente))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre de la categoría del ingrediente");
                }
                if (String.IsNullOrEmpty(descripcion))
                {
                    res.ListaDeErrores.Add("Ingrese la descripción del ingrediente");
                }
                if (String.IsNullOrEmpty(urlImagen))
                {
                    res.ListaDeErrores.Add("Debe seleccionar una imagen");
                }
                if (idCategoriaIngrediente <= 0)
                {
                    res.ListaDeErrores.Add("Debe seleccionar una categoría ingrediente");
                }
                Regex regex = new Regex("^[a-zA-Z0-9\\u00C0-\\u00FF ]*$");
                if (!regex.IsMatch(nombreIngrediente))
                {
                    res.ListaDeErrores.Add("El nombre del ingrediente no debe llevar caracteres especiales");
                }
                if (!regex.IsMatch(nombreCategoriaIngrediente))
                {
                    res.ListaDeErrores.Add("El nombre de la categoría del ingrediente no debe llevar caracteres especiales");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqIngrediente req = new ReqIngrediente();
                    Ingrediente ingrediente = new Ingrediente();
                    ingrediente.dscNombre = nombreIngrediente;
                    ingrediente.idCategoriaIngrediente = idCategoriaIngrediente;
                    ingrediente.dscNombreCategoriaIngrediente = nombreCategoriaIngrediente;
                    ingrediente.dscDescripcion = descripcion;
                    ingrediente.dscURLImagen = urlImagen;
                    ingrediente.idIngrediente = 0;
                    ingrediente.estado = true;
                    req.Ingrediente = ingrediente;
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync("https://localhost:44311/api/ingrediente/ingresar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResIngrediente>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar insertar un ingrediente");
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

        public async Task<ResIngrediente> ActualizarIngrediente(int idIngrediente, int idCategoriaIngrediente, string nombreIngrediente, string nombreCategoriaIngrediente, string descripcion, string urlImagen, decimal precio)
        {
            ResIngrediente res = new ResIngrediente();
            try
            {
                if (String.IsNullOrEmpty(nombreIngrediente))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre del ingrediente");
                }
                if (String.IsNullOrEmpty(nombreCategoriaIngrediente))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre de la categoría del ingrediente");
                }
                if (String.IsNullOrEmpty(descripcion))
                {
                    res.ListaDeErrores.Add("Ingrese la descripción del ingrediente");
                }
                if (String.IsNullOrEmpty(urlImagen))
                {
                    res.ListaDeErrores.Add("Debe seleccionar una imagen");
                }
                if (idCategoriaIngrediente <= 0)
                {
                    res.ListaDeErrores.Add("Debe seleccionar una categoría ingrediente");
                }
                Regex regex = new Regex("^[a-zA-Z0-9\\u00C0-\\u00FF ]*$");
                if (!regex.IsMatch(nombreIngrediente))
                {
                    res.ListaDeErrores.Add("El nombre del ingrediente no debe llevar caracteres especiales");
                }
                if (!regex.IsMatch(nombreCategoriaIngrediente))
                {
                    res.ListaDeErrores.Add("El nombre de la categoría del ingrediente no debe llevar caracteres especiales");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqIngrediente req = new ReqIngrediente();
                    Ingrediente ingrediente = new Ingrediente();
                    ingrediente.dscNombre = nombreIngrediente;
                    ingrediente.idCategoriaIngrediente = idCategoriaIngrediente;
                    ingrediente.dscNombreCategoriaIngrediente = nombreCategoriaIngrediente;
                    ingrediente.dscDescripcion = descripcion;
                    ingrediente.dscURLImagen = urlImagen;
                    ingrediente.idIngrediente = idIngrediente;
                    ingrediente.estado = true;
                    req.Ingrediente = ingrediente;
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync("https://localhost:44311/api/ingrediente/modificar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResIngrediente>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar modificar el ingrediente");
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

        public async Task<ResIngrediente> EliminarIngrediente(int id)
        {
            ResIngrediente res = new ResIngrediente();
            try
            {
                if (id != 0)
                {
                    ReqIngrediente req = new ReqIngrediente();
                    Ingrediente ingrediente = new Ingrediente();
                    ingrediente.dscNombre = "";
                    ingrediente.idCategoriaIngrediente = 0;
                    ingrediente.dscNombreCategoriaIngrediente = "";
                    ingrediente.dscDescripcion = "";
                    ingrediente.dscURLImagen = "";
                    ingrediente.idIngrediente = id;
                    ingrediente.estado = true;
                    req.Ingrediente = ingrediente;
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        // Crear la solicitud HttpRequestMessage
                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Delete,
                            RequestUri = new Uri("https://localhost:44311/api/ingrediente/eliminar"),
                            Content = jsonContent
                        };

                        // Enviar la solicitud usando SendAsync
                        var response = await httpClient.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResIngrediente>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar eliminar el ingrediente");
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
