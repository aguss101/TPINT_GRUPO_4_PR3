using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class ConsultasReportes
    {
        DataAccess conexion = new DataAccess();

        //REPORTES DE MEDICOS
        public DataTable GetCantidadMedicosxEspecialidad()
        {

            DataTable dtMedicosxEspecialidad = new DataTable();
            string query = @"SELECT E.descripcion AS Especialidad, COUNT(ME.Legajo ) AS CantMedicos FROM Especialidades E LEFT JOIN Medico ME ON ME.idEspecialidad = E.idEspecialidad GROUP BY E.descripcion ORDER BY CantMedicos DESC";
            using (SqlConnection con = conexion.AbrirConexion())
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter(query, con))
                {
                    cmd.Fill(dtMedicosxEspecialidad);
                }

                return dtMedicosxEspecialidad;

            }



        }
        public DataTable GetMedicosxEdad()
        {

            DataTable dtMedicosxEdad = new DataTable();
            string query = "SELECT CASE WHEN edad <= 30 THEN 'Joven' WHEN edad <= 50 THEN 'Adulto' ELSE 'AdultoMayor' END AS CategoriaEdad, COUNT(*) AS Cantidad FROM (SELECT DATEDIFF(YEAR, fechaNacimiento, GETDATE()) AS edad FROM Persona PE INNER JOIN Medico ME ON ME.DNI = PE.DNI) AS sub GROUP BY CASE WHEN edad <= 30 THEN 'Joven' WHEN edad <= 50 THEN 'Adulto' ELSE 'AdultoMayor' END ORDER BY CategoriaEdad";

            using (SqlConnection con = conexion.AbrirConexion())
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter(query, con))
                {
                    cmd.Fill(dtMedicosxEdad);
                }

                return dtMedicosxEdad;

            }
        }

        public DataTable GetCantidadTurnosxMedico()
        {
            DataTable dtCantidadTurnosxMedico = new DataTable();
            string query = "SELECT m.Legajo, p.nombre + ' ' + p.apellido AS NombreCompleto, COUNT(*) AS CantidadTurnos FROM Turnos t JOIN Medico m ON t.Legajo = m.Legajo JOIN Persona p ON m.DNI = p.DNI GROUP BY m.Legajo, p.nombre, p.apellido ORDER BY CantidadTurnos DESC";
            using (SqlConnection con = conexion.AbrirConexion())
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter(query, con))
                {
                    cmd.Fill(dtCantidadTurnosxMedico);
                }
            }

            return dtCantidadTurnosxMedico;
        }


        //REPORTES DE PACIENTES
        public DataTable GetPacientesxEdad()
        {
            DataTable dtPacientesxEdad = new DataTable();
            string query = "SELECT CASE WHEN edad <= 30 THEN 'Joven' WHEN edad <= 50 THEN 'Adulto' ELSE 'AdultoMayor' END AS CategoriaEdad, COUNT(*) AS Cantidad FROM (SELECT DATEDIFF(YEAR, fechaNacimiento, GETDATE()) AS edad FROM Persona PE INNER JOIN Paciente PA ON PA.DNI = PE.DNI) AS sub GROUP BY CASE WHEN edad <= 30 THEN 'Joven' WHEN edad <= 50 THEN 'Adulto' ELSE 'AdultoMayor' END ORDER BY CategoriaEdad";
            using (SqlConnection con = conexion.AbrirConexion())
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter(query, con))
                {
                    cmd.Fill(dtPacientesxEdad);
                }
                return dtPacientesxEdad;
            }
        }

        public DataTable GetPacientesxObraSocial()
        {
            DataTable dtPacientesxObraSocial = new DataTable();
            string query = "SELECT O.nombre AS ObraSocial, COUNT(PA.DNI) AS CantPacientes FROM Paciente PA LEFT JOIN ObraSocial O ON PA.ObraSocial = O.idObraSocial GROUP BY O.nombre ORDER BY CantPacientes DESC";
            using (SqlConnection con = conexion.AbrirConexion())
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter(query, con))
                {
                    cmd.Fill(dtPacientesxObraSocial);
                }
                return dtPacientesxObraSocial;
            }
        }

        public DataTable GetPacientesxSexo()
        {
            DataTable dtPacientesxSexo = new DataTable();
            string query = "SELECT S.descripcion AS Sexo, COUNT(*) AS CantidadPacientes FROM Paciente PA JOIN Persona P ON PA.DNI = P.DNI JOIN Sexos S ON P.sexo = S.idSexo GROUP BY S.descripcion ORDER BY CantidadPacientes DESC";
            using (SqlConnection con = conexion.AbrirConexion())
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter(query, con))
                {
                    cmd.Fill(dtPacientesxSexo);
                }
                return dtPacientesxSexo;
            }
        }
        //Trae reportes de la cantidad de ausentes en el mes actual
        public DataTable GetPacientesxAusentesMes()
        {
            DataTable dtPacientesxAusentesMes = new DataTable();

            string query = "SELECT YEAR(T.fechaPactada) AS Anio, MONTH(T.fechaPactada) AS Mes, COUNT(*) AS CantidadAusentes FROM Turnos T INNER JOIN EstadoTurnos ET ON T.estado = ET.idEstado WHERE ET.descripcion = 'AUSENTE' GROUP BY YEAR(T.fechaPactada), MONTH(T.fechaPactada) ORDER BY Anio, Mes";

            using (SqlConnection con = conexion.AbrirConexion())
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter(query, con))
                {
                    cmd.Fill(dtPacientesxAusentesMes);
                }
                return dtPacientesxAusentesMes;
            }

            
        }

        //REPORTES DE TURNOS
        public DataTable GetPromedioTurnosxEspecialidad()
        {
            DataTable dtPromedioTurnosxEspecialidad = new DataTable();
            string query = "SELECT E.descripcion AS Especialidad, AVG(CantidadTurnos * 1.0) AS PromediosTurnosxEspecialidad FROM (SELECT ME.Legajo, ME.idEspecialidad, COUNT(*) AS CantidadTurnos FROM Medico ME LEFT JOIN Turnos T ON T.Legajo = ME.Legajo GROUP BY ME.Legajo, ME.idEspecialidad) AS Sub INNER JOIN Especialidades E ON E.idEspecialidad = Sub.idEspecialidad GROUP BY E.descripcion ORDER BY PromediosTurnosxEspecialidad";
            using (SqlConnection con = conexion.AbrirConexion())
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter(query, con))
                {
                    cmd.Fill(dtPromedioTurnosxEspecialidad);
                }
                return dtPromedioTurnosxEspecialidad;
            }
        }



        public DataTable GetTurnosxEstado()
        {
            DataTable dtTurnosxEstado = new DataTable();
            string query = "SELECT ET.descripcion AS Estado, COUNT(*) AS CantidadTurnos FROM Turnos T JOIN EstadoTurnos ET ON T.estado = ET.idEstado GROUP BY ET.descripcion ORDER BY CantidadTurnos DESC;";
            using (SqlConnection con = conexion.AbrirConexion())
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter(query, con))
                {
                    cmd.Fill(dtTurnosxEstado);
                }
                return dtTurnosxEstado;
            }
        }


    }
}
