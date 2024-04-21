using BackEnd.data;
using BackEnd.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
   public class ValidacionesCategoriaProducto
    {
        public static void ValidarNombreCategoria(CategoriaProducto categoriaProducto, ResCategoriaProducto res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(categoriaProducto.dscNombreCategoria))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Nombre de la categoria faltante");
                tipoRegistro = 2;
            }
        }


    }
}
