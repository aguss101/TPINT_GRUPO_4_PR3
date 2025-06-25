using System;
using System.Collections.Generic;
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

                using (SqlCommand command = new SqlCommand("sp_ListarTurnosAdmin", conection))
                {


                    command.CommandType = CommandType.StoredProcedure;

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

        public List<Turno> GetTurnosMedico(string legajo)
        {
            List<Turno> turnos = new List<Turno>();

            using (SqlConnection con = conexion.AbrirConexion())
            {

                using (SqlCommand cmd = new SqlCommand("sp_ListarTurnosMedico", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 25) // Le paso el legajo al procedimiento.
                    {
                        Value = legajo
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

        public int MarcarAsistenciaTurno(string nombreProcedimiento, Turno turno)
        {

            Debug.Print("Legajo: " + turno.Legajo);
            Debug.Print("FechaPactada: " + turno.FechaPactada.ToString("s"));
            Debug.Print("Estado: " + turno.Estado);
            Debug.Print("Observacion: " + turno.Observacion);


            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@Legajo",       turno.Legajo),
        new SqlParameter("@FechaPactada", turno.FechaPactada),
        new SqlParameter("@Estado",       turno.Estado),
        new SqlParameter(
            "@Observacion",
            string.IsNullOrWhiteSpace(turno.Observacion)  //  Esto es para que no se rompa todo si se le ocurre no poner nada lo settea en null
                ? (object)DBNull.Value
                : turno.Observacion
        )
            };


            return conexion.EjecutarProcedimientoAlmacenado(nombreProcedimiento, parametros);
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



    }

}
