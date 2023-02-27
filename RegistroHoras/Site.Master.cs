using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Formulario_4
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Context.Session["encargado"] != null)
                {
                    usuarioBarra.InnerText = Context.Session["encargado"].ToString();
                }
                else
                {
                    usuarioBarra.InnerText = "Desconectado";
                }
                encargado.Visible = false;
                departamento.Visible = false;
                proyecto.Visible = false;
                clientes.Visible = false;
                facturabale.Visible = false;
                Control.Visible = false;
                A3.Visible = false;

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
                    else if (Context.Session["idEncargado"].ToString().ToUpper() == "DA410870-A864-4ED0-B099-2C1C18907E0A" || Context.Session["idEncargado"].ToString().ToLower() == "5c3cbc11-847f-4a9a-ac74-60a27b3e9c53" ) { 
                        Control.Visible = true;
                    }
                }
                if (Context.Session["supervisor"].ToString() == "1" || Context.Session["operaciones"].ToString() == "1")
                {
                    proyecto.Visible = true;
                    A3.Visible = true;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}