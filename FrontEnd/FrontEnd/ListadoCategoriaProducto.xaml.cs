using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd;

public partial class ListadoCategoriaProducto : ContentPage
{
	public ListadoCategoriaProducto()
	{
        InitializeComponent();
        CargarPublicaciones();
    }

    private List<CategoriaProducto> _listaDeCategoriasProducto = new List<CategoriaProducto>();

    //Refrezca los componentes una vez se pintan en la vista

    #region refrezcarCompomentes
    public List<CategoriaProducto> listaDeCategoriasProducto
    {
        get { return _listaDeCategoriasProducto; }
        set
        {
            _listaDeCategoriasProducto = value;
            OnPropertyChanged(nameof(listaDeCategoriasProducto));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private async void CargarPublicaciones()
    {
        listaDeCategoriasProducto = await CategoriasDesdeApi();
        BindingContext = this;
    }

    private async Task<List<CategoriaProducto>> CategoriasDesdeApi()
    {
        List<CategoriaProducto> retornarPublicacionApi = new List<CategoriaProducto>();
        String laURL = "https://apibubblehouse.azurewebsites.net/api/categoriaProducto/obtener";

        try
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laURL);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerCategoriaProducto res = JsonConvert.DeserializeObject<ResObtenerCategoriaProducto>(responseContent);

                    if (res.Resultado)
                    {
                        retornarPublicacionApi = res.listaCategoriaProducto;
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
        var item = button?.BindingContext as CategoriaProducto;

        if (item != null)
        {
            var formularioPractica = new FormularioCategoriaProducto();
            formularioPractica.BindingContext = item;
            Navigation.PushAsync(formularioPractica);
        }
    }


    private void TapCrearCategoria(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new FormularioCategoriaProducto());
    }
}