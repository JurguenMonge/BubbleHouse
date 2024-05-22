using BackEnd.domain.request;
using BackEnd.domain.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public class LogFactura
    {
        public ResFactura obtenerFacturasTodas()
        {
            ResFactura res = new ResFactura();
            short tipoRegistro = 0;
            try
            {


            }catch (Exception ex)
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener la lista de ingredientes");
                tipoRegistro = 3;
            }
            finally
            {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }
    }
}
