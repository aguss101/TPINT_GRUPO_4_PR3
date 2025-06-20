using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico : Persona
    {
        public string Legajo { get; set; }
        public int idEspecialidad { get; set; }
    }
}