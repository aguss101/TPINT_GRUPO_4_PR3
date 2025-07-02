using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DataAccess
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=ClinicaDB;Integrated Security=True;Encrypt=False";
        public SqlConnection AbrirConexion()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try { connection.Open(); }
            catch (Exception ex) { throw new Exception("Error al abrir la conexión: " + ex.Message); }
            return connection;
        }
        public void CerrarConexion(SqlConnection connection)
        {
            try { connection?.Close(); }
            catch (Exception ex) { throw new Exception("Error al cerrar la conexión: " + ex.Message); }
        }
        public DataTable EjecutarConsulta(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex) { throw new Exception("Error al ejecutar la consulta: " + ex.Message); }
            return dataTable;
        }
        public DataTable EjecutarConsultaConParametros(string query, SqlParameter[] parametros)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                if (parametros?.Length > 0)
                {
                    command.Parameters.AddRange(parametros);
                }
                adapter.Fill(dataTable);
            }
            catch (Exception ex) { throw new Exception("Error al ejecutar la consulta con parámetros: " + ex.Message); }
            return dataTable;
        }
        public int EjecutarComando(string query)
        {
            int filasAfectadas = 0;
            try
            {
                SqlConnection connection = AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                filasAfectadas = command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception("Error al ejecutar el comando: " + ex.Message); }
            return filasAfectadas;
        }
        public int EjecutarComandoConParametros(string query, SqlParameter[] parametros)
        {
            int filasAfectadas = 0;
            try
            {
                SqlConnection connection = AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                if (parametros?.Length > 0) { command.Parameters.AddRange(parametros); }
                filasAfectadas = command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception("Error al ejecutar el comando con parametros: " + ex.Message); }
            return filasAfectadas;
        }
        public int EjecutarProcedimientoAlmacenado(string nombreProcedimiento, SqlParameter[] parametros)
        {
            int filasAfectadas = 0;
            try
            {
                SqlConnection connection = AbrirConexion();
                SqlCommand command = new SqlCommand(nombreProcedimiento, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (parametros?.Length > 0) { command.Parameters.AddRange(parametros); }
                filasAfectadas = command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception("Error al ejecutar el procedimiento almacenado: " + ex.Message); }
            return filasAfectadas;
        }
    }
}
