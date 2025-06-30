using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas.admin
{
    public partial class Administrar_Turnos : System.Web.UI.Page
    {
        GestorTurnos gestorturnos = new GestorTurnos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblUser.Text = Session["User"] as string;

                CargarTurnos();

                ddlMedico.Items.Clear();
                ddlFecha.Items.Clear();
                ddlHora.Items.Clear();

            }

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

                ddlMedico.Items.Clear();
            }


            ddlMedico.Items.Insert(0, new ListItem("-- Seleccione un médico --", ""));
        }

        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            string legajo = ddlMedico.SelectedValue;
            if (string.IsNullOrEmpty(legajo))
            {
                ddlFecha.Items.Add(new ListItem("--Seleccione Fecha--", ""));
                return;
            }


            CargarFechas(legajo);
        }

        private void CargarFechas(string legajo)
        {
            ddlFecha.Items.Clear();
            ddlHora.Items.Clear();
            DateTime hoy = DateTime.Today;
            DateTime fin = hoy.AddDays(14);

            List<DateTime> fechas = gestorturnos.ObtenerFechasDisponibles(legajo, hoy, fin);

            ddlFecha.Items.Clear();
            ddlFecha.Items.Add(new ListItem("--Seleccione Fecha--", ""));
            foreach (DateTime fecha in fechas)
            {
                ddlFecha.Items.Add(new ListItem(
                    fecha.ToString("dddd dd/MM/yyyy"),
                    fecha.ToString("yyyy-MM-dd")
                ));
            }
        }

        protected void ddlFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlHora.Items.Clear();
            if (DateTime.TryParse(ddlFecha.SelectedValue, out DateTime fecha))
            {
                string legajo = ddlMedico.SelectedValue;
                DataTable dtHoras = gestorturnos.ObtenerHorasDisponibles(legajo, fecha);

                ddlHora.Items.Add(new ListItem("--Seleccione hora--", ""));
                foreach (DataRow row in dtHoras.Rows)
                {
                    string hora = row["rangoHorario"].ToString();
                    ddlHora.Items.Add(new ListItem(hora, hora));
                }
            }
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


    }
}