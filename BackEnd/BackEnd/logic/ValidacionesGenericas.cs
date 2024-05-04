using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackEnd.logic
{
    public class ValidacionesGenericas
    {
        public int validarString(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 1;
            }
            string patron = @"[!@#$%^&*()_+{}\[\]:;\""'<>,.?/\\|`~-]";
            Regex regex = new Regex(patron);
            if (regex.IsMatch(str))
            {
                return 2;
            }
            return 3;
        }
        public int validarInt(int inte) 
        { 
            if(inte < 0)
            {
                return 1;
            }
            return 3;
        }

    }
}
