using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Web;

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

            using (SqlConnection connection = conexion.AbrirConexion())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Medico medico = new Medico();
                            medico.Usuario = new Usuario();
                            
                            medico.Legajo = (reader["Legajo"].ToString());
                            medico.DNI = (reader["DNI"].ToString());
                            medico.idEspecialidad = (Convert.ToInt32(reader["idEspecialidad"]));
                            medico.nombre = (reader["nombre"].ToString());
                            medico.apellido = (reader["apellido"].ToString());
                            medico.genero = (Convert.ToInt32(reader["idSexo"]));
                            medico.fechaNacimiento = (DateTime)reader["FechaNacimiento"];
                            medico.Direccion = (reader["Direccion"].ToString());
                            medico.Localidad = (Convert.ToInt32(reader["idLocalidad"]));
                            medico.nacionalidad = (reader["nacionalidad"].ToString());
                            medico.Correo = (reader["Correo"].ToString());
                            medico.Telefono = (reader["telefono"].ToString());
                            medico.Usuario.NombreUsuario = reader["nombreUsuario"].ToString();
                            medico.Usuario.contrasenia = reader["contrasenia"].ToString();
                            
                            medicos.Add(medico);
                        }
                    }
                }
            }
            return medicos;

        }
        // Inserta un médico sin procedimientos almacenados
        public int InsertarMedico(Medico medico, Usuario usuario)
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
                        cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                        cmd.Parameters.AddWithValue("@Nombre", medico.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", medico.apellido);
                        cmd.Parameters.AddWithValue("@Sexo", medico.genero);
                        cmd.Parameters.AddWithValue("@Direccion", medico.Direccion);
                        cmd.Parameters.AddWithValue("@IdLocalidad", medico.Localidad);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", medico.fechaNacimiento);
                        cmd.Parameters.AddWithValue("@Nacionalidad", medico.nacionalidad);
                        cmd.ExecuteNonQuery();
                    }

                    string qMedico = @"INSERT INTO Medico (DNI, Legajo, idEspecialidad) VALUES (@DNI,@Legajo,@idEspecialidad)";
                    using (SqlCommand cmd = new SqlCommand(qMedico, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                        cmd.Parameters.AddWithValue("@Legajo", medico.Legajo);
                        cmd.Parameters.AddWithValue("@idEspecialidad", medico.idEspecialidad);
                        cmd.ExecuteNonQuery();
                    }

                    string qUsuario = @"INSERT INTO Usuario (DNI,nombreUsuario,idRol,contrasenia,alta,ultimoIngreso)
                                         VALUES (@DNI,@Usuario,@IdRol,@Contrasenia,@Alta,@UltimoIngreso)";
                    using (SqlCommand cmd = new SqlCommand(qUsuario, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                        cmd.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@IdRol", usuario.idRol);
                        cmd.Parameters.AddWithValue("@Contrasenia", usuario.contrasenia);
                        cmd.Parameters.AddWithValue("@Alta", usuario.alta);
                        cmd.Parameters.AddWithValue("@UltimoIngreso", usuario.ultimoIngreso);
                        cmd.ExecuteNonQuery();
                    }

                    string qTel = @"INSERT INTO Telefonos (idPersona,telefono) VALUES (@DNI,@Telefono)";
                    using (SqlCommand cmd = new SqlCommand(qTel, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                        cmd.Parameters.AddWithValue("@Telefono", medico.Telefono);
                        cmd.ExecuteNonQuery();
                    }

                    string qCorreo = @"INSERT INTO Correos (idPersona,correo) VALUES (@DNI,@Correo)";
                    using (SqlCommand cmd = new SqlCommand(qCorreo, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI", medico.DNI);
                        cmd.Parameters.AddWithValue("@Correo", medico.Correo);
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

        // Realiza la baja lógica de un médico
        public int EliminarMedico(string dni)
        {
            string q = "UPDATE Persona SET activo = 0 WHERE DNI = @DNI";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@DNI", dni)
            };
            return conexion.EjecutarComandoConParametros(q, parametros);
        }

        // Modifica los datos de un médico existente
        public int ModificarMedico(Medico medico, Usuario usuario, string DNI_VIEJO, string LEGAJO_VIEJO)
        {
            Debug.Print("Dni_Viejo: " + DNI_VIEJO);
            Debug.Print("Legajo_Viejo: " + LEGAJO_VIEJO);
            Debug.Print("Dni_Nuevo: " + medico.DNI);
            Debug.Print("Legajo_Nuevo: " + medico.Legajo);
            using (SqlConnection conn = conexion.AbrirConexion())
            using (SqlTransaction tx = conn.BeginTransaction())
            {
                try
                {
                    string qUsuario = @"UPDATE Usuario SET DNI=@DNI_NUEVO,nombreUsuario=@Usuario,contrasenia=@Contrasenia WHERE DNI=@DNI";
                    using (SqlCommand cmd = new SqlCommand(qUsuario, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                        cmd.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@Contrasenia", usuario.contrasenia);
                        cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qCorreo = @"UPDATE Correos SET idPersona=@DNI_NUEVO, correo=@Correo WHERE idPersona=@DNI";
                    using (SqlCommand cmd = new SqlCommand(qCorreo, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                        cmd.Parameters.AddWithValue("@Correo", medico.Correo);
                        cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qTel = @"UPDATE Telefonos SET idPersona=@DNI_NUEVO, telefono=@Telefono WHERE idPersona=@DNI";
                    using (SqlCommand cmd = new SqlCommand(qTel, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                        cmd.Parameters.AddWithValue("@Telefono", medico.Telefono);
                        cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qJornadas = "UPDATE Jornadas SET Legajo=@LegajoNuevo WHERE Legajo=@Legajo";
                    using (SqlCommand cmd = new SqlCommand(qJornadas, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@LegajoNuevo", medico.Legajo);
                        cmd.Parameters.AddWithValue("@Legajo", LEGAJO_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qTurnos = "UPDATE Turnos SET Legajo=@LegajoNuevo WHERE Legajo=@Legajo";
                    using (SqlCommand cmd = new SqlCommand(qTurnos, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@LegajoNuevo", medico.Legajo);
                        cmd.Parameters.AddWithValue("@Legajo", LEGAJO_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qPersona = @"UPDATE Persona SET DNI=@DNI_NUEVO,nombre=@Nombre,apellido=@Apellido,sexo=@Sexo,direccion=@Direccion,idLocalidad=@IdLocalidad,fechaNacimiento=@FechaNacimiento,nacionalidad=@Nacionalidad WHERE DNI=@DNI";
                    using (SqlCommand cmd = new SqlCommand(qPersona, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                        cmd.Parameters.AddWithValue("@Nombre", medico.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", medico.apellido);
                        cmd.Parameters.AddWithValue("@Sexo", medico.genero);
                        cmd.Parameters.AddWithValue("@Direccion", medico.Direccion);
                        cmd.Parameters.AddWithValue("@IdLocalidad", medico.Localidad);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", medico.fechaNacimiento);
                        cmd.Parameters.AddWithValue("@Nacionalidad", medico.nacionalidad);
                        cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
                        cmd.ExecuteNonQuery();
                    }

                    string qMedico = @"UPDATE Medico SET DNI=@DNI_NUEVO,idEspecialidad=@idEspecialidad,Legajo=@LegajoNuevo WHERE DNI=@DNI";
                    using (SqlCommand cmd = new SqlCommand(qMedico, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@DNI_NUEVO", medico.DNI);
                        cmd.Parameters.AddWithValue("@idEspecialidad", medico.idEspecialidad);
                        cmd.Parameters.AddWithValue("@LegajoNuevo", medico.Legajo);
                        cmd.Parameters.AddWithValue("@DNI", DNI_VIEJO);
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

        public Medico getMedicoPorID(string idMedico)
        {
            Medico medico = null;

            using (SqlConnection connection = conexion.AbrirConexion())
            {
                string query = "SELECT ME.*, PE.nombre, PE.apellido, PE.nacionalidad, PE.fechaNacimiento, PE.Direccion, S.idSexo, C.correo, T.telefono, L.idLocalidad, " +
                "U.nombreUsuario, U.contrasenia FROM Medico ME " + "INNER JOIN Persona PE ON ME.DNI = PE.DNI INNER JOIN Sexos S ON PE.sexo = S.idSexo " + 
                "INNER JOIN Localidades L ON PE.idLocalidad = L.idLocalidad" + "  LEFT JOIN Correos C ON PE.DNI = C.idPersona  LEFT JOIN Telefonos T ON PE.DNI = " +
                " T.idPersona INNER JOIN Usuario U ON PE.DNI = U.DNI WHERE ME.DNI = @id";


                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idMedico);


                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    medico = new Medico();
                    medico.Usuario = new Usuario();

                    medico.Legajo = reader["Legajo"].ToString();
                    medico.DNI = (reader["DNI"].ToString());
                    medico.idEspecialidad = (Convert.ToInt32(reader["idEspecialidad"]));
                    medico.nombre = (reader["nombre"].ToString());
                    medico.apellido = (reader["apellido"].ToString());
                    medico.genero = (Convert.ToInt32(reader["idSexo"]));
                    medico.fechaNacimiento = (DateTime)reader["FechaNacimiento"];
                    medico.Direccion = (reader["Direccion"].ToString());
                    medico.Localidad = (Convert.ToInt32(reader["idLocalidad"]));
                    medico.nacionalidad = (reader["nacionalidad"].ToString());
                    medico.Correo = (reader["Correo"].ToString());
                    medico.Telefono = (reader["Telefono"].ToString());
                    medico.Usuario.NombreUsuario = reader["nombreUsuario"].ToString();
                    medico.Usuario.contrasenia = reader["contrasenia"].ToString();
                    
                }

                reader.Close();
            }

            return medico;
        }
    }
}