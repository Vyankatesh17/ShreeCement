<%@ Page Title="Expense Master" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ExpenceMaster.aspx.cs" Inherits="ExpenceMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            <fieldset>
                <legend></legend>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

                    <asp:View ID="View1" runat="server">
                        <asp:Panel ID="pnlAdd" DefaultButton="addnew" runat="server">
             
                            <table>
                                <tr>
                                    <td style="padding-left: 18px">
                                        <asp:Button ID="addnew" runat="server" Text="Add New" OnClick="addnew_Click"
                                            CssClass="btn bg-blue-active" />
                                    </td>
                                </tr>
                            </table>
                            <br />

                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-body">
                                        <div class="form-group">

                                            <table width="100%">
                                                <tr>
                                                    <td align="right">
                                                        <b>
                                                            <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>

                                                            <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Width="100%"
                                                            PagerStyle-CssClass="pgr" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                            OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>


                                                                <asp:BoundField DataField="ExpenseId" HeaderText="Expense Type" Visible="false" />
                                                                <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" />
                                                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                <asp:TemplateField HeaderText="Action"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgview" runat="server" ImageUrl="~/Images/i_edit.png" CommandArgument='<%# Eval("ExpenseId") %>'
                                                                            OnClick="imgview_Click" />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Size="11pt" ForeColor="#3399FF" />

                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle CssClass="pagination" HorizontalAlign="Right" />
                                                            <EmptyDataTemplate>
                                                                No Data Exists .....
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
         
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                     
                                <!-- left column -->
                                <div class="col-md-6">

                                    <div class="box box-primary">
                                        <table width="100%">
                                            <br />


                                            <tr>
                                                <td valign="top">Expense Type:<span style="color: red;"> *</span>
                                                </td>
                                                <td>

                                                    <asp:TextBox ID="txtchargename" runat="server" Width="250px" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtchargename"
                                                        Display="None" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    <%--  <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                    </asp:ValidatorCalloutExtender>--%>
                                                    <asp:Label ID="lbldesid1" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td valign="top">Account Type:
                                                </td>
                                                <td>

                                                    <asp:TextBox ID="txtAccountType" runat="server" Width="250px" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAccountType"
                                                        Display="None" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="SS"></asp:RequiredFieldValidator>
                                                    <%--  <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                    </asp:ValidatorCalloutExtender>--%>
                                                    <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                                                    <br />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td valign="top">Status:
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rd_status" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Active" Value="0" Selected="True">
                                  
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Inactive" Value="1">
                                  
                                                        </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <br />
                                                </td>
                                            </tr>


                                            <tr>
                                                <td class="style1">&nbsp;
                                                </td>
                                                <td>
                                                
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" ValidationGroup="S"
                                                        OnClick="btnsubmit_Click" CssClass="btn bg-blue-active" />
                                                    &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel"
                                                        OnClick="btncancel_Click" CssClass="btn bg-blue-active" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                           
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

