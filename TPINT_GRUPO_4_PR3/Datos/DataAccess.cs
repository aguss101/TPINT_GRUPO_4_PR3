using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DataAccess
    {
        private string connectionString = "Data Source=DESKTOP-KCSRHEU\\SQLEXPRESS;Initial Catalog=ClinicaDB;Integrated Security=True;Encrypt=False";

        public SqlConnection AbrirConexion()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al abrir la conexión: " + ex.Message);
            }
            return connection;
        }

        public SqlConnection CerrarConexion()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cerrar la conexión: " + ex.Message);
            }
            return connection;
        }

        public DataTable EjecutarConsulta(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        try
                        {
                            adapter.Fill(dataTable);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error al ejecutar la consulta: " + ex.Message);
                        }
                    }
                }
            }
            return dataTable;
        }

        public int EjecutarComando(string query)
        {
            int filasAfectadas = 0;
            using (SqlConnection connection = AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        filasAfectadas = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al ejecutar el comando: " + ex.Message);
                    }
                }
            }
            return filasAfectadas;
        }


        public int EjecutarComandoConParametros(string query, SqlParameter[] parametros)
        {
            int filasAfectadas = 0;
            using (SqlConnection connection = AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros);
                    }
                    try
                    {
                        filasAfectadas = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al ejecutar el comando con parámetros: " + ex.Message);
                    }
                }
            }
            return filasAfectadas;
        }

        public int EjecutarProcedimientoAlmacenado(string nombreProcedimiento, SqlParameter[] parametros)
        {
            int filasAfectadas = 0;
            using (SqlConnection connection = AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand(nombreProcedimiento, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros);
                    }
                    try
                    {
                        filasAfectadas = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }
            return filasAfectadas;
        }



    }
}
