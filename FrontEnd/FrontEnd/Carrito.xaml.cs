using FrontEnd.Controller;
using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Request;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FrontEnd;

public partial class Carrito : ContentPage
{
    public Carrito()
    {
        InitializeComponent();
    }
    private List<ContenedorProducto> _listaCarrito = new List<ContenedorProducto>();
    private bool isFirstLoad = true;
    private float totalglobal;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        decimal total = 0;
        foreach (ContenedorProducto cat in Entidades.Entidad.Carrito.listaContenedorProducto)
        {
            total = total + (decimal)(cat.precio * cat.numCantidad); 
        }
        lbltotal.Text = "Total: " + total.ToString();
        totalglobal = (float)total;
        listaCarrito = Entidades.Entidad.Carrito.listaContenedorProducto;
        if (isFirstLoad)
        {
            isFirstLoad = false;
            await CargarProductos();
        }
    }

    #region refrezcarCompomentes
    public List<ContenedorProducto> listaCarrito
    {
        get { return _listaCarrito; }
        set
        {
            _listaCarrito = value;
            OnPropertyChanged(nameof(listaCarrito));
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    private async void btnAceptarCompra_Clicked(object sender, EventArgs e)
    {
        ReqFactura req = new ReqFactura();
        List<ContenedorProductoFactura> list = new List<ContenedorProductoFactura>();
        foreach (ContenedorProducto contenedor in listaCarrito)
        {
            ContenedorProductoFactura contenedorFactura = new ContenedorProductoFactura();
            contenedorFactura.idFactura = contenedor.idFactura;
            contenedorFactura.IdRFacturaProducto = contenedor.IdRFacturaProducto;
            
            if(contenedor.numSubtotal == 0)
            {
                contenedorFactura.numSubtotal = (decimal)contenedor.numCantidad * (decimal)contenedor.precio;
            }
            else
            {
                contenedorFactura.numSubtotal = contenedor.numSubtotal;
            }
            contenedorFactura.descuento = contenedor.descuento;
            contenedorFactura.numCantidad = contenedor.numCantidad;
            Producto producto = new Producto();
            producto.idProducto = contenedor.idProducto;
            producto.precio = contenedor.precio;
            producto.subcategoriaProducto = contenedor.subcategoriaProducto;
            contenedorFactura.producto = producto;
            list.Add(contenedorFactura);
        }
        Factura fact = new Factura();
        fact.productosList = list;
        fact.numTotal = totalglobal;
        req.Factura = fact;
        try
        {
            ResFactura res = new ResFactura();
            res = await FacturaController.insertarFactura(req);
            if (res.Resultado)
            {
                Entidades.Entidad.Carrito.listaContenedorProducto = new List<ContenedorProducto>();
                await DisplayAlert("Compra Realizada", "Gracias por su compra. Dentro de poco se efectuará el cobro.", "Aceptar");
                await Navigation.PushAsync(new Carrito());
            }
            else
            {
                DisplayAlert("Error en pedido", "Sucedio un error al completar el pedido" + res.ListaDeErrores.First(), "Aceptar");
            }
          
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Error interno", "Reinstale la aplicacion", "Aceptar");
        }

        
    }

    private void btncambiosCompra_Clicked(object sender, EventArgs e)
    {
        Entidades.Entidad.Carrito.listaContenedorProducto = _listaCarrito;
        Navigation.PushAsync(new Carrito());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }

    private void btnCancelarCompra_Clicked(object sender, EventArgs e)
    {
        Entidades.Entidad.Carrito.listaContenedorProducto = new List<ContenedorProducto>();
        Navigation.PushAsync(new Carrito());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }

    private void btnEliminarCompra_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.BindingContext as ContenedorProducto;
        Entidades.Entidad.Carrito.listaContenedorProducto.Remove(item);
        Navigation.PushAsync(new Carrito());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }

    private async Task CargarProductos()
    {
        BindingContext = this;
    }


}