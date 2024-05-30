
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

    }
}
 