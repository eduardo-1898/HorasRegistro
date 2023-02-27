using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Formulario_4
{
    public partial class Clientes : System.Web.UI.Page
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
                clienteTable.DataBind();
            }

        }

        protected void agregar_Click(object sender, EventArgs e)
        {
            if (Context.Session["admin"].ToString()=="1") {
                HttpContext.Current.Response.Redirect("MoClientes.aspx");
            }
        }

        protected void cliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "editarcli" && Context.Session["admin"].ToString() == "1")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    //int pagina = index / 7;
                    //int indexReal = ((index < 7) ? index : (index - (pagina) * 7));
                    Response.Redirect("MoClientes.aspx?id=" + clienteTable.Rows[index].Cells[3].Text.ToString());

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
            }
        }

    }
    }