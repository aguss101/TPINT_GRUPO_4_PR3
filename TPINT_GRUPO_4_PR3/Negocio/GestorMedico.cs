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
        public int InsertarMedico(string nombreprocedimiento, Medico medico, Usuario usuario)
        {
            return consultas.InsertarMedico(nombreprocedimiento, medico, usuario);
        }
        public int ModificarMedico(string nombreprocedimiento, Medico medico,Usuario usuario, string DNI_VIEJO, string LEGAJO_VIEJO)
        {
            return consultas.ModificarMedico(nombreprocedimiento, medico,usuario, DNI_VIEJO, LEGAJO_VIEJO);
        }
        public int EliminarMedico(string nombreprocedimiento, string DNI)
        {
            return consultas.EliminarMedico(nombreprocedimiento, DNI);
        }
    }
}