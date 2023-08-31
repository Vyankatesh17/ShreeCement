<%@ Page Title="Employee Presentee Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="attendance_present_report.aspx.cs" Inherits="attendance_present_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {
            var table = $('#ContentPlaceHolder1_gvAttendance').DataTable({
                lengthChange: false,
                ordering: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvAttendance_wrapper .col-sm-6:eq(0)');
        }


        $(window).load(function () {
            var table = $('#ContentPlaceHolder1_gvAttendance').DataTable({
                lengthChange: false,
                ordering: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvAttendance_wrapper .col-sm-6:eq(0)');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Employee Presentee Report</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2 form-group">
                            <label class="control-label">Year</label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 form-group">
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
                        </div>
                        <div class="col-md-3 form-group">
                            <label class="control-label">Employee</label>
                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ErrorMessage="Please select employee" ValidationGroup="A" InitialValue="0" ControlToValidate="ddlEmployee" runat="server" ID="req1" Display="Dynamic" CssClass="text-red" />
                        <asp:RequiredFieldValidator ErrorMessage="Please select employee" ValidationGroup="A" ControlToValidate="ddlEmployee" runat="server" ID="RequiredFieldValidator1" Display="Dynamic" CssClass="text-red" />
                        </div>
                        <div class="col-md-1 form-group">
                            <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" ValidationGroup="A" Text="Show" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
                <div class="box-header with-border"></div>
                <div class="box-body no-padding">
                    <div class="table-responsive">
                        <asp:GridView ID="gvAttendance" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="gvAttendance_RowDataBound" AutoGenerateColumns="false" AllowPaging="false">
                            <Columns>
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
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

