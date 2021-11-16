<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MedicalRecord.aspx.cs" Inherits="WebPatient.MedicalRecord" %>
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
            <h4 class="mb-3">Historia Medica</h4>

            <asp:Panel ID="viewError" class="alert alert-danger alert-dismissible mb-3" role="alert" runat="server" Visible="false">
                <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <div>
              <div class="row g-3">

                <div class="col-sm-12">
                    <label for="txtReason" class="form-label">Razon</label>
                    <asp:TextBox ID="txtReason" CssClass="form-control" textMode="MultiLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-12">
                    <label for="txtPrescription" class="form-label">Prescripcion</label>
                    <asp:TextBox ID="txtPrescription" CssClass="form-control" textMode="MultiLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-3">
                    <label for="txtDiseases" class="form-label">Enfermedades</label>
                    <asp:TextBox ID="txtDiseases" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-3">
                    <label for="txtAllergies" class="form-label">Alergias</label>
                    <asp:TextBox ID="txtAllergies" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-3">
                    <label for="txtMedicines" class="form-label">Medicinas</label>
                    <asp:TextBox ID="txtMedicines" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-sm-3">
                    <label for="txtSurgeries" class="form-label">Cirugias</label>
                    <asp:TextBox ID="txtSurgeries" CssClass="form-control" textMode="SingleLine" runat="server" Enabled="false"></asp:TextBox>
                </div>
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
