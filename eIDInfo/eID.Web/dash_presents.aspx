<%@ Page Title="presents list" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="dash_presents.aspx.cs" Inherits="dash_presents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Present List</h3>
                </div>
                <div class="box-body no-padding">
                    <div class="table-responsive">
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
                                <asp:BoundField HeaderText="Department" DataField="DeptName" />
                                <asp:BoundField HeaderText="Designation" DataField="DesigName" />
                                <asp:BoundField HeaderText="Device Code" DataField="EmployeeNo" />
                                <asp:BoundField HeaderText="Day" DataField="Day" />
                                <asp:BoundField HeaderText="Date" DataField="Date" />
                                <%--<asp:BoundField HeaderText="Punch In" DataField="PunchIn" />--%>
                                <%--<asp:BoundField HeaderText="Punch Time" DataField="PunchTime" />--%>
                                <asp:BoundField HeaderText="Day of Week" DataField="DayofWeek" />
                                <asp:BoundField HeaderText="Status" DataField="Status" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

