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
    public partial class Registro : System.Web.UI.Page
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
                try
                {
                    if ((Context.Session["usuario"] != null && Context.Session["admin"] != null) && Context.Session["admin"].ToString() == "1")
                    {
                        SqlDataSource1.SelectCommand = "SELECT a.id, a.fechaInicio, a.fechaFinal, a.actividad, a.comentario, b.encargado, " +
                            "c.departamento, d.proyecto, e.cliente, f.descripcion, " +
                            "CONVERT(varchar(5), DATEADD(minute, DATEDIFF(MINUTE, a.fechaInicio, a.fechaFinal), 0), 114) as HorasTotales " +
                            "from AST_horasRegistro a, AST_encargado b, AST_departamento c, AST_proyecto d, AST_cliente e, AST_facturable f " +
                            "where a.IdEncargado = b.Id " +
                            "and a.IdDepartamento = c.Id " +
                            "and a.IdProyecto = d.Id " +
                            "and a.IdCliente = e.Id " +
                            "and a.IdFacturable = f.Id " +
                            "AND  a.fechaInicio >= GETDATE()-10 " +
                            "ORDER BY fechaInicio desc";
                        registroTable.DataBind();
                    }
                    else
                    {
                        if (Context.Session["Supervisor"] != null && Context.Session["Supervisor"].ToString() == "1")
                        {
                            SqlDataSource1.SelectCommand = "SELECT a.id, a.fechaInicio, a.fechaFinal, a.actividad, a.comentario, b.encargado, " +
                                "c.departamento, d.proyecto, e.cliente, f.descripcion, CONVERT(varchar(5), " +
                                "DATEADD(minute, DATEDIFF(MINUTE, a.fechaInicio, a.fechaFinal), 0), 114) as HorasTotales " +
                                "from AST_horasRegistro a, AST_encargado b, AST_departamento c, AST_proyecto d, AST_cliente e, AST_facturable f, AST_Usuarios g " +
                                "where a.IdEncargado = b.Id " +
                                "and a.IdDepartamento = c.Id " +
                                "and a.IdProyecto = d.Id " +
                                "and a.IdCliente = e.Id " +
                                "and a.IdFacturable = f.Id " +
                                "and g.idEncargado = b.id " +
                                "AND  datepart(mm, a.fechaInicio) between datepart(mm, getdate())-1 and datepart(mm, getdate()) " +
                                "and b.idDepartamento = '" + departamento(Context.Session["usuario"].ToString()) + "' " +
                                " ORDER BY fechaInicio desc";
                            registroTable.DataBind();
                        }
                        else
                        {
                            SqlDataSource1.SelectCommand = "SELECT a.id, a.fechaInicio, a.fechaFinal, a.actividad, a.comentario, b.encargado, " +
                                "c.departamento, d.proyecto, e.cliente, f.descripcion, " +
                                "CONVERT(varchar(5), DATEADD(minute, DATEDIFF(MINUTE, a.fechaInicio, a.fechaFinal), 0), 114) as HorasTotales " +
                                "from AST_horasRegistro a, AST_encargado b, AST_departamento c, AST_proyecto d, AST_cliente e, AST_facturable f, AST_Usuarios g " +
                                "where a.IdEncargado = b.Id " +
                                "and a.IdDepartamento = c.Id " +
                                "and a.IdProyecto = d.Id " +
                                "and a.IdCliente = e.Id " +
                                "and a.IdFacturable = f.Id " +
                                "and g.idEncargado = b.id " +
                                "AND  datepart(mm, a.fechaInicio) between datepart(mm, getdate())-1 and datepart(mm, getdate()) " +
                                "and g.usuario = '" + Context.Session["usuario"].ToString() + "' " +
                                " ORDER BY fechaInicio desc";
                            registroTable.DataBind();
                        }


                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        public string departamento(string usuario) {
            string sql = "select b.idDepartamento from AST_Usuarios a, AST_encargado b where a.idEncargado = b.Id and a.usuario='" + usuario + "'";
            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);
            cnn2.Open();
            SqlCommand cmd2 = new SqlCommand(sql, cnn2);
            SqlDataReader dr = cmd2.ExecuteReader();
            while (dr.Read()) {
                string dato = dr["idDepartamento"].ToString();
                return dr["idDepartamento"].ToString();
            }
            return string.Empty;
        }

        protected void registro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    int indexReal = index;
                    Response.Redirect("Principal.aspx?id=" + registroTable.DataKeys[indexReal]["id"].ToString());
                }
            }
            catch (Exception)
            {
            }
        }

    }
}