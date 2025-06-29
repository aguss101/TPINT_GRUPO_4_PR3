using System.Collections.Generic;
using Datos;
using Entidades;
using System;
using System.Data;

namespace Negocio
{
    public class GestorTurnos
    {
        ConsultasTurnos consultas = new ConsultasTurnos();


        public List<Turno> GetTurnos()
        {
            return consultas.GetTurnosAdmin();
        }

        public List<Turno> GetTurnosMedico(string legajo, DateTime? fechaSelected)
        {
            return consultas.GetTurnosMedico(legajo, fechaSelected);
        }
        public List<Turno> FiltrarPacientexApellido(string legajo, string apellidoSelected)
        {
            return consultas.FiltrarPacientexApellido(legajo, apellidoSelected);
        }
        public List<Turno> FiltrarPacientexDNI(string legajo, string dniPaciente)
        {
            return consultas.FiltrarPacientexDNI(legajo, dniPaciente);
        }
        public DataTable ObtenerMedicosPorEspecialidad(int idEspecialidad)
        {
            return consultas.ObtenerMedicosPorEspecialidad(idEspecialidad);
        }
    };
}
