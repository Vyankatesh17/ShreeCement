<%@ Page Title="Leave Summary" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Leave_Summary.aspx.cs" Inherits="Leave_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grdOrder').DataTable({
                order: [],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }],
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grdOrder_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />            
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Simple Monthly Attendance Report</h3>
                            <div class="box-tools">                                        
                                        <asp:Button ID="btnImport" runat="server" CssClass="btn btn-box-tool btn-github" Text="Import Leave Balance" OnClick="btnImport_Click" />
                                    </div>
                        </div>
                        <div class="box-body">
                            <div class="row">                               
                                <div class="col-md-2 form-group" id="company" runat="server">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                </div>                                
                                <div class="col-md-2 form-group"  id="department" runat="server">
                                    <label class="control-label">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group"  id="employee" runat="server">
                                    <label class="control-label">Employee Name</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>                                
                                <div class="col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Show" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="box-body no-padding">
                            <div class="table-responsive">
                                <div class="card text-center card-info">
                                    <div class="card-header">
                                        <asp:Label ID="lblHeader" runat="server" CssClass="h3"></asp:Label>
                                    </div>
                                </div>

                                <div class="card-body no-padding">
                                    <div class="row">
                                        <div class="col-md-1"></div>
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="true" AllowPaging="false" RowStyle-Wrap="false"
                                                    CssClass="timecard" GridLines="Both" 
                                                     Width="100%" OnRowDataBound="grdOrder_RowDataBound">
                                                </asp:GridView>
                                            </div>
                                            <div class="form-group">
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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

