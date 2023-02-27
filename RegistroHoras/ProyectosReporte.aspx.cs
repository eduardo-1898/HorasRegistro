using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Formulario_4
{
    public partial class ProyectosReporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvProyectos.DataBind();
        }

        protected void btnBuscar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                SqlDataSource2.SelectCommand = "SELECT p.Id, p.proyecto, case p.estatus when 1 then 'Activo' else 'Inactivo' end as estatus, p.HorasEstimadas," +
                    "isnull((SELECT SUM(cantidadHoras) / 60 as Horas " +
                    "FROM " +
                    "(SELECT DATEDIFF(MINUTE, fechaInicio, fechaFinal) as cantidadHoras " +
                    "FROM AST_horasRegistro " +
                    "WHERE IdProyecto = p.Id) a),0) as HorasTotales " +
                    "FROM AST_proyecto p " +
                    "WHERE p.Facturar = " + ddlFacturar.SelectedValue + " " +
                    "AND p.Facturado = "+ ddlFacturado.SelectedValue + " ";

                gvProyectos.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
    }
}