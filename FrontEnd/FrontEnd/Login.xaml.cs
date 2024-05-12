using FrontEnd.Controller;
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
        LoginController login = new LoginController();
        try
        {
            await login.IngresarSesion(txtCorreo.Text, txtPassword.Text);
            string nombreUsuario = Preferences.Get("UsuarioNombre", string.Empty);
            DisplayAlert("Inicio de sesión", "Bienvenido "+ nombreUsuario, "Aceptar");
            Navigation.PushAsync(new FormularioCategoriaProducto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Usuario o Contraseña incorrecto", ex.Message, "Aceptar");
        }
    }
}