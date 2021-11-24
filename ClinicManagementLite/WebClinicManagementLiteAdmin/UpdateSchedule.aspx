<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="UpdateSchedule.aspx.cs" Inherits="WebClinicManagementLiteAdmin.UpdateSchedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div>
            <div class="py-5 text-center">
                <h2>Actualizar horario</h2>
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
                        <a href="ShowSchedules.aspx" class="alert-link">Volver al listado de horarios</a>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </asp:Panel>

                    <div>
                        <div class="row g-3">
                            <div class="col-sm-6">
                                <label for="ddlStartTime" class="form-label">Hora de inicio</label>
                                <asp:DropDownList ID="ddlStartTime" CssClass="form-select" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>

                            <div class="col-sm-6">
                                <label for="ddlEndTime" class="form-label">Hora de salida</label>
                                <asp:DropDownList ID="ddlEndTime" CssClass="form-select" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>

                            <div class="col-sm-12">
                                <label class="form-label">Días de la semana</label>
                            </div>

                            <div class="col-sm-12">
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxMon" runat="server" class="form-check-input" type="checkbox" value="MON" />
                                    <label class="form-check-label" for="cbxMon">Lunes</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxTue" runat="server" class="form-check-input" type="checkbox" value="TUE" />
                                    <label class="form-check-label" for="cbxTue">Martes</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxWed" runat="server" class="form-check-input" type="checkbox" value="WED" />
                                    <label class="form-check-label" for="cbxWed">Miercoles</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxThu" runat="server" class="form-check-input" type="checkbox" value="THU" />
                                    <label class="form-check-label" for="cbxThu">Jueves</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxFri" runat="server" class="form-check-input" type="checkbox" value="FRI" />
                                    <label class="form-check-label" for="cbxFri">Viernes</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxSat" runat="server" class="form-check-input" type="checkbox" value="SAT" />
                                    <label class="form-check-label" for="cbxSat">Sabado</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:CheckBox ID="cbxSun" runat="server" class="form-check-input" type="checkbox" value="SUN" />
                                    <label class="form-check-label" for="cbxSun">Domingo</label>
                                </div>
                            </div>

                            <hr class="my-4">

                            <asp:Button ID="btnCreateSchedule" CssClass="w-100 btn btn-primary btn-lg" runat="server" Text="Crear" OnClick="btnCreateSchedule_Click" />
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
