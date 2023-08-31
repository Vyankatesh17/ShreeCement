<%@ Page Title="Monthly Present Report " Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="view_present_report.aspx.cs" Inherits="view_present_report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_gv').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gv_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsearch" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box">
                        <div class="box-header box-solid">
                            <h3 class="box-title">Present Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2 form-group" Visible="false" id="company" runat="server">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group" Visible="false" id="department" runat="server">
                                    <label class="control-label">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group" Visible="false" id="employee" runat="server">
                                    <label class="control-label">Employee Name</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" DataTextField="EmpName" DataValueField="EmployeeId"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" Text="Show" OnClick="btnsearch_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="box-footer no-padding">
                            <asp:GridView ID="gv" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered" OnRowDataBound="gv_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Container.DataItemIndex+1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:BoundField HeaderText="Emp Name" DataField="EmpName" />
                                        <asp:BoundField HeaderText="Emp No" DataField="MachineID" />
                                        <asp:BoundField HeaderText="Device Code" DataField="EmployeeNo" />
                                        <asp:BoundField HeaderText="Day" DataField="Day" />
                                        <asp:BoundField HeaderText="Date" DataField="Date" />
                                        <asp:BoundField HeaderText="Day of Week" DataField="DayofWeek" />
                                        <asp:BoundField HeaderText="Shift" DataField="ShiftName" />
                                        <asp:BoundField HeaderText="Punch In" DataField="PunchIn" />
                                        <asp:BoundField HeaderText="Punch Out" DataField="PunchOut" />
                                        <asp:BoundField HeaderText="Late M" DataField="LateBy" />
                                        <asp:BoundField HeaderText="Actual" DataField="ActualHours" />
                                        <asp:BoundField HeaderText="Standard" DataField="StanardWorkHours" />
                                        <asp:BoundField HeaderText="Minimum" DataField="MinHours" />
                                        <asp:BoundField HeaderText="Status" DataField="Status" />
                                        <asp:BoundField HeaderText="Attendance Details" DataField="Remarks" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Exists.......!!!!
                                </EmptyDataTemplate>
                                <PagerStyle HorizontalAlign="Right" />
                                <EmptyDataRowStyle />
                            </asp:GridView>
                        </div>
                        <div class="box-body">
                            <table class="table table-bordered hidden">
                                <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
