<%@ Page Title="Device Punches" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="report_api_monitor.aspx.cs" Inherits="report_api_monitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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


            var table = $('#ContentPlaceHolder1_gvAttendance').DataTable({
                lengthChange: false,
                ordering: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvAttendance_wrapper .col-sm-6:eq(0)');
        }

        //        $(document).ready(function () {

        //            jqFunctions();
        //        });

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
                            <h3 class="box-title">Api Minotering Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                        <asp:BoundField HeaderText="API" DataField="API" />
                                        <asp:BoundField HeaderText="Api Action" DataField="ApiAction" />
                                        <asp:BoundField HeaderText="Date" DataField="AuditDate" />
                                        <asp:BoundField HeaderText="Execute On" DataField="ExecutePage" />
                                        <asp:BoundField HeaderText="Status" DataField="Status" />
                                        <asp:BoundField HeaderText="Execute By" DataField="ExecuteBy" />
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