
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vistas.admin
{
    public partial class Administrar_Pacientes : System.Web.UI.Page
    {
        private GestorPaciente gestorPaciente = new GestorPaciente();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUser.Text = Session["User"] as string;
                btnMod.Visible = false;
                btnBaja.Visible = false;
                setPlaceHolders();
            }
        }
        protected void setPlaceHolders()
        {

            txbDni.Attributes["placeholder"] = "DNI";
            txbNombre.Attributes["placeholder"] = "Nombre";
            txbApellido.Attributes["placeholder"] = "Apellido";
            txbFechaNacimiento.Attributes["placeholder"] = "Fecha de nacimiento";
            txbDireccion.Attributes["placeholder"] = "Dirección";
            txbTelefono.Attributes["placeholder"] = "Teléfono";
            txbCorreo.Attributes["placeholder"] = "Correo electrónico ";
        }
        protected void LimpiarFormularioAltaPaciente()
        {
            txbNombre.Text = "";
            txbApellido.Text = "";
            txbDni.Text = "";
            txbCorreo.Text = "";
            txbTelefono.Text = "";
            txbDireccion.Text = "";
            txbModFechaNacimiento.Text = "";

            ddlObraSocial.ClearSelection();
            ddlGenero.ClearSelection();
            ddlNacionalidad.ClearSelection();
            ddlProvincia.ClearSelection();
            ddlLocalidades.ClearSelection();


          
        }
        protected void btnAlta_Click(object sender, EventArgs e) { mvFormularios.ActiveViewIndex = 0; LimpiarFormularioAltaPaciente(); }
        protected void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvLecturaPaciente.Rows)
                {
                    if (row.FindControl("chkSeleccionar") is CheckBox chk && chk.Checked)
                    {
                        string DNI = row.Cells[1].Text;
                        gestorPaciente.EliminarPaciente(DNI);
                    }
                }
                loadGridPacientes();

                lblEliminado.Visible = true;
                lblEliminado.Text = "Paciente dado de baja correctamente.";
                lblEliminado.ForeColor = System.Drawing.Color.Red;
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
            foreach (GridViewRow row in gvLecturaPaciente.Rows)
            {
                if (row.FindControl("chkSeleccionar") is CheckBox chk && chk.Checked)
                {
                    string DNI = row.Cells[1].Text;
                    Paciente paciente = new Paciente();

                    paciente = gestorPaciente.getPacientePorID(DNI.Trim());
                    Session["DNI_VIEJO"] = paciente.DNI.Trim();
                    mvFormularios.ActiveViewIndex = 2;

                    txbModDni.Text = paciente.DNI;
                    txbModNombre.Text = paciente.nombre;
                    txbModApellido.Text = paciente.apellido;
                    ddlModNacionalidad.SelectedValue = paciente.nacionalidad;
                    ddlModProvincia.SelectedValue = paciente.Provincia.ToString();
                    ddlModLocalidades.SelectedValue = paciente.Localidad.ToString();
                    ddlModObraSocial.SelectedValue = paciente.ObraSocial.idObraSocial.ToString();
                    ddlModGenero.SelectedValue = paciente.sexos.idSexo.ToString();

                    DateTime fechaNac = paciente.fechaNacimiento.Date;
                    txbModFechaNacimiento.Text = fechaNac.ToString("yyyy-MM-dd");
                    txbModDireccion.Text = paciente.Direccion;
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
            lblEliminado.Visible = false;
            mvFormularios.ActiveViewIndex = 3;
            loadGridPacientes();
        }
        protected void btnRegistrarPaciente_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) { InsertarPacientes(); LimpiarFormularioAltaPaciente(); }
            else
            {
                lblAddUserState.Text = "⚠️ Por favor corrija los errores del formulario.";
                lblAddUserState.ForeColor = System.Drawing.Color.OrangeRed;
                lblAddUserState.Visible = true;
            }
        }
        protected void btnModificarPaciente_Click(object sender, EventArgs e) { ModificarPaciente(); btnMod.Visible = false; btnBaja.Visible = false;
        }
        protected void loadGridPacientes()
        {
            List<Paciente> listaPacientes = new GestorPaciente().GetPacientes();
            gvLecturaPaciente.DataSource = listaPacientes;
            gvLecturaPaciente.DataBind();
            lblModUser.Visible = false;
            btnMod.Visible = false;
            btnBaja.Visible = false;
        }
        protected void InsertarPacientes()
        {
            try
            {

                Paciente paciente = new Paciente()
                {
                    DNI = txbDni.Text.Trim(),
                    nombre = txbNombre.Text.Trim(),
                    apellido = txbApellido.Text.Trim(),
                    fechaNacimiento = Convert.ToDateTime(txbFechaNacimiento.Text.Trim()),
                    ultimaAtencion = DateTime.Now,
                    Alta = DateTime.Now,
                    ObraSocial = new ObraSocial { idObraSocial = int.Parse(ddlObraSocial.SelectedValue), Onombre = ddlObraSocial.SelectedItem.Text.Trim() },
                    sexos = new Sexos { idSexo = int.Parse(ddlGenero.SelectedValue), descripcion = ddlGenero.SelectedItem.Text },
                    nacionalidad = ddlNacionalidad.SelectedValue.ToString(),
                    Provincia = int.Parse(ddlProvincia.SelectedValue),
                    Localidad = int.Parse(ddlLocalidades.SelectedValue),
                    Direccion = txbDireccion.Text.Trim(),
                    Correo = txbCorreo.Text.Trim(),
                    Telefono = txbTelefono.Text.Trim(),
                };

                int filas = gestorPaciente.InsertarPaciente(paciente);
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
            catch (Exception ex)
            {
                lblAddUserState.Text = "❌ Error: " + ex.Message;
                lblAddUserState.ForeColor = System.Drawing.Color.Red;
                lblAddUserState.Visible = true;
            }
        }
        protected void ModificarPaciente()
        {
            lblModUser.Visible = false;
            Paciente paciente = new Paciente()
            {

                DNI = txbModDni.Text.Trim(),
                nombre = txbModNombre.Text.Trim(),
                apellido = txbModApellido.Text.Trim(),
                fechaNacimiento = Convert.ToDateTime(txbModFechaNacimiento.Text.Trim()),
                ObraSocial = new ObraSocial { idObraSocial = int.Parse(ddlModObraSocial.SelectedValue), Onombre = ddlModObraSocial.SelectedItem.Text.Trim() },
                sexos = new Sexos { idSexo = int.Parse(ddlModGenero.SelectedValue), descripcion = ddlModGenero.SelectedItem.Text.Trim() },
                ultimaAtencion = DateTime.Now,
                Alta = DateTime.Now,
                nacionalidad = ddlModNacionalidad.SelectedValue.ToString(),
                Provincia = int.Parse(ddlModProvincia.SelectedValue),
                Localidad = int.Parse(ddlModLocalidades.SelectedValue),
                Direccion = txbModDireccion.Text.Trim(),
                Telefono = txbModTelefono.Text.Trim(),
                Correo = txbModCorreo.Text.Trim()
            };
            string DNI_VIEJO = (Session["DNI_VIEJO"] as string)?.Trim();
            int filas = gestorPaciente.ModificarPaciente(paciente, DNI_VIEJO);
            lblModUser.Visible = true;
            if (filas > 0)
            {
                lblModUser.Text = "Se modifico correctamente el Paciente";
                lblModUser.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblModUser.Text = "❌ Error: no se modificó ninguna fila.";
                lblModUser.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvLecturaPaciente.Rows) { if (row.FindControl("chkSeleccionar") is CheckBox chk && chk != sender) { chk.Checked = false; } }
            btnMod.Visible = btnBaja.Visible = (sender as CheckBox)?.Checked == true;
        }
        protected void navigateButton_Click(object sender, CommandEventArgs e)
        {
            switch (e.CommandArgument.ToString())
            {
                case "Admin": Response.Redirect("~/admin/Admin.aspx"); break;
                case "Medicos": Response.Redirect("~/admin/Administrar_Medicos/Administrar_Medicos.aspx"); break;
                case "Pacientes": Response.Redirect("~/admin/Administrar_Pacientes/Administrar_Pacientes.aspx"); break;
                case "Turnos": Response.Redirect("~/admin/Administrar_Turnos/Administrar_Turnos.aspx"); break;
                default: break;
            }
        }
        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e) { ddlLocalidades.DataBind(); }
        protected void ddlModProvincia_SelectedIndexChanged(object sender, EventArgs e) { ddlModLocalidades.DataBind(); }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLecturaPaciente.PageIndex = e.NewPageIndex;
            if (Session["DNI"] != null)
            {
                cargarPacientesxDNI();
            }
            if (Session["Apellido"] != null)
            {
                cargarPacientesxApellido();
            }
            if (Session["DNI"] == null && Session["Apellido"] == null)
            {
                loadGridPacientes();

            }
        }
        protected void ddlBusqueda_Pacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["DNI"] = null;
            Session["Apellido"] = null;
            gvLecturaPaciente.PageIndex = 0;

            loadGridPacientes();

            switch (ddlBusqueda_Pacientes.SelectedIndex)
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
        protected void cargarPacientesxApellido()
        {
            string apellido = Session["Apellido"].ToString();

            gvLecturaPaciente.DataSource = gestorPaciente.FiltrarPacientexApellido(apellido);
            gvLecturaPaciente.DataBind();
        }
        protected void cargarPacientesxDNI()
        {
            string dniPaciente = Session["DNI"].ToString();

            gvLecturaPaciente.DataSource = gestorPaciente.FiltrarPacientexDNI(dniPaciente);
            gvLecturaPaciente.DataBind();
        }
        protected void txbPorApellido_TextChanged(object sender, EventArgs e)
        {
            Session["Apellido"] = txbPorApellido.Text.Trim();
            Session["DNI"] = null;
            gvLecturaPaciente.PageIndex = 0;

            txbPorApellido.Text = "";
            cargarPacientesxApellido();
        }

        protected void txbPorDNI_TextChanged(object sender, EventArgs e)
        {
            Session["DNI"] = txbPorDNI.Text.Trim();
            Session["Apellido"] = null;
            gvLecturaPaciente.PageIndex = 0;

            txbPorDNI.Text = "";
            cargarPacientesxDNI();
        }

    }
}