<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebClinicManagementLiteAdmin.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-3">
        <header>
            <div class="d-flex flex-column flex-md-row align-items-center pb-3 mb-4 border-bottom">
                <a href="/" class="d-flex align-items-center text-dark text-decoration-none">
                    <img src="assets/clinic_logo.png" alt="" width="72" height="72" class="me-2" viewbox="0 0 118 94" role="img"></img>
                    <span class="fs-4">Clinic Management Lite</span>
                </a>

                <nav class="d-inline-flex mt-2 mt-md-0 ms-md-auto">
                    <asp:Button CssClass="btn btn-primary" ID="btnSignOut" runat="server" Text="Cerrar sesión" OnClick="btnSignOut_Click"></asp:Button>
                </nav>
            </div>

            <div class="pricing-header p-3 pb-md-4 mx-auto text-center">
                <asp:Label ID="tvWelcomeMsg" runat="server" class="display-4 fw-normal">Bienvenido a su página de Inicio</asp:Label>
                <br />
                <br />
                <p class="fs-5 text-muted">Aquí podrá visualizar y administrar a sus pacientes, citas en espera de aprobación y demás.</p>
            </div>
        </header>

        <main>
            <div class="row row-cols-1 row-cols-md-3 mb-3 text-center">
                <div class="col">
                    <div class="card mb-4 rounded-3 shadow-sm">
                        <div class="card-header py-3">
                            <h4 class="my-0 fw-normal">Mis pacientes</h4>
                        </div>
                        <div class="card-body">
                            <h2 class="card-title pricing-card-title"><small class="text-muted fw-light">Podrá visualizar a sus pacientes ya atendidos con anterioridad.</small></h2>
                            <br />
                            <button type="btnPatient" class="w-100 btn btn-lg btn-outline-primary">Visualizar</button>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card mb-4 rounded-3 shadow-sm border-primary">
                        <div class="card-header py-3 text-white bg-primary border-primary">
                            <h4 class="my-0 fw-normal">Mis citas</h4>
                        </div>
                        <div class="card-body">
                            <h2 class="card-title pricing-card-title"><small class="text-muted fw-light">Podrá visualizar las citas pasadas.</small></h2>
                            <br />
                            <asp:Button ID="btnAppointments" runat="server" type="button" class="w-100 btn btn-lg btn-primary" Text="Visualizar" OnClick="btnAppointments_Click"></asp:Button>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card mb-4 rounded-3 shadow-sm">
                        <div class="card-header py-3">
                            <h4 class="my-0 fw-normal">Mis horarios</h4>
                        </div>
                        <div class="card-body">
                            <h2 class="card-title pricing-card-title"><small class="text-muted fw-light">Podrá administrar sus horarios de atención.</small></h2>
                            <br />
                            <button type="btnSchedule" class="w-100 btn btn-lg btn-outline-primary">Administrar</button>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card mb-4 rounded-3 shadow-sm">
                        <div class="card-header py-3">
                            <h4 class="my-0 fw-normal">Administrar doctores</h4>
                        </div>
                        <div class="card-body">
                            <h2 class="card-title pricing-card-title"><small class="text-muted fw-light">Módulo de administración de doctores registrados.</small></h2>
                            <br />
                            <button type="btnDoctors" class="w-100 btn btn-lg btn-outline-primary">Visualizar</button>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card mb-4 rounded-3 shadow-sm">
                        <div class="card-header py-3">
                            <h4 class="my-0 fw-normal">Administrar roles</h4>
                        </div>
                        <div class="card-body">
                            <h2 class="card-title pricing-card-title"><small class="text-muted fw-light">Módulo de administración de roles.</small></h2>
                            <br />
                            <asp:Button ID="btnRoles" runat="server" type="button" class="w-100 btn btn-lg btn-outline-primary" Text="Visualizar" OnClick="btnRoles_Click"></asp:Button>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card mb-4 rounded-3 shadow-sm">
                        <div class="card-header py-3">
                            <h4 class="my-0 fw-normal">Administrar areas</h4>
                        </div>
                        <div class="card-body">
                            <h2 class="card-title pricing-card-title"><small class="text-muted fw-light">Módulo de administración de areas de especialización.</small></h2>
                            <br />
                            <button type="btnAreas" class="w-100 btn btn-lg btn-outline-primary">Administrar</button>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </div>
</asp:Content>
