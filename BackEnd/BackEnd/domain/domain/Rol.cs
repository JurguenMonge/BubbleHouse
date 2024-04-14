using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain
{
    public class Rol
    {
        public int idRol { get; set; }
        public String tipoRol { get; set; }
        public String permisos { get; set; }
        public bool estado { get; set; }
    }
}
