<%@ Page Title="Advance Approve" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="AdvanceApproved.aspx.cs" Inherits="AdvanceApproved" %>


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

    <asp:UpdatePanel ID="updtpanel" runat="server">
        <ContentTemplate>



    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
    
            <section class="content">
                <%-- <div class="row">--%>
                <div class="box-body">
                    <div class="form-group">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="upp1" runat="server">
                                        <ContentTemplate>

                                            <div class="col-md-12">
                                                <div class="box box-primary">
                                                    <table width="30%" align="center">
                                                        <tr align="center">
                                                            <td align="left">
                                                                <div class="form-group">Status&nbsp;&nbsp; </div>
                                                            </td>

                                                            <td align="right">
                                                                <div class="form-group">
                                                                    <div class="input-group">
                                                                        <asp:DropDownList ID="rdpending" runat="server" Width="250px" AutoPostBack="True"
                                                                            CssClass="form-control" OnSelectedIndexChanged="rdpending_CheckedChanged">
                                                                            <asp:ListItem Text="Pending" />
                                                                            <asp:ListItem Text="Approved" />
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </td>

                                                            <%--  <asp:RadioButton ID="rdpending" runat="server" AutoPostBack="True" GroupName="a"
                                Text="Pending" oncheckedchanged="" />--%>

                                                            <%--   <td align="left">
                         <asp:RadioButton ID="rbApproved" runat="server" AutoPostBack="True" GroupName="a"
                                 Text="Approved" oncheckedchanged="rbApproved_CheckedChanged" />
                        </td>--%>
                                                        </tr>
                                                    </table>

                                                    <div style="margin-left: 950px">
                                                      <b>  <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>
                                                        <asp:Label ID="lblcnt" runat="server"></asp:Label></b>
                                                    </div>

                                                    <asp:Panel ID="pann" runat="server">
                                                        <asp:GridView ID="grdadvance" runat="server" BorderStyle="None" AutoGenerateColumns="false"
                                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowPaging="true" PageSize="10"
                                                            CssClass="table table-striped table-bordered bootstrap-datatable datatable">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="AdvanceId" HeaderText="AdvanceId" Visible="false" />
                                                                <asp:BoundField DataField="Name" HeaderText="Employee Name" />
                                                                <asp:BoundField DataField="EnterDate" HeaderText="Enter Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                                <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="ManagerStatus" HeaderText="Manager Status" />
                                                                <asp:BoundField DataField="DepartmentHeadStatus" HeaderText="Department Head Status" />
                                                                <asp:BoundField DataField="HRStatus" HeaderText="HR Status" />

                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkApproved" runat="server" Text="Approved" CommandArgument='<%# Eval("AdvanceId") %>'
                                                                            OnClick="lnkApproved_Click" />
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

                <%-- </div>--%>
            </section>
        </asp:View>

        <asp:View ID="View2" runat="server">
            <table>
            </table>
        </asp:View>


    </asp:MultiView>

    <%--<uc1:Time ID="modpop" runat="server" />--%>
    <table width="600px" id="tblpopup" class="modalPopup" style="display: none; background-color: Silver;"
        runat="server">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="center" style="font-family: 'COMic Sans MS'; font-size: large;">
                            <div style="text-align: right; height: 30px; float: right;">
                                <asp:ImageButton ID="imgclose" runat="server" ImageUrl="~/Images/Close.jpg" Height="30px"
                                    Width="30px" />
                            </div>
                            Advance Approval Details
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 500px; background-color: White;">
                                <table width="100%">
                                    <tr>
                                        <td>Previous Details : 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:DetailsView ID="dtprevdetails" runat="server" Width="500px" BackColor="White" AutoGenerateRows="false">
                                                <Fields>
                                                    <asp:BoundField HeaderText="Date" DataField="Date" />
                                                    <asp:BoundField HeaderText="Employee Name" DataField="Name" />
                                                    <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                                    <asp:BoundField DataField="DeductionMonths" HeaderText="Deduction Month" />
                                                    <asp:BoundField HeaderText="DueDate" DataField="DueDate" />
                                                    <asp:BoundField DataField="ApprovedName" HeaderText="Approved by" />
                                                    <asp:BoundField DataField="Approvedamt" HeaderText="Approvedamt" />

                                                </Fields>
                                            </asp:DetailsView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Add Details
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Date :
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
                                        <td>Amount :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtamount" runat="server" OnTextChanged="txtamount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="fmtid" runat="server" FilterType="Custom" ValidChars="0123456789."
                                                TargetControlID="txtamount">
                                            </asp:FilteredTextBoxExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtamount"
                                                Display="None" ErrorMessage="Enter amount" SetFocusOnError="True"
                                                ValidationGroup="S"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>Remarks :
                                        </td>
                                        <td>                                                <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Due Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtduedate" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtduedate">
                                            </asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtduedate"
                                                Display="None" ErrorMessage="Select Due Date" SetFocusOnError="True"
                                                ValidationGroup="S"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2"
                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Deduction in(Months) :
                                        </td>
                                        <td>&nbsp;&nbsp;
                                                <asp:TextBox ID="txtdeduction" runat="server" OnTextChanged="txtdeduction_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom" ValidChars="0123456789"
                                                TargetControlID="txtdeduction">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>



                                    <tr>
                                        <td colspan="2">
                                            <table width="100%" id="t1" runat="server">
                                                <%--    <tr>
                                           <td>
                                                    &nbsp;&nbsp;&nbsp;Select Month
                                                    <br />
                                                    <asp:DropDownList Width="155px" ID="ddlMonths" runat="server" >
                                                     
                                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                                        <asp:ListItem Value="7">Jul</asp:ListItem>
                                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                                        <asp:ListItem Value="9">Sep</asp:ListItem>                                                     
                                                        <asp:ListItem Value="10">Oct</asp:ListItem>                                                
                                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;Select Year
                                                    <br />
                                                    <asp:DropDownList Width="155px" ID="ddlYears" runat="server" >
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                &nbsp;&nbsp;&nbsp;Amount
                                                    <br />
                                                   <asp:TextBox ID="txtreceived" AutoPostBack="true" Width="50px" Text="0" runat="server"  OnTextChanged="On_Click_QtyCal"
                                                      ></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="txtreceived_FilteredTextBoxExtende111r" runat="server"
                                                        Enabled="True" TargetControlID="txtreceived" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    
                                                </td>
                                                <td>
                                                    <div style="margin-top: 15px;">
                                                        <asp:Button ID="btnadd" runat="server" CssClass="btn bg-blue-active"  Text="Add" 
                                                            onclick="btnadd_Click" />
                                                    </div>
                                                </td>
                                      <td>
                                      
                                      </td>
                                      </tr>--%>
                                                <tr>
                                                    <td colspan="4">
                                                        <div style="overflow: scroll; height: 150px;">
                                                            <asp:GridView ID="grdpayment" runat="server" AutoGenerateColumns="false"
                                                                CssClass="table table-striped table-bordered bootstrap-datatable datatable">
                                                                <Columns>
                                                                    <asp:BoundField DataField="mid" HeaderText="MonthID" />
                                                                    <asp:BoundField DataField="mname" HeaderText="MonthName" />
                                                                    <asp:BoundField DataField="Year" HeaderText="Year" />
                                                                    <%--  <asp:BoundField DataField="Amount" HeaderText="Amount" />--%>


                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtreceived" AutoPostBack="true" Width="50px" Enabled="false" Text='<%# Eval("Amount") %>' runat="server" OnTextChanged="On_Click_QtyCal"></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender ID="txtreceived_FilteredTextBoxExtende111r" runat="server"
                                                                                Enabled="True" TargetControlID="txtreceived" ValidChars="0123456789.">
                                                                            </asp:FilteredTextBoxExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>



                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                CssClass="btn bg-blue-active" />
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

               </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


