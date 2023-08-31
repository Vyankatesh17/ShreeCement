<%@ Page Title="Leave Allocation" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="leave_allocation.aspx.cs" Inherits="leave_allocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_Emp').DataTable({
                order: [],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }],
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Emp_wrapper .col-sm-6:eq(0)');
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
                var x = $("#ContentPlaceHolder1_grd_Emp_checkAll").is(":checked");

                if (x == true) {
                    $("#ContentPlaceHolder1_grd_Emp_checkAll").prop('checked', false);
                }
                else
                    console.log("You didn't check it! Let me check it for you.")
            }
        }


        //create checkall function
        function checkAll() {
            $("#ContentPlaceHolder1_grd_Emp").find("[type='checkbox']").prop('checked', true);
        }
        //create uncheckall function
        function uncheckAll() {
            $("#ContentPlaceHolder1_grd_Emp").find("[type='checkbox']").prop('checked', false);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsubmit" />
            <asp:PostBackTrigger ControlID="BtnAdd1" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <div class="col-lg-12">
                            <div class="box">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Leave Allocation List</h3>
                                    <div class="box-tools"></div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-3 form-group">
                                            <label class="control-label">Select Company</label>
                                            <asp:DropDownList runat="server" ID="ddlCompanyList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyList_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                                ControlToValidate="ddlCompanyList" Display="Dynamic"
                                                ErrorMessage="this field is required"
                                                ValidationGroup="S" CssClass="text-red" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3 form-group">
                                            <label class="control-label">Department Name</label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlDepartment" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 form-group">
                                            <label class="control-label">Employee Name</label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlEmployeeList"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 form-group">
                                            <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                            <div class="btn-group">
                                                <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-success" ValidationGroup="S" Text="Search" OnClick="btnsearch_Click" />
                                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning" Text="Reset" OnClick="btnReset_Click" />
                                            </div>
                                        </div>
                                        
                                            </div>
                                        <div class="row">
                                        <div class="form-group col-md-3">
                                            <label class="control-label">Leave Type <span class="text-red">*</span></label>                                            
                                                <asp:DropDownList ID="ddlmasterleavetype" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                </asp:DropDownList>                                               
                                            
                                        </div>
                                        <div class="col-md-2 form-group">
                                            <label class="control-label">&nbsp;&nbsp;</label>
                                            <div class="">
                                                <asp:Button ID="btnImport" runat="server" CssClass="btn btn-primary" ValidationGroup="A" Text="All Leave Allocation" OnClick="btnImport_Click" />
                                                <asp:HiddenField ID="hfTenant" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-body no-padding">
                                    <asp:GridView ID="grd_Emp" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        AllowPaging="false" OnRowDataBound="grd_Emp_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select All">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" CssClass="case" runat="server" onclick="validate(this);" />
                                                     <asp:Label ID="lblEmployeeId" runat="server" Text='<%#Eval("EmployeeId") %>' CssClass="hidden"></asp:Label>                                           
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="checkAll" runat="server" Text="&nbsp;All" onclick="validateHeader(this);" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtdetails" ReadOnly="true" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempid" ReadOnly="true" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemailid" ReadOnly="true" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldept" ReadOnly="true" runat="server" Text='<%# Eval("DeptName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcntct" ReadOnly="true" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Of Joining">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDOJ" ReadOnly="true" runat="server" Text='<%# Eval("DOJ1","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Of Confirmation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConfirmDate" ReadOnly="true" runat="server" Text='<%# Eval("ConfirmDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AllocationStatus" HeaderText="Allocation Status" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="btn-group">
                                                        <asp:LinkButton ID="lbtnAllocate" runat="server" OnClick="lbtnAllocate_Click" CssClass="btn btn-facebook btn-sm" CommandArgument='<%# Eval("Employeeid") %>'>
                                                <i class="fa fa-arrow-right"></i> Allocate
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnRenew" runat="server" OnClick="lbtnRenew_Click" CssClass="btn btn-github btn-sm" CommandArgument='<%# Eval("Employeeid") %>'>
                                                <i class="fa fa-recycle"></i> Renew
                                                        </asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8">
                            <div class="box">
                                <div class="box-header">
                                    <h3 class="box-title">Leave Allocation Information
                                    </h3>
                                </div>
                                <div class="box-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Company Name <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ID="RequiredFieldValidator1" runat="server" CssClass="text-red"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator2" runat="server" CssClass="text-red"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Leave Type <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlLeaveType" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlLeaveType" ID="RequiredFieldValidator3" runat="server" CssClass="text-red"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Year <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtLeaveYear" runat="server" CssClass="form-control" placeholder="Enter Leave Year" TextMode="Number"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtLeaveYear" ID="RequiredFieldValidator7" runat="server" CssClass="text-red"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <%--                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Start Date <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtStartdate" runat="server" CssClass="form-control" placeholder="Enter Start Date" TextMode="Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtStartdate" ID="RequiredFieldValidator4" runat="server" CssClass="text-red"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">End Date <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="Enter End Date" TextMode="Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlLeaveType" ID="RequiredFieldValidator5" runat="server" CssClass="text-red"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>--%>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Leaves for Allocation <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtLeaves" placeholder="Total Allocated Leaves" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtLeaves" ID="RequiredFieldValidator6" runat="server" CssClass="text-red"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="hfMaxLeaves" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4"></label>
                                            <div class="col-sm-7">
                                                <asp:Button ID="BtnAdd1" runat="server" CssClass="btn btn-primary" OnClick="BtnAdd1_Click" Text="Add" ValidationGroup="A" />
                                                <asp:Button ID="btnTestDate" runat="server" CssClass="btn btn-primary hidden" OnClick="btnTestDate_Click" Text="Test" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="box">
                                <div class="box-header">
                                    <h3 class="box-title">Leaves</h3>
                                </div>
                                <div class="box-body no-padding">
                                    <asp:GridView ID="grd_Leaves" runat="server" AllowPaging="true" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" CssClass="table table-bordered table-striped" ForeColor="Black" GridLines="Both" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="srno0" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LeaveAllocateID" HeaderText="LeaveAllocateID" Visible="false" />
                                            <asp:BoundField DataField="LeaveName" HeaderText="Leave Type" />
                                            <asp:BoundField DataField="FromDateAllocation" HeaderText="From Date" />
                                            <asp:BoundField DataField="ToDateAllocation" HeaderText="To Date" />
                                            <asp:BoundField DataField="TotalAllocatedLeaves" HeaderText="Total Allocated Leaves" />
                                            <asp:BoundField DataField="PendingLeaves" HeaderText="Pending Leaves" Visible="false" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgeditleave" runat="server" CommandArgument='<%# Eval("LeaveName") %>' Height="20px" ImageUrl="~/Images/i_edit.png" OnClick="imgeditleave_Click" Width="20px" ToolTip="Edit" />
                                                    <asp:ImageButton ID="imgdeleteleave" runat="server" CommandArgument='<%# Eval("LeaveName") %>' Height="20px" ImageUrl="~/Images/i_delete.png" OnClick="imgdeleteleave_Click" Width="20px" ToolTip="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>No Record Exists....!!</EmptyDataTemplate>
                                        <AlternatingRowStyle BackColor="White" />
                                        <FooterStyle />
                                        <HeaderStyle />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <RowStyle />
                                        <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle />
                                        <SortedAscendingHeaderStyle />
                                        <SortedDescendingCellStyle />
                                        <SortedDescendingHeaderStyle />
                                    </asp:GridView>
                                </div>
                                <div class="box-footer">
                                    <asp:Button ID="btncancel" runat="server" CssClass="btn btn-default" OnClick="btncancel_Click" Text="Cancel" CausesValidation="False" />
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary pull-right" OnClick="btnsubmit_Click" Text="Save" />
                                    <asp:HiddenField ID="hfKey" runat="server" />
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <div class="col-lg-12">
                            <div class="box">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Allocated Details</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnback" runat="server" CssClass="btn btn-warning btn-sm" Text="Back to List" OnClick="btnback_Click" ToolTip="Back" />
                                    </div>
                                </div>
                                <div class="box-body no-padding">
                                    <asp:GridView ID="GridViewRenew" runat="server" AllowPaging="true" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtdetails" ReadOnly="true" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee ID" Visible="false">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblempid" ReadOnly="true" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemailid" ReadOnly="true" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldept" ReadOnly="true" runat="server" Text='<%# Eval("DeptName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcntct" ReadOnly="true" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Of Joining">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDOJ" ReadOnly="true" runat="server" Text='<%# Eval("DOJ1","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAN Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpan" ReadOnly="true" runat="server" Text='<%# Eval("PanNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblRenewHistory" runat="server" CommandArgument='<%# Eval("Employeeid") %>'
                                                        CssClass="btn btn-success btn-sm" OnClick="lblRenewHistory_Click"><i class="fa fa-arrow-circle-right"></i> Renew</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="box-header with-border">
                                    <h3 class="box-title">Leave History</h3>
                                </div>
                                <div class="box-body no-padding">
                                    <asp:GridView ID="GridviewLeaveAllocationHistoryBind" runat="server" AllowPaging="true" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        PageSize="10" OnPageIndexChanging="GridviewLeaveAllocationHistoryBind_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="srno0" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LeaveAllocateID" HeaderText="LeaveAllocateID" Visible="false" />
                                            <asp:BoundField DataField="LeaveName" HeaderText="Leave Type" />
                                            <asp:BoundField DataField="FromDateAllocation" HeaderText="From Date" DataFormatString="{0:MM/dd/yyyy}" />
                                            <asp:BoundField DataField="ToDateAllocation" HeaderText="To Date" DataFormatString="{0:MM/dd/yyyy}" />
                                            <asp:BoundField DataField="TotalAllocatedLeaves" HeaderText="Total Allocated Leaves" />
                                            <asp:BoundField DataField="PendingLeaves" HeaderText="Pending Leaves" Visible="false" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                   <asp:ImageButton ToolTip="Edit" runat="server"
                                                            CommandArgument='<%#Eval("LeaveAllocateID")%>'
                                                            ID="lnk" OnClick="lblEditRenewHistory_Click" ImageUrl="~/Images/i_edit.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>No Record Exists....!!</EmptyDataTemplate>
                                    </asp:GridView>

                                     <asp:HiddenField ID="hfleaveallocationid" runat="server" />
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
                <asp:Label ID="litDates" runat="server" CssClass="hidden"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

