<%@ Page Title="Import Attendance Excel" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="attendance_import_sheet.aspx.cs" Inherits="attendance_import_sheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                 <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
                <ContentTemplate>
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Import Excel</h3>
                            <div class="box-tools">
                                <a href="Attachments/sample_sheet_for_attendance.xlsx" class="btn btn-dropbox"><i class="fa fa-download"></i>Download Sample File</a>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Company <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Year <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Month <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="0">Select month</asp:ListItem>
                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">File <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3"></label>
                                            <div class="col-sm-7">
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Import Sheet" ValidationGroup="A" OnClick="btnUpload_Click" />
                                                <div class="hidden">
                                                    <asp:Literal ID="litErrorMessage" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

