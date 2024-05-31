namespace FrontEnd.Pages;

public partial class FormProducto : ContentPage
{
    public int cateIngredienteId;
    private bool isPickerOpen = false;
    string path = "C:\\Users\\Jurguen Monge\\Documents\\GitHub\\BubbleHouse\\FrontEnd\\FrontEnd\\Resources\\Images";
    public string selectedImagePath;
    public FormProducto()
    {
        InitializeComponent();
    }

    private void pickCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void pickCategoria_Focused(object sender, FocusEventArgs e)
    {

    }

    private void btnIngresar_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de eliminar este Producto?", "Aceptar", "Cancelar");
        if (answer)
        {
            
            try
            {
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error interno", "Por favor, reinstale la aplicación", "Aceptar");
            }
        }
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
}