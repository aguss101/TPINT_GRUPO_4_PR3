using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Data;
namespace Negocio
{
    public class GestorReportes
    {
        ConsultasReportes consulta = new ConsultasReportes();
        
        //Medicos
        public DataTable GetCantidadMedicosxEspecialidad()
        {
            return consulta.GetCantidadMedicosxEspecialidad();
        }
        public DataTable GetMedicosxEdad()
        {
            return consulta.GetMedicosxEdad();
        }
        public DataTable GetCantidadTurnosxMedico()
        {
            return consulta.GetCantidadTurnosxMedico();
        }

        //Pacientes
        public DataTable GetPacientesxObraSocial()
        {
            return consulta.GetPacientesxObraSocial();
        }
        public DataTable GetPacientesxSexo()
        {
            return consulta.GetPacientesxSexo();
        }

        public DataTable GetPacientesxEdad()
        {
            return consulta.GetPacientesxEdad();
        }

        public DataTable GetPacientesxAusentesMes()
        {
            return consulta.GetPacientesxAusentesMes();
        }

        //Turnos
        public DataTable GetPromedioTurnosxEspecialidad()
        {
            return consulta.GetPromedioTurnosxEspecialidad();
        }

        public DataTable GetTurnosxEstado()
        {
            return consulta.GetTurnosxEstado();
        }


    }
}
