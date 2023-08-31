<%@ Page Title="Country" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="MasterCountry.aspx.cs" Inherits="MasterCountry" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updd" runat="server">
        <ContentTemplate>
      
            <table width="100%">

                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server"
                            ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <asp:Panel ID="pannn" runat="server" DefaultButton="btnadd">
                                <table width="100%">
                                    <tr>
                                        <td style="padding-left: 18px">
                                            <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                                Text="Add New" />
                                        </td>
                                    </tr>
                                </table>
                                    </asp:Panel>
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
                                                            <asp:GridView ID="grd_City" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                CssClass="table table-bordered table-striped" OnPageIndexChanging="grd_City_PageIndexChanging" AllowPaging="true" PageSize="10">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="CountryId" HeaderText="Country Id" Visible="false" />
                                                                    <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                                                    <asp:BoundField DataField="CountryCode" HeaderText="Country Code" />
                                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                             <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%# Eval("CountryId") %>'
                                                                                Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="Unnamed1_Click" ToolTip="Edit" />

                                                                            <%--<asp:LinkButton Text="Edit" runat="server" CommandArgument='<%#Eval("CountryId")%>' ID="lnk" OnClick="Unnamed1_Click" />--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <FooterStyle />
                                                                <HeaderStyle />
                                                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                                               <%-- <PagerStyle HorizontalAlign="Right" />--%>
                                                                <RowStyle />
                                                                <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                                <SortedAscendingCellStyle />
                                                                <SortedAscendingHeaderStyle />
                                                                <SortedDescendingCellStyle />
                                                                <SortedDescendingHeaderStyle />
                                                                <EmptyDataTemplate>
                                                                    No Data Exists....
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                                    <section class="content-header">
                                        <h3>Country Information </h3>

                                    </section>
                                    <section class="content">

                                        <!-- left column -->
                                        <div class="col-md-7">

                                            <div class="box box-primary">
                                            
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <%-- <asp:Panel ID="PnlAdd" runat="server" Width="500px">--%>
                                                            <table width="100%">
                                                                <caption>
                                                                    <br />
                                                                    <tr>
                                                                        <td valign="top">Country Name:<span style="color: red;"> *</span> </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtcountryname" runat="server" CausesValidation="True" CssClass="form-control" placeholder="Enter Country Name" ValidationGroup="w" MaxLength="40"></asp:TextBox>
                                                                            
                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtcountryname" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz">
                                                                            </asp:FilteredTextBoxExtender>
                                                                            <asp:Label ID="lblcityid" runat="server" Visible="False"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcountryname" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="w" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            <%--<asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                                            </asp:ValidatorCalloutExtender>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">Country Code:<span style="color: red;"> *</span> </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtcountrycode" runat="server" CausesValidation="True" MaxLength="10" CssClass="form-control" placeholder="Enter Country Code" ValidationGroup="w"></asp:TextBox>
                                                                            
                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtcountrycode" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+">
                                                                            </asp:FilteredTextBoxExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcountrycode" ErrorMessage="*" ValidationGroup="w" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                                            </asp:ValidatorCalloutExtender>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">Status: </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rd_status" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Selected="True" Text="Active" Value="0">
                                  
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
                                                                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn bg-blue-active" OnClick="btnsubmit_Click" Text="Save" ValidationGroup="w" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" OnClick="btncancel_Click" Text="Cancel" />
                                                                        </td>
                                                                    </tr>
                                                                </caption>
                                                            </table>
                                                            <%--  </asp:Panel>--%>
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



        </ContentTemplate>
    </asp:UpdatePanel>

   
</asp:Content>

