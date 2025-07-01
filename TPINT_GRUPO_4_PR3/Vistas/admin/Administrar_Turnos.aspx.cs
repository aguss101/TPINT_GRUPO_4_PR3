using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Entidades;
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
                mvAsignarTurnos.ActiveViewIndex = 0;

                lblUser.Text = Session["User"] as string;

                CargarTurnos();

                ddlMedico.Items.Clear();
                ddlFecha.Items.Clear();
                ddlHora.Items.Clear();

                txtObservacion.Attributes["placeholder"] = "Observación";
                txtDiagnostico.Attributes["placeholder"] = "Diagnóstico";

                btnMod.Visible = false;
                btnBaja.Visible = false;
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
            DateTime hoy = DateTime.Now;
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
        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvTurnos.Rows) { if (row.FindControl("chkSeleccionar") is CheckBox chk && chk != sender) { chk.Checked = false; } }
            btnMod.Visible = btnBaja.Visible = (sender as CheckBox)?.Checked == true;
        }


        protected void btnMod_Click(object sender, EventArgs e)
        { ModificarTurno(); }
        protected void btnModAplicarCambios_click(object sender, EventArgs e)
        {
            string legajo = Session["Legajo"].ToString();
            string valorFecha = Session["FechaVieja"].ToString();

            DateTime fechaPactada = DateTime.ParseExact(valorFecha, "d/M/yyyy H:mm:ss", CultureInfo.InvariantCulture);

            if (string.IsNullOrEmpty(ddlModFecha.SelectedValue) || ddlModFecha.SelectedValue == "--Seleccione Fecha--")
            {
                lblMensaje.Text = "Debe seleccionar una fecha válida.";
                return;
            }

            if (string.IsNullOrEmpty(ddlModHorario.SelectedValue))
            {
                lblMensaje.Text = "Debe seleccionar un horario.";
                return;
            }
            DateTime fechaNueva = DateTime.ParseExact(ddlModFecha.SelectedValue, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            TimeSpan horaNueva = TimeSpan.Parse(ddlModHorario.SelectedValue);
            DateTime fechaHoraNueva = fechaNueva.Add(horaNueva);

            string observacion = txtObservacion.Text.Trim();
            string diagnostico = txtDiagnostico.Text.Trim();

            List<Turno> turnos = gestorturnos.GetTurnosMedico(legajo, fechaPactada);
            Turno turnoSeleccionado = turnos.FirstOrDefault();

            if (turnoSeleccionado != null)
            {
                turnoSeleccionado.FechaOriginal = turnoSeleccionado.FechaPactada;
                turnoSeleccionado.FechaPactada = fechaHoraNueva;
                bool exito = gestorturnos.ModificarTurno(turnoSeleccionado);

                lblMensaje.Text = exito ? "Turno actualizado con éxito." : "No se pudo actualizar el turno.";
            }
            else
            {
                lblMensaje.Text = "No se encontró el turno a modificar.";
            }
        }
        protected void ModificarTurno()
        {
            foreach (GridViewRow row in gvTurnos.Rows)
            {
                if (row.FindControl("chkSeleccionar") is CheckBox chk && chk.Checked)
                {
                    string legajo = row.Cells[1].Text;
                    Session["Legajo"] = legajo;
                    string auxFechaPactada = row.Cells[3].Text.Trim();
                    Session["FechaVieja"] = auxFechaPactada;
                    DateTime fechaPactada = DateTime.ParseExact(auxFechaPactada, "d/M/yyyy H:mm:ss", CultureInfo.InvariantCulture);

                    List<Turno> turnos = gestorturnos.GetTurnosMedico(legajo, fechaPactada);

                    gvTurnos.DataSource = turnos;
                    gvTurnos.DataBind();
                    mvAsignarTurnos.ActiveViewIndex = 1;

                    DateTime fechaHoy = DateTime.Today;
                    List<DateTime> fechas = gestorturnos.ObtenerFechasDisponibles(legajo, fechaHoy, fechaHoy.AddDays(14));


                    ddlModFecha.Items.Clear();
                    ddlModFecha.Items.Add(new ListItem("--Seleccione Fecha--", ""));

                    foreach (DateTime fecha in fechas)
                    {
                        ddlModFecha.Items.Add(new ListItem(
                            fecha.ToString("dd/MM/yyyy"),
                            fecha.ToString("yyyy-MM-dd")
                        ));
                    }
                    break;
                }
            }
            btnMod.Visible = false;
            btnBaja.Visible = false;
        }
        protected void ddlModFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlModHorario.Items.Clear();
            if (!string.IsNullOrEmpty(ddlModFecha.SelectedValue))
            {
                string legajo = Session["Legajo"].ToString();
                DateTime fechaSeleccionada = DateTime.Parse(ddlModFecha.SelectedValue);


                if (fechaSeleccionada == DateTime.Now)
                {
                    fechaSeleccionada = DateTime.Now;
                }

                DataTable dtHorariosDisponibles = gestorturnos.ObtenerHorasDisponibles(legajo, fechaSeleccionada);

                ddlModHorario.Items.Add(new ListItem("--Seleccione hora--", ""));
                foreach (DataRow row in dtHorariosDisponibles.Rows)
                {
                    string hora = row["rangoHorario"].ToString();
                    ddlModHorario.Items.Add(new ListItem(hora, hora));
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