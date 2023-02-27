<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Extras.aspx.cs" Inherits="Formulario_4.Extras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        div.dt-top-container {
          display: grid;
          margin-bottom:5px;
          grid-template-columns: auto auto auto;
        }

        div.dt-center-in-div {
          margin: 0 auto;
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

        #MainContent_extrasTable table {
            width:100%;
        }
    </style>
    <style src="https://cdn.datatables.net/rowgroup/1.1.4/css/rowGroup.dataTables.min.css"></style>


    <div class="card">
        <div class="card-header bg-dark">
            <h5 class="text-white">Control de horas extras</h5>
        </div>
        <div class="card-body">

            <div class="table-responsive">
                <asp:GridView ID="extrasTable"
                    DataKeyNames="fechaInicio, fechaFinal, actividad, comentario, encargado, departamento, proyecto, cliente, descripcion, id, HorasTotales" runat="server" 
                    SortExpression="registro"
                    Width="100%"
                    BorderColor="Black"
                    CssClass="table table-hover table-striped "
                    AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource1" >
                <Columns>

                    <asp:BoundField HeaderText="Hora Inicio"
                        ReadOnly="true" ShowHeader="true" DataField="fechaInicio" />

                    <asp:BoundField HeaderText="Hora Final"
                        ReadOnly="true" ShowHeader="true" DataField="fechaFinal" />

                    <asp:BoundField HeaderText="Total Horas"
                        ReadOnly="true" ShowHeader="true" DataField="HorasTotales" />

                    <asp:BoundField HeaderText="Encargado"
                        ReadOnly="true" ShowHeader="true" DataField="encargado" />

                    <asp:BoundField HeaderText="Departamento"
                        ReadOnly="true" ShowHeader="true" DataField="departamento" />

                    <asp:BoundField HeaderText="Proyecto"
                        ReadOnly="true" ShowHeader="true" DataField="proyecto" />

                    <asp:BoundField HeaderText="Cliente"
                        ReadOnly="true" ShowHeader="true" DataField="cliente" />

                    <asp:BoundField HeaderText="Facturable"
                        ReadOnly="true" ShowHeader="true" DataField="descripcion" />

                    <asp:BoundField HeaderText="Actividad"
                        ReadOnly="true" ShowHeader="true" DataField="actividad" />

                    <asp:BoundField HeaderText="Comentario"
                        ReadOnly="true" ShowHeader="true" DataField="comentario" />

                    <asp:BoundField HeaderStyle-CssClass="hideBound"
                        ReadOnly="true" ShowHeader="false" DataField="id" ItemStyle-CssClass="hideBound"/>

                </Columns>
            </asp:GridView>
            </div>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:AST_InternaConnectionString %>"
                SelectCommand ="SELECT a.id, a.fechaInicio, a.fechaFinal, a.actividad, a.comentario, b.encargado, 
                    c.departamento, d.proyecto, e.cliente, f.descripcion , CONVERT(varchar(5), DATEADD(minute, DATEDIFF(MINUTE, a.fechaInicio, a.fechaFinal), 0), 114) as HorasTotales 
                    from AST_horasRegistro a, AST_encargado b, AST_departamento c, AST_proyecto d, AST_cliente e, AST_facturable f 
                    where a.IdEncargado = b.Id 
                    and a.IdDepartamento = c.Id 
                    and a.IdProyecto = d.Id 
                    and a.IdCliente = e.Id 
                    and a.IdFacturable = f.Id 
                    and a.Extras = 1
                    ORDER BY fechaInicio desc"></asp:SqlDataSource>


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

    <script type="text/javascript">
        $(document).ready(function () {
            var gvregistroTable = $("#MainContent_extrasTable").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "pageLength": 7,
                "ordering": true,
                "lengthMenu": [5, 7, 10, 15, 30],   
                "language": {
                    url: "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                buttons: [
                    {
                        extend: 'copy',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    },
                    {
                        extend: 'excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        }
                    }
                ],
                "order": [[3, 'desc']],
                dom: '<"dt-top-container"<l><"dt-center-in-div"B><f>r>t<ip>'
            });
        });
    </script>


</asp:Content>
