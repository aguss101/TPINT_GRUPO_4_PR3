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
            CargarTurnos();
        }

        protected void btnAdministrarMedicos_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Medicos.aspx");
        }

        protected void btnAdministrarPacientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Pacientes.aspx");
        }

        protected void btnAdministrarTurnos_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Turnos.aspx");
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
                GestorTurnos gestorTurnos = new GestorTurnos();
                ddlMedico.DataSource = gestorTurnos.ObtenerMedicosPorEspecialidad(idEspecialidad);
                ddlMedico.DataTextField = "NombreCompleto";
                ddlMedico.DataValueField = "Legajo";
                ddlMedico.DataBind();
            }
            else
            {
                // Limpia si se vuelve a la opción por defecto
                ddlMedico.Items.Clear();
            }

            // Siempre agregar al principio el ítem por defecto
            ddlMedico.Items.Insert(0, new ListItem("-- Seleccione un médico --", ""));
        }

        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}