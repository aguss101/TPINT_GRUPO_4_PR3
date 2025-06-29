using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class ConsultasTurnos
    {
        private DataAccess conexion = new DataAccess();
        public List<Turno> GetTurnosAdmin()
        {
            List<Turno> turnos = new List<Turno>();
            string query = "SELECT * FROM vw_TurnosConDatos ORDER BY FechaPactada,Legajo,DNIPaciente";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) { turnos.Add(MapearTurno(reader)); }
            }
            catch (Exception ex) { throw new Exception("Error al cargar turnos: " + ex.Message); }
            return turnos;
        }
        public List<Turno> GetTurnosMedico(string legajo, DateTime? fechaSelected)
        {
            List<Turno> turnos = new List<Turno>();
            string query = "SELECT * FROM vw_TurnosConDatos WHERE @Legajo = Legajo AND (@Fecha IS NULL OR CONVERT(date, fechaPactada) = CONVERT(date, @Fecha)) ORDER BY fechaPactada,Legajo,DNIPaciente";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                command.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25) { Value = legajo });
                command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime) { Value = fechaSelected.HasValue ? (object)fechaSelected.Value.Date : DBNull.Value});
                while (reader.Read()) { turnos.Add(MapearTurno(reader)); }
            }
            catch (Exception ex) { throw new Exception("Error al cargar turnos: " + ex.Message); }
            return turnos;
        }
        public int MarcarAsistenciaTurno(Turno turno)
        {
            string query = "UPDATE Turnos SET estado=@Estado, observacion=@Observacion, diagnostico=@Diagnostico WHERE Legajo=@Legajo AND fechaPactada=@Fecha";
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
            string query = "SELECT * FROM vw_TurnosConDatos WHERE Legajo = @Legajo AND (@Apellido IS NULL OR ApellidoPaciente COLLATE Latin1_General_CI_AI LIKE '%' + @Apellido + '%') ORDER BY fechaPactada, Legajo, DNIPaciente";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                command.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25) { Value = legajo });
                command.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 25) { Value = legajo });
                while (reader.Read()) { turnos.Add(MapearTurno(reader)); }
            }
            catch (Exception ex) { throw new Exception("Error al cargar turnos por apellido: " + ex.Message); }
            return turnos;
        }
        public List<Turno> FiltrarPacientexDNI(string legajo, string dniPaciente)
        {
            List<Turno> turnos = new List<Turno>();
            string query = "SELECT * FROM vw_TurnosConDatos WHERE Legajo = @Legajo AND (@DNI IS NULL OR DNIPaciente LIKE '%' + @DNI + '%') ORDER BY fechaPactada, Legajo, DNIPaciente";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                command.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25) { Value = legajo });
                command.Parameters.Add(new SqlParameter("@DNI", SqlDbType.VarChar, 25) { Value = legajo });
                while (reader.Read()) { turnos.Add(MapearTurno(reader)); }
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


        public DataTable ObtenerFechasDisponibles(string legajo, DateTime fecha)
        {
            string query = @"
                SELECT J.rangoHorario
                FROM Jornadas J
                LEFT JOIN Turnos T ON J.Legajo = T.Legajo
                AND CAST(T.FechaPactada AS DATE) = CAST(@Fecha AS DATE)               
                WHERE J.Legajo = @Legajo
                AND J.DiaSemana = DATENAME(WEEKDAY, @Fecha)
                
                ";
            SqlParameter[] parameteros = new SqlParameter[] {
            new SqlParameter("@Legajo", legajo),
            new SqlParameter("@Fecha", fecha)
            };

            return conexion.EjecutarConsultaConParametros(query, parameteros);
        }

    }
}