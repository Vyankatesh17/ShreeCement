<%@ Page Title="Passport" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Passport.aspx.cs" Inherits="Passport" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">





    <table width="100%">

        <tr>
            <td>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">





                        <table align="center" width="100%">
                            <tr>
                                <td colspan="5">
                                    <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                        Text="Add Passport" />
                                </td>
                            </tr>
                            <table align="right" width="70%">
                                <tr>
                                    <td>Sort By</td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                            <asp:ListItem>Company-Wise</asp:ListItem>
                                            <asp:ListItem>Employee-Wise</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server"  OnClick="btnSearch_Click" Text="Search" ValidationGroup="a" CssClass="btn bg-blue-active" />
                                    </td>
                                </tr>
                                <tr id="trData" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="lblSearch" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSort" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        &nbsp;<asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="ddlSort" Display="Dynamic" ErrorMessage="Select Search Criteria" Font-Size="9pt" ForeColor="Red" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>

                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>


                            <tr>
                                <td align="right" colspan="5">
                                    <b>
                                        <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                        </asp:Label>
                                        &nbsp;&nbsp;
                                                <asp:Label ID="lblcnt" runat="server">0</asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:GridView ID="grdData" runat="server" AllowPaging="true" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table table-bordered table-striped" ForeColor="Black" GridLines="Vertical" OnPageIndexChanging="grdData_PageIndexChanging" PageSize="10">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="PassportNo" HeaderText="Passport No" />
                                            <asp:BoundField DataField="IssueDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Issue Date" />
                                            <asp:BoundField DataField="ExpiryDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Expiry Date" />
                                            <asp:BoundField DataField="NameinPassport" HeaderText="Name on Passport " />
                                            <asp:BoundField DataField="CountryName" HeaderText="Country" />
                                            <asp:BoundField DataField="Address" HeaderText="Address" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgview" runat="server" ImageUrl="~/Images/View.png" CommandArgument='<%# Eval("PassportID") %>'
                                                        OnClick="imgview_Click" Height="20" Width="20" CausesValidation="False" ToolTip="View Details"/>
                                                </ItemTemplate>
                                                <ControlStyle Font-Size="11pt" ForeColor="#3399FF" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>



                                        </Columns>
                                        <EmptyDataTemplate>
                                            No Record Exists.........!!
                                        </EmptyDataTemplate>
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
                    </asp:View>
                    <asp:View ID="View2" runat="server">

                        <section class="content-header">
                            <h3>Passport Information </h3>

                        </section>

                        <section class="content">
                            <!-- left column -->
                            <div class="col-md-12">
                                <div class="box box-primary">

                                    <table align="center" cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="PnlAdd" runat="server" Width="100%">
                                                    <table cellpadding="10" cellspacing="2" width="80%">
                                                        <tr>
                                                            <td>Company
                                                                <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" Width="60%" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCompany" Display="Dynamic" ErrorMessage="Select Company" Font-Size="9pt" ForeColor="Red" ValidationGroup="S" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>Country
                                                                <asp:Label ID="Label6" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlContry" runat="server" CssClass="form-control" Width="80%" >
                                                                </asp:DropDownList>
                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlContry" Display="Dynamic" ErrorMessage="Select Country" Font-Size="9pt" ForeColor="Red" ValidationGroup="S" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Employee
                                                                <asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlEmp" runat="server" Width="60%" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlEmp" Display="Dynamic" ErrorMessage="Select Employee" Font-Size="9pt" ForeColor="Red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>Place of Birth</td>
                                                            <td>
                                                                <asp:TextBox ID="txtBirthPlace" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtBirthPlace_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtBirthPlace" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ ">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Name in Passport
                                                                <asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtName_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtName" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789">
                                                                </asp:FilteredTextBoxExtender>
                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Enter Name" Font-Size="9pt" ForeColor="Red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>Issue Date
                                                                <asp:Label ID="Label7" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtIssueDate" runat="server" OnTextChanged="txtPassportNo_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtIssueDate_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtIssueDate" ValidChars="0123456789/">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:CalendarExtender ID="txtIssueDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtIssueDate">
                                                                </asp:CalendarExtender>
                                                                &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtIssueDate" Display="Dynamic" ErrorMessage="Invalid Date Format" Font-Size="9pt" ForeColor="Red" Operator="DataTypeCheck" Type="Date" ValidationGroup="S"></asp:CompareValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtIssueDate" Display="Dynamic" ErrorMessage="Enter Issue Date" Font-Size="9pt" ForeColor="Red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Passport Number
                                                                <asp:Label ID="Label5" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPassportNo" runat="server" OnTextChanged="txtPassportNo_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtPassportNo_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtPassportNo" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789">
                                                                </asp:FilteredTextBoxExtender>
                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPassportNo" Display="Dynamic" ErrorMessage="Enter Passport No" Font-Size="9pt" ForeColor="Red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>Expiry Date
                                                                <asp:Label ID="Label8" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExpiryDate" runat="server" AutoPostBack="True" OnTextChanged="txtPassportNo_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtExpiryDate_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtExpiryDate" ValidChars="0123456789/">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtExpiryDate">
                                                                </asp:CalendarExtender>
                                                                &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtExpiryDate" Display="Dynamic" ErrorMessage="Invalid Date Format" Font-Size="9pt" ForeColor="Red" Operator="DataTypeCheck" Type="Date" ValidationGroup="S"></asp:CompareValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtExpiryDate" Display="Dynamic" ErrorMessage="Enter Issue Date" Font-Size="9pt" ForeColor="Red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Place of Issue</td>
                                                            <td>
                                                                <asp:TextBox ID="txtPlaceIssue" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtPlaceIssue_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtPlaceIssue" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>Address
                                                                <asp:Label ID="Label9" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="Enter Address" Font-Size="9pt" ForeColor="Red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;
                                                            
                                                            </td>

                                                            <td colspan="3">&nbsp;</td>

                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <fieldset>
                                                                    <legend>Document Details </legend>
                                                                    <table width="70%" cellpadding="10" cellspacing="10">
                                                                        <tr align="center">
                                                                            <td align="left">&nbsp;&nbsp;&nbsp; Document Name</td>
                                                                            <td align="left">&nbsp;&nbsp; Select File</td>
                                                                            <td align="left">File Name</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqcole" runat="server" ControlToValidate="txtDocName"
                                                                                    Display="Dynamic" ErrorMessage="Enter Document Name" SetFocusOnError="True" ValidationGroup="b" Font-Size="10pt" ForeColor="Red"></asp:RequiredFieldValidator>

                                                                            </td>
                                                                            <td>
                                                                                <asp:FileUpload ID="FUDoc" runat="server" accept="image/*" EnableTheming="True" CssClass="form-control"
                                                                                    onchange=" this.form.submit();" /></td>
                                                                            <td>

                                                                                <asp:Label ID="lblAttachPath" runat="server"></asp:Label>

                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnaddEdu" runat="server" Text="ADD" CssClass="btn bg-blue-active" ValidationGroup="b"
                                                                                    OnClick="btnaddEdu_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td align="left" colspan="3">
                                                                                <asp:GridView ID="grd" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" Width="100%">
                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="DocName" HeaderText="DocName" />
                                                                                        <asp:BoundField DataField="FileName" HeaderText="FileName" />
                                                                                        <asp:TemplateField HeaderText="Action">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("DocName") %>'
                                                                                                    Height="15px" ImageUrl="~/Images/i_edit.png" OnClick="imgedit_Click" Width="15px" />
                                                                                                <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%# Eval("DocName") %>'
                                                                                                    Height="15px" ImageUrl="~/Images/i_delete.png" OnClick="imgdelete_Click" Width="15px" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EmptyDataTemplate>
                                                                                        No Record Exists.........!!
                                                                                    </EmptyDataTemplate>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>

                                                                    </table>
                                                                </fieldset>
                                                            </td>
                                                        </tr>

                                                    </table>

                                                </asp:Panel>
                                            </td>
                                        </tr>

                                        <tr align="center">
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn bg-blue-active" OnClick="btnsubmit_Click" TabIndex="9" Text="Save" ValidationGroup="S" />
                                                &nbsp;
                                                                <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" OnClick="btncancel_Click" TabIndex="10" Text="Cancel" />
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

    <uc1:Time ID="modpop" runat="server" />



</asp:Content>

