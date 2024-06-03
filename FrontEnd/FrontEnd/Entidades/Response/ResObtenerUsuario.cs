using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Response
{
    public class ResObtenerUsuario : ResBase
    {
        public List<Usuario> listaUsuarios = new List<Usuario>();
    }
}
