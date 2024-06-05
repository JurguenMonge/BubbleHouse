using FrontEnd.Controller;
using FrontEnd.Entidades.Response;

namespace FrontEnd.Pages;

public partial class UsuarioAdminPage : ContentPage
{
    private bool isPasswordVisible = false;
    private bool isNewPasswordVisible = false;
    private bool isRepeatPasswordVisible = false;
    public UsuarioAdminPage()
	{
		InitializeComponent();
	}

    private async void btnModificar_Clicked(object sender, EventArgs e)
    {
        UsuarioController controller = new UsuarioController();
        try
        {
                ResUsuario res = new ResUsuario();
                if (txtNewPassword.Text.Equals(txtRepeatPassword.Text))
                {
                    res = await controller.ActualizarUsuario(int.Parse(txtId.Text), txtNombre.Text, txtPrimerApellido.Text, txtSegundoApellido.Text, txtCorreo.Text, txtNewPassword.Text, txtPassword.Text, txtTelefono.Text);
                    if (res.Resultado)
                    {
                        await DisplayAlert("Actualiación Exitosa", "Usuario actualizado con éxito", "Aceptar");
                        txtPassword.Text = "";
                        txtNewPassword.Text = "";
                        txtRepeatPassword.Text = "";
                    }
                    else
                    {
                        await DisplayAlert("Error en actualiación", "Sucedió un error al actualizar: " + res.ListaDeErrores.First(), "Aceptar");
                    }
                }
                else
                {
                    await DisplayAlert("Error!", "Las contraseñas no coinciden", "Aceptar");
                }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Por favor, reinstale la aplicación", "Aceptar");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        txtId.Text = Preferences.Get("UsuarioId",0).ToString();
        txtNombre.Text = Preferences.Get("UsuarioNombre", string.Empty);
        txtPrimerApellido.Text = Preferences.Get("UsuarioPrimerApellido", string.Empty);
        txtSegundoApellido.Text = Preferences.Get("UsuarioSegundoApellido", string.Empty);
        txtCorreo.Text = Preferences.Get("UsuarioCorreo", string.Empty);
        txtTelefono.Text = Preferences.Get("UsuarioTelefono", string.Empty);
    }

    private void TogglePasswordVisibility(object sender, EventArgs e)
    {
        isPasswordVisible = !isPasswordVisible;
        txtPassword.IsPassword = !isPasswordVisible;
        ((ImageButton)sender).Source = isPasswordVisible ? "hide.png" : "show.png";
    }

    private void ToggleNewPasswordVisibility(object sender, EventArgs e)
    {
        isNewPasswordVisible = !isNewPasswordVisible;
        txtNewPassword.IsPassword = !isNewPasswordVisible;
        ((ImageButton)sender).Source = isNewPasswordVisible ? "hide.png" : "show.png";
    }

    private void ToggleRepeatPasswordVisibility(object sender, EventArgs e)
    {
        isRepeatPasswordVisible = !isRepeatPasswordVisible;
        txtRepeatPassword.IsPassword = !isRepeatPasswordVisible;
        ((ImageButton)sender).Source = isRepeatPasswordVisible ? "hide.png" : "show.png";
    }
}