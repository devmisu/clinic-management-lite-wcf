<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="UpdateArea.aspx.cs" Inherits="WebClinicManagementLiteAdmin.UpdateArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div>
            <div class="py-5 text-center">
                <h2>Actualizar area</h2>
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
                        <a href="ShowAreas.aspx" class="alert-link">Volver al listado de areas</a>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </asp:Panel>

                    <div>

                        <div class="row g-3">

                            <div class="col-sm-6">
                                <label for="txtAreaName" class="form-label">Nombre del área</label>
                                <asp:TextBox ID="txtAreaName" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                            </div>

                            <div class="col-sm-6">
                                <label for="txtAreaDescription" class="form-label">Descripción del área</label>
                                <asp:TextBox ID="txtAreaDescription" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                            </div>

                            <hr class="my-4">

                            <asp:Button ID="btnUpdateArea" CssClass="w-100 btn btn-primary btn-lg" runat="server" Text="Actualizar" OnClick="btnUpdateArea_Click"/>
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
