using FrontEnd.Pages;

namespace FrontEnd;

public partial class PrincipalAdministrativa : TabbedPage
{
	public PrincipalAdministrativa()
	{
		InitializeComponent();

        var aceptarFacturasPage = new NavigationPage(new AceptarFacturas());
        aceptarFacturasPage.Title = "Pedidos";
        aceptarFacturasPage.IconImageSource = "pedido.svg";
        NavigationPage.SetHasNavigationBar(aceptarFacturasPage, true);

        var productoPage = new NavigationPage(new ProductoPage());
        productoPage.Title = "Productos";
        productoPage.IconImageSource = "producto.svg";
        NavigationPage.SetHasNavigationBar(productoPage, true);

        var ingredientePage = new NavigationPage(new IngredientePage());
        ingredientePage.Title = "Ingredientes";
        ingredientePage.IconImageSource = "ingrediente.svg";
        NavigationPage.SetHasNavigationBar(ingredientePage, true);

        var recetaPage = new NavigationPage(new RecetaPage());
        recetaPage.Title = "Recetas";
        recetaPage.IconImageSource = "receta.svg";
        NavigationPage.SetHasNavigationBar(recetaPage, true);

        var usuarioAdminPage = new NavigationPage(new UsuarioAdminPage());
        usuarioAdminPage.Title = "Usuarios";
        usuarioAdminPage.IconImageSource = "usuario.svg";
        NavigationPage.SetHasNavigationBar(usuarioAdminPage, true);

        Children.Add(aceptarFacturasPage);
        Children.Add(productoPage);
        Children.Add(ingredientePage);
        Children.Add(recetaPage);
        Children.Add(usuarioAdminPage);
    }

    //private void btnCategorias_Clicked(object sender, EventArgs e)
    //{
    //    Navigation.PushAsync(new ListadoCategoriaProducto());
    //}

    //private void btnSubCategorias_Clicked(object sender, EventArgs e)
    //{
    //    Navigation.PushAsync(new SubCategoriaProductoPage());
    //}

    //private void btnProductos_Clicked(object sender, EventArgs e)
    //{

    //}

    //private void btnAceptarPedidos_Clicked(object sender, EventArgs e)
    //{
    //    Navigation.PushAsync(new AceptarFacturas());
    //}

    //private void btnIngredientes_Clicked(object sender, EventArgs e)
    //{
    //    Navigation.PushAsync(new IngredientePage());
    //}
}