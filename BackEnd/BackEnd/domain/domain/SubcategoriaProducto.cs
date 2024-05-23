using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain
{
    public class SubcategoriaProducto
    {
        public int idSubcategoriaProducto {  get; set; }
        public int cateProductoId {  get; set; }
        public string dscNombreSubCategoria {  get; set; }
        public string dscNombreCategoria { get; set; }
        public bool estado {  get; set; }

    }
}
