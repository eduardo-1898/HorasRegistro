<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoEncargado.aspx.cs" Inherits="Formulario_4.MoEncargado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mb-auto">
        <div class="card border-black mb-2">
            <div class="card text-white bg-dark">
                <div class="card-header">
                    <h5>Modificar Datos</h5>
                </div>
            </div>

            <div class="card-body">

                <div class="row mb-3">
                    <div class="col-6">
                        <label for="txtEncargado">Encargado:</label>
                        <asp:TextBox runat="server" ID="txtEncargado" class="form-control" placeholder="Jorge Perez" />
                    </div>

                    <div class="col-6">
                        <label for="txtCedula">Cédula:</label>
                        <asp:TextBox runat="server" class="form-control" ID="txtCedula" placeholder="1-1717-0761" />
                    </div>
                </div>


                <div class="row mb-3">
                    <div class="col-6">
                        <label for="txtTelefono">Teléfono:</label>
                        <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" placeholder="70590207" />
                    </div>

                    <div class="col-6">
                        <label for="txtEmail">Correo:</label>
                        <asp:TextBox TextMode="Email" runat="server" CssClass="form-control" ID="txtEmail" placeholder="Example@gmail.com" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-6">
                        <label for="txtDate">Cumpleaños:</label>
                        <asp:TextBox TextMode="Date" runat="server" ID="txtDate" CssClass="form-control"/>
                    </div>
                    <div class="col-6">
                        <label for="ListaDepartamento">Departamento</label>
                        <asp:DropDownList CssClass="form-select" ID="ListaDepartamento" runat="server" >
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-3">
                    
                    <div class="col col-6">                    
                        <label for="dtFechaIngreso">Fecha de Ingreso</label>
                        <asp:TextBox TextMode="date" runat="server" id="dtFechaIngreso" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-6">
                        <label for="txtEmail">Nombre de usuario:</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUsername" placeholder="Nombre de usuario" />
                    </div>

                </div>

                <div class="row">
                    <div class="col-6 ">
                        <div class="form-check">
                            <asp:CheckBox runat="server" ID="cbxSupervisor"/>
                            <label for="cbxEstado" class="form-check-label">Supervisor</label>
                        </div>
                        <div class="form-check">
                            <asp:CheckBox runat="server" ID="cbxAdmin"/>
                            <label for="cbxEstado" class="form-check-label">Admin</label>
                        </div>
                        <div class="form-check">
                            <asp:CheckBox runat="server" ID="cbxOperaciones"/>
                            <label for="cbxEstado" class="form-check-label">Operaciones</label>
                        </div>
                        <div class="form-check">
                            <asp:CheckBox runat="server" ID="cbxEstado"/>
                            <label for="cbxEstado" class="form-check-label">Activo</label>
                        </div>
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
