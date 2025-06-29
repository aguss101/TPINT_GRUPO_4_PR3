using System.Collections.Generic;
using Datos;
using Entidades;

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

        public int InsertarMedico(Medico medico, Usuario usuario)
        {
            return consultas.InsertarMedico(medico, usuario);
        }

        public int ModificarMedico(Medico medico, Usuario usuario, string DNI_VIEJO, string LEGAJO_VIEJO)
        {
            return consultas.ModificarMedico(medico, usuario, DNI_VIEJO, LEGAJO_VIEJO);
        }

        public int EliminarMedico(string DNI)
        {
            return consultas.EliminarMedico(DNI);
        }
    }
}