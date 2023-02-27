using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Formulario_4
{
    public partial class encargado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Session.Timeout = 1440;
            if (Context.Session["admin"] == null)
            {
                Context.Session["sesion"] = "yes";
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                encargadoTable.DataBind();
            }

        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            if (Context.Session["admin"].ToString() == "1")
            {
                HttpContext.Current.Response.Redirect("MoEncargado.aspx");
            }

        }

        protected void Encargado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Context.Session["admin"]!=null) {
                    if (e.CommandName == "editar" && Context.Session["admin"].ToString() == "1")
                    {
                        try
                        {
                            int index = Convert.ToInt32(e.CommandArgument);
                            //int pagina = index / 7;
                            //int indexReal = ((index < 7) ? index : (index - (pagina) * 7));
                            Response.Redirect("MoEncargado.aspx?id=" + encargadoTable.DataKeys[index]["id"].ToString());
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}