using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain
{
    public class RecetaCompleta
    {
        public int recetaId {  get; set; }
        public string nombreReceta { get; set; }
        public DateTime fecha { get; set; }
        public List<string> ingredientes { get; set; } = new List<string>();
    }
}
