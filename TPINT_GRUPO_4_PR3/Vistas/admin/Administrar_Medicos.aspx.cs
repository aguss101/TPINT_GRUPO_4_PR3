using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Entidades;
using Negocio;

namespace Vistas.admin
{
    public partial class Administrar_Medicos : System.Web.UI.Page
    {
        private GestorMedico gestorMedico = new GestorMedico();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUser.Text = Session["User"] as string;
                btnMod.Visible = false;
                btnBaja.Visible = false;
                txbLegajo.Attributes["placeholder"] = "Legajo del medico";
                txbLegajo.Text = string.Empty;
            }
        }
        protected void btnAlta_Click(object sender, EventArgs e)
        {
            lblAddUserState0.Visible = false;
            mvFormularios.ActiveViewIndex = 0;
        }
        protected void btnBaja_Click(object sender, EventArgs e)
        {
            lblAddUserState0.Visible = false;
            try
            {
                foreach (GridViewRow row in gvLecturaMedico.Rows)
                {
                    if (row.FindControl("chkSeleccionar") is CheckBox chk && chk.Checked)
                    {
                        string DNI = row.Cells[2].Text;
                        gestorMedico.EliminarMedico(DNI);
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
            foreach (GridViewRow row in gvLecturaMedico.Rows)
            {
                if (row.FindControl("chkSeleccionar") is CheckBox chk && chk.Checked)
                {
                    string DNI = row.Cells[2].Text;
                    Medico medico = new Medico() { Usuario = new Usuario() };
                    medico = gestorMedico.getMedicoPorID(DNI);
                    lblAddUserState0.Visible = false;
                    Session["DNI_VIEJO"] = medico.DNI;
                    Session["LEGAJO_VIEJO"] = medico.Legajo;
                    mvFormularios.ActiveViewIndex = 2;
                    txtbModMedicoDNI.Text = medico.DNI;
                    txtbModMedicoLegajo.Text = medico.Legajo.ToString();
                    txtbModMedicoNombre.Text = medico.nombre;
                    txtbModMedicoApellido.Text = medico.apellido;
                    DateTime fechaNac = medico.fechaNacimiento.Date;
                    txtbModFechaNac.Text = fechaNac.ToString("dd-MM-yyyy");
                    ddlModNacionalidad.SelectedValue = medico.nacionalidad;
                    if (ddlModEspecialidad.Items.FindByValue(medico.Especialidad.idEspecialidad.ToString()) != null) { ddlModEspecialidad.SelectedValue = medico.Especialidad.idEspecialidad.ToString(); }
                    //ddlModEspecialidad.SelectedValue = medico.Especialidad.idEspecialidad.ToString();
                    ddlModGenero.SelectedValue = medico.sexos.idSexo.ToString();
                    //if (ddlModGenero.Items.FindByValue(medico.sexos.idSexo.ToString()) != null)  { ddlGenero.SelectedValue = medico.sexos.idSexo.ToString(); }
                    txtbModMedicoDireccion.Text = medico.Direccion;
                    txtbModMedicoTelefono.Text = medico.Telefono.ToString();
                    txtbModMedicoCorreo.Text = medico.Correo;
                    txtbModMedicoUsuario.Text = medico.Usuario.NombreUsuario;
                    txtbModMedicoContrasenia.Text = medico.Usuario.contrasenia;
                    break;
                }
            }
            btnMod.Visible = false;
            btnBaja.Visible = false;
        }
        protected void btnLectura_Click(object sender, EventArgs e)
        {
            lblAddUserState0.Visible = false;
            mvFormularios.ActiveViewIndex = 3;
            loadGridMedicos();
        }
        protected void InsertarMedicos()
        {
            lblAddUserState0.Visible = false;
            try
            {
                /// VALIDACIONES - INSERTAR
                /// Es necesario pasar todo esto a RFV(RequiredFieldValidator) este codigo es MUY TOSCO
                if (txbDni.Text.Trim() == "" || txbLegajo.Text.Trim() == "" || txbNombre.Text.Trim() == "" || txbApellido.Text.Trim() == "" || txbDireccion.Text.Trim() == "" || txbCorreo.Text.Trim() == "" || txbTelefono.Text.Trim() == "")
                {
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = "Faltan campos por rellenar";
                }
                string mensajeError;

                if (!Regex.IsMatch(txbApellido.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$"))
                {
                    mensajeError = "⚠️ Apellido inválido. Solo se permiten letras.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (string.IsNullOrEmpty(ddlEspecialidad.SelectedValue))
                {
                    mensajeError = "⚠️ Debe seleccionar una especialidad.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (string.IsNullOrEmpty(ddlGenero.SelectedValue))
                {
                    mensajeError = "⚠️ Debe seleccionar un género.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (!DateTime.TryParse(txbFechaNacimiento.Text.Trim(), out DateTime fechaNac))
                {
                    mensajeError = "⚠️ Fecha de nacimiento inválida.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                var edadMin = DateTime.Today.AddYears(-120);
                var edadMax = DateTime.Today.AddYears(-18);
                if (fechaNac < edadMin || fechaNac > edadMax)
                {
                    mensajeError = "⚠️ La edad debe ser entre 18 y 120 años.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (string.IsNullOrEmpty(ddlNacionalidad.SelectedValue))
                {
                    mensajeError = "⚠️ Debe seleccionar una nacionalidad.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (string.IsNullOrEmpty(ddlProvincia.SelectedValue))
                {
                    mensajeError = "⚠️ Debe seleccionar una provincia.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }

                if (string.IsNullOrEmpty(ddlLocalidades.SelectedValue))
                {
                    mensajeError = "⚠️ Debe seleccionar una localidad.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txbDireccion.Text))
                {
                    mensajeError = "⚠️ Dirección obligatoria.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (!Regex.IsMatch(txbCorreo.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    mensajeError = "⚠️ Correo inválido.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (!Regex.IsMatch(txbTelefono.Text.Trim(), @"^\d{6,15}$"))
                {
                    mensajeError = "⚠️ Teléfono inválido. Con 10 digitos. Formato: 11 1234 1234";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txbUsuario.Text))
                {
                    mensajeError = "⚠️ Nombre de usuario obligatorio.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txbContrasenia.Text))
                {
                    mensajeError = "⚠️ Contraseña obligatoria.";
                    lblAddUserState0.Visible = true;
                    lblAddUserState0.Text = mensajeError;
                    return;
                }
                else ///FIN VALIDACIONES
                {

                    Medico medico = new Medico()
                    {
                        Usuario = new Usuario()
                        {
                            NombreUsuario = txbUsuario.Text.Trim(),
                            contrasenia = txbContrasenia.Text.Trim(),
                            alta = Convert.ToDateTime(DateTime.Now),
                            ultimoIngreso = Convert.ToDateTime(DateTime.Now),
                            idRol = 2
                        },
                        Legajo = txbLegajo.Text.Trim(),
                        DNI = txbDni.Text.Trim(),
                        nombre = txbNombre.Text.Trim(),
                        apellido = txbApellido.Text.Trim(),
                        Especialidad = new Especialidad { idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue), descripcion = ddlEspecialidad.SelectedItem.Text.Trim() },
                        sexos = new Sexos { idSexo = int.Parse(ddlGenero.SelectedValue), descripcion = ddlGenero.SelectedItem.Text },
                        nacionalidad = ddlNacionalidad.SelectedValue.ToString(),
                        fechaNacimiento = Convert.ToDateTime(txbFechaNacimiento.Text.Trim()),
                        Direccion = txbDireccion.Text.Trim(),
                        Localidad = int.Parse(ddlLocalidades.SelectedValue),
                        Correo = txbCorreo.Text.Trim(),
                        Telefono = txbTelefono.Text.Trim()
                    };


                    int filas = gestorMedico.InsertarMedico(medico, medico.Usuario);

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
            Medico medico = new Medico()
            {
                Usuario = new Usuario() { NombreUsuario = txtbModMedicoUsuario.Text.Trim(), contrasenia = txtbModMedicoContrasenia.Text.Trim() },
                DNI = txtbModMedicoDNI.Text.Trim(),
                Legajo = txtbModMedicoLegajo.Text.Trim(),
                nombre = txtbModMedicoNombre.Text.Trim(),
                apellido = txtbModMedicoApellido.Text.Trim(),
                fechaNacimiento = Convert.ToDateTime(txtbModFechaNac.Text.Trim()),
                Especialidad = new Especialidad { idEspecialidad = int.Parse(ddlModEspecialidad.SelectedValue), descripcion = ddlModEspecialidad.SelectedItem.Text.Trim() },
                sexos = new Sexos { idSexo = int.Parse(ddlModGenero.SelectedValue), descripcion = ddlModGenero.SelectedItem.Text.Trim() },
                nacionalidad = ddlModNacionalidad.SelectedValue,
                Localidad = int.Parse(ddlModLocalidad.SelectedValue),
                Direccion = txtbModMedicoDireccion.Text,
                Telefono = txtbModMedicoTelefono.Text.Trim(),
                Correo = txtbModMedicoCorreo.Text
            };
            string DNI_VIEJO = (Session["DNI_VIEJO"] as string).Trim();
            string LEGAJO_VIEJO = (Session["LEGAJO_VIEJO"] as string).Trim();

            if (medico.DNI == "" || medico.Legajo == "" || medico.nombre == "" || medico.apellido == "")
            {
                lblModificarMedico.Text = "⚠️ Faltan datos obligatorios.";
                lblModificarMedico.ForeColor = System.Drawing.Color.OrangeRed;
                lblModificarMedico.Visible = true;
                return;
            }

            DateTime edadMinima = DateTime.Today.AddYears(-18);
            DateTime edadMaxima = DateTime.Today.AddYears(-120);

            if (medico.fechaNacimiento > edadMinima || medico.fechaNacimiento < edadMaxima)
            {
                lblModificarMedico.Text = "⚠️ Fecha de nacimiento inválida.";
                lblModificarMedico.ForeColor = System.Drawing.Color.OrangeRed;
                lblModificarMedico.Visible = true;
                return;
            }
            int filas = gestorMedico.ModificarMedico(medico, medico.Usuario, DNI_VIEJO, LEGAJO_VIEJO);

            if (filas > 0)
            {
                lblModificarMedico.Text = "Se modifico correctamente el Medico.";
                lblModificarMedico.ForeColor = System.Drawing.Color.Green;
                lblModificarMedico.Visible = true;
            }
            else
            {
                lblModificarMedico.Text = "❌ Error: no se modificó ninguna fila.";
                lblModificarMedico.ForeColor = System.Drawing.Color.Red;
                lblModificarMedico.Visible = true;
            }
        }
        protected void loadGridMedicos()
        {
            List<Medico> listaMedicos = new GestorMedico().GetMedicos();
            gvLecturaMedico.DataSource = listaMedicos;
            gvLecturaMedico.DataBind();
        }
        protected void btnRegistrarMedico_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertarMedicos();
            }
        }
        protected void btnModificarMedico_Click(object sender, EventArgs e)
        {
            ModificarMedico();
        }
        protected void navigateButton_Click(object sender, CommandEventArgs e)
        {
            switch (e.CommandArgument.ToString())
            {
                case "Medicos": Response.Redirect("/admin/Administrar_Medicos.aspx"); break;
                case "Pacientes": Response.Redirect("/admin/Administrar_Pacientes.aspx"); break;
                case "Turnos": Response.Redirect("/admin/Administrar_Turnos.aspx"); break;
                default: break;
            }
        }
        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvLecturaMedico.Rows)
            {
                if (row.FindControl("chkSeleccionar") is CheckBox chk && chk != sender)
                { chk.Checked = false; }
            }
            btnMod.Visible = btnBaja.Visible = (sender as CheckBox)?.Checked == true;
        }
        protected void btnEliminar_Click(object sender, EventArgs e) { mvFormularios.ActiveViewIndex = 1; }
        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e) { ddlLocalidades.DataBind(); }
        protected void ddlModProvincia_SelectedIndexChanged(object sender, EventArgs e) { ddlModLocalidad.DataBind(); }
    }
}