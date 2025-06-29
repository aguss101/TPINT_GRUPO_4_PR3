using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class ConsultasMedico
    {
        private DataAccess conexion = new DataAccess();
        public List<Medico> GetMedicos()
        {
            List<Medico> medicos = new List<Medico>();
            string query = "SELECT ME.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, PE.Direccion, S.idSexo, C.correo, T.telefono, L.idLocalidad," +
                "U.nombreUsuario, U.contrasenia FROM Medico ME " + " INNER JOIN Persona PE ON ME.DNI = PE.DNI INNER JOIN Sexos S ON PE.sexo = S.idSexo " + 
                "INNER JOIN Localidades L ON PE.idLocalidad = L.idLocalidad" + "  LEFT JOIN Correos C ON PE.DNI = C.idPersona  LEFT JOIN Telefonos T ON PE.DNI = " +
                "T.idPersona  INNER JOIN Usuario U ON PE.DNI = U.DNI" +
                " WHERE activo = 1 " ;
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Medico medico = new Medico()
                    {
                        Usuario = new Usuario()
                        {
                            NombreUsuario = reader["nombreUsuario"].ToString(),
                            contrasenia = reader["contrasenia"].ToString()
                        },
                        Legajo = reader["Legajo"].ToString(),
                        DNI = reader["DNI"].ToString(),
                        idEspecialidad = Convert.ToInt32(reader["idEspecialidad"]),
                        nombre = reader["nombre"].ToString(),
                        apellido = reader["apellido"].ToString(),
                        genero = Convert.ToInt32(reader["idSexo"]),
                        fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        Direccion = reader["Direccion"].ToString(),
                        Localidad = Convert.ToInt32(reader["idLocalidad"]),
                        nacionalidad = reader["nacionalidad"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["telefono"].ToString()
                    };
                    medicos.Add(medico);
                }
            }catch(Exception ex) { throw new Exception("Error al buscar medicos: " + ex.Message); }
            return medicos;

        }
        public int InsertarMedico(Medico medico, Usuario usuario)
        {
            string queryPersona = @"INSERT INTO Persona (DNI,nombre,apellido,sexo,direccion,idLocalidad,fechaNacimiento,nacionalidad) VALUES (@DNI,@Nombre,@Apellido,@Sexo,@Direccion,@IdLocalidad,@FechaNacimiento,@Nacionalidad)";
            string queryMedico = @"INSERT INTO Medico (DNI, Legajo, idEspecialidad) VALUES (@DNI,@Legajo,@idEspecialidad)";
            string queryUsuario = @"INSERT INTO Usuario (DNI,nombreUsuario,idRol,contrasenia,alta,ultimoIngreso) VALUES (@DNI,@Usuario,@IdRol,@Contrasenia,@Alta,@UltimoIngreso)";
            string queryTelefono = @"INSERT INTO Telefonos (idPersona,telefono) VALUES (@DNI,@Telefono)";
            string queryCorreo = @"INSERT INTO Correos (idPersona,correo) VALUES (@DNI,@Correo)";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand(queryPersona, connection, transaction);
                command.Parameters.AddWithValue("@DNI", medico.DNI);
                command.Parameters.AddWithValue("@Nombre", medico.nombre);
                command.Parameters.AddWithValue("@Apellido", medico.apellido);
                command.Parameters.AddWithValue("@Sexo", medico.genero);
                command.Parameters.AddWithValue("@Direccion", medico.Direccion);
                command.Parameters.AddWithValue("@IdLocalidad", medico.Localidad);
                command.Parameters.AddWithValue("@FechaNacimiento", medico.fechaNacimiento);
                command.Parameters.AddWithValue("@Nacionalidad", medico.nacionalidad);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryMedico, connection, transaction);
                command.Parameters.AddWithValue("@DNI", medico.DNI);
                command.Parameters.AddWithValue("@Legajo", medico.Legajo);
                command.Parameters.AddWithValue("@idEspecialidad", medico.idEspecialidad);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryUsuario, connection, transaction);
                command.Parameters.AddWithValue("@DNI", medico.DNI);
                command.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@IdRol", usuario.idRol);
                command.Parameters.AddWithValue("@Contrasenia", usuario.contrasenia);
                command.Parameters.AddWithValue("@Alta", usuario.alta);
                command.Parameters.AddWithValue("@UltimoIngreso", usuario.ultimoIngreso);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryTelefono, connection, transaction);
                command.Parameters.AddWithValue("@DNI", medico.DNI);
                command.Parameters.AddWithValue("@Telefono", medico.Telefono);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryCorreo, connection, transaction);
                command.Parameters.AddWithValue("@DNI", medico.DNI);
                command.Parameters.AddWithValue("@Correo", medico.Correo);
                command.ExecuteNonQuery();
                try { transaction.Commit(); return 1; }catch(Exception ex) { transaction.Rollback(); }
                
            } catch (Exception ex) { throw new Exception("Error al insertar medico: " + ex.Message); }
            return 0;
        }
        public int EliminarMedico(string dni)
        {
            string query = "UPDATE Persona SET activo = 0 WHERE DNI = @DNI";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dni)
            };
            return conexion.EjecutarComandoConParametros(query, parametros);
        }
        public int ModificarMedico(Medico medico, Usuario usuario, string DNI_VIEJO, string LEGAJO_VIEJO)
        {
            string queryUsuario = @"UPDATE Usuario SET DNI=@DNI_NUEVO,nombreUsuario=@Usuario,contrasenia=@Contrasenia WHERE DNI=@DNI";
            string queryCorreo = @"UPDATE Correos SET idPersona=@DNI_NUEVO, correo=@Correo WHERE idPersona=@DNI";
            string queryTelefono = @"UPDATE Telefonos SET idPersona=@DNI_NUEVO, telefono=@Telefono WHERE idPersona=@DNI";
            string queryJornadas = "UPDATE Jornadas SET Legajo=@LegajoNuevo WHERE Legajo=@Legajo";
            string queryTurnos = "UPDATE Turnos SET Legajo=@LegajoNuevo WHERE Legajo=@Legajo";
            string queryPersona = @"UPDATE Persona SET DNI=@DNI_NUEVO,nombre=@Nombre,apellido=@Apellido,sexo=@Sexo,direccion=@Direccion,idLocalidad=@IdLocalidad,fechaNacimiento=@FechaNacimiento,nacionalidad=@Nacionalidad WHERE DNI=@DNI";
            string queryMedico = @"UPDATE Medico SET DNI=@DNI_NUEVO,idEspecialidad=@idEspecialidad,Legajo=@LegajoNuevo WHERE DNI=@DNI";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand(queryUsuario, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                command.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Contrasenia", usuario.contrasenia);
                command.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryCorreo, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                command.Parameters.AddWithValue("@Correo", medico.Correo);
                command.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryTelefono, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                command.Parameters.AddWithValue("@Telefono", medico.Telefono);
                command.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryJornadas, connection, transaction);
                command.Parameters.AddWithValue("@LegajoNuevo", medico.Legajo);
                command.Parameters.AddWithValue("@Legajo", LEGAJO_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryTurnos, connection, transaction);
                command.Parameters.AddWithValue("@LegajoNuevo", medico.Legajo);
                command.Parameters.AddWithValue("@Legajo", LEGAJO_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryPersona, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                command.Parameters.AddWithValue("@Nombre", medico.nombre);
                command.Parameters.AddWithValue("@Apellido", medico.apellido);
                command.Parameters.AddWithValue("@Sexo", medico.genero);
                command.Parameters.AddWithValue("@Direccion", medico.Direccion);
                command.Parameters.AddWithValue("@IdLocalidad", medico.Localidad);
                command.Parameters.AddWithValue("@FechaNacimiento", medico.fechaNacimiento);
                command.Parameters.AddWithValue("@Nacionalidad", medico.nacionalidad);
                command.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                command.ExecuteNonQuery();
                command = new SqlCommand(queryMedico, connection, transaction);
                command.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                command.Parameters.AddWithValue("@idEspecialidad", medico.idEspecialidad);
                command.Parameters.AddWithValue("@LegajoNuevo", medico.Legajo);
                command.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                command.ExecuteNonQuery();
                try { transaction.Commit();return 1; }catch(Exception ex) { transaction.Rollback(); }
            }catch(Exception ex) { throw new Exception("Error al modificar medico: " + ex.Message); }
            return 0;
        }
        public Medico getMedicoPorID(string idMedico)
        {
            Medico medico = null;
            string query = "SELECT ME.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, PE.Direccion, S.idSexo, C.correo, T.telefono, L.idLocalidad, " +
                "U.nombreUsuario, U.contrasenia FROM Medico ME " + "INNER JOIN Persona PE ON ME.DNI = PE.DNI INNER JOIN Sexos S ON PE.sexo = S.idSexo " +
                "INNER JOIN Localidades L ON PE.idLocalidad = L.idLocalidad" + "  LEFT JOIN Correos C ON PE.DNI = C.idPersona  LEFT JOIN Telefonos T ON PE.DNI = " +
                " T.idPersona INNER JOIN Usuario U ON PE.DNI = U.DNI WHERE ME.DNI = @id";
            try
            {
                SqlConnection connection = conexion.AbrirConexion();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", idMedico);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    medico = new Medico()
                    {
                        Usuario = new Usuario()
                        {
                            NombreUsuario = reader["nombreUsuario"].ToString(),
                            contrasenia = reader["contrasenia"].ToString()
                        },
                        Legajo = reader["Legajo"].ToString(),
                        DNI = reader["DNI"].ToString(),
                        idEspecialidad = Convert.ToInt32(reader["idEspecialidad"]),
                        nombre = reader["nombre"].ToString(),
                        apellido = reader["apellido"].ToString(),
                        genero = Convert.ToInt32(reader["idSexo"]),
                        fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        Direccion = reader["Direccion"].ToString(),
                        Localidad = Convert.ToInt32(reader["idLocalidad"]),
                        nacionalidad = reader["nacionalidad"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    };
                }
            }catch(Exception ex) { throw new Exception("Error al buscar medico por ID: " + ex.Message); }
            return medico;
        }
    }
}