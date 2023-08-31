<%@ Page Title="Training" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Training.aspx.cs" Inherits="Training" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <!-- left column -->
        <div class="col-md-12">

            <div class="box box-primary">
                <div class="box-body">
                <table width="70%">
                    <tr>
                        <td>Department Name:<span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="ddlDept"
                                Display="None" ErrorMessage="Select Department Name" Operator="NotEqual"
                                ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="True"></asp:CompareValidator>

                            <asp:ValidatorCalloutExtender ID="cmp2_ValidatorCalloutExtender" runat="server"
                                Enabled="True" TargetControlID="cmp2">
                            </asp:ValidatorCalloutExtender>
                           &nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td>Employee Name:<span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlEmp"
                                Display="None" ErrorMessage="Select Employee Name" Operator="NotEqual"
                                ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="True"></asp:CompareValidator>
                            <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="CompareValidator1">
                            </asp:ValidatorCalloutExtender>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Training Date:<span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Class="fa fa-calendar" ReadOnly="true" placeholder="Enter Training Date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                Display="None" ErrorMessage="Enter Training Date" SetFocusOnError="True"
                                ValidationGroup="S"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Date Should be Greater Than Current Date" Operator="GreaterThanEqual"
ControlToValidate="txtDate"   SetFocusOnError="true" Display="None" ValidationGroup="S"/>

                            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                            </asp:ValidatorCalloutExtender>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                                runat="server" Enabled="True" TargetControlID="CompareValidator2">
                            </asp:ValidatorCalloutExtender>


                            <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                Enabled="True" TargetControlID="txtDate">
                            </asp:CalendarExtender>
                            &nbsp;
                        </td>
                    </tr>
                    <br />

                    <tr>
                        <td>Training Subject:<span style="color: red;">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="Enter Training Subject"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtSubject" Display="None"
                                ErrorMessage="Enter Training Subject" SetFocusOnError="True"
                                ValidationGroup="S"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                            </asp:ValidatorCalloutExtender>
                            &nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td>Trainer:<span style="color: red;">*</span></td>
                        <td>

                            <asp:TextBox ID="txtTrainer" runat="server" CssClass="form-control" placeholder="Enter Trainer Name"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="txtTrainer" Display="None" ErrorMessage="Enter Trainer Name"
                                SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                            </asp:ValidatorCalloutExtender>
                            &nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td>Training Time:<span style="color: red;">*</span></td>
                        <td>

                            <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" placeholder="Enter Trainig Time"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtTime_MaskedEditExtender" runat="server"
                                TargetControlID="txtTime"
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="True"
                                AutoCompleteValue="00:00"
                                CultureName="en-US">
                            </asp:MaskedEditExtender>
                            <%-- <ajaxToolkit:MaskedEditExtender
    TargetControlID="TextBox2" 
    Mask="9,999,999.99"
    MessageValidatorTip="true" 
    OnFocusCssClass="MaskedEditFocus" 
    OnInvalidCssClass="MaskedEditError"
    MaskType="Number" 
    InputDirection="RightToLeft" 
    AcceptNegative="Left" 
    DisplayMoney="Left"
    ErrorTooltipEnabled="True"/>
                            --%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="txtTime" Display="None" ErrorMessage="Enter Training Time"
                                SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator4_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                            </asp:ValidatorCalloutExtender>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                     <tr>
                        <td></td>
                        <td></td>
                    </tr>
                     <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn bg-blue-active"
                                OnClick="btnSubmit_Click1" ValidationGroup="S" />

                            <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" CssClass="btn bg-blue-active" />

                        </td>
                    </tr>
                </table>
                    </div>
            </div>
        </div>
        </div>
</asp:Content>

