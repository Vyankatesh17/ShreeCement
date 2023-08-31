<%@ Page Title="Attendance Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="attendance_report.aspx.cs" Inherits="attendance_report" %>

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
                            <h3 class="box-title">Employee Attendance Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Shift</label>
                                    <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                 <div class="col-md-2 form-group">
                                    <label class="control-label">From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ddlDepartment" />                                     
                                
                                </Triggers>
                                <ContentTemplate>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>  
                                 </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Employee</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-1 form-group">
                                    <label class="control-label">Code</label>
                                    <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>    
                                <div class="col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" ValidationGroup="A" Text="Show" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-body no-padding hidden">
                            <table class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th style="font-weight: bolder">Details</th>
                                        <th>Absent</th>
                                        <th>Half Day</th>
                                        <th>Holiday</th>
                                        <th>Late Marks</th>
                                        <th>Paid Leave</th>
                                        <th>LOP</th>
                                        <th>ODD</th>
                                        <th>Present</th>
                                        <th>Weekly Off</th>
                                        <th>Month Days</th>
                                        <th>Pay Days</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>Nos</th>
                                        <asp:Literal ID="litBody" runat="server"></asp:Literal>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="box-header with-border"></div>
                        <div class="box-body no-padding">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAttendance" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="gvAttendance_RowDataBound" AutoGenerateColumns="false" AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Emp Name" DataField="EmpName" />
                                        <asp:BoundField HeaderText="Emp No" DataField="MachineID" />
                                         <asp:BoundField HeaderText="Company" DataField="CompanyName" />
                                        <asp:BoundField HeaderText="Department" DataField="DeptName" />
                                        <asp:BoundField HeaderText="Designation" DataField="DesigName" />
                                        <asp:BoundField HeaderText="Device Code" DataField="EmployeeNo" />
                                        <asp:BoundField HeaderText="Day" DataField="Day" />
                                        <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>
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

