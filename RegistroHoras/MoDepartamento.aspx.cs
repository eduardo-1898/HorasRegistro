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
    public partial class MoDepartamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Session.Timeout = 1440;
            if (Context.Session["admin"] == null)
            {
                Context.Session["sesion"] = "yes";
                Response.Redirect("Login.aspx");
            }
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    if (!IsPostBack)
                    {
                        string sql = "select * from AST_departamento where id='" + Request.QueryString["id"].ToString() + "'";
                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);

                        try
                        {
                            cnn2.Open();
                            SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                            SqlDataReader dr = cmd2.ExecuteReader();

                            while (dr.Read())
                            {
                                txtEncargado.Text = dr["departamento"].ToString();
                                cbxEstado.Checked = Convert.ToBoolean(dr["Estatus"]);
                            }
                            cnn2.Close();
                        }
                        catch (Exception ex)
                        {
                            string jsAlert = "Swal.fire({ " +
                                "icon: 'error', " +
                                "title: 'Error', " +
                                "text: 'Ha ocurrido un error al registrar los datos'})";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
                        }
                    }

                }
            }
            catch (Exception)
            {
                string jsAlert = "Swal.fire({ " +
                    "icon: 'error', " +
                    "title: 'Error', " +
                    "text: 'Ha ocurrido un error al registrar los datos'})";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
            }
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            try
            {

                string enca = txtEncargado.Text;
                bool estatus = cbxEstado.Checked;

                try
                {
                    string sql = String.Empty;

                    if (Request.QueryString["id"] == null)
                    {
                        sql = "INSERT INTO AST_departamento (departamento, Estatus)" +
                            "VALUES(@departamento, @estatus)";
                    }
                    else
                    {
                        sql = "update AST_departamento " +
                        "SET departamento = @departamento," +
                        "estatus = @estatus " +
                        "WHERE id='" + Request.QueryString["id"].ToString() + "'";
                    }

                    {
                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);

                        cnn2.Open();
                        SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.Add("@departamento", SqlDbType.VarChar).Value = enca;
                        cmd2.Parameters.Add("@estatus", SqlDbType.Bit).Value = estatus;
                        cmd2.ExecuteNonQuery();
                        cnn2.Close();

                        Response.Redirect("Departamento.aspx");

                    }

                }
                catch (Exception ex)
                {
                    string jsAlert = "Swal.fire({ " +
                        "icon: 'error', " +
                        "title: 'Error', " +
                        "text: 'Ha ocurrido un error al registrar los datos'})";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
                }


            }
            catch (Exception ex)
            {
                string jsAlert = "Swal.fire({ " +
                    "icon: 'error', " +
                    "title: 'Error', " +
                    "text: 'Ha ocurrido un error al registrar los datos'})";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Departamento.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}