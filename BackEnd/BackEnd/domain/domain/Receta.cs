﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain
{
    public class Receta
    {
        public int idReceta { get; set; }
        public String dscNombre { get; set; }
        public DateTime fecha { get; set; }
        public bool estado { get; set; }

    }
}
