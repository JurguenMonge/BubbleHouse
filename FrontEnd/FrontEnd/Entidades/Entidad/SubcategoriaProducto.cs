﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Entidades.Entidad
{
    public class SubcategoriaProducto
    {
        public int idSubcategoriaProducto { get; set; }
        public int cateProductoId { get; set; }
        public string dscNombreSubCategoria { get; set; }
        public bool estado { get; set; }

    }
}