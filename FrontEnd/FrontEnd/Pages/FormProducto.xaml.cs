using FrontEnd.Controller;
using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Pages;

public partial class FormProducto : ContentPage
{
    private List<SubcategoriaProducto> _listaDeSubCategoriaProducto = new List<SubcategoriaProducto>();
    private List<RecetaCompleta> _listaDeReceta = new List<RecetaCompleta>();
    private Producto selectedProducto;
    public int subCategoriaProductoId;
    public int recetaId;
    private bool isPickerOpen = false;
    string path = "C:\\Users\\Jurguen Monge\\Documents\\GitHub\\BubbleHouse\\FrontEnd\\FrontEnd\\Resources\\Images";
    public string selectedImagePath;

    public FormProducto()
    {
        InitializeComponent();
        CargarSubCategoriaProducto();
        CargarRecetas();
    }

    #region refrezcarCompomentes
    public List<SubcategoriaProducto> listaDeSubCategoriaProducto
    {
        get { return _listaDeSubCategoriaProducto; }
        set
        {
            _listaDeSubCategoriaProducto = value;
            OnPropertyChanged(nameof(listaDeSubCategoriaProducto));
        }
    }

    public List<RecetaCompleta> listaDeReceta
    {
        get { return _listaDeReceta; }
        set
        {
            _listaDeReceta = value;
            OnPropertyChanged(nameof(listaDeReceta));
        }
    }

    public Producto SelectedProducto
    {
        get => selectedProducto;
        set
        {
            if (selectedProducto != value)
            {
                selectedProducto = value;
                OnPropertyChanged(nameof(SelectedProducto));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private async void CargarSubCategoriaProducto()
    {
        listaDeSubCategoriaProducto = await SubCategoriaProductoDesdeApi();

        // Agregar el elemento de placeholder al principio de la lista
        listaDeSubCategoriaProducto.Insert(0, new SubcategoriaProducto { idSubcategoriaProducto = -1, dscNombreSubCategoria = "Seleccionar una SubCategoría" });

        pickSubCategoria.ItemsSource = listaDeSubCategoriaProducto;
        pickSubCategoria.ItemDisplayBinding = new Binding("dscNombreSubCategoria");


        if (subCategoriaProductoId != 0)
        {
            SetSelectedSubCategoriaById(subCategoriaProductoId);
        }
        else
        {
            SetSelectedSubCategoriaById(-1);
        }
        BindingContext = this;
    }

    private async void CargarRecetas()
    {
        listaDeReceta = await RecetasDesdeApi();

        // Agregar el elemento de placeholder al principio de la lista
        listaDeReceta.Insert(0, new RecetaCompleta { recetaId = -1, nombreReceta = "Seleccionar una Receta" });

        pickReceta.ItemsSource = listaDeReceta;
        pickReceta.ItemDisplayBinding = new Binding("nombreReceta");


        if (recetaId != 0)
        {
            SetSelectedRecetaId(recetaId);
        }
        else
        {
            SetSelectedRecetaId(-1);
        }
        BindingContext = this;
    }

    private async Task<List<SubcategoriaProducto>> SubCategoriaProductoDesdeApi()
    {
        List<SubcategoriaProducto> subcategoriaProductos = new List<SubcategoriaProducto>();
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
                        subcategoriaProductos = res.listaSubCategoriaProducto;
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

        return subcategoriaProductos;
    }

    private void SetSelectedSubCategoriaById(int subCategoriaProductoId)
    {
        foreach (var subCate in listaDeSubCategoriaProducto)
        {
            if (subCate.idSubcategoriaProducto == subCategoriaProductoId)
            {
                pickSubCategoria.SelectedItem = subCate;
                break;
            }
        }
    }

    private async Task<List<RecetaCompleta>> RecetasDesdeApi()
    {
        List<RecetaCompleta> recetasDesdeApi = new List<RecetaCompleta>();
        String laURL = "https://localhost:44311/api/receta/obtener";

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
                        recetasDesdeApi = res.listaRecetas;
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

        return recetasDesdeApi;
    }

    private void SetSelectedRecetaId(int recetaId)
    {
        foreach (var receta in listaDeReceta)
        {
            if (receta.recetaId == recetaId)
            {
                pickReceta.SelectedItem = receta;
                break;
            }
        }
    }

    private void pickCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void pickCategoria_Focused(object sender, FocusEventArgs e)
    {

    }

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {
        ProductoController productoController = new ProductoController();
        try
        {
            SubcategoriaProducto subCate = (SubcategoriaProducto)pickSubCategoria.SelectedItem;
            RecetaCompleta rece = (RecetaCompleta)pickReceta.SelectedItem;
            if (subCate != null && rece != null)
            {

                ResProducto res = new ResProducto();
                if (int.Parse(txtId.Text) == 0)
                {
                    res = await productoController.IngresarProducto(subCate.idSubcategoriaProducto, rece.recetaId, txtNombreProducto.Text, txtDescripcion.Text, selectedImagePath, decimal.Parse(txtPrecio.Text));
                    if (res.Resultado)
                    {
                        await DisplayAlert("Insercion Exitosa", "Producto guardado con éxito", "Aceptar");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error en insercion", "Sucedio un error al guardar: " + res.ListaDeErrores.First(), "Aceptar");
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(selectedImagePath))
                    {
                        var imageSource = selectedImage.Source;


                        if (imageSource is FileImageSource fileImageSource)
                        {
                            selectedImagePath = fileImageSource.File;
                        }

                    }

                    //res = await productoController.ActualizarIngrediente(int.Parse(txtId.Text), cate.idCateIngrediente, txtNombreIngrediente.Text, txtDescripcion.Text, selectedImagePath, decimal.Parse(txtPrecio.Text));
                    //if (res.Resultado)
                    //{
                    //    await DisplayAlert("Actualiación Exitosa", "Ingrediente actualizado con éxito", "Aceptar");
                    //    await Navigation.PopAsync();

                    //}
                    //else
                    //{
                    //    await DisplayAlert("Error en actualiación", "Sucedió un error al actualizar: " + res.ListaDeErrores.First(), "Aceptar");
                    //}
                }
            }
            else
            {
                await DisplayAlert("Error!", "Debe Seleccionar una subcategoria de producto y una receta", "Aceptar");
            }


        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Por favor, reinstale la aplicacion", "Aceptar");
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnSeleccionar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Seleccione una imagen"
            });

            if (result != null)
            {

                var stream = await result.OpenReadAsync();


                var fileName = Path.GetFileName(result.FullPath);


                var imagePath = Path.Combine(path, fileName);


                Directory.CreateDirectory(Path.GetDirectoryName(imagePath));


                using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    await stream.CopyToAsync(fileStream);
                }


                selectedImage.Source = ImageSource.FromStream(() => new FileStream(imagePath, FileMode.Open, FileAccess.Read));


                selectedImagePath = fileName;

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}