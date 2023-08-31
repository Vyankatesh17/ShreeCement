﻿<%@ Page Title="Employee Weekly Off." Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Import_EmployeeWise_WeeklyOff.aspx.cs" Inherits="Import_EmployeeWise_WeeklyOff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnImport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Import Employee Wise Weekly Off List</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2"></label>
                                    <label class="control-label col-sm-2">Select File</label>
                                    <div class="col-sm-5">
                                        <div class="input-group">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                            <div class="input-group-btn">
                                                <asp:Button ID="btnImport" runat="server" CssClass="btn btn-primary" Text="Import" OnClick="btnImport_Click" />
                                                <asp:HiddenField ID="hfTenant" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2"><a href="Attachments/Weekly_Off_import_sample.xlsx" class="btn btn-dropbox"><i class="fa fa-download"></i> Download Sample File</a></div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer no-padding">
                            <table class="table table-bordered table-striped text-red">
                                <asp:Literal ID="litErrors" runat="server"></asp:Literal>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

