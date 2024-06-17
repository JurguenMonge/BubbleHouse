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
    public class FacturaController
    {
        public static async Task<ResFactura> insertarFactura(ReqFactura req)
        {
            ResFactura res = new ResFactura();
            try
            {
                if (!req.Factura.productosList.Any())
                {
                    res.ListaDeErrores.Add("No hay productos en la factura");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync("https://apibubblehouse.azurewebsites.net/api/factura/ingresar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResFactura>(responseContent);
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

        public static async Task<ResFactura> ModificarFactura(ReqFactura req)
        {
            ResFactura res = new ResFactura();
            try
            {
                if (!req.Factura.productosList.Any())
                {
                    res.ListaDeErrores.Add("No hay productos en la factura");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync("https://apibubblehouse.azurewebsites.net/api/factura/modificar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResFactura>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add(" Error al intentar modificar una factura");
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

        public static async Task<ResFactura> ModificarEstadoNoPreparadoFactura(ReqFactura req)
        {
            ResFactura res = new ResFactura();
            try
            {
                if (!req.Factura.productosList.Any())
                {
                    res.ListaDeErrores.Add("No hay productos en la factura");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync("https://apibubblehouse.azurewebsites.net/api/factura/modificarEstadoNoPreparado", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResFactura>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add(" Error al intentar modificar una factura");
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

        public static async Task<ResFactura> ModificarEstadoCompletadoFactura(ReqFactura req)
        {
            ResFactura res = new ResFactura();
            try
            {
                if (!req.Factura.productosList.Any())
                {
                    res.ListaDeErrores.Add("No hay productos en la factura");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync("https://apibubblehouse.azurewebsites.net/api/factura/modificarEstadoCompletado", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResFactura>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add(" Error al intentar modificar una factura");
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

        public static async Task<ResFactura> EliminarProducto(ReqContenedorProducto req)
        {
            ResFactura res = new ResFactura();
            try
            {
                if (res.ListaDeErrores.Count() == 0)
                {
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync("https://apibubblehouse.azurewebsites.net/api/productofactura/eliminar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResFactura>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add(" Error al intentar eliminar un producto");
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

        public static async Task<ResFactura> EliminarFactura(ReqFactura req)
        {
            ResFactura res = new ResFactura();
            try
            {
                if (res.ListaDeErrores.Count() == 0)
                {
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync("https://apibubblehouse.azurewebsites.net/api/factura/eliminar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResFactura>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add(" Error al intentar eliminar una factura");
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
