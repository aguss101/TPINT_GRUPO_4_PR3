using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Entidades;

namespace Datos
{
    public class ConsultasTurnos
    {
        private DataAccess conexion = new DataAccess();

        public List<Turno> GetTurnosAdmin()
        {


            List<Turno> turnos = new List<Turno>();

            using (SqlConnection conection = conexion.AbrirConexion())
            {

                using (SqlCommand command = new SqlCommand("SELECT * FROM vw_TurnosConDatos ORDER BY FechaPactada,Legajo,DNIPaciente", conection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Uso el mapeo de Turno

                            turnos.Add(MapearTurno(reader));
                        }
                    }
                }
                return turnos;
            }
        }
        public List<Turno> GetTurnosMedico(string legajo, DateTime? fechaSelected)
        {
            List<Turno> turnos = new List<Turno>();

            using (SqlConnection con = conexion.AbrirConexion())
            {

                string q = "SELECT * FROM vw_TurnosConDatos WHERE @Legajo = Legajo AND (@Fecha IS NULL OR CONVERT(date, fechaPactada) = CONVERT(date, @Fecha)) ORDER BY fechaPactada,Legajo,DNIPaciente";
                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25)
                    {
                        Value = legajo
                    });
                    cmd.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime)
                    {
                        Value = fechaSelected.HasValue ? (object)fechaSelected.Value.Date : DBNull.Value
                    });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Uso el mapeo de Turno
                            turnos.Add(MapearTurno(reader));
                        }
                    }
                }
                return turnos;
            }
        }

        // Marca la asistencia de un turno sin procedimientos almacenados
        public int MarcarAsistenciaTurno(Turno turno)
        {

            Debug.Print("Legajo: " + turno.Legajo);
            Debug.Print("FechaPactada: " + turno.FechaPactada.ToString("s"));
            Debug.Print("Estado: " + turno.Estado);
            Debug.Print("Observacion: " + turno.Observacion);


            string q = "UPDATE Turnos SET estado=@Estado, observacion=@Observacion, diagnostico=@Diagnostico WHERE Legajo=@Legajo AND fechaPactada=@Fecha";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Estado", turno.Estado),
                new SqlParameter("@Observacion", string.IsNullOrWhiteSpace(turno.Observacion) ? (object)DBNull.Value : turno.Observacion),
                new SqlParameter("@Diagnostico", string.IsNullOrWhiteSpace(turno.Diagnostico) ? (object)DBNull.Value : turno.Diagnostico),
                new SqlParameter("@Legajo", turno.Legajo),
                new SqlParameter("@Fecha", turno.FechaPactada)
            };
            return conexion.EjecutarComandoConParametros(q, parametros);
        }





        private Turno MapearTurno(SqlDataReader reader)
        {
            return new Turno
            {
                Legajo = reader["Legajo"].ToString(),
                DNIPaciente = reader["DNIPaciente"].ToString(),
                FechaPactada = (DateTime)reader["fechaPactada"],
                EstadoId = (int)reader["EstadoId"],
                EstadoDescripcion = reader["EstadoDescripcion"].ToString(),
                Observacion = reader["observacion"].ToString(),
                Diagnostico = reader["diagnostico"].ToString(),
                Paciente = reader["Paciente"].ToString(),
                ObraSocial = reader["ObraSocial"].ToString(),
                Medico = reader["Medico"].ToString(),
                IdEspecialidad = (int)reader["idEspecialidad"],
                Especialidad = reader["Especialidad"].ToString()
            };
        }

        public List<Turno> FiltrarPacientexApellido(string legajo, string apellido)
        {
            List<Turno> turnos = new List<Turno>();

            using (SqlConnection con = conexion.AbrirConexion())
            {

                string q = "SELECT * FROM vw_TurnosConDatos WHERE Legajo = @Legajo AND (@Apellido IS NULL OR ApellidoPaciente COLLATE Latin1_General_CI_AI LIKE '%' + @Apellido + '%') ORDER BY fechaPactada, Legajo, DNIPaciente";
                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25)
                    {
                        Value = legajo
                    });
                    cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 25)
                    {
                        Value = apellido
                    });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Uso el mapeo de Turno
                            turnos.Add(MapearTurno(reader));
                        }
                    }
                }
                return turnos;
            }
        }
        public List<Turno> FiltrarPacientexDNI(string legajo, string dniPaciente)
        {
            List<Turno> turnos = new List<Turno>();

            using (SqlConnection con = conexion.AbrirConexion())
            {

                string q = "SELECT * FROM vw_TurnosConDatos WHERE Legajo = @Legajo AND (@DNI IS NULL OR DNIPaciente LIKE '%' + @DNI + '%') ORDER BY fechaPactada, Legajo, DNIPaciente";
                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25)
                    {
                        Value = legajo
                    });
                    cmd.Parameters.Add(new SqlParameter("@DNI", SqlDbType.VarChar, 20)
                    {
                        Value = dniPaciente
                    });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Uso el mapeo de Turno
                            turnos.Add(MapearTurno(reader));
                        }
                    }
                }
                return turnos;
            }
        }
        public DataTable ObtenerMedicosPorEspecialidad(int idEspecialidad)
        {
            string consulta = @"
        SELECT M.Legajo, P.nombre + ' ' + P.apellido AS NombreCompleto
        FROM Medico M
        INNER JOIN Persona P ON M.DNI = P.DNI
        WHERE M.idEspecialidad = @idEspecialidad";

        SqlParameter[] parametros = new SqlParameter[]
        {
            new SqlParameter("@idEspecialidad", idEspecialidad)
        };

            return conexion.EjecutarConsultaConParametros(consulta, parametros);
        }

    }

}
