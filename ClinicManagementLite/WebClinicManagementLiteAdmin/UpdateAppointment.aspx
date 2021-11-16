﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="UpdateAppointment.aspx.cs" Inherits="WebClinicManagementLiteAdmin.UpdateAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="d-flex flex-column flex-md-row align-items-center pb-3 mb-4 border-bottom">
            <a href="Index.aspx" class="d-flex align-items-center text-dark text-decoration-none">
                <h1>Actualizar información de cita</h1>
            </a>

            <nav class="d-inline-flex mt-2 mt-md-0 ms-md-auto">
                <asp:Button CssClass="btn btn-success" ID="btnFinishAppointment" runat="server" Text="Finalizar Cita" OnClick="btnFinishAppointment_Click"></asp:Button>
                <p style="color: white">__</p>
                <asp:Button CssClass="btn btn-danger" ID="btnDeleteAppointment" runat="server" Text="Eliminar Cita" OnClick="btnDeleteAppointment_Click"></asp:Button>
            </nav>
        </div>
        <div class="row" style="margin-top: 32px;">

            <div class="col">
                <asp:Panel ID="viewError" class="alert alert-danger alert-dismissible mb-3" role="alert" runat="server" Visible="false">
                    <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </asp:Panel>

                <asp:Panel ID="viewSuccess" class="alert alert-success alert-dismissible mb-3" role="alert" runat="server" Visible="false">
                    <asp:Label ID="lblSuccessMessage" runat="server"></asp:Label>
                    <a href="ShowAppointments.aspx" class="alert-link">Volver al listado de citas</a>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </asp:Panel>

                <div id="divContainerInfo" runat="server">
                    <div class="row g-3">

                        <h4 class="mb-3">Doctor</h4>

                        <div class="col-sm-8">
                            <label for="txtDoctor" class="form-label">Nombres y Apellidos</label>
                            <asp:TextBox ID="txtDoctor" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-sm-4">
                            <label for="txtDoctorDni" class="form-label">DNI</label>
                            <asp:TextBox ID="txtDoctorDni" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-sm-6">
                            <label for="txtDoctorPhone" class="form-label">Celular</label>
                            <asp:TextBox ID="txtDoctorPhone" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-sm-6">
                            <label for="txtDoctorEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtDoctorEmail" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <hr class="my-4">

                        <h4 class="mb-3">Cita</h4>

                        <div class="col-sm-6">
                            <label for="txtArea" class="form-label">Especialidad</label>
                            <asp:TextBox ID="txtArea" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-sm-6">
                            <label for="txtState" class="form-label">Estado</label>
                            <asp:TextBox ID="txtState" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-sm-4">
                            <label for="txtDate" class="form-label">Fecha</label>
                            <asp:TextBox ID="txtDate" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-sm-4">
                            <label for="txtStartHour" class="form-label">Hora Inicio</label>
                            <asp:TextBox ID="txtStartHour" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-sm-4">
                            <label for="txtEndHour" class="form-label">Hora Fin</label>
                            <asp:TextBox ID="txtEndHour" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
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
