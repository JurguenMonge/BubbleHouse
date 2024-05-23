using FrontEnd.Controller;
using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Pages;

public partial class FormSubCategoriaProducto : ContentPage
{
    private readonly string Placeholder = "Seleccionar categoría";
    
    private List<CategoriaProducto> _listaDeCategoriasProducto = new List<CategoriaProducto>();
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

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private async void CargarPublicaciones()
    {
        listaDeCategoriasProducto = await CategoriasDesdeApi();
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
            CategoriaProducto cate = (CategoriaProducto)CategoryPicker.SelectedItem;

            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            if (int.Parse(txtId.Text) == 0)
            {
                res = await controller.IngresarSubCategoriaProducto(txtNombre.Text,cate.idCategoriaProducto);
                if (res.Resultado)
                {
                    DisplayAlert("Insercion Exitosa", "Categoria de producto guardada con exito", "Aceptar");
                    Navigation.PushAsync(new SubCategoriaProductoPage());
                }
                else
                {
                    DisplayAlert("Error en insercion", "Sucedio un error al guardar: " + res.ListaDeErrores.First(), "Aceptar");
                }
            }
            else
            {
                res = await controller.ActualizarSubCategoriaProducto(int.Parse(txtId.Text), cate.idCategoriaProducto, txtNombre.Text);
                if (res.Resultado)
                {
                    DisplayAlert("Actualiación Exitosa", "Subcategoría de producto actualizada con éxito", "Aceptar");
                    Navigation.PushAsync(new SubCategoriaProductoPage());
                }
                else
                {
                    DisplayAlert("Error en actualiación", "Sucedió un error al actualizar: " + res.ListaDeErrores.First(), "Aceptar");
                }
            }

        }
        catch (Exception ex)
        {
            DisplayAlert("Error interno", "Porfavor reinstale la aplicacion", "Aceptar");
        }
    }

    private async void btnEliminar_ClickedAsync(object sender, EventArgs e)
    {
        SubCategoriaProductoController controller = new SubCategoriaProductoController();
        try
        {
            ResSubCategoriaProducto res = new ResSubCategoriaProducto();
            res = await controller.EliminarSubCategoriaProducto(int.Parse(txtId.Text));
            if (res.Resultado)
            {
                DisplayAlert("Eliminación Exitosa", "Subcategoría de producto eliminada con éxito", "Aceptar");
                Navigation.PushAsync(new SubCategoriaProductoPage());
            }
            else
            {
                DisplayAlert("Error en eliminación", "Sucedió un error al eliminar: " + res.ListaDeErrores.First(), "Aceptar");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error interno", "Por favor reinstale la aplicación", "Aceptar");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SubcategoriaProducto subcategoria)
        {
            txtId.Text = subcategoria.idSubcategoriaProducto.ToString();
            txtNombre.Text = subcategoria.dscNombreSubCategoria.ToString();
            
            foreach(CategoriaProducto cate in listaDeCategoriasProducto)
            {
                if(cate.idCategoriaProducto == subcategoria.cateProductoId)
                {
                    CategoryPicker.SelectedItem = cate;
                    break;
                }
            }
            

            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Text != "0")
            {
                lblTitulo.Text = "Modificar Subcategoría de Producto";
                btnIngresar.Text = "Actualizar";
                btnEliminar.IsVisible = true;
            }
            else
            {
                btnIngresar.Text = "Ingresar";
                btnEliminar.IsVisible = false;
            }
        }
    }

}