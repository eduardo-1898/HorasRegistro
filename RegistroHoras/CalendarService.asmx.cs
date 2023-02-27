using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Formulario_4
{

    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
 
    [System.Web.Script.Services.ScriptService]
    public class CalendarService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getData(string mes, string ano)
        {
            var registers = new List<Registros>();

            string sql = "SELECT idEncargado, CAST(fechaInicio AS date) as Fecha, DATEDIFF(HOUR,MIN(fechaInicio),MAX(FechaFinal)) as Horas " +
                "FROM[dbo].[AST_horasRegistro] " +
                "WHERE IdEncargado = @idEncargado " +
                "AND MONTH(fechaInicio) = @mes " +
                "AND YEAR(fechaInicio) = @ano " +
                "GROUP BY IdEncargado, CAST(fechaInicio AS date); ";

            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);

            try
            {
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.Parameters.AddWithValue("@idEncargado", Session["idEncargado"]);
                cmd2.Parameters.AddWithValue("@mes", mes);
                cmd2.Parameters.AddWithValue("@ano", ano);
                SqlDataReader dr = cmd2.ExecuteReader();

                while (dr.Read())
                {
                    registers.Add(new Registros { fecha = (DateTime)dr["fecha"], horas = (int)dr["Horas"], IdEncargado = (Guid)dr["idEncargado"] });
                }
                cnn2.Close();
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(registers);
            }

            return JsonConvert.SerializeObject(registers);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getActivities(string mes, string ano, string dia)
        {
            var registers = new List<Actividades>();

            string sql = "SELECT a.id, " +
                "DATEDIFF(HOUR,MIN(a.fechaInicio),MAX(a.fechaFinal)) as horas, " +
                "a.fechaInicio, " +
                "SUBSTRING(CAST(convert(time, a.fechaInicio) AS varchar), 1, 5) as HoraInicio, " +
                "SUBSTRING(CAST(convert(time, a.fechaFinal) AS varchar), 1, 5) as HoraFinal, b.proyecto, c.cliente, a.actividad " +
                "FROM AST_horasRegistro a " +
                "LEFT JOIN AST_proyecto b ON a.IdProyecto = b.id " +
                "LEFT JOIN AST_cliente c ON c.Id = a.IdCliente " +
                "WHERE MONTH(a.fechaInicio) = @mes " +
                "AND YEAR(a.fechaInicio) = @ano " +
                "AND DAY(a.fechaInicio) = @dia " +
                "AND a.IdEncargado = @idEncargado " +
                "GROUP BY a.id,b.proyecto,a.fechaInicio, c.cliente,a.actividad, " +
                "SUBSTRING(CAST(convert(time, a.fechaInicio) AS varchar), 1, 5), " +
                "SUBSTRING(CAST(convert(time, a.fechaFinal) AS varchar), 1, 5) " +
                "ORDER BY a.fechaInicio asc";

            string Conexion = ConfigurationManager.ConnectionStrings["AST_InternaConnectionString"].ToString();
            SqlConnection cnn2 = new SqlConnection(Conexion);

            try
            {
                cnn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql, cnn2);
                cmd2.CommandType = System.Data.CommandType.Text;

                cmd2.Parameters.AddWithValue("@mes", mes);
                cmd2.Parameters.AddWithValue("@ano", ano);
                cmd2.Parameters.AddWithValue("@dia", dia);
                cmd2.Parameters.AddWithValue("@idEncargado", Session["idEncargado"]);
                SqlDataReader dr = cmd2.ExecuteReader();

                while (dr.Read())
                {
                    registers.Add(new Actividades { 
                        Actividad = dr["actividad"].ToString(), 
                        Horas = dr["horas"].ToString(),
                        Cliente = dr["cliente"].ToString(),
                        FechaInicio = dr["HoraInicio"].ToString(),
                        FechaFinal = dr["HoraFinal"].ToString(),
                        id = (Guid)dr["id"],
                        Proyecto = dr["proyecto"].ToString()
                    });
                }
                cnn2.Close();
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(registers);
            }

            return JsonConvert.SerializeObject(registers);
        }


    }
}
