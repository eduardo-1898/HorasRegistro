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
    public partial class HorasFacturables : System.Web.UI.Page
    {
        public int Horas = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Context.Session.Timeout = 1440;
                if (Context.Session["admin"] == null)
                {
                    Context.Session["sesion"] = "yes";
                    Response.Redirect("Login.aspx");
                }

                if (!IsPostBack)
                {
                    try
                    {
                        string sqlFacturado = "select * from AST_Facturable where Estatus = 1 order by descripcion";
                        string sqlCliente = "select * from AST_cliente where Estatus = 1 order by cliente";

                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);
                        //Carga facturado
                        cnn2.Open();
                        SqlCommand cmd6 = new SqlCommand(sqlFacturado, cnn2);
                        SqlDataReader dr4 = cmd6.ExecuteReader();
                        ListItem b = new ListItem("Seleccione...", "");
                        ListaFacturable.Items.Add(b);
                        while (dr4.Read())
                        {
                            ListItem i = new ListItem(dr4["descripcion"].ToString(), dr4["id"].ToString());
                            ListaFacturable.Items.Add(i);
                        }
                        cnn2.Close();

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

                    }
                    catch (Exception)
                    {
                    }
                }
                HorasFacturablesTable.DataBind();
            }
            catch (Exception ex)
            {
            }

        }

        protected void btnBuscar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Horas = 0;
                if (dtFechaFin.Text == "" && dtFechaInicio.Text == "" && ListaFacturable.SelectedValue == "" && ListaCliente.SelectedValue == "")
                {
                    string jsAlert = "Swal.fire({ " +
                        "icon: 'warning', " +
                        "title: 'Aviso', " +
                        "text: 'No has seleccionado ningún campo para filtrar'})";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
                }
                else if (dtFechaFin.Text == "" && dtFechaInicio.Text == "")
                {
                    SqlDataSource1.SelectCommand =  "SELECT c.departamento, " +
                        "convert(date, GETDATE()) as fechaFacturada, " +
                        "b.encargado, " +
                        "a.actividad as comentario, " +
                        "convert(date, fechaInicio) as fechaInicio, " +
                        "convert(time, a.fechaInicio) horaInicio, " +
                        "CONVERT(time, a.fechaFinal) as HoraFinal, " +
                        "d.proyecto, " +
                        "convert(varchar(8), cast(DATEADD(HOUR, DATEDIFF(HOUR, a.fechaInicio, a.fechaFinal), 0) as time)) as TotalHoras, " +
                        "convert(int, (select sum(datediff(HOUR, fechaInicio, fechaFinal)) from AST_horasRegistro where id = a.id)) as tiempoTotal, "+
                        "e.cliente, " +
                        "f.descripcion " +
                        "from AST_horasRegistro a, AST_encargado b, AST_departamento c, AST_proyecto d, AST_cliente e, AST_facturable f " +
                        "where a.IdEncargado = b.Id " +
                        "and a.IdDepartamento = c.Id " +
                        "and a.IdProyecto = d.Id " +
                        "and a.IdCliente = e.Id " +
                        "and a.IdCliente like '%" + ListaCliente.SelectedValue.ToString().ToUpper() + "%' " +
                        "and a.IdFacturable like '%" + ListaFacturable.SelectedValue.ToString().ToUpper() + "%' " +
                        "and a.IdFacturable = f.Id " +
                        "ORDER BY fechaInicio desc";
                }
                else {

                    string fi = DateTime.Parse(dtFechaInicio.Text).ToString("yyyy-MM-dd");
                    string ff = DateTime.Parse(dtFechaFin.Text).ToString("yyyy-MM-dd");
                    SqlDataSource1.SelectCommand = "SELECT c.departamento, " +
                        "convert(date, GETDATE()) as fechaFacturada, " +
                        "b.encargado, " +
                        "a.actividad as comentario, " +
                        "convert(date, fechaInicio) as fechaInicio, " +
                        "convert(time, a.fechaInicio) horaInicio, " +
                        "CONVERT(time, a.fechaFinal) as HoraFinal, " +
                        "d.proyecto, " +
                        "convert(varchar(8), cast(DATEADD(HOUR, DATEDIFF(HOUR, a.fechaInicio, a.fechaFinal), 0) as time)) as TotalHoras, " +
                        "convert(int, (select sum(datediff(HOUR, fechaInicio, fechaFinal)) from AST_horasRegistro where id = a.id)) as tiempoTotal, " +
                        "e.cliente, " +
                        "f.descripcion " +
                        "from AST_horasRegistro a, AST_encargado b, AST_departamento c, AST_proyecto d, AST_cliente e, AST_facturable f " +
                        "where a.IdEncargado = b.Id " +
                        "and a.IdDepartamento = c.Id " +
                        "and a.IdProyecto = d.Id " +
                        "and a.IdCliente = e.Id " +
                        "and a.IdCliente like '%" + ListaCliente.SelectedValue.ToString().ToUpper() + "%' " +
                        "and a.IdFacturable like '%" + ListaFacturable.SelectedValue.ToString().ToUpper() + "%' " +
                        "and CONVERT(date, a.fechaInicio) between '" + fi + "' and '" + ff + "' " +
                        "and a.IdFacturable = f.Id " +
                        "ORDER BY fechaInicio desc";
                }

            }
            catch (Exception ex)
            {

            }

            HorasFacturablesTable.DataBind();
        }

        protected void ListaFacturable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from AST_facturable where id='" + ListaFacturable.SelectedValue + "'";
                string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                SqlConnection cnn2 = new SqlConnection(Conexion);
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                SqlDataReader dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    ListaCliente.SelectedValue = (dr["idCliente"] == null) ? "" : dr["idCliente"].ToString();
                }
                cnn2.Close();
            }
            catch (Exception ex)
            {
            }
        }

        protected void HorasFacturablesTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                Horas += Convert.ToInt32(e.Row.Cells[0].Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Horas Totales";
                e.Row.Cells[2].Text = Convert.ToString(Horas);
            }
        }
    }
}