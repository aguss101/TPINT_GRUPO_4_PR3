using System;
using System.Collections.Generic;
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
            string query = @"SELECT U.DNI,U.IdRol,U.nombreUsuario,U.contrasenia,U.ultimoIngreso, U.alta FROM Usuario U";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                DNI = reader["DNI"].ToString(),
                                idRol = Convert.ToInt32(reader["IdRol"]),
                                NombreUsuario = reader["nombreUsuario"].ToString(),
                                contrasenia = reader["contrasenia"].ToString(),
                                ultimoIngreso = Convert.ToDateTime(reader["ultimoIngreso"]),
                                alta = Convert.ToDateTime(reader["alta"])
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Error al cargar usuarios: " + ex.Message); }
            return usuarios;
        }
    }
}