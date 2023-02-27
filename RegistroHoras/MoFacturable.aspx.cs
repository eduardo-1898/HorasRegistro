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
    public partial class MoFacturable : System.Web.UI.Page
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
                        string sql = "select * from AST_facturable where id='" + Request.QueryString["id"].ToString() + "'";
                        string sqlCliente = "select * from AST_cliente where Estatus = 1 order by cliente";
                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);

                        try
                        {

                            cnn2.Open();
                            SqlCommand cmd3 = new SqlCommand(sqlCliente, cnn2);
                            SqlDataReader dr1 = cmd3.ExecuteReader();
                            ListItem j = new ListItem("Seleccione...", "");
                            ListaCliente.Items.Add(j);
                            while (dr1.Read())
                            {
                                ListItem i = new ListItem(dr1["cliente"].ToString(), dr1["id"].ToString());
                                ListaCliente.Items.Add(i);
                            }
                            cnn2.Close();


                            cnn2.Open();
                            SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                            SqlDataReader dr = cmd2.ExecuteReader();

                            while (dr.Read())
                            {
                                txtEncargado.Text = dr["descripcion"].ToString();
                                cbxEstado.Checked = Convert.ToBoolean(dr["Estatus"]);
                                ListaCliente.SelectedValue = (dr["idCliente"] == null) ? "": dr["idCliente"].ToString();
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
                Guid idCliente = Guid.Parse(ListaCliente.SelectedValue);

                try
                {
                    string sql = String.Empty;

                    if (Request.QueryString["id"] == null)
                    {
                        sql = "INSERT INTO AST_facturable (descripcion, Estatus, idCliente)" +
                            "VALUES(@descripcion, @estatus, @idCliente)";
                    }
                    else
                    {
                        sql = "update AST_facturable " +
                        "SET descripcion = @descripcion," +
                        "estatus = @estatus, idCliente = @idCliente " +
                        "WHERE id='" + Request.QueryString["id"].ToString() + "'";
                    }

                    {
                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);

                        cnn2.Open();
                        SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = enca;
                        cmd2.Parameters.Add("@estatus", SqlDbType.Bit).Value = estatus;
                        cmd2.Parameters.Add("@idCliente", SqlDbType.UniqueIdentifier).Value = idCliente;
                        cmd2.ExecuteNonQuery();
                        cnn2.Close();

                        Response.Redirect("Facturable.aspx");

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
                Response.Redirect("Facturable.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}