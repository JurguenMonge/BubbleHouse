namespace FrontEnd.Pages;

public partial class GoLoginPage : ContentPage
{
	public GoLoginPage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Application.Current.MainPage = new NavigationPage(new Login());
    }
}