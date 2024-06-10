using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using FrontEnd.Controller;

namespace FrontEnd.Pages;

public partial class ProductoPage : ContentPage
{
    public ProductoPage()
    {
        InitializeComponent();
        CargarProductos();

    }

    private ObservableCollection<Producto> _listaProducto = new ObservableCollection<Producto>();

    #region refrezcarCompomentes
    public ObservableCollection<Producto> listaProducto
    {
        get { return _listaProducto; }
        set
        {
            _listaProducto = value;
            OnPropertyChanged(nameof(listaProducto));
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

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

    private void TapModificarProducto(object sender, TappedEventArgs e)
    {
        var button = sender as Frame;
        var item = button?.BindingContext as Producto;

        if (item != null)
        {
            var formularioProducto = new FormProducto();
            formularioProducto.BindingContext = item;
            Navigation.PushAsync(formularioProducto);
        }
    }

    private void TapAgregarIngrediente(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new FormProducto());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarProductos();
    }

    private void estado_Toggled(object sender, ToggledEventArgs e)
    {
        
    }
}