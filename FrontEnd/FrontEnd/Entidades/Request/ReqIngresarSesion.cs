using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades
{
    public class ReqIngresarSesion
    {
        public String password { get; set; }
        public String correo { get; set; }
        public String origen { get; set; }
    }
}
