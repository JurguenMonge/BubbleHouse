using BackEnd.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public static class ValidacionProducto
    {
        public static void ValidarSubCategoria(Producto producto, ResProducto res, ref short tipoRegistro)
        {
            if (producto.subcategoriaProducto.idSubcategoriaProducto == 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Subcategoría inexistente");
                tipoRegistro = 2;
            }
        }
        public static void ValidarNombre(Producto producto, ResProducto res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(producto.nombreProducto))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Nombre faltante");
                tipoRegistro = 2;
            }
        }

        public static void ValidarDescripcion(Producto producto, ResProducto res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(producto.descripcion))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Descripción faltante");
                tipoRegistro = 2;
            }
        }

        public static void ValidarUrlImagen(Producto producto, ResProducto res, ref short tipoRegistro)
        {
            if (String.IsNullOrEmpty(producto.urlImgen))
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Imagen faltante");
                tipoRegistro = 2;
            }
        }

        public static void ValidarPrecio(Producto producto, ResProducto res, ref short tipoRegistro)
        {
            if (producto.precio <= 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Precio faltante o negativo");
                tipoRegistro = 2;
            }
        }

    }
}
