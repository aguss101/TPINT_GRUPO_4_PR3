using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["User"] as string;
        }

        protected void btnAdministrarMedicos_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Medicos.aspx");
        }

        protected void Administrar_Pacientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Pacientes.aspx");
        }

        protected void Administrar_Turnos_Click(object sender, EventArgs e)
        { Response.Redirect("/admin/Administrar_Turnos.aspx"); }

        protected void ddlReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
                ddlCategoria.DataSource =
                ddlCategoria.DataTextField = "NombreCompleto";
                ddlCategoria.DataValueField = "Legajo";
                ddlCategoria.DataBind();
        }
    }
}