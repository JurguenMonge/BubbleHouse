using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain.response
{
    public class ResObtenerIngredientes : ResBase
    {
        public List<Ingrediente> ListaIngredientes = new List<Ingrediente>();
    }
}
