<%@ Page Title="Leave Allocation" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="LeaveAllocationNew.aspx.cs" Inherits="LeaveAllocationNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
    </div>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">

            <div class="box box-primary">
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-3 form-group">
                            Select Company
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                            ControlToValidate="ddlCompanyList" Display="Dynamic"
                            ErrorMessage="*" SetFocusOnError="True"
                            ValidationGroup="S" ForeColor="Red" InitialValue="--Select--"></asp:RequiredFieldValidator>
                            <asp:DropDownList runat="server" ID="ddlCompanyList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyList_SelectedIndexChanged"></asp:DropDownList>

                        </div>
                        <div class="col-lg-3 form-group">
                            Department Name
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlDepartment" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3 form-group">
                            Employee Name
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlEmployeeList" OnSelectedIndexChanged="ddlEmployeeList_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3 form-group">
                            <div style="margin-top: 15px;">
                                <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" ValidationGroup="S"
                                    Text="Search" OnClick="btnsearch_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn bg-blue-active" Text="Reset" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div style="margin-left: 750px">
                                        <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                        </asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblcnt" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>

                                    <asp:GridView ID="grd_Emp" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                        BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                        GridLines="Vertical" CssClass="table table-bordered table-striped"
                                        AllowPaging="true"
                                        PageSize="10" OnSelectedIndexChanged="grd_Emp_SelectedIndexChanged"
                                        OnPageIndexChanging="grd_Emp_PageIndexChanging">
                                        <AlternatingRowStyle BackColor="White" />
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
                                            <asp:TemplateField HeaderText="Date Of Confirmation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConfirmDate" ReadOnly="true" runat="server" Text='<%# Eval("ConfirmDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--                                                    <asp:TemplateField HeaderText="PAN Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpan" ReadOnly="true" runat="server" Text='<%# Eval("PanNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                            <asp:BoundField DataField="AllocationStatus" HeaderText="Allocation Status" />

                                            <asp:TemplateField HeaderText="Allocate Leaves">
                                                <ItemTemplate>
                                                    <asp:ImageButton ImageUrl="~/Images/a.png" Height="30px" Width="30px" ID="lblAllocate" runat="server"
                                                        Text="Allocate" OnClick="OnClick_Edit"
                                                        CommandArgument='<%# Eval("Employeeid") %>' ToolTip="Allocate Leaves" />

                                                    <%--<asp:LinkButton ID="LinkButton1" runat="server" Text="Allocate" OnClick="OnClick_Edit"
                                                                CommandArgument='<%# Eval("Employeeid") %>' ToolTip="Allocate"
                                                                ForeColor="Blue" />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Renew Leaves">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel runat="server" ID="up1">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="lblRenew" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:ImageButton ImageUrl="~/Images/Renew.jpg" Height="35px" Width="35px" ID="lblRenew" runat="server" Text="Renew" CommandArgument='<%# Eval("Employeeid") %>'
                                                                OnClick="lblRenew_Click" ToolTip="Renew Leaves" />

                                                            <%--                                                                     <asp:LinkButton ID="LinkButton1" runat="server" Text="Renew" CommandArgument='<%# Eval("Employeeid") %>'
                                                                        ForeColor="Blue" OnClick="lblRenew_Click" ToolTip="Renew" />--%>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <%--     <asp:PostBackTrigger ControlID="Edit" />--%>
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
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

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

        </asp:View>
        <asp:View ID="View2" runat="server">

            <section class="content-header">
                <h3>Leave Allocation Information</h3>
            </section>
            <div class="col-md-12">
                <div class="box box-primary">

                    <div class="box-body">
                        <div class="form-group">

                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td>
                                        <div class="form-group">
                                            Leave Type:
                                                                <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </div>
                                    </td>
                                    <td colspan="3">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>



                                                        <asp:DropDownList ID="ddlLeaveType" runat="server"
                                                            CssClass="form-control" Width="270px">
                                                        </asp:DropDownList><br />
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                            ControlToValidate="ddlLeaveType" Display="Dynamic"
                                                            ErrorMessage="Select Leave Type" Operator="NotEqual" ValidationGroup="w"
                                                            ValueToCompare="--Select--" SetFocusOnError="True" Font-Size="10pt" ForeColor="Red"></asp:CompareValidator>



                                                        <%--   <asp:ValidatorCalloutExtender ID="reqLeave"
                                                            runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                                        </asp:ValidatorCalloutExtender>--%>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </td>
                                    <td></td>

                                </tr>
                                <tr>
                                    <td>
                                        <div class="form-group">
                                            Start Date:
                                                             
                                                                <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            &nbsp;<asp:TextBox Visible="false" ID="TextBox1" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="form-group">
                                            <asp:UpdatePanel runat="server">
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="txtStartdate" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtStartdate" runat="server" CssClass="form-control" Width="270px" placeholder="Enter Start Date" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>


                                                    <asp:CalendarExtender ID="txtjoiningdate_CalendarExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtStartdate">
                                                    </asp:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                        ControlToValidate="txtStartdate" Display="Dynamic"
                                                        ErrorMessage="Enter Start Date" SetFocusOnError="True"
                                                        ValidationGroup="w" Font-Size="10pt" ForeColor="Red"></asp:RequiredFieldValidator>

                                                    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Invalid Date" ControlToCompare="txtDate" ControlToValidate="txtStartdate" ForeColor="Red" Operator="GreaterThanEqual" Type="Date" ValidationGroup="w"></asp:CompareValidator>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                    <td>

                                        <%--   <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2"
                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </asp:ValidatorCalloutExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="form-group">
                                            End Date:
                                                                   <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            &nbsp;<asp:TextBox Visible="false" ID="TextBox2" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="form-group">
                                            <asp:UpdatePanel runat="server">
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="txtEndDate" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="Enter End Date" Width="270px" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>


                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Enabled="True" TargetControlID="txtEndDate">
                                                    </asp:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtEndDate" Display="Dynamic"
                                                        ErrorMessage="Enter End Date" SetFocusOnError="True"
                                                        ValidationGroup="w" Font-Size="10pt" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Invalid Date"
                                                        ControlToCompare="txtStartdate" ControlToValidate="txtEndDate" ForeColor="Red" Operator="GreaterThanEqual" Type="Date" ValidationGroup="w"></asp:CompareValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="form-group">Total Allocated Leaves:</div>
                                    </td>
                                    <td>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtTotalAllocatedLeaves" placeholder="Total Allocated Leaves" runat="server" AutoCompleteType="None" CssClass="form-control" Width="270px" MaxLength="3"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="txtTotalAllocatedLeaves_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtTotalAllocatedLeaves" ValidChars="0123456789">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTotalAllocatedLeaves" Display="Dynamic" ErrorMessage="Enter Total Allocated Leaves" SetFocusOnError="True" ValidationGroup="w" Font-Size="10pt" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                            <asp:Label ID="lblduration" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                    </td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>

                                    <td>
                                        <div style="display: none">
                                            <asp:TextBox ID="txtDate" runat="server" Text="Label"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td colspan="3">

                                        <asp:Button ID="BtnAdd1" runat="server" CssClass="btn bg-blue-active" OnClick="BtnAdd1_Click" Text="Add" ValidationGroup="w" />

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div class="form-group">
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
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3">
                                        <div class="footer">
                                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn bg-blue-active" OnClick="btnsubmit_Click" Text="Save" />
                                            &nbsp;
                                                                <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" OnClick="btncancel_Click" Text="Cancel" CausesValidation="False" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="View3" runat="server">

            <section class="content-header">
                <h3>Leave Allocation Information</h3>
            </section>
            <div class="col-md-12">
                <div class="box box-primary">

                    <div class="box-body">
                        <div class="form-group">


                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridViewRenew" runat="server" AllowPaging="true"
                                            AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px"
                                            CellPadding="4" CssClass="table table-bordered table-striped" ForeColor="Black" GridLines="Both"
                                            PageSize="10">
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
                                                        <asp:LinkButton ID="lblRenewHistory" runat="server" Text="Renew" CommandArgument='<%# Eval("Employeeid") %>'
                                                            ForeColor="Blue" ToolTip="Renew" OnClick="lblRenewHistory_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
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
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                </div>
            </div>

            <section class="content-header">
                <h3>Renew Leave History </h3>
            </section>
            <div class="col-md-12">
                <div class="box box-primary">

                    <div class="box-body">
                        <div class="form-group">

                            <asp:GridView ID="GridviewLeaveAllocationHistoryBind" runat="server" AllowPaging="true"
                                AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="4" CssClass="table table-bordered table-striped" ForeColor="Black" GridLines="Both"
                                PageSize="10"
                                OnPageIndexChanging="GridviewLeaveAllocationHistoryBind_PageIndexChanging">
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

                            <br />
                            <asp:Button ID="btnback" runat="server" CssClass="btn bg-blue-active" Text="Back" OnClick="btnback_Click" ToolTip="Back" />



                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>


    <%--  <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator11">
                                                    </asp:ValidatorCalloutExtender>--%>

    <%--   <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2"
                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </asp:ValidatorCalloutExtender>--%>
</asp:Content>

