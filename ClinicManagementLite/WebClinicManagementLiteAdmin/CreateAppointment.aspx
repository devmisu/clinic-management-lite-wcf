<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="CreateAppointment.aspx.cs" Inherits="WebClinicManagementLiteAdmin.CreateAppointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
      <div>
        <div class="py-5 text-center">
          <h2>Agendar cita a un paciente</h2>
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
                <a href="ShowAppointments.aspx" class="alert-link">Volver al listado de citas</a>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <div>
              <div class="row g-3">
                <div class="col-sm-6">
                    <label for="ddlArea" class="form-label">Especialidad</label>
                    <asp:DropDownList ID="ddlArea" CssClass="form-select" runat="server" AutoPostBack="true" ></asp:DropDownList>
                </div>

                <div class="col-sm-6">
                    <label for="ddlDoctor" class="form-label">Doctor</label>
                    <asp:DropDownList ID="ddlDoctor" CssClass="form-select" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>

                <div class="col-sm-6">
                    <label for="txtDate" class="form-label">Fecha</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtDate" CssClass="form-control" textMode="Date" runat="server"></asp:TextBox>
                        <asp:Button ID="btnShowAvailableSchedules" CssClass="btn btn-outline-primary" runat="server" Text="Ver horarios disponibles" OnClick="btnShowAvailableSchedules_Click"  />
                    </div>
                </div>

                <div class="col-sm-6">
                    <label for="ddlPatient" class="form-label">Paciente</label>
                    <asp:DropDownList ID="ddlPatient" CssClass="form-select" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>

                <div class="col-sm-6">
                    <label for="ddlSchedule" class="form-label">Horarios</label>
                    <asp:DropDownList ID="ddlSchedule" CssClass="form-select" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>

                <div class="col-sm-6"></div>

                <hr class="my-4">
              
              <asp:Button ID="btnScheduleAppointment" CssClass="w-100 btn btn-primary btn-lg" runat="server" Text="Agendar" OnClick="btnScheduleAppointment_Click"  />
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
