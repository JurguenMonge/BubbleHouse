using BackEnd.domain.request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.domain
{
    public class ReqIngresarUsuario : ReqBase
    {
        public Usuario Usuario { get; set; }
    }
}
