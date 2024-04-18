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
         public int idProducto { get; set; }
         public int idSesion {  get; set; }
         public float numSubTotal { get; set; }
         public float numDescuento { get; set; }
         public float numTotal {  get; set; }
         public DateTime fecha {  get; set; }
         public bool estado {  get; set; }

    }
}
