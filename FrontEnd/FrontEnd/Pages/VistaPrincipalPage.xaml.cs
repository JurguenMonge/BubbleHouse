namespace FrontEnd.Pages;

public partial class VistaPrincipalPage : TabbedPage
{
	public VistaPrincipalPage()
	{
		InitializeComponent();

        var aceptarFacturasPage = new NavigationPage(new AceptarFacturas());
        aceptarFacturasPage.Title = "Menú";
        aceptarFacturasPage.IconImageSource = "menu.svg";
        NavigationPage.SetHasNavigationBar(aceptarFacturasPage, true);

        var crearBubble = new NavigationPage(new SeleccionarTamanio());
        crearBubble.Title = "Crear Bubble";
        crearBubble.IconImageSource = "bubble.svg";
        NavigationPage.SetHasNavigationBar(crearBubble, true);

        var carritoPage = new NavigationPage(new Carrito());
        carritoPage.Title = "Carrito";
        carritoPage.IconImageSource = "carrito.svg";
        NavigationPage.SetHasNavigationBar(carritoPage, true);

        

        Children.Add(aceptarFacturasPage);
        Children.Add(crearBubble);
        Children.Add(carritoPage);
    }
}