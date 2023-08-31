<%@ Page Title="Gratuity Details" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="GratuityDetails.aspx.cs" Inherits="GratuityDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>

                                <table width="100%">
                                    <tr>
                                        <td>Company Name :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlcompany" runat="server"></asp:DropDownList>
                                             <asp:CompareValidator ID="intValidator" runat="server" ControlToValidate="ddlcompany" ValueToCompare="--Select--"
                                                                            Operator="NotEqual"  ErrorMessage="Please Select Company"
                                                                            ForeColor="Red" ValidationGroup="S"/>
                                            <asp:Label ID="lblbasicday" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblbasicAmt" runat="server" Visible="false"></asp:Label>
                                        &nbsp;&nbsp;
                                            <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" CssClass="btn bg-blue-active"
                                                Text="Search" ValidationGroup="S"/>
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
                                                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                                     <asp:BoundField DataField="DOJ" HeaderText="Joining Date" DataFormatString="{0:MM/dd/yyyy}"/>
                                                                    <asp:BoundField DataField="year" HeaderText="Years"/>
                                                                    <asp:BoundField DataField="Monthcount" HeaderText="Months" />
                                                                    <asp:BoundField DataField="DayBasic" HeaderText="Day Basic" />
                                                                    <asp:BoundField DataField="GratuityAmt" HeaderText="Gratuity Amount" />

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
                          
                 
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

