<%@ Page Title="Leave Application" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="leave_application.aspx.cs" Inherits="leave_application" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <link href="admin_theme/dist/css/bootstrap_multiselect.css" rel="stylesheet" />
    <script src="admin_theme/dist/js/bootstrap_multiselect.js"></script>
    <script type="text/javascript">
        function jqFunctions() {


            $('#ContentPlaceHolder1_lstFruits').multiselect({

                includeSelectAllOption: true,
                enableFiltering: true,
                maxHeight: 900,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
                dropRight: true,
                onDropdownShow: function (event) {
                    $(this).closest('select').css('width', '1000px')
                }
            });



            var table = $('#ContentPlaceHolder1_gvDataList').DataTable({
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
    <style>
        .pad-top {
            padding-top: 32px;
            padding-left: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
            <asp:View ID="View1" runat="server">
                <div class="col-lg-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Leave List</h3>
                            <div class="box-tools">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm" OnClick="btnAdd_Click" />
                            </div>
                        </div>
                        <div class="box-body">
                            <asp:GridView ID="gvDataList" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvDataList_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Employee" DataField="EmpName" />
                                    <asp:BoundField HeaderText="EmployeeNo" DataField="EmployeeNo" />
                                    <asp:BoundField HeaderText="Leave Type" DataField="LeaveType" />
                                    <asp:BoundField HeaderText="Leave Reason" DataField="LeaveReason" />
                                    <asp:BoundField HeaderText="Leave From" DataField="LeaveStartDate" />
                                    <asp:BoundField HeaderText="Leave To" DataField="LeaveEndDate" />
                                    <asp:BoundField HeaderText="Is Approve" DataField="ManagerStatus" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <div class='<%# Eval("ManagerStatus").Equals("Approve")?"hidden":"" %>'>
                                                <asp:LinkButton ID="lbtnEvent" runat="server" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("LeaveApplicationId") %>' OnClick="lbtnEvent_Click"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
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
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Balance Leaves</h3>
                        </div>
                        <div class="box-body no-padding">
                            <div class="table-responsive">
                                <asp:GridView ID="grd_Leavependings" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
                                    <Columns>
                                        <asp:BoundField DataField="LeaveName" HeaderText="Leave Name" />
                                        <asp:BoundField DataField="EligibleLeaves" HeaderText="Leave Eligible" />
                                        <asp:BoundField DataField="TakenLeaves" HeaderText="Leave Taken" />
                                        <asp:BoundField DataField="LeaveBalance" HeaderText="Leave Balance" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No data Exists!!!!!!!!!!!!!!!!
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Apply for Leave</h3>
                        </div>
                        <div class="box-body">
                            <asp:UpdatePanel runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="txtEndDate" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="form-group col-lg-3">
                                            <label class="control-label">Company Name <span class="text-red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3">
                                            <label class="control-label">Department Name <span class="text-red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3">
                                            <label class="control-label">Employee Name <span class="text-red">*</span></label>
                                            <div class="">
                                                <asp:ListBox ID="lstFruits" runat="server" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"></asp:ListBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-red" ControlToValidate="lstFruits" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                       <%-- <div class="form-group col-lg-4">
                                            <label class="control-label">Employee Name <span class="text-red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="text-red" ControlToValidate="ddlEmployee" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlEmployee" InitialValue="0" ID="RequiredFieldValidator9" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 form-group">
                                            <label class="control-label">Leave Type</label>
                                            <asp:DropDownList ID="ddlLeaveType" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 form-group">
                                            <label class="control-label">Leave Reason</label>
                                            <asp:TextBox CssClass="form-control" ID="txtPurpose" runat="server" placeholder="Enter Purpose"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-4 form-group">
                                            <label class="control-label">Leave Days</label>
                                            <asp:TextBox placeholder="Enter Leave Duration" CssClass="form-control" ID="txtDuration" runat="server" Enabled="False"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 form-group">
                                            <label class="control-label">From</label>
                                            <asp:TextBox placeholder="Enter Start Date" TextMode="Date" CssClass="form-control" ID="txtStartDate" runat="server" AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged" CausesValidation="True" ValidationGroup="a"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtStartDate" Display="Dynamic"
                                                ErrorMessage="Select Start Date" SetFocusOnError="True"
                                                ValidationGroup="a" ForeColor="Red" Font-Size="9pt"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-lg-2 form-group">
                                            <div class="pad-top">
                                                <asp:RadioButton ID="rbFromFirst" runat="server" Checked="true" GroupName="From" Text="    First Half" AutoPostBack="true" OnCheckedChanged="rbToFirst_CheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2 form-group">
                                            <div class="pad-top">
                                                <asp:RadioButton ID="rbFromSecond" runat="server" GroupName="From" Text="    Second Half" AutoPostBack="true" OnCheckedChanged="rbToFirst_CheckedChanged" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 form-group">
                                            <label class="control-label">To</label>
                                            <asp:TextBox placeholder="Enter End Date" TextMode="Date" CssClass="form-control" ID="txtEndDate" runat="server" AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged" CausesValidation="True" ValidationGroup="a"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="txtEndDate" Display="Dynamic"
                                                ErrorMessage="Select End Date" SetFocusOnError="True"
                                                ValidationGroup="a" ForeColor="Red" Font-Size="9pt"></asp:RequiredFieldValidator>


                                        </div>
                                        <div class="col-lg-2 form-group">
                                            <div class="pad-top">
                                                <asp:RadioButton ID="rbToFirst" runat="server" GroupName="To" Text="    First Half" AutoPostBack="true" OnCheckedChanged="rbToFirst_CheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2 form-group">
                                            <div class="pad-top">
                                                <asp:RadioButton ID="rbToSecond" runat="server" Checked="true" GroupName="To" Text="    Second Half" AutoPostBack="true" OnCheckedChanged="rbToFirst_CheckedChanged" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn bg-blue-active"
                                                OnClick="btnSubmit_Click" ValidationGroup="a" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn bg-blue-active"
                                                OnClick="btnCancel_Click" CausesValidation="False" />
                                        </div>
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
        <asp:HiddenField ID="hfKey" runat="server" />
    </div>
</asp:Content>
