using BackEnd.domain.response;
using BackEnd.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public class ValidacionesIngrediente
    {
        public static void ValidarCategoria(Ingrediente ingrediente, ResIngrediente res, ref short tipoRegistro)
        {
            if (ingrediente.idCategoriaIngrediente == 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Categoría de ingrediente inexistente");
                tipoRegistro = 2;
            }
        }
        public static void ValidarNombre(Ingrediente ingrediente, ResIngrediente res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(ingrediente.dscNombre))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Nombre faltante");
                tipoRegistro = 2;
            }
        }
        public static void ValidarDescripcion(Ingrediente ingrediente, ResIngrediente res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(ingrediente.dscDescripcion))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Descripción faltante");
                tipoRegistro = 2;
            }
        }
        public static void ValidarUrlImagen(Ingrediente ingrediente, ResIngrediente res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(ingrediente.dscURLImagen))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Imagen faltante");
                tipoRegistro = 2;
            }
        }

        public static void ValidarPrecio(Ingrediente ingrediente, ResIngrediente res, ref short tipoRegistro)
        {
            if (ingrediente.numPrecio < 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Precio faltante o negativo");
                tipoRegistro = 2;
            }
        }
    }
}
