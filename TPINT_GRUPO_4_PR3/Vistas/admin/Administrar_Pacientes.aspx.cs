using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.admin
{
    public partial class Administrar_Pacientes : System.Web.UI.Page
    {
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
    }
}