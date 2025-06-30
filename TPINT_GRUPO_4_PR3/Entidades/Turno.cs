using System;

namespace Entidades
{
    public class Turno
    {
        public string Legajo { get; set; }
        public string DNIPaciente { get; set; }
        public DateTime FechaPactada { get; set; }
        public int Estado { get; set; }
        public string Observacion { get; set; }
        public string Diagnostico { get; set; }
        public int EstadoId { get; set; }
        public string EstadoDescripcion { get; set; }
        public string Paciente { get; set; }
        public string ObraSocial { get; set; }
        public string Medico { get; set; }
        public int IdEspecialidad { get; set; }
        public string Especialidad { get; set; }

        //Extras para logica como reemplazo del IdTurno el cual no tenemos
        public DateTime FechaOriginal { get; set; }
    }
}
