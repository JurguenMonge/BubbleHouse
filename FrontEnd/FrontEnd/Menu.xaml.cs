using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using Newtonsoft.Json;
using System.Collections.ObjectModel;


namespace FrontEnd;

public partial class Menu : ContentPage
{
    private ObservableCollection<Producto> _listaProducto = new ObservableCollection<Producto>();
    private ObservableCollection<Producto> _productosFiltrados = new ObservableCollection<Producto>();

    public Menu()
    {
        InitializeComponent();
        CargarProductos();

    }

    public ObservableCollection<Producto> listaProducto
    {
        get { return _productosFiltrados; }
        set
        {
            _productosFiltrados = value;
            OnPropertyChanged(nameof(listaProducto));
        }
    }

    private async void CargarProductos()
    {
        var productos = await ProductosDesdeApi();
        foreach (var product in productos)
        {
            _listaProducto.Add(product);
        }
        _productosFiltrados = new ObservableCollection<Producto>(_listaProducto); 
        BindingContext = this;
    }

    private async Task<List<Producto>> ProductosDesdeApi()
    {
        List<Producto> retornarProductosApi = new List<Producto>();
        String laURL = "https://localhost:44311/api/producto/obtener";

        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laURL);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerProducto res = JsonConvert.DeserializeObject<ResObtenerProducto>(responseContent);

                    if (res.Resultado)
                    {
                        retornarProductosApi = res.listaProductos;
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

        return retornarProductosApi;
    }

    private void btnCombos_Clicked(object sender, EventArgs e)
    {
        var productosFiltrados = _listaProducto.Where(p =>
            p.subcategoriaProducto.dscNombreSubCategoria.Contains("Combo 1") ||
            p.subcategoriaProducto.dscNombreSubCategoria.Contains("Combo 2") ||
            p.subcategoriaProducto.dscNombreSubCategoria.Contains("Combo 3")).ToList();

        ActualizarListaFiltrada(productosFiltrados);
    }

    private void btnRamen_Clicked(object sender, EventArgs e)
    {
        var productosFiltrados = _listaProducto.Where(p =>
            p.categoriaProducto.dscNombreCategoria.Contains("Ramen")).ToList();

        ActualizarListaFiltrada(productosFiltrados);
    }

    private void btnCornDog_Clicked(object sender, EventArgs e)
    {
        var productosFiltrados = _listaProducto.Where(p =>
            p.categoriaProducto.dscNombreCategoria.Contains("Corndog")).ToList();

        ActualizarListaFiltrada(productosFiltrados);
    }

    private void btnSushis_Clicked(object sender, EventArgs e)
    {
        var productosFiltrados = _listaProducto.Where(p =>
            p.categoriaProducto.dscNombreCategoria.Contains("Sushi")).ToList();

        ActualizarListaFiltrada(productosFiltrados);
    }
    private void btnBubbles_Clicked(object sender, EventArgs e)
    {
        var productosFiltrados = _listaProducto.Where(p =>
          p.categoriaProducto.dscNombreCategoria.Contains("Bubble Te")).ToList();

        ActualizarListaFiltrada(productosFiltrados);
    }
    private void ActualizarListaFiltrada(List<Producto> productosFiltrados)
    {
        _productosFiltrados.Clear();
        foreach (var producto in productosFiltrados)
        {
            _productosFiltrados.Add(producto);
        }
    }

    private async Task btnMenu_ClickedAsync(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new Menu());
    }

    private async Task<bool> MostrarModalAgregarCarrito(Producto producto)
    {
        bool agregar = await DisplayAlert("Agregar al carrito", $"¿Deseas agregar {producto.nombreProducto} al carrito?", "Sí", "Cancelar");

        if (agregar)
        {
            // Crear un objeto ContenedorProducto 
            ContenedorProducto item = new ContenedorProducto();
            item.nombreProducto = producto.nombreProducto;
            item.precio = producto.precio;

            // Agregar al carrito
            Entidades.Entidad.Carrito.listaContenedorProducto.Add(item);
        }

        return agregar;
    }

    private async void ProductoFrameTapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        var producto = frame.BindingContext as Producto;

        bool resultado = await MostrarModalAgregarCarrito(producto);

       
        if (resultado)
        {
            await DisplayAlert("Éxito", "El producto se agregó al carrito.", "Aceptar");
        }
    }

}
