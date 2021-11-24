<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="ShowSchedules.aspx.cs" Inherits="WebClinicManagementLiteAdmin.ShowSchedules" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="d-flex flex-column flex-md-row align-items-center pb-3 mb-4 border-bottom">
            <a href="Index.aspx" class="d-flex align-items-center text-dark text-decoration-none">
                <h1>Lista de horarios</h1>
            </a>

            <nav class="d-inline-flex mt-2 mt-md-0 ms-md-auto">
                <asp:Button CssClass="btn btn-primary" ID="btnCreateSchedule" runat="server" Text="Crear horario" OnClick="btnCreateSchedule_Click"></asp:Button>
            </nav>
        </div>
        <div class="row" style="margin-top: 32px;">

            <div class="col-sm-1 col-md-1 col-lg-1"></div>
            <div class="col-sm-10 col-md-10 col-lg-10">
                <div class="panel panel-default">
                    <div class="panel-body table-responsive">
                        <asp:GridView ID="gdvSchedules" Width="100%" CssClass="table table-striped table-bordered table-hover" runat="server" OnSelectedIndexChanged="gdvSchedules_SelectedIndexChanged">
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-sm-1 col-md-1 col-lg-1"></div>

            <footer class="my-5 pt-5 text-muted text-center text-small">
                <p class="mb-1">&copy; 2021 Clinic Management Lite</p>
                <ul class="list-inline">
                    <li class="list-inline-item"><a href="Index.aspx">Inicio</a></li>
                </ul>
            </footer>

        </div>
    </div>

</asp:Content>
