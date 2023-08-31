<%@ Page Title="Monthly Summary Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="rpt_attend_summary.aspx.cs" Inherits="rpt_attend_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .table {
            margin-bottom: 0 !important;
        }

        table.timecard {
            margin: auto;
            border-collapse: collapse;
            border: 1px solid #fff; /*for older IE*/
        }

            table.timecard caption {
                background-color: #c7c3c1;
                color: #fff;
                font-size: x-large;
                font-weight: bold;
                letter-spacing: .3em;
            }

            table.timecard thead th {
                padding: 8px;
                background-color: #c7c3c1;
                font-size: large;
            }

                table.timecard thead th#thDay {
                    width: 40%;
                }

                table.timecard thead th#thRegular, table.timecard thead th#thOvertime, table.timecard thead th#thTotal {
                    width: 20%;
                }

            table.timecard th, table.timecard td {
                padding: 3px;
                border-width: 1px;
                border-style: solid;
                border-color: #f4f4f4 #ccc;
            }

            table.timecard td {
                text-align: center;
            }

            table.timecard tbody th {
                text-align: center;
                font-weight: normal;
            }

            table.timecard tfoot {
                font-weight: bold;
                font-size: large;
                background-color: #687886;
                color: #fff;
            }

            table.timecard tr.even {
                background-color: #fde9d9;
            }

            table.timecard td {
                width: 40px !important;
            }

                table.timecard td:nth-child(1) {
                    width: 62px !important;
                }
    </style>

   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Employee Attendance Summary</h3>

                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2 form-group" Visible="false" id="company" runat="server">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ErrorMessage="This field is required" ID="RequiredFieldValidator2" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                </div>
                                <div class="col-md-2 form-group" Visible="false" id="department" runat="server">
                                    <label class="control-label">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group" Visible="false" id="employee" runat="server">
                                    <label class="control-label">Employee</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-1 form-group">
                                    <label class="control-label">Code</label>
                                    <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Month</label>
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
                                    <asp:RequiredFieldValidator ControlToValidate="ddlMonth" ErrorMessage="This field is required" ID="RequiredFieldValidator1" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Year</label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlYear" ErrorMessage="This field is required" ID="RequiredFieldValidator5" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />

                                </div>
                                <div class="col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" ValidationGroup="A" Text="Show" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="table-responsive">
                                <asp:Repeater ID="rptrTables" runat="server" OnItemDataBound="rptrTables_ItemDataBound">
                                    <HeaderTemplate>
                                        <div class="col-md-12">
                                            <div class="card text-center card-info">
                                                <div class="card-header">
                                                    <asp:Label ID="lblHeader" runat="server" CssClass="h3"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-12">
                                            <div class="card card-info">
                                                <div class="card-header with-border">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="9" style="width: 25%">Employee Name : <b>
                                                                    <asp:Label ID="lblEmpIdId" CssClass="hidden" runat="server" Text='<%# Eval("EmployeeId") %>' />
                                                                    <%# Eval("EmpName") %></b></td>
                                                                <td colspan="2" style="width: 15%">Employee No : <b><%# Eval("MachineID") %></b></td>
                                                                <td colspan="2" style="width: 15%">Device Code : <b><%# Eval("EmployeeNo") %></b></td>
                                                                <td colspan="5" style="width: 20%">Department : <b><%# Eval("DeptName") %></b></td>
                                                                <td colspan="7" style="width: 25%">Designation : <b><%# Eval("DesigName") %></b></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="card-body no-padding">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="true" AllowPaging="true"
                                                                    CssClass="timecard" GridLines="Both"
                                                                    PageSize="10" Width="100%" OnRowDataBound="grdOrder_RowDataBound">
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:Literal ID="litFooter" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-header with-border">
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <div class="box-footer">
                            <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="ExportToExcel" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

