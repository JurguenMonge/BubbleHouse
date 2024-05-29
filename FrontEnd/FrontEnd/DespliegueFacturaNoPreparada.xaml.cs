using FrontEnd.Controller;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Request;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace FrontEnd;

public partial class DespliegueFacturaNoPreparada : ContentPage
{
    private List<ContenedorProducto> _listaDeProductos = new List<ContenedorProducto>();
    Factura fact = new Factura();
    private bool isFirstLoad = true;

    public DespliegueFacturaNoPreparada()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is Factura factura)
        {
            fact = factura;
            lblIdFactura.Text = "Numero De Pedido: " + factura.idFactura.ToString();
            lblfecha.Text = "Fecha y Hora: " + factura.fecha.ToString();

            if (isFirstLoad)
            {
                isFirstLoad = false;
                await CargarProductosAsync();
            }
            decimal total = 0;
            foreach (ContenedorProducto cont in listaDeProductos)
            {
                total = total + cont.numSubtotal;
            }
        }
    }

    #region RefrescarComponentes
    public List<ContenedorProducto> listaDeProductos
    {
        get { return _listaDeProductos; }
        set
        {
            _listaDeProductos = value;
            OnPropertyChanged(nameof(listaDeProductos));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private async Task CargarProductosAsync()
    {
        listaDeProductos = await productosDesdeApi();
        BindingContext = this;
    }

    private async Task<List<ContenedorProducto>> productosDesdeApi()
    {
        List<ContenedorProducto> retornarProdutosApi = new List<ContenedorProducto>();
        String laURL = "https://localhost:44311/api/factura/obtenerProductos";

        try
        {

            using (HttpClient httpClient = new HttpClient())
            {
                ReqFactura req = new ReqFactura();
                req.Factura = fact;
                req.idSesion = Preferences.Get("IdSesion", string.Empty);
                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(laURL, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerProductosFactura res = JsonConvert.DeserializeObject<ResObtenerProductosFactura>(responseContent);

                    if (res.Resultado)
                    {
                        retornarProdutosApi = res.Contenedores;
                        Console.WriteLine(retornarProdutosApi);
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el backend");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error interno");
        }
        spinner.IsVisible = false;
        spinner.IsRunning = false;
        return retornarProdutosApi;
    }

    private async void btnAceptar_Clicked(object sender, EventArgs e)
    {
        ReqFactura req = new ReqFactura();
        List<ContenedorProductoFactura> list = new List<ContenedorProductoFactura>();
        foreach (ContenedorProducto contenedor in listaDeProductos)
        {
            ContenedorProductoFactura contenedorFactura = new ContenedorProductoFactura();
            contenedorFactura.idFactura = contenedor.idFactura;
            contenedorFactura.IdRFacturaProducto = contenedor.IdRFacturaProducto;
            contenedorFactura.numSubtotal = contenedor.numSubtotal;
            contenedorFactura.descuento = contenedor.descuento;
            contenedorFactura.numCantidad = contenedor.numCantidad;
            Producto producto = new Producto();
            producto.idProducto = contenedor.idProducto;
            producto.precio = contenedor.precio;

            producto.subcategoriaProducto = contenedor.subcategoriaProducto;
            contenedorFactura.producto = producto;
            list.Add(contenedorFactura);
        }
        fact.productosList = list;
        req.Factura = fact;
        try
        {
            ResFactura res = new ResFactura();
            res = await FacturaController.ModificarEstadoCompletadoFactura(req);
            if (res.Resultado)
            {
                DisplayAlert("Pedido Aceptado", "Factura aceptada con exito", "Aceptar");
                Navigation.PushAsync(new AceptarFacturas());
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            }
            else
            {
            DisplayAlert("Error en factura", "Sucedio un error al modificar la factura" + res.ListaDeErrores.First(), "Aceptar");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error interno", "Porfavor reinstale la aplicacion", "Aceptar");
        }
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        ReqFactura req = new ReqFactura();
        req.Factura = fact;
        try
        {
            ResFactura res = new ResFactura();
            res = await FacturaController.EliminarFactura(req);
            if (res.Resultado)
            {
                DisplayAlert("Pedido cancelado", "pedido cancelado con exito", "Aceptar");
                Navigation.PushAsync(new AceptarFacturas());
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            }
            else
            {
                DisplayAlert("Error en factura", "Sucedio un error al eliminar la factura" + res.ListaDeErrores.First(), "Aceptar");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error interno", "Porfavor reinstale la aplicacion", "Aceptar");
        }
    }

    private void btnVolver_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CompletarFacturas());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }
}