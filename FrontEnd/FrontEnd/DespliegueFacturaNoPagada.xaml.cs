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

    private void btncambios_Clicked(object sender, EventArgs e)
    {

    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {

    }
}