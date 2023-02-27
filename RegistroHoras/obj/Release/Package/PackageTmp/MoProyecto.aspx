<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoProyecto.aspx.cs" Inherits="Formulario_4.MoProyecto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container mb-auto">
        <div class="card border-black mb-2">
            <div class="card text-white bg-dark">
                <div class="card-header">
                    <h5>Modificar o agregar proyecto</h5>
                </div>
            </div>

            <div class="card-body">

                <div class="row mb-3">
                    <div class="col-4">
                        <label>Proyecto:</label>
                        <asp:TextBox runat="server" ID="txtEncargado" CssClass="form-control" placeholder="Proyecto" />
                    </div>
                    <div class="col-4">
                        <label>Cliente:</label>
                        <asp:DropDownList CssClass="form-select" ID="ListaCliente" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-2">
                        <label>Estimadas:</label>
                        <asp:TextBox CssClass="form-control" ID="txtEstimado" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-2">
                        <label>Trabajadas:</label>
                        <asp:TextBox CssClass="form-control" ID="txtHoras" ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-2" style="margin-top:10px;">
                        <asp:CheckBox  runat="server" CssClass="form-check" ID="cbxEstado" Text=" Activo"/>
                        <asp:CheckBox  runat="server" CssClass="form-check" ID="cbxFacturar" Text=" Facturar"/>
                        <asp:CheckBox  runat="server" CssClass="form-check" ID="cbxFacturado" Text=" Facturado"/>
                    </div>
                </div>

                <hr />

                <div class="row justify-content-center">
                    <div class="col-5 mt-3 md-3">
                        <asp:Button CssClass="btn w-100 text-white"
                            OnClick="Guardar_Click"
                            BackColor="#FF6031" ID="Guardar" runat="server"
                            Text="Guardar" />

                    </div>

                    <div class="col-5 mt-3 md-3 ">

                        <asp:Button CssClass="btn w-100 text-white"
                            OnClick="Cancelar_Click"
                            BackColor="#FF6031" ID="Cancelar" runat="server"
                            Text="Cancelar" />

                    </div>
                </div>
            </div>
        </div>

</asp:Content>
