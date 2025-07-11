using System;
using System.Collections.Generic;
using Entidades;
using Negocio;
namespace Vistas
{
    public partial class Login : System.Web.UI.Page
    {
        GestorMedico gestorMedico = new GestorMedico();
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
                if (usuario.NombreUsuario.ToLower() == txbUser.Text.ToLower() && usuario.contrasenia.ToLower() == TxbPassword.Text.ToLower())
                {
                    Session["User"] = usuario.NombreUsuario;

                    if (usuario.idRol == 1)
                    {
                        Response.Redirect("~/admin/Admin.aspx");


                    }

                    else
                    {

                        Medico medico = gestorMedico.getMedicoPorID(usuario.DNI);
                        string legajo = medico.Legajo;
                        Session["LegajoMedico"] = legajo;

                        Response.Redirect("~/PanelMedico/PanelMedico.aspx");
                    }
                    return;
                }

            }
            lblError.Text = "Usuario o contraseña incorrectos.";
            lblError.Visible = true;

        }

    }

}