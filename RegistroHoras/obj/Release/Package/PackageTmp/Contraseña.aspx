<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contraseña.aspx.cs" Inherits="Formulario_4.Contraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="alert alert-success alert-dismissible fade show" visible="false" role="alert" id="mensaje" runat="server">
        <strong id="textoMensaje" runat="server"></strong>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>

    <div class="alert alert-danger alert-dismissible fade show" visible="false" role="alert" id="mensajeError" runat="server">
        <strong id="textoMensajeError" runat="server"></strong>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>

        <div class="card border-black mb-2">
            <div class="card text-white bg-dark">
                <div class="card-header">
                    <h5>Modificar contraseña</h5>
                </div>
            </div>

            <div class="card-body">

                <div class="row mb-3">
                    <div class="col-6">
                        <label>Contraseña actual:</label>
                        <asp:TextBox runat="server" TextMode="Password" ID="txtContrasenaVieja" CssClass="form-control" placeholder="Contraseña anterior" />
                    </div>
                    <div class="col-6">
                        <label>Contraseña nueva:</label>
                        <asp:TextBox runat="server" TextMode="Password" ID="txtContrasenaNueva" CssClass="form-control" placeholder="Contraseña nueva" />
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
