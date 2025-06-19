using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public string DNI { get; set; }
        public int idRol { get; set; }
        public string NombreUsuario { get; set; }
        public string contrasenia { get; set; }
        public DateTime ultimoIngreso { get; set; }
        public DateTime alta { get; set; }
    }
}
