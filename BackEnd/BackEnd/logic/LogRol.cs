using BackEnd.data;
using BackEnd.domain;
using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace BackEnd.logic
{
    public class LogRol
    {
        public ResObtenerRol obtenerRoles()
        {
            ResObtenerRol res = new ResObtenerRol();
            short tipoRegistro = 0; //1 Exitoso - 2 Error en logica - 3 Error inesperado
            try
            {
                ConexionDataContext linq = new ConexionDataContext();
                int? idError = 0;
                String errorBD = "";
                var linqRoles = linq.Obtener_Roles(ref idError, ref errorBD).ToList();
                if (idError == 0)
                {
                    res.Resultado = true;
                    tipoRegistro = 1;
                    foreach (var item in linqRoles)
                    {
                        Rol rol = factoryArmarRoles(item);
                        if (rol != null)
                        {
                            res.listaRoles.Add(rol);
                        }
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaDeErrores.Add("Ocurrió un error en la base de datos, intentalo más tarde");
                    tipoRegistro = 2;
                }
            }
            catch (Exception)
            {
                res.Resultado = false;
                res.ListaDeErrores.Add("Ocurrió un error al obtener la lista de roles");
                tipoRegistro = 3; 
            }finally {
                utils.Utils.crearBitacora(res.ListaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No hay request", JsonConvert.SerializeObject(res));
            }
            return res;
        }

        private Rol factoryArmarRoles(Obtener_RolesResult rolDeLinq)
        {
            Rol rol = new Rol();
            rol.idRol = rolDeLinq.ID_ROL;
            rol.tipoRol = rolDeLinq.DSC_TIPO_ROL;
            rol.permisos = rolDeLinq.DSC_PERMISOS;
            rol.estado = rolDeLinq.ESTADO.HasValue ? (rolDeLinq.ESTADO.Value == 1 ? true : false) : false;
            return rol;
        }
    }
}
