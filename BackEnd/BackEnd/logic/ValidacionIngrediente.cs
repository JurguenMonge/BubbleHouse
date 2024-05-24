

using BackEnd.domain;
using BackEnd.domain.response;
using System.Text.RegularExpressions;

namespace BackEnd.logic
{
    public static class ValidacionIngrediente
    {
        public static void ValidarCategoria(Ingrediente ingrediente, ResReceta res, ref short tipoRegistro)
        {
            if (ingrediente.idCategoriaIngrediente == 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Categoría inexistente");
                tipoRegistro = 2;
            }
        }

        public static void ValidarNombreIngrediente(Ingrediente ingrediente, ResReceta res, ref short tipoRegistro)
        {
            if (ingrediente.idCategoriaIngrediente == 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Categoría inexistente");
                tipoRegistro = 2;
            }
        }

        public static void ValidarDescripcion(Ingrediente ingrediente, ResReceta res, ref short tipoRegistro)
        {

            if (string.IsNullOrEmpty(ingrediente.dscDescripcion))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Descripción faltante");
                tipoRegistro = 2;
            }
            string pattern = @"^[a-zA-Z0-9 ./]*$";
            if (!Regex.IsMatch(ingrediente.dscDescripcion, pattern))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("La descripción contiene caracteres no permitidos");
                tipoRegistro = 2;
            }
        }

        public static void ValidarImagen(Ingrediente ingrediente, ResReceta res, ref short tipoRegistro)
        {
            if (string.IsNullOrEmpty(ingrediente.dscURLImagen))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Imagen faltante");
                tipoRegistro = 2;
            }
            string pattern = @"^[0-9./]+$";
            if (!Regex.IsMatch(ingrediente.dscURLImagen, pattern))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("La imagen contiene caracteres no permitidos. Solo se permiten números, puntos y barras diagonales.");
                tipoRegistro = 2;
            }
        }

        public static void ValidarPrecio(Ingrediente ingrediente, ResReceta res, ref short tipoRegistro)
        {
            if (ingrediente.numPrecio == 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Precio inexistente");
                tipoRegistro = 2;
            }
        }
    }
}
