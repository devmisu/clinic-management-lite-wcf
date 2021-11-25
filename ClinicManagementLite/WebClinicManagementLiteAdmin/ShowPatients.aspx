<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="ShowPatients.aspx.cs" Inherits="WebClinicManagementLiteAdmin.ShowPatients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="d-flex flex-column flex-md-row align-items-center pb-3 mb-4 border-bottom">
            <a href="Index.aspx" class="d-flex align-items-center text-dark text-decoration-none">
                <h1>Lista de pacientes</h1>
            </a>

            <nav class="d-inline-flex mt-2 mt-md-0 ms-md-auto">
                <asp:Button CssClass="btn btn-primary" ID="btnCreatePatient" runat="server" Text="Crear Paciente" OnClick="btnCreatePatient_Click"></asp:Button>
            </nav>
        </div>
        <div class="row" style="margin-top: 32px;">

            <div id="divContainerNoQueues" runat="server" class="col-sm-12 col-md-12 col-lg-12" visible="false">
                <h3>No cuenta con pacientes.</h3>
            </div>
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body table-responsive">
                        <asp:GridView ID="gdvPatients" Width="100%" CssClass="table table-striped table-bordered table-hover" runat="server" OnSelectedIndexChanged="gdvPatients_SelectedIndexChanged">
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <footer class="my-5 pt-5 text-muted text-center text-small">
                <p class="mb-1">&copy; 2021 Clinic Management Lite</p>
                <ul class="list-inline">
                    <li class="list-inline-item"><a href="Index.aspx">Inicio</a></li>
                </ul>
            </footer>

        </div>
    </div>

</asp:Content>
