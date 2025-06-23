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
    public partial class Administrar_Medicos : System.Web.UI.Page
    {

        private GestorMedico gestorMedico = new GestorMedico();
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
                foreach (GridViewRow row in gvLecturaMedico.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                    if (chk != null && chk.Checked)
                    {
                        string DNI = row.Cells[2].Text;
                        gestorMedico.EliminarMedico("sp_EliminarMedico", DNI);
                    }
                }
                loadGridMedicos();
                lblAddUserState0.Text = "Médico/s dado/s de baja correctamente.";
                lblAddUserState0.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblAddUserState0.Text = "❌ Error: " + ex.Message;
                lblAddUserState0.ForeColor = System.Drawing.Color.Red;
            }
            btnMod.Visible = false;
            btnBaja.Visible = false;
        }


        protected void btnMod_Click(object sender, EventArgs e)
        {
            //recorro el gridview buscando el cbx seleccionado
            foreach (GridViewRow row in gvLecturaMedico.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");

                //verifico que no sea null el valor y este tildado
                if (chk != null && chk.Checked)
                {
                    //obtengo el DNI del seleccionado
                    string DNI = row.Cells[1].Text;

                    Medico medico = new Medico();

                    medico = gestorMedico.getMedicoPorID(DNI);
                    mvFormularios.ActiveViewIndex = 2;
                    txtbModMedicoDNI.Text = medico.DNI;
                    txtbModMedicoLegajo.Text = medico.Legajo.ToString();
                    txtbModMedicoNombre.Text = medico.nombre;
                    DateTime fechaNac = medico.fechaNacimiento.Date;
                    txtbModFechaNac.Text = fechaNac.ToString("dd-MM-yyyy");
                    txtbModMedicoApellido.Text = medico.apellido;
                    ddlModEspecialidad.SelectedValue = medico.idEspecialidad.ToString();
                    ddlModNacionalidad.SelectedValue = medico.nacionalidad;
                    txtbModMedicoDireccion.Text = medico.Direccion;
                    txtbModMedicoTelefono.Text = medico.Telefono.ToString();
                    txtbModMedicoCorreo.Text = medico.Correo;

                    break;
                }
            }
            btnMod.Visible = false;
            btnBaja.Visible = false;
        }

        protected void btnLectura_Click(object sender, EventArgs e)
        {
            mvFormularios.ActiveViewIndex = 3;
            loadGridMedicos();
        }
        protected void InsertarMedicos()
        {
            try
            {
                Medico medico = new Medico();

                medico.Legajo = txbLegajo.Text.Trim();
                medico.DNI = txbDni.Text.Trim();
                medico.nombre = txbNombre.Text.Trim();
                medico.apellido = txbApellido.Text.Trim();
                medico.idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
                medico.genero = int.Parse(ddlGenero.SelectedValue);
                medico.nacionalidad = ddlNacionalidad.SelectedValue.ToString();
                medico.fechaNacimiento = Convert.ToDateTime(txbFechaNacimiento.Text.Trim());
                medico.Direccion = txbDireccion.Text.Trim();
                medico.Localidad = int.Parse(ddlLocalidades.SelectedValue);
                medico.Correo = txbCorreo.Text.Trim();
                medico.Telefono = txbTelefono.Text.Trim();

                string nombreProcedimiento = "sp_AltaMedico";
                int filas = gestorMedico.InsertarMedico(nombreProcedimiento, medico);

                if (filas > 0)
                {
                    lblAddUserState0.Text = "Se agregó correctamente el médico";
                    lblAddUserState0.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblAddUserState0.Text = "Hubo un error durante la carga (no se insertó ninguna fila)";
                    lblAddUserState0.ForeColor = System.Drawing.Color.Red;
                }

                lblAddUserState0.Visible = true;
            }
            catch (Exception ex)
            {
                lblAddUserState0.Text = "❌ Error: " + ex.Message;
                lblAddUserState0.ForeColor = System.Drawing.Color.Red;
                lblAddUserState0.Visible = true;
            }
        }
        protected void ModificarMedico()
        {
            Medico medico = new Medico();

            medico.DNI = txtbModMedicoDNI.Text.Trim();
            medico.Legajo = txtbModMedicoLegajo.Text.Trim();
            medico.nombre = txtbModMedicoNombre.Text.Trim();
            medico.apellido = txtbModMedicoApellido.Text.Trim();
            medico.fechaNacimiento = Convert.ToDateTime(txtbModFechaNac.Text.Trim());
            medico.idEspecialidad = int.Parse(ddlModEspecialidad.SelectedValue.Trim());
            medico.genero = int.Parse(ddlModGenero.SelectedValue);
            medico.Localidad = int.Parse(ddlModLocalidad.SelectedValue);
            medico.nacionalidad = ddlModNacionalidad.SelectedValue;
            medico.Direccion = txtbModMedicoDireccion.Text;
            medico.Telefono = txtbModMedicoTelefono.Text.Trim();
            medico.Correo = txtbModMedicoCorreo.Text;

            string nombreProcedimiento = "sp_ModificarMedico";
            int filas = gestorMedico.ModificarMedico(nombreProcedimiento, medico);
            if(filas > 0)
            {
                lblModificarMedico.Text = "Se modifico correctamente el Medico.";
                lblModificarMedico.ForeColor = System.Drawing.Color.Green;
                lblModificarMedico.Visible = true;
            }
        }
        protected void loadGridMedicos()
        {
            GestorMedico gestorMedico = new GestorMedico();
            List<Medico> listaMedicos = gestorMedico.GetMedicos();

            gvLecturaMedico.DataSource = listaMedicos;
            gvLecturaMedico.DataBind();
        }
        protected void btnRegistrarMedico_Click(object sender, EventArgs e)
        {
            InsertarMedicos();
        }
        protected void btnModificarMedico_Click(object sender, EventArgs e)
        {
            ModificarMedico();
        }
        protected void btnAdministrarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Medicos.aspx");
        }

        protected void btnAdministrarPacientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Pacientes.aspx");
        }

        protected void btnAdministrarTurnos_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Administrar_Turnos.aspx");
        }

        protected void gvLecturaMedico_RowDataBound(object sender, GridViewRowEventArgs e)
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
            foreach (GridViewRow row in gvLecturaMedico.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                if (chk != sender)
                {
                    chk.Checked = false;
                }


            }
            bool algunoMarcado = false;
            foreach (GridViewRow row in gvLecturaMedico.Rows)
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            mvFormularios.ActiveViewIndex = 1;

        }

    }

}