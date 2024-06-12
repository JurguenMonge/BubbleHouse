using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using FrontEnd.Controller;
using FrontEnd.Entidades.Response;

namespace FrontEnd.Pages;

public partial class ProductoPage : ContentPage
{
    private bool isFirstLoad = true;
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

    private async void estado_Toggled(object sender, ToggledEventArgs e)
    {
       
            var toggledSwitch = (Switch)sender;
            if (!e.Value)
            {
                var aceptarCambio = await DisplayAlert("Confirmación", "¿Estás seguro que quieres colocar el producto como agotado?", "Si", "No");
                if (aceptarCambio)
                {
                    var item = toggledSwitch.BindingContext as Producto;
                    if (item != null)
                    {
                        ProductoController productoController = new ProductoController();
                        int idProducto = item.idProducto;
                        try
                        {
                            // Llamada a la API para modificar el estado del producto a 2 (agotado)
                            ResProducto res = await productoController.modificarProducto(idProducto, item.subcategoriaProducto.idSubcategoriaProducto, item.receta.idReceta, item.nombreProducto, item.descripcion, item.urlImgen, (decimal)item.precio, 2);
                            if (res.Resultado)
                            {
                                await DisplayAlert("Actualización Exitosa", "Producto actualizado con éxito", "Aceptar");
                            }
                            else
                            {
                                await DisplayAlert("Error en actualización", "Sucedió un error al actualizar: " + res.ListaDeErrores.First(), "Aceptar");
                            }
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Error interno", "Por favor, reinstale la aplicación", "Aceptar");
                            // Manejo de excepciones generales
                        }
                    }
                }
                else
                {
                    // Si el usuario no acepta el cambio, revertir el estado del switch
                    toggledSwitch.Toggled -= estado_Toggled;
                    toggledSwitch.IsToggled = true;
                    toggledSwitch.Toggled += estado_Toggled;
                }
            }
    }
}