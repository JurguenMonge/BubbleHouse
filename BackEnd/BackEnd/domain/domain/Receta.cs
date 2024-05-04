using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain
{
    public class Receta
    {
        public int idReceta { get; set; }
        public int idIngLacteo { get; set; }
        public int idIngSabor { get; set; }
        public int idIngAzucar { get; set; }
        public int idIngTopping { get; set; }
        public int idIngBordeado { get; set; }
        public int idIngBubbles { get; set; }
        public String dscNombre { get; set; }
        public String dscTamano { get; set; }
        public DateTime fecha { get; set; }
        public bool estado { get; set; }
        public Producto producto { get; set; }

        public List<Ingrediente> listaIngrediente = new List<Ingrediente>();

    }
}
