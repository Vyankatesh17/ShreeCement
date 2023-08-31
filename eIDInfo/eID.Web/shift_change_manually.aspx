<%@ Page Title="Change Shift" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="shift_change_manually.aspx.cs" Inherits="shift_change_manually" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCalculate" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Change Shift</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel runat="server" ID="Panel1" DefaultButton="btnCalculate">
                                <div class="row">
                                    <div class="form-group col-md-3">
                                        <label class="control-label">Company </label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ErrorMessage="This field is required" ID="RequiredFieldValidator3" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator4" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">Department</label>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label">Employee</label>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtFromDate" ErrorMessage="This field is required" ID="RequiredFieldValidator1" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                                    <div class="col-md-1 ">
                                          <label class="control-label">&nbsp;&nbsp;</label>
                                        <asp:Button ID="btnCalculate" runat="server" CssClass="btn btn-primary" Text="Show Current Shift" ValidationGroup="A" OnClick="btnCalculate_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-2">
                                        <label class="control-label">Current Shift</label>
                                        <asp:Label ID="lblCurrentShift" CssClass="form-control" runat="server"></asp:Label>
                                        <asp:Label ID="lblDate" CssClass="form-control" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">New Shift</label>
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlShift" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator2" ValidationGroup="B" Display="Dynamic" CssClass="text-red" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="ddlShift" ErrorMessage="This field is required" ID="RequiredFieldValidator5" ValidationGroup="B" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                        <asp:Button ID="btnAssignShift" runat="server" CssClass="btn btn-primary" Text="Assign New Shift" Visible="false" ValidationGroup="B" OnClick="btnAssignShift_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

