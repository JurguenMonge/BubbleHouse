using FrontEnd.Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Request
{
    public class ReqFactura : ReqBase
    {
        public Factura Factura { get; set; }
    }
}
