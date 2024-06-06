using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FrontEnd;

public partial class SeleccionarTopping : ContentPage
{
	public SeleccionarTopping()
	{
		InitializeComponent();
	}
    private ObservableCollection<Ingrediente> _listaIngrediente = new ObservableCollection<Ingrediente>();
    private Ingrediente ingredienteSeleccionado = new Ingrediente();
    private List<Ingrediente> ingredientesSeleccionados = new List<Ingrediente>();
    private bool selecionado = false;
    private bool isFirstLoad = true;

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is List<Ingrediente> ingredientes)
        {
            foreach (Ingrediente ing in ingredientes)
            {
                if (ing.idCategoriaIngrediente == 7)
                {
                    imgVaso.Source = ing.dscURLImagen;
                }
                if (ing.idCategoriaIngrediente == 2)
                {
                    imgSabor.Source = ing.dscURLImagen;
                }
            }
            ingredientesSeleccionados = ingredientes;
            if (isFirstLoad)
            {
                isFirstLoad = false;
                await CargarIngredientes();
            }
        }
    }

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

    private async Task CargarIngredientes()
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
        String laURL = "https://localhost:44311/api/ingrediente/obtener";

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
                        List<Ingrediente> listaFiltrada = new List<Ingrediente>();
                        foreach (Ingrediente ingre in res.listaIngredientes)
                        {
                            if (ingre.idCategoriaIngrediente == 4)
                            {
                                listaFiltrada.Add(ingre);
                            }
                        }
                        retornarIngredientesApi = listaFiltrada;
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

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var button = sender as RadioButton;
        var selectedIngredient = button?.BindingContext as Ingrediente;
        if (selectedIngredient != null)
        {
            imgTopping.Source = selectedIngredient.dscURLImagen;
            ingredienteSeleccionado = selectedIngredient;
            if (selecionado == false)
            {
                btnSiguiente.IsEnabled = true;
                selecionado = true;
            }
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ingredientesSeleccionados.Add(ingredienteSeleccionado);
        var siguiente = new SeleccionarBordeado();
        siguiente.BindingContext = ingredientesSeleccionados;
        Navigation.PushAsync(siguiente);
    }
}