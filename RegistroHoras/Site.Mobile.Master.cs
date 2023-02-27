using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Formulario_4
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                encargado.Visible = false;
                departamento.Visible = false;
                proyecto.Visible = false;
                clientes.Visible = false;
                facturabale.Visible = false;
                Control.Visible = false;
                if (Context.Session["admin"] != null)
                {
                    if (Context.Session["admin"].ToString() == "1")
                    {
                        encargado.Visible = true;
                        departamento.Visible = true;
                        proyecto.Visible = true;
                        clientes.Visible = true;
                        facturabale.Visible = true;
                        Control.Visible = true;
                    }
                    else if (Context.Session["encargado"].ToString() == "Jeremy Lewis")
                    {
                        proyecto.Visible = true;
                        clientes.Visible = true;
                        Control.Visible = true;
                    }
                    else if (Context.Session["encargado"].ToString() == "Aileen Arias")
                    {
                        Control.Visible = true;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}