<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoDepartamento.aspx.cs" Inherits="Formulario_4.MoDepartamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


        <div class="container mb-auto">
        <div class="card border-black mb-2">
            <div class="card text-white bg-dark">
                <div class="card-header">
                    <h5>Modificar o agregar departamento</h5>
                </div>
            </div>

            <div class="card-body">

                <div class="row mb-3">
                    <div class="col-6">
                        <label>Departamento:</label>
                        <asp:TextBox runat="server" ID="txtEncargado" CssClass="form-control" placeholder="Nombre" />
                    </div>
                    <div class="col-6" style="margin-top:30px;">
                        <asp:CheckBox  runat="server" ID="cbxEstado" CssClass="form-check" Text="Activo"/>
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
    </div>


</asp:Content>


