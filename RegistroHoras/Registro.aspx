<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Formulario_4.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <style>
        #MainContent_registroTable_filter {
          margin-bottom:5px;
        }

        #MainContent_registroTable_length {
          margin-bottom:5px;
        }

        .dt-buttons{
            margin-left:30%;
        }

    </style>
    <style src="https://cdn.datatables.net/rowgroup/1.1.4/css/rowGroup.dataTables.min.css"></style>

            <div class="card">
                <div class="card-header bg-dark text-white">
                    <h5>Registro</h5>
                </div>

                <div class="card-body">
                    
                    <div class="table-responsive">
                        <asp:GridView ID="registroTable"
                            DataKeyNames="fechaInicio, fechaFinal, actividad, comentario, encargado, departamento, proyecto, cliente, descripcion, id, HorasTotales" runat="server" 
                            SortExpression="registro"
                            BorderColor="Black"
                            CssClass="table table-hover "
                            Width="100%"
                            AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSource1" 
                            OnRowCommand="registro_RowCommand">
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

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>

                                    <asp:Button CssClass="btn text-white" Width="90px" ID="editar"
                                    BackColor="#FF6031" runat="server" CommandName="editar"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    Text="Editar" />

                                </ItemTemplate>
                            </asp:TemplateField>

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
    <script src="https://cdn.datatables.net/rowgroup/1.1.4/js/dataTables.rowGroup.min.js"></script>
    <script src="https://cdn.datatables.net/searchbuilder/1.3.2/js/dataTables.searchBuilder.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var gvregistroTable = $("#MainContent_registroTable").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "pageLength": 7,
                "ordering": true,
                "lengthMenu": [5, 7, 10, 15, 30],   
                "language": {
                    url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
                },
                searchBuilder: {
                    columns: [3,4,5]
                },
                rowGroup: {
                    dataSrc: 3
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
                dom: 'QlBfrtip'

            });
        });
    </script>




</asp:Content>
