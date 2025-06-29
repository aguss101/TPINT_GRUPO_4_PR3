using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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


        public int InsertarPaciente(Paciente paciente)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            using (SqlTransaction tx = conn.BeginTransaction())
            {
                try
                {
                    string qPersona = @"INSERT INTO Persona (DNI,nombre,apellido,sexo,direccion,idLocalidad,fechaNacimiento,nacionalidad)
                                        VALUES (@DNI,@Nombre,@Apellido,@Sexo,@Direccion,@IdLocalidad,@FechaNacimiento,@Nacionalidad)";
                    using (SqlCommand cmd = new SqlCommand(qPersona, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI", paciente.DNI);
                        cmd.Parameters.AddWithValue("@Nombre", paciente.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", paciente.apellido);
                        cmd.Parameters.AddWithValue("@Sexo", paciente.genero);
                        cmd.Parameters.AddWithValue("@Direccion", paciente.Direccion);
                        cmd.Parameters.AddWithValue("@IdLocalidad", paciente.Localidad);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", paciente.fechaNacimiento);
                        cmd.Parameters.AddWithValue("@Nacionalidad", paciente.nacionalidad);
                        cmd.ExecuteNonQuery();
                    }

                    string qPaciente = @"INSERT INTO Paciente (DNI,ObraSocial,ultimaAtencion,alta) VALUES (@DNI,@ObraSocial,@UltimaAtencion,@Alta)";
                    using (SqlCommand cmd = new SqlCommand(qPaciente, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI", paciente.DNI);
                        cmd.Parameters.AddWithValue("@ObraSocial", paciente.ObraSocial);
                        cmd.Parameters.AddWithValue("@UltimaAtencion", paciente.ultimaAtencion);
                        cmd.Parameters.AddWithValue("@Alta", paciente.Alta);
                        cmd.ExecuteNonQuery();
                    }

                    string qTel = @"INSERT INTO Telefonos (idPersona,telefono) VALUES (@DNI,@Telefono)";
                    using (SqlCommand cmd = new SqlCommand(qTel, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI", paciente.DNI);
                        cmd.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                        cmd.ExecuteNonQuery();
                    }

                    string qCorreo = @"INSERT INTO Correos (idPersona,correo) VALUES (@DNI,@Correo)";
                    using (SqlCommand cmd = new SqlCommand(qCorreo, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI", paciente.DNI);
                        cmd.Parameters.AddWithValue("@Correo", paciente.Correo);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return 1;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // Realiza la baja lógica de un paciente por DNI
        public int EliminarPaciente(string dni)
        {
            string q = "UPDATE Persona SET activo = 0 WHERE DNI = @DNI";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dni)
            };
            return conexion.EjecutarComandoConParametros(q, parametros);
        }

        // Modifica un paciente existente identificado por DNI_VIEJO
        public int ModificarPaciente(Paciente paciente, string DNI_VIEJO)
        {
            Debug.Print("DNI_VIEJO: " + DNI_VIEJO);
            Debug.Print("DNI_NUEVO: " + paciente.DNI);
            using (SqlConnection conn = conexion.AbrirConexion())
            using (SqlTransaction tx = conn.BeginTransaction())
            {
                try
                {
                    string qPersona = @"UPDATE Persona SET DNI=@DNI_NUEVO,nombre=@Nombre,apellido=@Apellido,sexo=@Sexo,direccion=@Direccion,idLocalidad=@IdLocalidad,fechaNacimiento=@FechaNacimiento,nacionalidad=@Nacionalidad WHERE DNI=@DNI_VIEJO";
                    using (SqlCommand cmd = new SqlCommand(qPersona, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                        cmd.Parameters.AddWithValue("@Nombre", paciente.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", paciente.apellido);
                        cmd.Parameters.AddWithValue("@Sexo", paciente.genero);
                        cmd.Parameters.AddWithValue("@Direccion", paciente.Direccion);
                        cmd.Parameters.AddWithValue("@IdLocalidad", paciente.Localidad);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", paciente.fechaNacimiento);
                        cmd.Parameters.AddWithValue("@Nacionalidad", paciente.nacionalidad);
                        cmd.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qPaciente = @"UPDATE Paciente SET DNI=@DNI_NUEVO,ObraSocial=@ObraSocial WHERE DNI=@DNI_VIEJO";
                    using (SqlCommand cmd = new SqlCommand(qPaciente, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                        cmd.Parameters.AddWithValue("@ObraSocial", paciente.ObraSocial);
                        cmd.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qTel = @"UPDATE Telefonos SET idPersona=@DNI_NUEVO, telefono=@Telefono WHERE idPersona=@DNI_VIEJO";
                    using (SqlCommand cmd = new SqlCommand(qTel, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                        cmd.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                        cmd.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qCorreo = @"UPDATE Correos SET idPersona=@DNI_NUEVO, correo=@Correo WHERE idPersona=@DNI_VIEJO";
                    using (SqlCommand cmd = new SqlCommand(qCorreo, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                        cmd.Parameters.AddWithValue("@Correo", paciente.Correo);
                        cmd.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qTurnos = "UPDATE Turnos SET DNIPaciente=@DNI_NUEVO WHERE DNIPaciente=@DNI_VIEJO";
                    using (SqlCommand cmd = new SqlCommand(qTurnos, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", paciente.DNI);
                        cmd.Parameters.AddWithValue("@DNI_VIEJO", DNI_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return 1;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
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
