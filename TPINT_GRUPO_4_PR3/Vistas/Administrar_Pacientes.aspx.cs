using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace Vistas.admin
{
    public partial class Administrar_Pacientes : System.Web.UI.Page
    {
        private GestorPaciente gestorPaciente = new GestorPaciente();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

       

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            mvFormularios.ActiveViewIndex = 0;
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            mvFormularios.ActiveViewIndex = 1;
        }

        protected void btnMod_Click(object sender, EventArgs e)
        {
            mvFormularios.ActiveViewIndex = 2;

        }

        protected void btnLectura_Click(object sender, EventArgs e)
        {
            mvFormularios.ActiveViewIndex = 3;
            loadGridPacientes();

        }

        protected void btnRegistrarPaciente_Click(object sender, EventArgs e)
        {
            InsertarPacientes();
        }

        protected void loadGridPacientes()
        {

            GestorPaciente gestorPaciente = new GestorPaciente();
            List<Paciente> listaPacientes = gestorPaciente.GetPacientes();

            GridView2.DataSource = listaPacientes;
            GridView2.DataBind();
           
        }

        /*
        protected void deletedGridPaciente()
        {
            GestorPaciente gestorPaciente = new GestorPaciente();
            gestorPaciente.
        }
        */


        protected void InsertarPacientes()
        {
            Paciente paciente = new Paciente();
            paciente.nombre = txbNombre.Text.Trim();
            paciente.apellido = txbApellido.Text.Trim();
            paciente.DNI = int.Parse(txbDni.Text.Trim());
            paciente.fechaNacimiento = Convert.ToDateTime(txbFechaNacimiento.Text.Trim());
            paciente.ObraSocial = int.Parse(ddlObraSocial.SelectedValue);
            paciente.genero = int.Parse(ddlGenero.SelectedValue);
            paciente.Localidad = int.Parse(ddlLocalidades.SelectedValue);
            paciente.ultimaAtencion = DateTime.Now;
            paciente.Alta = DateTime.Now;
            paciente.nacionalidad = ddlNacionalidad.SelectedValue.ToString();
            paciente.Telefono = int.Parse(txbTelefono.Text.Trim());
            paciente.Direccion = txbDireccion.Text.Trim();
            paciente.Correo = txbCorreo.Text.Trim();


            string nombreProcedimiento = "sp_AltaPaciente";
            int filas = gestorPaciente.InsertarPaciente(nombreProcedimiento, paciente);
            if (filas > 0)
            {
                lblAddUserState.Text = "Se agrego correctamente el Paciente";
                lblAddUserState.ForeColor = System.Drawing.Color.Green; // DEBUG para testear si a;ade o no el paciente
                lblAddUserState.Visible = true;
            }
      
            else
                lblAddUserState.Text = "Hubo un error durante la carga";
            lblAddUserState.ForeColor = System.Drawing.Color.Red;
            lblAddUserState.Visible = true;

        }

      
    }
}