using BackEnd.domain.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain
{
    public class Factura
    {
         public int idFactura {  get; set; }
         public int idSesion {  get; set; }
         public float numTotal {  get; set; }
         public DateTime fecha {  get; set; }
         public byte estado {  get; set; }
         public List<ContenedorProductoFactura> productosList { get; set; }

    }
}
