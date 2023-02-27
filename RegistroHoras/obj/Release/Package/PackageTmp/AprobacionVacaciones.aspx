<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AprobacionVacaciones.aspx.cs" Inherits="Formulario_4.AprobacionVacaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style>
        div.dt-top-container {
          display: grid;
          margin-bottom:5px;
          grid-template-columns: auto auto auto;
        }

        div.dt-center-in-div {
          margin: 0 auto;
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

    </style>


    <div class="card">
        <div class="card-header bg-dark text-white">
            <h4>Aprobación de vacaciones</h4>
        </div>
        <div class="card-body shadow">
                <div class="table-responsive text-center">
                        
                    <asp:GridView ID="encargadoTable" 
                        DataSourceID="SqlDataSource1"
                        DataKeyNames="id, RevisadoSupervisor, AprobacionDepartamento" runat="server" 
                        SortExpression="encargado"
                        CssClass="table table-striped table-hover table-sm" 
                        OnRowCommand="encargadoTable_RowCommand"
                        OnRowDataBound="encargadoTable_RowDataBound"
                        AutoGenerateColumns="False" 
                        Width="100%"
                        BorderColor="Black">
                        <Columns>
                            <asp:BoundField HeaderText="Fecha de solicitud"
                                ReadOnly="true" ShowHeader="true" DataField="FechaSolicitud" DataFormatString="{0:dd/MM/yyyy}"/>

                            <asp:BoundField HeaderText="Fecha de salida"
                                ReadOnly="true" ShowHeader="true" DataField="FechaSalida" DataFormatString="{0:dd/MM/yyyy}" />

                            <asp:BoundField HeaderText="Fecha de entrada"
                                ReadOnly="true" ShowHeader="true" DataField="FechaEntrada" DataFormatString="{0:dd/MM/yyyy}" />

                            <asp:BoundField HeaderText="Tipo de solicitud"
                                ReadOnly="true" ShowHeader="true" DataField="TipoSolicitud" />

                            <asp:BoundField HeaderText="Solicitante"
                                ReadOnly="true" ShowHeader="true" DataField="Solicitante" />

                            <asp:BoundField HeaderText="Cantidad de días"
                                ReadOnly="true" ShowHeader="true" DataField="CantidadDias" />

                            <asp:BoundField HeaderStyle-CssClass="hideBound"
                                ReadOnly="true" ShowHeader="false" ItemStyle-CssClass="hideBound" DataField="id"   />

                            <asp:TemplateField HeaderText="Supervisor">
                                <ItemTemplate>

                                         <asp:LinkButton CssClass="btn btn-success text-white btn-sm" 
                                            ID="aprobarSup" 
                                            runat="server" 
                                            CommandName="AprobarSupervisor"
                                            CommandArgument='<%# Container.DataItemIndex %>'>
                                            <i class="fas fa-check"></i>
                                        </asp:LinkButton>

                                        <asp:LinkButton CssClass="btn btn-danger text-white btn-sm" 
                                            ID="rechazarSup" 
                                            runat="server" 
                                            CommandName="RechazarSupervisor"
                                            CommandArgument='<%# Container.DataItemIndex %>'>
                                            <i class="fas fa-times"></i>
                                        </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Operaciones">
                                <ItemTemplate>

                                    <asp:LinkButton CssClass="btn btn-success text-white btn-sm" 
                                        ID="aprobarOp" 
                                        runat="server" 
                                        CommandName="AprobarOperaciones"
                                        CommandArgument='<%# Container.DataItemIndex %>'>
                                        <i class="fas fa-check"></i>
                                    </asp:LinkButton>

                                    <asp:LinkButton CssClass="btn btn-danger text-white btn-sm" 
                                        ID="rechazarOp" 
                                        runat="server" 
                                        CommandName="RechazarOperaciones"
                                        CommandArgument='<%# Container.DataItemIndex %>'>
                                        <i class="fas fa-times"></i>
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    </div>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConnectionString="<%$ ConnectionStrings:AST_InternaConnectionString %>"
                        SelectCommand="SELECT *,b.Encargado as Solicitante 
                            FROM AST_VacacionesRegistro a 
                            JOIN AST_encargado b ON a.idEncargado = b.Id 
                            WHERE (RevisadoSupervisor = 1 AND AprobacionDepartamento = 1) 
                            AND a.id not in(SELECT ID FROM AST_VacacionesRegistro WHERE RevisadoOperaciones = 1 AND RevisadoSupervisor=1)
                            OR (RevisadoSupervisor = 0 AND RevisadoOperaciones = 0)">
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

    <script type="text/javascript">
        $(document).ready(function () {
            var gvregistroTable = $("#MainContent_encargadoTable").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "pageLength": 7,
                "ordering": false,
                "lengthMenu": [5, 7, 10, 15, 30],
                "language": {
                    url: "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                buttons: [

                ],
                dom: '<"dt-top-container"<l><"dt-center-in-div"B><f>r>t<ip>'
            });
        });
    </script>

</asp:Content>
