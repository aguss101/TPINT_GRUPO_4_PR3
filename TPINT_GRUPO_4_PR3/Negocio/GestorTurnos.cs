using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
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
        public bool ModificarTurnoG( Turno turno ) { return consultas.ModificarTurnoG(turno); }

        public List<DateTime> ObtenerFechasDisponibles(string legajo, DateTime desde, DateTime hasta) { return consultas.ObtenerFechasDisponibles(legajo, desde, hasta); }
    
    }
}