using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades
{
    public class ResBase
    {
        public bool Resultado { get; set; }
        public List<String> ListaDeErrores = new List<String>();
    }
}
