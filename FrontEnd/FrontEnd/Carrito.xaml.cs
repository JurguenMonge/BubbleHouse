using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using Newtonsoft.Json;

namespace FrontEnd;

public partial class Carrito : ContentPage
{
    public Carrito()
    {
        InitializeComponent();
        CargarProductos();
    }

    private List<Producto> _listaProducto = new List<Producto>();

    #region refrezcarCompomentes
    public List<Producto> listaProducto
    {
        get { return _listaProducto; }
        set
        {
            _listaProducto = value;
            OnPropertyChanged(nameof(listaProducto));
        }
    }

    private async void btnAceptarCompra_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Compra Realizada", "Gracias por su compra. Dentro de poco se efectuará el cobro.", "Aceptar");
    }

    private void btncambiosCompra_Clicked(object sender, EventArgs e)
    {

    }

    private void btnCancelarCompra_Clicked(object sender, EventArgs e)
    {

    }

    private void btnEliminarCompra_Clicked(object sender, EventArgs e)
    {

    }

    private async void CargarProductos()
    {
        listaProducto = await ProductosDesdeApi();
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

    #endregion
}