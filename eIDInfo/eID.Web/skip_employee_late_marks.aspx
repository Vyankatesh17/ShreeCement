<%@ Page Title=" Skip Latemark" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="skip_employee_late_marks.aspx.cs" Inherits="skip_employee_late_marks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

     <link href="admin_theme/dist/css/bootstrap_multiselect.css" rel="stylesheet" />
    <script src="admin_theme/dist/js/bootstrap_multiselect.js"></script>

    <script type="text/javascript">
        function jqFunctions() {

            $('#ContentPlaceHolder1_lstFruits').multiselect({

                includeSelectAllOption: true,
                enableFiltering: true,
                maxHeight: 850,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
                dropRight: true,
                onDropdownShow: function (event) {
                    $(this).closest('select').css('width', '800px')
                }
            });




            var table = $('#ContentPlaceHolder1_gvDataList').DataTable({
                order: [],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }],
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvDataList_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row form-horizontal">
                <div class="col-md-6">
                    <div class="box">
                        <div class="box-header box-solid">
                            <h3 class="box-title">Create</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <label class="control-label col-sm-4">Company <span class="text-red">*</span></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ID="RequiredFieldValidator4" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                             <div class="form-group">
                                        <label class="control-label col-sm-4">Department <span class="text-red">*</span></label>
                                 <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" ID="RequiredFieldValidator2" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" InitialValue="0" ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                             <div class="form-group">
                                                <label class="control-label col-sm-4">Employee </label>
                                                <div class="col-sm-6">
                                                <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                               <%-- <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control">
                                                </asp:DropDownList>--%>
                                                <asp:RequiredFieldValidator ControlToValidate="lstFruits" ID="RequiredFieldValidator1" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <%--<asp:RequiredFieldValidator ControlToValidate="lstFruits" InitialValue="0" ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                                            </div>
                                                </div>
                           <%-- <div class="form-group">
                                <label class="control-label col-sm-4">Employee</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" DataTextField="EmpName" DataValueField="EmployeeId"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ErrorMessage="Please select employee" ValidationGroup="A" InitialValue="0" ControlToValidate="ddlEmployee" runat="server" ID="req1" Display="Dynamic" CssClass="text-red" />
                                    <asp:RequiredFieldValidator ErrorMessage="Please select employee" ValidationGroup="A" ControlToValidate="ddlEmployee" runat="server" ID="RequiredFieldValidator1" Display="Dynamic" CssClass="text-red" />
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <label class="control-label col-sm-4">Skip Latemark</label>
                                <div class="col-sm-6">
                                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="1">In Active</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label class="control-label col-sm-4">Single Punch Present</label>
                                <div class="col-sm-6">
                                    <asp:RadioButtonList ID="rblsinglepunch" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="1">In Active</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn bg-blue-active" ValidationGroup="A" OnClick="btnSave_Click" />
                            <asp:HiddenField ID="hfId" runat="server" />

                        </div>
                    </div>
                    <div class="form-group">
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">List</h3>
                        </div>
                        <div class="box-body">
                            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvList_PageIndexChanging" CssClass="table table-bordered table-striped table-responsive">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Employee Name" DataField="EmpName" />
                                    <asp:BoundField HeaderText="Employee Code" DataField="EmployeeNo" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <label>
                                                <%#Eval("Status") %>
                                            </label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Single Punch">
                                        <ItemTemplate>
                                            <label>
                                                <%#Eval("SinglePunch") %>
                                            </label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#Eval("Skip_Id") %>' OnClick="lbtnEdit_Click">
                                                <i class="fa fa-edit"></i> Edit
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

