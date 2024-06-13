using FrontEnd.Pages;
using Microsoft.Maui.Controls;

namespace FrontEnd;

public partial class PrincipalAdministrativa : TabbedPage
{
    public PrincipalAdministrativa()
    {
        InitializeComponent();

        // M�todo auxiliar para configurar una p�gina con navegaci�n
        NavigationPage CreateNavigationPage(Page page, string title, string icon)
        {
            var navigationPage = new NavigationPage(page);
#if WINDOWS
            navigationPage.Title = title;
            navigationPage.IconImageSource = null; // No mostrar el �cono en Windows
            NavigationPage.SetHasNavigationBar(navigationPage, true);
#else
            navigationPage.Title = null; // No mostrar el t�tulo en otras plataformas
            navigationPage.IconImageSource = icon;
            NavigationPage.SetHasNavigationBar(navigationPage, true);
#endif
            return navigationPage;
        }

        // Crear las p�ginas con navegaci�n
        var aceptarFacturasPage = CreateNavigationPage(new AceptarFacturas(), "Pedidos", "pedido.svg");
        var productoPage = CreateNavigationPage(new ProductoPage(), "Productos", "producto.svg");
        var ingredientePage = CreateNavigationPage(new IngredientePage(), "Ingredientes", "ingrediente.svg");
        var recetaPage = CreateNavigationPage(new RecetaPage(), "Recetas", "receta.svg");
        var usuarioAdminPage = CreateNavigationPage(new UsuarioAdminPage(), "Usuario", "usuario.svg");
        var carritoPage = CreateNavigationPage(new Carrito(), "Carrito", "carrito.svg");
        var usuarioSuperAdminPage = CreateNavigationPage(new UsuariosSuperAdminPage(), "Usuarios", "usuarios.svg");
        var logout = CreateNavigationPage(new LogoutPage(), "Cerrar Sesi�n", "salida.svg");

        // Agregar las p�ginas como hijos de TabbedPage
        Children.Add(aceptarFacturasPage);
        Children.Add(productoPage);
        Children.Add(ingredientePage);
        Children.Add(recetaPage);
        Children.Add(usuarioAdminPage);
        Children.Add(usuarioSuperAdminPage);
        Children.Add(logout);
    }
}
