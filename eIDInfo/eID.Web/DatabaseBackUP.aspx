<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="DatabaseBackUP.aspx.cs" Inherits="DatabaseBackUP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            <%--   <fieldset id="field"><legend>Database Backup</legend>--%>
            <table width="97%">
                <tr>
                    <td>
                        <div style="float: right">
                            <h4><small><a href="admin_dashboard.aspx">Dashboard</a>&nbsp; ><a href="#"> Manage User</a>&nbsp; > &nbsp;<a href="DatabaseBackUP.aspx">Database Backup</a>&nbsp;</small></h4>
                        </div>
                    </td>
                </tr>
            </table>
            <section class="content-header">
                <h3>Database Backup</h3>
            </section>

            <div class="col-md-6">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <table cellpadding="2" cellspacing="2" width="100%">

                                <tr>
                                    <td>

                                        <table cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PnlAdd" runat="server" Width="500px">
                                                        <table cellpadding="2" cellspacing="2" width="100%">


                                                            <%--  <tr>
                                                        <td>
                                                            Database BackUp:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdesigname" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdesigname"
                                                                Display="None" ErrorMessage="Enter Designation Name" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                            </asp:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>--%>

                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Button ID="btnsubmit" runat="server" Text="Take Backup" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                                        CssClass="btn bg-blue-active" />
                                                                    &nbsp
                                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                CssClass="btn bg-blue-active" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <uc1:Time ID="modpop" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


