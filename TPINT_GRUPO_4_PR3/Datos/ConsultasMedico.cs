using System;
using System.Collections.Generic;
using System.Data;
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
            string query = @"SELECT * FROM vw_MedicoConDatos
            WHERE activo = 1 ";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                                Especialidad = new Especialidad() { descripcion = reader["Especialidad"].ToString() },
                                nombre = reader["nombre"].ToString(),
                                apellido = reader["apellido"].ToString(),
                                sexos = new Sexos() { descripcion = (reader["genero"].ToString()) },
                                fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                                Direccion = reader["Direccion"].ToString(),
                                Provincia = Convert.ToInt32(reader["idProvincia"]),
                                Provincias = new Provincias
                                {
                                    idProvincia = Convert.ToInt32(reader["idProvincia"]),
                                    nombreProvincia = reader["nombreProvincia"].ToString()
                                },
                                Localidad = Convert.ToInt32(reader["idLocalidad"]),
                                Localidades = new Localidades
                                {
                                    idLocalidad = Convert.ToInt32(reader["idLocalidad"]),
                                    nombreLocalidad = reader["nombreLocalidad"].ToString()
                                },
                                nacionalidad = reader["nacionalidad"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Telefono = reader["telefono"].ToString()
                            };
                            medicos.Add(medico);
                        }
                    }
                }

            }
            catch (Exception ex) { throw new Exception("Error al buscar medicos: " + ex.Message); }
            return medicos;
        }





        public int InsertarMedico(Medico medico, Usuario usuario)
        {
            string queryPersona = @"INSERT INTO Persona (DNI, nombre, apellido, sexo, direccion, idLocalidad, fechaNacimiento, nacionalidad) 
                                    VALUES (@DNI, @Nombre, @Apellido, @Sexo, @Direccion, @IdLocalidad, @FechaNacimiento, @Nacionalidad)";

            string queryMedico = @"INSERT INTO Medico (DNI, Legajo, idEspecialidad) 
                                    VALUES (@DNI, @Legajo, @idEspecialidad)";

            string queryUsuario = @"INSERT INTO Usuario (DNI,nombreUsuario,idRol,contrasenia,alta,ultimoIngreso)
                                    VALUES (@DNI, @Usuario, @IdRol, @Contrasenia, @Alta, @UltimoIngreso)";

            string queryTelefono = @"INSERT INTO Telefonos (idPersona, telefono) 
                                        VALUES (@DNI, @Telefono)";

            string queryCorreo = @"INSERT INTO Correos (idPersona, correo) 
                                    VALUES (@DNI, @Correo)";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlTransaction transaction = con.BeginTransaction())
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(queryPersona, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                            cmd.Parameters.AddWithValue("@Nombre", medico.nombre);
                            cmd.Parameters.AddWithValue("@Apellido", medico.apellido);
                            cmd.Parameters.AddWithValue("@Sexo", medico.sexos.idSexo);
                            cmd.Parameters.AddWithValue("@FechaNacimiento", medico.fechaNacimiento);
                            cmd.Parameters.AddWithValue("@Nacionalidad", medico.nacionalidad);
                            cmd.Parameters.AddWithValue("@IdLocalidad", medico.Localidad);
                            cmd.Parameters.AddWithValue("@Direccion", medico.Direccion);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryMedico, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                            cmd.Parameters.AddWithValue("@Legajo", medico.Legajo);
                            cmd.Parameters.AddWithValue("@idEspecialidad", medico.Especialidad.idEspecialidad);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryUsuario, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                            cmd.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                            cmd.Parameters.AddWithValue("@IdRol", usuario.idRol);
                            cmd.Parameters.AddWithValue("@Contrasenia", usuario.contrasenia);
                            cmd.Parameters.AddWithValue("@Alta", usuario.alta);
                            cmd.Parameters.AddWithValue("@UltimoIngreso", usuario.ultimoIngreso);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryTelefono, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                            cmd.Parameters.AddWithValue("@Telefono", medico.Telefono);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryCorreo, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                            cmd.Parameters.AddWithValue("@Correo", medico.Correo);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return 1;
                    }
                    catch (Exception ex) { transaction.Rollback(); throw new Exception("Error durante la transacción: " + ex.Message); }
            }
            catch (Exception ex) { throw new Exception("Error al insertar medico: " + ex.Message); }
        }



        public int EliminarMedico(string dni)
        {
            string query = @"UPDATE Persona 
                             SET activo = 0 
                             WHERE DNI = @DNI";
            SqlParameter[] parametros = new SqlParameter[]
            { new SqlParameter("@DNI", dni) };
            return conexion.EjecutarComandoConParametros(query, parametros);
        }

        public int ModificarMedico(Medico medico, Usuario usuario, string DNI_VIEJO, string LEGAJO_VIEJO)
        {
            string queryUsuario = @"UPDATE Usuario SET DNI = @DNI_NUEVO, nombreUsuario = @Usuario, contrasenia = @Contrasenia WHERE DNI = @DNI";

            string queryCorreo = @"UPDATE Correos SET idPersona = @DNI_NUEVO, correo = @Correo WHERE idPersona = @DNI";

            string queryTelefono = @"UPDATE Telefonos SET idPersona = @DNI_NUEVO, telefono = @Telefono WHERE idPersona = @DNI";

            string queryJornadas = "UPDATE Jornadas SET Legajo = @LegajoNuevo WHERE Legajo = @Legajo";

            string queryTurnos = "UPDATE Turnos SET Legajo = @LegajoNuevo WHERE Legajo = @Legajo";

            string queryPersona = @"UPDATE Persona SET DNI = @DNI_NUEVO, nombre = @Nombre, apellido = @Apellido, sexo = @Sexo, direccion = @Direccion, idLocalidad = @IdLocalidad, 
                                    fechaNacimiento = @FechaNacimiento, nacionalidad = @Nacionalidad WHERE DNI = @DNI";

            string queryMedico = @"UPDATE Medico SET DNI = @DNI_NUEVO, idEspecialidad = @idEspecialidad, Legajo = @LegajoNuevo WHERE DNI = @DNI";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlTransaction transaction = con.BeginTransaction())
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(queryUsuario, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                            cmd.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                            cmd.Parameters.AddWithValue("@Contrasenia", usuario.contrasenia);
                            cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryCorreo, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                            cmd.Parameters.AddWithValue("@Correo", medico.Correo);
                            cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryTelefono, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                            cmd.Parameters.AddWithValue("@Telefono", medico.Telefono);
                            cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryJornadas, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@LegajoNuevo", medico.Legajo);
                            cmd.Parameters.AddWithValue("@Legajo", LEGAJO_VIEJO);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryTurnos, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@LegajoNuevo", medico.Legajo);
                            cmd.Parameters.AddWithValue("@Legajo", LEGAJO_VIEJO);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryPersona, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                            cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                            cmd.Parameters.AddWithValue("@Nombre", medico.nombre);
                            cmd.Parameters.AddWithValue("@Apellido", medico.apellido);
                            cmd.Parameters.AddWithValue("@Sexo", medico.sexos.idSexo);
                            cmd.Parameters.AddWithValue("@FechaNacimiento", medico.fechaNacimiento);
                            cmd.Parameters.AddWithValue("@Nacionalidad", medico.nacionalidad);
                            cmd.Parameters.AddWithValue("@IdLocalidad", medico.Localidad);
                            cmd.Parameters.AddWithValue("@Direccion", medico.Direccion);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(queryMedico, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                            cmd.Parameters.AddWithValue("@idEspecialidad", medico.Especialidad.idEspecialidad);
                            cmd.Parameters.AddWithValue("@LegajoNuevo", medico.Legajo);
                            cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return 1;
                    }
                    catch (Exception ex) { transaction.Rollback(); throw new Exception("Error durante la transacción: " + ex.Message); }
            }
            catch (Exception ex) { throw new Exception("Error al modificar medico: " + ex.Message); }
        }

        public Medico getMedicoPorID(string idMedico)
        {
            Medico medico = null;
            string query = @"SELECT * FROM vw_MedicoConDatos ME
                WHERE ME.DNI = @id";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idMedico);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //MapearMedico(reader);
                            medico = new Medico()
                            {
                                Usuario = new Usuario()
                                {
                                    NombreUsuario = reader["nombreUsuario"].ToString(),
                                    contrasenia = reader["contrasenia"].ToString()
                                },
                                Legajo = reader["Legajo"].ToString(),
                                DNI = reader["DNI"].ToString(),
                                Especialidad = new Especialidad() { descripcion = reader["Especialidad"].ToString() },
                                nombre = reader["nombre"].ToString(),
                                apellido = reader["apellido"].ToString(),
                                sexos = new Sexos { idSexo = Convert.ToInt32(reader["idSexo"]), descripcion = reader["genero"].ToString() },
                                fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                                Direccion = reader["Direccion"].ToString(),
                                Provincia = Convert.ToInt32(reader["idProvincia"]),
                                Provincias = new Provincias
                                {
                                    idProvincia = Convert.ToInt32(reader["idProvincia"]),
                                    nombreProvincia = reader["nombreProvincia"].ToString()
                                },
                                Localidad = Convert.ToInt32(reader["idLocalidad"]), // NO BORRAR
                                Localidades = new Localidades
                                {
                                    idLocalidad = Convert.ToInt32(reader["idLocalidad"]),
                                    nombreLocalidad = reader["nombreLocalidad"].ToString()
                                },
                                nacionalidad = reader["nacionalidad"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Telefono = reader["Telefono"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Error al buscar medico por ID: " + ex.Message); }
            return medico;
        }
        public Medico MapearMedico(SqlDataReader reader)
        {
            return new Medico
            {
                Usuario = new Usuario()
                {
                    NombreUsuario = reader["nombreUsuario"].ToString(),
                    contrasenia = reader["contrasenia"].ToString()
                },
                Legajo = reader["Legajo"].ToString(),
                DNI = reader["DNI"].ToString(),
                Especialidad = new Especialidad() { descripcion = reader["Especialidad"].ToString() },
                nombre = reader["nombre"].ToString(),
                apellido = reader["apellido"].ToString(),
                sexos = new Sexos { idSexo = Convert.ToInt32(reader["idSexo"]), descripcion = reader["genero"].ToString() },
                fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                Direccion = reader["Direccion"].ToString(),
                Provincia = Convert.ToInt32(reader["idProvincia"]),
                Provincias = new Provincias
                {
                    idProvincia = Convert.ToInt32(reader["idProvincia"]),
                    nombreProvincia = reader["nombreProvincia"].ToString()
                },
                Localidad = Convert.ToInt32(reader["idLocalidad"]), // NO BORRAR
                Localidades = new Localidades
                {
                    idLocalidad = Convert.ToInt32(reader["idLocalidad"]),
                    nombreLocalidad = reader["nombreLocalidad"].ToString()
                },
                nacionalidad = reader["nacionalidad"].ToString(),
                Correo = reader["Correo"].ToString(),
                Telefono = reader["Telefono"].ToString()
            };
        }
        public List<Medico> FiltrarMedicoxApellido(string apellido)
        {
            List<Medico> medico = new List<Medico>();
            string query = @"SELECT * FROM vw_MedicoConDatos
                WHERE activo = 1
                AND (@Apellido IS NULL OR apellido COLLATE Latin1_General_CI_AI LIKE '%' + @Apellido + '%')
                ORDER BY Legajo";

            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 25) { Value = apellido });
                    using (SqlDataReader reader = cmd.ExecuteReader()) { while (reader.Read()) { medico.Add(MapearMedico(reader)); } }
                }
            }
            catch (Exception ex) { throw new Exception("Error al cargar médicos por apellido: " + ex.Message); }
            return medico;
        }
        public List<Medico> FiltrarMedicoxDNI(string dniMedico)
        {
            List<Medico> medico = new List<Medico>();
            string query = @"SELECT * FROM vw_MedicoConDatos
                WHERE activo = 1
                AND (@DNI IS NULL OR dniMedico COLLATE Latin1_General_CI_AI LIKE '%' + @DNI + '%')
                ORDER BY Legajo";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@DNI", SqlDbType.VarChar, 20) { Value = dniMedico });
                    using (SqlDataReader reader = cmd.ExecuteReader()) { while (reader.Read()) { medico.Add(MapearMedico(reader)); } }
                }
            }
            catch (Exception ex) { throw new Exception("Error al cargar turnos por DNI: " + ex.Message); }
            return medico;
        }
        public List<Medico> FiltrarMedicoxLegajo(string legajo)
        {
            List<Medico> medico = new List<Medico>();
            string query = @"
               SELECT * FROM vw_MedicoConDatos
               WHERE activo = 1
               AND (@Legajo IS NULL OR Legajo LIKE '%' + @Legajo + '%')
                ORDER BY Legajo";
            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@Legajo", SqlDbType.VarChar, 20) { Value = legajo });
                    using (SqlDataReader reader = cmd.ExecuteReader()) { while (reader.Read()) { medico.Add(MapearMedico(reader)); } }
                }
            }
            catch (Exception ex) { throw new Exception("Error al cargar turnos por legajo: " + ex.Message); }
            return medico;
        }


        public int InsertarOActualizarJornadas(string legajo, List<string> diasLaborales, TimeSpan hora)
        {
            const string sql = @"
       
            UPDATE Jornadas
           SET rangoHorario = @RangoHorario
            WHERE Legajo    = @Legajo
            AND DiaSemana = @DiaSemana;

       
             IF @@ROWCOUNT = 0
            INSERT INTO Jornadas (Legajo, DiaSemana, rangoHorario)
                 VALUES          (@Legajo, @DiaSemana, @RangoHorario);";

            try
            {
                using (SqlConnection con = conexion.AbrirConexion())
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        foreach (string dia in diasLaborales)
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, con, tx))
                            {
                                cmd.Parameters.AddWithValue("@Legajo", legajo);
                                cmd.Parameters.AddWithValue("@DiaSemana", dia);
                                cmd.Parameters.AddWithValue("@RangoHorario", hora);
                                cmd.ExecuteNonQuery();
                            }
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
            catch (Exception ex)
            {
                throw new Exception("Error al registrar/actualizar jornadas: " + ex.Message, ex);
            }
        }

    }





}