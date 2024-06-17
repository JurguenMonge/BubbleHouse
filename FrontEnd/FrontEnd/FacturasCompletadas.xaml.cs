using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Request;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace FrontEnd;

public partial class FacturasCompletadas : ContentPage
{
    private List<Factura> _listaDeFacturas = new List<Factura>();
    public FacturasCompletadas()
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

    private async void CargarFacturas()
    {
        listaDeFacturas = await FacturasDesdeApi();
        BindingContext = this;
    }

    private async Task<List<Factura>> FacturasDesdeApi()
    {
        List<Factura> retornarPublicacionApi = new List<Factura>();
        String laURL = "https://apibubblehouse.azurewebsites.net/api/factura/obtenerCompletadas";
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
                        Console.WriteLine("No se encontró el backend");
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

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var button = sender as Frame;
        var item = button?.BindingContext as Factura;

        if (item != null)
        {
            var formularioPractica = new DespliegueFactura();
            formularioPractica.BindingContext = item;
            Navigation.PushAsync(formularioPractica);
        }
    }

    private void btnPorPreparar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CompletarFacturas());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }

    private void btnPorCobrar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AceptarFacturas());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }
}