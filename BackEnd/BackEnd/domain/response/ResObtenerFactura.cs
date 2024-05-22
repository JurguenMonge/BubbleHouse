using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain.response
{
    public class ResObtenerFactura : ResBase
    {
        public List<Factura> listaFacturas = new List<Factura>();
    }
}
