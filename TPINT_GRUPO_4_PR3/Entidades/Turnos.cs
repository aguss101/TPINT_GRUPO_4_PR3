using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Turnos
    {
        public string Legajo { get; set; }
        public string DNIPaciente { get; set; }
        public DateTime FechaPactada { get; set; }
        public int Estado { get; set; } 

        public string observacion { get; set; }
        public string diagnostico { get; set; }


    }
}
