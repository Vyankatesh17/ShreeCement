<%@ Page Title="Add Condidate" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="AddCandidateForEmployee.aspx.cs" Inherits="AddCandidateForEmployee" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updemp" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="chksame" />
            <asp:PostBackTrigger ControlID="btnuploadresume" />
        </Triggers>
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div class="col-md-12">
                        <div>
                            <div>
                                <div class="form-group">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnaddnew" runat="server" CssClass="btn bg-blue-active"
                                                    Text="Add New" OnClick="btnaddnew_Click" />
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Candidate List</legend>
                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label14" runat="server" Text="No. Of Count :" Font="Bold"></asp:Label>
                                                    &nbsp;
                                                  <asp:Label ID="lblcount" runat="server" Font="Bold">0</asp:Label>
                                                    <asp:Label ID="lblcandidateID" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdcandidatelist" runat="server" AutoGenerateColumns="false" 
                                                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                                                    CssClass="table table-bordered table-striped" Width="99%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Entry_Date" HeaderText="Entry Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField DataField="Vacancy" HeaderText="Vacancy" />
                                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                                            <asp:BoundField DataField="CandidateId_No" HeaderText="Candidate Id" />
                                                            <asp:BoundField DataField="DOB" HeaderText="DOB" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField DataField="Religion" HeaderText="Religion" />
                                                            <asp:BoundField DataField="Nationality" HeaderText="Nationality" />
                                                            <asp:BoundField DataField="Contact_No" HeaderText="Contact No." />
                                                            <asp:BoundField DataField="Email_Address" HeaderText="Email Id" />

                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageEditCanditList" runat="server" CommandArgument='<%# Eval("Candidate_ID") %>'
                                                                        Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="ImageEditCanditList_Click" />

                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Record Exist....!!!!!!!!!!!!!!
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>




                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>


                <asp:View ID="View2" runat="server">
                    <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Vacancy Info</legend>
                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td valign="top">Select Company:
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </td>
                                                <td>

                                                    <asp:DropDownList ID="ddlComp" runat="server" style="margin-left: 9px" AutoPostBack="True"  OnSelectedIndexChanged="ddlComp_SelectedIndexChanged" CssClass="form-control">
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                        ControlToValidate="ddlComp" Display="Dynamic"
                                                        ErrorMessage="Select Company" SetFocusOnError="True"
                                                        ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <br />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td valign="top">Vacancy :
                                                    <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlvaccancy" runat="server" style="margin-left: 9px" OnSelectedIndexChanged="ddlvaccancy_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlvaccancy" Display="Dynamic"
                                                        ErrorMessage="Select Vacancy" ForeColor="Red" InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="S">
                                                    </asp:RequiredFieldValidator>
                                                    <%-- <asp:CompareValidator ID="cmp3" runat="server" ControlToValidate="ddlvaccancy" Display="Static"
                                                        ForeColor="Red" ErrorMessage="Select Vacancy" Operator="NotEqual" ValidationGroup="a"
                                                        ValueToCompare="--Select--"></asp:CompareValidator>--%>
                                                    <br />


                                                </td>
                                            </tr>
                                            <tr id="trVcodedate" runat="server" visible="false">
                                                <td valign="top">
                                                    <asp:Label ID="lblvCodedate" runat="server"></asp:Label>
                                                </td>
                                                <td valign="top">&nbsp;Code :
                                                    <asp:Label ID="lblcode" runat="server"></asp:Label>
                                                    &nbsp; &nbsp;&nbsp; &nbsp; 
                                                   Date : 
                                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                    <br />

                                                </td>
                                            </tr>


                                            <tr>
                                                <td valign="top">Name :
                                                    <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>

                                                    <asp:FilteredTextBoxExtender ID="txtname_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtname" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                       </asp:FilteredTextBoxExtender>

                                                   <%-- <asp:FilteredTextBoxExtender ID="txtmname_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtname" ValidChars="abcdefghijklmnopqrstuvwxyz  ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                    </asp:FilteredTextBoxExtender>--%>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter Candidate Name" ForeColor="Red" ControlToValidate="txtname" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td valign="top">DOB :
                                                    <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdob" runat="server"    onchange="javascript:calDate(this.value);" OnTextChanged="txtdob_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="chkOut_MaskedEditExtender" runat="server"
                                                        Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                        TargetControlID="txtdob" UserDateFormat="MonthDayYear">
                                                    </asp:MaskedEditExtender>
                                                   
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdob" Format="MM/dd/yyyy"
                                                        EnableViewState="true">
                                                    </asp:CalendarExtender>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorGFYUF1" runat="server" ErrorMessage="Enter DOB" ForeColor="Red" ControlToValidate="txtdob" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>

                                             <tr>
                                                    <td valign="top">DOB : <asp:Label ID="Labeldfsgdfg14" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td>
                                                        
                                                            <asp:TextBox ID="txtdob" runat="server"  CssClass="form-control"  OnTextChanged="txtdob_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate" runat="server" ErrorMessage="Enter DOB" ForeColor="Red" ControlToValidate="txtdob" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            <asp:CalendarExtender ID="dtprelivedate_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtdob" Format="MM/dd/yyyy">
                                                        </asp:CalendarExtender>



                                                    </td>
                                                </tr>



                                            <tr>
                                                <td valign="top">Gender :
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdgender" runat="server"    RepeatDirection="Horizontal" CssClass="form-control">
                                                        <asp:ListItem Selected="True">Male</asp:ListItem>
                                                        <asp:ListItem>Female</asp:ListItem>

                                                    </asp:RadioButtonList>
                                                    <br />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td valign="top">Contact No. :
                                                    <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>

                                                    <asp:Label ID="lblcompanyId" runat="server" Visible="false"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcontactno" runat="server"  MaxLength="10" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        Enabled="True" TargetControlID="txtcontactno" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Contact No." ForeColor="Red" ControlToValidate="txtcontactno" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter Valid Contact No."
                                                        ValidationGroup="S" ControlToValidate="txtcontactno" Display="Static" ForeColor="Red"
                                                        ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Alt. Contact No. :
                                                   
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtaltcontact" runat="server"  CssClass="form-control"  MaxLength="10"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                        Enabled="True" TargetControlID="txtaltcontact" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Enter Valid Alt. Contact No."
                                                        ValidationGroup="S" ControlToValidate="txtaltcontact" Display="Static" ForeColor="Red"
                                                        ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>

                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Vacancy Info</legend>
                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td valign="top">Candidate Id :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcandidateId" runat="server"  CssClass="form-control"  BorderColor="White" ReadOnly="True"></asp:TextBox>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Marital Status :
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rd_maritalstatus" runat="server"  CssClass="form-control"  RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True">Single</asp:ListItem>
                                                        <asp:ListItem>Married</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Religion :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlreligion" runat="server" CssClass="form-control">
                                                        <asp:ListItem>Hinduism</asp:ListItem>
                                                        <asp:ListItem>Muslim</asp:ListItem>
                                                        <asp:ListItem>Sikhism</asp:ListItem>
                                                        <asp:ListItem>Buddhism</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Nationality :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlnational" runat="server" CssClass="form-control">
                                                        <asp:ListItem>Indian</asp:ListItem>
                                                        <asp:ListItem>Pakistan</asp:ListItem>
                                                        <asp:ListItem>Bangladesh</asp:ListItem>
                                                        <asp:ListItem>Nepal</asp:ListItem>
                                                        <asp:ListItem>American</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Email :
                                                    <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtemail" runat="server"  CssClass="form-control" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Email" ForeColor="Red" ControlToValidate="txtemail" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Correct Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="S" SetFocusOnError="True">
                                                                            
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td valign="top">PAN No. : 
                                                    <asp:Label ID="Label16" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtpanno" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                        Enabled="True" TargetControlID="txtpanno" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Enter PAN No." ForeColor="Red" ControlToValidate="txtpanno" ValidationGroup="S"></asp:RequiredFieldValidator>


                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkpassport" runat="server" AutoPostBack="True" OnCheckedChanged="chkpassport_CheckedChanged"/>
                                                    Pass Port :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtpassport" runat="server"  CssClass="form-control"  ReadOnly="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtpassport"
                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Pass Port No." SetFocusOnError="True" Enabled="false"
                                                        ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Current Address</legend>
                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td colspan="2">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">State :
                                                    <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCsate" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCsate_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlCsate"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Select State" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td valign="top">City :
                                                    <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCcity" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCcity"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Select City" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Zip Code :
                                                    


                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCzipcode" runat="server"  CssClass="form-control"  MaxLength="6"></asp:TextBox>

                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        Enabled="True" TargetControlID="txtCzipcode" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>

                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCzipcode"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter ZipCode" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Landmark :
                                                   <%-- <asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label>--%>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtClandmark" runat="server"  CssClass="form-control" ></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtClandmark"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Landmark" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>

                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Permanent Address</legend>




                                        <table width="100%" cellspacing="8px">
                                            <tr>

                                                <td>

                                                    <asp:CheckBox ID="chksame" runat="server" Text="Same" AutoPostBack="True"  OnCheckedChanged="chksame_CheckedChanged" />


                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">State :
                                                    <asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPstatte" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPstatte_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPstatte"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Select State" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">City :
                                                    <asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPcity" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPcity"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Select City" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Zip Code :
                                                    <%--<asp:Label ID="Label12" runat="server" Text="*" ForeColor="Red"></asp:Label>--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPzipcode" runat="server" CssClass="form-control"   MaxLength="6"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                        Enabled="True" TargetControlID="txtPzipcode" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>

                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPzipcode"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter ZipCode" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Landmark :
                                                    <%--<asp:Label ID="Label13" runat="server" Text="*" ForeColor="Red"></asp:Label>--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPlandmark" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPlandmark"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Landmark" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>

                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>
                        
                    </div>
                    <div class="row">







                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Profession</legend>
                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Year
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Month
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>Total Experience :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlyear" runat="server" style="margin-left: 9px;width:100px" CssClass="form-control">
                                                        <asp:ListItem Value="0" Selected="true">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                    </asp:DropDownList>


                                                    <%-- <asp:TextBox ID="txttotalexp" runat="server"   ></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                          Enabled="True" TargetControlID="txttotalexp" ValidChars="0123456789"></asp:FilteredTextBoxExtender>--%>

                                    
                                                </td>

                                                <td>
                                                    <%-- <asp:DropDownList ID="ddlperiod" runat="server"   >
                                       <asp:ListItem selected="true">Year</asp:ListItem>
                                       <asp:ListItem>Month</asp:ListItem>
                                   </asp:DropDownList>--%>


                                                    <asp:DropDownList ID="ddlmonth" runat="server" style="margin-left: 9px;width:100px" CssClass="form-control">
                                                        <asp:ListItem Value="0" Selected="true">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Year
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Month
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>Relevant Experience :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlrelyear" runat="server" style="margin-left: 9px;width:100px" CssClass="form-control">
                                                        <asp:ListItem Value="0" Selected="true">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <%--<asp:TextBox ID="txtrelevent" runat="server"   ></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                          Enabled="True" TargetControlID="txtrelevent" ValidChars="0123456789"></asp:FilteredTextBoxExtender>--%>
                                    
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlrelmont" runat="server" style="margin-left: 9px;width:100px" CssClass="form-control">
                                                        <asp:ListItem Value="0" Selected="true">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>

                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="top">Notice Period(In days) :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtnoticeperiod" runat="server"    MaxLength="2" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        Enabled="True" TargetControlID="txtnoticeperiod" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>
                                                    <br />
                                                </td>
                                                <%-- <td>
                         <asp:DropDownList ID="ddlnoticePeriod" runat="server"   >
                                       <asp:ListItem selected="true">Days</asp:ListItem>
                                       <asp:ListItem>Month</asp:ListItem>
                                   </asp:DropDownList>
                                </td>
                                                --%>
                                            </tr>
                                            <tr>
                                                <td valign="top">Skills :
                                                </td>
                                                <td>&nbsp;&nbsp;
                                                    <asp:TextBox ID="txtskliis" runat="server" TextMode="MultiLine"  CssClass="form-control" ></asp:TextBox>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Upload Resume :
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="FileUpload1" runat="server"    />
                                                    <br />

                                                </td>

                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnuploadresume" Text="Upload" runat="server" CssClass="btn bg-blue-active" ValidationGroup="r" OnClick="btnuploadresume_Click" />
                                                    <asp:Label ID="lblresumepath" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Experience Details</legend>
                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td>&nbsp;&nbsp;&nbsp;Company Name
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;Designation
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtcompanyName" runat="server"  CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtcompanyName"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Company Name" SetFocusOnError="True"
                                                        ValidationGroup="X"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtdesignation"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Designation" SetFocusOnError="True"
                                                        ValidationGroup="X"></asp:RequiredFieldValidator>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp;&nbsp;From Month                
                                                </td>

                                                <td>&nbsp;&nbsp;&nbsp;From Year   

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlFrommonth" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFrommonth_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Selected="True">Januray</asp:ListItem>
                                                        <asp:ListItem Value="2">Februray</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">Agust</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtfromyear" runat="server"  CssClass="form-control"  MaxLength="4" AutoPostBack="True" OnTextChanged="txtfromyear_TextChanged"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                        Enabled="True" TargetControlID="txtfromyear" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtfromyear"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Year" SetFocusOnError="True"
                                                        ValidationGroup="X"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtfromyear"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Valid Year" ValidationExpression="\d{4}">
                                                    </asp:RegularExpressionValidator>


                                                </td>


                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp;&nbsp;To Month

                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;To Year

                                                </td>

                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddltommonth" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltommonth_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Selected="True">Januray</asp:ListItem>
                                                        <asp:ListItem Value="2">Februray</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">Agust</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txttoyear" runat="server" CssClass="form-control" MaxLength="4" AutoPostBack="True" OnTextChanged="txttoyear_TextChanged"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                        Enabled="True" TargetControlID="txttoyear" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txttoyear"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Year" SetFocusOnError="True"
                                                        ValidationGroup="X"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txttoyear"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Valid Year" ValidationExpression="\d{4}">
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp;&nbsp;Company  Address
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtcompanyAddress" runat="server"  CssClass="form-control"  TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtcompanyAddress"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Company Address" SetFocusOnError="True"
                                                        ValidationGroup="X"></asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="top">
                                                    <asp:Button ID="btnaddExperience" runat="server" Text="Add" CssClass="btn bg-blue-active" ValidationGroup="X" OnClick="btnaddExperience_Click" />
                                                </td>
                                                <td colspan="2">&nbsp; 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnl" runat="server" >
                                                        <asp:GridView ID="grdexperience" runat="server" AutoGenerateColumns="false" Width="99%" 
                                                            CssClass="table table-bordered table-striped">
                                                            <Columns>
                                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                <asp:BoundField DataField="FromMonth" HeaderText="From Month" />
                                                                <asp:BoundField DataField="FromYear" HeaderText="From Year" />

                                                                <asp:BoundField DataField="ToMonth" HeaderText="From Month" />
                                                                <asp:BoundField DataField="ToYear" HeaderText="From Year" />

                                                                <asp:BoundField DataField="Address" HeaderText="Address" />

                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgExpedit" runat="server" CommandArgument='<%# Eval("CompanyName") %>'
                                                                            Height="20px" ImageUrl="~/Images/i_edit.png" OnClick="imgExpedit_Click" />
                                                                        <asp:ImageButton ID="imgExpdelete" runat="server" CommandArgument='<%# Eval("CompanyName") %>'
                                                                            Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" OnClick="imgExpdelete_Click" />
                                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="true"
                                                                            ConfirmText="Do you Really want to delete ..?" Enabled="True" TargetControlID="imgExpdelete">
                                                                        </asp:ConfirmButtonExtender>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="70px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No Record Exist....!!!!!!!!!!!!!!
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>

                                                    </asp:Panel>


                                                </td>
                                            </tr>

                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>
                        
                    </div>
                    <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Other Info</legend>
                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td valign="top">current CTC :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCTC" runat="server" CssClass="form-control"   MaxLength="7"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        Enabled="True" TargetControlID="txtCTC" ValidChars="0123456789.">
                                                    </asp:FilteredTextBoxExtender>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Expected CTC :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExpectedCTC" runat="server" CssClass="form-control"   MaxLength="7"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        Enabled="True" TargetControlID="txtExpectedCTC" ValidChars="0123456789.">
                                                    </asp:FilteredTextBoxExtender>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Reference By :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlrefrenceby" runat="server" CssClass="form-control"  OnSelectedIndexChanged="ddlrefrenceby_SelectedIndexChanged" AutoPostBack="True">
                                                        <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                                        <asp:ListItem>Agency</asp:ListItem>
                                                        <asp:ListItem>Employee</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="trrefren" runat="server" visible="false">
                                                <td valign="top">
                                                    <asp:Label ID="lblrefnameby" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlrefNameid" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlrefNameid"
                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Select Reference Name" SetFocusOnError="True"
                                                        ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    <br />
                                                </td>
                                            </tr>




                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Education Qualification</legend>
                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td>Education Name
                                                </td>
                                                <td>Passing Year
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txteduname" runat="server" CssClass="form-control"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txteduname"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Education" SetFocusOnError="True"
                                                        ValidationGroup="e"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtpassingyear" runat="server"  CssClass="form-control"  AutoPostBack="True" OnTextChanged="txtpassingyear_TextChanged"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="txtpassingyear_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtpassingyear" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtpassingyear"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Passing Year" SetFocusOnError="True"
                                                        ValidationGroup="e"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtpassingyear"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Valid Year" ValidationExpression="\d{4}">
                                                    </asp:RegularExpressionValidator>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>University
                                                </td>
                                                <td>Percentage
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtuniversity" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtuniversity"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter University/School/College" SetFocusOnError="True"
                                                        ValidationGroup="e"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtpercentage" runat="server"  CssClass="form-control"  MaxLength="3"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="txtpercentage_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtpercentage" ValidChars=".0123456789">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpercentage"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Percentage" SetFocusOnError="True"
                                                        ValidationGroup="e"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtpercentage"
                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Valid Percentage"
                                                        ValidationExpression="^(100([\.][0]{1,})?$|[0-9]{2,2}([\.][0-9]{2,})?)$"></asp:RegularExpressionValidator>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnaddedu" runat="server" Text="Add" CssClass="btn bg-blue-active" ValidationGroup="e" OnClick="btnaddedu_Click" />
                                                </td>
                                                <td>&nbsp; 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="grdeducation" runat="server" AutoGenerateColumns="false" 
                                                       CssClass="table table-bordered table-striped" Width="99%">
                                                        <Columns>
                                                            <asp:BoundField DataField="EducationName" HeaderText="Education" />
                                                            <asp:BoundField DataField="YearOfPassing" HeaderText="Year Of Passing" />
                                                            <asp:BoundField DataField="University" HeaderText="University" />

                                                            <asp:BoundField DataField="ObtainPercent" DataFormatString="{0:0.00}" HeaderText="Percent" />
                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("EducationName") %>'
                                                                        Height="20px" ImageUrl="~/Images/i_edit.png" OnClick="imgedit_Click" Width="20px" />
                                                                    <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%# Eval("EducationName") %>'
                                                                        Height="20px" ImageUrl="~/Images/i_delete.png" OnClick="imgdelete_Click" Width="20px" />
                                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="true"
                                                                        ConfirmText="Do you Really want to delete ..?" Enabled="True" TargetControlID="imgdelete">
                                                                    </asp:ConfirmButtonExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Record Exist....!!!!!!!!!!!!!!
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>




                                                </td>
                                            </tr>

                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>

                        
                    </div>
                    <div class="row">







                    <%--  <div class="col-md-4">
                        <div>
                            <div>
                                <div class="form-group">

                                    <table width="100%" cellspacing="8px">
                                        <tr>
                                            <td>
                                               &nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </div>
                        </div>
                    </div>--%>

                    <div class="col-md-4">
                        <div>
                            <div>
                                <div class="form-group">

                                    <table width="100%" cellspacing="8px">
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-8">
                        <div>
                            <div>
                                <div class="form-group">

                                    <table width="100%" cellspacing="8px">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnsave" runat="server" CssClass="btn bg-blue-active" Text="Save" OnClick="btnsave_Click" ValidationGroup="S" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel" OnClick="btnCancel_Click" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </div>
                        </div>
                    </div>
</div>

                </asp:View>

            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

