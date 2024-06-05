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
        spinner.IsRunning = true;
        spinner.IsVisible = true;
        LoginController login = new LoginController();
        try
        {
            await login.IngresarSesion(txtCorreo.Text, txtPassword.Text);
            string nombreUsuario = Preferences.Get("UsuarioNombre", string.Empty);
            DisplayAlert("Inicio de sesión", "Bienvenido "+ nombreUsuario, "Aceptar");
            //Navigation.PushAsync(new PrincipalAdministrativa());
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            // Navegar a la página principal después de iniciar sesión
            ((App)Application.Current).NavigateToMainPage();
        }
        catch (Exception ex)
        {
            DisplayAlert("Usuario o Contraseña incorrecto", ex.Message, "Aceptar");
        }
    }

    private void btnVistaPrincipal_Clicked(object sender, EventArgs e)
    {
        ((App)Application.Current).NavigateToMainPagePublic();
    }
}