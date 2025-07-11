using Entidades;
using Negocio;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class PanelMedico : System.Web.UI.Page
    {
        private GestorTurnos gestorturnos = new GestorTurnos();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblUser.Text = Session["User"] + "🩺";
                ddlEstados.SelectedValue = Session["EstadoDescripcion"] as string ?? "TODOS";
                cargarTurnosAll();
            }
        }
        protected void cargarTurnosAll()
        {
            string Legajo = Session["LegajoMedico"].ToString();
            DateTime? fechaSelected = null;

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
            CheckBox chkClicked = (CheckBox)sender;


            foreach (GridViewRow row in gvTurnos.Rows)
            {

                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                TextBox txtDiag = (TextBox)row.FindControl("txbDiagnostico");
                TextBox txtObs = (TextBox)row.FindControl("txbObs");
                DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");
                Button btnEnviar = (Button)row.FindControl("btnEnviarDiagnostico");



                bool esSeleccionada = chk == chkClicked && chk.Checked;

                chk.Checked = esSeleccionada;
                txtDiag.Enabled = esSeleccionada;
                txtObs.Enabled = esSeleccionada;
                ddlEstado.Visible = esSeleccionada;
                btnEnviar.Visible = esSeleccionada;

                if (!esSeleccionada)
                {
                    txtDiag.Text = gvTurnos.DataKeys[row.RowIndex]["diagnostico"].ToString();
                    txtObs.Text = gvTurnos.DataKeys[row.RowIndex]["observacion"].ToString();
                }
            }
        }

        protected void calendarMedico_SelectionChanged(object sender, EventArgs e)
        {
            Session["fechaxCalendar"] = calendarMedico.SelectedDate;
            cargarTurnosxFecha();
        }


        protected void btnPorFecha_Click(object sender, EventArgs e)
        {
            cargarTurnosxFecha();
        }

        protected void ddlBusqueda_Turnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTurnosAll();
            switch (ddlBusqueda_Turnos.SelectedIndex)
            {
                case 1:
                    mwBusqueda.ActiveViewIndex = 0;
                    break;
                case 2:
                    mwBusqueda.ActiveViewIndex = 1;
                    break;
                case 3:
                    mwBusqueda.ActiveViewIndex = 2;
                    break;
                case 4:
                    mwBusqueda.ActiveViewIndex = 3;
                    break;
                default:
                    mwBusqueda.ActiveViewIndex = -1;
                    break;
            }
        }
        protected void txbPorApellido_TextChanged(object sender, EventArgs e)
        {
            Session["apellidoPaciente"] = txbPorApellido.Text;
            txbPorApellido.Text = "";
            cargarTurnosxApellido();
        }

        protected void txbPorDNI_TextChanged(object sender, EventArgs e)
        {
            Session["dniPaciente"] = txbPorDNI.Text;
            txbPorDNI.Text = "";
            cargarTurnosxDNI();
        }


        protected void btnEnviarDiagnostico_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvTurnos.Rows)
            {
                if (row.FindControl("chkSeleccionar") is CheckBox chk && chk.Checked)
                {
                    string legajom = Session["LegajoMedico"] as string;
                    DateTime fechaPactada = DateTime.Parse(row.Cells[3].Text);
                    Debug.WriteLine(fechaPactada, "fECHA");

                    TextBox txtDiag = (TextBox)row.FindControl("txbDiagnostico");
                    TextBox txtObs = (TextBox)row.FindControl("txbObs");
                    DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");

                    string diagnostico = txtDiag.Text;
                    string observacion = txtObs.Text;
                    int estado = ddlEstado.SelectedIndex;

                    Turno turno = new Turno()
                    {
                        Legajo = legajom,
                        FechaPactada = fechaPactada,
                        Diagnostico = diagnostico,
                        Estado = estado,
                        Observacion = observacion

                    };
                    int filas = gestorturnos.MarcarAsistenciaTurnoMedico(turno);
                }
            }
            cargarTurnosAll();
        }

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string legajo = Session["LegajoMedico"] as string;
            if (string.IsNullOrEmpty(legajo)) { return; }

            string estado = ddlEstados.SelectedValue;
            if (estado == "TODOS") estado = null;

            gvTurnos.DataSource = gestorturnos.FiltradoTurnosMedico(estado, legajo);
            gvTurnos.DataBind();
        }
    }
}