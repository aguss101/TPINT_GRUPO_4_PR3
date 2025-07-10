using System;
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class GestorMedico
    {
        ConsultasMedico consultas = new ConsultasMedico();
        public List<Medico> GetMedicos() { return consultas.GetMedicos(); }
        public Medico getMedicoPorID(string idMedico) { return consultas.getMedicoPorID(idMedico); }
        public int InsertarMedico(Medico medico, Usuario usuario) { return consultas.InsertarMedico(medico, usuario); }
        public int InsertarJornadasMedico(string legajo, List<string> diasLaborales, TimeSpan hora) { return consultas.InsertarJornadasMedico(legajo, diasLaborales, hora); }
        public bool ValidarLegajo(string Legajo) { return consultas.ExisteLegajo(Legajo); }
        public int ModificarMedico(Medico medico, Usuario usuario) { return consultas.ModificarMedico(medico, usuario); }
        public int EliminarMedico(string DNI) { return consultas.EliminarMedico(DNI); }
        public List<Medico> FiltrarMedicoxApellido(string apellido) { return consultas.FiltrarMedicoxApellido(apellido); }
        public List<Medico> FiltrarMedicoxDNI(string DNI) { return consultas.FiltrarMedicoxDNI(DNI); }
        public List<Medico> FiltrarMedicoxLegajo(string legajo) { return consultas.FiltrarMedicoxLegajo(legajo); }

        public string GetDiaHabilesDeMedico(string legajo) { return consultas.GetDiaHabilesDeMedico(legajo); }
        private string SiglasDia(string diaCompleto) { return consultas.SiglasDias(diaCompleto); }
    }
}