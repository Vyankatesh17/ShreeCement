<%@ Page Title="Agency Master" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="AgencyMaster.aspx.cs" Inherits="Recruitment_AgencyMaster" EnableEventValidation="false" %>

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
                            <table width="70%">
                                      <tr>
                                    <td style="width:70px" valign="top">
                                        Search By :
                                    </td>
                                    <td style="width:120px" valign="top">
                                        <asp:DropDownList ID ="ddlsortby" runat="server" CssClass="form-control" Width="170px" AutoPostBack="True" OnSelectedIndexChanged="ddlsortby_SelectedIndexChanged">
                                            <asp:ListItem>ALL</asp:ListItem>
                                               <asp:ListItem>Agency-Wise</asp:ListItem>
                                               <asp:ListItem>Contact Person-Wise</asp:ListItem>
                                               <asp:ListItem>Speciality-Wise</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:100px" valign="top">
                                        &nbsp;
                                        <asp:Label ID="lblname" runat="server" Text="Name" Visible="false"></asp:Label>
                                    </td>
                                    <td style="width:350px" valign="top">
                                        <asp:TextBox ID="txtagencywise" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                         <asp:AutoCompleteExtender ID="txtagencywise_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                    Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtagencywise"
                                    CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetCompletionListAgency">
                                </asp:AutoCompleteExtender>
                                         <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                            ControlToValidate="txtagencywise" Display="Dynamic"
                                                            ErrorMessage="Enter Agency Name" SetFocusOnError="True"
                                                            ValidationGroup="b" ForeColor="Red"></asp:RequiredFieldValidator>--%>


                                        <asp:TextBox ID="txtcontactwise" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                         <asp:AutoCompleteExtender ID="txtcontactwise_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                    Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtcontactwise"
                                    CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetCompletionListcontact">
                                </asp:AutoCompleteExtender>
                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                            ControlToValidate="txtcontactwise" Display="Dynamic"
                                                            ErrorMessage="Enter Contact Person" SetFocusOnError="True"
                                                            ValidationGroup="b" ForeColor="Red"></asp:RequiredFieldValidator>--%>


                                         <asp:TextBox ID="txtspecilitywise" runat="server" CssClass="form-control" Visible="false" OnTextChanged="txtspecilitywise_TextChanged" AutoPostBack="True"></asp:TextBox>
                                         <asp:AutoCompleteExtender ID="txtspecilitywise_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                    Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtspecilitywise"
                                    CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetCompletionListSpeciality">
                                </asp:AutoCompleteExtender>

                                         <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                            ControlToValidate="txtspecilitywise" Display="Dynamic"
                                                            ErrorMessage="Enter Speciality" SetFocusOnError="True"
                                                            ValidationGroup="b" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    <td valign="top">
                                       <asp:Button ID="btnsearch" runat="server" Text="Search" ValidationGroup="b"
                                            CssClass="btn bg-blue-active" OnClick="btnsearch_Click" UseSubmitBehavior="False" />

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
                                    <asp:Label ID="lblcount" runat="server" Font="Bold">0</asp:Label>
                                    <asp:Label ID="lblcandidateID" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblviewId" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                                    <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="grdAgency" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-striped" OnPageIndexChanging="grdAgency_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" />
                                                <asp:BoundField DataField="Address" HeaderText="Address" />
                                                <asp:BoundField DataField="MobNo" HeaderText="Mobile No" />
                                                <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" />
                                                <asp:BoundField DataField="EmailId" HeaderText="Email Id" />
                                                <asp:BoundField DataField="SpecialityName" HeaderText="Speciality" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("AgencyId") %>' ImageUrl="~/Recruitment/Images/i_edit.png" OnClick="imgedit_Click" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
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
            <section class="content-header">
                <h3>Agency Information </h3>
            </section>
             <section class="content">
                <%-- <div class="row">--%>
                <div class="box-body">
                    <div class="form-group">
                        <table width="100%">
                            <tr>
                                <td>
                                    <%--<asp:UpdatePanel ID="upp1" runat="server">
                                        <ContentTemplate>--%>
                                            <asp:Panel ID="pan" runat="server" DefaultButton="btnsave">
                                                <div class="col-md-6">
                                                    <div class="box box-primary">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2">&nbsp;
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td valign="top">Agency Name : 
                                                    <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAgency" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtAgency_TextChanged"></asp:TextBox><asp:Label ID="lblAgencyId" runat="server" Visible="False"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" SetFocusOnError="True" ErrorMessage="Enter Agency Name" ForeColor="Red" ControlToValidate="txtAgency" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">Area : 
                                                    <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtarea" runat="server" TextMode="MultiLine" CssClass="form-control" Height="50px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                        SetFocusOnError="True" ErrorMessage="Enter Area"
                                                                        ForeColor="Red" ControlToValidate="txtarea" ValidationGroup="S">
                                                                    </asp:RequiredFieldValidator>

                                                                </td>
                                                            </tr>



                                                            <tr>
                                                                <td valign="top">Mobile No: 
                                                    <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtmob" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                                    <asp:FilteredTextBoxExtender ID="filet1" runat="server" TargetControlID="txtmob" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="True" ErrorMessage="Enter Mobile No" ForeColor="Red"
                                                                        ControlToValidate="txtmob" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Valid Mobile No."
                                                                        ValidationGroup="S" ControlToValidate="txtmob" Display="Static" ForeColor="Red"
                                                                        ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td valign="top">Contact Person :
                                                    <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContactP" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                        ErrorMessage="Enter Contact Person" ForeColor="Red" ControlToValidate="txtContactP"
                                                                        ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtContactP"
                                                                        ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">Password :
                                                    <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtpassword" runat="server" AutoComplete="off" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                        ErrorMessage="Enter Password" ForeColor="Red" ControlToValidate="txtpassword"
                                                                        ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="box box-primary">
                                                        <table width="100%">
                                                           
                                                            <tr>
                                                                <td valign="top">Email Id :
                                                          <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtemailID" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtemailID_TextChanged"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter E-Mail" ForeColor="Red" ControlToValidate="txtemailID" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemailID"
                                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Correct Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                        ValidationGroup="S" SetFocusOnError="True">
                                                                    </asp:RegularExpressionValidator>


                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td valign="top">Alternate Mobile No :
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtAltmob" runat="server" CssClass="form-control" MaxLength="15" AutoPostBack="True"></asp:TextBox>

                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        TargetControlID="txtAltmob" FilterType="Numbers">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:RegularExpressionValidator Display="Static" ControlToValidate="txtAltmob" ID="RegularExpressionValidator4"
                                                                        ValidationExpression="^[\s\S]{6,15}$" runat="server" ValidationGroup="S" ForeColor="Red"
                                                                        ErrorMessage="Minimum 6 and Maximum 15 characters required."></asp:RegularExpressionValidator>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">Speciality :
                                                                <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlSpeciality" runat="server" CssClass="form-control"></asp:DropDownList>

                                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlSpeciality" Display="Dynamic"
                                                                        ErrorMessage="Select Speciality" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Select Speciality" ForeColor="Red" ControlToValidate="ddlSpeciality" ValidationGroup="S"></asp:RequiredFieldValidator>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">Payment :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtpayment" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                        TargetControlID="txtpayment" FilterType="Custom" ValidChars=".0123456789">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:CompareValidator ID="intValidator" runat="server" ControlToValidate="txtpayment"
                                                                        Operator="DataTypeCheck" Type="Double" ErrorMessage="Please Insert Correct Value"
                                                                        ForeColor="Red" ValidationGroup="S" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">Agreement : 
                                                                </td>

                                                                <td>
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                    <asp:Label ID="lblagreementpath" runat="server"></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnuploadresume" Text="Upload" runat="server" CssClass="btn bg-blue-active" ValidationGroup="r" OnClick="btnuploadresume_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">&nbsp;
                                                                </td>
                                                                <br /><br />
                                                            </tr>
                                                           
                                                        </table>
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
                                                                                <asp:Button ID="btnsave" runat="server" CssClass="btn bg-blue-active" Text="Save" ValidationGroup="S" OnClick="btnsave_Click" />
                                                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel" OnClick="btnCancel_Click" />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                       <%-- </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>

                <%-- </div>--%>
            </section>
        </asp:View>
    </asp:MultiView>
</asp:Content>

