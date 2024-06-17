
using Firebase.Storage;
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
    private Producto selectedProducto;
    public int subCategoriaProductoId;
    public int recetaId;
    public int estado;
    private bool isPickerOpen = false;
    string path = "C:\\Users\\Jurguen Monge\\Documents\\GitHub\\BubbleHouse\\FrontEnd\\FrontEnd\\Resources\\Images";
    public string selectedImagePath;
    private string urlImage { get; set; }

    public FormProducto()
    {
        InitializeComponent();
        CargarSubCategoriaProducto();
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


    private async Task<List<SubcategoriaProducto>> SubCategoriaProductoDesdeApi()
    {
        List<SubcategoriaProducto> subcategoriaProductos = new List<SubcategoriaProducto>();
        String laURL = "https://apibubblehouse.azurewebsites.net/api/subCategoriaProducto/obtener";

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

    private void SetSelectedEstadoById(int estado)
    {
        if (estado == 1)
        {
            pickEstado.SelectedIndex = 0; // "Disponible"
        }
        else if (estado == 2)
        {
            pickEstado.SelectedIndex = 1; // "Agotado"
        }
    }

    private void pickSubCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void pickSubCategoria_Focused(object sender, FocusEventArgs e)
    {

    }

    private void pickReceta_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void pickReceta_Focused(object sender, FocusEventArgs e)
    {

    }

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {
        ProductoController productoController = new ProductoController();
        try
        {
            SubcategoriaProducto subCate = (SubcategoriaProducto)pickSubCategoria.SelectedItem; 
            if (subCate != null)
            {

                ResProducto res = new ResProducto();
                if (int.Parse(txtId.Text) == 0)
                {
                    res = await productoController.IngresarProducto(subCate.idSubcategoriaProducto, 1, txtNombreProducto.Text, txtDescripcion.Text, selectedImagePath, decimal.Parse(txtPrecio.Text));
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
                    if (pickEstado.SelectedIndex == 0)
                    {
                        res = await productoController.modificarProducto(int.Parse(txtId.Text), subCate.idSubcategoriaProducto, 1, txtNombreProducto.Text, txtDescripcion.Text, selectedImagePath, decimal.Parse(txtPrecio.Text), 1);
                    }
                    else
                    {
                        res = await productoController.modificarProducto(int.Parse(txtId.Text), subCate.idSubcategoriaProducto, 1, txtNombreProducto.Text, txtDescripcion.Text, selectedImagePath, decimal.Parse(txtPrecio.Text), 2);
                    }

                   
                    if (res.Resultado)
                    {
                        await DisplayAlert("Actualiación Exitosa", "Producto actualizado con éxito", "Aceptar");
                        await Navigation.PopAsync();

                    }
                    else
                    {
                        await DisplayAlert("Error en actualiación", "Sucedió un error al actualizar: " + res.ListaDeErrores.First(), "Aceptar");
                    }
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

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de eliminar este producto?", "Aceptar", "Cancelar");
        if (answer)
        {
            ProductoController controller = new ProductoController();
            try
            {
                ResProducto res = new ResProducto();
                res = await controller.EliminarProducto(int.Parse(txtId.Text));
                if (res.Resultado)
                {
                    await DisplayAlert("Eliminación Exitosa", "Producto eliminado con éxito", "Aceptar");
                    await Navigation.PopAsync();
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
    private async void btnSeleccionar_Clicked(object sender, EventArgs e)
    {
        var foto = await MediaPicker.PickPhotoAsync();

        if (foto != null)
        {
            var stream = await foto.OpenReadAsync();
            urlImage = await new FirebaseStorage("bubblehouse-30c28.appspot.com")
                                    .Child("Fotos")
                                    .Child(foto.FileName)
                                    .PutAsync(stream);
            selectedImage.Source = urlImage;
        }
        /*try
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
        }*/
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(path + selectedImagePath))
        {
            File.Delete(path + selectedImagePath);
        }
        Navigation.PopAsync();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is Producto producto)
        {
            txtId.Text = producto.idProducto.ToString();
            txtNombreProducto.Text = producto.nombreProducto.ToString();
            txtDescripcion.Text = producto.descripcion;
            txtPrecio.Text = producto.precio.ToString();
            selectedImage.Source = producto.urlImgen;
            recetaId = producto.receta.idReceta;
            subCategoriaProductoId = producto.subcategoriaProducto.idSubcategoriaProducto;
            estado = producto.estado;
            SetSelectedEstadoById(estado);
            SetSelectedSubCategoriaById(subCategoriaProductoId);


            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Text != "0")
            {

                lblTitulo.Text = "Modificar Producto";
                btnIngresar.Text = "Modificar";
                btnEliminar.IsVisible = true;
                pickEstado.IsVisible = true;
            }
            else
            {
                pickEstado.IsVisible = false;
                btnIngresar.Text = "Ingresar";
                btnEliminar.IsVisible = false;
            }
        }
    }

    private void pickEstado_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void pickEstado_Focused(object sender, FocusEventArgs e)
    {

    }
}