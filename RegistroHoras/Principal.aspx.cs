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
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Session.Timeout = 1440;
            if (Context.Session["admin"] == null) {
                Context.Session["sesion"] = "yes";
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                NormalFields();
                string sqlEnca = "select * from AST_encargado where Estatus = 1 order by encargado";
                string sqlCliente = "select * from AST_cliente where Estatus = 1 order by cliente";
                string sqlDepa = "select * from AST_departamento where Estatus = 1 order by departamento";
                string sqlFacturado = "select * from AST_Facturable where Estatus = 1 order by descripcion";
                string sqlProyecto = "select * from AST_proyecto where Estatus = 1 order by proyecto";
                string sqlEncargado = string.Empty;
                string sqlDepartamento = string.Empty;
                if (Context.Session["usuario"] != null)
                {
                    sqlEncargado = "select * from AST_Usuarios where usuario = '" + Context.Session["usuario"].ToString() + "'";
                    sqlDepartamento = "select a.* from ast_encargado a, ast_usuarios b where a.id = b.idEncargado and b.usuario ='" + Context.Session["usuario"].ToString() + "'";
                }
                string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                SqlConnection cnn2 = new SqlConnection(Conexion);

                try
                {
                    //Carga encargado
                    cnn2.Open();
                    SqlCommand cmd2 = new SqlCommand(sqlEnca, cnn2);
                    SqlDataReader dr = cmd2.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem i = new ListItem(dr["Encargado"].ToString(), dr["id"].ToString());
                        ListaEncargado.Items.Add(i);
                    }
                    cnn2.Close();

                    //Carga cliente
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

                    //Carga departamento
                    cnn2.Open();
                    SqlCommand cmd4 = new SqlCommand(sqlDepa, cnn2);
                    SqlDataReader dr2 = cmd4.ExecuteReader();
                    ListItem k = new ListItem("Seleccione...", "");
                    ListaDepartamento.Items.Add(k);
                    while (dr2.Read())
                    {
                        ListItem i = new ListItem(dr2["departamento"].ToString(), dr2["id"].ToString());
                        ListaDepartamento.Items.Add(i);
                    }

                    cnn2.Close();

                    //Carga proyecto
                    cnn2.Open();
                    SqlCommand cmd5 = new SqlCommand(sqlProyecto, cnn2);
                    SqlDataReader dr3 = cmd5.ExecuteReader();
                    ListItem a = new ListItem("Seleccione...", "");
                    ListaProyecto.Items.Add(a);
                    while (dr3.Read())
                    {
                        ListItem i = new ListItem(dr3["proyecto"].ToString(), dr3["id"].ToString());
                        ListaProyecto.Items.Add(i);
                    }

                    cnn2.Close();

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

                    //Encargado x defecto
                    cnn2.Open();
                    SqlCommand cmd10 = new SqlCommand(sqlEncargado, cnn2);
                    SqlDataReader dr10 = cmd10.ExecuteReader();
                    while (dr10.Read())
                    {
                        ListaEncargado.SelectedValue = dr10["idEncargado"].ToString().ToLower();
                    }
                    cnn2.Close();

                    ListaEncargado.Enabled = false;
                    if (Context.Session["admin"] != null && Context.Session["admin"].ToString() == "1")
                    {
                        ListaEncargado.Enabled = true;
                        ListaEncargado.BackColor = System.Drawing.Color.White;
                    }


                    cnn2.Open();
                    SqlCommand cmd11 = new SqlCommand(sqlDepartamento, cnn2);
                    SqlDataReader dr11 = cmd11.ExecuteReader();
                    while (dr11.Read())
                    {
                        if (dr11["idDepartamento"] != null)
                        {
                            ListaDepartamento.SelectedValue = dr11["idDepartamento"].ToString().ToLower();
                        }
                    }
                    cnn2.Close();

                    if (Request.QueryString["id"] != null)
                    {
                        Guid id = Guid.Parse(Request.QueryString["id"]);
                        CargaPrincipalEditar(id);
                    }
                    else if (Request.QueryString["Fecha"] != null)
                    {
                        DateTime fecha = DateTime.Parse(Request.QueryString["Fecha"].ToString());
                        dtFechaInicio.Text = fecha.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        dtFechaInicio.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    }
                }
                catch (Exception ex)
                {
                    mensajeError.Visible = true;
                    textoMensajeError.InnerText = ex.Message;
                }
            }
        }

        public void CargaPrincipalEditar(Guid id)
        {
            bool cargado = false;
            string sql = "select * from AST_horasRegistro where id='" + Request.QueryString["id"].ToString() + "'";
            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);

            try
            {
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                SqlDataReader dr = cmd2.ExecuteReader();

                while (dr.Read())
                {
                    if (Convert.ToDateTime(dr["fechaInicio"].ToString()).ToString("dd-MM-yyyy") == DateTime.Today.ToString("dd-MM-yyyy"))
                    {
                        txtActividad.Text = dr["actividad"].ToString();
                        txtComentario.Text = dr["comentario"].ToString();
                        ListaCliente.SelectedValue = dr["idCliente"].ToString();
                        ListaDepartamento.SelectedValue = dr["idDepartamento"].ToString();
                        ListaEncargado.SelectedValue = dr["IdEncargado"].ToString();
                        ListaFacturable.SelectedValue = dr["idFacturable"].ToString();
                        ListaProyecto.SelectedValue = dr["idProyecto"].ToString();
                        string fechaInicio = DateTime.Parse(dr["fechaInicio"].ToString()).ToString("HH:mm");
                        string fechafin = DateTime.Parse(dr["fechaFinal"].ToString()).ToString("HH:mm");
                        txtHoraInicio.Text = fechaInicio;
                        txtHoraFin.Text = fechafin;
                        dtFechaInicio.Text = Convert.ToDateTime(dr["fechaInicio"].ToString()).ToString("yyyy-MM-dd");
                        cbxExtras.Checked = (dr["Extras"].ToString() == "True") ? true : false;
                        cargado = true;
                    }
                    else if (Context.Session["admin"] != null && Context.Session["admin"].ToString() == "1")
                    {
                        txtActividad.Text = dr["actividad"].ToString();
                        txtComentario.Text = dr["comentario"].ToString();
                        ListaCliente.SelectedValue = dr["idCliente"].ToString();
                        ListaDepartamento.SelectedValue = dr["idDepartamento"].ToString();
                        ListaEncargado.SelectedValue = dr["IdEncargado"].ToString();
                        ListaFacturable.SelectedValue = dr["idFacturable"].ToString();
                        ListaProyecto.SelectedValue = dr["idProyecto"].ToString();
                        string fechaInicio = DateTime.Parse(dr["fechaInicio"].ToString()).ToString("HH:mm");
                        string fechafin = DateTime.Parse(dr["fechaFinal"].ToString()).ToString("HH:mm");
                        txtHoraInicio.Text = fechaInicio;
                        txtHoraFin.Text = fechafin;
                        dtFechaInicio.Text = Convert.ToDateTime(dr["fechaInicio"].ToString()).ToString("yyyy-MM-dd");
                        cbxExtras.Checked = (dr["Extras"].ToString() == "True") ? true : false;
                        cargado = true;
                    }
                    else if(Context.Session["supervisor"] != null && Context.Session["supervisor"].ToString() == "1") {

                        txtActividad.Text = dr["actividad"].ToString();
                        txtComentario.Text = dr["comentario"].ToString();
                        ListaCliente.SelectedValue = dr["idCliente"].ToString();
                        ListaDepartamento.SelectedValue = dr["idDepartamento"].ToString();
                        ListaEncargado.SelectedValue = dr["IdEncargado"].ToString();
                        ListaFacturable.SelectedValue = dr["idFacturable"].ToString();
                        ListaProyecto.SelectedValue = dr["idProyecto"].ToString();
                        string fechaInicio = DateTime.Parse(dr["fechaInicio"].ToString()).ToString("HH:mm");
                        string fechafin = DateTime.Parse(dr["fechaFinal"].ToString()).ToString("HH:mm");
                        txtHoraInicio.Text = fechaInicio;
                        txtHoraFin.Text = fechafin;
                        dtFechaInicio.Text = Convert.ToDateTime(dr["fechaInicio"].ToString()).ToString("yyyy-MM-dd");
                        cbxExtras.Checked = (dr["Extras"].ToString() == "True") ? true : false;
                        cargado = true;
                    }

                }
                cnn2.Close();

                if (!cargado) {
                    string jsAlert = "Swal.fire({ " +
                        "icon: 'warning', " +
                        "title: 'Aviso', " +
                        "text: 'No puedes modificar un registro que no se haya registrado hoy, debes solicitar ayuda a un administrador para modificar'})";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string comentario = txtComentario.Text;
                string actividad = txtActividad.Text;
                Guid idEncargado = Guid.Parse(ListaEncargado.SelectedValue);
                Guid idCliente = Guid.Parse(ListaCliente.SelectedValue);
                Guid idDepartamento = Guid.Parse(ListaDepartamento.SelectedValue);
                Guid idProyecto = Guid.Parse(ListaProyecto.SelectedValue);
                Guid idFacturable = Guid.Parse(ListaFacturable.SelectedValue);
                DateTime fechaIncio = DateTime.Now;
                DateTime fechaFin = DateTime.Now;
                string sql = string.Empty;

                if (Request.QueryString["id"]==null) {
                    sql = "INSERT INTO AST_horasRegistro (fechaInicio, fechaFinal, actividad, comentario, idEncargado, IdDepartamento, IdProyecto, IdCliente, IdFacturable, Extras)" +
                    "VALUES(@fechaInicio, @fechaFinal, @actividad, @comentario, @idEncargado, @IdDepartamento, @IdProyecto, @IdCliente, @IdFacturable, @Extras)";
                    fechaIncio = DateTime.Parse(dtFechaInicio.Text + " " + txtHoraInicio.Text + ":00");
                    fechaFin = DateTime.Parse(dtFechaInicio.Text + " " + txtHoraFin.Text + ":00");
                }
                else
                {
                    sql = "UPDATE AST_horasRegistro set fechaInicio = @fechaInicio, fechaFinal = @fechaFinal, actividad = @actividad, comentario = @comentario, idEncargado = @idEncargado, IdDepartamento = @IdDepartamento, IdProyecto = @IdProyecto, IdCliente =  @IdCliente, IdFacturable=@IdFacturable, Extras=@Extras where Id = '" + Request.QueryString["id"].ToString() + "'";
                    fechaIncio = DateTime.Parse(dtFechaInicio.Text + " " + txtHoraInicio.Text);
                    fechaFin = DateTime.Parse(dtFechaInicio.Text + " " + txtHoraFin.Text);
                }

                string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();

                SqlConnection cnn2 = new SqlConnection(Conexion);

                if (fechaIncio > fechaFin) {
                    ShowEmptyFields();
                    string jsAlert = "Swal.fire({ " +
                        "icon: 'error', " +
                        "title: 'Error', " +
                        "text: 'La fecha de inicio no puede ser más reciente que la final'})";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
                }
                else if (idFacturable.ToString() == "" || idCliente.ToString() == "" || idProyecto.ToString() == ""
                    || txtActividad.Text == "" || txtHoraFin.Text == "" || txtHoraInicio.Text == "") {
                    ShowEmptyFields();
                    string jsAlert = "Swal.fire({ " +
                        "icon: 'error', " +
                        "title: 'Error', " +
                        "text: 'No puedes dejar una lista o campo sin seleccionar'})";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
                } else if (!validateForInsert()) {
                    string jsAlert = "Swal.fire({ " +
                        "icon: 'error', " +
                        "title: 'Error', " +
                        "text: 'Ya existe un registro en el horario indicado'})";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);
                }
                else
                {
                    cnn2.Open();
                    SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                    cmd2.CommandType = System.Data.CommandType.Text;
                    cmd2.Parameters.Add("@fechaFinal", SqlDbType.DateTime).Value = fechaFin;
                    cmd2.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = fechaIncio;
                    cmd2.Parameters.Add("@actividad", SqlDbType.VarChar).Value = actividad;
                    cmd2.Parameters.Add("@comentario", SqlDbType.VarChar).Value = comentario;
                    cmd2.Parameters.Add("@idEncargado", SqlDbType.UniqueIdentifier).Value = idEncargado;
                    cmd2.Parameters.Add("@IdDepartamento", SqlDbType.UniqueIdentifier).Value = idDepartamento;
                    cmd2.Parameters.Add("@IdProyecto", SqlDbType.UniqueIdentifier).Value = idProyecto;
                    cmd2.Parameters.Add("@IdCliente", SqlDbType.UniqueIdentifier).Value = idCliente;
                    cmd2.Parameters.Add("@IdFacturable", SqlDbType.UniqueIdentifier).Value = idFacturable;
                    cmd2.Parameters.Add("@Extras", SqlDbType.Bit).Value = cbxExtras.Checked;
                    cmd2.ExecuteNonQuery();
                    cnn2.Close();

                    string jsAlert = "Swal.fire({ " +
                        "icon: 'success', " +
                        "title: 'Éxito', " +
                        "text: 'Se ha guardado el registro con exito'})";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);


                    txtActividad.Text = "";
                    txtComentario.Text = "";
                    txtHoraInicio.Text = "";
                    txtHoraFin.Text = "";
                    dtFechaInicio.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    ListaFacturable.SelectedValue = "";
                    ListaCliente.SelectedValue = "";
                    ListaProyecto.SelectedValue = "";
                    cbxExtras.Checked = false;
                    NormalFields();

                    if (Request.QueryString["id"] != null)
                    {
                        Response.Redirect("Principal.aspx");
                    }
                }

            }
            catch (Exception ex)
            {

                string jsAlert = "Swal.fire({ " +
                    "icon: 'error', " +
                    "title: 'Error', " +
                    "text: 'Ha ocurrido un error, revise que no haya omitido algún campo'})";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", jsAlert, true);

                dtFechaInicio.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
           
        }

        public bool validateForInsert() {
            bool banderita = true;
            try
            {
                DateTime fechaIncio = DateTime.Parse(dtFechaInicio.Text + " " + txtHoraInicio.Text);
                DateTime fechaFnal = DateTime.Parse(dtFechaInicio.Text + " " + txtHoraFin.Text);

                string fechaFin = DateTime.Parse(dtFechaInicio.Text).ToString("dd/MM/yyyy");
                DateTime fechaInic = DateTime.Parse(dtFechaInicio.Text + " " + "23:59");

                string sql = string.Empty;

                if (Request.QueryString["id"]== null) { 
                   sql = "select * from AST_horasRegistro where idEncargado='" + ListaEncargado.SelectedValue + "' and Convert(date, fechaInicio) = '" + fechaFin +"'";
                }
                else {
                    sql = "select * from AST_horasRegistro where idEncargado='" + ListaEncargado.SelectedValue + "' and fechaInicio between '" + fechaFin + "' and '" + fechaInic + "' and id not in ('"+ Request.QueryString["id"].ToString()+"') ";
                }
                string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
                SqlConnection cnn2 = new SqlConnection(Conexion);
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                SqlDataReader dr = cmd2.ExecuteReader();
                DateTime maxDate = DateTime.Today;
                while (dr.Read()) {

                    maxDate = (maxDate > DateTime.Parse(dr["fechaFinal"].ToString())) ? maxDate : DateTime.Parse(dr["fechaFinal"].ToString());

                    if ( (fechaIncio > DateTime.Parse(dr["fechaInicio"].ToString())) && (fechaIncio < DateTime.Parse(dr["fechaFinal"].ToString())) ) {
                        banderita = false;
                    }
                    if ((fechaFnal > DateTime.Parse(dr["fechaInicio"].ToString())) && (fechaFnal < DateTime.Parse(dr["fechaFinal"].ToString()))) { 
                        banderita = false;
                    }
                    if (fechaIncio == DateTime.Parse(dr["fechaInicio"].ToString()) && fechaFnal > DateTime.Parse(dr["fechaFinal"].ToString()) ) {
                        banderita = false;
                    }
                    if (fechaIncio == DateTime.Parse(dr["fechaInicio"].ToString()) && fechaFnal == DateTime.Parse(dr["fechaFinal"].ToString()) ) {
                        banderita = false;
                    }
                }
                if (fechaIncio < maxDate && fechaFnal > maxDate)
                {
                    banderita = false;
                }
            }
            catch (Exception ex)
            {
            }
            return banderita;
        }

        protected void ListaProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ListaProyecto.SelectedItem.ToString() == "Reunión Diaria") {
                txtActividad.Text = "Reunión Diaria";
                txtHoraInicio.Text = "07:30";
                txtHoraFin.Text = "08:00";
                dtFechaInicio.Text = DateTime.Today.ToString("yyyy-MM-dd");
                ListaFacturable.SelectedValue = "a647b104-1d52-48e2-b13e-5989927a6933";
                ListaCliente.SelectedValue = "6ca67c84-4849-4f37-adc8-d508c26a39dd";
                cbxExtras.Checked = false;
                NormalFields();
                ListaProyecto.Focus();

            }
        }

        public void ShowEmptyFields() {
            txtActividad.BorderColor = (txtActividad.Text == "") ? System.Drawing.Color.Red : System.Drawing.Color.LightGray;
            ListaFacturable.BorderColor = (ListaFacturable.SelectedValue == "") ? System.Drawing.Color.Red : System.Drawing.Color.LightGray;
            ListaCliente.BorderColor = (ListaCliente.SelectedValue == "") ? System.Drawing.Color.Red : System.Drawing.Color.LightGray;
            ListaProyecto.BorderColor = (ListaProyecto.SelectedValue == "") ? System.Drawing.Color.Red : System.Drawing.Color.LightGray;
            txtHoraInicio.BorderColor = (txtHoraInicio.Text == "") ? System.Drawing.Color.Red : System.Drawing.Color.LightGray;
            txtHoraFin.BorderColor = (txtHoraFin.Text == "") ? System.Drawing.Color.Red : System.Drawing.Color.LightGray;
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
                NormalFields();
                ListaFacturable.Focus();
            }
            catch (Exception ex)
            {
            }

        }

        public void NormalFields()
        {
            txtActividad.BorderColor = System.Drawing.Color.LightGray;
            ListaFacturable.BorderColor = System.Drawing.Color.LightGray;
            ListaCliente.BorderColor = System.Drawing.Color.LightGray;
            ListaProyecto.BorderColor = System.Drawing.Color.LightGray;
            txtHoraInicio.BorderColor = System.Drawing.Color.LightGray;
            txtHoraFin.BorderColor = System.Drawing.Color.LightGray;
        }

    }
}