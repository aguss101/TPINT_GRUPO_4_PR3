using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class GestorUsuario
    {
        private ConsultasUsuario consultaUsuario= new ConsultasUsuario();
        public List<Usuario> GetUsuarios() { return consultaUsuario.getUsuarios(); }
    }
}