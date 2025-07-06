using System.Collections.Generic;
using Entidades;
using Datos;

namespace Negocio
{
   public class GestorPaciente
    {
        ConsultasPacientes consultas = new ConsultasPacientes();
        public List<Paciente> GetPacientes() { return consultas.GetPacientes(); }
        public Paciente getPacientePorID(string idPaciente){ return consultas.getPacientePorID(idPaciente);}
        public int InsertarPaciente(Paciente paciente) { return consultas.InsertarPaciente(paciente); }
        public int ModificarPaciente(Paciente paciente, string DNI_VIEJO) { return consultas.ModificarPaciente(paciente, DNI_VIEJO); }
        public int EliminarPaciente(string DNI) { return consultas.EliminarPaciente(DNI);}
        public List<Paciente> FiltrarPacientexApellido(string apellido) { return consultas.FiltrarPacientexApellido(apellido); }
        public List<Paciente> FiltrarPacientexDNI(string DNI) { return consultas.FiltrarPacientexDNI(DNI); }
    }
}