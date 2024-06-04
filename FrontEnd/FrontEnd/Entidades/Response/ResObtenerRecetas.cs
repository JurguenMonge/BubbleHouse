

using FrontEnd.Entidades.Entidad;

namespace FrontEnd.Entidades.Response
{
    public class ResObtenerRecetas : ResBase
    {
        public List<RecetaCompleta> listaRecetas = new List<RecetaCompleta>();
    }
}
