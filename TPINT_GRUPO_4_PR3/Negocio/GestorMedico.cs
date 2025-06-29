using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
    public class GestorMedico
    {
        ConsultasMedico consultas = new ConsultasMedico();
        public List<Medico> GetMedicos()
        {

            return consultas.GetMedicos();
        }
        public Medico getMedicoPorID(string idMedico)
        {
            return consultas.getMedicoPorID(idMedico);
        }
        // Inserta un médico
        public int InsertarMedico(Medico medico, Usuario usuario)
        {
            return consultas.InsertarMedico(medico, usuario);
        }
        // Modifica un médico existente
        public int ModificarMedico(Medico medico,Usuario usuario, string DNI_VIEJO, string LEGAJO_VIEJO)
        {
            return consultas.ModificarMedico(medico,usuario, DNI_VIEJO, LEGAJO_VIEJO);
        }
        // Elimina lógicamente un médico
        public int EliminarMedico(string DNI)
        {
            return consultas.EliminarMedico(DNI);
        }
    }
}