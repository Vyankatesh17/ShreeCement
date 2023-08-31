<%@ Page Title="HR Policy" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="HRPolicy.aspx.cs" Inherits="HRPolicy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
            <table width="100%">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <table>

                                    <tr>
                                        <td>
                                            <asp:Button ID="btnadd" runat="server" CssClass="btn bg-blue-active"
                                                Text="Add New" OnClick="btnadd_Click" />
                                             <asp:Label ID="lbup" runat="server" Visible="false"></asp:Label>
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
                                                            <asp:GridView ID="grd_Expense" runat="server" AutoGenerateColumns="False" DataKeyNames="DocID"
                                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="grd_Expense_PageIndexChanging"
                                                                CssClass="table table-bordered table-striped">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="DocID" HeaderText="DocID" Visible="false" />
                                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />

                                                                    <asp:BoundField DataField="PolicyName" HeaderText="Policy Name" />
                                                                    <asp:BoundField DataField="PolicyNumber" HeaderText="Policy Number" />
                                                                    <asp:BoundField DataField="Date" HeaderText="Policy Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" />

                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Imagedownloadfile" runat="server" CommandArgument='<%# Eval("DocID") %>'
                                                                                Height="20px" ImageUrl="~/Images/load.png" Width="20px" ToolTip="Download File" OnClick="Imagedownloadfile_Click" />

                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="70px" />
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
                                                                    Data Not Exists.........
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

                                <asp:Panel ID="pnl" runat="server" Height="800px" DefaultButton="btnsubmit">
                                    <section class="content-header">
                                        <h3>HR Policy Information </h3>

                                    </section>
                                    <section class="content">

                                        <!-- left column -->
                                        <div class="col-md-12" style="height: 960px;">

                                            <div class="box box-primary">

                                                <%-- <asp:Panel ID="PnlAdd" runat="server" Width="500px">--%>
                                                <br />
                                                <table width="100%">
                                                    <tr>
                                                        <td>Company Name:<asp:Label ID="lblcompanyid" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>Policy Date:
                                                        </td>
                                                        <td>Policy Name
                                                        </td>
                                                        <td>Policy Number
                                                        </td>
                                                        <td>Document Name
                                                        </td>
                                                        <td>Upload Document
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCompany" runat="server" Width="250px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;
                                                            <asp:TextBox ID="txtpolicyDate" runat="server" CausesValidation="True" CssClass="form-control"
                                                                ValidationGroup="w"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtexpensedate_CalendarExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtpolicyDate">
                                                            </asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtpolicyDate" ErrorMessage="*" ForeColor="Red"
                                                                ValidationGroup="w"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                            <asp:TextBox ID="txtpolicyname" runat="server" CssClass="form-control" CausesValidation="True"
                                                                ValidationGroup="S" MaxLength="25"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                ControlToValidate="txtpolicyname" ErrorMessage="*" ForeColor="Red"
                                                                ValidationGroup="S"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                            <asp:TextBox ID="txtpolicyNumber" runat="server" CssClass="form-control" CausesValidation="True"
                                                                ValidationGroup="S" MaxLength="20"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtpolicyNumber" ErrorMessage="*" ForeColor="Red"
                                                                ValidationGroup="S"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                            <asp:TextBox ID="txtdocname" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filter" runat="server" TargetControlID="txtdocname" Enabled="true" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ."></asp:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator14" runat="server"
                                                                ControlToValidate="txtdocname" ErrorMessage="*"
                                                                SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                            <asp:FileUpload ID="FileUploadDocu" runat="server" CssClass="form-control" Width="230px" />
                                                            <asp:Label ID="lblpath" runat="server"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td>&nbsp;
                                                            <asp:ImageButton ID="ImgCitAdd" runat="server" ImageUrl="~/Images/i_add.png" Style="height: 16px" ToolTip="Add" ValidationGroup="S" OnClick="ImgCitAdd_Click" />

                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="grdnewdoc" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                                                                    CssClass="table table-bordered table-striped" PageSize="10">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNo" runat="server" Text="<%# Container.DataItemIndex + 1  %>" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" Visible="false" />
                                                                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Policy Date" />
                                                                        <asp:BoundField DataField="PolicyName" HeaderText="Policy Name" />
                                                                        <asp:BoundField DataField="PolicyNumber" HeaderText="Policy Number" />
                                                                        <asp:BoundField DataField="DocName" HeaderText="Document Name" />
                                                                        <asp:BoundField DataField="DocPath" HeaderText="Document Path" />
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <center>
                                                                                         <asp:ImageButton ID="btnedit" runat="server" BorderStyle="None" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                                           CommandName="cmdEdit" Height="20px" ImageUrl="~/Images/i_edit.png" ToolTip="Edit" OnClick="btnedit_Click" 
                                                                                            />

                                                                                           <asp:ImageButton ID="btndelet" runat="server" BorderStyle="None"
                                                                                            CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="cmdEdit" Height="20px"
                                                                                            ImageUrl="~/Images/i_delete.png" ToolTip="Delete" OnClick="btndelet_Click"   /></center>
                                                                            </ItemTemplate>
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
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">


                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <div style="margin-left: 470px">
                                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" ValidationGroup="w"
                                                                        CssClass="btn bg-blue-active" OnClick="btnsubmit_Click" />
                                                                    &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel"
                                                                    CssClass="btn bg-blue-active" OnClick="btncancel_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </table>

                                            </div>
                                    </section>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        
</asp:Content>

