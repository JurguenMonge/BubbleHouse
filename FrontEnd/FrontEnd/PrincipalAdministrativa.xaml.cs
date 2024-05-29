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
    }

    private void btnSubCategorias_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SubCategoriaProductoPage());
    }

    private void btnProductos_Clicked(object sender, EventArgs e)
    {

    }

    private void btnAceptarPedidos_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AceptarFacturas());
    }

    private void btnIngredientes_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new IngredientePage());
    }
}