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
            string query = "SELECT ME.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, S.Descripcion, L.nombreLocalidad FROM Medico ME INNER JOIN Persona PE ON ME.DNI = ME.DNI INNER JOIN Sexos S ON PE.sexo = S.IdSexo INNER JOIN Localidades L ON PE.IdLocalidad = L.IdLocalidad  ";
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
                            medico.genero = (Convert.ToInt32(reader["Descripcion"]));
                            medico.fechaNacimiento = (DateTime)reader["FechaNacimiento"];
                            medico.nacionalidad = (reader["nacionalidad"].ToString());
                            medico.Direccion = (reader["Direccion"].ToString());
                            medico.Localidad = (Convert.ToInt32(reader["nombreLocalidad"]));
                            medico.Correo = (reader["Correo"].ToString());
                            medico.Telefono = (Convert.ToInt32(reader["Telefono"]));

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

        public int EliminarMedico(string nombreProcedimiento, int dni)
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


    }
}