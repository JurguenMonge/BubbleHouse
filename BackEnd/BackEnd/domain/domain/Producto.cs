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
        public SubcategoriaProducto subcategoriaProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion {  get; set; }
        public string urlImgen {  get; set; }
        public float precio { get; set; }
        public bool estado {  get; set; }

    }
}
