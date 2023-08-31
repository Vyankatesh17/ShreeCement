<%@ Page Title="Import Roster" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="mst_roster_import.aspx.cs" Inherits="mst_roster_import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Import Roster</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-lg-2"></div>
                                    <div class="col-lg-8">
                                        <div class="form-group">
                                            <label class="col-sm-3">Company</label>
                                            <div class="col-sm-7">
                                               <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3">Select File</label>
                                            <div class="col-sm-7">
                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnUpload_Click" />
                                            </div>
                                            <div class="col-sm-2">
                                                <a href="Attachments/sample_roster_import.xlsx" class="btn btn-dropbox"><i class="fa fa-download"></i> Download Sample File</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer no-padding">
                            <table class="table table-bordered">
                            <asp:Literal ID="litErrors" runat="server"></asp:Literal>
                                </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

