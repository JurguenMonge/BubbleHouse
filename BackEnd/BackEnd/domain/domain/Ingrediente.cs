using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain
{
    public class Ingrediente : CategoriaIngrediente
    {

        public int idIngrediente { get; set; }
        public String dscDescripcion { get; set; }
        public String dscURLImagen { get; set; }
        public float numPrecio { get; set; }
        public bool estadoIngre { get; set; }

    }
}
