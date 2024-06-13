using FrontEnd.Pages;
using Microsoft.Maui.Controls;

namespace FrontEnd.Pages
{
    public partial class VistaPrincipalPage : TabbedPage
    {
        public VistaPrincipalPage()
        {
            InitializeComponent();

            // Método auxiliar para configurar una página con navegación
            NavigationPage CreateNavigationPage(Page page, string title, string icon)
            {
                var navigationPage = new NavigationPage(page);
#if WINDOWS
                navigationPage.Title = title;
                navigationPage.IconImageSource = null; // No mostrar el ícono en Windows
                NavigationPage.SetHasNavigationBar(navigationPage, true);
#else
                navigationPage.Title = null; // No mostrar el título en otras plataformas
                navigationPage.IconImageSource = icon;
                NavigationPage.SetHasNavigationBar(navigationPage, true);
#endif
                return navigationPage;
            }

            // Crear las páginas con navegación
            var menuPage = CreateNavigationPage(new Menu(), "Menú", "menu.svg");
            var crearBubble = CreateNavigationPage(new SeleccionarTamanio(), "Crear Bubble", "bubble.svg");
            var carritoPage = CreateNavigationPage(new Carrito(), "Carrito", "carrito.svg");
            var login = CreateNavigationPage(new GoLoginPage(), "Login", "usuario.svg");

            // Agregar las páginas como hijos de TabbedPage
            Children.Add(menuPage);
            Children.Add(crearBubble);
            Children.Add(carritoPage);
            Children.Add(login);
        }
    }
}
