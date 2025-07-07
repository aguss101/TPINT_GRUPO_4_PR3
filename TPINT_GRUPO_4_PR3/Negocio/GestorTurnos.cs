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
        public List<Turno> GetTurnosOrdX(string query) { return consultas.GetTurnosOrdX(query); }

        public DataTable FiltradoTurnosMedico(string estadoTurno, string legajo) { return consultas.FiltradoTurnosMedico(estadoTurno, legajo); }
        public List<Turno> FiltrarPacientexApellido(string legajo, string apellidoSelected) { return consultas.FiltrarPacientexApellido(legajo, apellidoSelected); }

        public List<Turno> FiltrarPacientexDNI(string legajo, string dniPaciente) { return consultas.FiltrarPacientexDNI(legajo, dniPaciente); }
        public DataTable ObtenerMedicosPorEspecialidad(int idEspecialidad) { return consultas.ObtenerMedicosPorEspecialidad(idEspecialidad); }
        public DataTable ObtenerHorasDisponibles(string legajo, DateTime fecha) { return consultas.ObtenerHorasDisponibles(legajo, fecha); }
        public bool ModificarTurno(Turno turno) { return consultas.ModificarTurno(turno); }
        public int EliminarTurno(string legajo, DateTime fechapactada) { return consultas.EliminarTurno(legajo, fechapactada); }
        public DataTable ObtenerEspecialidades() { return consultas.ObtenerEspecialidades(); }
        public int InsertarTurno(Turno turno) { return consultas.InsertarTurno(turno); }

        public int MarcarAsistenciaTurnoMedico(Turno turno) { return consultas.MarcarAsistenciaTurnoMedico(turno); }

        public List<DateTime> ObtenerFechasDisponibles(string legajo, DateTime desde, DateTime hasta) { return consultas.ObtenerFechasDisponibles(legajo, desde, hasta); }

    }
}