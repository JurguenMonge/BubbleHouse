using FrontEnd.Controller;
using FrontEnd.Entidades.Response;

namespace FrontEnd.Pages;

public partial class LogoutPage : ContentPage
{
	public LogoutPage()
	{
		InitializeComponent();
    }

    private async void cerrarSesion()
    {
        try
        {
            bool answer = await DisplayAlert("Confirmaci�n", "�Est�s seguro de cerrar la sesi�n?", "Aceptar", "Cancelar");
            if (answer)
            {
                LogOutController controller = new LogOutController();
                ResLogOut res = await controller.CerrarSesion();
                if (res.Resultado)
                {
                    Application.Current.MainPage = new NavigationPage(new Login());
                }
                else
                {
                    DisplayAlert("Error al cerrar sesion", "Ocurrio un error al cerrar sesion" + res.ListaDeErrores.First(), "Aceptar");
                }
            }
            else
            {
                ((App)Application.Current).NavigateToMainPage();
            }

        }
        catch (Exception ex)
        {
            DisplayAlert("Usuario o Contrase�a incorrecto", ex.Message, "Aceptar");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        cerrarSesion();
    }

}