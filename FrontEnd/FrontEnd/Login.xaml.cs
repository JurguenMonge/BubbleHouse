using FrontEnd.Entidades;
using Newtonsoft.Json;
using System.Text;

namespace FrontEnd;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {
        if(String.IsNullOrEmpty(txtCorreo.Text) || String.IsNullOrEmpty(txtPassword.Text))
        {
            DisplayAlert("Datos faltantes", "Ingrese un correo y contraseña", "Aceptar");
        }
        else
        {
            try
            {
                var plataform = DeviceInfo.Platform;
                ReqIngresarSesion req = new ReqIngresarSesion();
                req.correo = txtCorreo.Text;
                req.password = txtPassword.Text;
                req.origen = plataform.ToString();
                var jsonConten = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.PostAsync("https://localhost:44311/api/login", jsonConten);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResIngresarSesion res = new ResIngresarSesion();
                    res = JsonConvert.DeserializeObject<ResIngresarSesion>(responseContent);
                    if(res.Resultado)
                    {
                        Usuario usuario = new Usuario();
                        usuario = res.Sesion.Usuario;
                        DisplayAlert("Inicio de sesion", "Sesion: " + res.Sesion.Id_Sesion, "Aceptar");
                    }
                    else
                    {
                        DisplayAlert("Contraseña o Usuario incorrecto", res.ListaDeErrores.First(), "Aceptar");
                    }
                }
                else
                {
                    DisplayAlert("Error de conexion", "Intente mas tarde", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error Interno", "Contacte con adminitracion, o reinstale la aplicacion", "Aceptar");
            }
        }
    }
}