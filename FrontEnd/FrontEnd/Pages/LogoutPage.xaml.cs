namespace FrontEnd.Pages;

public partial class LogoutPage : ContentPage
{
	public LogoutPage()
	{
		InitializeComponent();
    }

    private async void cerrarSesion()
    {
        bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de cerrar la sesión?", "Aceptar", "Cancelar");
        if (answer)
        {
            //Victor aquí maneja la logica para cerrar la sesión


            Application.Current.MainPage = new NavigationPage(new Login());
        }
        else
        {
            ((App)Application.Current).NavigateToMainPage();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        cerrarSesion();
    }

}