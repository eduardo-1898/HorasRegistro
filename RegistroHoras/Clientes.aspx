<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Formulario_4.Clientes" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

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

        #MainContent_clienteTable table {
            width:100%;
        }
    </style>


        <div class="card border-black">
            <div class="card-header bg-dark text-white">
                <div class="row">
                    <div class="col col-6">
                        <h5>Clientes</h5>
                    </div>

                    <div class="col col-6 float-end">
                        <asp:Button runat="server" ID="agregar" Width="90" OnClick="agregar_Click"
                            CssClass="btn text-white float-end" BackColor="#FF6031" Text="Agregar" />

                    </div>
                </div>
            </div>


            <div class="card-body">

                <div class="table-responsive">
                    <asp:GridView ID="clienteTable" 
                        DataSourceID="SqlDataSource1" 
                        DataKeyNames="cliente"
                        BorderColor="Black"
                        Width="100%"
                        CssClass="table table-striped" 
                        runat="server" 
                        OnRowCommand="cliente_RowCommand"
                        AutoGenerateColumns="False" 
                        >
                        <Columns>

                            <asp:BoundField DataField="Cliente" HeaderText="Clientes"
                                SortExpression="cliente"
                                ReadOnly="true" ShowHeader="true" />

                            <asp:BoundField DataField="representante" HeaderText="Representante"
                                SortExpression="representante" ReadOnly="true" ShowHeader="true" />

                            <asp:BoundField HeaderText="Estatus"
                                ReadOnly="true" ShowHeader="true" DataField="Estatus" />

                            <asp:BoundField HeaderStyle-CssClass="hideBound"
                                ReadOnly="true" ShowHeader="true" DataField="id" ItemStyle-CssClass="hideBound" />

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button CssClass="btn text-white" CommandName="editarcli"
                                        CommandArgument='<%# Container.DataItemIndex %>' BackColor="#FF6031"
                                        Text="Editar" ID="editar" runat="server"></asp:Button>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:AST_InternaConnectionString %>"
                    SelectCommand="SELECT [cliente],[representante],[id],case Estatus when 1 then 'Activo' else 'Inactivo' end as Estatus FROM [AST_cliente] order by cliente asc"
                    DeleteCommand="DELETE FROM AST_cliente where cliente=@cliente"></asp:SqlDataSource>


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
            var gvregistroTable = $("#MainContent_clienteTable").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
