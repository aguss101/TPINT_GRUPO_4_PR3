using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;



namespace Datos
{
    public class ConsultasPacientes
    {
        private DataAccess conexion = new DataAccess();

        public List<Paciente> GetPacientes()
        {
            List<Paciente> pacientes = new List<Paciente>();
            string query = "SELECT PA.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, S.Descripcion, O.Nombre, L.nombreLocalidad FROM Paciente PA INNER JOIN Persona PE ON PA.DNI = PE.DNI INNER JOIN Sexos S ON PE.sexo = S.IdSexo INNER JOIN ObraSocial O ON PA.ObraSocial = O.idObraSocial INNER JOIN Localidades L ON PE.IdLocalidad = L.IdLocalidad  ";
            using (SqlConnection connection = conexion.AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Paciente paciente = new Paciente();

                           paciente.DNI = (Convert.ToInt32(reader["DNI"]));
                            paciente.ObraSocial = (reader["Nombre"].ToString());
                            paciente.nombre = (reader["nombre"].ToString());
                            paciente.apellido = (reader["apellido"].ToString());
                            paciente.ultimaAtencion = (DateTime)reader["ultimaAtencion"];
                            paciente.Alta = (DateTime)reader["alta"];
                            paciente.genero = (reader["descripcion"].ToString());
                            paciente.fechaNacimiento = (DateTime)reader["FechaNacimiento"];
                            paciente.Localidad = (reader["nombreLocalidad"].ToString());
                            paciente.nacionalidad = (reader["nacionalidad"].ToString());

                            pacientes.Add(paciente);
                        }
                    }
                }
            }
            return pacientes;

        }
    }
}
