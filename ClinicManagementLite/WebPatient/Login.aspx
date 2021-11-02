<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebPatient.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="www/css/Login.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-signin text-center">
        <asp:Panel ID="viewError" class="alert alert-danger alert-dismissible mb-4" role="alert" runat="server" Visible="false">
            <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </asp:Panel>

        <img class="mb-4" src="www/img/clinic_logo.png" alt="" width="100" height="100">
        <h1 class="h3 mb-3 fw-normal">Pacientes</h1>

        <div class="form-floating">
            <asp:TextBox ID="txtDni" TextMode="SingleLine" placeholder="DNI" CssClass="form-control" runat="server" MaxLength="8"></asp:TextBox>
            <label for="txtDni">DNI</label>
        </div>
        <div class="form-floating">
            <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Contraseña" CssClass="form-control" runat="server"></asp:TextBox>
            <label for="txtPassword">Contraseña</label>
        </div>
        
        <asp:Button ID="btnLogin" CssClass="w-100 btn btn-lg btn-primary" runat="server" Text="Ingresar" OnClick="btnLogin_Click" />
        <p class="mt-5 mb-3 text-muted">&copy; 2021</p>
        <p>No tienes una cuenta? <a href="Register.aspx"> Registrate aqui</a></p>
    </div>
</asp:Content>
