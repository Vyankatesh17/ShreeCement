<%@ Page Title="Documents" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="DocumentForm.aspx.cs" Inherits="DocumentForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
             
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <table width="100%">
                                    <tr>
                                        <td style="padding-left: 18px">
                                           <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                                        Text="Add New" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                   <table width="100%" cellspacing="8px">
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
                                                    <asp:GridView ID="grddoc" runat="server"
                                                        AutoGenerateColumns="False" BorderStyle="None" CssClass="table table-bordered table-striped"
                                                       AllowPaging="true" PageSize="10">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Document_Name" HeaderText="Document Name" />
                                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="lnkedit" runat="server" CommandArgument='<%# Eval("Document_Id") %>'
                                                                        Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="lnkedit_Click" ToolTip="Edit" />
                                                                    </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle />
                                                        <HeaderStyle />
                                                        <%--<PagerStyle HorizontalAlign="Right" />--%>
                                                         <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                                        <RowStyle />
                                                        <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle />
                                                        <SortedAscendingHeaderStyle />
                                                        <SortedDescendingCellStyle />
                                                        <SortedDescendingHeaderStyle />
                                                        <EmptyDataTemplate>
                                                            No Data Exists.....
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>




                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>


                <asp:View ID="View2" runat="server">
                    <section class="content-header">
                                    <h3>Add Document Name</h3>
                                </section>

                                <section class="content">

                    <div class="col-md-7">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <asp:Panel ID="panl" runat="server" DefaultButton="btnsubmit">
                                 <table width="100%" cellspacing="8px">
                                     <tr>
                                         <td colspan="2">
                                             &nbsp;
                                         </td>
                                     </tr>
                                            <tr>
                                                <td valign="top">Document Name:<span style="color:red;"> *</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdocumentname" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="filter" runat="server" TargetControlID="txtdocumentname" Enabled="true" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz "></asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtdocumentname" ForeColor="Red"
                                                        ErrorMessage="*" SetFocusOnError="True"
                                                        ValidationGroup="S">
                                                    </asp:RequiredFieldValidator>
                                                   
                                                    <asp:Label ID="lbldocid" runat="server" Visible="False">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Status :
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rd_status" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="0">Active</asp:ListItem>
                                                        <asp:ListItem Value="1">Inactive</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                        CssClass="btn bg-blue-active" />
                                                    &nbsp
                                                                <asp:Button ID="Button1" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                    <br />
                                                </td>
                                            </tr>

                                        </table>
                                        </asp:Panel>
                                    </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div>
                            <div>
                                <div class="form-group">

                                    <table width="100%" cellspacing="8px">
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div>
                            <div>
                                <div class="form-group">

                                    <table width="100%" cellspacing="8px">
                                        <tr>
                                            <td></td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </div>
                        </div>
                    </div>


                </asp:View>

            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

