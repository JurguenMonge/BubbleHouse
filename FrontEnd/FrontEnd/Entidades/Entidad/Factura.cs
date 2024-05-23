using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Entidad
{
    public class Factura
    {
        public int idFactura { get; set; }
        public float numTotal { get; set; }
        public DateTime fecha { get; set; }
        public byte estado { get; set; }
        public List<ContenedorProductoFactura> productosList = new List<ContenedorProductoFactura>();

    }
}
