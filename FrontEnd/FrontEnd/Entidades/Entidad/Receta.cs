using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Entidad
{
    public class Receta
    {
        public int idReceta { get; set; }
        public String dscNombre { get; set; }
        public DateTime fecha { get; set; }
        public bool estado { get; set; }

        public List<Ingrediente> listaIngrediente = new List<Ingrediente>();

    }
}
