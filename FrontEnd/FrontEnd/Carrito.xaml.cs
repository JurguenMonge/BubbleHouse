using FrontEnd.Entidades;
using FrontEnd.Entidades.Entidad;
using Newtonsoft.Json;

namespace FrontEnd;

public partial class Carrito : ContentPage
{
    public Carrito()
    {
        InitializeComponent();
        CargarProductos();
    }
    public List<ContenedorProducto> ListaCarrito => Entidades.Entidad.Carrito.listaContenedorProducto;
    private List<Carrito> _listaCarrito = new List<Carrito>();

    #region refrezcarCompomentes
    public List<Carrito> listaCarrito
    {
        get { return _listaCarrito; }
        set
        {
            _listaCarrito = value;
            OnPropertyChanged(nameof(listaCarrito));
        }
    }
    #endregion

    private async void btnAceptarCompra_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Compra Realizada", "Gracias por su compra. Dentro de poco se efectuará el cobro.", "Aceptar");
    }

    private void btncambiosCompra_Clicked(object sender, EventArgs e)
    {

    }

    private void btnCancelarCompra_Clicked(object sender, EventArgs e)
    {

    }

    private void btnEliminarCompra_Clicked(object sender, EventArgs e)
    {

    }

    private async void CargarProductos()
    {
        Entidades.Entidad.Carrito.listaContenedorProducto.Add(new ContenedorProducto
        {
            IdRFacturaProducto = 1,
            idFactura = 12345,
            numSubtotal = 100,
            descuento = 10,
            numCantidad = 2,
            informacionReceta = "Instrucciones de preparación",
            nombreProducto = "Producto de ejemplo",
            precio = 50,
            subcategoriaProducto = new SubcategoriaProducto(),
            categoriaProducto = new CategoriaProducto(),
            descripcion = "Descripción del producto",
            urlImgen = "http://example.com/image.png",
            estado = true,
            receta = new Receta()
        });
        BindingContext = this;
    }


}