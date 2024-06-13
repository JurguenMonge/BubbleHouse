namespace FrontEnd.Pages;

public partial class LogoutPage : ContentPage
{
	public LogoutPage()
	{
		InitializeComponent();
    }

    private async void cerrarSesion()
    {
        bool answer = await DisplayAlert("Confirmaci�n", "�Est�s seguro de cerrar la sesi�n?", "Aceptar", "Cancelar");
        if (answer)
        {
            //Victor aqu� maneja la logica para cerrar la sesi�n


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