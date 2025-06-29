using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
   public class GestorPaciente
    {
        ConsultasPacientes consultas = new ConsultasPacientes();
        public List<Paciente> GetPacientes()
        {
           
            return consultas.GetPacientes();
        }
        public Paciente getPacientePorID(string idPaciente)
        {
            return consultas.getPacientePorID(idPaciente);
        }

        // Inserta un paciente en la base de datos
        public int InsertarPaciente(Paciente paciente)
        {
            return consultas.InsertarPaciente(paciente);
        }

        // Modifica un paciente
        public int ModificarPaciente(Paciente paciente, string DNI_VIEJO)
        {
            return consultas.ModificarPaciente(paciente, DNI_VIEJO);
        }
        // Elimina lógicamente un paciente
        public int EliminarPaciente(string DNI)
        {
            return consultas.EliminarPaciente(DNI);
        }
    }
}
