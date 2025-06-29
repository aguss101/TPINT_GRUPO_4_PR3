using System;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas.admin
{
    public partial class Administrar_Turnos : System.Web.UI.Page
    {
        GestorTurnos gestorturnos = new GestorTurnos();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["User"] as string;
            CargarTurnos();
        }
        protected void navigateButton_Click(object sender, CommandEventArgs e)
        {
            switch (e.CommandArgument.ToString())
            {
                case "Medicos": Response.Redirect("/admin/Administrar_Medicos.aspx"); break;
                case "Pacientes": Response.Redirect("/admin/Administrar_Pacientes.aspx"); break;
                case "Turnos": Response.Redirect("/admin/Administrar_Turnos.aspx"); break;
                default: break;
            }
        }
        protected void CargarTurnos()
        {
            gvTurnos.DataSource = gestorturnos.GetTurnos();
            gvTurnos.DataBind();
        }
        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEspecialidad;

            if (int.TryParse(ddlEspecialidad.SelectedValue, out idEspecialidad) && idEspecialidad > 0)
            {
                ddlMedico.DataSource = new GestorTurnos().ObtenerMedicosPorEspecialidad(idEspecialidad);
                ddlMedico.DataTextField = "NombreCompleto";
                ddlMedico.DataValueField = "Legajo";
                ddlMedico.DataBind();
            }
            else
            {
                ddlMedico.Items.Clear();
            }
            ddlMedico.Items.Insert(0, new ListItem("-- Seleccione un médico --", ""));
        }

        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}