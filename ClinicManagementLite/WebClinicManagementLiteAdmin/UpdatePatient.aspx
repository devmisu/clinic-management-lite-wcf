<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="UpdatePatient.aspx.cs" Inherits="WebClinicManagementLiteAdmin.UpdatePatient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
      <div>
        <div class="py-5 text-center">
          <h2>Editar paciente</h2>
        </div>

        <div class="row g-5">
          <div class="col">
            <h4 class="mb-3">Formulario</h4>

            <asp:Panel ID="viewError" class="alert alert-danger alert-dismissible mb-3" role="alert" runat="server" Visible="false">
                <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <asp:Panel ID="viewSuccess" class="alert alert-success alert-dismissible mb-3" role="alert" runat="server" Visible="false">
                <asp:Label ID="lblSuccessMessage" runat="server"></asp:Label>
                <a href="ShowPatients.aspx" class="alert-link">Volver al listado de pacientes</a>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <div>
              <div class="row g-3">

                <div class="col-sm-6">
                    <label for="txtFirstName" class="form-label">Nombres</label>
                    <asp:TextBox ID="txtFirstName" CssClass="form-control" textMode="SingleLine" runat="server"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtLastName" class="form-label">Apellidos</label>
                    <asp:TextBox ID="txtLastName" CssClass="form-control" textMode="SingleLine" runat="server"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtDni" class="form-label">DNI</label>
                    <asp:TextBox ID="txtDni" CssClass="form-control" textMode="SingleLine" MaxLength="8" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtPhone" class="form-label">Celular</label>
                    <asp:TextBox ID="txtPhone" CssClass="form-control" textMode="Phone" MaxLength="9" runat="server"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" CssClass="form-control" textMode="Email" placeholder="nombre@ejemplo.com" runat="server"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtDate" class="form-label">Fecha</label>
                    <asp:TextBox ID="txtDate" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtPassword" class="form-label">Contraseña</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" CssClass="form-control" textMode="SingleLine" type="password" runat="server"></asp:TextBox>
                        <asp:Button ID="btnShowPassword" CssClass="btn btn-outline-primary" runat="server" Text="Ver" OnClick="btnShowPassword_Click" />
                    </div>
                </div>

                <hr class="my-4">
              
                <asp:Button ID="btnUpdateUser" CssClass="w-100 btn btn-primary btn-lg" runat="server" Text="Actualizar" OnClick="btnUpdateUser_Click" />
                <asp:Button ID="btnDeleteUser" CssClass="w-100 btn btn-danger btn-lg" runat="server" Text="Eliminar" OnClick="btnDeleteUser_Click" />
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
      <br />
    </div>
  </div>

</asp:Content>
