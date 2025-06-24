using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
            string query = "SELECT PA.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, PE.Direccion, S.idSexo, O.idObraSocial, C.correo, T.telefono, L.idLocalidad FROM Paciente PA" +
                " INNER JOIN Persona PE ON PA.DNI = PE.DNI INNER JOIN Sexos S ON PE.sexo = S.idSexo INNER JOIN ObraSocial O ON PA.ObraSocial = O.idObraSocial " +
                "INNER JOIN Localidades L ON PE.idLocalidad = L.idLocalidad  " + " LEFT JOIN Correos C ON PE.DNI = C.idPersona  LEFT JOIN Telefonos T ON PE.DNI = T.idPersona " + 
                "WHERE activo = 1";
            using (SqlConnection connection = conexion.AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Paciente paciente = new Paciente();

                            paciente.DNI = (reader["DNI"].ToString());
                            paciente.ObraSocial = (Convert.ToInt32(reader["idObraSocial"]));
                            paciente.nombre = (reader["nombre"].ToString());
                            paciente.apellido = (reader["apellido"].ToString());
                            paciente.ultimaAtencion = (DateTime)reader["ultimaAtencion"];
                            paciente.Alta = (DateTime)reader["alta"];
                            paciente.genero = (Convert.ToInt32(reader["idSexo"]));
                            paciente.fechaNacimiento = (DateTime)reader["fechaNacimiento"];
                            paciente.Direccion = (reader["Direccion"].ToString());
                            paciente.Localidad = (Convert.ToInt32(reader["idLocalidad"]));
                            paciente.nacionalidad = (reader["nacionalidad"].ToString());
                            paciente.Correo = (reader["Correo"].ToString());
                            paciente.Telefono = (reader["telefono"].ToString());

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

        public int EliminarPaciente(string nombreProcedimiento, string dni)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dni)
            };
            return conexion.EjecutarProcedimientoAlmacenado(nombreProcedimiento, parametros);
        }

        public int ModificarPaciente(string nombreProcedimiento, Paciente paciente, string DNI_VIEJO)
        {
            Debug.Print("DNI_VIEJO: " + DNI_VIEJO);
            Debug.Print("DNI_NUEVO: " + paciente.DNI);
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI_NUEVO", paciente.DNI),
                new SqlParameter("@DNI_VIEJO", DNI_VIEJO),
                new SqlParameter("@Nombre", paciente.nombre),
                new SqlParameter("@Apellido", paciente.apellido),
                new SqlParameter("@Nacionalidad", paciente.nacionalidad),
                new SqlParameter("@FechaNacimiento", paciente.fechaNacimiento),
                new SqlParameter("@Sexo", paciente.genero),
                new SqlParameter("@IdLocalidad", paciente.Localidad),
                new SqlParameter("@ObraSocial", paciente.ObraSocial),
                new SqlParameter("@Telefono", paciente.Telefono),
                new SqlParameter("@Direccion", paciente.Direccion),
                new SqlParameter("@Correo", paciente.Correo)
            };
            return conexion.EjecutarProcedimientoAlmacenado(nombreProcedimiento, parametros);
        }

        public Paciente getPacientePorID(string idPaciente)
        {
            Paciente paciente = null;

            

            using (SqlConnection connection = conexion.AbrirConexion())
            {
                string query = "SELECT PA.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, PE.Direccion, S.idSexo, O.idObraSocial, C.correo, T.telefono, L.idLocalidad FROM Paciente PA" +
                " INNER JOIN Persona PE ON PA.DNI = PE.DNI INNER JOIN Sexos S ON PE.sexo = S.idSexo INNER JOIN ObraSocial O ON PA.ObraSocial = O.idObraSocial " +
                "INNER JOIN Localidades L ON PE.idLocalidad = L.idLocalidad  " + " LEFT JOIN Correos C ON PE.DNI = C.idPersona  LEFT JOIN Telefonos T ON PE.DNI = T.idPersona " +
                "WHERE activo = 1 AND PE.DNI = @id ";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idPaciente);

               
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    paciente = new Paciente();


                    paciente.DNI = (reader["DNI"].ToString());
                    paciente.ObraSocial = (Convert.ToInt32(reader["idObraSocial"]));
                    paciente.nombre = (reader["nombre"].ToString());
                    paciente.apellido = (reader["apellido"].ToString());
                    paciente.ultimaAtencion = (DateTime)reader["ultimaAtencion"];
                    paciente.Alta = (DateTime)reader["alta"];
                    paciente.genero = (Convert.ToInt32(reader["idSexo"]));
                    paciente.fechaNacimiento = (DateTime)reader["fechaNacimiento"];
                    paciente.Direccion = reader["direccion"].ToString();
                    paciente.Localidad = (Convert.ToInt32(reader["idLocalidad"]));
                    paciente.nacionalidad = (reader["nacionalidad"].ToString());
                    paciente.Correo = (reader["Correo"].ToString());
                    paciente.Telefono = (reader["Telefono"].ToString());

                }
                reader.Close();
            }

            return paciente;
        }


    }
}
