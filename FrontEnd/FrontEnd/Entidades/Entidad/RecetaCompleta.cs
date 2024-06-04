

namespace FrontEnd.Entidades.Entidad
{
    public class RecetaCompleta
    {
        public int recetaId { get; set; }
        public string nombreReceta { get; set; }
        public DateTime fecha { get; set; }
        public List<string> ingredientes { get; set; } = new List<string>();
    }
}
