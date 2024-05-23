using FrontEnd.Entidades;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Pages;

public partial class SubCategoriaProductoPage : ContentPage
{
	public SubCategoriaProductoPage()
	{
		InitializeComponent();
        CargarSubCategorias();
    }

    private List<SubcategoriaProducto> _listaSubCategoriasProducto = new List<SubcategoriaProducto>();

    #region refrezcarCompomentes
    public List<SubcategoriaProducto> listaSubCategoriasProducto
    {
        get { return _listaSubCategoriasProducto; }
        set
        {
            _listaSubCategoriasProducto = value;
            OnPropertyChanged(nameof(listaSubCategoriasProducto));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion


    private async void CargarSubCategorias()
    {
        listaSubCategoriasProducto = await SubCategoriasDesdeApi();
        BindingContext = this;
    }

    private async Task<List<SubcategoriaProducto>> SubCategoriasDesdeApi()
    {
        List<SubcategoriaProducto> retornarSubCategoriasApi = new List<SubcategoriaProducto>();
        String laURL = "https://localhost:44311/api/subCategoriaProducto/obtener";

        try
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laURL);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerSubCategoriaProducto res = JsonConvert.DeserializeObject<ResObtenerSubCategoriaProducto>(responseContent);

                    if (res.Resultado)
                    {
                        retornarSubCategoriasApi = res.listaSubCategoriaProducto;
                        Console.WriteLine(retornarSubCategoriasApi);
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

        return retornarSubCategoriasApi;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var button = sender as Frame;
        var item = button?.BindingContext as SubcategoriaProducto;

        if (item != null)
        {
            var formularioSubCate = new FormSubCategoriaProducto();
            formularioSubCate.BindingContext = item;
            Navigation.PushAsync(formularioSubCate);
        }
    }

    private void btnCrearSubCategoria_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new FormSubCategoriaProducto());
    }
}