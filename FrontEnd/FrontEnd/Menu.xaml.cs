using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace FrontEnd;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
        CargarProductos();
    }

    private ObservableCollection<Producto> _listaProducto = new ObservableCollection<Producto>();
    public ObservableCollection<Producto> listaProducto
    {
        get { return _listaProducto; }
        set
        {
            _listaProducto = value;
            OnPropertyChanged(nameof(listaProducto));
        }
    }


    private async void CargarProductos()
    {
        listaProducto.Clear();
        var productos = await ProductosDesdeApi();
        foreach (var product in productos)
        {
            listaProducto.Add(product);
        }
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

    }

    private void btnRamen_Clicked(object sender, EventArgs e)
    {

    }

    private void btnCornDog_Clicked(object sender, EventArgs e)
    {

    }

    private void btnSushis_Clicked(object sender, EventArgs e)
    {

    }
}