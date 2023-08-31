<%@ Page Title="Generate Login Code" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="generate_login_code_for_app.aspx.cs" Inherits="generate_login_code_for_app" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {
            var table = $('#ContentPlaceHolder1_gvPendingLeaves').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvPendingLeaves_wrapper .col-sm-6:eq(0)');
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
                    <h3 class="box-title">Generate Login Code</h3>
                </div>
                <div class="box-body no-padding">
                    <div class="table-responsive">
                        <asp:gridview id="gvPendingLeaves" runat="server" autogeneratecolumns="false" cssclass="table table-bordered table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee" />
                                                <asp:BoundField DataField="EmployeeNo" HeaderText="Code" />
                                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Mobile No" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                                <asp:BoundField DataField="DeptName" HeaderText="Department" />
                                                <asp:BoundField DataField="DesigName" HeaderText="Designation" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="lbtnApprove" runat="server" CommandArgument='<%#Eval("Email") %>' OnClick="lbtnApprove_Click" CssClass="btn btn-info btn-sm"> Generate & Send <i class="fa fa-forward"></i></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:gridview>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

