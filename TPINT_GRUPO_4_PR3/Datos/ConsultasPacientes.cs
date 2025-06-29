using Entidades;
using System;
using System.Collections.Generic;

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
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Paciente paciente = new Paciente
                    {
                        DNI = reader["DNI"].ToString(),
                        ObraSocial = Convert.ToInt32(reader["idObraSocial"]),
                        nombre = reader["nombre"].ToString(),
                        apellido = reader["apellido"].ToString(),
                        ultimaAtencion = Convert.ToDateTime(reader["ultimaAtencion"]),
                        Alta = Convert.ToDateTime(reader["alta"]),
                        genero = Convert.ToInt32(reader["idSexo"]),
                        fechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]),
                        Direccion = reader["Direccion"].ToString(),
                        Localidad = Convert.ToInt32(reader["idLocalidad"]),
                        nacionalidad = reader["nacionalidad"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["telefono"].ToString()
                    };
                    pacientes.Add(paciente);
                }
                
            } catch (Exception ex) { throw new Exception("Error al cargar usuarios: " + ex.Message); }
            return pacientes;
        public int InsertarPaciente(Paciente paciente)
        {
            string queryPersona = "INSERT INTO Persona (DNI,nombre,apellido,sexo,direccion,idLocalidad,fechaNacimiento,nacionalidad) VALUES(@DNI, @Nombre, @Apellido, @Sexo, @Direccion, @IdLocalidad, @FechaNacimiento, @Nacionalidad)";
            string queryPaciente = @"INSERT INTO Paciente (DNI,ObraSocial,ultimaAtencion,alta) VALUES (@DNI,@ObraSocial,@UltimaAtencion,@Alta)";
            string queryTelefono = @"INSERT INTO Telefonos (idPersona,telefono) VALUES (@DNI,@Telefono)";
            string queryCorreo = @"INSERT INTO Correos (idPersona,correo) VALUES (@DNI,@Correo)";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand(queryPersona, connection, transaction);
                command.Parameters.AddWithValue("@DNI", paciente.DNI);
                command.Parameters.AddWithValue("@Nombre", paciente.nombre);
                command.Parameters.AddWithValue("@Apellido", paciente.apellido);
                command.Parameters.AddWithValue("@Sexo", paciente.genero);
                command.Parameters.AddWithValue("@Direccion", paciente.Direccion);
                command.Parameters.AddWithValue("@IdLocalidad", paciente.Localidad);
                command.Parameters.AddWithValue("@FechaNacimiento", paciente.fechaNacimiento);
                command.Parameters.AddWithValue("@Nacionalidad", paciente.nacionalidad);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryPaciente, connection, transaction);
                command.Parameters.AddWithValue("@DNI", paciente.DNI);
                command.Parameters.AddWithValue("@ObraSocial", paciente.ObraSocial);
                command.Parameters.AddWithValue("@UltimaAtencion", paciente.ultimaAtencion);
                command.Parameters.AddWithValue("@Alta", paciente.Alta);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryTelefono, connection, transaction);
                command.Parameters.AddWithValue("@DNI", paciente.DNI);
                command.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryCorreo, connection, transaction);
                command.Parameters.AddWithValue("@DNI", paciente.DNI);
                command.Parameters.AddWithValue("@Correo", paciente.Correo);
                command.ExecuteNonQuery();
                try { transaction.Commit(); return 1; }catch(Exception ex) { transaction.Rollback(); }
            } catch (Exception ex) { throw new Exception("Error al insertar paciente: " + ex.Message); }
            return 0;
        }
        public int EliminarPaciente(string dni)
        {
            string query = "UPDATE Persona SET activo = 0 WHERE DNI = @DNI";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dni)
            };
            return conexion.EjecutarComandoConParametros(query, parametros);
        }
        public int ModificarPaciente(Paciente paciente, string DNI_VIEJO)
        {
            string queryPersona = @"UPDATE Persona SET DNI=@DNI_NUEVO,nombre=@Nombre,apellido=@Apellido,sexo=@Sexo,direccion=@Direccion,idLocalidad=@IdLocalidad,fechaNacimiento=@FechaNacimiento,nacionalidad=@Nacionalidad WHERE DNI=@DNI_VIEJO";
            string queryPaciente = @"UPDATE Paciente SET DNI=@DNI_NUEVO,ObraSocial=@ObraSocial WHERE DNI=@DNI_VIEJO";
            string queryTelefono = @"UPDATE Telefonos SET idPersona=@DNI_NUEVO, telefono=@Telefono WHERE idPersona=@DNI_VIEJO";
            string queryCorreo = @"UPDATE Correos SET idPersona=@DNI_NUEVO, correo=@Correo WHERE idPersona=@DNI_VIEJO";
            string queryTurnos = "UPDATE Turnos SET DNIPaciente=@DNI_NUEVO WHERE DNIPaciente=@DNI_VIEJO";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand(queryPersona, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                command.Parameters.AddWithValue("@Nombre", paciente.nombre);
                command.Parameters.AddWithValue("@Apellido", paciente.apellido);
                command.Parameters.AddWithValue("@Sexo", paciente.genero);
                command.Parameters.AddWithValue("@Direccion", paciente.Direccion);
                command.Parameters.AddWithValue("@IdLocalidad", paciente.Localidad);
                command.Parameters.AddWithValue("@FechaNacimiento", paciente.fechaNacimiento);
                command.Parameters.AddWithValue("@Nacionalidad", paciente.nacionalidad);
                command.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryPaciente, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                command.Parameters.AddWithValue("@ObraSocial", paciente.ObraSocial);
                command.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryTelefono, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                command.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                command.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryCorreo, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                command.Parameters.AddWithValue("@Correo", paciente.Correo);
                command.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryTurnos, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                command.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                command.ExecuteNonQuery();
                try { transaction.Commit();return 1; }catch(Exception ex) { transaction.Rollback(); }
            } catch (Exception ex) { throw new Exception("Error al modificar paciente: " + ex.Message); }
            return 0;
        }
        public Paciente getPacientePorID(string idPaciente)
        {
            Paciente paciente = null;
            string query = "SELECT PA.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, PE.Direccion, S.idSexo, O.idObraSocial, C.correo, T.telefono, L.idLocalidad FROM Paciente PA" +
            " INNER JOIN Persona PE ON PA.DNI = PE.DNI INNER JOIN Sexos S ON PE.sexo = S.idSexo INNER JOIN ObraSocial O ON PA.ObraSocial = O.idObraSocial " +
            "INNER JOIN Localidades L ON PE.idLocalidad = L.idLocalidad  " + " LEFT JOIN Correos C ON PE.DNI = C.idPersona  LEFT JOIN Telefonos T ON PE.DNI = T.idPersona " +
            "WHERE activo = 1 AND PE.DNI = @id ";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", idPaciente);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    paciente = new Paciente()
                    {
                        DNI = reader["DNI"].ToString(),
                        ObraSocial = Convert.ToInt32(reader["idObraSocial"]),
                        nombre = reader["nombre"].ToString(),
                        apellido = reader["apellido"].ToString(),
                        ultimaAtencion = Convert.ToDateTime(reader["ultimaAtencion"]),
                        Alta = Convert.ToDateTime(reader["alta"]),
                        genero = Convert.ToInt32(reader["idSexo"]),
                        fechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]),
                        Direccion = reader["direccion"].ToString(),
                        Localidad = Convert.ToInt32(reader["idLocalidad"]),
                        nacionalidad = reader["nacionalidad"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    };
                }
            }catch(Exception ex) { throw new Exception("Error al buscar paciente por ID: " + ex.Message); }
            return paciente;
        }
    }
}