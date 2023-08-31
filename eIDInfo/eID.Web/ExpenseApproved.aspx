<%@ Page Title="Approve Expense" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="ExpenseApproved.aspx.cs" Inherits="AdvanceApproved" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
           
                <div class="box-body">
                    <div class="form-group">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="upp1" runat="server">
                                        <ContentTemplate>

                                            <div class="col-md-12">
                                                <div class="box box-primary">
                <table width="15%" align="center">
                    <tr align="center" ">
                        <td align="left"><div class="form-group"> Status&nbsp;&nbsp; </div>  </td>
                        
                        <td align="right"><div class="form-group"><div class="input-group">
                            <asp:DropDownList ID="rdpending" runat="server" Width="250px" AutoPostBack="True" 
                                CssClass="form-control" OnSelectedIndexChanged="rdpending_CheckedChanged">
                                <asp:ListItem Text="Pending" />
                                <asp:ListItem Text="Approved" />
                            </asp:DropDownList> </div></div> </td>
                        
                       <%--  <td align="right">
                            <asp:RadioButton ID="rdpending" runat="server" AutoPostBack="True" GroupName="a"
                                Text="Pending" OnCheckedChanged="rdpending_CheckedChanged" />
                        </td>
                        <td align="left">
                            <asp:RadioButton ID="rbApproved" runat="server" AutoPostBack="True" GroupName="a"
                                Text="Approved" OnCheckedChanged="rbApproved_CheckedChanged" />  </td>--%>
                      
                    </tr>
                       </table>
                   <asp:Label ID="lbup" runat="server" Visible="false"></asp:Label>
                        <asp:Panel ID="pann" runat="server">
                             <div style="margin-left: 940px">
                                            <b>
                                                <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>
                                                <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                        </div>
                       <asp:GridView ID="grdadvance" runat="server" BorderStyle="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-striped table-bordered bootstrap-datatable datatable">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No.">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpensedetailID" HeaderText="ExpensedetailID" Visible="false" />
                        <asp:BoundField DataField="Name" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ManagerStatus" HeaderText="Manager Status" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DepartmentHeadStatus" HeaderText="Department Head Status"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="HRStatus" HeaderText="HR Status" ItemStyle-HorizontalAlign="Center" />
                        
                          <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <center>
                                                                    <asp:ImageButton ID="btnedit" runat="server" BorderStyle="None" CommandArgument='<%#Eval("ExpensedetailID")%>'
                                                                      CssClass="form-control"  CommandName="cmdEdit" Height="20px" ImageUrl="~/images/load.png" Width="20px"
                                                                        OnClick="btnedit_Click" />
                                                                </center>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkApproved" runat="server" Text="Approved" CommandArgument='<%# Eval("ExpensedetailID") %>'
                                    OnClick="lnkApproved_Click" /></ItemTemplate>
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
                        No data Exists!!!!!!!!!!!!!!!!
                    </EmptyDataTemplate>
                </asp:GridView>
                    </asp:Panel>
              </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>

             
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table>
                </table>
            </asp:View>
        </asp:MultiView>
    </fieldset>
    <uc1:Time ID="modpop" runat="server" />
    <table width="600px" id="tblpopup" class="modalPopup" style="display: none; background-color: Silver;"
        runat="server">
        <tr>
            <td>
                <table width="100%">
                   
                    <tr>
                        <td>
                            <div style="height: 500px; background-color: White;">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            Previous Details :
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:DetailsView ID="dtprevdetails" runat="server" Width="500px" BackColor="White"
                                                AutoGenerateRows="false" Visible="true">
                                                <Fields>
                                                    <asp:BoundField HeaderText="Date" DataField="Date" />
                                                    <asp:BoundField HeaderText="Employee Name" DataField="Name" />
                                                    <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                                    <asp:BoundField HeaderText="ExpenseType" DataField="ExpenseType" />
                                                    <asp:BoundField DataField="ApprovedName" HeaderText="Approved by" />
                                                    <asp:BoundField DataField="Approvedamt" HeaderText="Approvedamt" />
                                                </Fields>
                                            </asp:DetailsView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Add Details
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True"
                                                TargetControlID="txtDate">
                                            </asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                                Display="None" ErrorMessage="Select Application Date" SetFocusOnError="True"
                                                ValidationGroup="S"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Amount :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtamount" runat="server" OnTextChanged="txtamount_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="fmtid" runat="server" FilterType="Custom" ValidChars="0123456789."
                                                TargetControlID="txtamount">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtamount"
                                                Display="None" ErrorMessage="Enter amount" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                TargetControlID="RequiredFieldValidator2">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Remarks :
                                        </td>
                                        <td>                                           
                                            <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Approve Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtduedate" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtduedate">
                                            </asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtduedate"
                                                Display="None" ErrorMessage="Select Due Date" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                                TargetControlID="RequiredFieldValidator3">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                CssClass="btn bg-blue-active"  />
                                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                CssClass="btn bg-blue-active" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Label ID="lbl" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="mod" runat="server" TargetControlID="lbl" CancelControlID="imgclose"
        PopupControlID="tblpopup">
    </asp:ModalPopupExtender>
</asp:Content>
