<%@ Page Title="Gratuity Master" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="GratuityMaster.aspx.cs" Inherits="GratuityMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                            <asp:Button ID="btnaddnew" runat="server" OnClick="btnaddnew_Click" CssClass="btn bg-blue-active"
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
                                                            <asp:GridView ID="grd_Gratuity" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                CssClass="table table-bordered table-striped" OnPageIndexChanging="grd_Gratuity_PageIndexChanging" AllowPaging="true" PageSize="10">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="FromYears" HeaderText="From Years" />
                                                                            <asp:BoundField DataField="ToYears" HeaderText="To Years" />
                                                                            <asp:BoundField DataField="BasicDays" HeaderText="Basic Days" />
                                                                     <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%#Eval("GratuityId") %>'
                                                                                        Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" ToolTip="Edit" OnClick="imgedit_Click" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%#Eval("GratuityId") %>'
                                                                                        Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" ToolTip="Delete" OnClick="imgdelete_Click" />
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
                                                                    No Data Exists.....
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
                                <section class="content-header">
                                    <h3>Gratuity Information </h3>
                                </section>
                                <section class="content">
                                    <div class="col-md-7">
                                        <div class="box box-primary">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <!-- left column -->
                                                    <asp:Panel ID="pan" runat="server" DefaultButton="btnsubmit">
                                                        <table cellpadding="8px" cellspacing="2" width="100%">
                                                           
                                                            
                                                            <tr>
                                                                <td>From Years :<span style="color: red;"> *</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtfromyear" runat="server" CssClass="form-control" placeholder="Enter From Years"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="flt1" runat="server" TargetControlID="txtfromyear" FilterType="Custom" ValidChars=".0123456789"></asp:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                        ControlToValidate="txtfromyear" ForeColor="Red"
                                                                        ErrorMessage="Enter From Years" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ID="intValidator" runat="server" ControlToValidate="txtfromyear"
                                                                            Operator="DataTypeCheck" Type="Double" ErrorMessage="Please Insert Correct Value"
                                                                            ForeColor="Red" ValidationGroup="S"/>
                                                                    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtfromyear"
                                                                            Operator="LessThan" ControlToCompare="txtToyear" ErrorMessage="You can not enter From Years greater than To Years"
                                                                            ForeColor="Red" ValidationGroup="S"/>--%>
                                                                   

                                                                    <asp:Label ID="lblgratutyid" runat="server" visible="false"></asp:Label>
                                                                   

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>To Years :<span style="color: red;"> *</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtToyear" runat="server" CssClass="form-control" placeholder="Enter To Years"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtToyear" FilterType="Custom" ValidChars=".0123456789"></asp:FilteredTextBoxExtender>
                                                                     
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                        ControlToValidate="txtToyear" ForeColor="Red"
                                                                        ErrorMessage="Enter To Years" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToyear"
                                                                            Operator="DataTypeCheck" Type="Double" ErrorMessage="Please Insert Correct Value"
                                                                            ForeColor="Red" ValidationGroup="S"/>
                                                                     <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtToyear"
                                                                            Operator="GreaterThan" ControlToCompare="txtfromyear" ErrorMessage="You can not enter To Years less than From Years"
                                                                            ForeColor="Red" ValidationGroup="S"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Basic Day :<span style="color: red;"> *</span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtbasicday" runat="server" CssClass="form-control" placeholder="Enter Basic Day"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtbasicday" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                        ControlToValidate="txtbasicday" ForeColor="Red"
                                                                        ErrorMessage="Enter Basic Day" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               <td></td>
                                                                <td>
                                                                    
                                                                   
                                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn bg-blue-active"
                                                                        Text="Add" ValidationGroup="S" OnClick="btnAdd_Click" TabIndex="7" />
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:GridView ID="grdaddgratuity" runat="server"
                                                                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                        Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sr.No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="FromYears" HeaderText="From Years" />
                                                                            <asp:BoundField DataField="ToYears" HeaderText="To Years" />
                                                                            <asp:BoundField DataField="BasicDays" HeaderText="Basic Days" />
                                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                                        Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" ToolTip="Edit" OnClick="imgDocedit_Click" />

                                                                                    <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                                        Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" ToolTip="Delete" OnClick="imgDocdelete_Click" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <FooterStyle />
                                                                        <HeaderStyle />
                                                                        <PagerStyle HorizontalAlign="left" />
                                                                        <RowStyle />
                                                                        <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                                        <SortedAscendingCellStyle />
                                                                        <SortedAscendingHeaderStyle />
                                                                        <SortedDescendingCellStyle />
                                                                        <SortedDescendingHeaderStyle />
                                                                        <RowStyle CssClass="odd" />
                                                                        <AlternatingRowStyle CssClass="even" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" 
                                                                        CssClass="btn bg-blue-active" />
                                                                    &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </asp:Panel>




                                                </div>
                                            </div>
                                        </div>
                                </section>

                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

