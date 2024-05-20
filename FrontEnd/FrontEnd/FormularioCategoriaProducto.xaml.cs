using FrontEnd.Controller;
using FrontEnd.Entidades.Entidad;
using FrontEnd.Entidades.Response;

namespace FrontEnd;

public partial class FormularioCategoriaProducto : ContentPage
{
	public FormularioCategoriaProducto()
	{
		InitializeComponent();
	}

    private async void btnIngresar_ClickedAsync(object sender, EventArgs e)
    {
        CategoriaProductoController controller = new CategoriaProductoController();
        try
        {
            ResCategoriaProducto res = new ResCategoriaProducto();
            if (int.Parse(txtId.Text) == 0)
            {
                res = await controller.IngresarCategoriaProducto(txtNombre.Text);
                if (res.Resultado)
                {
                    DisplayAlert("Insercion Exitosa", "Categoria de producto guardada con exito", "Aceptar");
                    Navigation.PushAsync(new ListadoCategoriaProducto());
                }
                else
                {
                    DisplayAlert("Error en insercion", "Sucedio un error al guardar: " + res.ListaDeErrores.First(), "Aceptar");
                }
            }
            else
            {
                res = await controller.ActualizarCategoriaProducto(txtNombre.Text, int.Parse(txtId.Text));
                if (res.Resultado)
                {
                    DisplayAlert("Actualizacion Exitosa", "Categoria de producto actualizada con exito", "Aceptar");
                    Navigation.PushAsync(new ListadoCategoriaProducto());
                }
                else
                {
                    DisplayAlert("Error en actualiacion", "Sucedio un error al actualizar: " + res.ListaDeErrores.First(), "Aceptar");
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
        CategoriaProductoController controller = new CategoriaProductoController();
        try
        {
            ResCategoriaProducto res = new ResCategoriaProducto();
            res = await controller.EliminarCategoriaProducto(int.Parse(txtId.Text));
            if (res.Resultado)
            {
                DisplayAlert("Eliminacion Exitosa", "Categoria de producto eliminada con exito", "Aceptar");
                Navigation.PushAsync(new ListadoCategoriaProducto());
            }
            else
            {
                DisplayAlert("Error en eliminacion", "Sucedio un error al eliminar: " + res.ListaDeErrores.First(), "Aceptar");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error interno", "Porfavor reinstale la aplicacion", "Aceptar");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CategoriaProducto categoria)
        {
            txtId.Text = categoria.idCategoriaProducto.ToString();
            txtNombre.Text = categoria.dscNombreCategoria.ToString();

            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Text != "0")
            {
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