using BackEnd.domain.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain.request
{
    public class ReqContenedorProducto : ReqBase
    {
        public ContenedorProductoFactura contenedor {  get; set; }
    }
}
