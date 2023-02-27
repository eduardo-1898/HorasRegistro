<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vacaciones.aspx.cs" Inherits="Formulario_4.Vacaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="card">
        <div class="card-header bg-dark text-white">
            <h5>Solicitud de vacaciones</h5>
        </div>

        <div class="card-body">
            <div class="row">
                <div class="col">
                    <div class="card-body shadow">
                        <h4 class="text-center mb-3">Formulario de solicitud</h4>
                        <div class="row">

                            <div class="col col-12">
                                <label for="slctTipo" class="mb-1">Tipo de solicitud 
                                </label>
                                <asp:DropDownList CssClass="form-select" runat="server" ID="slctTipo">
                                    <asp:ListItem>Vacaciones</asp:ListItem>
                                    <asp:ListItem>Cita Medica</asp:ListItem>
                                    <asp:ListItem>Permiso con goce</asp:ListItem>
                                    <asp:ListItem>Permiso sin goce</asp:ListItem>
                                    <asp:ListItem>Otros</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col col-12 mt-1">
                                <label for="dtFechaInicio" class="mb-1">Fecha Salida
                                    <b style="font-weight:bold; color:red">*</b>
                                </label>
                                <asp:TextBox 
                                    TextMode="Date" 
                                    CssClass="form-control" 
                                    runat="server" 
                                    ID="dtFechaInicio"
                                    OnTextChanged="dtFechaFin_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </div>

                            <div class="col col-12 mt-1">
                                <label for="dtFechaFin" class="mb-1">Fecha Entrada
                                    <b style="font-weight:bold; color:red">*</b>
                                </label>
                                <asp:TextBox 
                                    TextMode="Date" 
                                    CssClass="form-control" 
                                    runat="server" 
                                    ID="dtFechaFin"
                                    OnTextChanged="dtFechaFin_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </div>

                             <div class="col col-12 mt-1">
                                <label for="txtcantidad" class="mb-1">Cantidad de vacaciones solicitadas</label>
                                <asp:TextBox 
                                    TextMode="Number" 
                                    CssClass="form-control" 
                                    runat="server" 
                                    ID="txtcantidad" 
                                    ReadOnly="true">
                                </asp:TextBox>
                            </div>

                            <div class="col col-12 mt-1">
                                <label for="txtcantidad" class="mb-1">Motivo</label>
                                <asp:TextBox  
                                    CssClass="form-control" 
                                    runat="server" 
                                    ID="txtObservacion"
                                    placeholder="Ejemplo: Salida del país">
                                </asp:TextBox>
                            </div>

                            <label style="font-size:11px; margin-top:4px;" >
                                Estas vacaciones se encuentran sujetas a aprobación por parte del supervisor de departamento y el supervidor de operaciones.
                            </label>

                            <div class="col col-12 mt-2">
                                <asp:LinkButton 
                                    CssClass="btn float-end text-white" 
                                    BackColor="#FF6031" 
                                    runat="server" 
                                    ID="btnAgregar"
                                    OnClick="btnAgregar_Click">
                                    <i class="fas fa-plus"></i> Solicitar
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card-body shadow ">

                        <h4 class="text-center mb-3">Vacaciones disponibles</h4>
                        <div class="row">
                            <div class="col col-6">
                                <label for="txtFechaIngreso">Fecha de ingreso</label>
                                <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ReadOnly="true" ID="txtFechaIngreso"></asp:TextBox>
                            </div>
                        
                            <div class="col col-6">
                                <label for="txtVacacionesDisponibles">Vacaciones dispobibles</label>
                                <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" ID="txtVacacionesDisponibles"></asp:TextBox>
                            </div>
                            
                            <label style="font-size:11px; margin-top:4px;" >
                                Si la fecha de ingreso es la misma que la del día de hoy, contacte al administrador del sistema para modificar la fecha de ingreso.
                            </label>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
