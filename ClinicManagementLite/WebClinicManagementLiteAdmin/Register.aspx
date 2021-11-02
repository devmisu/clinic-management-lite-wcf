<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebClinicManagementLiteAdmin.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
      <div>
        <div class="py-5 text-center">
          <img class="d-block mx-auto mb-4" src="assets/clinic_logo.png" alt="" width="100" height="100">
          <h2>Registro</h2>
          <p class="lead">Complete los campos con la informacion necesaria, al termino de este proceso usted podra administrar citas, ver a sus pacientes, entre otros.</p>
        </div>

        <div class="row g-5">
          <div class="col">
            <h4 class="mb-3">Informacion personal</h4>

            <asp:Panel ID="viewError" class="alert alert-danger alert-dismissible mb-3" role="alert" runat="server" Visible="false">
                <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <asp:Panel ID="viewSuccess" class="alert alert-success alert-dismissible mb-3" role="alert" runat="server" Visible="false">
                <asp:Label ID="lblSuccessMessage" runat="server"></asp:Label>
                <a href="Login.aspx" class="alert-link">Volver al inicio</a>
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
                    <asp:TextBox ID="txtDni" CssClass="form-control" textMode="SingleLine" MaxLength="8" runat="server"></asp:TextBox>
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
                    <label for="txtSpecialization" class="form-label">Especialización</label>
                    <asp:TextBox ID="txtSpecialization" CssClass="form-control" textMode="SingleLine" runat="server"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="txtPassword" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" textMode="Password" runat="server"></asp:TextBox>
                    <small class="text-muted">Contraseña</small>
                </div>

                <div class="col-sm-6">
                    <label for="txtPassword2" class="form-label">Confirmar contraseña</label>
                    <asp:TextBox ID="txtPassword2" CssClass="form-control" textMode="Password" runat="server"></asp:TextBox>
                </div>

                <div class="col-sm-6">
                    <label for="dropArea" class="form-label">Area</label>
                    <asp:DropDownList ID="dropArea" class="form-select" runat="server"></asp:DropDownList>
                </div>

                <div class="col-sm-6">
                    <label for="dropRole" class="form-label">Rol</label>
                    <asp:DropDownList ID="dropRole" class="form-select" runat="server"></asp:DropDownList>
                </div>

              <hr class="my-4">
              
              <asp:Button ID="btnRegister" CssClass="w-100 btn btn-primary btn-lg" runat="server" Text="Registrarse" OnClick="btnRegister_Click" />
            </div>
          </div>
        </div>
      </div>

      <footer class="my-5 pt-5 text-muted text-center text-small">
        <p class="mb-1">&copy; 2021 Clinic Management Lite</p>
        <ul class="list-inline">
          <li class="list-inline-item"><a href="Login.aspx">Inicio</a></li>
        </ul>
      </footer>
    </div>
  </div>
</asp:Content>