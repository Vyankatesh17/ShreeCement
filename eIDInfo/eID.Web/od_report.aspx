<%@ Page Title="OD Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="od_report.aspx.cs" Inherits="od_report" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">OD Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3 form-group" Visible="false" id="company" runat="server">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <label class="control-label">Employee Code</label>
                                    <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-3" Visible="false" id="employee" runat="server">
                                    <label class="control-label">Employee Name</label>
                                    <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <div class="">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-body no-padding">
                            <div class="table-responsive">
                                <asp:GridView ID="gvDataList" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvDataList_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeNo" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="TravelDate" HeaderText="Date" />
                                        <asp:BoundField DataField="FromTime" HeaderText="From" />
                                        <asp:BoundField DataField="ToTime" HeaderText="To" />
                                        <asp:BoundField DataField="TravelPlace" HeaderText="Travel Place" />
                                        <asp:BoundField DataField="TravelReason" HeaderText="Reason" />
                                        <asp:BoundField DataField="ManagerStatus" HeaderText="Manager Status" />
                                        <asp:BoundField DataField="HrStatus" HeaderText="HR Status" />
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

