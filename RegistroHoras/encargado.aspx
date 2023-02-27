<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="encargado.aspx.cs" Inherits="Formulario_4.encargado" %>
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

        #MainContent_Encargado table {
            width:100%;
        }
    </style>

        <div class=" container-fluid">

            <div class="card">
                    <div class="card-header text-white bg-dark">
                        <div class="row">
                            <div class="col col-6">
                                <h5>Encargado</h5>
                            </div>
                            <div class="col col-6 float-end">
                                <asp:Button runat="server" ID="btnagregar" Width="90" OnClick="btnagregar_Click"
                                    CssClass="btn float-end text-white" BackColor="#FF6031" Text="Agregar"></asp:Button>
                            </div>
                        </div>
                    </div>

                <div class="card-body">

                    <div class="table-responsive">
                        
                    <asp:GridView ID="encargadoTable" 
                        DataSourceID="SqlDataSource1"
                        DataKeyNames="Encargado,id" runat="server" 
                        SortExpression="encargado"
                        CssClass="table table-striped table-hover" 
                        OnRowCommand="Encargado_RowCommand"
                        AutoGenerateColumns="False" 
                        Width="100%"
                        BorderColor="Black">
                        <Columns>
                            <asp:BoundField HeaderText="Encargado"
                                ReadOnly="true" ShowHeader="true" DataField="encargado" />

                            <asp:BoundField HeaderText="Cédula"
                                ReadOnly="true" ShowHeader="true" DataField="cedula" />

                            <asp:BoundField HeaderText="Teléfono"
                                ReadOnly="true" ShowHeader="true" DataField="telefono" />

                            <asp:BoundField HeaderText="Correo"
                                ReadOnly="true" ShowHeader="true" DataField="correo" />

                            <asp:BoundField HeaderText="Cumpleaños"
                                ReadOnly="true" ShowHeader="true" DataField="cumpleanos" />

                            <asp:BoundField HeaderText="Estatus"
                                ReadOnly="true" ShowHeader="true" DataField="Estatus" />

                            <asp:BoundField HeaderText="Departamento"
                                ReadOnly="true" ShowHeader="true" DataField="departamento" />

                            <asp:BoundField HeaderStyle-CssClass="hideBound"
                                ReadOnly="true" ShowHeader="false" ItemStyle-CssClass="hideBound" DataField="id"   />

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>

                                    <asp:Button CssClass="btn text-white" ID="editar"
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
                        SelectCommand="SELECT [Encargado],[telefono],[cedula],[correo],[cumpleanos],a.[id],b.[departamento] as departamento, case a.Estatus when 1 then 'Activo' else 'Inactivo' end as Estatus 
                        FROM [AST_encargado] a, 
                        [AST_departamento] b  
                        where a.idDepartamento = b.id
                        order by encargado asc"
                        DeleteCommand="DELETE FROM AST_encargado where encargado=@encargado">
                    </asp:SqlDataSource>

                </div>
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
