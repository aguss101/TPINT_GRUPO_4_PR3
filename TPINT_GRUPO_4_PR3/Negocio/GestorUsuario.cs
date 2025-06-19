using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
namespace Negocio
{
    public class GestorUsuario
    {
        private ConsultasUsuario consultaUsuario= new ConsultasUsuario();
        public List<Usuario> GetUsuarios()
        {
            return consultaUsuario.getUsuarios();
        }
    }
}
