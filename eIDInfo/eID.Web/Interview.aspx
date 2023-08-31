<%@ Page Title="Interview" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Interview.aspx.cs" Inherits="Interview" %>

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
                                            <td>
                                                <asp:Button ID="btnadd" runat="server" Text="Add New" OnClick="btnadd_Click" CssClass="btn bg-blue-active" />







                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <b>
                                                    <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                                    </asp:Label>&nbsp;
                                            <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grd_Interview" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped" Width="100%" OnPageIndexChanging="grd_Interview_PageIndexChanging" AllowPaging="true" PageSize="10">
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="InterviewDate" HeaderText="Interview Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />

                                                        <asp:BoundField DataField="DeptName" HeaderText="Department Name" />
                                                        <asp:BoundField DataField="DesigName" HeaderText="Designation Name" />
                                                        <asp:BoundField DataField="CandidateName" HeaderText="Interviewer's Name" />
                                                        <asp:BoundField DataField="Mobile" HeaderText="Mobile No" />

                                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                                        <%--  <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton CommandArgument='<%#Eval("InterviewerID") %>' runat="server" ImageUrl="~/Images/a.png" ID="btnselect" OnClick="btnselect_Click" Height="50px" ToolTip="Go For Interview Status"  />
                            </ItemTemplate>
                        </asp:TemplateField>--%>


                                                        <%--                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Edit" runat="server" Text="Edit" OnClick="OnClick_Edit" CommandArgument='<%# Eval("InterviewerID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No Record Exists.........!!

                                                    </EmptyDataTemplate>
                                                    <PagerStyle HorizontalAlign="Right" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View2" runat="server">

                                    <asp:Panel ID="PnlAdd" runat="server" DefaultButton="btnsubmit">
                                        <section class="content-header">
                                            <h3>Interview Information </h3>
                                        </section>

                                        <section class="content">
                                            <%-- <div class="row">--%>
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:UpdatePanel ID="upp1" runat="server">
                                                                    <ContentTemplate>

                                                                        <div class="col-md-6">
                                                                            <div class="box box-primary">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td>Interview Date</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="lblDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            <asp:FilteredTextBoxExtender ID="lblDate_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="lblDate" ValidChars="0123456789/">
                                                                                            </asp:FilteredTextBoxExtender>
                                                                                            <asp:CalendarExtender ID="lblDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="lblDate">
                                                                                            </asp:CalendarExtender>
                                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="lblDate" ErrorMessage="Invalid Date Format" ForeColor="Red" Operator="DataTypeCheck" Type="Date" ValidationGroup="S"></asp:CompareValidator>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Select Company :
                                                                <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                                                        </td>
                                                                                        <td><asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" TabIndex="1">
                                                                                        </asp:DropDownList>
                                                                                            <asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="ddlCompany" ErrorMessage="Select Company" ForeColor="Red" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" Display="Dynamic"></asp:CompareValidator>
                                                                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Company" ControlToValidate="ddlCompany" ForeColor="Red" ValidationGroup="S" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Interviewer&#39;s Name :
                                                                <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtName" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                                                                            <asp:FilteredTextBoxExtender ID="txtName_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtName" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                                            </asp:FilteredTextBoxExtender>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Enter Interviewer's Name" ForeColor="Red" ValidationGroup="S" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Department&nbsp; :
                                                                <asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                                                        </td>
                                                                                        <td>&nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddldept" runat="server" DataTextField="DeptName" CssClass="form-control" DataValueField="DeptID" OnSelectedIndexChanged="ddldept_SelectedIndexChanged" AutoPostBack="True" TabIndex="3">
                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddldept" ErrorMessage="Select Department" ForeColor="Red" ValidationGroup="S" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ID="cmp3" runat="server" ControlToValidate="ddldept" ErrorMessage="Select Department" ForeColor="Red" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" Display="Dynamic"></asp:CompareValidator>
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Mobile No :
                                                                <asp:Label ID="Label5" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtMob" runat="server" MaxLength="10" TabIndex="4"  CssClass="form-control"></asp:TextBox>
                                                                                            <asp:FilteredTextBoxExtender ID="txtMob_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtMob" ValidChars="0123456789">
                                                                                            </asp:FilteredTextBoxExtender>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMob" Display="Dynamic" ErrorMessage="In-Valid Mobile" ForeColor="Red" ValidationExpression="^[0-9]{10}$" ValidationGroup="S"></asp:RegularExpressionValidator>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMob" ErrorMessage="Enter Interviewer's Mobile" ForeColor="Red" ValidationGroup="S" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6">
                                                                            <div class="box box-primary">

                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td>Position :<asp:Label ID="lblid" runat="server" Visible="False"></asp:Label>
                                                                                            &nbsp;<asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                                                                        </td>
                                                                                        <td>&nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddldesign" runat="server" DataTextField="DeptName" CssClass="form-control"
                                                                DataValueField="DeptID" TabIndex="5">
                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddldesign" ErrorMessage="Select Position" ForeColor="Red" ValidationGroup="S" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddldesign" ErrorMessage="Select Position" Operator="NotEqual"
                                                                                                ValidationGroup="S" ValueToCompare="--Select--" ForeColor="Red" Display="Dynamic"></asp:CompareValidator>

                                                                                            &nbsp;&nbsp;

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Address :</td>
                                                                                        <td>
                                                                <asp:TextBox ID="txtAddr" runat="server" TabIndex="6" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Reference By :</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtRefBy" runat="server" TabIndex="7" CssClass="form-control"></asp:TextBox>
                                                                                            <asp:FilteredTextBoxExtender ID="txtRefBy_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtRefBy" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                                            </asp:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Email ID :</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtEmail" runat="server" TabIndex="8" CssClass="form-control"></asp:TextBox>
                                                                                            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="In-Valid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="S" Display="Dynamic"></asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td></td>
                                                                                        <td>
                                                                                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn bg-blue-active" OnClick="btnsubmit_Click" Text="Save" ValidationGroup="S" TabIndex="9" />
                                                                                            &nbsp;
                                                            <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" OnClick="btncancel_Click" Text="Cancel" TabIndex="10" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>


                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                            </div>

                                            <%-- </div>--%>
                                        </section>
                                    </asp:Panel>

                                </asp:View>



                            </asp:MultiView>
                        </td>
                    </tr>
                </table>
            
            <uc1:Time ID="modpop" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

