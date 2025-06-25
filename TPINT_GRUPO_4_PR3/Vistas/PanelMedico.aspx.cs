using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace Vistas
{
    public partial class PanelMedico : System.Web.UI.Page
    {
        private GestorPaciente gestorPaciente = new GestorPaciente();
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarPacientes();


        }
        protected void cargarPacientes()
        {
            List<Paciente> pacientes = gestorPaciente.GetPacientes();

            gvTurnos.DataSource = pacientes;
           
            gvTurnos.DataBind();

        }
    }
}