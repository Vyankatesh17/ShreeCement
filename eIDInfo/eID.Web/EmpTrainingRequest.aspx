<%@ Page Title="Training Request" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="EmpTrainingRequest.aspx.cs" Inherits="EmpTrainingRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-7">
            <div class="box box-primary">
                <div class="box-body">
                    <table width="80%">
                        <tr>
                            <td>Request Date:<span style="color: red;">*</span>
                            </td>
                            <td>&nbsp;
                                    <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                    Display="None" ErrorMessage="Enter Training Date" SetFocusOnError="True"
                                    ValidationGroup="S"></asp:RequiredFieldValidator>

                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Date Should be Greater Than Current Date" Operator="GreaterThanEqual"
                                    ControlToValidate="txtDate" SetFocusOnError="true" Display="None" ValidationGroup="S" />
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
                        <tr>
                            <td>Training Topic:<span style="color: red;">*</span></td>
                            <td>
                                <asp:TextBox ID="txtSubject" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtSubject" Display="None"
                                    ErrorMessage="Enter Training Subject" SetFocusOnError="True"
                                    ValidationGroup="S"></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </asp:ValidatorCalloutExtender>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div style="margin-left: 200px">
                        <asp:Button ID="btnSubmit" runat="server" Text="Request"
                            OnClick="btnSubmit_Click1" ValidationGroup="S" CssClass="btn bg-blue-active" />

                        <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" CssClass="btn bg-blue-active" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

