using FrontEnd.Entidades;
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
    public class UsuarioController
    {
        public async Task<ResUsuario> IngresarUsuario(string nombre, string primerApellido, string segundoApellido, string correo, string password, string telefono, Rol rol)
        {
            ResUsuario res = new ResUsuario();
            try
            {
                if (String.IsNullOrEmpty(nombre))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre del usuario");
                }
                if (String.IsNullOrEmpty(primerApellido))
                {
                    res.ListaDeErrores.Add("Ingrese el primer apellido del usuario");
                }
                if (String.IsNullOrEmpty(segundoApellido))
                {
                    res.ListaDeErrores.Add("Ingrese el segundo apellido del usuario");
                }
                if (String.IsNullOrEmpty(correo))
                {
                    res.ListaDeErrores.Add("Ingrese el correo del usuario");
                }
                if (String.IsNullOrEmpty(password))
                {
                    res.ListaDeErrores.Add("Ingrese la contraseña del usuario");
                }
                if (String.IsNullOrEmpty(password))
                {
                    res.ListaDeErrores.Add("Ingrese la contraseña del usuario");
                }
                if (String.IsNullOrEmpty(telefono))
                {
                    res.ListaDeErrores.Add("Ingrese el teléfono del usuario");
                }
                Regex regex = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ ]*$");

                if (!regex.IsMatch(nombre))
                {
                    res.ListaDeErrores.Add("El nombre del usuario no debe llevar caracteres especiales");
                }
                if (!regex.IsMatch(primerApellido))
                {
                    res.ListaDeErrores.Add("El primer apellido del usuario no debe llevar caracteres especiales");
                }
                if (!regex.IsMatch(segundoApellido))
                {
                    res.ListaDeErrores.Add("El segundo apellido del usuario no debe llevar caracteres especiales");
                }
                if (rol is null)
                {
                    res.ListaDeErrores.Add("Debe seleccionar un rol");
                }

                Regex regexCorreo = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                if (!regexCorreo.IsMatch(correo))
                {
                    res.ListaDeErrores.Add("Ingrese un correo válido!!");
                }

                Regex regexNum = new Regex(@"^[26789]\d{7}$");
                if (!regexNum.IsMatch(telefono))
                {
                    res.ListaDeErrores.Add("Ingrese un número de teléfono válido!!");
                }

                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqIngresarUsuario req = new ReqIngresarUsuario();
                    Usuario usuario = new Usuario();
                    usuario.Nombre = nombre;
                    usuario.PrimerApellido = primerApellido;
                    usuario.SegundoApellido = segundoApellido;
                    usuario.CorreoElectronico = correo;
                    usuario.Password = password;
                    usuario.NumeroTelefono = telefono;
                    usuario.rol = rol;
                    usuario.estado = true;
                    req.Usuario = usuario;
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);


                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync("https://localhost:44311/api/usuario/ingresar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResUsuario>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar crear un usuario");
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

        
        public async Task<ResUsuario> ActualizarUsuarioSuperAdmin(int idUsuario, string nombre, string primerApellido, string segundoApellido, string correo, string telefono, Rol rol)
        {
            ResUsuario res = new ResUsuario();
            try
            {
                if (String.IsNullOrEmpty(nombre))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre del usuario");
                }
                if (String.IsNullOrEmpty(primerApellido))
                {
                    res.ListaDeErrores.Add("Ingrese el primer apellido del usuario");
                }
                if (String.IsNullOrEmpty(segundoApellido))
                {
                    res.ListaDeErrores.Add("Ingrese el segundo apellido del usuario");
                }
                if (String.IsNullOrEmpty(correo))
                {
                    res.ListaDeErrores.Add("Ingrese el correo del usuario");
                }
                if (String.IsNullOrEmpty(telefono))
                {
                    res.ListaDeErrores.Add("Ingrese el teléfono del usuario");
                }
                Regex regex = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ ]*$");

                if (!regex.IsMatch(nombre))
                {
                    res.ListaDeErrores.Add("El nombre del usuario no debe llevar caracteres especiales");
                }
                if (!regex.IsMatch(primerApellido))
                {
                    res.ListaDeErrores.Add("El primer apellido del usuario no debe llevar caracteres especiales");
                }
                if (!regex.IsMatch(segundoApellido))
                {
                    res.ListaDeErrores.Add("El segundo apellido del usuario no debe llevar caracteres especiales");
                }
                if (rol is null)
                {
                    res.ListaDeErrores.Add("Debe seleccionar un rol");
                }

                Regex regexCorreo = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                if (!regexCorreo.IsMatch(correo))
                {
                    res.ListaDeErrores.Add("Ingrese un correo válido!!");
                }

                Regex regexNum = new Regex(@"^[26789]\d{7}$");
                if (!regexNum.IsMatch(telefono))
                {
                    res.ListaDeErrores.Add("Ingrese un número de teléfono válido!!");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqIngresarUsuario req = new ReqIngresarUsuario();
                    Usuario usuario = new Usuario();
                    usuario.IdUsuario = idUsuario;
                    usuario.Nombre = nombre;
                    usuario.PrimerApellido = primerApellido;
                    usuario.SegundoApellido = segundoApellido;
                    usuario.CorreoElectronico = correo;
                    usuario.Password = "";
                    usuario.NumeroTelefono = telefono;
                    usuario.rol = rol;
                    usuario.estado = true;
                    req.Usuario = usuario;
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync("https://localhost:44311/api/usuario/modificarSuperAdmin", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResUsuario>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar modificar el usuario");
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

        public async Task<ResUsuario> ActualizarUsuario(int idUsuario, string nombre, string primerApellido, string segundoApellido, string correo, string password, string telefono)
        {
            ResUsuario res = new ResUsuario();
            try
            {
                if (String.IsNullOrEmpty(nombre))
                {
                    res.ListaDeErrores.Add("Ingrese el nombre del usuario");
                }
                if (String.IsNullOrEmpty(primerApellido))
                {
                    res.ListaDeErrores.Add("Ingrese el primer apellido del usuario");
                }
                if (String.IsNullOrEmpty(segundoApellido))
                {
                    res.ListaDeErrores.Add("Ingrese el segundo apellido del usuario");
                }
                if (String.IsNullOrEmpty(correo))
                {
                    res.ListaDeErrores.Add("Ingrese el correo del usuario");
                }
                if (String.IsNullOrEmpty(password))
                {
                    res.ListaDeErrores.Add("Ingrese la contraseña del usuario");
                }
                if (String.IsNullOrEmpty(telefono))
                {
                    res.ListaDeErrores.Add("Ingrese el teléfono del usuario");
                }
                Regex regex = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ ]*$");

                if (!regex.IsMatch(nombre))
                {
                    res.ListaDeErrores.Add("El nombre del usuario no debe llevar caracteres especiales");
                }
                if (!regex.IsMatch(primerApellido))
                {
                    res.ListaDeErrores.Add("El primer apellido del usuario no debe llevar caracteres especiales");
                }
                if (!regex.IsMatch(segundoApellido))
                {
                    res.ListaDeErrores.Add("El segundo apellido del usuario no debe llevar caracteres especiales");
                }

                Regex regexCorreo = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                if (!regexCorreo.IsMatch(correo))
                {
                    res.ListaDeErrores.Add("Ingrese un correo válido!!");
                }

                Regex regexNum = new Regex(@"^[26789]\d{7}$");
                if (!regexNum.IsMatch(telefono))
                {
                    res.ListaDeErrores.Add("Ingrese un número de teléfono válido!!");
                }
                if (res.ListaDeErrores.Count() == 0)
                {
                    ReqIngresarUsuario req = new ReqIngresarUsuario();
                    Usuario usuario = new Usuario();
                    usuario.IdUsuario = idUsuario;
                    usuario.Nombre = nombre;
                    usuario.PrimerApellido = primerApellido;
                    usuario.SegundoApellido = segundoApellido;
                    usuario.CorreoElectronico = correo;
                    usuario.Password = password;
                    usuario.NumeroTelefono = telefono;
                    usuario.rol = null;
                    usuario.estado = true;
                    req.Usuario = usuario;
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync("https://localhost:44311/api/usuario/modificar", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResUsuario>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar modificar el usuario");
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

        public async Task<ResUsuario> EliminarUsuario(int id)
        {
            ResUsuario res = new ResUsuario();
            try
            {
                if (id != 0)
                {
                    ReqIngresarUsuario req = new ReqIngresarUsuario();
                    Usuario usuario = new Usuario();
                    usuario.IdUsuario = id;
                    usuario.Nombre = "";
                    usuario.PrimerApellido = "";
                    usuario.SegundoApellido = "";
                    usuario.CorreoElectronico = "";
                    usuario.Password = "";
                    usuario.NumeroTelefono = "";
                    usuario.rol = null;
                    usuario.estado = true;
                    req.Usuario = usuario;
                    req.idSesion = Preferences.Get("IdSesion", string.Empty);

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        // Crear la solicitud HttpRequestMessage
                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Delete,
                            RequestUri = new Uri("https://localhost:44311/api/usuario/eliminar"),
                            Content = jsonContent
                        };

                        // Enviar la solicitud usando SendAsync
                        var response = await httpClient.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<ResUsuario>(responseContent);
                        }
                        else
                        {
                            res.ListaDeErrores.Add("Error al intentar eliminar el usuario");
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
