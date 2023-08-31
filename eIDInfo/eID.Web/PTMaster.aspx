<%@ Page Title="PT Master" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="PTMaster.aspx.cs" Inherits="PTMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .style1 {
            padding: 10px;
            text-align: left;
            word-spacing: normal;
            border: 1px solid;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                                        <table width="100%">
                                            <tr>
                                                                                            
                                                <td>
                                                    <div style="margin-left:930px;">
                                                        <b> <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>

                                                                <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                                    </div>
                                                    <asp:GridView ID="grd_Expense" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-bordered table-striped"
                                                        OnPageIndexChanging="grd_Expense_PageIndexChanging" AllowPaging="true" PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="SlabFrom" HeaderText="Slab From " />
                                                            <asp:BoundField DataField="SlabTo" HeaderText="Slab To" />
                                                            <asp:BoundField DataField="StateName" HeaderText="Location" />
                                                            <asp:BoundField DataField="Charge" HeaderText="Charge" />
                                                            <asp:BoundField DataField="Jan" HeaderText="Jan" />
                                                            <asp:BoundField DataField="Feb" HeaderText="Feb" />
                                                            <asp:BoundField DataField="Mar" HeaderText="Mar" />
                                                            <asp:BoundField DataField="Apr" HeaderText="Apr" />
                                                            <asp:BoundField DataField="May" HeaderText="May" />
                                                            <asp:BoundField DataField="Jun" HeaderText="Jun" />
                                                            <asp:BoundField DataField="Jul" HeaderText="Jul" />
                                                            <asp:BoundField DataField="Aug" HeaderText="Aug" />
                                                            <asp:BoundField DataField="Sep" HeaderText="Sep" />
                                                            <asp:BoundField DataField="Oct" HeaderText="Oct" />
                                                            <asp:BoundField DataField="Nov" HeaderText="Nov" />
                                                            <asp:BoundField DataField="Dec" HeaderText="Dec" />
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgview" runat="server" ImageUrl="~/Images/i_edit.png" CommandArgument='<%# Eval("slabid") %>'
                                                                        OnClick="imgview_Click" />
                                                                </ItemTemplate>
                                                                <ControlStyle Font-Size="11pt" ForeColor="#3399FF" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                      <AlternatingRowStyle BackColor="White" />
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
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                            <section class="content-header">
                                <h3>Professional Tax Information </h3>

                            </section>
                            <section class="content">

                                <!-- left column -->
                                <div class="col-md-6">

                                    <div class="box box-primary">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <%-- <asp:Panel ID="PnlAdd" runat="server" Width="500px">--%>
                                                    <table width="100%">
                                                        <br />
                                                        <tr>
                                                            <td valign="top">&nbsp; Location:<span style="color: red;"> *</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DropDownList1"
                                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="h" ForeColor="Red" SetFocusOnError="true"
                                                                    ValueToCompare="--Select--"></asp:CompareValidator>
                                                                
                                                                <asp:Label ID="lblexpenseid" runat="server" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">&nbsp; Slab From:<span style="color: red;"> *</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsalbfrom" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                               
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtsalbfrom" ValidChars="0123456789.">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsalbfrom"
                                                                    ErrorMessage="*" ValidationGroup="h" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">&nbsp; Slab To:<span style="color: red;"> *</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtslabTo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                               
                                                                <asp:FilteredTextBoxExtender ID="txtexpenseAmt_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" TargetControlID="txtslabTo" ValidChars="0123456789.">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtslabTo"
                                                                 ErrorMessage="*" ValidationGroup="h" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">&nbsp; Charges:<span style="color: red;"> *</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcharge" runat="server" CssClass="form-control"></asp:TextBox>
                                                                
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtcharge" ValidChars="0123456789.">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcharge"
                                                                 ErrorMessage="*" ValidationGroup="h" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                               
                                                                <br />

                                                            </td>
                                                        </tr>
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

                                <div class="col-md-6">
                                    <div class="box box-primary">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <br />
                                                <asp:Panel ID="Panel1" CssClass="style1" runat="server" Width="500px">
                                                    <div>
                                                        &nbsp;<%--<asp:CheckBox ID="CheckBox1" runat="server" Text="Same To All" AutoPostBack="True"
                                                          OnCheckedChanged="CheckBox1_CheckedChanged1" />--%><asp:Button ID="btncheck" runat="server" Text="Same To All"  CssClass="btn bg-blue-active" OnClick="btncheck_Click" />

                                                    </div>
                                                    <table>
                                                        <tr>
                                                            <td>JAN: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtjan" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                            <td>FEB: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtfeb" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>MAR: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtmar" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                            <td>APR: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtapr" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>MAY: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtmay" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                            <td>JUN: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtjun" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>JUL: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtjul" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                            <td>AUG: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtaug" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>SEP: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtsep" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                            <td>OCT: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtoct" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>NOV: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtnov" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                            <td>DEC: &nbsp; &nbsp;
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtdec" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div style="background-color: #FFF; padding: 15px; height: 70px; margin-top: -12px;">
                                        <div class="box-body">
                                            <div class="footer">
                                                <table width="100%" cellspacing="8px" allign="center">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <div class="col-md-offset-5">
                                                                <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="h"
                                                                    CssClass="btn bg-blue-active" />
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </section>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
</asp:Content>
