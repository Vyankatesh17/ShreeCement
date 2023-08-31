<%@ Page Title="Education" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="MasterEducation.aspx.cs" Inherits="MasterEducation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            
            <table width="100%">

                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
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
                                                            <asp:GridView ID="grd_Dept" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                CssClass="table table-bordered table-striped" OnPageIndexChanging="grd_Dept_PageIndexChanging" AllowPaging="true" PageSize="10">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="EducationName" HeaderText="Education Name" />
                                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%# Eval("EducationId") %>'
                                                                                Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="OnClick_Edit" ToolTip="Edit" />

                                                                           <%-- <asp:LinkButton ID="Edit" runat="server" Text="Edit" OnClick="OnClick_Edit" CommandArgument='<%# Eval("EducationId") %>'
                                                                                CssClass="linkbutton1" />--%>
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
                                                                <EmptyDataTemplate>
                                                                    No Data Exist....
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
                            <asp:View ID="View2" runat="server">
                                <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                                    <section class="content-header">
                                        <h3>Education Information </h3>

                                    </section>
                                    <section class="content">

                                        <!-- left column -->
                                        <div class="col-md-7">

                                            <div class="box box-primary">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="PnlAdd" runat="server" DefaultButton="btnsubmit">
                                                                <table width="100%">
                                                                <br />
                                                                    <tr>
                                                                        <td valign="top">Education Name :<span style="color: red;"> *</span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtdeptname" runat="server" CssClass="form-control" MaxLength="40" placeholder="Enter Education Name"></asp:TextBox>
                                                                            
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                                ControlToValidate="txtdeptname"  ForeColor="Red"
                                                                                ErrorMessage="*" SetFocusOnError="True"
                                                                                ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                           
                                                                            <asp:Label ID="lbldeptid" runat="server" Visible="False"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">Status :
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
                                                                        <td></td>
                                                                        <td>
                                                                            <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                                                CssClass="btn bg-blue-active" />
                                                                            &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <%-- <asp:RoundedCornersExtender ID="rn" runat="server" TargetControlID="PnlAdd" Radius="10"
                                                Corners="All" BorderColor="#333">
                                            </asp:RoundedCornersExtender>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </section>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>

            <%--<uc1:time id="modpop" runat="server" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

