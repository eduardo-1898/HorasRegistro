<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoClientes.aspx.cs" Inherits="Formulario_4.MoClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mb-auto" >
        <div class="card border-black mb-auto">
            <div class="card text-white bg-dark">
                <div class="card-header">
                    <h5>Modificar Datos</h5>
                </div>
            </div>

            <div class="card-body">
                <div class="row mb-3">
                    <div class="col-6">
                        <label>Cliente:</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCliente" placeholder="Cliente" />
                    </div>


                    <div class="col-6">
                        <label>Representante:</label>
                        <asp:TextBox runat="server" ID="txtRepresentante" CssClass="form-control" placeholder="Representante" />
                    </div>
                </div>

                <hr />

                <div class="row justify-content-center">
                    <div class="col-5 mt-3 md-3">

                        <asp:Button CssClass="btn w-100 text-white" BackColor="#FF6031"
                            ID="Guardar" CommandName="Guardar" OnClick="Guardar_Click" runat="server"
                            Text="Guardar" />
                    </div>

                    <div class="col-5 mt-3 md-3">
                        <asp:Button CssClass="btn w-100 text-white" 
                            BackColor="#FF6031" ID="Cancelar" OnClick="Cancelar_Click" runat="server"
                            Text="Cancelar" />

                  </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

