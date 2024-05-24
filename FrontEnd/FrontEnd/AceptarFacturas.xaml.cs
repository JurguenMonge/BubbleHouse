using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Request;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace FrontEnd;

public partial class AceptarFacturas : ContentPage
{
    private List<Factura> _listaDeFacturas = new List<Factura>();

    public AceptarFacturas()
	{
        InitializeComponent();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
        CargarFacturas();                                                                                                                                                                                                                                                          
    }
    
    #region refrezcarCompomentes
    public List<Factura> listaDeFacturas
    {
        get { return _listaDeFacturas; }
        set
        {
            _listaDeFacturas = value;
            OnPropertyChanged(nameof(listaDeFacturas));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    int selecionado = 0;
    private async void CargarFacturas()
    {
        listaDeFacturas = await FacturasDesdeApi();
        BindingContext = this;
    }

    private async Task<List<Factura>> FacturasDesdeApi()
    {
        List<Factura> retornarPublicacionApi = new List<Factura>();
        String laURL = "https://localhost:44311/api/factura/obtenerNoPagadas";
        try
        {

            using (HttpClient httpClient = new HttpClient())
            {
                ReqFactura req = new ReqFactura();
                req.idSesion = Preferences.Get("IdSesion", string.Empty);
                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(laURL, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerFactura res = JsonConvert.DeserializeObject<ResObtenerFactura>(responseContent);

                    if (res.Resultado)
                    {
                        retornarPublicacionApi = res.listaFacturas;
                        Console.WriteLine(retornarPublicacionApi);
                    }
                    else
                    {
                        Console.WriteLine("No se encontr� el backend");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error interno");
        }

        return retornarPublicacionApi;
    }

    private void btnTarjeta_Clicked(object sender, EventArgs e)
    {
        if(selecionado != 0)
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
        Navigation.PushAsync(new FormularioCategoriaProducto());
    }

}