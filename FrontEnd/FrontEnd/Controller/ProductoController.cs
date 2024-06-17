using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Request;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace FrontEnd.Controller
{
    public class ProductoController
    {
        public async Task<ResProducto> IngresarProducto(int idSubCategoriaProducto, int idReceta, string nombreProducto, string descripcion, string urlImagen, decimal precio)
        {
            ResProducto res = new ResProducto();
            try
            {
                if (String.IsNullOrEmpty(nombreProducto))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre del producto");
                }
                if (String.IsNullOrEmpty(descripcion))
                {
                    res.ListaDeErrores.Add("Ingrese la descripción del producto");
                }
                if (String.IsNullOrEmpty(urlImagen))
                {
                    res.ListaDeErrores.Add("Debe seleccionar una imagen");
                }
                if (precio < 0)
                {
                    res.ListaDeErrores.Add("Ingrese un precio");
                }
                if (idSubCategoriaProducto <= 0)
                {
                    res.ListaDeErrores.Add("Debe seleccionar una subcategoría producto");
                }
                if (idReceta <= 0)
                {
                    res.ListaDeErrores.Add("Debe seleccionar una receta");
                }
                Regex regex = new Regex("^[a-zA-Z0-9\\u00C0-\\u00FF ]*$");
                if (!regex.IsMatch(nombreProducto))
                {
                    res.ListaDeErrores.Add("El nombre del producto no debe llevar caracteres especiales");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqIngresarProducto req = new ReqIngresarProducto();
                    Producto producto = new Producto();
                    producto.subcategoriaProducto = new Entidades.SubcategoriaProducto();
                    producto.receta = new Receta();
                    producto.nombreProducto = nombreProducto;
                    producto.subcategoriaProducto.idSubcategoriaProducto = idSubCategoriaProducto;
                    producto.receta.idReceta = idReceta;
                    producto.descripcion = descripcion;
                    producto.urlImgen = urlImagen;
                    producto.precio = (float)precio;
                    producto.estado = 1;
                    req.Producto = producto;
                    //req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync("https://apibubblehouse.azurewebsites.net/api/producto/ingresar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResProducto>(responseContent);
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

        public async Task<ResProducto> modificarProducto(int idProducto, int idSubCategoriaProducto, int idReceta, string nombreProducto, string descripcion, string urlImagen, decimal precio, int estado)
        {
            ResProducto res = new ResProducto();
            try
            {
                if (String.IsNullOrEmpty(nombreProducto))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre del producto");
                }
                if (String.IsNullOrEmpty(descripcion))
                {
                    res.ListaDeErrores.Add("Ingrese la descripción del producto");
                }
                if (String.IsNullOrEmpty(urlImagen))
                {
                    res.ListaDeErrores.Add("Debe seleccionar una imagen");
                }
                if (precio < 0)
                {
                    res.ListaDeErrores.Add("Ingrese un precio");
                }
                if (idSubCategoriaProducto <= 0)
                {
                    res.ListaDeErrores.Add("Debe seleccionar una subcategoría producto");
                }
                if (idReceta <= 0)
                {
                    res.ListaDeErrores.Add("Debe seleccionar una receta");
                }
                Regex regex = new Regex("^[a-zA-Z0-9\\u00C0-\\u00FF ]*$");
                if (!regex.IsMatch(nombreProducto))
                {
                    res.ListaDeErrores.Add("El nombre del producto no debe llevar caracteres especiales");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqIngresarProducto req = new ReqIngresarProducto();
                    Producto producto = new Producto();
                    producto.idProducto = idProducto;
                    producto.subcategoriaProducto = new Entidades.SubcategoriaProducto();
                    producto.receta = new Receta();
                    producto.nombreProducto = nombreProducto;
                    producto.subcategoriaProducto.idSubcategoriaProducto = idSubCategoriaProducto;
                    producto.receta.idReceta = idReceta;
                    producto.descripcion = descripcion;
                    producto.urlImgen = urlImagen;
                    producto.precio = (float)precio;
                    producto.estado = estado;
                    req.Producto = producto;
                    //req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync("https://apibubblehouse.azurewebsites.net/api/producto/modificar", jsonContent);
                        if (response.IsSuccessStatusCode)   
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResProducto>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar modificar un producto");
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


        public async Task<ResProducto> EliminarProducto(int id)
        {
            ResProducto res = new ResProducto();
            try
            {
                if (id != 0)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        // Crear la solicitud HttpRequestMessage
                        var response = await client.DeleteAsync("https://apibubblehouse.azurewebsites.net/api/producto/eliminar/" + id);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResProducto>(responseContent);
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
