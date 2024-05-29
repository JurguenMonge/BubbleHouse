using FrontEnd.Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Response
{
    public class ResObtenerIngredientes : ResBase
    {
        public List<Ingrediente> listaIngredientes = new List<Ingrediente>();
    }
}
