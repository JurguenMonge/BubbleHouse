using FrontEnd.Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Request
{
    public class ReqReceta
    {
        public String id_Sesion { get; set; }
        public Producto Producto { get; set; }
    }
}
