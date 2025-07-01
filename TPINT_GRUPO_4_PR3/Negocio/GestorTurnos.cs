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
        //public List<Turno> GetTurnosObs_Dia(string observacion, string diagnostico) { return consultas.GetTurnosObs_Dia(observacion, diagnostico); }
        public List<Turno> GetTurnosMedico(string legajo, DateTime? fechaSelected) { return consultas.GetTurnosMedico(legajo, fechaSelected); }
        public List<Turno> FiltrarPacientexApellido(string legajo, string apellidoSelected) { return consultas.FiltrarPacientexApellido(legajo, apellidoSelected); }
        public List<Turno> FiltrarPacientexDNI(string legajo, string dniPaciente) { return consultas.FiltrarPacientexDNI(legajo, dniPaciente); }
        public DataTable ObtenerMedicosPorEspecialidad(int idEspecialidad) { return consultas.ObtenerMedicosPorEspecialidad(idEspecialidad); }
        public DataTable ObtenerHorasDisponibles(string legajo, DateTime fecha) { return consultas.ObtenerHorasDisponibles(legajo, fecha); }
        public bool ModificarTurnoG(Turno turno)
        {
            return consultas.ModificarTurnoG(turno);
        }

        public List<DateTime> ObtenerFechasDisponibles(string legajo, DateTime desde, DateTime hasta)
        {
            List<DateTime> fechas = new List<DateTime>();
            DateTime dia = desde.Date;

            while (dia <= hasta.Date)
            {
                DataTable dtHoras = consultas.ObtenerHorasDisponibles(legajo, dia);

                if (dtHoras.Rows.Count > 0)
                {

                    if (dia > DateTime.Today)
                    {
                        fechas.Add(dia);
                    }
                    else if (dia == DateTime.Today)
                    {
                        TimeSpan horaActual = DateTime.Now.TimeOfDay;

                        var filasValidas = dtHoras.AsEnumerable()
                        .Where(row =>
                        {
                            
                            TimeSpan horaTurno = TimeSpan.Parse(row["RangoHorario"].ToString());
                            return horaTurno > horaActual;
                        });
                        if (filasValidas.Any())
                        {
                            fechas.Add(dia);
                        }

                    }
                }
                dia = dia.AddDays(1);
            }
            return fechas;

        }
    }
}