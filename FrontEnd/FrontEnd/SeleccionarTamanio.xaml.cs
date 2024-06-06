using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FrontEnd;

public partial class SeleccionarTamanio : ContentPage
{
    public SeleccionarTamanio()
    {
        InitializeComponent();
        CargarIngredientes();
    }

    private ObservableCollection<Ingrediente> _listaIngrediente = new ObservableCollection<Ingrediente>();
    private Ingrediente ingredienteSeleccionado = new Ingrediente();
    private bool selecionado = false;

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
                            if (ingre.idCategoriaIngrediente == 7)
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
            imgVaso.Source = selectedIngredient.dscURLImagen;
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
        List<Ingrediente> ingredientesSeleccionados = new List<Ingrediente>();
        ingredientesSeleccionados.Add(ingredienteSeleccionado);
        var siguiente = new SeleccionarLacteo();
        siguiente.BindingContext = ingredientesSeleccionados;
        Navigation.PushAsync(siguiente);
    }

}