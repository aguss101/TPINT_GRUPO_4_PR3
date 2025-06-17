using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txbUser.Attributes["placeholder"] = "Nombre de usuario";
            TxbPassword.Attributes["placeholder"] = "Contraseña";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txbUser.Text.Trim();
            string password = TxbPassword.Text.Trim();
            Errores(user, password);

        }
            protected void Errores(string user, string password)
            {
                if (!RedirectBoth(user, password))
                {

                    if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                    {
                        lblError.Text = "Por favor, ingrese usuario y contraseña.";
                        lblError.Visible = true;
                        return;
                    }
                    else
                    {
                        lblError.Text = "Usuario o contraseña incorrectos.";
                        lblError.Visible = true;
                    }
                }
            }

            protected bool RedirectBoth(string user, string password)
            {
                if (user == "admin" && password == "1234")
                {

                    Session["User"] = user;
                    Response.Redirect("Admin.aspx");
                    return true;
                }
                else if (user == "medico" && password == "1234")
                {
                    Session["User"] = user;
                    Response.Redirect("Medico.aspx");
                    return true;
                }
                else
                {
                    return false;
                }
            }
    }
}