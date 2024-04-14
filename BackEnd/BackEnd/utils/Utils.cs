using BackEnd.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.utils
{
    public class Utils
    {
        public static void crearBitacora(List<String> listaDeErrores, short tipo, string laClase, string elMetodo, string elRequest, string elResponse)
        {
            try
            {
                ConexionDataContext linq = new ConexionDataContext();
                linq.Insertar_Bitacora(laClase, elMetodo, tipo, listaDeErrores.ToString(), elRequest, elResponse);
            }
            catch (Exception e)
            {
                try
                {
                    //No se pudo bitacorear en BD. Entonces en un txt
                    string dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
                    string path = Path.Combine(dataDirectory, "logErroresEnBd.txt");

                    // Verificar si el directorio existe, si no, crearlo
                    if (!Directory.Exists(dataDirectory))
                    {
                        Directory.CreateDirectory(dataDirectory);
                    }
                    TextWriter loguearEnTexto = new StreamWriter(path);
                    loguearEnTexto.WriteLine("NO SE PUDO BITACOREAR EN BD EL MENSAJE DE ERROR FUE: " + e.StackTrace.ToString() + " --> CLASE: " + laClase + " METODO: " + elMetodo + " TIPO " + tipo + " DESCRIPCION " + listaDeErrores.ToString() + " REQ: " + elRequest + " RES " + elResponse + " FECHA: " + DateTime.Now.ToString());
                    loguearEnTexto.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al escribir en el archivo de log: " + ex.Message);
                }
            }
        }
    }
}
