using FrontEnd.Controller;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Request;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace FrontEnd;

public partial class DespliegueFacturaNoPagada : ContentPage
{
    bool aceptado = false;
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
            fact.numTotal = (float)total;
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
    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.BindingContext as ContenedorProducto;

        try
        {
            if (item != null)
            {
                ReqContenedorProducto req = new ReqContenedorProducto();
                ContenedorProductoFactura contenedor = new ContenedorProductoFactura();
                contenedor.IdRFacturaProducto = item.IdRFacturaProducto;
                req.contenedor = contenedor;
                ResFactura res = new ResFactura();
                res = await FacturaController.EliminarProducto(req);

                if (res.Resultado)
                {
                    await CargarProductosAsync();
                    var formularioPractica = new DespliegueFacturaNoPagada();
                    formularioPractica.BindingContext = fact;
                    DisplayAlert("Producto eliminado", "producto eliminado con exito", "Aceptar");
                    Navigation.PushAsync(formularioPractica);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                else
                {
                    await DisplayAlert("Error en factura", "Sucedio un error al eliminar el producto" + res.ListaDeErrores.First(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "No se pudo obtener el elemento seleccionado", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Por favor reinstale la aplicación", "Aceptar");
        }
    }

    private async void btnAceptar_Clicked(object sender, EventArgs e)
    {
        aceptado = true;
        btncambios_Clicked(sender, e);
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
            decimal valor = decimal.Parse(txtefectivo.Text, CultureInfo.InvariantCulture);
            if (selecionado == 1 && valor == 0)
            {
                DisplayAlert("Efectivo", "El efectivo no a sido ingresado", "Aceptar");
            }
            else if(float.Parse(txtefectivo.Text) < fact.numTotal)
            {
                DisplayAlert("Efectivo", "El efectivo ingresado es menor al total a cobrar", "Aceptar");
            }
            else
            {
                ResFactura res = new ResFactura();
                res = await FacturaController.ModificarEstadoNoPreparadoFactura(req);
                if (res.Resultado)
                {
                    if (selecionado == 1)
                    {
                        DisplayAlert("Pedido aceptado", "Su cambio: " + (valor - (decimal)req.Factura.numTotal), "Aceptar");
                    }
                    else
                    {
                        DisplayAlert("Pedido Aceptado", "Factura aceptada con exito", "Aceptar");
                    }
                    Navigation.PushAsync(new AceptarFacturas());
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                else
                {
                    DisplayAlert("Error en factura", "Sucedio un error al modificar la factura" + res.ListaDeErrores.First(), "Aceptar");
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error interno", "Porfavor reinstale la aplicacion", "Aceptar");
        }
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
                if (!aceptado)
                {
                    DisplayAlert("Factura Modificada", "Factura modificada con exito", "Aceptar");
                    var formularioPractica = new DespliegueFacturaNoPagada();
                    formularioPractica.BindingContext = fact;
                    Navigation.PushAsync(formularioPractica);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
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

    private void btnAceptar_Clicked_1(object sender, EventArgs e)
    {

    }
}