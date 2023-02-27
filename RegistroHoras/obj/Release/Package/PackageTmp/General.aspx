<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="General.aspx.cs" Inherits="Formulario_4.General" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/series-label.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://code.highcharts.com/modules/accessibility.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js" integrity="sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk=" crossorigin="anonymous"></script>
    <link href="Content/calendar.css" rel="stylesheet" />


    <div class="row mb-2">

        <div class="container mt-3">
            <div class="left">
                <div class="calendar">
                    <div class="month">
                        <i class="fas fa-angle-left prev"></i>
                        <div class="date">december 2015</div>
                        <i class="fas fa-angle-right next"></i>
                    </div>
                    <div class="weekdays mb-2">
                        <div>D</div>
                        <div>L</div>
                        <div>M</div>
                        <div>X</div>
                        <div>J</div>
                        <div>V</div>
                        <div>S</div>
                    </div>
                    <div class="days"></div>
                    <div class="goto-today">
                        <button class="today-btn">Hoy</button>
                    </div>
                </div>
            </div>
            <div class="right">
                <div class="today-date">
                    <div class="event-date">

                    </div>
                </div>
                <div class="events"></div>
            </div>
            <a class="add-event" href="Principal.aspx">
                <i class="fas fa-plus"></i>
            </a>
        </div>

    </div>

    <script src="Scripts/calendar.js"></script>

</asp:Content>
