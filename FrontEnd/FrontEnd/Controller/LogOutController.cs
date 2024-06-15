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
    public class LogOutController
    {
        public async Task<ResLogOut> CerrarSesion()
        {
            try
            {
                var plataform = DeviceInfo.Platform; // Asegúrate de tener acceso a esta propiedad desde aquí

                ReqLogOut req = new ReqLogOut();
                req.id_Sesion = Preferences.Get("IdSesion", string.Empty);
                req.dsc_cierre = "Logout";

                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();
                var response = await httpClient.PostAsync("https://localhost:44311/api/logout", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResLogOut res = JsonConvert.DeserializeObject<ResLogOut>(responseContent);

                    if (res != null)
                    {
                        return res;
                    }
                    else
                    {
                        throw new Exception(res.ListaDeErrores.First());
                    }
                }
                else
                {
                    throw new HttpRequestException("Intente más tarde");
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
