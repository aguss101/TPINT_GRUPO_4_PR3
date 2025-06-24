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
                    Session["DNI_VIEJO"] = paciente.DNI;

                    mvFormularios.ActiveViewIndex = 2;

                    txbModDni.Text = paciente.DNI;
                    txbModNombre.Text = paciente.nombre;
                    txbModApellido.Text = paciente.apellido;
                    ddlModGenero.SelectedValue = paciente.genero.ToString();
                    ddlModNacionalidad.SelectedValue = paciente.nacionalidad;
                    ddlModLocalidades.SelectedValue = paciente.Localidad.ToString();
                    ///ddlModProvincias.SelectedValue = paciente.Provincia.ToString();
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

        protected void InsertarPacientes()
        {
            lblAddUserState.Visible = false;

            Paciente paciente = new Paciente();

            paciente.DNI = txbDni.Text.Trim();
            paciente.ObraSocial = int.Parse(ddlObraSocial.SelectedValue);
            paciente.ultimaAtencion = DateTime.Now;
            paciente.Alta = DateTime.Now;
            paciente.nombre = txbNombre.Text.Trim();
            paciente.apellido = txbApellido.Text.Trim();
            paciente.genero = int.Parse(ddlGenero.SelectedValue);
            paciente.fechaNacimiento = Convert.ToDateTime(txbFechaNacimiento.Text.Trim());
            paciente.Direccion = txbDireccion.Text.Trim();
            paciente.Localidad = int.Parse(ddlLocalidades.SelectedValue);
            paciente.nacionalidad = ddlNacionalidad.SelectedValue.ToString();
            paciente.Correo = txbCorreo.Text.Trim();
            paciente.Telefono = txbTelefono.Text.Trim();


            string nombreProcedimiento = "sp_AltaPaciente";
            int filas = gestorPaciente.InsertarPaciente(nombreProcedimiento, paciente);
            if (filas > 0)
            {
                lblAddUserState.Text = "Se agrego correctamente el Paciente";
                lblAddUserState.ForeColor = System.Drawing.Color.Green;
                lblAddUserState.Visible = true;
            }
            else
            {
                lblAddUserState.Text = "Hubo un error durante la carga (no se insertó ninguna fila)";
                lblAddUserState.ForeColor = System.Drawing.Color.Red;
            }
            lblAddUserState.Visible = true;

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
            paciente.Telefono = txbModTelefono.Text.Trim();
            paciente.Direccion = txbModDireccion.Text.Trim();
            paciente.Correo = txbModCorreo.Text.Trim();

            string DNI_VIEJO = (Session["DNI_VIEJO"] as string).Trim();
            string nombreProcedimiento = "sp_ModificarPaciente";
            int filas = gestorPaciente.ModificarPaciente(nombreProcedimiento, paciente, DNI_VIEJO);
            if (filas > 0)
            {
                lblModUser.Text = "Se modifico correctamente el Paciente";
                lblModUser.ForeColor = System.Drawing.Color.Green;
                lblModUser.Visible = true;
            }
        }



        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSeleccionar");
                chk.AutoPostBack = true;
                chk.CheckedChanged += new EventHandler(chkSeleccionar_CheckedChanged);
            }
        }

        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            
            foreach (GridViewRow row in GridView2.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                if (chk != sender)
                {
                    chk.Checked = false;
                }
            }

          
            bool algunoMarcado = false;
            foreach (GridViewRow row in GridView2.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                if (chk.Checked)
                {
                    algunoMarcado = true;
                   
                    break;
                }
            }

            btnMod.Visible = algunoMarcado;
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