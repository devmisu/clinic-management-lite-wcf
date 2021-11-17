<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Queue.aspx.cs" Inherits="WebPatient.Queue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
      <div>
        <div class="py-5 text-center">
          <h2>Detalle</h2>
        </div>

        <div class="row g-5">
          <div class="col">
            <h4 class="mb-3">Cita</h4>

            <asp:Panel ID="viewError" class="alert alert-danger alert-dismissible mb-3" role="alert" runat="server" Visible="false">
                <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <asp:Panel ID="viewSuccess" class="alert alert-success alert-dismissible mb-3" role="alert" runat="server" Visible="false">
                <asp:Label ID="lblSuccessMessage" runat="server"></asp:Label>
                <a href="Index.aspx" class="alert-link">Volver al inicio</a>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <div>
              <div class="row g-3">

                <div class="col-sm-6">
                    <label for="txtArea" class="form-label">Especialidad</label>
                    <asp:TextBox ID="txtArea" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtState" class="form-label">Estado</label>
                    <asp:TextBox ID="txtState" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtDate" class="form-label">Fecha</label>
                    <asp:TextBox ID="txtDate" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtStartHour" class="form-label">Hora</label>
                    <asp:TextBox ID="txtStartHour" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <hr class="my-4">

                <h4 class="mb-3">Doctor</h4>

                <div class="col-sm-8">
                    <label for="txtDoctor" class="form-label">Nombres y Apellidos</label>
                    <asp:TextBox ID="txtDoctor" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-4">
                    <label for="txtDoctorDni" class="form-label">DNI</label>
                    <asp:TextBox ID="txtDoctorDni" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtDoctorPhone" class="form-label">Celular</label>
                    <asp:TextBox ID="txtDoctorPhone" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtDoctorEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtDoctorEmail" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <hr class="my-4">
              
                <asp:Button ID="btnCancel" CssClass="w-100 btn btn-danger btn-lg" runat="server" Text="Cancelar" OnClick="btnCancel_Click" />
            </div>
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
