using FrontEnd.Entidades;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Pages;

public partial class FormUsuario : ContentPage
{

    private List<Rol> _listaDeRoles = new List<Rol>();

    private Rol selectedRol;
    public int RolId;
    private bool isPickerOpen = false;

    public FormUsuario()
	{
		InitializeComponent();
        CargarRoles();

    }

    #region refrezcarCompomentes
    public List<Rol> listaDeRoles
    {
        get { return _listaDeRoles; }
        set
        {
            _listaDeRoles = value;
            OnPropertyChanged(nameof(listaDeRoles));
        }
    }

    public Rol SelectedRol
    {
        get => selectedRol;
        set
        {
            if (selectedRol != value)
            {
                selectedRol = value;
                OnPropertyChanged(nameof(SelectedRol));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private async void CargarRoles()
    {
        listaDeRoles = await RolesDesdeApi();

        // Agregar el elemento de placeholder al principio de la lista
        listaDeRoles.Insert(0, new Rol { idRol = -1, tipoRol = "Seleccione un rol" });

        pickRol.ItemsSource = listaDeRoles;
        pickRol.ItemDisplayBinding = new Binding("tipoRol");

        // Si hay un cateProductoId seleccionado, configura el Picker
        if (RolId != 0)
        {
            SetSelectedRolById(RolId);
        }
        else
        {
            SetSelectedRolById(-1);
        }
        BindingContext = this;
    }

    private async Task<List<Rol>> RolesDesdeApi()
    {
        List<Rol> retornarRolApi = new List<Rol>();
        String laURL = "https://localhost:44311/api/rol/obtener";

        try
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laURL);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerRol res = JsonConvert.DeserializeObject<ResObtenerRol>(responseContent);

                    if (res.Resultado)
                    {
                        retornarRolApi = res.listaRoles;
                        Console.WriteLine(retornarRolApi);
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

        return retornarRolApi;
    }

    private void btnIngresar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void SetSelectedRolById(int rolId)
    {
        foreach (var cate in listaDeRoles)
        {
            if (cate.idRol == rolId)
            {
                pickRol.SelectedItem = cate;
                break;
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is Usuario usuario)
        {
            txtId.Text = usuario.IdUsuario.ToString();
            txtNombre.Text = usuario.Nombre;
            txtPrimerApellido.Text = usuario.PrimerApellido;
            txtSegundoApellido.Text = usuario.SegundoApellido;
            txtCorreo.Text = usuario.CorreoElectronico;
            txtTelefono.Text = usuario.NumeroTelefono;

            RolId = usuario.rol.idRol;


            SetSelectedRolById(RolId);


            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Text != "0")
            {
                lblPassword.IsVisible = false;
                txtPassword.IsVisible = false;
                lblRepeatPassword.IsVisible = false;
                txtRepeatPassword.IsVisible = false;
                lblTitulo.Text = "Modificar Usuario";
                btnIngresar.Text = "Modificar";
                btnEliminar.IsVisible = true;
            }
            else
            {
                btnIngresar.Text = "Crear Usuario";
                btnEliminar.IsVisible = false;
            }
        }
    }

    private void pickRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Obtener el índice seleccionado
        int selectedIndex = pickRol.SelectedIndex;

        // Si se seleccionó el primer elemento (placeholder), no hacer nada
        if (isPickerOpen && selectedIndex == 0)
        {
            pickRol.SelectedItem = false;
        }
    }

    private void pickRol_Focused(object sender, FocusEventArgs e)
    {
        isPickerOpen = true;
    }
}