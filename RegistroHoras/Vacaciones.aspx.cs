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
    public partial class Vacaciones : System.Web.UI.Page
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
                ObtenerCantidadVacaciones();
            }
        }

        private void ObtenerCantidadVacaciones() {

            string sql = "SELECT FechaIngreso, " +
                "DATEDIFF(MONTH, FechaIngreso, GETDATE())-(SELECT isnull(SUM(CantidadDias), 0) FROM AST_VacacionesRegistro WHERE idEncargado = @id AND AprobacionOperaciones = 1 ) as Cantidad " +
                "FROM AST_encargado " +
                "WHERE id = @id";
            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);

            try
            {
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                cmd2.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = new Guid(Context.Session["idEncargado"].ToString());
                SqlDataReader dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    txtFechaIngreso.Text = Convert.ToDateTime(
                        dr["FechaIngreso"].ToString() == "" ?//Condición
                            DateTime.Now.ToString() ://true
                            dr["FechaIngreso"].ToString()//false
                        ).ToString("yyyy-MM-dd");

                    txtVacacionesDisponibles.Text = dr["Cantidad"].ToString()==""?"0": dr["Cantidad"].ToString();
                }
                cnn2.Close();
            }
            catch (Exception)
            {
            }
        }

        private void LimpiarFormulario() {
            dtFechaFin.Text = "";
            dtFechaInicio.Text = "";
            txtcantidad.Text = "";
            txtObservacion.Text = "";
        }

        private void InsertarSolicitud() {

            string sql = "INSERT INTO AST_VacacionesRegistro (FechaSalida, FechaEntrada, idEncargado, TipoSolicitud, CantidadDias, DescripcionSolicitud, AprobacionDepartamento, AprobacionOperaciones, FechaSolicitud, RevisadoSupervisor, RevisadoOperaciones)" +
                "VALUES (@FechaSalida, @FechaEntrada, @idEncargado, @TipoSolicitud, @CantidadDias, @DescripcionSolicitud, @AprobacionDepartamento, @AprobacionOperaciones, GETDATE(), 0,0)";

            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);

            try
            {
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.Parameters.Add("@FechaSalida", SqlDbType.VarChar).Value = dtFechaInicio.Text;
                cmd2.Parameters.Add("@FechaEntrada", SqlDbType.VarChar).Value = dtFechaFin.Text;
                cmd2.Parameters.Add("@idEncargado", SqlDbType.UniqueIdentifier).Value = new Guid(Context.Session["idEncargado"].ToString());
                cmd2.Parameters.Add("@TipoSolicitud", SqlDbType.VarChar).Value = slctTipo.SelectedItem.Text;
                cmd2.Parameters.Add("@CantidadDias", SqlDbType.Int).Value = Convert.ToInt32(txtcantidad.Text);
                cmd2.Parameters.Add("@DescripcionSolicitud", SqlDbType.VarChar).Value = txtObservacion.Text;
                cmd2.Parameters.Add("@AprobacionDepartamento", SqlDbType.Bit).Value = false;
                cmd2.Parameters.Add("@AprobacionOperaciones", SqlDbType.Bit).Value = false;
                cmd2.ExecuteNonQuery();
                cnn2.Close();

            }
            catch (Exception ex)
            {
            }
        }

        //Rutina para obtener los días de vacaciones tomando en cuenta que no hayan días ni sabados ni domingos

        private string getDaysDiferences(string fecha1, string fecha2)
        {
            int cantidadDiasValidos = 0;

            DateTime fechaFin = Convert.ToDateTime(fecha1);
            DateTime fechaInicio = Convert.ToDateTime(fecha2);
            TimeSpan Diff_dates = fechaFin.Subtract(fechaInicio);

            for (int i = 0; i < Diff_dates.Days+1; i++)
            {
                if (fechaInicio.ToString("dddd")!= "sábado" && fechaInicio.ToString("dddd") != "domingo")
                {
                    cantidadDiasValidos++;
                }
                fechaInicio = fechaInicio.AddDays(1);
            }

            return cantidadDiasValidos.ToString();
        }

        //seccion para actualizaciones en el desarrollo
        protected void dtFechaFin_TextChanged(object sender, EventArgs e)
        {
            if (dtFechaInicio.Text != "" && dtFechaFin.Text != "") {
                txtcantidad.Text = getDaysDiferences(dtFechaFin.Text, dtFechaInicio.Text);

                if (Convert.ToInt32(getDaysDiferences(dtFechaFin.Text, dtFechaInicio.Text)) > Convert.ToInt32(txtVacacionesDisponibles.Text))
                {
                    string jsAlert = "Swal.fire({ " +
                        "icon: 'info', " +
                        "title: 'Observación', " +
                        "text: 'Se han solicitado más días de vacaciones de los que dispone, " +
                            "si no es correcto la cantidad de días a solicitar verifique nuevamente'})";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
                }
            }

        }

        public Correos DevuelveModeloCorreo()
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

                modelResponse.Asunto = "Nueva solicitud de permiso";
                modelResponse.Cuerpo = "Estimado usuario. <br/><br/> Se ha ingresado una nueva gestión para solicitud de permisos, a continuación se detalla: <br/><br/> " +
                    "<ul><li>Tipo de solicitud: " + slctTipo.SelectedItem.Text + " </li> " +
                    "<li>Fecha de salida: " + dtFechaInicio.Text + "</li>" +
                    "<li>Fecha de entrada: " + dtFechaFin.Text + "</li>" +
                    "<li>Motivo: " + txtObservacion.Text + "</li>" +
                    "</ul><br/><br/>" +
                    "Estado de flujo 1/3 <br> " +
                    "Saludos cordiales.";
                cnn2.Close();
            }
            catch (Exception)
            {
            }
            return modelResponse;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            InsertarSolicitud();
            InterfaceEmail.SendEmail(DevuelveModeloCorreo());
            LimpiarFormulario();

            string jsAlert = "Swal.fire({ " +
                "icon: 'success', " +
                "title: 'Éxito', " +
                "text: 'Se han ingresado las vacaciones correctamente'})";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
        }
    }
}