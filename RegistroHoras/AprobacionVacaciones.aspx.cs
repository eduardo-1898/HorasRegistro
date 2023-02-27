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
    public partial class AprobacionVacaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Session.Timeout = 1440;
            if (Context.Session["admin"] == null)
            {
                Context.Session["sesion"] = "yes";
                Response.Redirect("Login.aspx");
            }
            else {
                if (Context.Session["supervisor"].ToString() == "1" && Context.Session["operaciones"].ToString() == "0")
                {
                    ObtenerDatosTableSupervisor();
                }
                encargadoTable.DataBind();
            }
        }
            
        private void ObtenerDatosTableSupervisor() { 
            SqlDataSource1.SelectCommand = "SELECT *,b.Encargado as Solicitante " +
                "FROM AST_VacacionesRegistro a " +
                "JOIN AST_encargado b ON a.idEncargado = b.Id " +
                "WHERE b.idDepartamento = (SELECT idDepartamento FROM AST_encargado where id = '"+Context.Session["idEncargado"].ToString()+"')";
        }


        public Correos DevuelveModeloCorreoOpera(Guid id, string estado)
        {

            var modelResponse = new Correos();
            string sql = "SELECT distinct correo FROM (SELECT correo  " +
                "FROM AST_encargado " +
                "WHERE ID = @id " +
                "UNION ALL " +
                "SELECT b.correo as correo " +
                "FROM AST_Usuarios a " +
                "LEFT JOIN AST_encargado B ON a.idEncargado = b.id " +
                "WHERE Supervisor = 1 " +
                "AND b.idDepartamento = (SELECT idDepartamento from AST_encargado where id = @id) " +
                "UNION ALL " +
                "SELECT correo " +
                "FROM AST_Usuarios a " +
                "LEFT JOIN AST_encargado B ON a.idEncargado = b.id " +
                "WHERE Operaciones = 1) a";

            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);

            try
            {
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                cmd2.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = new Guid(Context.Session["idEncargado"].ToString());
                SqlDataReader dr = cmd2.ExecuteReader();
                modelResponse.Destinatarios = new List<string>();
                while (dr.Read())
                {
                    modelResponse.Destinatarios.Add(dr["correo"].ToString());
                }

                string sql2 = "SELECT * FROM AST_VacacionesRegistro where id = @id";

                SqlConnection cnn3 = new SqlConnection(Conexion);

                cnn3.Open();
                SqlCommand cmd3 = new SqlCommand(sql2, cnn3);
                cmd3.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                SqlDataReader dr2 = cmd3.ExecuteReader();

                while (dr2.Read())
                {
                    modelResponse.Asunto = "Cambio de estado por operaciones solicitud de permiso";
                    modelResponse.Cuerpo = "Estimado usuario. <br/><br/> Se ha " + estado + " la gestión para solicitud de permisos, a continuación se detalla: <br/><br/> " +
                        "<ul><li>Tipo de solicitud: " + dr2["TipoSolicitud"].ToString() + " </li>" +
                        "<li>Fecha de salida: " + ObtenerFechaFormato(dr2["FechaSalida"].ToString()) + "</li>" +
                        "<li>Fecha de entrada: " + ObtenerFechaFormato(dr2["FechaEntrada"].ToString()) + "</li>" +
                        "</ul><br/><br/> " +
                        "Estado de flujo: 3/3<br/><br/>" +
                        "Se ha completado el flujo de permiso.<br/><br/>" +
                        "Saludos cordiales.";
                    cnn2.Close();
                }
                cnn3.Close();


            }
            catch (Exception ex)
            {
            }
            return modelResponse;
        }

        private string ObtenerFechaFormato(string date)
        {
            return Convert.ToDateTime(date).ToString("dd/MM/yyyy");
        }
        public Correos DevuelveModeloCorreo(Guid id, string estado)
        {

            var modelResponse = new Correos();
            string sql = "SELECT distinct correo FROM (SELECT correo  " +
                "FROM AST_encargado " +
                "WHERE ID = @id " +
                "UNION ALL " +
                "SELECT b.correo as correo " +
                "FROM AST_Usuarios a " +
                "LEFT JOIN AST_encargado B ON a.idEncargado = b.id " +
                "WHERE Supervisor = 1 " +
                "AND b.idDepartamento = (SELECT idDepartamento from AST_encargado where id = @id) " +
                "UNION ALL " +
                "SELECT correo " +
                "FROM AST_Usuarios a " +
                "LEFT JOIN AST_encargado B ON a.idEncargado = b.id " +
                "WHERE Operaciones = 1) a";

            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);

            try
            {
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                cmd2.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = new Guid(Context.Session["idEncargado"].ToString());
                SqlDataReader dr = cmd2.ExecuteReader();
                modelResponse.Destinatarios = new List<string>();
                while (dr.Read())
                {
                    modelResponse.Destinatarios.Add(dr["correo"].ToString());
                }

                string sql2 = "SELECT * FROM AST_VacacionesRegistro where id = @id";

                SqlConnection cnn3 = new SqlConnection(Conexion);

                cnn3.Open();
                SqlCommand cmd3 = new SqlCommand(sql2, cnn3);
                cmd3.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                SqlDataReader dr2 = cmd3.ExecuteReader();

                while (dr2.Read())
                {
                    modelResponse.Asunto = "Cambio de estado por supervisor solicitud de permiso";
                    modelResponse.Cuerpo = "Estimado usuario. <br/><br/> Se ha " + estado + " la gestión para solicitud de permisos, a continuación se detalla: <br/><br/> " +
                        "<ul><li>Tipo de solicitud: " + dr2["TipoSolicitud"].ToString() + " </li>" +
                        "<li>Fecha de salida: " + ObtenerFechaFormato(dr2["FechaSalida"].ToString()) + "</li>" +
                        "<li>Fecha de entrada: " + ObtenerFechaFormato(dr2["FechaEntrada"].ToString()) + "</li>" +
                        "<li>Supervisor de revisión: " + Context.Session["encargado"].ToString() + "</li>" +
                        "</ul><br/><br/>" +
                        "Estado de flujo: 2/3<br/><br/>" +
                        "Saludos cordiales.";
                    cnn2.Close();
                }
                cnn3.Close();


            }
            catch (Exception ex)
            {
            }
            return modelResponse;
        }

        private void ActualizadorDatos(string sql, Guid id)
        {
            try
            {
                string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                SqlConnection cnn2 = new SqlConnection(Conexion);

                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                cmd2.ExecuteNonQuery();
                cnn2.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void AprobarSupervisor(Guid id)
        {
            string sql = "UPDATE AST_VacacionesRegistro set AprobacionDepartamento = 1, RevisadoSupervisor = 1 " +
                "WHERE id = @id";
            ActualizadorDatos(sql, id);
            InterfaceEmail.SendEmail(DevuelveModeloCorreo(id, "Aprobado"));
            Response.Redirect("AprobacionVacaciones");
        }

        private void RechazarSupervisor(Guid id) {
            string sql = "UPDATE AST_VacacionesRegistro set AprobacionDepartamento = 0 , RevisadoSupervisor = 1, " +
                "RevisadoOperaciones = 1, AprobacionOperaciones = 0 " +
                "WHERE id = @id";
            ActualizadorDatos(sql, id);
            InterfaceEmail.SendEmail(DevuelveModeloCorreo(id, "Rechazado"));
            Response.Redirect("AprobacionVacaciones");
        }

        private void AprobarOperaciones(Guid id)
        {
            string sql = "UPDATE AST_VacacionesRegistro set AprobacionOperaciones = 1 , RevisadoOperaciones = 1 " +
             "WHERE id = @id";
            ActualizadorDatos(sql, id);
            InterfaceEmail.SendEmail(DevuelveModeloCorreoOpera(id, "Aprobado"));
            Response.Redirect("AprobacionVacaciones");
        }

        private void RechazarOperaciones(Guid id) { 
            string sql = "UPDATE AST_VacacionesRegistro set AprobacionOperaciones = 0 , RevisadoOperaciones = 1 " +
                "WHERE id = @id";
            ActualizadorDatos(sql, id);
            InterfaceEmail.SendEmail(DevuelveModeloCorreoOpera(id, "Rechazado"));
            Response.Redirect("AprobacionVacaciones");
        }

        protected void encargadoTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Guid id = new Guid(encargadoTable.DataKeys[index]["id"].ToString());
            switch (e.CommandName) {
                case "AprobarSupervisor":
                    AprobarSupervisor(id);
                    break;
                case "RechazarSupervisor":
                    RechazarSupervisor(id);
                    break;
                case "AprobarOperaciones":
                    AprobarOperaciones(id);
                    break;
                case "RechazarOperaciones":
                    RechazarOperaciones(id);
                    break;
            }
        }

        protected void encargadoTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string Supervidor = string.Empty;
                string rechazado = string.Empty;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Supervidor = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "RevisadoSupervisor").ToString();
                    rechazado = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "AprobacionDepartamento").ToString();

                    LinkButton btnAprobar = (LinkButton)e.Row.FindControl("aprobarSup");
                    LinkButton btnRechazar = (LinkButton)e.Row.FindControl("rechazarSup");

                    LinkButton btnAprobarOp = (LinkButton)e.Row.FindControl("aprobarOp");
                    LinkButton btnRechazarOp = (LinkButton)e.Row.FindControl("rechazarOp");

                    if (Supervidor == "True")
                    {
                        btnAprobar.Visible = false;
                        btnRechazar.Visible = false; 
                    }
                    if (Supervidor == "True" && rechazado == "False")
                    {
                        btnAprobarOp.Visible = false;
                        btnRechazarOp.Visible = false;
                    }
                    if (Supervidor == "True" && rechazado == "False")
                    {
                        btnAprobarOp.Visible = false;
                        btnRechazarOp.Visible = false;
                    }
                    if (Context.Session["supervisor"].ToString() == "1" && Context.Session["operaciones"].ToString() == "0") {

                        btnAprobarOp.Visible = false;
                        btnRechazarOp.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}