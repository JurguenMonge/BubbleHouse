
using FrontEnd.Pages;
using Microsoft.Maui.Controls;

namespace FrontEnd
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new Login();
        }

        public void NavigateToMainPage()
        {
            // Navegar a la página principal con la TabbedPage después de iniciar sesión
            MainPage = new NavigationPage(new PrincipalAdministrativa());
        }

        public void NavigateToMainPagePublic()
        {
            // Navegar a la vista principal con la TabbedPage
            MainPage = new NavigationPage(new VistaPrincipalPage());
        }

    }
}
 