using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Diagnostics;

namespace Vistas.admin
{
    public partial class Administrar_Pacientes : System.Web.UI.Page
    {
        private GestorPaciente gestorPaciente = new GestorPaciente();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                btnMod.Visible = false;
                btnBaja.Visible = false;
            }
        }
        protected void btnAlta_Click(object sender, EventArgs e)
        {
            mvFormularios.ActiveViewIndex = 0;
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GridView2.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                    if (chk != null && chk.Checked)
                    {
                        string DNI = row.Cells[1].Text;
                        gestorPaciente.EliminarPaciente("sp_EliminarPaciente", DNI);
                    }
                }
                loadGridPacientes();
                lblAddUserState.Text = "Paciente/s dado/s de baja correctamente.";
                lblAddUserState.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblAddUserState.Text = "❌ Error: " + ex.Message;
                lblAddUserState.ForeColor = System.Drawing.Color.Red;
            }
            btnMod.Visible = false;
            btnBaja.Visible = false;
        }

        protected void btnMod_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");

                if (chk != null && chk.Checked)
                {
                    string DNI = row.Cells[1].Text;



                    Paciente paciente = new Paciente();

                    paciente = gestorPaciente.getPacientePorID(DNI);
                    mvFormularios.ActiveViewIndex = 2;
                    txbModDni.Text = paciente.DNI;
                    txbModNombre.Text = paciente.nombre;
                    txbModApellido.Text = paciente.apellido;
                    ddlModGenero.SelectedValue = paciente.genero.ToString();
                    ddlModNacionalidad.SelectedValue = paciente.nacionalidad;
                    ddlModLocalidades.SelectedValue = paciente.Localidad.ToString();
                    //ddlModProvincias.SelectedIndex = ;
                    DateTime fechaNac = paciente.fechaNacimiento.Date;
                    txbModFechaNacimiento.Text = fechaNac.ToString("dd-MM-yyyy");
                    txbModDireccion.Text = paciente.Direccion;
                    ddlModObraSocial.SelectedValue = paciente.ObraSocial.ToString();
                    txbModTelefono.Text = paciente.Telefono.ToString();
                    txbModCorreo.Text = paciente.Correo;             

                    break;
                }
            }

            btnMod.Visible = false;
            btnBaja.Visible = false;
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
        protected void btnModificarPaciente_Click(object sender, EventArgs e)
        {
            ModificarPaciente();
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

        }
      
        protected void ModificarPaciente()
        {
            Paciente paciente = new Paciente();

            paciente.nombre = txbModNombre.Text.Trim();
            paciente.apellido = txbModApellido.Text.Trim();
            paciente.DNI = txbModDni.Text.Trim();
            paciente.fechaNacimiento = Convert.ToDateTime(txbModFechaNacimiento.Text.Trim());
            paciente.ObraSocial = int.Parse(ddlModObraSocial.SelectedValue);
            paciente.genero = int.Parse(ddlModGenero.SelectedValue);
            paciente.Localidad = int.Parse(ddlModLocalidades.SelectedValue);
            paciente.ultimaAtencion = DateTime.Now;
            paciente.Alta = DateTime.Now;
            paciente.nacionalidad = ddlModNacionalidad.SelectedValue.ToString();
            paciente.Telefono = int.Parse(txbModTelefono.Text.Trim());
            paciente.Direccion = txbModDireccion.Text.Trim();
            paciente.Correo = txbModCorreo.Text.Trim();


            string nombreProcedimiento = "sp_ModificarPaciente";
            int filas = gestorPaciente.ModificarPaciente(nombreProcedimiento, paciente);
            if (filas > 0)
            {
                lblModUser.Text = "Se modifico correctamente el Paciente";
                lblModUser.ForeColor = System.Drawing.Color.Green; // DEBUG para testear si añade o no el paciente
                lblModUser.Visible = true;
            }
        }



        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSeleccionar");
                chk.AutoPostBack = true;
                chk.CheckedChanged += new EventHandler(chkSeleccionar_CheckedChanged);  // Cada vez que se checkea un checkbox se dispara el evento, que recorre todos los checkboxs y desmarca los que no dispararon el evento.
            }
        }

        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            
            foreach (GridViewRow row in GridView2.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");  // Recorre todas las filas y desmarca todos aquellos checkboxs que no dispararon el evento.
                if (chk != sender)
                {
                    chk.Checked = false;
                }
            }

          
            bool algunoMarcado = false;
            foreach (GridViewRow row in GridView2.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar"); // Esta verifica que haya alguno marcado para mostrar los botones.
                if (chk.Checked)
                {
                    algunoMarcado = true;
                   
                    break;
                }
            }

            btnMod.Visible = algunoMarcado;   // Si AlgunoMarcado es FALSE no se muestra ningun boton.
            btnBaja.Visible = algunoMarcado;
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