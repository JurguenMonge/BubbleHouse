using FrontEnd.Controller;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FrontEnd.Pages;

public partial class RecetaPage : ContentPage
{
	public RecetaPage()
	{
		InitializeComponent();
        CargarRecetas();

    }

    private ObservableCollection<RecetaCompleta> _listaReceta = new ObservableCollection<RecetaCompleta>();

    #region refrezcarCompomentes
    public ObservableCollection<RecetaCompleta> listaReceta
    {
        get { return _listaReceta; }
        set
        {
            _listaReceta = value;
            OnPropertyChanged(nameof(listaReceta));
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private async void CargarRecetas()
    {
        listaReceta.Clear();
        var recetas = await RecetasDesdeApi();
        foreach (var receta in recetas)
        {
            listaReceta.Add(receta);
        }
        BindingContext = this;
    }

    private async Task<List<RecetaCompleta>> RecetasDesdeApi()
    {
        List<RecetaCompleta> recetasApi = new List<RecetaCompleta>();
        String laURL = "https://apibubblehouse.azurewebsites.net/api/receta/obtener";

        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laURL);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerRecetas res = JsonConvert.DeserializeObject<ResObtenerRecetas>(responseContent);

                    if (res.Resultado)
                    {
                        recetasApi = res.listaRecetas;
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

        return recetasApi;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var receta = (RecetaCompleta)button.BindingContext;
        bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de eliminar esta receta?", "Aceptar", "Cancelar");
        if (answer)
        {
            RecetaController controller = new RecetaController();
            try
            {
                ResReceta res = new ResReceta();
                res = await controller.EliminarReceta(receta.recetaId);
                if (res.Resultado)
                {
                    await DisplayAlert("Eliminación Exitosa", "Receta eliminada con éxito", "Aceptar");
                    CargarRecetas();
                }
                else
                {
                    await DisplayAlert("Error en eliminación", "Sucedió un error al eliminar: " + res.ListaDeErrores.First(), "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error interno", "Por favor, reinstale la aplicación", "Aceptar");
            }
        }
    }

   
}