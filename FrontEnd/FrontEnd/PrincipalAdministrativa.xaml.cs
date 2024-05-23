using FrontEnd.Pages;

namespace FrontEnd;

public partial class PrincipalAdministrativa : ContentPage
{
	public PrincipalAdministrativa()
	{
		InitializeComponent();
	}

    private void btnCategorias_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ListadoCategoriaProducto());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }

    private void btnSubCategorias_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SubCategoriaProductoPage());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }

    private void btnProductos_Clicked(object sender, EventArgs e)
    {

    }

    private void btnAceptarPedidos_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AceptarFacturas());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }
}