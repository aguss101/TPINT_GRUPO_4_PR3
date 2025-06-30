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
                        //Tomo los horarios actuales para saber cuales vencieron...
                        TimeSpan horaActual = DateTime.Now.TimeOfDay;



                        //AsEnumerable convierte un dataTable en una coleccion enumerable de filas
                        var filasValidas = dtHoras.AsEnumerable()
                        //Consulto si algun turno de hoy, cuenta con un horario mayor al actual, para poder efectuar la modificacion y no este vencido el horario
                        .Where(row =>
                        {
                            
                            TimeSpan horaTurno = TimeSpan.Parse(row["RangoHorario"].ToString());
                            return horaTurno > horaActual;
                        });
                        //.Any verifica si al menos hay alguna coincidencia, en caso de ser asi, retorna el bool de filas validas, y agrega el horario de hoy disponible
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