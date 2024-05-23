
using FrontEnd.Entidades.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FrontEnd.Entidades.Response;
using FrontEnd.Entidades;

namespace FrontEnd.Controller
{
    public class SubCategoriaProductoController
    {

        public async Task<ResSubCategoriaProducto> IngresarSubCategoriaProducto(String nombre, int idCategoria)
        {
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            try
            {
                if (String.IsNullOrEmpty(nombre))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre de la subcategoría del producto");
                }
                if (idCategoria <= 0)
                {
                    res.ListaDeErrores.Add("Debe seleccionar una categoría");
                }
                Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
                if (!regex.IsMatch(nombre))
                {
                    res.ListaDeErrores.Add("El nombre de la subcategoría del producto no debe llevar caracteres especiales");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqSubCategoriaProducto req = new ReqSubCategoriaProducto();
                    SubcategoriaProducto subcategoria = new SubcategoriaProducto();
                    subcategoria.dscNombreSubCategoria = nombre;
                    subcategoria.cateProductoId = idCategoria;
                    subcategoria.idSubcategoriaProducto = 0;
                    subcategoria.estado = true;
                    req.SubCategoriaProducto = subcategoria;
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync("https://localhost:44311/api/subCategoriaProducto/ingresar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResSubCategoriaProducto>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar insertar una subcategoría de producto");
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

        public async Task<ResSubCategoriaProducto> ActualizarSubCategoriaProducto(int idSubCategoria, int idCategoria, String nombre)
        {
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            try
            {
                if (String.IsNullOrEmpty(nombre))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre de la subcategoría del producto");
                }
                if (idCategoria <= 0)
                {
                    res.ListaDeErrores.Add("Debe seleccionar una categoría");
                }
                Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
                if (!regex.IsMatch(nombre))
                {
                    res.ListaDeErrores.Add("El nombre de la subcategoría del producto no debe llevar caracteres especiales");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqSubCategoriaProducto req = new ReqSubCategoriaProducto();
                    SubcategoriaProducto subcategoria = new SubcategoriaProducto();
                    subcategoria.dscNombreSubCategoria = nombre;
                    subcategoria.cateProductoId = idCategoria;
                    subcategoria.idSubcategoriaProducto = idSubCategoria;
                    subcategoria.estado = true;
                    req.SubCategoriaProducto = subcategoria;

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync("https://localhost:44311/api/subCategoriaProducto/modificar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResSubCategoriaProducto>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar modificar una subcategoría de producto");
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

        public async Task<ResSubCategoriaProducto> EliminarSubCategoriaProducto(int id)
        {
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            try
            {
                if (id != 0)
                {
                    ReqSubCategoriaProducto req = new ReqSubCategoriaProducto();
                    SubcategoriaProducto subcategoria = new SubcategoriaProducto();
                    subcategoria.dscNombreSubCategoria = "";
                    subcategoria.cateProductoId = 0;
                    subcategoria.idSubcategoriaProducto = id;
                    subcategoria.estado = true;
                    req.SubCategoriaProducto = subcategoria;

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        // Crear la solicitud HttpRequestMessage
                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Delete,
                            RequestUri = new Uri("https://localhost:44311/api/subCategoriaProducto/eliminar"),
                            Content = jsonContent
                        };

                        // Enviar la solicitud usando SendAsync
                        var response = await httpClient.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResSubCategoriaProducto>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar eliminar una subcategoría de producto");
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
