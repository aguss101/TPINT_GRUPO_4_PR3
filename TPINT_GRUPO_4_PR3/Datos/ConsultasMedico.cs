using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class ConsultasMedico
    {
        private DataAccess conexion = new DataAccess();
        public List<Medico> GetMedicos()
        {
            List<Medico> medicos = new List<Medico>();
            string query = "SELECT ME.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, PE.Direccion, S.idSexo, C.correo, T.telefono, L.idLocalidad " +
                "FROM Medico ME " + "INNER JOIN Persona PE ON ME.DNI = PE.DNI INNER JOIN Sexos S ON PE.sexo = S.idSexo " + "INNER JOIN Localidades L ON PE.idLocalidad " +
                "= L.idLocalidad" + "  LEFT JOIN Correos C ON PE.DNI = C.idPersona  LEFT JOIN Telefonos T ON PE.DNI = T.idPersona ";

            using (SqlConnection connection = conexion.AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Medico medico = new Medico();

                            medico.Legajo = (reader["Legajo"].ToString());
                            medico.DNI = (reader["DNI"].ToString());
                            medico.idEspecialidad = (Convert.ToInt32(reader["idEspecialidad"]));
                            medico.nombre = (reader["nombre"].ToString());
                            medico.apellido = (reader["apellido"].ToString());
                            medico.genero = (Convert.ToInt32(reader["idSexo"]));
                            medico.fechaNacimiento = (DateTime)reader["FechaNacimiento"];
                            medico.nacionalidad = (reader["nacionalidad"].ToString());
                            medico.Direccion = (reader["Direccion"].ToString());
                            medico.Localidad = (Convert.ToInt32(reader["idLocalidad"]));
                            medico.Correo = (reader["Correo"].ToString());
                            medico.Telefono = (Convert.ToInt32(reader["telefono"]));

                            medicos.Add(medico);
                        }
                    }
                }
            }
            return medicos;

        }

        public int InsertarMedico(string nombreprocedimiento, Medico medico)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@Legajo", medico.Legajo),
        new SqlParameter("@DNI", medico.DNI),
        new SqlParameter("@idEspecialidad", medico.idEspecialidad),
        new SqlParameter("@Nombre", medico.nombre),
        new SqlParameter("@Apellido", medico.apellido),
        new SqlParameter("@Nacionalidad", medico.nacionalidad),
        new SqlParameter("@FechaNacimiento", medico.fechaNacimiento),
        new SqlParameter("@Sexo", medico.genero),
        new SqlParameter("@IdLocalidad", medico.Localidad),
        new SqlParameter("@Telefono", medico.Telefono),
        new SqlParameter("@Direccion", medico.Direccion),
        new SqlParameter("@Correo", medico.Correo)
            };


            return conexion.EjecutarProcedimientoAlmacenado(nombreprocedimiento, parametros);
        }

        public int EliminarMedico(string nombreProcedimiento, string dni)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dni)
            };
            return conexion.EjecutarProcedimientoAlmacenado(nombreProcedimiento, parametros);
        }

        public int ModificarMedico(string nombreProcedimiento, Medico medico)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Legajo", medico.DNI),
                new SqlParameter("@DNI", medico.DNI),
                new SqlParameter("@idEspecialidad", medico.idEspecialidad),
                new SqlParameter("@Nombre", medico.nombre),
                new SqlParameter("@Apellido", medico.apellido),
                new SqlParameter("@Nacionalidad", medico.nacionalidad),
                new SqlParameter("@FechaNacimiento", medico.fechaNacimiento),
                new SqlParameter("@Sexo", medico.genero),
                new SqlParameter("@IdLocalidad", medico.Localidad),
                new SqlParameter("@Telefono", medico.Telefono),
                new SqlParameter("@Direccion", medico.Direccion),
                new SqlParameter("@Correo", medico.Correo)
            };
            return conexion.EjecutarProcedimientoAlmacenado(nombreProcedimiento, parametros);
        }

        public Medico getMedicoPorID(string idMedico)
        {
            Medico medico = null; // null si no se encuentra



            using (SqlConnection connection = conexion.AbrirConexion())
            {
                string query = "SELECT ME.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, PE.Direccion, S.idSexo, C.correo, T.telefono, L.idLocalidad " +
                "FROM Medico ME " + "INNER JOIN Persona PE ON ME.DNI = ME.DNI INNER JOIN Sexos S ON PE.sexo = S.idSexo " + "INNER JOIN Localidades L ON PE.idLocalidad " +
                "= L.idLocalidad" + "  INNER JOIN Correos C ON PE.DNI = C.idPersona  INNER JOIN Telefonos T ON PE.DNI = T.idPersona";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idMedico);


                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    medico = new Medico();


                    medico.DNI = (reader["DNI"].ToString());
                    medico.nombre = (reader["nombre"].ToString());
                    medico.apellido = (reader["apellido"].ToString());
                    medico.genero = (Convert.ToInt32(reader["idSexo"]));
                    medico.fechaNacimiento = (DateTime)reader["FechaNacimiento"];
                    medico.Direccion = (reader["Direccion"].ToString());
                    medico.Localidad = (Convert.ToInt32(reader["idLocalidad"]));
                    medico.nacionalidad = (reader["nacionalidad"].ToString());
                    medico.Correo = (reader["Correo"].ToString());
                    medico.Telefono = (Convert.ToInt32(reader["Telefono"]));

                }

                reader.Close();
            }

            return medico;
        }
    }
}