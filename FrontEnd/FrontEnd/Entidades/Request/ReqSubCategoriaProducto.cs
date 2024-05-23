using FrontEnd.Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Request
{
    public class ReqSubCategoriaProducto : ReqBase
    {
        public SubcategoriaProducto SubCategoriaProducto { get; set; }
    }
}
