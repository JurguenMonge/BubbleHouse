using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades
{
    public class Sesion
    {
        public String Id_Sesion { get; set; }
        public Usuario Usuario { get; set; }
        public String Token_Sesion { get; set; }
        public String Origen { get; set; }
        public String Cierre { get; set; }
        public DateTime Fec_inicio { get; set; }
        public DateTime Fec_cierre { get; set; }
        public int Estado { get; set; }
    }
}
