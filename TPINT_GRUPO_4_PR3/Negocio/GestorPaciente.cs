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

        public int InsertarPaciente(string nombreprocedimiento, Paciente paciente)
        {
            return consultas.InsertarPaciente(nombreprocedimiento, paciente);
        }
    }
}
