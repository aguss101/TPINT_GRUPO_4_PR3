using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Entidades;

namespace Datos
{
    public class ConsultasTurnos
    {
        private DataAccess conexion = new DataAccess();
        public List<Turno> GetTurnosAdmin()
        {
            List<Turno> turnos = new List<Turno>();
            string query = @"SELECT * FROM vw_TurnosConDatos
                             ORDER BY FechaPactada,Legajo,DNIPaciente";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    { while (reader.Read()) { turnos.Add(MapearTurno(reader)); } }
                }
            }
            catch (Exception ex) { throw new Exception("Error al cargar turnos: " + ex.Message); }
            return turnos;
        }
        public List<Turno> GetTurnosMedico(string legajo, DateTime? fechaSelected)
        {
            var turnos = new List<Turno>();
            string query = @"
                    SELECT *
                    FROM vw_TurnosConDatos
                    WHERE Legajo = @Legajo
                    AND (@Fecha IS NULL OR CONVERT(date, fechaPactada) = @Fecha)
                    ORDER BY fechaPactada, Legajo, DNIPaciente;
            ";


            using (SqlConnection connection = conexion.AbrirConexion())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25) { Value = legajo });
                command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.Date) { Value = fechaSelected.HasValue ? (object)fechaSelected.Value.Date : DBNull.Value });

                using (SqlDataReader reader = command.ExecuteReader())
                { while (reader.Read()) { turnos.Add(MapearTurno(reader)); } }
            }
            return turnos;
        }
        public bool ModificarTurno(Turno turno)
        {
            string query = @"
            UPDATE Turnos 
            SET fechaPactada = @FechaNueva, observacion = @Observacion, diagnostico = @Diagnostico
            WHERE Legajo = @Legajo 
            AND DNIPaciente = @DNIPaciente
            AND CONVERT(date, fechaPactada) = CONVERT(date, @FechaOriginal)";


            using (SqlConnection con = conexion.AbrirConexion())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FechaNueva", turno.FechaPactada);
                cmd.Parameters.AddWithValue("@FechaOriginal", turno.FechaOriginal);
                cmd.Parameters.AddWithValue("@Observacion", turno.Observacion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Diagnostico", turno.Diagnostico ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Legajo", turno.Legajo);
                cmd.Parameters.AddWithValue("@DNIPaciente", turno.DNIPaciente);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public int MarcarAsistenciaTurno(Turno turno)
        {
            string query = @"
                UPDATE Turnos
                SET estado=@Estado, observacion=@Observacion, diagnostico=@Diagnostico 
                WHERE Legajo=@Legajo AND fechaPactada=@Fecha";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Estado", turno.Estado),
                new SqlParameter("@Observacion", string.IsNullOrWhiteSpace(turno.Observacion) ? (object)DBNull.Value : turno.Observacion),
                new SqlParameter("@Diagnostico", string.IsNullOrWhiteSpace(turno.Diagnostico) ? (object)DBNull.Value : turno.Diagnostico),
                new SqlParameter("@Legajo", turno.Legajo),
                new SqlParameter("@Fecha", turno.FechaPactada)
            };
            return conexion.EjecutarComandoConParametros(query, parametros);
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
            string query = @"
            SELECT * FROM vw_TurnosConDatos 
            WHERE Legajo = @Legajo 
            AND (@Apellido IS NULL OR ApellidoPaciente 
            COLLATE Latin1_General_CI_AI LIKE '%' + @Apellido + '%') 
            ORDER BY fechaPactada, Legajo, DNIPaciente";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25) { Value = legajo });
                    cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 25) { Value = apellido });
                    using (SqlDataReader reader = cmd.ExecuteReader()) { while (reader.Read()) { turnos.Add(MapearTurno(reader)); } }
                }
            }
            catch (Exception ex) { throw new Exception("Error al cargar turnos por apellido: " + ex.Message); }
            return turnos;
        }
        public List<Turno> FiltrarPacientexDNI(string legajo, string dniPaciente)
        {
            List<Turno> turnos = new List<Turno>();
            string query = @"SELECT * FROM vw_TurnosConDatos 
                            WHERE Legajo = @Legajo 
                            AND (@DNI IS NULL OR DNIPaciente LIKE '%' + @DNI + '%')
                            ORDER BY fechaPactada, Legajo, DNIPaciente";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25) { Value = legajo });
                    cmd.Parameters.Add(new SqlParameter("@DNI", SqlDbType.VarChar, 25) { Value = dniPaciente });
                    using (SqlDataReader reader = cmd.ExecuteReader()) { while (reader.Read()) { turnos.Add(MapearTurno(reader)); } }
                }
            }
            catch (Exception ex) { throw new Exception("Error al cargar turnos por DNI: " + ex.Message); }
            return turnos;
        }
        public DataTable ObtenerMedicosPorEspecialidad(int idEspecialidad)
        {
            string query = @"SELECT M.Legajo, P.nombre + ' ' + P.apellido AS NombreCompleto FROM Medico M INNER JOIN Persona P ON M.DNI = P.DNI WHERE M.idEspecialidad = @idEspecialidad";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idEspecialidad", idEspecialidad)
            };
            return conexion.EjecutarConsultaConParametros(query, parametros);
        }


        public DataTable ObtenerHorasDisponibles(string legajo, DateTime fecha)
        {
            string query = @"
                SELECT J.rangoHorario
                FROM Jornadas J
                LEFT JOIN Turnos T ON J.Legajo = T.Legajo
                AND CAST(T.FechaPactada AS DATE) = CAST(@Fecha AS DATE)    
                AND CONVERT(TIME, T.FechaPactada) = J.rangoHorario
                WHERE J.Legajo = @Legajo
                AND J.DiaSemana = FORMAT(@Fecha, 'dddd', 'es-AR')
                AND T.Legajo IS NULL
                ORDER BY J.rangoHorario
                
                ";
            SqlParameter[] parameteros = new SqlParameter[]
            {
                new SqlParameter("@Legajo", legajo),
                new SqlParameter("@Fecha", fecha.Date)
            };

            return conexion.EjecutarConsultaConParametros(query, parameteros);
        }
        public List<DateTime> ObtenerFechasDisponibles(string legajo, DateTime desde, DateTime hasta)
        {
            List<DateTime> fechas = new List<DateTime>();
            DateTime dia = desde.Date;

            while (dia <= hasta.Date)
            {
                DataTable dtHoras = ObtenerHorasDisponibles(legajo, dia);
                if (dtHoras.Rows.Count > 0)
                {
                    if (dia > DateTime.Today)
                    {
                        fechas.Add(dia);
                    }
                    else if (dia == DateTime.Today)
                    {
                        TimeSpan horaActual = DateTime.Now.TimeOfDay;

                        IEnumerable<DataRow> filasValidas = dtHoras.AsEnumerable()
                        .Where(row =>
                        {
                            TimeSpan horaTurno = TimeSpan.Parse(row["RangoHorario"].ToString());
                            return horaTurno > horaActual;
                        });
                        if (filasValidas.Any()) { fechas.Add(dia); }
                    }
                }
                dia = dia.AddDays(1);
            }
            return fechas;
        }


        public int InsertarTurno(Turno turno)
        {
            string query = @"INSERT INTO Turnos (Legajo, DNIPaciente, fechaPactada, estado, observacion, diagnostico)
                     VALUES (@Legajo, @DNI, @Fecha, @Estado, @Obs, @Diag)";

            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@Legajo", turno.Legajo),
        new SqlParameter("@DNI", turno.DNIPaciente),
        new SqlParameter("@Fecha", turno.FechaPactada),
        new SqlParameter("@Estado", turno.Estado = 3 ),
        new SqlParameter("@Obs", turno.Observacion ?? ""),
        new SqlParameter("@Diag", turno.Diagnostico ?? "")
            };

            return conexion.EjecutarComandoConParametros(query, parametros);
        }



        public int EliminarTurno(string legajo, DateTime fechapactada)
        {
            string query = @"UPDATE Turnos SET estado = @Estado WHERE Legajo = @Legajo AND fechaPactada = @Fecha";

            SqlParameter[] parametros = new SqlParameter[]
            {

                new SqlParameter("@Legajo", legajo),
                new SqlParameter("@Fecha", fechapactada),
                new SqlParameter("@Estado", 2)

            };

            return conexion.EjecutarComandoConParametros(query, parametros);

        }

        //Sobrecargo el metodo de GetTurnosMedico, para que me traiga los turnos ordenados segun el legajo, sin importar la fecha

        public List<Turno> GetTurnosOrdX(string query)
        {
            var turnos = new List<Turno>();


            using (SqlConnection connection = conexion.AbrirConexion())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        turnos.Add(MapearTurno(reader));
                    }
                }
            }

            return turnos;
        }
    }
}
