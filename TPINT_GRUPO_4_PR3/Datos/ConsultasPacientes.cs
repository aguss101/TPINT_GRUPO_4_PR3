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
                            paciente.ObraSocial = (Convert.ToInt32(reader["Nombre"]));
                            paciente.nombre = (reader["nombre"].ToString());
                            paciente.apellido = (reader["apellido"].ToString());
                            paciente.ultimaAtencion = (DateTime)reader["ultimaAtencion"];
                            paciente.Alta = (DateTime)reader["alta"];
                            paciente.genero = (Convert.ToInt32(reader["Descripcion"]));
                            paciente.fechaNacimiento = (DateTime)reader["FechaNacimiento"];
                            paciente.Localidad = (Convert.ToInt32(reader["nombreLocalidad"]));
                            paciente.nacionalidad = (reader["nacionalidad"].ToString());

                            pacientes.Add(paciente);
                        }
                    }
                }
            }
            return pacientes;

        }

        public int InsertarPaciente(string nombreprocedimiento, Paciente paciente)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@DNI", paciente.DNI),
        new SqlParameter("@Nombre", paciente.nombre),
        new SqlParameter("@Apellido", paciente.apellido),
        new SqlParameter("@Nacionalidad", paciente.nacionalidad),
        new SqlParameter("@FechaNacimiento", paciente.fechaNacimiento),
        new SqlParameter("@Sexo", paciente.genero),
        new SqlParameter("@IdLocalidad", paciente.Localidad),
        new SqlParameter("@ObraSocial", paciente.ObraSocial),
        new SqlParameter("@UltimaAtencion", paciente.ultimaAtencion),
        new SqlParameter("@Alta", paciente.Alta),
        new SqlParameter("@Telefono", paciente.Telefono),
        new SqlParameter("@Direccion", paciente.Direccion),
        new SqlParameter("@Correo", paciente.Correo)
            };

           
            return conexion.EjecutarProcedimientoAlmacenado(nombreprocedimiento, parametros);
        }

        public int EliminarPaciente(string nombreProcedimiento, int dni)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dni)
            };
            return conexion.EjecutarProcedimientoAlmacenado(nombreProcedimiento, parametros);
        }

        public int ModificarPaciente(string nombreProcedimiento, Paciente paciente)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", paciente.DNI),
                new SqlParameter("@Nombre", paciente.nombre),
                new SqlParameter("@Apellido", paciente.apellido),
                new SqlParameter("@Nacionalidad", paciente.nacionalidad),
                new SqlParameter("@FechaNacimiento", paciente.fechaNacimiento),
                new SqlParameter("@Sexo", paciente.genero),
                new SqlParameter("@IdLocalidad", paciente.Localidad),
                new SqlParameter("@ObraSocial", paciente.ObraSocial),
                new SqlParameter("@UltimaAtencion", paciente.ultimaAtencion),
                new SqlParameter("@Alta", paciente.Alta),
                new SqlParameter("@Telefono", paciente.Telefono),
                new SqlParameter("@Direccion", paciente.Direccion),
                new SqlParameter("@Correo", paciente.Correo)
            };
            return conexion.EjecutarProcedimientoAlmacenado(nombreProcedimiento, parametros);
        }


    }
}
