<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProyectosReporte.aspx.cs" Inherits="Formulario_4.ProyectosReporte" %>
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
            <h5 class="text-white">Control de proyectos</h5>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col">
                    <label for="ListaFacturable">Facturar: </label>
                    <asp:DropDownList CssClass="form-select" ID="ddlFacturar" runat="server" >
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Sí"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col">
                    <label for="ListaCliente">Facturado: </label>
                    <asp:DropDownList CssClass="form-select" ID="ddlFacturado" runat="server" >
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Sí"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col">
                    <button runat="server" id="btnBuscar" class="btn btn-success text-white float-end" style="margin-top:22px;" onserverclick="btnBuscar_ServerClick"><i class="fas fa-search"></i></button>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gvProyectos"
                    ClientIDMode="Static"
                    DataKeyNames="Id, proyecto, estatus, HorasEstimadas, HorasTotales" runat="server" 
                    SortExpression="registro"
                    BorderColor="Black"
                    Width="100%"
                    CssClass="table table-hover table-striped text-center"
                    AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource2"
                    ShowFooter="true">
                <Columns>

                    <asp:BoundField HeaderText="Id"
                        ReadOnly="true" ShowHeader="true" DataField="Id" HeaderStyle-CssClass="hideBound" ItemStyle-CssClass="hideBound"/>

                    <asp:BoundField HeaderText="Proyecto"
                        ReadOnly="true" ShowHeader="true" DataField="proyecto" />

                    <asp:BoundField HeaderText="Estatus"
                        ReadOnly="true" ShowHeader="true" DataField="estatus"/>

                    <asp:BoundField HeaderText="Horas estimadas"
                        ReadOnly="true" ShowHeader="true" DataField="HorasEstimadas" />

                    <asp:BoundField HeaderText="Horas totales"
                        ReadOnly="true" ShowHeader="true" DataField="HorasTotales" />

                </Columns>
            </asp:GridView>
            </div>

            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                ConnectionString="<%$ ConnectionStrings:AST_InternaConnectionString %>"
                SelectCommand ="SELECT p.Id, p.proyecto, case p.estatus when 1 then 'Activo' else 'Inactivo' end as estatus, p.HorasEstimadas,
                                    isnull((SELECT SUM(cantidadHoras) / 60 as Horas 
	                                        FROM 
		                                        (SELECT DATEDIFF(MINUTE, fechaInicio, fechaFinal) as cantidadHoras 
			                                        FROM AST_horasRegistro 
			                                        WHERE IdProyecto = p.Id) a),0) as HorasTotales
                                FROM AST_proyecto p">
            </asp:SqlDataSource>


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
            var gvregistroTable = $("#gvProyectos").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
                            columns: [1, 2, 3, 4]
                        }
                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: [1, 2, 3, 4]
                        }
                    },
                    {
                        extend: 'excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4]
                        }   
                    }
                ],
                "order": [[3, 'desc']],
                dom: '<"dt-top-container"<l><"dt-center-in-div"B><f>r>t<ip>'
            });
        });
    </script>

</asp:Content>
