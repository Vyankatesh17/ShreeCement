<%@ Page Title="Advance" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="Advance.aspx.cs" Inherits="Advance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updd" runat="server">
        <ContentTemplate>

            <table width="100%">
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
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
                                                <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>
                                                <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pann" runat="server">
                                            <asp:GridView ID="grdAdvance" runat="server" AutoGenerateColumns="False" BorderStyle="None" PagerStyle-CssClass="paging"
                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-striped table-bordered bootstrap-datatable datatable"
                                                Width="100%" AllowPaging="true" PageSize="10" OnPageIndexChanging="grd_Company_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Name" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="EnterDate" HeaderText="Enter Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"  />
                                                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <asp:BoundField DataField="ManagerStatus" HeaderText="Manager Status"  />
                                                    <asp:BoundField DataField="DepartmentHeadStatus" HeaderText="Department Head Status"  />
                                                    <asp:BoundField DataField="HRStatus" HeaderText="HR Status"  />
                                                </Columns>
                                                <FooterStyle />
                                                <HeaderStyle />
                                                <PagerStyle HorizontalAlign="Right" CssClass="BottompagerRowStyle" />
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
                                    <h3>Advance Information </h3>

                                </section>
                                <section class="content">

                                    <!-- left column -->
                                    <div class="col-md-7">

                                        <div class="box box-primary">
                                            <table width="80%">
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            Date:<span style="color:red;">*</span>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-group">
                                                            &nbsp;
                                                            <asp:TextBox CssClass="form-control" Width="270px" ID="txtDate" runat="server" ReadOnly="true"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtDate" ValidChars="0123456789/">
                                                            </asp:FilteredTextBoxExtender>
                                                            <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True"
                                                                TargetControlID="txtDate">
                                                            </asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                                                Display="None" ErrorMessage="Select Date" SetFocusOnError="True"
                                                                ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                            </asp:ValidatorCalloutExtender>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            Amount:<span style="color:red;">*</span>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtamount" Width="270px" placeholder="Enter Amount" CssClass="form-control" runat="server" AutoPostBack="true"
                                                                OnTextChanged="txtamount_TextChanged"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="fmtid" runat="server" FilterType="Custom" ValidChars="0123456789."
                                                                TargetControlID="txtamount">
                                                            </asp:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtamount"
                                                                Display="None" ErrorMessage="Enter Amount" SetFocusOnError="True"
                                                                ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                            </asp:ValidatorCalloutExtender>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            Remarks:
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtremarks" placeholder="Enter Remark"  Width="270px" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            Deduction in(Months):
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtdeduction" Width="270px" runat="server" CssClass="form-control" placeholder="Enter Deduction in Months"
                                                                OnTextChanged="txtdeduction_TextChanged"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom" ValidChars="0123456789"
                                                                TargetControlID="txtdeduction">
                                                            </asp:FilteredTextBoxExtender>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            Due Date:<span style="color:red;">*</span>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtduedate" runat="server" CssClass="form-control" Width="270px" ReadOnly="true"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="txtduedate_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtduedate" ValidChars="0123456789/">
                                                            </asp:FilteredTextBoxExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                                TargetControlID="txtduedate" PopupPosition="TopLeft">
                                                            </asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtduedate"
                                                                Display="None" ErrorMessage="Select Due Date" SetFocusOnError="True"
                                                                ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2"
                                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                                            </asp:ValidatorCalloutExtender>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                     <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                    </td>
                                                    <td>
                                               
                                                <div style="margin-left:30px">
                                                     <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                                CssClass="btn bg-blue-active" />
                                                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                CssClass="btn bg-blue-active" />
                                                </div>
                                                        </td>
                                                    </tr>
                                            </table>
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
