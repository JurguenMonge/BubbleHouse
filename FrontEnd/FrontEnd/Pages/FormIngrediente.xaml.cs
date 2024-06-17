using Firebase.Storage;
using FrontEnd.Controller;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Pages;

public partial class FormIngrediente : ContentPage
{

    private List<CategoriaIngrediente> _listaDeCategoriaIngrediente = new List<CategoriaIngrediente>();

    private CategoriaIngrediente selectedCategoriaIngrediente;
    public int cateIngredienteId;
    private bool isPickerOpen = false;
    string path = "D:\\JB\\Documents\\BubbleHouse\\FrontEnd\\FrontEnd\\Resources\\Images\\";
    public string selectedImagePath;
    private string urlImage {  get; set; }

    public FormIngrediente()
	{
		InitializeComponent();
        CargarCategoriasIngrediente();

    }


    #region refrezcarCompomentes
    public List<CategoriaIngrediente> listaDeCategoriaIngrediente
    {
        get { return _listaDeCategoriaIngrediente; }
        set
        {
            _listaDeCategoriaIngrediente = value;
            OnPropertyChanged(nameof(listaDeCategoriaIngrediente));
        }
    }

    public CategoriaIngrediente SelectedCategoriaIngrediente
    {
        get => selectedCategoriaIngrediente;
        set
        {
            if (selectedCategoriaIngrediente != value)
            {
                selectedCategoriaIngrediente = value;
                OnPropertyChanged(nameof(SelectedCategoriaIngrediente));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion


    private async void CargarCategoriasIngrediente()
    {
        listaDeCategoriaIngrediente = await CategoriasIngredienteDesdeApi();

        // Agregar el elemento de placeholder al principio de la lista
        listaDeCategoriaIngrediente.Insert(0, new CategoriaIngrediente { idCateIngrediente = -1, dscNombreCategoria = "Seleccionar una categoría" });

        pickCategoria.ItemsSource = listaDeCategoriaIngrediente;
        pickCategoria.ItemDisplayBinding = new Binding("dscNombreCategoria");

        
        if (cateIngredienteId != 0)
        {
            SetSelectedCategoriaById(cateIngredienteId);
        }
        else
        {
            SetSelectedCategoriaById(-1);
        }
        BindingContext = this;
    }

    private async Task<List<CategoriaIngrediente>> CategoriasIngredienteDesdeApi()
    {
        List<CategoriaIngrediente> retornarCategoriasIngredienteApi = new List<CategoriaIngrediente>();
        String laURL = "https://apibubblehouse.azurewebsites.net/api/categoriaIngrediente/obtener";

        try
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laURL);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerCategoriaIngrediente res = JsonConvert.DeserializeObject<ResObtenerCategoriaIngrediente>(responseContent);

                    if (res.Resultado)
                    {
                        retornarCategoriasIngredienteApi = res.listaCategoriaIngrediente;
                        Console.WriteLine(retornarCategoriasIngredienteApi);
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

        return retornarCategoriasIngredienteApi;
    }

    private void pickCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        int selectedIndex = pickCategoria.SelectedIndex;

        
        if (isPickerOpen && selectedIndex == 0)
        {
            pickCategoria.SelectedItem = false;
        }
    }

    private void pickCategoria_Focused(object sender, FocusEventArgs e)
    {
        isPickerOpen = true;
    }

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {
        IngredienteController controller = new IngredienteController();
        try
        {
            CategoriaIngrediente cate = (CategoriaIngrediente)pickCategoria.SelectedItem;
            if (cate != null)
            {
                
                ResIngrediente res = new ResIngrediente();
                if (int.Parse(txtId.Text) == 0)
                {
                    res = await controller.IngresarIngrediente(cate.idCateIngrediente, txtNombreIngrediente.Text, txtDescripcion.Text, urlImage, decimal.Parse(txtPrecio.Text));
                    if (res.Resultado)
                    {
                        await DisplayAlert("Insercion Exitosa", "Ingrediente guardado con éxito", "Aceptar");
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

                    res = await controller.ActualizarIngrediente(int.Parse(txtId.Text), cate.idCateIngrediente, txtNombreIngrediente.Text, txtDescripcion.Text, urlImage, decimal.Parse(txtPrecio.Text));
                    if (res.Resultado)
                    {
                        await DisplayAlert("Actualiación Exitosa", "Ingrediente actualizado con éxito", "Aceptar");
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
                await DisplayAlert("Error!", "Debe Seleccionar una categoría de ingrediente", "Aceptar");
            }


        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Por favor, reinstale la aplicacion", "Aceptar");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de eliminar este ingrediente?", "Aceptar", "Cancelar");
        if (answer)
        {
            IngredienteController controller = new IngredienteController();
            try
            {
                ResIngrediente res = new ResIngrediente();
                res = await controller.EliminarIngrediente(int.Parse(txtId.Text));
                if (res.Resultado)
                {
                    await DisplayAlert("Eliminación Exitosa", "Ingrediente eliminado con éxito", "Aceptar");
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

    private void SetSelectedCategoriaById(int cateIngredienteId)
    {
        foreach (var cate in listaDeCategoriaIngrediente)
        {
            if (cate.idCateIngrediente == cateIngredienteId)
            {
                pickCategoria.SelectedItem = cate;
                break;
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is Ingrediente ingrediente)
        {
            txtId.Text = ingrediente.idIngrediente.ToString();
            txtNombreIngrediente.Text = ingrediente.dscNombre.ToString();
            txtDescripcion.Text = ingrediente.dscDescripcion;
            txtPrecio.Text = ingrediente.numPrecio.ToString();
            selectedImage.Source = ingrediente.dscURLImagen;
            
            cateIngredienteId = ingrediente.idCategoriaIngrediente;

            
            SetSelectedCategoriaById(cateIngredienteId);
            

            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Text != "0")
            {

                lblTitulo.Text = "Modificar Ingrediente";
                btnIngresar.Text = "Modificar";
                btnEliminar.IsVisible = true;
            }
            else
            {
                btnIngresar.Text = "Ingresar";
                btnEliminar.IsVisible = false;
            }
        }
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        
        if (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(path+selectedImagePath))
        {
            File.Delete(path+selectedImagePath);
        }
        Navigation.PopAsync();
    }

    private async void btnSeleccionar_Clicked(object sender, EventArgs e)
    {

        var foto = await MediaPicker.PickPhotoAsync();

        if (foto != null) {
            var stream = await foto.OpenReadAsync();
            urlImage = await new FirebaseStorage("bubblehouse-30c28.appspot.com")
                                    .Child("Fotos")
                                    .Child(foto.FileName)
                                    .PutAsync(stream);
            selectedImage.Source = urlImage;
        }

        //try
        //{
        //    var result = await FilePicker.PickAsync(new PickOptions
        //    {
        //        FileTypes = FilePickerFileType.Images,
        //        PickerTitle = "Seleccione una imagen"
        //    });

        //    if (result != null)
        //    {
                
        //        var stream = await result.OpenReadAsync();

                
        //        var fileName = Path.GetFileName(result.FullPath);

                
        //        var imagePath = Path.Combine(path, fileName);

                
        //        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

                
        //        using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
        //        {
        //            await stream.CopyToAsync(fileStream);
        //        }

                
        //        selectedImage.Source = ImageSource.FromStream(() => new FileStream(imagePath, FileMode.Open, FileAccess.Read));

                
        //        selectedImagePath = fileName;

        //    }
        //}
        //catch (Exception ex)
        //{
        //    await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        //}
    }

}