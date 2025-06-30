
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
                    ddlModGenero.SelectedValue = paciente.genero.ToString();
                    ddlModNacionalidad.SelectedValue = paciente.nacionalidad;
                    ddlModLocalidades.SelectedValue = paciente.Localidad.ToString();
                    DateTime fechaNac = paciente.fechaNacimiento.Date;
                    txbModFechaNacimiento.Text = fechaNac.ToString("yyyy-MM-dd");
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
            Paciente paciente = new Paciente();

            paciente.DNI = txbDni.Text.Trim();
            paciente.ObraSocial = int.Parse(ddlObraSocial.SelectedValue);
            paciente.ultimaAtencion = DateTime.Now;
            paciente.Alta = DateTime.Now;
            paciente.nombre = txbNombre.Text.Trim();
            paciente.apellido = txbApellido.Text.Trim();
            paciente.genero = int.Parse(ddlGenero.SelectedValue);
            paciente.fechaNacimiento = Convert.ToDateTime(txbFechaNacimiento.Text.Trim());
            paciente.nacionalidad = ddlNacionalidad.SelectedValue.ToString();
            paciente.Localidad = int.Parse(ddlLocalidades.SelectedValue);
            paciente.Direccion = txbDireccion.Text.Trim();
            paciente.Correo = txbCorreo.Text.Trim();
            paciente.Telefono = txbTelefono.Text.Trim();

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
        protected void ModificarPaciente()
        {
            Paciente paciente = new Paciente();

            paciente.DNI = txbModDni.Text.Trim();
            paciente.nombre = txbModNombre.Text.Trim();
            paciente.apellido = txbModApellido.Text.Trim();
            paciente.fechaNacimiento = Convert.ToDateTime(txbModFechaNacimiento.Text.Trim());
            paciente.ObraSocial = int.Parse(ddlModObraSocial.SelectedValue);
            paciente.genero = int.Parse(ddlModGenero.SelectedValue);
            paciente.ultimaAtencion = DateTime.Now;
            paciente.Alta = DateTime.Now;
            paciente.nacionalidad = ddlModNacionalidad.SelectedValue.ToString();
            paciente.Localidad = int.Parse(ddlModLocalidades.SelectedValue);
            paciente.Direccion = txbModDireccion.Text.Trim();
            paciente.Telefono = txbModTelefono.Text.Trim();
            paciente.Correo = txbModCorreo.Text.Trim();

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
        protected void ddlModProvincias_SelectedIndexChanged(object sender, EventArgs e) { ddlModLocalidades.DataBind(); }
    }
}