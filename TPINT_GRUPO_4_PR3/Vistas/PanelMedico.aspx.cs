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
            cargarTurnos();
        }
        protected void cargarTurnos()
        {

            string Legajo = Session["LegajoMedico"].ToString();
            DateTime fechaSelected = Session["fechaxCalendar"] != null
                ? (DateTime)Session["fechaxCalendar"]
                : DateTime.Today;
            System.Diagnostics.Debug.WriteLine("Legajo" + Legajo);
            gvTurnos.DataSource = gestorturnos.GetTurnosMedico(Legajo, fechaSelected);
            gvTurnos.DataBind();

        }

        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;
            cargarTurnos();
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
            cargarTurnos();
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {

        }

        protected void btnEstado_Click(object sender, EventArgs e)
        {

        }

        protected void btnPorFecha_Click(object sender, EventArgs e)
        {
            mwVerTurnos.ActiveViewIndex = 0;
        }

        protected void btnVerTodos_Click(object sender, EventArgs e)
        {
            mwVerTurnos.ActiveViewIndex = 1;
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
                default:
                    mwBusqueda.ActiveViewIndex = -1;
                    break;
            }
        }
    }
}