<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="ShowAppointments.aspx.cs" Inherits="WebClinicManagementLiteAdmin.ShowAppointments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="page-wrapper">
        <div class="row">
            <div class="col-sm-1 col-md-1 col-lg-0"></div>
            <div class="col-sm-10 col-md-10 col-lg-10">
                <h1>Lista de Citas</h1>
            </div>
        </div>
        <div class="row" style="margin-top: 32px;">
            <div class="col-sm-1 col-md-1 col-lg-0"></div>
            <div class="col-sm-10 col-md-10 col-lg-10">
                <div class="panel panel-default">
                    <div class="panel-body table-responsive">
                        <asp:GridView ID="gdvAppointments" width="100%" CssClass="table table-striped table-bordered table-hover" runat="server">
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-sm-1 col-md-1 col-lg-2"></div>
        </div>
    </div>

</asp:Content>
