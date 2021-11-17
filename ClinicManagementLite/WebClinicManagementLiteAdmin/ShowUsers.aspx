<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="ShowUsers.aspx.cs" Inherits="WebClinicManagementLiteAdmin.ShowUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="d-flex flex-column flex-md-row align-items-center pb-3 mb-4 border-bottom">
            <a href="Index.aspx" class="d-flex align-items-center text-dark text-decoration-none">
                <h1>Lista de usuarios</h1>
            </a>

            <nav class="d-inline-flex mt-2 mt-md-0 ms-md-auto">
                <asp:Button CssClass="btn btn-primary" ID="btnCreateUser" runat="server" Text="Crear Usuario" OnClick="btnCreateUser_Click"></asp:Button>
            </nav>
        </div>
        <div class="row" style="margin-top: 32px;">

            <div class="col-sm-1 col-md-8 col-lg-8"></div>

            <div class="col-sm-10 col-md-4 col-lg-4">
                <label for="ddlDoctor" class="form-label">Doctor</label>
                <asp:DropDownList ID="ddlDoctor" CssClass="form-select" runat="server" AutoPostBack="true"></asp:DropDownList>
                <br />
            </div>
            <div id="divContainerNoQueues" runat="server" class="col-sm-12 col-md-12 col-lg-12" visible="false">
                <h3>No cuenta con usuarios.</h3>
            </div>
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body table-responsive">
                        <asp:GridView ID="gdvUsers" Width="100%" CssClass="table table-striped table-bordered table-hover" runat="server" OnSelectedIndexChanged="gdvUsers_SelectedIndexChanged">
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
