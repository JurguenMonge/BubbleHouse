using FrontEnd.Controller;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Request;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace FrontEnd;

public partial class DespliegueFacturaNoPagada : ContentPage
{

    private List<ContenedorProducto> _listaDeProductos = new List<ContenedorProducto>();
    int selecionado = 0;
    private bool isFirstLoad = true;
    Factura fact = new Factura();

    public DespliegueFacturaNoPagada()
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
            foreach(ContenedorProducto cont in listaDeProductos)
            {
                total = total + cont.numSubtotal;
            }
            lbltotal.Text = "Total: " + total;
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
    

    private void btnTarjeta_Clicked(object sender, EventArgs e)
    {
        if (selecionado != 0)
        {
            btnTarjeta.BackgroundColor = Colors.Black;
            btnTarjeta.TextColor = Colors.White;
            btnEfectivo.BackgroundColor = Colors.White;
            btnEfectivo.TextColor = Colors.Black;
            selecionado = 0;
            txtefectivo.IsVisible = false;
            lblefectivo.IsVisible = false;
        }
    }

    private void btnEfectivo_Clicked(object sender, EventArgs e)
    {
        if (selecionado == 0)
        {
            btnTarjeta.BackgroundColor = Colors.White;
            btnTarjeta.TextColor = Colors.Black;
            btnEfectivo.BackgroundColor = Colors.Black;
            btnEfectivo.TextColor = Colors.White;
            selecionado = 1;
            txtefectivo.IsVisible = true;
            lblefectivo.IsVisible = true;
        }
    }
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnAceptar_Clicked(object sender, EventArgs e)
    {

    }

    private async void btncambios_Clicked(object sender, EventArgs e)
    {
        ReqFactura req = new ReqFactura();
        List<ContenedorProductoFactura> list = new List<ContenedorProductoFactura>();
        foreach(ContenedorProducto contenedor in listaDeProductos)
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
            res = await FacturaController.ModificarFactura(req);
            if (res.Resultado)
            {
                DisplayAlert("Factura Modificada", "Factura modificada con exito", "Aceptar");
                var formularioPractica = new DespliegueFacturaNoPagada();
                formularioPractica.BindingContext = fact;
                Navigation.PushAsync(formularioPractica);
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

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {

    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        var item = entry?.BindingContext as ContenedorProducto;
        decimal precio = 0;

        if (item != null)
        {
            if (item.descuento != 0)
            {
                precio = (decimal)item.precio - ((decimal)item.precio * (item.descuento / 100));
            }
            else
            {
                precio = (decimal)item.precio;
            }

            // Actualizar la cantidad del producto
            if (int.TryParse(entry.Text, out int nuevaCantidad))
            {
                item.numCantidad = nuevaCantidad;
            }
            else
            {
                // Manejar el caso en el que la entrada no sea un número válido
                // Puedes mostrar un mensaje de error o realizar alguna otra acción.
            }

            // Calcular el nuevo subtotal
            item.numSubtotal = item.numCantidad * precio;

            // Actualizar el producto modificado en la lista
            var index = listaDeProductos.FindIndex(p => p == item);
            if (index != -1)
            {
                listaDeProductos[index] = item;
                OnPropertyChanged(nameof(listaDeProductos));
            }
        }
    }
}