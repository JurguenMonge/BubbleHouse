using FrontEnd.Controller;
using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Pages;

public partial class FormSubCategoriaProducto : ContentPage
{
        
    private List<CategoriaProducto> _listaDeCategoriasProducto = new List<CategoriaProducto>();

    private CategoriaProducto selectedCategoriaProducto;
    public int cateProductoId;
    private bool isPickerOpen = false;

    public FormSubCategoriaProducto()
	{
		InitializeComponent();
        CargarPublicaciones();
        
    }

    #region refrezcarCompomentes
    public List<CategoriaProducto> listaDeCategoriasProducto
    {
        get { return _listaDeCategoriasProducto; }
        set
        {
            _listaDeCategoriasProducto = value;
            OnPropertyChanged(nameof(listaDeCategoriasProducto));
        }
    }

    public CategoriaProducto SelectedCategoriaProducto
    {
        get => selectedCategoriaProducto;
        set
        {
            if (selectedCategoriaProducto != value)
            {
                selectedCategoriaProducto = value;
                OnPropertyChanged(nameof(SelectedCategoriaProducto));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private async void CargarPublicaciones()
    {
        listaDeCategoriasProducto = await CategoriasDesdeApi();

        // Agregar el elemento de placeholder al principio de la lista
        listaDeCategoriasProducto.Insert(0, new CategoriaProducto { idCategoriaProducto = -1, dscNombreCategoria = "Seleccionar una subcategoría" });

        pickCategoria.ItemsSource = listaDeCategoriasProducto;
        pickCategoria.ItemDisplayBinding = new Binding("dscNombreCategoria");

        // Si hay un cateProductoId seleccionado, configura el Picker
        if (cateProductoId != 0)
        {
            SetSelectedCategoriaById(cateProductoId);
        }
        else
        {
            SetSelectedCategoriaById(-1);
        }
        BindingContext = this;
    }

    private async Task<List<CategoriaProducto>> CategoriasDesdeApi()
    {
        List<CategoriaProducto> retornarPublicacionApi = new List<CategoriaProducto>();
        String laURL = "https://localhost:44311/api/categoriaProducto/obtener";

        try
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laURL);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerCategoriaProducto res = JsonConvert.DeserializeObject<ResObtenerCategoriaProducto>(responseContent);

                    if (res.Resultado)
                    {
                        retornarPublicacionApi = res.listaCategoriaProducto;
                        Console.WriteLine(retornarPublicacionApi);
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

        return retornarPublicacionApi;
    }

    private async void btnIngresar_ClickedAsync(object sender, EventArgs e)
    {
        SubCategoriaProductoController controller = new SubCategoriaProductoController();
        try
        {
            CategoriaProducto cate = (CategoriaProducto)pickCategoria.SelectedItem;
            if(cate != null)
            {
                ResSubCategoriaProducto res = new ResSubCategoriaProducto();
                if (int.Parse(txtId.Text) == 0)
                {
                    res = await controller.IngresarSubCategoriaProducto(txtNombre.Text, cate.idCategoriaProducto);
                    if (res.Resultado)
                    {
                        await DisplayAlert("Insercion Exitosa", "Subcategoría de producto guardada con éxito", "Aceptar");
                        Navigation.PushAsync(new SubCategoriaProductoPage());
                    }
                    else
                    {
                        await DisplayAlert("Error en inserción", "Sucedió un error al guardar: " + res.ListaDeErrores.First(), "Aceptar");
                    }
                }
                else
                {
                    res = await controller.ActualizarSubCategoriaProducto(int.Parse(txtId.Text), cate.idCategoriaProducto, txtNombre.Text);
                    if (res.Resultado)
                    {
                        await DisplayAlert("Actualiación Exitosa", "Subcategoría de producto actualizada con éxito", "Aceptar");
                        Navigation.PushAsync(new SubCategoriaProductoPage());
                    }
                    else
                    {
                        await DisplayAlert("Error en actualiación", "Sucedió un error al actualizar: " + res.ListaDeErrores.First(), "Aceptar");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error!", "Debe Seleccionar una categoría de producto", "Aceptar");
            }
            

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Por favor, reinstale la aplicación", "Aceptar");
        }
    }

    private async void btnEliminar_ClickedAsync(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de eliminar esta subcategoría?", "Aceptar", "Cancelar");
        if (answer)
        {
            SubCategoriaProductoController controller = new SubCategoriaProductoController();
            try
            {
                ResSubCategoriaProducto res = new ResSubCategoriaProducto();
                res = await controller.EliminarSubCategoriaProducto(int.Parse(txtId.Text));
                if (res.Resultado)
                {
                    await DisplayAlert("Eliminación Exitosa", "Subcategoría de producto eliminada con éxito", "Aceptar");
                    Navigation.PushAsync(new SubCategoriaProductoPage());
                }
                else
                {
                    await DisplayAlert("Error en eliminación", "Sucedió un error al eliminar: " + res.ListaDeErrores.First(), "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error interno", "Por favor reinstale la aplicación", "Aceptar");
            }
        }
    }

    private void SetSelectedCategoriaById(int cateProductoId)
    {
        foreach (var cate in listaDeCategoriasProducto)
        {
            if (cate.idCategoriaProducto == cateProductoId)
            {
                pickCategoria.SelectedItem = cate;
                break;
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SubcategoriaProducto subcategoria)
        {
            txtId.Text = subcategoria.idSubcategoriaProducto.ToString();
            txtNombre.Text = subcategoria.dscNombreSubCategoria.ToString();

            // Guardar el cateProductoId para usarlo después de cargar las categorías
            cateProductoId = subcategoria.cateProductoId;

            if (listaDeCategoriasProducto.Any())
            {
                SetSelectedCategoriaById(cateProductoId);
            }

            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Text != "0")
            {
                
                lblTitulo.Text = "Modificar Subcategoría de Producto";
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

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}