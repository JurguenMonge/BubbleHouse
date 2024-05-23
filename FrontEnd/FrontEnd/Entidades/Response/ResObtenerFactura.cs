using FrontEnd.Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Response
{
    public class ResObtenerFactura : ResBase
    {
        public List<Factura> listaFacturas = new List<Factura>();
    }
}
