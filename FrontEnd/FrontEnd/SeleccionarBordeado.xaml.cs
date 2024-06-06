using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FrontEnd;

public partial class SeleccionarBordeado : ContentPage
{
	public SeleccionarBordeado()
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
            List<Ingrediente> filtrada = new List<Ingrediente>();
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
                if (ing.idCategoriaIngrediente == 4)
                {
                    imgTopping.Source = ing.dscURLImagen;
                }
                if (ing.idCategoriaIngrediente != 5)
                {
                    filtrada.Add(ing);
                }
            }
            ingredientesSeleccionados = filtrada;
            ingredienteSeleccionado.idIngrediente = 13;
            ingredienteSeleccionado.dscNombre = "No agregado";
            ingredienteSeleccionado.dscDescripcion = "No agregado";
            ingredienteSeleccionado.dscURLImagen = "No agregado";
            ingredienteSeleccionado.numPrecio = 0;
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
                            if (ingre.idCategoriaIngrediente == 5)
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
            imgBordeado.Source = selectedIngredient.dscURLImagen;
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
        var siguiente = new SeleccionarBubbles();
        siguiente.BindingContext = ingredientesSeleccionados;
        Navigation.PushAsync(siguiente);
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }

    private void btnVolver_Clicked(object sender, EventArgs e)
    {
        var siguiente = new SeleccionarTopping();
        siguiente.BindingContext = ingredientesSeleccionados;
        Navigation.PushAsync(siguiente);
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    }
}