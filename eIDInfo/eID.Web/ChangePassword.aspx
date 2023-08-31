<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="uppd" runat="server">
        <ContentTemplate>





            <asp:MultiView ID="MultiView1" runat="server">





                <table width="100%">

                    <tr>
                        <td>

                            <asp:View ID="View1" runat="server">

                                <div class="col-md-6">
                                    <div class="box box-primary">
                                        <div class="box-body">
                                            <div class="form-group">

                                                <asp:Panel ID="pnl" runat="server" DefaultButton="Button1">
                                                    <table width="100%" cellpadding="5">

                                                        <tr>
                                                            <td>
                                                                <table width="500px" cellpadding="9" runat="server" id="tblsecurity">


                                                                    <tr>
                                                                        <td>Security Question :
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtQuestion" runat="server" ReadOnly="True" CssClass="form-control"
                                                                                TextMode="MultiLine"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Answer : </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAnswer" runat="server" Width="250px" Style="margin-right: 100px" CssClass="form-control"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <caption>

                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="Button1" runat="server" OnClick="btnsubmit_Click" Style="margin-left: 30px" CssClass="btn bg-blue-active"
                                                                                    Text="Submit" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" CssClass="btn bg-blue-active"
                                                                                    Text="Cancel" />
                                                                            </td>
                                                                        </tr>
                                                                    </caption>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </td>
                    </tr>
                </table>

                <asp:View ID="View2" runat="server">
                    <table width="97%">
                        <tr>
                            <td>
                                <div style="float: right">
                                    <h4><small><a href="admin_dashboard.aspx">Dashboard</a>&nbsp; ><a href="#"> Manage User</a>&nbsp; > &nbsp;<a href="ChangePassword.aspx">Change Password</a>&nbsp;</small></h4>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <section class="content-header">
                        <h3>Change Password</h3>
                    </section>

                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">

                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnSubmit">

                                        <table width="100%">

                                            <tr>
                                                <td>


                                                    <table width="500px">

                                                        <tr>
                                                            <td>Current Password :<asp:Label runat="server" ForeColor="Red" Text=" *"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox5" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rq1" runat="server"
                                                                    ControlToValidate="TextBox5" Display="None"
                                                                    ErrorMessage="Current Password Required" SetFocusOnError="True"
                                                                    ValidationGroup="S"></asp:RequiredFieldValidator>
                                                               <%-- <asp:ValidatorCalloutExtender ID="rq1_ValidatorCalloutExtender" runat="server"
                                                                    Enabled="True" TargetControlID="rq1">
                                                                </asp:ValidatorCalloutExtender>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>New Password :<asp:Label runat="server" ForeColor="Red" Text=" *"></asp:Label></td>
                                                            <td>
                                                                <br />
                                                                <asp:TextBox ID="TextBox6" runat="server" TextMode="Password" MaxLength="20" CssClass="form-control"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="rq2" runat="server"
                                                                    ControlToValidate="TextBox6" Display="None"
                                                                    ErrorMessage="" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                               <%-- <asp:ValidatorCalloutExtender ID="rq2_ValidatorCalloutExtender" runat="server"
                                                                    Enabled="True" TargetControlID="rq2">
                                                                </asp:ValidatorCalloutExtender>--%>

                                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="TextBox6" Display="None" 
                            ErrorMessage="Please Enter atleast 6 characters" SetFocusOnError="True" 
                            ValidationExpression="^[a-zA-Z0-9'@&#.\s]{6,20}$" ValidationGroup="S">
                            </asp:RegularExpressionValidator>--%>
                                                                <%-- <asp:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender" 
                            runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                        </asp:ValidatorCalloutExtender>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Confirm Password :<asp:Label runat="server" ForeColor="Red" Text=" *"></asp:Label></td>
                                                            <td>
                                                                <br />
                                                                <asp:TextBox ID="TextBox7" runat="server" TextMode="Password" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rq3" runat="server"
                                                                    ControlToValidate="TextBox7" Display="None"
                                                                    ErrorMessage="" SetFocusOnError="True"
                                                                    ValidationGroup="S"></asp:RequiredFieldValidator>
                                                               <%-- <asp:ValidatorCalloutExtender ID="rq3_ValidatorCalloutExtender" runat="server"
                                                                    Enabled="True" TargetControlID="rq3">
                                                                </asp:ValidatorCalloutExtender>--%>


                                                             <%--   <asp:CompareValidator ID="cr1" runat="server" ControlToCompare="TextBox6"
                                                                    ControlToValidate="TextBox7" Display="None"
                                                                    ErrorMessage="Enter Correct Password" ValidationGroup="S"></asp:CompareValidator>

                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server"
                                                                    TargetControlID="cr1">
                                                                </asp:ValidatorCalloutExtender>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <br />
                                                                <asp:Button ID="BtnSubmit" runat="server" Text="Update" CssClass="btn bg-blue-active"
                                                                    OnClick="BtnSubmit_Click" ValidationGroup="S" />


                                                                <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="btn bg-blue-active"
                                                                    OnClick="btncancel_Click" />
                                                            </td>



                                                        </tr>
                                                    </table>

                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>
            </asp:MultiView>

            <%--  <uc1:Time ID="modpop" runat="server" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

