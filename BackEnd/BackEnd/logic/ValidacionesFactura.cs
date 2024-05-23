using BackEnd.domain.response;
using BackEnd.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BackEnd.domain.domain;

namespace BackEnd.logic
{
    public class ValidacionesFactura { 


        public static void ValidarFactura(Factura factura, ResFactura res, ref short tipoRegistro)
        {
            if (factura == null)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("factura nula.");
                tipoRegistro = 2;
                return;
            }
            if (factura.idFactura <= 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("factura vacia");
                tipoRegistro = 2;
            }
        }
    public static void ValidarFecha(Factura factura, ResFactura res, ref short tipoRegistro)
        {
            if (factura.fecha == null)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("La fecha de la factura no puede estar vacía.");
                tipoRegistro = 1; // Tipo de registro para error de fecha nula
                return;
            }
        }
        public static void ValidarTotal(Factura factura, ResFactura res, ref short tipoRegistro)
        {
            if (factura.numTotal == null)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("El total de la factura no puede estar vacía.");
                tipoRegistro = 2; // Tipo de registro para error de fecha nula
                return;
            }
            const float tolerancia = 0.0001f;
            if (Math.Abs(factura.numTotal) < tolerancia)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("El total de la factura no puede ser 0 o ser menor");
                tipoRegistro = 2; // Tipo de registro para error de fecha nula
                return;
            }
        }
        public static void ValidarNumSubTotal(ContenedorProductoFactura contenedor, ResFactura res, ref short tipoRegistro)
        {
            if (contenedor.numSubtotal == null)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("El subtotal de la factura no puede estar vacía.");
                tipoRegistro = 2; // Tipo de registro para error de fecha nula
                return;
            }
            if (contenedor.numSubtotal <= 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("El subtotal de la factura no puede ser 0 o ser menor");
                tipoRegistro = 2; // Tipo de registro para error de fecha nula
                return;
            }
        }
        public static void ValidarDescuento(ContenedorProductoFactura contenedor, ResFactura res, ref short tipoRegistro)
        {
            if (contenedor.descuento == null)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("El descuento de la factura no puede estar vacía.");
                tipoRegistro = 2; // Tipo de registro para error de fecha nula
                return;
            }
            if (contenedor.descuento < 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("El descuento de la factura no puede ser 0 o ser menor");
                tipoRegistro = 2; // Tipo de registro para error de fecha nula
                return;
            }
        }
        public static void ValidarCantidad(ContenedorProductoFactura contenedor, ResFactura res, ref short tipoRegistro)
        {
            if (contenedor.numCantidad == null)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("El total de la factura no puede estar vacía.");
                tipoRegistro = 2; // Tipo de registro para error de fecha nula
                return;
            }
            if (contenedor.numCantidad <= 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("El total de la factura no puede ser 0 o ser menor");
                tipoRegistro = 2; // Tipo de registro para error de fecha nula
                return;
            }
        }

        public static void ValidarPrecio(Producto producto, ResFactura res, ref short tipoRegistro)
        {
            if (producto == null)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Producto nulo.");
                tipoRegistro = 2;
                return;
            }
            if (producto.precio <= 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Precio faltante o negativo");
                tipoRegistro = 2;
            }
        }

        public static void ValidarProducto(Producto producto, ResFactura res, ref short tipoRegistro)
        {
            if (producto == null)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Producto nulo.");
                tipoRegistro = 2; 
                return;
            }
            if (producto.idProducto <= 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Producto vacio");
                tipoRegistro = 2;
            }
        }

        public static void ValidarContenedor(ContenedorProductoFactura contenedor, ResFactura res, ref short tipoRegistro)
        {
            if (contenedor == null)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("contendor nulo.");
                tipoRegistro = 2;
                return;
            }
            if (contenedor.IdRFacturaProducto <= 0)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Contenedor vacio");
                tipoRegistro = 2;
            }
        }

        internal static void ValidarNombre(string nombreProducto, ResFactura res, ref short tipoRegistro)
        {
            throw new NotImplementedException();
        }
    }
}
