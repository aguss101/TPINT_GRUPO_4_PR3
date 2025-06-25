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
