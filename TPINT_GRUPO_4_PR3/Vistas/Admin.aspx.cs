using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Discovery;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["User"] as string;
            //CargarGrafico();
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



        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            GestorReportes gestor = new GestorReportes();
            string tipo = ddlReportes.SelectedValue.ToString();
            string cat = ddlCategoria.SelectedValue.ToString();

            switch (tipo)
            {
                case "1":  // Médicos
                    if (cat == "1")
                        CargarGrafico("Especialidad", "CantMedicos", gestor.GetCantidadMedicosxEspecialidad());
                    else if (cat == "2")
                        CargarGrafico("NombreCompleto", "CantidadTurnos", gestor.GetCantidadTurnosxMedico());
                    else if (cat == "3")
                        CargarGrafico("CategoriaEdad", "Cantidad", gestor.GetMedicosxEdad());
                    break;

                case "2":  // Pacientes
                    if (cat == "1")
                        CargarGrafico("CategoriaEdad", "Cantidad", gestor.GetPacientesxEdad());
                    else if (cat == "2")
                        CargarGrafico("ObraSocial", "CantPacientes", gestor.GetPacientesxObraSocial());
                    else if (cat == "3")
                        CargarGrafico("Estado", "CantidadTurnos", gestor.GetTurnosxEstado());
                    else if (cat == "4")
                        CargarGrafico("Mes", "CantidadAusentes", gestor.GetPacientesxAusentesMes());
                    break;

                case "3":  // Turnos
                    if (cat == "1")
                        CargarGrafico("Estado", "CantidadTurnos", gestor.GetTurnosxEstado());
                    else if (cat == "2")
                        CargarGrafico("Especialidad", "PromediosTurnosxEspecialidad", gestor.GetPromedioTurnosxEspecialidad());
                    break;

                default:
                    graficoReportes.Series.Clear();
                    break;
            }
        }
        protected void CargarGrafico(string x, string y, DataTable data)
        {
           
            graficoReportes.Series.Clear();
           
            var serie = new Series
            {
                XValueMember = x,
                YValueMembers = y,
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true
            };
            graficoReportes.Series.Add(serie);

         
            graficoReportes.ChartAreas[0].AxisX.Interval = 1;

            graficoReportes.DataSource = data;
            graficoReportes.DataBind();
        }

        protected void ddlReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategoria.SelectedIndex = 0;
        }
        
    }
}