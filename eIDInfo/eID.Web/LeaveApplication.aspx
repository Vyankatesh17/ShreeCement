<%@ Page Title="Leave Application" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="LeaveApplication.aspx.cs" Inherits="LeaveApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
        }

        .pad-top {
            padding-top: 32px;
            padding-left: 10px;
        }

        .ajax__calendar {
            z-index: 999 !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
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
                <div class="box-header">
                    <h3 class="box-title">Apply Leave</h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
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
                                    <asp:TextBox placeholder="Enter Start Date" CssClass="form-control" ID="txtStartDate" runat="server" AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged" CausesValidation="True" ValidationGroup="a"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtStartDate">
                                    </asp:CalendarExtender>
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
                                    <asp:TextBox placeholder="Enter End Date" CssClass="form-control" ID="txtEndDate" runat="server" AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged" CausesValidation="True" ValidationGroup="a"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtEndDate">
                                    </asp:CalendarExtender>

                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Invalid Date" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" ForeColor="Red" Operator="GreaterThanEqual" Type="Date" ValidationGroup="a" Font-Size="9pt" Display="Dynamic"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtEndDate" Display="Dynamic"
                                        ErrorMessage="Select End Date" SetFocusOnError="True"
                                        ValidationGroup="a" ForeColor="Red" Font-Size="9pt"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="chkEndIsDate" runat="server" Display="Dynamic"
                                        Operator="DataTypeCheck" Type="Date" ControlToValidate="txtEndDate"
                                        ErrorMessage="You must supply a valid end date" ValidationGroup="a" ForeColor="Red" Font-Size="9pt" />

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
    </div>

</asp:Content>
