using FrontEnd.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FrontEnd.Controller
{
    public class LoginController
    {
        public async Task IngresarSesion(string correo, string password)
        {
            try
            {
                if (String.IsNullOrEmpty(correo) || String.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Ingrese un correo y contraseña");
                }

                var plataform = DeviceInfo.Platform; // Asegúrate de tener acceso a esta propiedad desde aquí

                ReqIngresarSesion req = new ReqIngresarSesion();
                req.correo = correo;
                req.password = password;
                req.origen = plataform.ToString();

                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();
                var response = await httpClient.PostAsync("https://localhost:44311/api/login", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResIngresarSesion res = JsonConvert.DeserializeObject<ResIngresarSesion>(responseContent);

                    if (res.Resultado)
                    {
                        Usuario usuario = res.Sesion.Usuario;
                        Preferences.Set("IdSesion", res.Sesion.Id_Sesion);
                        Preferences.Set("UsuarioId", res.Sesion.Usuario.IdUsuario);
                        Preferences.Set("UsuarioNombre", res.Sesion.Usuario.Nombre);
                        Preferences.Set("UsuarioPrimerApellido", res.Sesion.Usuario.PrimerApellido);
                    }
                    else
                    {
                        throw new Exception(res.ListaDeErrores.First());
                    }
                }
                else
                {
                    throw new HttpRequestException("Intente mas tarde");
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción aquí o lanzarla para que sea manejada en el controlador del formulario
                throw ex;
            }
        }
    }
}
