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
            System.Diagnostics.Debug.WriteLine("Legajo" + Legajo);
            gvTurnos.DataSource = gestorturnos.GetTurnosMedico(Legajo);
            gvTurnos.DataBind();

        }

        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;
            cargarTurnos();
        }
    }
}