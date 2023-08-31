<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="rpt_attendance_report.aspx.cs" Inherits="rpt_attendance_report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="row">
        <div class="col-lg-12">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">Attendance Summary Report</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-2 form-group">
                            <label class="control-label">Month</label>
                            <asp:DropDownList ID="ddmonth" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                            <asp:RequiredFieldValidator InitialValue="0" ControlToValidate="ddmonth" ErrorMessage="this field is required" ID="RequiredFieldValidator15" runat="server" Display="Dynamic" ValidationGroup="S" CssClass="text-red"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ControlToValidate="ddmonth" ErrorMessage="this field is required" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ValidationGroup="S" CssClass="text-red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-lg-1 form-group">
                            <label class="control-label">Year</label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0" ControlToValidate="ddlYear" ErrorMessage="this field is required" ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ValidationGroup="S" CssClass="text-red"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ControlToValidate="ddlYear" ErrorMessage="this field is required" ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="S" CssClass="text-red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-lg-2 form-group"  Visible="false" id="company" runat="server">
                            <label class="control-label">Company</label>
                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 form-group"  Visible="false" id="department" runat="server">
                            <label class="control-label">Department</label>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 form-group"  Visible="false" id="employee" runat="server">
                            <label class="control-label">Employee</label>
                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 form-group">
                            <label class="control-label">Employee Code</label>
                            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-1 form-group">
                            <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" ValidationGroup="a" />
                            <asp:Label ID="lblcompId" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <div class="row">
                        <div class="col-lg-12">
                            <rsweb:ReportViewer ID="ReportViewer1" Width="100%" runat="server"></rsweb:ReportViewer>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>








</asp:Content>

