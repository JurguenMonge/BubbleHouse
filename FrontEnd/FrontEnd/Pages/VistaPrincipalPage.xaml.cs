namespace FrontEnd.Pages;

public partial class VistaPrincipalPage : TabbedPage
{
	public VistaPrincipalPage()
	{
		InitializeComponent();

        var MenuPage = new NavigationPage(new Menu());
        MenuPage.Title = "Menú";
        MenuPage.IconImageSource = "menuOpcion.svg";
        NavigationPage.SetHasNavigationBar(MenuPage, true);

        var crearBubble = new NavigationPage(new SeleccionarTamanio());
        crearBubble.Title = "Crear Bubble";
        crearBubble.IconImageSource = "bubble.svg";
        NavigationPage.SetHasNavigationBar(crearBubble, true);

        var carritoPage = new NavigationPage(new Carrito());
        carritoPage.Title = "Carrito";
        carritoPage.IconImageSource = "carrito.svg";
        NavigationPage.SetHasNavigationBar(carritoPage, true);


        Children.Add(MenuPage);
        Children.Add(crearBubble);
        Children.Add(carritoPage);
       
    }
}