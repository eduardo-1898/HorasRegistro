<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HorasFacturables.aspx.cs" Inherits="Formulario_4.HorasFacturables" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


     <style>
        div.dt-top-container {
          display: grid;
          margin-bottom:5px;
          margin-top:15px;
          grid-template-columns: auto auto auto;
        }

        div.dt-center-in-div {
          margin: 0 auto;
          margin-top:15px;
          padding-left:400px;
          margin-bottom:5px;
        }

        div.dt-filter-spacer {
          margin: 10px 0;
        }
        .buttons-csv {
          background-color: #FF6031;
          color: white;
          border: none;
          box-shadow: 0 10px 15px -3px rgba(0,0,0,.07)
        }
        .buttons-copy {
          background-color: #FF6031;
          color: white;
          border: none;
          box-shadow: 0 10px 15px -3px rgba(0,0,0,.07)

        }
        .buttons-excel {
          background-color: #FF6031;
          color: white;
          border: none;
          box-shadow: 0 10px 15px -3px rgba(0,0,0,.07)
        }

        #MainContent_HorasFacturablesTable table {
            width:100%;
        }
    </style>
    <style src="https://cdn.datatables.net/rowgroup/1.1.4/css/rowGroup.dataTables.min.css"></style>


    <div class="card">
        <div class="card-header bg-dark">
            <h5 class="text-white">Control de horas facturables</h5>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col">
                    <label for="ListaCliente">Fecha inicio: </label>
                    <asp:TextBox runat="server" ID="dtFechaInicio" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="ListaCliente">Fecha Final: </label>
                    <asp:TextBox runat="server" ID="dtFechaFin" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="ListaFacturable">Facturable: </label>
                    <asp:DropDownList CssClass="form-select" ID="ListaFacturable" runat="server" OnSelectedIndexChanged="ListaFacturable_SelectedIndexChanged"  AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col">
                    <label for="ListaCliente">Cliente: </label>
                    <asp:DropDownList CssClass="form-select" ID="ListaCliente" runat="server" >
                    </asp:DropDownList>
                </div>

                <div class="col">
                    <button runat="server" id="btnBuscar" class="btn btn-success text-white float-end" style="margin-top:22px;" onserverclick="btnBuscar_ServerClick"><i class="fas fa-search"></i></button>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="HorasFacturablesTable"
                    DataKeyNames="departamento, fechaFacturada, encargado, comentario, fechaInicio, horaInicio, HoraFinal, proyecto, TotalHoras, cliente, descripcion, tiempoTotal" runat="server" 
                    SortExpression="registro"
                    BorderColor="Black"
                    Width="100%"
                    CssClass="table table-hover table-striped "
                    AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource1"
                    OnRowDataBound="HorasFacturablesTable_RowDataBound"
                    ShowFooter="true">
                <Columns>

                    <asp:BoundField HeaderText="Total Horas"
                        ReadOnly="true" ShowHeader="true" DataField="tiempoTotal" HeaderStyle-CssClass="hideBound" ItemStyle-CssClass="hideBound"/>

                    <asp:BoundField HeaderText="Departamento"
                        ReadOnly="true" ShowHeader="true" DataField="departamento" />

                    <asp:BoundField HeaderText="Fecha Facturada"
                        ReadOnly="true" ShowHeader="true" DataField="fechaFacturada" DataFormatString="{0:d}" />

                    <asp:BoundField HeaderText="Encargado"
                        ReadOnly="true" ShowHeader="true" DataField="encargado" />

                    <asp:BoundField HeaderText="Comentario"
                        ReadOnly="true" ShowHeader="true" DataField="comentario" />

                    <asp:BoundField HeaderText="Fecha Inicio"
                        ReadOnly="true" ShowHeader="true" DataField="fechaInicio" DataFormatString="{0:d}" />

                    <asp:BoundField HeaderText="Hora de Inicio"
                        ReadOnly="true" ShowHeader="true" DataField="horaInicio" />

                    <asp:BoundField HeaderText="Hora Final"
                        ReadOnly="true" ShowHeader="true" DataField="HoraFinal" />

                    <asp:BoundField HeaderText="Proyecto"
                        ReadOnly="true" ShowHeader="true" DataField="proyecto" />

                    <asp:BoundField HeaderText="Total de Horas"
                        ReadOnly="true" ShowHeader="true" DataField="TotalHoras" />

                    <asp:BoundField
                        ReadOnly="true" ShowHeader="false" DataField="cliente" HeaderText="Cliente"/>

                    <asp:BoundField HeaderText="Descripcion"
                        ReadOnly="true" ShowHeader="true" DataField="descripcion" />

                </Columns>
            </asp:GridView>
            </div>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:AST_InternaConnectionString %>"
                SelectCommand ="SELECT TOP 0 c.departamento, 
                    convert(date, GETDATE()) as fechaFacturada, 
                    b.encargado, 
                    a.actividad as comentario, 
                    convert(date, fechaInicio) as fechaInicio, 
                    cast(a.fechaInicio as time) as  horaInicio, 
                    CONVERT(time,a.fechaFinal) as HoraFinal, 
                    d.proyecto,
                    convert(varchar(8), cast(DATEADD(HOUR, DATEDIFF(HOUR, a.fechaInicio, a.fechaFinal), 0) as time )) as TotalHoras,
	                convert(int,(select sum(datediff(HOUR,fechaInicio, fechaFinal)) from AST_horasRegistro where id=a.id)) as tiempoTotal,
                    e.cliente,
                    f.descripcion 
                    from AST_horasRegistro a, AST_encargado b, AST_departamento c, AST_proyecto d, AST_cliente e, AST_facturable f 
                    where a.IdEncargado = b.Id 
                    and a.IdDepartamento = c.Id 
                    and a.IdProyecto = d.Id 
                    and a.IdCliente = e.Id 
                    and a.IdFacturable = f.Id 
                    ORDER BY fechaInicio desc"
                DeleteCommand="DELETE FROM AST_proyecto where proyecto=@proyecto"></asp:SqlDataSource>


        </div>
    </div>

    <script src="Scripts/vendor/datatables/jquery.dataTables.min.js"></script>
    <script src="Scripts/vendor/datatables/dataTables.buttons.min.js"></script>
    <script src="Scripts/vendor/datatables/buttons.bootstrap4.min.js"></script>
    <script src="Scripts/vendor/datatables/jszip.min.js"></script>
    <script src="Scripts/vendor/datatables/pdfmake.min.js"></script>
    <script src="Scripts/vendor/datatables/vfs_fonts.js"></script>
    <script src="Scripts/vendor/datatables/buttons.html5.min.js"></script>
    <script src="Scripts/vendor/datatables/buttons.print.min.js"></script>
    <script src="Scripts/vendor/datatables/buttons.colVis.min.js"></script>
    <script src="Scripts/vendor/datatables/Jquery.datables.total.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var gvregistroTable = $("#MainContent_HorasFacturablesTable").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "pageLength": 7,
                "totalCol   ": true,
                "ordering": true,
                "lengthMenu": [5, 7, 10, 15, 30],   
                "language": {
                    url: "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                buttons: [
                    {
                        extend: 'copy',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9,10,11]
                        }
                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11]
                        }
                    },
                    {
                        extend: 'excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11]
                        }   
                    }
                ],
                "order": [[3, 'desc']],
                dom: '<"dt-top-container"<l><"dt-center-in-div"B><f>r>t<ip>'
            });
        });
    </script>




</asp:Content>
