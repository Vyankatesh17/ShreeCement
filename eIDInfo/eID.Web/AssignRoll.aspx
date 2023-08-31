<%@ Page Title="Manage User" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="AssignRoll.aspx.cs" Inherits="AssignRoll" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 372px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="ddemployee" />
            <asp:PostBackTrigger ControlID="btnsubmit" />
        </Triggers>
        <ContentTemplate>
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">Assign Role</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label">Company</label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label">Employee</label>
                                <asp:DropDownList ID="ddemployee" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddemployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Panel ID="UserInformation" runat="server" Visible="false">
                            <div class="col-lg-3">
                                <label class="control-label">User Name</label>
                                <asp:TextBox ID="txtusername" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-3">
                                <label class="control-label">
                                    Password
                                </label>
                                <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password" ReadOnly="True"></asp:TextBox>
                            </div>
                            <div class="col-lg-3">
                                <label class="control-label">
                                    Status :  <asp:Label ID="lblstatus" runat="server" ForeColor="Red" Text="NotActive"></asp:Label>
                                </label>
                            </div>
                            <div class="col-lg-3">
                                <label class="control-label">
                                    User Type : <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="NotActive"></asp:Label>
                                </label>
                            </div>
                          
                        </asp:Panel>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-3"></div>
                        <div class="col-lg-6">
                            <asp:TreeView ID="TreeView1" runat="server" ShowCheckBoxes="All">
                            </asp:TreeView>
                        </div>
                        <div class="col-lg-3"></div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:HiddenField ID="HdSource0" runat="server" Value="" />
                    <asp:Button ID="btncancel" runat="server" OnClick="btncancel_Click" Text="Cancel" CssClass="btn btn-default" />
                    <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit" CssClass="btn btn-primary pull-right" ValidationGroup="p" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

