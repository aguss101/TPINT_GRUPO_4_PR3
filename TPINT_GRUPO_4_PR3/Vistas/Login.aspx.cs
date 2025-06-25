using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
namespace Vistas
{
    public partial class Login : System.Web.UI.Page
    {
        private GestorUsuario gestorUsuario = new GestorUsuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            txbUser.Attributes["placeholder"] = "Nombre de usuario";
            TxbPassword.Attributes["placeholder"] = "Contraseña";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = gestorUsuario.GetUsuarios();
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.NombreUsuario == txbUser.Text.Trim() && usuario.contrasenia == TxbPassword.Text.Trim())
                {
                    Session["User"] = usuario.NombreUsuario;

                    if (usuario.idRol == 1)
                    {
                        Response.Redirect("Admin.aspx");
                        

                    }

                    else
                    {
                    Response.Redirect("PanelMedico.aspx");
                    }
                    return;
                }

           }
            lblError.Text = "Usuario o contraseña incorrectos.";
            lblError.Visible = true;

        }
        
    }
    
}