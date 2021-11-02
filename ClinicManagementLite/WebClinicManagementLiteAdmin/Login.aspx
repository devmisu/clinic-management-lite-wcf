<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebClinicManagementLiteAdmin.Login.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
      .bd-placeholder-img {
        font-size: 1.125rem;
        text-anchor: middle;
        -webkit-user-select: none;
        -moz-user-select: none;
        user-select: none;
      }

      @media (min-width: 768px) {
        .bd-placeholder-img-lg {
          font-size: 3.5rem;
        }
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
    <main class="form-signin text-center">
  <form>
    <img class="mb-4" src="assets/clinic_logo.png" alt="" width="72" height="72">
    <h1 class="h3 mb-3 fw-normal">Colaborador</h1>

    <div class="form-floating">
      <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" type="text" class="form-control"  placeholder="Dni del colaborador"/>
      <label for="floatingInput">Identificación</label>
    </div>
    <div class="form-floating">
      <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" type="password" class="form-control" placeholder="Password"/>
      <label for="floatingPassword">Password</label>
    </div>

    <asp:Button ID="btnSignIn" runat="server" CssClass="btn w-100 btn btn-lg btn-primary" type="submit" Text="Iniciar sesión" OnClick="btnSignIn_Click"/>
    <asp:Label ID="errorMessage" runat="server" class="mt-5 mb-3 text-muted" Visible="false">&copy; Mensaje de error</asp:Label>
      <p style="margin-top:16px">No tienes una cuenta? <a href="Register.aspx"> Registrate aqui</a></p>
  </form>
</main>
</asp:Content>
