using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Formulario_4
{
    public partial class Contraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Session.Timeout = 1440;
            if (Context.Session["admin"] == null)
            {
                Context.Session["sesion"] = "yes";
                Response.Redirect("Login.aspx");
            }
        }

        private void MostrarMensaje(string texto)
        {
            mensaje.Visible = true;
            mensajeError.Visible = false;
            textoMensajeError.InnerHtml = string.Empty;
            textoMensaje.InnerHtml = texto;
        }
        private void MostrarMensajeError(string texto)
        {
            mensaje.Visible = false;
            mensajeError.Visible = true;
            textoMensajeError.InnerHtml = texto;
            textoMensaje.InnerHtml = string.Empty;
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Context.Session["usuario"] == null)
                {
                    MostrarMensajeError("Se ha finalizado el tiempo de sesion, ingrese a la app nuevamente");
                }
                else
                {
                    string sqlSelect = "select contrasena from AST_usuarios " +
                        "where usuario = '" + Context.Session["usuario"].ToString() + "'";
                    string ConexionSelect = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                    SqlConnection cnn3 = new SqlConnection(ConexionSelect);
                    string contrasena = string.Empty;
                    cnn3.Open();
                    SqlCommand cmd3 = new SqlCommand(sqlSelect, cnn3);
                    SqlDataReader dr = cmd3.ExecuteReader();
                    while (dr.Read())
                    {
                        contrasena = dr["contrasena"].ToString();
                    }
                    cnn3.Close();

                    if (contrasena == txtContrasenaVieja.Text)
                    {
                        string sql = "UPDATE AST_usuarios set contrasena = '" + txtContrasenaNueva.Text + "' " +
                        "WHERE usuario = '" + Context.Session["usuario"].ToString() + "'";

                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);
                        cnn2.Open();
                        SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                        cmd2.ExecuteNonQuery();
                        cnn2.Close();

                        MostrarMensaje("Se ha modificado la contraseña con éxito");
                    }
                    else
                    {
                        MostrarMensajeError("La contraseña actual no coincide con la contraseña registrada");
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }
    }
}