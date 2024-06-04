using FrontEnd.Entidades;
using FrontEnd.Entidades.Response;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FrontEnd.Pages;

public partial class UsuariosSuperAdminPage : ContentPage
{
	public UsuariosSuperAdminPage()
	{
		InitializeComponent();
        CargarUsuarios();

    }

    private ObservableCollection<Usuario> _listaUsuario = new ObservableCollection<Usuario>();

    #region refrezcarCompomentes
    public ObservableCollection<Usuario> listaUsuario
    {
        get { return _listaUsuario; }
        set
        {
            _listaUsuario = value;
            OnPropertyChanged(nameof(listaUsuario));
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion


    private async void CargarUsuarios()
    {
        
        listaUsuario.Clear();
        var usuarios = await UsuariosDesdeApi();
        foreach (var usuario in usuarios)
        {
            listaUsuario.Add(usuario);
        }
        BindingContext = this;
    }

    private async Task<List<Usuario>> UsuariosDesdeApi()
    {
        List<Usuario> retornarUsuariosApi = new List<Usuario>();
        String laURL = "https://localhost:44311/api/usuario/obtener";

        try
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laURL);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerUsuario res = JsonConvert.DeserializeObject<ResObtenerUsuario>(responseContent);

                    if (res.Resultado)
                    {
                        retornarUsuariosApi = res.listaUsuarios;
                        Console.WriteLine(retornarUsuariosApi);
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

        return retornarUsuariosApi;
    }

    private void TapCrearUsuario(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new FormUsuario());
    }

    private void TapModificarUsuario(object sender, TappedEventArgs e)
    {
        var button = sender as Frame;
        var item = button?.BindingContext as Usuario;

        if (item != null)
        {
            var formularioUsuario = new FormUsuario();
            formularioUsuario.BindingContext = item;
            Navigation.PushAsync(formularioUsuario);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarUsuarios();
    }
}