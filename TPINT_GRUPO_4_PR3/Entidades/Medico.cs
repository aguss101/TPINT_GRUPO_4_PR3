using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico : Persona
    {
        public Usuario Usuario { get; set; }
        public string Legajo { get; set; }
        public Especialidad Especialidad { get; set; }
    }
}