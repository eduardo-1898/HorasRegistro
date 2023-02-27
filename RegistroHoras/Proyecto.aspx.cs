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
    public partial class Proyecto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Session.Timeout = 1440;
            if (Context.Session["admin"] == null)
            {
                Context.Session["sesion"] = "yes";
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack){
                proyectoTable.DataBind();
            }

        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Context.Session["admin"].ToString() == "1" || Context.Session["supervisor"].ToString() == "1")
                {
                    Response.Redirect("MoProyecto.aspx");
                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void proyecto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit" && (Context.Session["admin"].ToString() == "1" || Context.Session["supervisor"].ToString() == "1"))
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    string pruebaMiedo = proyectoTable.DataKeys[index]["id"].ToString();
                    Response.Redirect("MoProyecto.aspx?id=" + pruebaMiedo);
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}