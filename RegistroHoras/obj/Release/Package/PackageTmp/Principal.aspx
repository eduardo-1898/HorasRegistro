<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Formulario_4.Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="alert alert-success alert-dismissible fade show" visible="false" role="alert" id="mensaje" runat="server">
        <strong id="textoMensaje" runat="server"></strong>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>

    <div class="alert alert-danger alert-dismissible fade show" visible="false" role="alert" id="mensajeError" runat="server">
        <strong id="textoMensajeError" runat="server"></strong>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>

    <div class="card">
        <div class="card-header text-white bg-dark">
            <h5>Registro de tiempos</h5>
        </div>
        <div class="card-body">
            <div class="row mb-3">
                <div class="col col-6">
                    <div class="dropdown">
                        <label for="ListaEncargado">Encargado: </label>
                        <asp:DropDownList CssClass="form-select" ID="ListaEncargado" BackColor="LightGray" runat="server" >
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-6">
                    <label for="ListaDepartamento">Departamento:</label>
                    <asp:DropDownList CssClass="form-select" ID="ListaDepartamento" runat="server" >
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col col-6">
                    <label for="dtFechaInicio">Fecha Inicio:</label>
                    <asp:TextBox ID="dtFechaInicio" CssClass="form-control" TextMode="Date" runat="server">
                    </asp:TextBox>
                </div>

                <div class="col-6">
                    <label for="ListaFacturable">Facturable: </label>
                    <asp:DropDownList CssClass="form-select" ID="ListaFacturable" runat="server" OnSelectedIndexChanged="ListaFacturable_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>

            </div>

            <div class="row mb-3">
                <div class="col-6">
                    <label for="txtHoraInicio">Hora Inicio: </label>
                    <asp:TextBox CssClass="form-control" ID="txtHoraInicio" TextMode="Time" runat="server" >
                    </asp:TextBox>
                </div>
                <div class="col-6">
                    <label for="txtHoraFin">Hora Fin: </label>
                    <asp:TextBox CssClass="form-control" ID="txtHoraFin" TextMode="Time" runat="server">
                    </asp:TextBox>
                </div>
            </div>

            <div class="row mb-3">

                <div class="col-6">
                    <label for="ListaCliente">Cliente: </label>
                    <asp:DropDownList CssClass="form-select" ID="ListaCliente" runat="server" >
                    </asp:DropDownList>
                </div>

                <div class="col-6">
                    <label for="ListaProyecto">Proyecto:</label>
                    <asp:DropDownList CssClass="form-select" ID="ListaProyecto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListaProyecto_SelectedIndexChanged" >
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mb-3">

                <div class="col-6">
                    <label for="txtActividad">Actividad:</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtActividad"
                        placeholder="Tipo de actividad" >
                    </asp:TextBox>

                </div>

                <div class="col col-6">
                    <label for="txtComentario">Informacion Adicional: </label>
                    <asp:TextBox CssClass="form-control" ID="txtComentario" runat="server"></asp:TextBox>
                </div>

            </div>

            <div class="row mb-3">

                <div class="col-6">
                    <asp:CheckBox runat="server" Text="Extras" ID="cbxExtras" CssClass="form-control-check"/>
                </div>

            </div>


            <hr style="color: black" />
            <div class="row d-flex justify-content-center">
                <div class="col-5 mt-3 md-3">
                    <asp:Button CssClass="btn text-white w-100" BackColor="#FF6031"
                        ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" />
                </div>

            </div>
        </div>

    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:AST_InternaConnectionString %>"
        SelectCommand="SELECT [fechaInicio],[fechaFinal],[actividad],[comentario] FROM AST_horasRegistro"
        DeleteCommand="DELETE FROM AST_encargado where encargado=@encargado"></asp:SqlDataSource>

</asp:Content>
