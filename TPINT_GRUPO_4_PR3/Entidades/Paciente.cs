using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Paciente : Persona
    {
       public string ObraSocial { get; set; }
        public DateTime ultimaAtencion { get; set; }

        public DateTime Alta { get; set; }

    }
}
