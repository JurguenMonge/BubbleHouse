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

    }

    private void btnProductos_Clicked(object sender, EventArgs e)
    {

    }
}