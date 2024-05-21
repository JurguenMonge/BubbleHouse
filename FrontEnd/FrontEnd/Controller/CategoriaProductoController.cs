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
    public class CategoriaProductoController
    {
        public async Task<ResCategoriaProducto> IngresarCategoriaProducto(String nombre)
        {
            ResCategoriaProducto res = new ResCategoriaProducto();
            try
            {
                if (String.IsNullOrEmpty(nombre))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre de la categoria del producto");
                }
                Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
                if (!regex.IsMatch(nombre))
                {
                    res.ListaDeErrores.Add("El nombre de la categoria del producto no debe llevar caracteres especiales");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqCategoriaProducto req = new ReqCategoriaProducto();
                    CategoriaProducto categoria = new CategoriaProducto();
                    categoria.dscNombreCategoria = nombre;
                    categoria.idCategoriaProducto = 0;
                    categoria.estado = true;
                    req.CategoriaProducto = categoria;
                    req.idSesion =  Preferences.Get("IdSesion",string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync("https://localhost:44311/api/categoriaProducto/ingresar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResCategoriaProducto>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar insertar una categoria de producto");
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

        public async Task<ResCategoriaProducto> ActualizarCategoriaProducto(String nombre, int id)
        {
            ResCategoriaProducto res = new ResCategoriaProducto();
            try
            {
                if (String.IsNullOrEmpty(nombre))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre de la categoria del producto");
                }
                Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
                if (!regex.IsMatch(nombre))
                {
                    res.ListaDeErrores.Add("El nombre de la categoria del producto no debe llevar caracteres especiales");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqCategoriaProducto req = new ReqCategoriaProducto();
                    CategoriaProducto categoria = new CategoriaProducto();
                    categoria.dscNombreCategoria = nombre;
                    categoria.idCategoriaProducto = id;
                    categoria.estado = true;
                    req.CategoriaProducto = categoria;

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync("https://localhost:44311/api/categoriaProducto/modificar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResCategoriaProducto>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar modificar una categoria de producto");
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

        public async Task<ResCategoriaProducto> EliminarCategoriaProducto(int id)
        {
            ResCategoriaProducto res = new ResCategoriaProducto();
            try
            {
                if (id != 0)
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.DeleteAsync("https://localhost:44311/api/categoriaProducto/eliminar/" + id);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResCategoriaProducto>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar eliminar una categoria de producto");
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

