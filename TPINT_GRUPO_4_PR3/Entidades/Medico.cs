using System;

namespace Entidades
{
    public class Medico : Persona
    {
        public Usuario Usuario { get; set; }
        public string Legajo { get; set; }
        public TimeSpan entrada { get; set; }
        public Especialidad Especialidad { get; set; }

    }
}