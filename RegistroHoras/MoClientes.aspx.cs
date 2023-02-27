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
    public partial class MoClientes : System.Web.UI.Page
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
                        string sql = "select * from AST_cliente where id='" + Request.QueryString["id"].ToString() + "'";
                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);

                        try
                        {

                            cnn2.Open();
                            SqlCommand cmd2 =new SqlCommand(sql,cnn2);
                            SqlDataReader dr = cmd2.ExecuteReader();

                            while (dr.Read())
                            {
                                txtCliente.Text = dr["cliente"].ToString();
                                txtRepresentante.Text = dr["Representante"].ToString();
                            }
                            cnn2.Close();
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

                }
            }
            catch (Exception)
            {
                string jsAlert = "Swal.fire({ " +
                    "icon: 'error', " +
                    "title: 'Error', " +
                    "text: 'Ha ocurrido un error al cargar los datos'})";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
            }

        }
        protected void Guardar_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = String.Empty;

                if (Request.QueryString["id"] == null)
                {
                    sql = "insert into AST_cliente(cliente,representante,Estatus) values" +
                    " (@cliente,@representante,@Estatus)";
                }
                else
                {
                    sql = "update AST_cliente " +
                    "SET cliente = @cliente," +
                    "representante = @representante, " +
                    "Estatus = @Estatus " +
                    "WHERE id='" + Request.QueryString["id"].ToString() + "'";
                }
                    string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                    SqlConnection cnn2 = new SqlConnection(Conexion);

                    cnn2.Open();
                    SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                    cmd2.CommandType = System.Data.CommandType.Text;
                    cmd2.Parameters.Add("@cliente", SqlDbType.VarChar).Value = txtCliente.Text;
                    cmd2.Parameters.Add("@representante", SqlDbType.VarChar).Value = txtRepresentante.Text;
                    cmd2.Parameters.Add("@Estatus", SqlDbType.Bit).Value = true;
                    cmd2.ExecuteNonQuery();
                    cnn2.Close();
                    Response.Redirect("Clientes.aspx");

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
            Response.Redirect("Clientes.aspx");

        }
    }
}