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
        //protected void InsertarMedicos()
        //{
        //    Medico medico = new Medico();

        //    medico.Legajo = txbLegajo.Text.Trim();
        //    medico.DNI = txbDni.Text.Trim();
        //    medico.nombre = txbNombre.Text.Trim();
        //    medico.apellido = txbApellido.Text.Trim();
        //    medico.idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
        //    medico.genero = int.Parse(ddlGenero.SelectedValue);
        //    medico.nacionalidad = ddlNacionalidad.SelectedValue.ToString();
        //    medico.fechaNacimiento = Convert.ToDateTime(txbFechaNacimiento.Text.Trim());
        //    medico.Direccion = txbDireccion.Text.Trim();
        //    medico.Localidad = int.Parse(ddlLocalidades.SelectedValue);
        //    medico.Correo = txbCorreo.Text.Trim();
        //    medico.Telefono = int.Parse(txbTelefono.Text.Trim());


        //    string nombreProcedimiento = "sp_AltaMedico";
        //    int filas = gestorMedico.InsertarMedico(nombreProcedimiento, medico);
        //    if (filas > 0)
        //    {
        //        lblAddUserState.Text = "Se agrego correctamente el médico";
        //        lblAddUserState.ForeColor = System.Drawing.Color.Green;
        //        lblAddUserState.Visible = true;
        //    }

        //    else
        //        lblAddUserState.Text = "Hubo un error durante la carga";
        //    lblAddUserState.ForeColor = System.Drawing.Color.Red;
        //    lblAddUserState.Visible = true;

        //}

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

        protected void btnRegistrarMedico_Click(object sender, EventArgs e)
        {
            InsertarMedicos();
        }
    }
}