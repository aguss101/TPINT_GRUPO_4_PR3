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
            mvFormularios.ActiveViewIndex = 3;
            loadGridPacientes();




        }

        protected void btnMod_Click(object sender, EventArgs e)
        {

            //recorro el gridview buscando el cbx seleccionado
            foreach (GridViewRow row in GridView2.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");

                //verifico que no sea null el valor y este tildado
                if (chk != null && chk.Checked)
                {
                    //obtengo el DNI del seleccionado
                    string DNI = row.Cells[1].Text;



                    Paciente paciente = new Paciente();

                    paciente = gestorPaciente.getPacientePorID(DNI);
                    mvFormularios.ActiveViewIndex = 2;
                    txtbModPacienteDNI.Text = paciente.DNI;
                    txtbModPacienteNombre.Text = paciente.nombre;
                    txtbModPacienteApellido.Text = paciente.apellido;
                    

                    break;
                }
            }


        }

        protected void buttonsCbxVisibility(object sender, EventArgs e)
        {
            CheckBox cbox = (CheckBox)sender;
            if (cbox.Checked)
            {
            btnMod.Visible = true;
            btnBaja.Visible = true ;
            }
            else
            {
                btnMod.Visible = false;
                btnBaja.Visible = false;

            }
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
            paciente.DNI = txbDni.Text.Trim();
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
                lblAddUserState.ForeColor = System.Drawing.Color.Green; // DEBUG para testear si añade o no el paciente
                lblAddUserState.Visible = true;
            }
      
            else
                lblAddUserState.Text = "Hubo un error durante la carga";
            lblAddUserState.ForeColor = System.Drawing.Color.Red;
            lblAddUserState.Visible = true;

        }

        protected void btnAdministrarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Medicos.aspx");
        }

        protected void btnAdministrarPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Pacientes.aspx");
        }

        protected void btnAdministrarTurnos_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Turnos.aspx");
        }
    }
}