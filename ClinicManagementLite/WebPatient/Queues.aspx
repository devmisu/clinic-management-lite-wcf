<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Queues.aspx.cs" Inherits="WebPatient.Queues" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="container">
                <header class="d-flex flex-wrap align-items-center justify-content-center justify-content-md-between py-3 mb-4 border-bottom">
                  <h4 class="d-flex align-items-center col-md-3 mb-2 mb-md-0 text-dark text-decoration-none">
                      <asp:Label ID="lblWelcome" CssClass="" runat="server">Bienvenido</asp:Label>
                  </h4>

                  <ul class="nav col-12 col-md-auto mb-2 justify-content-center mb-md-0">
                    <li>
                        <asp:LinkButton ID="btnHome" CssClass="nav-link px-2 link-dark" runat="server" OnClick="btnHome_Click">Mis Citas</asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnQueue" CssClass="nav-link px-2 link-secondary" runat="server" OnClick="btnQueue_Click">Mis Solicitudes</asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnProfile" CssClass="nav-link px-2 link-dark" runat="server" OnClick="btnProfile_Click">Perfil</asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnMedicalRecord" CssClass="nav-link px-2 link-dark" runat="server" OnClick="btnMedicalRecord_Click">Historias Medicas</asp:LinkButton>
                    </li>
                  </ul>

                  <div class="col-md-3 text-end">
                    <asp:LinkButton CssClass="btn btn-primary" OnClick="btnLogOut_Click" runat="server">Cerrar Sesion</asp:LinkButton>
                  </div>
                </header>

                <asp:Panel ID="viewError" class="alert alert-danger alert-dismissible mb-4" role="alert" runat="server" Visible="false">
                    <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </asp:Panel>

                <h4>Mis Solicitudes</h4>

                <br />

                <asp:LinkButton ID="btnCreateQueue" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btnCreateQueue_Click">Solicitar Cita</asp:LinkButton>

                <br />
                <br />

                <div class="table-responsive">
                    <asp:GridView id="gridViewQueue" CssClass="table table-striped table-sm" EmptyDataText="No se encontraron datos." BorderWidth="0" runat="server"
                        OnSelectedIndexChanged="gridViewQueue_SelectedIndexChanged">
                    </asp:GridView>
                </div>
            </div>
</asp:Content>
