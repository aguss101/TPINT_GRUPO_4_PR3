using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;


namespace Datos
{
    public class ConsultasUsuario
    {
        private DataAccess conexion = new DataAccess();

        public List<Usuario> getUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            

            string query = "SELECT * FROM Usuario";
            using (SqlConnection connection = conexion.AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario();

                            usuario.DNI = (reader["DNI"].ToString());
                            usuario.idRol = (Convert.ToInt32(reader["idRol"]));
                            usuario.NombreUsuario = (reader["nombreUsuario"].ToString());
                            usuario.contrasenia = (reader["contrasenia"].ToString());
                            usuario.ultimoIngreso = (Convert.ToDateTime(reader["ultimoIngreso"]));
                            usuario.alta = (Convert.ToDateTime(reader["alta"]));

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }
    }
}
