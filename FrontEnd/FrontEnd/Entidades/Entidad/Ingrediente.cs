using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Entidad
{
    public class Ingrediente
    {

        public int idIngrediente { get; set; }
        public int idCategoriaIngrediente { get; set; }
        public String dscNombre { get; set; }
        public String dscDescripcion { get; set; }
        public String dscNombreCategoriaIngrediente { get; set; }
        public String dscURLImagen { get; set; }
        public decimal numPrecio { get; set; }
        public bool estado { get; set; }

    }
}
