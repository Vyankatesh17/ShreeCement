<%@ Page Title="Roster Details" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="mst_roster_list.aspx.cs" Inherits="mst_roster_list" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
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

    <script>
        function validateHeader(check) {
            if (check.checked) { console.log('checked'); checkAll(); }
            else {
                console.log('un-checked');
                uncheckAll();
            }
        }
        function validate(selCb) {
            if (selCb.checked) {
                console.log("checked");
            } else {
                var x = $("#ContentPlaceHolder1_gvEmployeeList_checkAll").is(":checked");

                if (x == true) {
                    $("#ContentPlaceHolder1_gvEmployeeList_checkAll").prop('checked', false);
                }
                else
                    console.log("You didn't check it! Let me check it for you.")
            }
        }


        //create checkall function
        function checkAll() {
            $("#ContentPlaceHolder1_gvEmployeeList").find("[type='checkbox']").prop('checked', true);
        }
        //create uncheckall function
        function uncheckAll() {
            $("#ContentPlaceHolder1_gvEmployeeList").find("[type='checkbox']").prop('checked', false);
        }

    </script>








</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGenerate" />
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
           <%-- <asp:Panel ID="Panel1" runat="server" DefaultButton="btnGenerate">--%>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="box">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Assigned Rosters</h3>
                                        <div class="box-tools">
                                            <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-box-tool bg-orange" Text="Add New" OnClick="btnAddNew_Click" />
                                            <a href="mst_roster_import.aspx" class="btn btn-warning btn-sm">Import Roster</a>
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <asp:GridView ID="gvDataList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowDataBound="gvDataList_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RosterId" HeaderText="RosterId" Visible="false" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                <asp:BoundField DataField="DeptName" HeaderText="Department Name" />
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="EmployeeNo" HeaderText="Employee Code" />
                                                <asp:BoundField DataField="FromDate" HeaderText="Shift From" />
                                                <asp:BoundField DataField="ToDate" HeaderText="Shift To" />
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <a href="view_employee_roster.aspx?id=<%#Eval("RosterId") %>" class="btn btn-primary"><i class="fa fa-eye"></i>View</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="box">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Assign Roster</h3>
                                        <div class="box-tools">
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-lg-3 form-group">
                                                <label class="control-label">Company Name</label>
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator1" runat="server"
                                                    ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-3 form-group">
                                                <label class="control-label">Department</label>
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator6" runat="server"
                                                    ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-3 form-group">
                                                <label class="control-label">Shift</label>
                                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlShift" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator7" runat="server"
                                                    ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-3 form-group">
                                                <label class="control-label">Employee </label>
                                                <div class="">
                                                <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                               <%-- <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control">
                                                </asp:DropDownList>--%>
                                                <asp:RequiredFieldValidator ControlToValidate="lstFruits" ID="RequiredFieldValidator2" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <%--<asp:RequiredFieldValidator ControlToValidate="lstFruits" InitialValue="0" ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                                            </div>
                                                </div>
                                            <div class="col-lg-3 form-group">
                                                <label class="control-label">From Date</label>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtFromDate" ID="RequiredFieldValidator4" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-3 form-group">
                                                <label class="control-label">To Date</label>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtToDate" ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-1 form-group">
                                                <label class="control-label">&nbsp;&nbsp;&nbsp;</label>
                                                <asp:Button ID="btnGenerate" runat="server" Text="Generate Roster" CssClass="btn btn-info" ValidationGroup="A" OnClick="btnGenerate_Click" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="box-body">
                                        <asp:DataList ID="datalistdisplay" DataKeyField="DayNo" runat="server" RepeatDirection="Horizontal" OnItemDataBound="datalistdisplay_ItemDataBound" GridLines="Both" RepeatColumns="11">
                                            <ItemTemplate>
                                                <table class="table table-striped">
                                                    <tr style="background-color: gray;">
                                                        <td>
                                                            <asp:Label ID="lbldays" runat="server" Text='<%# Eval("DayNo") %>' CssClass="hidden"/>
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Convert.ToDateTime(Eval("Date")).ToString("dd-MM-yyyy") %>'  />
                                                            <asp:Label ID="lblMonth" runat="server" Text='<%# Convert.ToDateTime(Eval("Date")).ToString("MM") %>' CssClass="hidden" />
                                                            <asp:Label ID="lblYear" runat="server" Text='<%#  Convert.ToDateTime(Eval("Date")).ToString("yyyy") %>' CssClass="hidden" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlist" runat="server" CssClass="form-control">
                                                                <asp:ListItem>First</asp:ListItem>
                                                                <asp:ListItem>Second</asp:ListItem>
                                                                <asp:ListItem>Third</asp:ListItem>
                                                                <asp:ListItem>WO</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary pull-right" OnClick="btnSave_Click" ValidationGroup="A" />
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
           <%-- </asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

