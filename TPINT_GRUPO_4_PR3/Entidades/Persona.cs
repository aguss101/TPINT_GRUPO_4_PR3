using System;

namespace Entidades
{
    public class Persona
    {
        public string DNI { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public Sexos sexos { get; set; }
        public string generoDescripcion { get; set; }
        public int Provincia { get; set; }
        public Provincias Provincias { get; set; }
        public int Localidad { get; set; }
        public Localidades Localidades { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string nacionalidad { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
        public string Correo { get; set; }


    }
}
