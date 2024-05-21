using BackEnd.domain.response;
using BackEnd.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public class ValidacionesSubCategoriaProducto
    {
        public static void ValidarNombreSubCategoria(SubcategoriaProducto subCategoriaProducto, ResSubCategoriaProducto res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(subCategoriaProducto.dscNombreSubCategoria))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Nombre de la categoria faltante");
                tipoRegistro = 2;
            }
        }
    }
}
