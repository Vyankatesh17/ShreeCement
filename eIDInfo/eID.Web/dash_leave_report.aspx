<%@ Page Title="Leave Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="dash_leave_report.aspx.cs" Inherits="dash_leave_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {
            var table = $('#ContentPlaceHolder1_gvDataList').DataTable({
                lengthChange: false,
                ordering: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvDataList_wrapper .col-sm-6:eq(0)');
        }

        $(window).load(function () {
            var table = $('#ContentPlaceHolder1_gvDataList').DataTable({
                lengthChange: false,
                ordering: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvDataList_wrapper .col-sm-6:eq(0)');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Leave Report</h3>
                </div>
                <div class="box-body no-padding">
                    <div class="table-responsive">
                        <asp:gridview id="gvDataList" runat="server" cssclass="table table-bordered table-striped" autogeneratecolumns="false" allowpaging="false" onrowdatabound="gvDataList_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeNo" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="LeaveName" HeaderText="Leave Name" />
                                        <asp:BoundField DataField="LeaveStartDate" HeaderText="From" />
                                        <asp:BoundField DataField="LeaveEndDate" HeaderText="To" />
                                        <asp:BoundField DataField="Duration" HeaderText="Duration" />
                                        <asp:BoundField DataField="LeaveReason" HeaderText="Reason" />
                                        <asp:BoundField DataField="ManagerStatus" HeaderText="Manager Status" />
                                        <asp:BoundField DataField="HrStatus" HeaderText="HR Status" />
                                    </Columns>
                                </asp:gridview>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

