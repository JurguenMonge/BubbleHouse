using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain
{
    public class Producto 
    {
        public int idProducto { get; set; }
        public int idSubcateProducto { get; set; }
        public string dscNombreProducto { get; set; }
        public string dscDescripcion {  get; set; }
        public string dscUrlImgen {  get; set; }
        public float numPrecio { get; set; }
        public bool estado {  get; set; }

    }
}
