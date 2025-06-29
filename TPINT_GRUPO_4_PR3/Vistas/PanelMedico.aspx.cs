using System;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas
{
    public partial class PanelMedico : System.Web.UI.Page
    {
        private GestorTurnos gestorturnos = new GestorTurnos();
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarTurnosAll();

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

            btnCargar.Visible = algunoMarcado;


        }

        protected void calendarMedico_SelectionChanged(object sender, EventArgs e)
        {
            Session ["fechaxCalendar"] = calendarMedico.SelectedDate;
            cargarTurnosxFecha();
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {

        }

        protected void btnEstado_Click(object sender, EventArgs e)
        {

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

    }
}