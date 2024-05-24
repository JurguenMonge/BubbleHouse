using BackEnd.domain.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain.response
{
    public class ResObtenerProductosFactura : ResBase
    {
        public List<ContenedorProducto> Contenedores = new List<ContenedorProducto>();
    }
}
