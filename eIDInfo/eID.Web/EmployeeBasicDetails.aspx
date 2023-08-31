<%@ Page Title="Employee Basic Info" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="EmployeeBasicDetails.aspx.cs" Inherits="EmployeeBasicDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table width="100%">

        <tr>
            <td>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="97%">
     
                            <tr>
                                <td style="padding-left: 15px">
                                    <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                        Text="Add New" />
                                </td>

                            </tr>
                        </table>
                        <br />
                        <asp:Panel ID="pnlhd" runat="server" DefaultButton="btnSearch">
                        <table>
                            <tr>
                                <td colspan="5" style="padding-left: 15px">&nbsp;&nbsp;Search By :&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsearch" runat="server" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlsearch_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Employee Name-Wise</asp:ListItem>
                                        <asp:ListItem>Email ID-Wise</asp:ListItem>

                                    </asp:DropDownList>
                                </td>


                                <td>
                                    <asp:TextBox ID="txtempsearch" runat="server" Visible="false" Width="250px" CssClass="form-control"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtempsearch"
                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetEmployeeName"
                                        UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                    <asp:FilteredTextBoxExtender ID="txtempsearch_FilteredTextBoxExtender"
                                        runat="server" Enabled="True" TargetControlID="txtempsearch"
                                        ValidChars="ab cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtemailidsearch" runat="server" Visible="false" CssClass="form-control" Width="250px"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtemailidsearch"
                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetEmailID"
                                        UseContextKey="True">
                                    </asp:AutoCompleteExtender>

                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn bg-blue-active"
                                        OnClick="btnSearch_Click" UseSubmitBehavior="False" />
                                </td>
                            </tr>
                        </table>
                            </asp:Panel>
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
                                                    <asp:GridView ID="grd_Emp" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                        CssClass="table table-bordered table-striped" OnPageIndexChanging="grd_Dept_PageIndexChanging" AllowPaging="true" PageSize="10">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id" Visible="false" />
                                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                            <asp:BoundField DataField="DOJ" HeaderText="Date of Joining" DataFormatString="{0:MM/dd/yyyy}" />
                                                            <asp:BoundField DataField="BirthDate" HeaderText="Date of Birth" DataFormatString="{0:MM/dd/yyyy}" />
                                                            
                                                                    <asp:TemplateField HeaderText="Personal Email">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblemailidp" ReadOnly="true" runat="server" Text='<%# Eval("personalEmail") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                            <asp:BoundField DataField="EmailId" HeaderText="Email Id" />
                                                            <asp:BoundField DataField="UserType" HeaderText="User Type" />

                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Edit" runat="server" Text="Resend Code" OnClick="OnClick_Edit" CommandArgument='<%# Eval("EmployeeId") %>'
                                                                        CssClass="linkbutton1" />
                                                                </ItemTemplate>

                                                                 <%--<ItemTemplate>
                                                                            <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/Images/i_edit.png" CommandArgument='<%# Eval("EmployeeId") %>'
                                                                                OnClick="OnClick_Edit" ToolTip="Edit" />
                                                                        </ItemTemplate>--%>


                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle />
                                                        <HeaderStyle />
                                                        <PagerStyle HorizontalAlign="Right" />
                                                        <EmptyDataTemplate>No Data Exists.......</EmptyDataTemplate>
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                            <section class="content-header">
                                <h3>Register New  Employee</h3>

                            </section>
                            <section class="content">

                                <!-- left column -->
                                <div class="col-md-8">

                                    <div class="box box-primary">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2">Activation Code:</td>
                                                <td colspan="5">&nbsp; 
                                                        <asp:TextBox Text="" runat="server" ID="lblempCode" />
                                                    <asp:Label ID="lblempid" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Device Code:</td>
                                                <td colspan="5">&nbsp; 
                                                        <asp:TextBox Text="" runat="server" ID="txtDeviceCode" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Name:<span style="color: red;"> *</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlsalitude" runat="server" Width="80px" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlsalitude_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">Mr</asp:ListItem>
                                                        <asp:ListItem>Ms</asp:ListItem>
                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                        <asp:ListItem>Dr.</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <br />
                                                    <asp:TextBox ID="txtfname" runat="server" Width="150px" CssClass="form-control" MaxLength="40" placeholder="First Name"></asp:TextBox>

                                                   <%-- <asp:TextBoxWatermarkExtender ID="txtfname_TextBoxWatermarkExtender"
                                                        runat="server" Enabled="True" TargetControlID="txtfname"
                                                        WatermarkText="First Name">
                                                    </asp:TextBoxWatermarkExtender>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfname" Display="None" ErrorMessage="*" ForeColor="Red" ValidationGroup="a" SetFocusOnError="true"></asp:RequiredFieldValidator>


                                                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtfname"
                                                                                FilterType="Custom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789_,. -" Enabled="True">
                                                                            </asp:FilteredTextBoxExtender>
                                                    <%--<asp:ValidatorCalloutExtender ID="VC" runat="server" TargetControlID="RequiredFieldValidator2" Enabled="true"></asp:ValidatorCalloutExtender>--%>

                                                    <br />
                                                </td>
                                               
                                                <td>
                                                    <asp:TextBox ID="txtmname" runat="server" Width="150px" CssClass="form-control" MaxLength="40" placeholder="Middle Name"></asp:TextBox>
                                                   <%-- <asp:TextBoxWatermarkExtender ID="txtmname_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txtmname" WatermarkText="Middle Name">
                                                    </asp:TextBoxWatermarkExtender>--%>

                                                       <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtmname"
                                                                                FilterType="Custom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789_,. -" Enabled="True">
                                                                            </asp:FilteredTextBoxExtender>

                                                </td>
                                                <td>
                                                    <br />
                                                    <asp:TextBox ID="txtlname" runat="server" Width="150px" CssClass="form-control" MaxLength="40" placeholder="Last Name"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtlname" Display="None" ErrorMessage="Enter Last Name" ValidationGroup="a" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator3" Enabled="true"></asp:ValidatorCalloutExtender>--%>

                                                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtlname"
                                                                                FilterType="Custom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789_,. -" Enabled="True">
                                                                            </asp:FilteredTextBoxExtender>

                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Birth Date:
                                                </td>
                                                <td colspan="5">

                                                    <asp:TextBox ID="txtbirtdate" runat="server" Width="190px" CssClass="form-control" OnTextChanged="txtbirtdate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <br />
                                                    <asp:CalendarExtender ID="txtbirtdate_CalendarExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtbirtdate">
                                                    </asp:CalendarExtender>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Date Of Joining:</td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="txtdateofjoining" runat="server" Width="190px" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    <br />
                                                    <asp:CalendarExtender ID="txtdateofjoining_CalendarExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtdateofjoining">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Email ID:<span style="color: red;"> *</span></td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="txtEmail" runat="server" CausesValidation="True" Width="190px" CssClass="form-control"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtEmail" Display="None"
                                                        ErrorMessage="Enter Email Id"
                                                        ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>



                                                   <%-- <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                    </asp:ValidatorCalloutExtender>--%>



                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                        runat="server" ControlToValidate="txtEmail"
                                                        Display="None"
                                                        ErrorMessage="Enter Email Id in proper format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="a"
                                                        SetFocusOnError="True"></asp:RegularExpressionValidator>




                                                    <asp:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender"
                                                        runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                                                    </asp:ValidatorCalloutExtender>



                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">User Type:<span style="color: red;"> *</span> </td>
                                                <td colspan="5">
                                                    <asp:DropDownList ID="ddusertype" runat="server" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddusertype_SelectedIndexChanged" Width="190px">
                                                        <asp:ListItem Selected="true" Value="--Select--" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Admin" Text="Admin"></asp:ListItem>
                                                        <asp:ListItem Value="Employee" Text="Employee"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server"
                                                        ControlToValidate="ddusertype" Display="None" ErrorMessage="Select UserType"
                                                        Operator="NotEqual" ValidationGroup="a"
                                                        ValueToCompare="--Select--" SetFocusOnError="True"></asp:CompareValidator>
                                                    <asp:ValidatorCalloutExtender ID="CompareValidator2_ValidatorCalloutExtender"
                                                        runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                                    </asp:ValidatorCalloutExtender>



                                                </td>
                                            </tr>



                                            <tr>
                                                <td colspan="2">Gender:&nbsp;
                                                </td>
                                                <td colspan="5">

                                                    <asp:RadioButtonList ID="RbGender" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="0">Male</asp:ListItem>
                                                        <asp:ListItem Value="1">Female</asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </td>
                                            </tr>
                                            <caption>
                                                <br />
                                                <tr>
                                                    <td colspan="2" style="margin-left: 340px"></td>
                                                    <td colspan="5">
                                                        <br />
                                                        <asp:Button ID="btnsubmit" runat="server" CssClass="btn bg-blue-active" OnClick="btnsubmit_Click" Text="Submit" ValidationGroup="a" Width="120px" />
                                                        <asp:Button ID="btncancel" runat="server" CausesValidation="False" CssClass="btn bg-blue-active" OnClick="btncancel_Click" Text="Cancel" Width="120px" />
                                                    </td>
                                                </tr>
                                            </caption>


                                        </table>
                                    </div>
                                </div>
                            </section>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
   <%-- <uc1:Time ID="modpop" runat="server" />--%>

</asp:Content>

