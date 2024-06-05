using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Request
{
    public class ReqComprobarPassword
    {
        public String correo { get; set; }
        public String password { get; set; }
    }
}
