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
    public partial class MoEncargado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Session.Timeout = 1440;
            if (Context.Session["admin"] == null && Context.Session["supervisor"] == null)
            {
                Context.Session["sesion"] = "yes";
                Response.Redirect("Login.aspx");
            }

            try
            {
                string sqlDepa = "select * from AST_departamento where Estatus = 1 order by departamento";
                string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                SqlConnection cnn2 = new SqlConnection(Conexion);
                //Carga departamento
                cnn2.Open();
                SqlCommand cmd5 = new SqlCommand(sqlDepa, cnn2);
                SqlDataReader dr5 = cmd5.ExecuteReader();
                while (dr5.Read())
                {
                    ListItem i = new ListItem(dr5["departamento"].ToString(), dr5["id"].ToString());
                    ListaDepartamento.Items.Add(i);
                }

                cnn2.Close();

                if (Request.QueryString["id"] != null)
                {
                    if(!IsPostBack)
                    {
                        string sql = "select a.*, us.usuario, us.admin, us.Operaciones, us.Supervisor from AST_encargado a left join AST_Usuarios us on us.idEncargado = a.id where a.id='" + Request.QueryString["id"].ToString() + "'";

                        try
                        {
                            cnn2.Open();
                            SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                            SqlDataReader dr = cmd2.ExecuteReader();

                            while (dr.Read())
                            {
                                txtCedula.Text = dr["cedula"].ToString();
                                txtEncargado.Text = dr["encargado"].ToString();
                                txtTelefono.Text = dr["telefono"].ToString();
                                txtEmail.Text = dr["correo"].ToString();
                                txtUsername.Text = dr["usuario"].ToString();
                                cbxEstado.Checked = Convert.ToBoolean(dr["estatus"]);
                                cbxAdmin.Checked = Convert.ToBoolean(dr["admin"]);
                                cbxOperaciones.Checked = Convert.ToBoolean(dr["Operaciones"]);
                                cbxSupervisor.Checked = Convert.ToBoolean(dr["Supervisor"]);


                                txtDate.Text = Convert.ToDateTime(
                                    dr["cumpleanos"].ToString()==""?
                                        DateTime.Now.ToString(): //true
                                        dr["cumpleanos"].ToString() //false
                                    ).ToString("yyyy-MM-dd");

                                dtFechaIngreso.Text = Convert.ToDateTime(
                                    dr["FechaIngreso"].ToString()==""?
                                        DateTime.Now.ToString()://true
                                        dr["FechaIngreso"].ToString()//false
                                    ).ToString("yyyy-MM-dd");
                                ListaDepartamento.SelectedValue = dr["idDepartamento"].ToString().ToLower();
                            }
                            cnn2.Close();
                        }
                        catch (Exception ex)
                        {
                            string jsAlert = "Swal.fire({ " +
                                "icon: 'error', " +
                                "title: 'Error', " +
                                "text: 'Ha ocurrido un error al cargar los datos'})";
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
                string tele = txtTelefono.Text;
                string cedu = txtCedula.Text;
                string correo = txtEmail.Text;
                string date = txtDate.Text;
                string fechaIngreso = dtFechaIngreso.Text;
                bool estatus = cbxEstado.Checked;

                try
                {
                    string sql = String.Empty;

                    if (Request.QueryString["id"] == null && fechaIngreso != "")
                    {
                        sql = "INSERT INTO AST_encargado (encargado, telefono, cedula, correo, cumpleanos, estatus, idDepartamento, fechaIngreso)" +
                            "VALUES(@encargado, @telefono, @cedula, @correo, @cumpleanos, @estatus, @idDepartamento, @fechaIngreso)";

                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);

                        cnn2.Open();
                        SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.Add("@encargado", SqlDbType.VarChar).Value = enca;
                        cmd2.Parameters.Add("@telefono", SqlDbType.VarChar).Value = tele;
                        cmd2.Parameters.Add("@cedula", SqlDbType.VarChar).Value = cedu;
                        cmd2.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
                        cmd2.Parameters.Add("@cumpleanos", SqlDbType.VarChar).Value = date;
                        cmd2.Parameters.Add("@estatus", SqlDbType.Bit).Value = estatus;
                        cmd2.Parameters.Add("@fechaIngreso", SqlDbType.VarChar).Value = fechaIngreso;
                        cmd2.Parameters.Add("@idDepartamento", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ListaDepartamento.SelectedValue);
                        cmd2.ExecuteNonQuery();
                        cnn2.Close();

                        createNewUser();
                        SendNewUser();

                        Response.Redirect("encargado.aspx");
                    }
                    else if (fechaIngreso != "")
                    {
                        sql = "update AST_encargado " +
                        "SET encargado = @encargado," +
                        "telefono = @telefono, " +
                        "cedula = @cedula, " +
                        "correo = @correo, " +
                        "cumpleanos = @cumpleanos, " +
                        "estatus = @estatus, " +
                        "idDepartamento = @idDepartamento, " +
                        "fechaIngreso = @fechaIngreso " +
                        "WHERE id='" + Request.QueryString["id"].ToString() + "'";

                        string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                        SqlConnection cnn2 = new SqlConnection(Conexion);

                        cnn2.Open();
                        SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.Parameters.Add("@encargado", SqlDbType.VarChar).Value = enca;
                        cmd2.Parameters.Add("@telefono", SqlDbType.VarChar).Value = tele;
                        cmd2.Parameters.Add("@cedula", SqlDbType.VarChar).Value = cedu;
                        cmd2.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
                        cmd2.Parameters.Add("@cumpleanos", SqlDbType.VarChar).Value = date;
                        cmd2.Parameters.Add("@estatus", SqlDbType.Bit).Value = estatus;
                        cmd2.Parameters.Add("@fechaIngreso", SqlDbType.VarChar).Value = fechaIngreso;
                        cmd2.Parameters.Add("@idDepartamento", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ListaDepartamento.SelectedValue);
                        cmd2.ExecuteNonQuery();
                        cnn2.Close();

                        UpdateUser();

                        Response.Redirect("encargado.aspx");
                    }
                    else {
                        string jsAlert = "Swal.fire({ " +
                            "icon: 'error', " +
                            "title: 'Error', " +
                            "text: 'Se debe de registar una fecha de ingreso para el colaborador'})";
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
            catch (Exception ex)
            {
                string jsAlert = "Swal.fire({ " +
                    "icon: 'error', " +
                    "title: 'Error', " +
                    "text: 'Ha ocurrido un error al registrar los datos'})";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
            }
        }

        private Guid getEncargadoIdValue() {
            
            Guid id = new Guid();
            try
            {
                string sqlDepa = "select * from AST_encargado where Encargado = '" + txtEncargado.Text + "'";
                string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                SqlConnection cnn2 = new SqlConnection(Conexion);

                cnn2.Open();
                SqlCommand cmd5 = new SqlCommand(sqlDepa, cnn2);
                SqlDataReader dr5 = cmd5.ExecuteReader();

                while (dr5.Read())
                {
                    id = (Guid)dr5["Id"];
                }

                cnn2.Close();

                return id;
            }
            catch (Exception)
            {
                return id;
            }
        }

        private void createNewUser() {

            string sql = "INSERT INTO AST_Usuarios (usuario, contrasena, admin, idEncargado,Supervisor, Operaciones)" +
                "VALUES(@usuario, @contrasena, @admin, @idEncargado, @Supervisor, @Operacion)";

            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);

            cnn2.Open();
            SqlCommand cmd2 = new SqlCommand(sql, cnn2);
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Parameters.Add("@usuario", SqlDbType.VarChar).Value = txtUsername.Text;
            cmd2.Parameters.Add("@contrasena", SqlDbType.VarChar).Value = txtUsername.Text + ".123";
            cmd2.Parameters.Add("@admin", SqlDbType.Bit).Value = cbxAdmin.Checked;
            cmd2.Parameters.Add("@idEncargado", SqlDbType.UniqueIdentifier).Value = getEncargadoIdValue();
            cmd2.Parameters.Add("@Supervisor", SqlDbType.VarChar).Value = cbxSupervisor.Checked;
            cmd2.Parameters.Add("@Operacion", SqlDbType.Bit).Value = cbxOperaciones.Checked;
            
            cmd2.ExecuteNonQuery();
            cnn2.Close();


        }

        private void SendNewUser() {
            var model = new Correos();
            model.Asunto = "Acceso a plataforma de registro de horas";
            model.Cuerpo = "Buenas Señor(a)(ita) <br/><br/> Se ha creado un usuario para su ingreso a la plataforma de registro de horas en ASTSoft " +
                "las credenciales para poder ingresar son las siguientes: <br/><br/>" +
                "Usuario: "+ txtUsername.Text + "<br/> " +
                "Contraseña: "+ txtUsername.Text + ".123"+ " <br/><br/> se le recomienda cambiar la contraseña una vez ingresado dentro de la aplicación " +
                "el link para ingresar a la plataforma es el siguiente: http://astsoft3519.cloudapp.net/Registro%20Horas/Login " +
                "en caso de consultas o dudas favor contactar al administrador del sistema o bien a su supervisor <br/><br/> " +
                "Este es un correo automatico por lo cual agradecemos no contestar al mismo";
            
            List<string> destinatario = new List<string>();
            destinatario.Add(txtEmail.Text);

            model.Destinatarios = destinatario;

            InterfaceEmail.SendEmail(model);
        }

        private void UpdateUser() {
            string sql = "update AST_Usuarios " +
                        "SET usuario = @encargado," +
                        "admin = @admin, " +
                        "Supervisor = @supervisor, " +
                        "Operaciones = @operaciones " +
                        "WHERE idEncargado='" + Request.QueryString["id"].ToString() + "'";

            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);

            cnn2.Open();
            SqlCommand cmd2 = new SqlCommand(sql, cnn2);
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Parameters.Add("@encargado", SqlDbType.VarChar).Value = txtUsername.Text;
            cmd2.Parameters.Add("@admin", SqlDbType.VarChar).Value = cbxAdmin.Checked;
            cmd2.Parameters.Add("@supervisor", SqlDbType.VarChar).Value = cbxSupervisor.Checked;
            cmd2.Parameters.Add("@operaciones", SqlDbType.VarChar).Value = cbxOperaciones.Checked;

            cmd2.ExecuteNonQuery();
            cnn2.Close();
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            txtCedula.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtEncargado.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            Response.Redirect("encargado.aspx");

        }
    }

}      

