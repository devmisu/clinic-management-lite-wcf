<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="CreateRol.aspx.cs" Inherits="WebClinicManagementLiteAdmin.CreateRol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div>
            <div class="py-5 text-center">
                <h2>Creación de roles</h2>
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
                        <a href="ShowRoles.aspx" class="alert-link">Volver al listado de roles</a>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </asp:Panel>

                    <div>

                        <div class="row g-3">

                            <div class="col-sm-6">
                                <label for="txtRolName" class="form-label">Nombre del rol</label>
                                <asp:TextBox ID="txtRolName" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                            </div>

                            <div class="col-sm-6"></div>

                            <div class="col-sm-12">
                                <label for="rBtnAttributes" class="form-label">Especialidad</label>
                            </div>

                            <div class="col-sm-12">
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxSchedule" runat="server" class="form-check-input" type="checkbox" value="schedule"/>
                                    <label class="form-check-label" for="cbxSchedule">Horarios</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxAppointment" runat="server" class="form-check-input" type="checkbox" value="appointment"/>
                                    <label class="form-check-label" for="cbxAppointment">Citas</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxQueue" runat="server" class="form-check-input" type="checkbox" value="queue"/>
                                    <label class="form-check-label" for="cbxQueue">Solicitudes</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxDoctors" runat="server" class="form-check-input" type="checkbox" value="doctors"/>
                                    <label class="form-check-label" for="cbxDoctors">Doctores</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxRol" runat="server" class="form-check-input" type="checkbox" value="rol"/>
                                    <label class="form-check-label" for="cbxRol">Roles</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxPatient" runat="server" class="form-check-input" type="checkbox" value="patient"/>
                                    <label class="form-check-label" for="cbxPatient">Pacientes</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxArea" runat="server" class="form-check-input" type="checkbox" value="area"/>
                                    <label class="form-check-label" for="cbxArea">Area</label>
                                </div>
                            </div>

                            <hr class="my-4">

                            <asp:Button ID="btnCreateRol" CssClass="w-100 btn btn-primary btn-lg" runat="server" Text="Crear" OnClick="btnCreateRol_Click"/>
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
