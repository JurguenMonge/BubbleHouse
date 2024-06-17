using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FrontEnd.Pages;

public partial class IngredientePage : ContentPage
{
	public IngredientePage()
	{
		InitializeComponent();
        CargarIngredientes();

    }

    private ObservableCollection<Ingrediente> _listaIngrediente = new ObservableCollection<Ingrediente>();

    #region refrezcarCompomentes
    public ObservableCollection<Ingrediente> listaIngrediente
    {
        get { return _listaIngrediente; }
        set
        {
            _listaIngrediente = value;
            OnPropertyChanged(nameof(listaIngrediente));
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion


    private async void CargarIngredientes()
    {
        listaIngrediente.Clear();
        var ingredientes = await IngredientesDesdeApi();
        foreach (var ingrediente in ingredientes)
        {
            listaIngrediente.Add(ingrediente);
        }
        BindingContext = this;
    }

    private async Task<List<Ingrediente>> IngredientesDesdeApi()
    {
        List<Ingrediente> retornarIngredientesApi = new List<Ingrediente>();
        String laURL = "https://apibubblehouse.azurewebsites.net/api/ingrediente/obtener";

        try
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laURL);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerIngredientes res = JsonConvert.DeserializeObject<ResObtenerIngredientes>(responseContent);

                    if (res.Resultado)
                    {
                        retornarIngredientesApi = res.listaIngredientes;
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

        return retornarIngredientesApi;
    }

    private void TapModificarIngrediente(object sender, TappedEventArgs e)
    {
        var button = sender as Frame;
        var item = button?.BindingContext as Ingrediente;

        if (item != null)
        {
            var formularioIngrediente = new FormIngrediente();
            formularioIngrediente.BindingContext = item;
            Navigation.PushAsync(formularioIngrediente);
        }
    }

    private void TapAgregarIngrediente(object sender, TappedEventArgs e)
    {
        

        Navigation.PushAsync(new FormIngrediente());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarIngredientes(); 
    }

}