﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain.response
{
    public class ResObtenerRecetas : ResBase
    {
        public List<Receta> listaRecetas = new List<Receta>();
    }
}