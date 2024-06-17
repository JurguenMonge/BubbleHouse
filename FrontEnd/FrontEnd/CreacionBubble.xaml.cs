using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FrontEnd;

public partial class CreacionBubble : ContentPage
{
	public CreacionBubble()
	{
		InitializeComponent();
        CargarIngredientes();
    }

    private Ingrediente _selectedIngredient;
    private string _selectedIngredientImageUrl;
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
                        List<Ingrediente> listaFiltrada = new List<Ingrediente>();
                        foreach(Ingrediente ingre in res.listaIngredientes)
                        {
                            if(ingre.idCategoriaIngrediente == 7)
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

    public Ingrediente SelectedIngredient
    {
        get => _selectedIngredient;
        set
        {
            if (_selectedIngredient != value)
            {
                _selectedIngredient = value;
                OnPropertyChanged(nameof(SelectedIngredient));
                SelectedIngredientImageUrl = _selectedIngredient?.dscURLImagen;
            }
        }
    }

    public string SelectedIngredientImageUrl
    {
        get => _selectedIngredientImageUrl;
        set
        {
            if (_selectedIngredientImageUrl != value)
            {
                _selectedIngredientImageUrl = value;
                OnPropertyChanged(nameof(SelectedIngredientImageUrl));
            }
        }
    }

    private void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var radioButton = sender as RadioButton;
        if (radioButton != null && e.Value)
        {
            var selectedIngredient = radioButton.BindingContext as Ingrediente;
            if (selectedIngredient != null)
            {
                var viewModel = BindingContext as CreacionBubble;
                viewModel.SelectedIngredient = selectedIngredient;
            }
        }
    }
}