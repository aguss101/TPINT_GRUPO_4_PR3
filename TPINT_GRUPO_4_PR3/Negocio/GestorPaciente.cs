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

        public int InsertarPaciente(string nombreprocedimiento, Paciente paciente)
        {
            return consultas.InsertarPaciente(nombreprocedimiento, paciente);
        }

        public int ModificarPaciente(string nombreprocedimiento, Paciente paciente)
        {
            return consultas.ModificarPaciente(nombreprocedimiento, paciente);
        }
    }
}
