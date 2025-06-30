using System;
using System.Collections.Generic;
using System.Data;
using Datos;
using Entidades;

namespace Negocio
{
    public class GestorTurnos
    {
        ConsultasTurnos consultas = new ConsultasTurnos();
        public List<Turno> GetTurnos() { return consultas.GetTurnosAdmin(); }
        public List<Turno> GetTurnosMedico(string legajo, DateTime? fechaSelected) { return consultas.GetTurnosMedico(legajo, fechaSelected); }
        public List<Turno> FiltrarPacientexApellido(string legajo, string apellidoSelected) { return consultas.FiltrarPacientexApellido(legajo, apellidoSelected); }
        public List<Turno> FiltrarPacientexDNI(string legajo, string dniPaciente) { return consultas.FiltrarPacientexDNI(legajo, dniPaciente); }
        public DataTable ObtenerMedicosPorEspecialidad(int idEspecialidad) { return consultas.ObtenerMedicosPorEspecialidad(idEspecialidad); }
        public DataTable ObtenerHorasDisponibles(string legajo, DateTime fecha) { return consultas.ObtenerHorasDisponibles(legajo, fecha); }

        public List<DateTime> ObtenerFechasDisponibles(string legajo, DateTime desde, DateTime hasta)
        {
            List<DateTime> fechas = new List<DateTime>();
            DateTime dia = desde.Date;
            while (dia <= hasta.Date)
            {
                DataTable dtHoras = consultas.ObtenerHorasDisponibles(legajo, dia);

                if (dtHoras.Rows.Count > 0)
                {
                    fechas.Add(dia);
                }
                dia = dia.AddDays(1);
            }
            return fechas;

        }
    }
}