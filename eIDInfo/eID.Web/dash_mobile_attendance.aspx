<%@ Page Title="Mobile Attendance" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="dash_mobile_attendance.aspx.cs" Inherits="dash_mobile_attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function jqFunctions() {
        //    $('#ContentPlaceHolder1_grd_Emp').DataTable({
        //        "paging": true,
        //        "lengthChange": true,
        //        "searching": true,
        //        "ordering": true,
        //        "info": true,
        //        "autoWidth": true
        //    });

            var table = $('#ContentPlaceHolder1_gvList').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvList_wrapper .col-sm-6:eq(0)');
        }
        
        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="box">
                <div class="box-header box-solid">
                    <h3 class="box-title">View Mobile Attendance</h3>
                </div>
                <div class="box-footer no-padding">
                    <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="gvList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Company" DataField="Company" />
                            <asp:BoundField HeaderText="Employee Name" DataField="EmpName" />
                            <asp:BoundField HeaderText="Emp Code" DataField="MachineID" />
                            <asp:BoundField HeaderText="Device Code" DataField="EmployeeNo" />
                            <asp:BoundField HeaderText="Latitude" DataField="Latitude" />
                            <asp:BoundField HeaderText="Longitude" DataField="Longitude" />
                            <asp:BoundField HeaderText="Location" DataField="Location" />
                            <asp:TemplateField HeaderText="Attendance Date">
                                <ItemTemplate>
                                     <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date", "{0:MM/dd/yyyy}")%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Time" DataField="Time" />
                            <asp:BoundField HeaderText="Punch Status" DataField="PunchType" />

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

