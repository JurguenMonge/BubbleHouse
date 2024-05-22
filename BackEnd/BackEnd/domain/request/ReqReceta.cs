using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain.request
{
    public class ReqReceta
    {
        public Receta Receta { get; set; }
        public int idProducto { get; set; }

        public String id_Sesion { get; set; }
    }
}
