<%@ Page Title="Process Salary" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="salary_process_ngp.aspx.cs" Inherits="salary_process_ngp" %>

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
                            <h3 class="box-title">Process Salary</h3>
                            <div class="box-tools">
                                <a href="salary_process_list_ngp.aspx" class="btn btn-warning btn-sm">Back to List</a>
                            </div>
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
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" ErrorMessage="This field is required" ID="RequiredFieldValidator1" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator2" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />

                                    </div>
                                    <%--<div class="form-group col-md-2">
                                        <label class="control-label">Week</label>
                                        <asp:TextBox ID="txtWeek" runat="server" CssClass="form-control" TextMode="Week"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtWeek" ErrorMessage="This field is required" ID="RequiredFieldValidator5" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>--%>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtFromDate" ErrorMessage="This field is required" ID="RequiredFieldValidator5" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div><div class="form-group col-md-2">
                                        <label class="control-label">To Date</label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtToDate" ErrorMessage="This field is required" ID="RequiredFieldValidator6" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                                    <div class="form-group col-md-2" style="padding-top:25px;">
                                         <label class="control-label"></label>
                                        <asp:Button ID="btnCalculate" runat="server" CssClass="btn btn-primary" Text="Calculate Salary" ValidationGroup="A" OnClick="btnCalculate_Click" />
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

