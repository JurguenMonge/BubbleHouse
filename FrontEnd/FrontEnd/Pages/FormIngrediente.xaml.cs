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
    public string selectedImagePath;

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
        String laURL = "https://localhost:44311/api/categoriaIngrediente/obtener";

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
        // Obtener el índice seleccionado
        int selectedIndex = pickCategoria.SelectedIndex;

        // Si se seleccionó el primer elemento (placeholder), no hacer nada
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
                    res = await controller.IngresarIngrediente(cate.idCateIngrediente, txtNombreIngrediente.Text, txtDescripcion.Text, selectedImagePath, decimal.Parse(txtPrecio.Text));
                    if (res.Resultado)
                    {
                        await DisplayAlert("Insercion Exitosa", "Ingrediente guardado con éxito", "Aceptar");
                        Navigation.PushAsync(new IngredientePage());
                    }
                    else
                    {
                        await DisplayAlert("Error en insercion", "Sucedio un error al guardar: " + res.ListaDeErrores.First(), "Aceptar");
                    }
                }
                else
                {
                    res = await controller.ActualizarIngrediente(int.Parse(txtId.Text), cate.idCateIngrediente, txtNombreIngrediente.Text, txtDescripcion.Text, selectedImagePath, decimal.Parse(txtPrecio.Text));
                    if (res.Resultado)
                    {
                        await DisplayAlert("Actualiación Exitosa", "Ingrediente actualizado con éxito", "Aceptar");
                        Navigation.PushAsync(new IngredientePage());
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
                    Navigation.PushAsync(new IngredientePage());
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
        Navigation.PopAsync();
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
                // Obtener el stream de la imagen seleccionada
                var stream = await result.OpenReadAsync();

                // Generar un nombre único para la imagen
                var fileName = Path.GetFileName(result.FullPath);
                //var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";

                // Crear la ruta completa para guardar la imagen en la carpeta Resources/Images
                var imagePath = Path.Combine("D:\\JB\\Documents\\BubbleHouse\\FrontEnd\\FrontEnd\\Resources\\Images\\", fileName);

                // Asegurarse de que el directorio exista
                Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

                // Copiar la imagen al directorio del proyecto
                using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    await stream.CopyToAsync(fileStream);
                }

                // Actualizar la imagen seleccionada en la UI
                selectedImage.Source = ImageSource.FromStream(() => new FileStream(imagePath, FileMode.Open, FileAccess.Read));

                // Guardar la ruta relativa de la imagen para usarla más adelante
                selectedImagePath = fileName;

            }
        }
        catch (Exception ex)
        {
            // Manejar excepciones si es necesario
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }

}