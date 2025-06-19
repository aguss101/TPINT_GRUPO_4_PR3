using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Persona
    {
        private int DNI { get; set; }
        private string nombre { get; set; }
        private string apellido { get; set; }
        private Genero genero { get; set; }
        private int idLocalidad { get; set; }
        private DateTime fechaNacimiento { get; set; }
        private string nacionalidad { get; set; }

    }
}
