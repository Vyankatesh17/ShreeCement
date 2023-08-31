<%@ Page Title="Menu Master" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="MasterMenu.aspx.cs" Inherits="MasterMenu" %>

<%--
<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>

                        <asp:MultiView ID="MultiView1" runat="server">
                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:View ID="View1" runat="server">
                                                <table width="97%">
                                                    <tr>
                                                        <td style="padding-left: 15px">

                                                            <asp:Button ID="btnadd" runat="server" Text="Add New" OnClick="btnadd_Click" CssClass="btn bg-blue-active" />

                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>

                                                </table>


                                                <div class="col-md-12">
                                                    <div class="box box-primary">
                                                        <div class="box-body">
                                                            <div class="form-group">
                                                               
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="right">
                                                                            <b>
                                                                                <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                                                                </asp:Label>&nbsp;&nbsp;
                                                    <asp:Label ID="lblcnt" runat="server">
                                                    </asp:Label></b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="grd_Menu" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                                OnPageIndexChanging="grd_menu_OnPageIndexChanging" PageSize="10" Width="100%"
                                                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-bordered table-striped">
                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                                                                        </ItemTemplate>

                                                                                    </asp:TemplateField>
                                                                                    <%-- <asp:BoundField DataField="menuid" HeaderText="Menu Id" />--%>
                                                                                    <asp:BoundField DataField="menuname" HeaderText="Menu Name" />
                                                                                    <asp:BoundField DataField="parentname" HeaderText="Parent Menu" />
                                                                                    <asp:BoundField DataField="PageUrl" HeaderText="Page Url" />
                                                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <%--   <ItemTemplate>
                                                                <asp:LinkButton ID="Edit" runat="server" Text="Edit" OnClick="OnClick_Edit" CommandArgument='<%# Eval("menuid") %>' />
                                                            </ItemTemplate>--%>

                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imgview" runat="server" ImageUrl="~/Images/i_edit.png" CommandArgument='<%# Eval("menuid") %>'
                                                                                                OnClick="OnClick_Edit" ToolTip="Edit" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <PagerStyle HorizontalAlign="Right" />
                                                                                <EmptyDataTemplate>
                                                                                    No Data Exists!!!!
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:View>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:View ID="View2" runat="server">
                              
                                <table cellpadding="2" cellspacing="2" width="100%">


                                    <tr>
                                        <td>
                                            <div class="col-md-7">
                                                <div class="box box-primary">
                                                    <div class="box-body">
                                                        <div class="form-group">
                                                            <asp:Panel ID="PnlAdd" runat="server" Width="548px">

                                                                 <asp:Panel runat="server"  DefaultButton="btnsubmit">

                                                                <table cellpadding="2" cellspacing="2" width="100%">
                                                                    <tr>
                                                                        <td>Menu Name:<asp:Label Text=" *" ForeColor="Red" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtmenuname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmenuname"
                                                                                Display="None" ErrorMessage="Menu Name Required" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                                            </asp:ValidatorCalloutExtender>
                                                                            <asp:Label ID="lblmenuid" runat="server" Visible="False"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style1">Parent Menu:<asp:Label Text=" *" ForeColor="Red" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>&nbsp;
                                                                <asp:DropDownList ID="ddlParentMenu" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>

                                                                            <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddlParentMenu" Display="Dynamic"
                                                                                ErrorMessage="*" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Page URL :<asp:Label Text=" *" ForeColor="Red" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPageUrl" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rq2" runat="server" ControlToValidate="txtPageUrl"
                                                                                Display="None" ErrorMessage="Page URL Required" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                            <asp:ValidatorCalloutExtender ID="rq2_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                                TargetControlID="rq2">
                                                                            </asp:ValidatorCalloutExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Status:
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rd_status" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Text="Active" Value="0" Selected="True">
                                  
                                                                                </asp:ListItem>
                                                                                <asp:ListItem Text="Inactive" Value="1">
                                  
                                                                                </asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td>
                                                                            <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                                                CausesValidation="false" CssClass="btn bg-blue-active" />
                                                                            &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                </asp:Panel>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
