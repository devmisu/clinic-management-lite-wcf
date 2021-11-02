<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebPatient.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="www/css/Index.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
      <asp:Label ID="lblWelcome" CssClass="navbar-brand col-md-3 col-lg-2 me-0 px-3" runat="server">Bienvenido</asp:Label>
      <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <input class="form-control form-control-dark w-100" type="text" placeholder="Buscar" aria-label="Buscar">
      <div class="navbar-nav">
        <div class="nav-item text-nowrap">
            <asp:LinkButton CssClass="nav-link px-3" OnClick="btnLogOut_Click" runat="server">Cerrar sesion</asp:LinkButton>
        </div>
      </div>
    </header>

    <div class="container-fluid">
      <div class="row">
        <nav id="sidebarMenu" class="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse">
          <div class="position-sticky pt-3">
            <ul class="nav flex-column">
              <li class="nav-item">
                <a class="nav-link active" href="Index.aspx?option=appointments">
                  Mis Citas
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="Index.aspx?option=patient">
                  Mi Perfil
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="Index.aspx?option=medical_record">
                  Historia Clinica
                </a>
              </li>
            </ul>
          </div>
        </nav>

        <div class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
          <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
            <h1 class="h2">Mis Citas</h1>
            <!--div class="btn-toolbar mb-2 mb-md-0">
              <div class="btn-group me-2">
                <button type="button" class="btn btn-sm btn-outline-secondary">Share</button>
                <button type="button" class="btn btn-sm btn-outline-secondary">Export</button>
              </div>
              <button type="button" class="btn btn-sm btn-outline-secondary dropdown-toggle">
                <span data-feather="calendar"></span>
                This week
              </button>
            </div-->
          </div>

            <asp:Panel ID="viewError" class="alert alert-danger alert-dismissible mb-4" role="alert" runat="server" Visible="false">
                <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <div class="table-responsive">
                <asp:GridView id="gridView" CssClass="table table-striped table-sm" AutoGenerateColumns="false" AllowSorting="false" EmptyDataText="No se encontraron datos." BorderWidth="0" runat="server"
                    OnSelectedIndexChanged="gridView_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" />
                        <asp:BoundField DataField="User.Area.Name" HeaderText="Especialidad" />
                        <asp:BoundField DataField="User.FirstName" HeaderText="Doctor" />
                        <asp:BoundField DataField="Date" HeaderText="Fecha" />
                        <asp:BoundField DataField="StartHour" HeaderText="Hora Inicio" />
                        <asp:BoundField DataField="EndHour" HeaderText="Hora Fin" />
                        <asp:BoundField DataField="ArrivalHour" HeaderText="Hora Llegada" />
                        <asp:BoundField DataField="DepartureHour" HeaderText="Hora Salida" />
                        <asp:BoundField DataField="State" HeaderText="Estado" />
                        <asp:CommandField ButtonType="Image" HeaderText="Acciones"
                            ShowSelectButton="true" SelectImageUrl="~/www/img/icon_view_16.png" SelectText="Ver" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
      </div>
    </div>
</asp:Content>
