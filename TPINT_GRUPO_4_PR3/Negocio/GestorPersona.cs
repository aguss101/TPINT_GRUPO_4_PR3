using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
   public class GestorPersona
    {
        private ConsultasPersona consultaPersona = new ConsultasPersona();
        public List<Persona> GetPersonas()
        {
            return consultaPersona.getPersona();
        }
    }
}
