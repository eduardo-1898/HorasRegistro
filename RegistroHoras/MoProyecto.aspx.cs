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
    public partial class MoProyecto : System.Web.UI.Page
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
                string sqlCliente = "select * from AST_cliente where Estatus = 1 order by cliente";
                string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                SqlConnection cnn2 = new SqlConnection(Conexion);
                if (Request.QueryString["id"] != null)
                {
                    if (!IsPostBack)
                    {

                        ObtenerHorasProyecto(Request.QueryString["id"].ToString());
                        string sql = "select * from AST_proyecto where id='" + Request.QueryString["id"].ToString() + "'";

                        try
                        {
                            //Carga cliente
                            cnn2.Open();
                            SqlCommand cmd3 = new SqlCommand(sqlCliente, cnn2);
                            SqlDataReader dr1 = cmd3.ExecuteReader();
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
                                txtEncargado.Text = dr["proyecto"].ToString();
                                cbxEstado.Checked = Convert.ToBoolean(dr["Estatus"]);
                                cbxFacturado.Checked = Convert.ToBoolean(dr["Facturado"]);
                                cbxFacturar.Checked = Convert.ToBoolean(dr["Facturar"]);
                                ListaCliente.SelectedValue = dr["idCliente"].ToString();
                                txtEstimado.Text = dr["HorasEstimadas"].ToString();
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

                cnn2.Open();
                SqlCommand cmd5 = new SqlCommand(sqlCliente, cnn2);
                SqlDataReader dr3 = cmd5.ExecuteReader();
                while (dr3.Read())
                {
                    ListItem i = new ListItem(dr3["cliente"].ToString(), dr3["id"].ToString());
                    ListaCliente.Items.Add(i);
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

        private void ObtenerHorasProyecto(string guid) {
            string sqlCliente = "SELECT SUM(cantidadHoras) / 60 as Horas FROM " +
                "(SELECT DATEDIFF(MINUTE, fechaInicio, fechaFinal) as cantidadHoras FROM AST_horasRegistro " +
                "WHERE IdProyecto ='"+guid+"') v";
            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);
            try
            {
                //Carga cliente
                cnn2.Open();
                SqlCommand cmd3 = new SqlCommand(sqlCliente, cnn2);
                SqlDataReader dr1 = cmd3.ExecuteReader();
                while (dr1.Read())
                {
                    txtHoras.Text = dr1["Horas"].ToString();
                }

                cnn2.Close();
            }
            catch (Exception ex)
            {

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
                        sql = "INSERT INTO AST_proyecto (proyecto, Estatus, idCliente, HorasEstimadas, Facturado, Facturar)" +
                            "VALUES(@proyecto, @estatus, @idCliente, @estimadas, @facturado, @facturar)";
                    }
                    else
                    {
                        sql = "update AST_proyecto " +
                        "SET proyecto = @proyecto," +
                        "estatus = @estatus, " +
                        "idCliente = @idCliente, " +
                        "HorasEstimadas = @estimadas, " +
                        "Facturado = @facturado, " +
                        "Facturar = @facturar " +
                        "WHERE id='" + Request.QueryString["id"].ToString() + "'";
                    }

                    {
                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);

                        cnn2.Open();
                        SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.Add("@proyecto", SqlDbType.VarChar).Value = enca;
                        cmd2.Parameters.Add("@estatus", SqlDbType.Bit).Value = estatus;
                        cmd2.Parameters.Add("@facturado", SqlDbType.Bit).Value = cbxFacturado.Checked;
                        cmd2.Parameters.Add("@facturar", SqlDbType.Bit).Value = cbxFacturar.Checked;
                        cmd2.Parameters.Add("@idCliente", SqlDbType.UniqueIdentifier).Value = idCliente;
                        cmd2.Parameters.Add("@estimadas", SqlDbType.VarChar).Value = txtEstimado.Text;
                        cmd2.ExecuteNonQuery();
                        cnn2.Close();

                        Response.Redirect("Proyecto.aspx");

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
                Response.Redirect("Proyecto.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}