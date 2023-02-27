using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Formulario_4
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Session["sesion"] != null) {
                string jsAlert = "Swal.fire({ " +
                    "icon: 'warning', " +
                    "title: 'Tiempo agotado', " +
                    "text: 'Se ha cerrado la sesión por inactividad'})";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
            }
            Context.Session.Clear();
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string sql = "select a.*, b.encargado from AST_Usuarios a, AST_encargado b where b.id = a.idEncargado and a.usuario='" + txtUsuario.Text + "' and a.contrasena = '" + txtContrasena.Text + "' and b.Estatus = 1 ";
            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();

            SqlConnection cnn2 = new SqlConnection(Conexion);

            try
            {
                bool login = false;
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                SqlDataReader dr = cmd2.ExecuteReader();

                while (dr.Read())
                {
                    if (dr["usuario"].ToString().ToUpper() == txtUsuario.Text.ToUpper() && dr["contrasena"].ToString().ToUpper() == txtContrasena.Text.ToUpper()) {
                        login = true;
                        Context.Session["admin"] = (dr["admin"].ToString() == "False") ?"0":"1";
                        Context.Session["usuario"] = txtUsuario.Text;
                        Context.Session["supervisor"] = (dr["Supervisor"].ToString() == "False") ? "0" : "1";
                        Context.Session["encargado"] = dr["encargado"].ToString();
                        Context.Session["operaciones"] = (dr["Operaciones"].ToString() == "False") ?"0":"1";
                        Context.Session["idEncargado"] = dr["idEncargado"].ToString();
                        Context.Session.Timeout = 1440;
                    }
                }
                cnn2.Close();

                if (login)
                {
                    Response.Redirect("General.aspx");
                }
                else {
                    string jsAlert = "Swal.fire({ " +
                        "icon: 'error', " +
                        "title: 'Error', " +
                        "text: 'Contraseña o usuario incorrecto'})";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
                }
                

            }
            catch (Exception ex)
            {
                string jsAlert = "Swal.fire({ " +
                    "icon: 'error', " +
                    "title: 'Error', " +
                    "text: 'Contraseña o usuario incorrecto'})";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
            }
        }
    }
}