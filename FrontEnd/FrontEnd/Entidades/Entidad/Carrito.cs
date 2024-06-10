using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Entidad
{
    public static class Carrito
    {
        public static List<ContenedorProducto> listaContenedorProducto = new List<ContenedorProducto> ();

        static Carrito()
        {
            InicializarProductos();
        }

        public static void InicializarProductos()
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
                idProducto = 1,
                subcategoriaProducto = new SubcategoriaProducto(),
                categoriaProducto = new CategoriaProducto(),
                descripcion = "Descripción del producto",
                urlImgen = "http://example.com/image.png",
                estado = true,
                receta = new Receta()
            }) ;
        }
    }
}
