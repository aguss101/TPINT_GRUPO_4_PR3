using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Negocio;
using Entidades;
using System.Linq;

namespace Vistas
{
    public partial class PanelMedico : System.Web.UI.Page
    {
        private GestorTurnos gestorturnos = new GestorTurnos();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
            cargarTurnosAll();

            }

        }
        protected void cargarTurnosAll()
        {
                string Legajo = Session["LegajoMedico"].ToString();
                DateTime ? fechaSelected = null;

                gvTurnos.DataSource = gestorturnos.GetTurnosMedico(Legajo, fechaSelected);
                gvTurnos.DataBind();
            
        }
        protected void cargarTurnosxFecha()
        {
            string Legajo = Session["LegajoMedico"].ToString();
            DateTime fechaSelected = Session["fechaxCalendar"] != null
                ? (DateTime)Session["fechaxCalendar"]
                : DateTime.Today;
            gvTurnos.DataSource = gestorturnos.GetTurnosMedico(Legajo, fechaSelected);
            gvTurnos.DataBind();
        }
        protected void cargarTurnosxApellido()
        {
                string Legajo = Session["LegajoMedico"].ToString();
                string apellidoSelected = Session["apellidoPaciente"].ToString();

                gvTurnos.DataSource = gestorturnos.FiltrarPacientexApellido(Legajo, apellidoSelected);
                gvTurnos.DataBind();
        }
        protected void cargarTurnosxDNI()
        {
                string Legajo = Session["LegajoMedico"].ToString();
                string dniPaciente = Session["dniPaciente"].ToString();

                gvTurnos.DataSource = gestorturnos.FiltrarPacientexDNI(Legajo, dniPaciente);
                gvTurnos.DataBind();
        }

        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;
            cargarTurnosxFecha();
        }

        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvTurnos.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                if (chk != sender)
                {
                    chk.Checked = false;
                }


            }
            bool algunoMarcado = false;
            foreach (GridViewRow row in gvTurnos.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                if (chk.Checked)
                {
                    algunoMarcado = true;

                    break;
                }
            }

            //btnMod.Visible = algunoMarcado;


        }

        protected void calendarMedico_SelectionChanged(object sender, EventArgs e)
        {
            Session ["fechaxCalendar"] = calendarMedico.SelectedDate;
            cargarTurnosxFecha();
        }


        protected void btnEstado_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            

            
            string estado = boton.CommandArgument; // "Presente" o "Ausente"

            //boton.BackColor = estado == "Presente" ? System.Drawing.Color.Green : System.Drawing.Color.Red;


        }

        protected void btnPorFecha_Click(object sender, EventArgs e)
        {
            cargarTurnosxFecha();
        }

        protected void ddlBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlBusqueda.SelectedIndex)
            {
                case 1:
                    mwBusqueda.ActiveViewIndex = 0;
                    break;
                case 2:
                    mwBusqueda.ActiveViewIndex = 1;
                    break;
                case 3:
                    mwBusqueda.ActiveViewIndex= 2;
                    break;
                default:
                    mwBusqueda.ActiveViewIndex = -1;
                    break;
            }
        }

        protected void txbPorApellido_TextChanged(object sender, EventArgs e)
        {
            Session["apellidoPaciente"] = txbPorApellido.Text;
            cargarTurnosxApellido();
        }

        protected void txbPorDNI_TextChanged(object sender, EventArgs e)
        {
            Session["dniPaciente"] = txbPorDNI.Text;
            cargarTurnosxDNI();
        }


        protected void btnEnviarDiagnostico_Click(object sender,EventArgs e) {

            Button boton = (Button)sender;
            GridViewRow fila = (GridViewRow)boton.NamingContainer;

            TextBox txtDiagnostico = (TextBox)fila.FindControl("txbDiagnostico");
            TextBox txtObs = (TextBox)fila.FindControl("txtObs");

            string diagnostico = txtDiagnostico != null ? txtDiagnostico.Text : "";
            string observacion = txtObs != null ? txtObs.Text : "";

            // También podés obtener valores de columnas de tipo BoundField

            DateTime fechaPactada = Convert.ToDateTime( fila.Cells[2].Text);


            DropDownList ddlEstado = (DropDownList)fila.FindControl("ddlEstado");
            int estadoSeleccionado = Convert.ToInt32(ddlEstado.SelectedValue);

            List<Turno> turnoViejo = gestorturnos.GetTurnosMedico(Session["LegajoMedico"].ToString(), fechaPactada);

            ///Turno turno = turnoViejo.First();
            TextBox1.Text = Session["LegajoMedico"].ToString();
            TextBox2.Text = txtDiagnostico.Text;
            TextBox3.Text = ddlEstado.SelectedValue;

            //if (turno != null)
            //{
            //    turno.Diagnostico = txtDiagnostico?.Text;
            //    turno.Observacion = txtObs?.Text;
            //    turno.Estado = estadoSeleccionado;
            //    gestorturnos.ModificarTurno(turno);
            //}



        }
    }
}