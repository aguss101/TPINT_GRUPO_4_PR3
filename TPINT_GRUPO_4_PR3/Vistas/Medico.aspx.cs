using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
namespace Vistas
{
    public partial class Medico : System.Web.UI.Page
    {
        private GestorUsuario gestorUsuario = new GestorUsuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarUsuarios();


        }
        protected void cargarUsuarios()
        {
            List<Usuario> usuarios = gestorUsuario.GetUsuarios();

            DropDownList1.DataSource = usuarios;
            DropDownList1.DataTextField = "NombreUsuario"; // Lo que se muestra
            DropDownList1.DataValueField = "DNI";    // Valor oculto que se envía al servidor
            DropDownList1.DataBind();

        }
    }
}