<%@ Page Title="Salary Increment" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="SalaryIncrement.aspx.cs" Inherits="SalaryIncrement" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table width="100%">
                <tr>
                    <td style="padding-left: 18px">
                        <asp:Button ID="btnadd" runat="server" CssClass="btn bg-blue-active" Text="Add New" OnClick="btnadd_Click" />
                        <asp:Label ID="lblagencyidforsendmail" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblmailid" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblpassword" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblusername" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>

            </table>
            <br />
            <div class="col-md-12">
                <div class="box box-primary">

                    <div class="box-body">
                        <div class="form-group">
                            <asp:Panel ID="pnalset" runat="server" DefaultButton="btnsearch">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90px" valign="top">Search By :
                                        </td>
                                        <td style="width: 150px" valign="top">
                                            <asp:DropDownList ID="ddlsortby" runat="server" CssClass="form-control" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlsortby_SelectedIndexChanged">
                                                <asp:ListItem>ALL</asp:ListItem>
                                                <asp:ListItem>Employee Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <div runat="server" id="divEmpTextBoxSerach" visible="false">


                                            <td style="width: 100px" valign="top">&nbsp;
                                        
                                            </td>
                                            <td style="width: 350px" valign="top">
                                                <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txtagencywise_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                                    Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtEmployeeName"
                                                    CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetEmployeeList">
                                                </asp:AutoCompleteExtender>

                                            </td>
                                        </div>
                                        <td valign="top">
                                            <asp:Button ID="btnsearch" runat="server" Text="Search" ValidationGroup="b"
                                                CssClass="btn bg-blue-active" UseSubmitBehavior="False" OnClick="btnsearch_Click" />

                                            <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="b"
                                                CssClass="btn bg-blue-active" UseSubmitBehavior="False" OnClick="btnReset_Click" />

                                            <asp:Label ID="lblspecialitiID" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td align="right" colspan="5">
                                        <asp:Label ID="Label14" runat="server" Text="No. Of Count :" Font="Bold"></asp:Label>
                                        &nbsp;
                                    <asp:Label ID="lblcnt" runat="server" Font="Bold">0</asp:Label>
                                        <asp:Label ID="lblcandidateID" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblviewId" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="grdIncrement" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Name" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="PromotionIncrement" HeaderText="Promotion/Increment" />
                                                <asp:BoundField DataField="ActionDate" HeaderText="Action Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="WithEffectDate" HeaderText="With Effect Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="DeptName" HeaderText="Department" />
                                                <asp:BoundField DataField="DesigName" HeaderText="Designaction" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("SalPromId") %>' ImageUrl="~/Images/i_edit.png" OnClick="imgedit_Click" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                        <asp:Label runat="server" Visible="false" ID="EditId"></asp:Label>
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
                <h3>Salary Increment  Information </h3>
            </section>
            <section class="content">
                <div class="col-md-6">
                    <div class="box box-primary">

                        <%--<asp:UpdatePanel ID="upp1" runat="server">
                                        <ContentTemplate>--%>
                        <%-- <asp:Panel ID="pan" runat="server" DefaultButton="btnsave">--%>

                        <table width="100%">
                            <tr>
                                <td colspan="2">&nbsp;
                                </td>

                            </tr>
                            <tr>
                                <td valign="top">Promotion Status
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdIncrementStatus" runat="server"
                                        RepeatDirection="Horizontal" TabIndex="14">
                                        <asp:ListItem Text="Promotion" Value="0" Selected="True">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Increment" Value="1">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Both" Value="2">
                                        </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>

                            </tr>
                            <tr>
                                <td>Employee Name :
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlEmployee" CssClass="form-control" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="True" ErrorMessage="*" ForeColor="Red"
                                        ControlToValidate="ddlEmployee" ValidationGroup="S"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cmp5" runat="server" ControlToValidate="ddlEmployee" Display="Dynamic" ErrorMessage="*" ForeColor="Red" Operator="NotEqual" SetFocusOnError="true" ValidationGroup="S" ValueToCompare="--Select--"></asp:CompareValidator>
                                </td>
                            </tr>

                            <tr>
                                <td>Date:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtActionDate" CssClass="form-control"></asp:TextBox>

                                    <asp:CalendarExtender ID="txtbirtdate_CalendarExtender" runat="server"
                                        Enabled="True" TargetControlID="txtActionDate">
                                    </asp:CalendarExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True" ErrorMessage="*" ForeColor="Red"
                                        ControlToValidate="txtActionDate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td>With Effect Date:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEffectDate" CssClass="form-control"></asp:TextBox>

                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                        Enabled="True" TargetControlID="txtEffectDate">
                                    </asp:CalendarExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="True" ErrorMessage="*" ForeColor="Red"
                                        ControlToValidate="txtEffectDate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td>Grade :
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlGrade" CssClass="form-control"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True" ErrorMessage="*" ForeColor="Red"
                                        ControlToValidate="ddlGrade" ValidationGroup="S"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlGrade" Display="Dynamic" ErrorMessage="*" ForeColor="Red" Operator="NotEqual" SetFocusOnError="true" ValidationGroup="S" ValueToCompare="--Select--"></asp:CompareValidator>
                                </td>
                            </tr>

                            <tr>
                                <td>Department :
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="True" ErrorMessage="*" ForeColor="Red"
                                        ControlToValidate="ddlDepartment" ValidationGroup="S"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlDepartment" Display="Dynamic" ErrorMessage="*" ForeColor="Red" Operator="NotEqual" SetFocusOnError="true" ValidationGroup="S" ValueToCompare="--Select--"></asp:CompareValidator>
                                </td>
                            </tr>

                            <tr>
                                <td>Designation :
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlDesignation" CssClass="form-control"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" SetFocusOnError="True" ErrorMessage="*" ForeColor="Red"
                                        ControlToValidate="ddlDesignation" ValidationGroup="S"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlDesignation" Display="Dynamic" ErrorMessage="*" ForeColor="Red" Operator="NotEqual" SetFocusOnError="true" ValidationGroup="S" ValueToCompare="--Select--"></asp:CompareValidator>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lbLast" Visible="false" Text="Basic Salary:"></asp:Label>

                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbBasicSalary" Visible="false"></asp:Label>
                                </td>
                                
                            </tr>

                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnsubmit" runat="server" Text="Save"
                                        CssClass="btn bg-blue-active" TabIndex="8" OnClick="btnsubmit_Click"  ValidationGroup="S"/>
                                    &nbsp
                                                                <asp:Button ID="Button1" runat="server" Text="Cancel"
                                                                    CssClass="btn bg-blue-active" TabIndex="9" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>


                    </div>
                </div>
            </section>
        </asp:View>
    </asp:MultiView>


</asp:Content>

