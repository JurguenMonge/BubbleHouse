using FrontEnd.Entidades.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Response
{
    public class ResObtenerProductosFactura : ResBase
    {
        public List<ContenedorProducto> Contenedores = new List<ContenedorProducto>();
    }
}
