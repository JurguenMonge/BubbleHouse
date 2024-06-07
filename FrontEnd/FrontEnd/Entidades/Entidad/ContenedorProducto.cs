using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Entidad
{
    public class ContenedorProducto : Producto
    {
        public int IdRFacturaProducto { get; set; }
        public int idFactura { get; set; }
        public decimal numSubtotal { get; set; }
        public decimal descuento { get; set; }
        public int numCantidad { get; set; }
        public String informacionReceta { get; set; }
        public string InformacionCompleta => $"{informacionReceta} - {nombreProducto}";
    }
}
