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
            mvFormularios.ActiveViewIndex = 1;
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
                    txtbModMedicoNombre.Text = medico.nombre;
                    txtbModMedicoApellido.Text = medico.apellido;

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
                medico.Telefono = int.Parse(txbTelefono.Text.Trim());

                string nombreProcedimiento = "sp_AltaMedico";
                int filas = gestorMedico.InsertarMedico(nombreProcedimiento, medico);

                if (filas > 0)
                {
                    lblAddUserState.Text = "Se agregó correctamente el médico";
                    lblAddUserState.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblAddUserState.Text = "Hubo un error durante la carga (no se insertó ninguna fila)";
                    lblAddUserState.ForeColor = System.Drawing.Color.Red;
                }

                lblAddUserState.Visible = true;
            }
            catch (Exception ex)
            {
                lblAddUserState.Text = "❌ Error: " + ex.Message;
                lblAddUserState.ForeColor = System.Drawing.Color.Red;
                lblAddUserState.Visible = true;
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

    }
}