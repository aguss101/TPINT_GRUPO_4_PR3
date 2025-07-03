
﻿using System;
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
            }
        }
        protected void btnAlta_Click(object sender, EventArgs e) { mvFormularios.ActiveViewIndex = 0; }
        protected void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GridView2.Rows)
                {
                    if (row.FindControl("chkSeleccionar") is CheckBox chk && chk.Checked)
                    {
                        string DNI = row.Cells[1].Text;
                        gestorPaciente.EliminarPaciente(DNI);
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
                    ddlModGenero.SelectedValue = paciente.sexos.idSexo.ToString();
                    ddlModNacionalidad.SelectedValue = paciente.nacionalidad;
                    ddlModLocalidades.SelectedValue = paciente.Localidad.ToString();
                    ddlModObraSocial.SelectedValue = paciente.ObraSocial.ToString();
                    if (ddlGenero.Items.FindByValue(paciente.sexos.idSexo.ToString()) != null) { ddlGenero.SelectedValue = paciente.sexos.idSexo.ToString(); }
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
            mvFormularios.ActiveViewIndex = 3;
            loadGridPacientes();
        }
        protected void btnRegistrarPaciente_Click(object sender, EventArgs e) { InsertarPacientes(); }
        protected void btnModificarPaciente_Click(object sender, EventArgs e) { ModificarPaciente(); }
        protected void loadGridPacientes()
        {
            List<Paciente> listaPacientes = new GestorPaciente().GetPacientes();
            GridView2.DataSource = listaPacientes;
            GridView2.DataBind();
        }
        protected void InsertarPacientes()
        {
            lblAddUserState.Visible = false;
            try
            {

                Paciente paciente = new Paciente()
                {
                    DNI = txbDni.Text.Trim(),
                    ObraSocial = int.Parse(ddlObraSocial.SelectedValue),
                    ultimaAtencion = DateTime.Now,
                    Alta = DateTime.Now,
                    nombre = txbNombre.Text.Trim(),
                    apellido = txbApellido.Text.Trim(),
                    sexos = new Sexos { idSexo = int.Parse(ddlGenero.SelectedValue), descripcion = ddlGenero.SelectedItem.Text },
                    fechaNacimiento = Convert.ToDateTime(txbFechaNacimiento.Text.Trim()),
                    nacionalidad = ddlNacionalidad.SelectedValue.ToString(),
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
            Paciente paciente = new Paciente()
            {

                DNI = txbModDni.Text.Trim(),
                nombre = txbModNombre.Text.Trim(),
                apellido = txbModApellido.Text.Trim(),
                fechaNacimiento = Convert.ToDateTime(txbModFechaNacimiento.Text.Trim()),
                ObraSocial = int.Parse(ddlModObraSocial.SelectedValue),
                sexos = new Sexos { idSexo = int.Parse(ddlGenero.SelectedValue), descripcion = ddlGenero.SelectedItem.Text },
                ultimaAtencion = DateTime.Now,
                Alta = DateTime.Now,
                nacionalidad = ddlModNacionalidad.SelectedValue.ToString(),
                Localidad = int.Parse(ddlModLocalidades.SelectedValue),
                Direccion = txbModDireccion.Text.Trim(),
                Telefono = txbModTelefono.Text.Trim(),
                Correo = txbModCorreo.Text.Trim()
            };
            string DNI_VIEJO = (Session["DNI_VIEJO"] as string)?.Trim();
            int filas = gestorPaciente.ModificarPaciente(paciente, DNI_VIEJO);
            if (filas > 0)
            {
                lblModUser.Text = "Se modifico correctamente el Paciente";
                lblModUser.ForeColor = System.Drawing.Color.Green;
                lblModUser.Visible = true;
            }
        }
        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView2.Rows) { if (row.FindControl("chkSeleccionar") is CheckBox chk && chk != sender) { chk.Checked = false; } }
            btnMod.Visible = btnBaja.Visible = (sender as CheckBox)?.Checked == true;
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
        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e) { ddlLocalidades.DataBind(); }
        protected void ddlModProvincia_SelectedIndexChanged(object sender, EventArgs e) { ddlModLocalidades.DataBind(); }
    }
}