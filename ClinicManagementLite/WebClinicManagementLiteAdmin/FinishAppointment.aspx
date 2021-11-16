<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="FinishAppointment.aspx.cs" Inherits="WebClinicManagementLiteAdmin.FinishAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="d-flex flex-column flex-md-row align-items-center pb-3 mb-4 border-bottom">
            <a href="Index.aspx" class="d-flex align-items-center text-dark text-decoration-none">
                <h1>Finalizar consulta</h1>
            </a>
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

                        <div class="col-sm-6">
                            <label for="txtRealStartHour" class="form-label">Hora de llegada del paciente</label>
                            <asp:TextBox ID="txtRealStartHour" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="true"></asp:TextBox>
                        </div>

                        <div class="col-sm-6">
                            <label for="txtRealEndHour" class="form-label">Hora de salida del paciente</label>
                            <asp:TextBox ID="txtRealEndHour" CssClass="form-control" TextMode="SingleLine" runat="server" Enabled="true"></asp:TextBox>
                        </div>

                        <hr class="my-4">

                        <h4 class="mb-3">Prescripción</h4>

                        <div class="col-sm-6">
                            <label for="txtReason" class="form-label">Motivo de consulta</label>
                            <asp:TextBox ID="txtReason" CssClass="form-control" TextMode="MultiLine" runat="server" Enabled="true"></asp:TextBox>
                        </div>

                        <div class="col-sm-6">
                            <label for="txtPrescription" class="form-label">Prescripción</label>
                            <asp:TextBox ID="txtPrescription" CssClass="form-control" TextMode="MultiLine" runat="server" Enabled="true"></asp:TextBox>
                        </div>

                        <div class="col-sm-3">
                            <label for="txtDiseases" class="form-label">Alergias</label>
                            <asp:TextBox ID="txtDiseases" CssClass="form-control" TextMode="MultiLine" runat="server" Enabled="true"></asp:TextBox>
                        </div>

                        <div class="col-sm-3">
                            <label for="txtAllergies" class="form-label">Enfermadades</label>
                            <asp:TextBox ID="txtAllergies" CssClass="form-control" TextMode="MultiLine" runat="server" Enabled="true"></asp:TextBox>
                        </div>

                        <div class="col-sm-3">
                            <label for="txtMedicines" class="form-label">Medicinas recetadas</label>
                            <asp:TextBox ID="txtMedicines" CssClass="form-control" TextMode="MultiLine" runat="server" Enabled="true"></asp:TextBox>
                        </div>

                        <div class="col-sm-3">
                            <label for="txtSurgeries" class="form-label">Cirugias</label>
                            <asp:TextBox ID="txtSurgeries" CssClass="form-control" TextMode="MultiLine" runat="server" Enabled="true"></asp:TextBox>
                        </div>

                        <hr class="my-4">

                        <asp:Button ID="btnFinishAppointment" CssClass="w-100 btn btn-primary btn-lg" runat="server" Text="Finalizar consulta" OnClick="btnFinishAppointment_Click" />

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
