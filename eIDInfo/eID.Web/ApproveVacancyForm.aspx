<%@ Page Title="Recruitment" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ApproveVacancyForm.aspx.cs" Inherits="Recruitment_ApproveVacancyForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadDoc" />
        </Triggers>
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">

                    <div class="box-body">
                        <div class="form-group">
                            <fieldset>
                                <legend>Approve vacancy</legend>
                            </fieldset>
                            <table width="100%">

                                <tr>
                                    <td>
                                        <asp:MultiView ID="MultiView1" runat="server">
                                            <asp:View ID="View1" runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>

                                                    <tr runat="server" id="trserchname" visible="false">
                                                        <td>Company Name

                                                        </td>
                                                        <td>Vacancy Name

                                                        </td>
                                                        <td>Department

                                                        </td>
                                                        <td>Designation

                                                        </td>

                                                    </tr>
                                                    <tr runat="server" id="trserchnametxt" visible="false">
                                                        <td>
                                                            <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control" OnTextChanged="txtcompany_TextChanged" AutoPostBack="True"></asp:TextBox>

                                                            <asp:AutoCompleteExtender ID="txtcompany_AutoCompleteExtender" runat="server"
                                                                DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionListCompany" ServicePath="" EnableCaching="true"
                                                                TargetControlID="txtcompany" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                            </asp:AutoCompleteExtender>
                                                            <asp:Label ID="lblcompaniId" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtvacancy" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <asp:AutoCompleteExtender ID="txtvacancy_AutoCompleteExtender" runat="server"
                                                                DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionListVacancy" ServicePath="" EnableCaching="true"
                                                                TargetControlID="txtvacancy" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                            </asp:AutoCompleteExtender>




                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdepart" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtdepart_TextChanged"></asp:TextBox>

                                                            <asp:AutoCompleteExtender ID="txtdepart_AutoCompleteExtender" runat="server"
                                                                DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionListDepartment" ServicePath="" EnableCaching="true"
                                                                TargetControlID="txtdepart" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                            </asp:AutoCompleteExtender>
                                                            <asp:Label ID="lbldeptId" runat="server" Visible="false"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdesig" runat="server" CssClass="form-control" OnTextChanged="txtdesig_TextChanged" AutoPostBack="True"></asp:TextBox>

                                                            <asp:AutoCompleteExtender ID="txtdesig_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                                                                ServiceMethod="GetCompletionListDesig" ServicePath="" EnableCaching="true"
                                                                TargetControlID="txtdesig" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                            </asp:AutoCompleteExtender>
                                                            <asp:Label ID="lbldesigID" runat="server" Visible="false"></asp:Label>

                                                        </td>

                                                    </tr>
                                                    <tr runat="server" id="trbtn" visible="false">
                                                        <td>
                                                            <asp:Button ID="btnsearch" Text="Search" runat="server" CssClass="btn bg-blue-active" />
                                                        </td>
                                                    </tr>



                                                    <tr>
                                                        <td align="right" colspan="4">
                                                            <b>
                                                                <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                                                </asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblcnt" runat="server">
                                            </asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:GridView ID="grdAtt" runat="server" CssClass="table table-bordered table-striped"
                                                                AutoGenerateColumns="False" BorderStyle="None" Width="100%"
                                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAtt_PageIndexChanging">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                                    <asp:BoundField DataField="VacancyName" HeaderText="Vacancy Name" />
                                                                    <asp:BoundField DataField="DeptName" HeaderText="Department" />
                                                                    <asp:BoundField DataField="DesigName" HeaderText="Designation" />
                                                                    <asp:BoundField DataField="Quota" HeaderText="No. Of Vacancy" />
                                                                    <asp:BoundField DataField="NOofInterview" HeaderText="No. Of Interview" />
                                                                    <asp:BoundField DataField="SelectedCandidate" HeaderText="No. Of Selected Candidate" />
                                                                    <asp:BoundField DataField="VacancyStatus" HeaderText="Vacancy Status" />
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>

                                                                            <asp:LinkButton ID="Lnknedit" runat="server" BorderStyle="None" Text="Approval" CommandArgument='<%#Eval("VacancyId")%>'
                                                                                Width="20px" ToolTip="Approve" OnClick="Lnknedit_Click"></asp:LinkButton>


                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="70px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>No Record Exists.....!!</EmptyDataTemplate>
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
                                                <asp:Panel ID="PnlAdd" runat="server" Width="100%" DefaultButton="btnsubmit">
                                                    <table cellpadding="0" cellspacing="4" align="center" width="100%">
                                                        <tr>
                                                            <td>Select Company:
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>

                                                                <asp:DropDownList ID="ddlComp" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%" OnSelectedIndexChanged="ddlComp_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <br />
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="ddlComp" Display="Dynamic"
                                                        ErrorMessage="Select Company" SetFocusOnError="True"
                                                        ValidationGroup="S" ForeColor="Red" InitialValue="--Select--"></asp:RequiredFieldValidator>--%>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="ddlComp" Display="Dynamic"
                                                                    ErrorMessage="Select Company" SetFocusOnError="True"
                                                                    ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>

                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>Select Department:
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDept" Display="Dynamic" ErrorMessage="Select Department" ForeColor="Red" InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Designation:<asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDesign" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%">
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDesign" Display="Dynamic" ErrorMessage="Select Designation" ForeColor="Red" InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Vacancy Code:  <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCode" Display="Dynamic" ErrorMessage="Enter Code" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                <br />

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Vacancy Title:
                                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="Enter Title" ForeColor="Red" InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Required Quota: <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtQuota" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                    Enabled="True" TargetControlID="txtQuota" ValidChars="0123456789">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtQuota" Display="Dynamic" ErrorMessage="Enter Required Quota" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Employee Type:
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:RadioButtonList ID="rbType" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbType_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True">Permanent</asp:ListItem>
                                                                    <asp:ListItem>Temporary</asp:ListItem>
                                                                    <asp:ListItem>Other</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:Label ID="lblid" runat="server" Visible="false"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td align="left" runat="server" id="tdOther" visible="false">
                                                                <asp:TextBox ID="txtOther" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Qualification:
                                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                    <asp:ListBox ID="lstQualifi" runat="server" SelectionMode="Multiple"  CssClass="form-control">
                                                        <asp:ListItem>HSC</asp:ListItem>
                                                        <asp:ListItem>SSC</asp:ListItem>
                                                        <asp:ListItem>Diploma</asp:ListItem>
                                                        <asp:ListItem>B.A</asp:ListItem>
                                                        <asp:ListItem>B.arch</asp:ListItem>
                                                        <asp:ListItem>BCA</asp:ListItem>
                                                        <asp:ListItem>B.B.A</asp:ListItem>
                                                        <asp:ListItem>B.Com</asp:ListItem>
                                                        <asp:ListItem>B.Ed</asp:ListItem>
                                                        <asp:ListItem>BDS</asp:ListItem>
                                                        <asp:ListItem>BHM</asp:ListItem>
                                                        <asp:ListItem>B.Pharma</asp:ListItem>
                                                        <asp:ListItem>B.Sc</asp:ListItem>
                                                        <asp:ListItem>B.Tech/B.E</asp:ListItem>
                                                        <asp:ListItem>LLB</asp:ListItem>
                                                        <asp:ListItem>MCA</asp:ListItem>
                                                        <asp:ListItem>MBA</asp:ListItem>
                                                        <asp:ListItem>MBBS</asp:ListItem>
                                                        <asp:ListItem>BVSC</asp:ListItem>
                                                        <asp:ListItem>PHD</asp:ListItem>
                                                    </asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="lstQualifi" Display="Dynamic" ErrorMessage="Select Education" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Skills:
                                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSkills" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Skills" Display="Static"
                                                                    ForeColor="Red" ControlToValidate="txtSkills" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Experience (In Year):
                                                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExpr" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    Enabled="True" TargetControlID="txtExpr" ValidChars="0123456789">
                                                                </asp:FilteredTextBoxExtender>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter Experience" Display="Static"
                                                                    ForeColor="Red" ControlToValidate="txtExpr" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr id="trid" runat="server" visible="false">
                                                            <td>Place of Work:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Place of Work:
                                                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlcity" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select City" Display="Static"
                                                                    ForeColor="Red" ControlToValidate="ddlcity" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Salary:
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                    Enabled="True" TargetControlID="txtSalary" ValidChars="0123456789.">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSalary" Display="Dynamic" ErrorMessage="Enter Salary" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Approved By:
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                    <asp:ListBox ID="lstEmp" runat="server" AutoPostBack="True" SelectionMode="Multiple"  CssClass="form-control" Enabled="False"></asp:ListBox>
                                                                &nbsp;
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="lstEmp" Display="Dynamic" ErrorMessage="Approved By" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Description:</td>
                                                            <td>&nbsp;&nbsp;
                                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Width="70%" />

                                                                <br />
                                                                <br />

                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <fieldset runat="server" id="fieldsetatt" class="fieldset">
                                                        <legend>Attach any other document</legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>Document Name:
                                                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtdocname" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                                        TargetControlID="txtdocname" ValidChars="0123456789abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>

                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtdocname"
                                                                        Display="Static" ForeColor="Red" ErrorMessage="Enter Document Name" SetFocusOnError="True" EnableClientScript="false"
                                                                        ValidationGroup="d"></asp:RequiredFieldValidator>
                                                                    <%-- <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True"
                                                                                TargetControlID="RequiredFieldValidator14">
                                                                            </asp:ValidatorCalloutExtender>--%>
                                                                </td>
                                                                <td>Upload Document
                                                                </td>
                                                                <td>
                                                                    <asp:FileUpload ID="FileUploadDocu" runat="server" />
                                                                    <asp:Label ID="lblpath" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnUploadDoc" runat="server" CssClass="btn bg-blue-active" AutoPostBack="False"
                                                                        Text="Add" ValidationGroup="d" OnClientClick="this.disabled = true; this.value = 'Submitting...';" UseSubmitBehavior="false" OnClick="btnUploadDoc_Click" />
                                                                    <clientsideevents click="btnUploadDoc_Click" />
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="pandocu" runat="server" >
                                                                        <asp:GridView ID="grdnewdoc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" EnableViewState="true"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNo" runat="server" Text="<%# Container.DataItemIndex + 1  %>" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Document_Name" HeaderText="Document Name" />
                                                                                <asp:BoundField DataField="Document_Path" HeaderText="Document Path" />
                                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                                                                    <ItemTemplate>
                                                                                        <center>
                                                                                                    <asp:ImageButton ID="Imgbtnedit" runat="server" BorderStyle="None" CommandArgument='<%#Eval("Document_Name")%>'
                                                                                                        CommandName="cmdEdit" Height="20px" ImageUrl="~/Images/i_edit.png" 
                                                                                                        Width="20px" ToolTip="Edit" OnClick="Imgbtnedit_Click" />
                                                                                                    <asp:ImageButton ID="Imgbtndelet" runat="server" BorderStyle="None" CommandArgument='<%#Eval("Document_Name")%>'
                                                                                                        CommandName="cmdEdit" Height="20px" ImageUrl="~/Images/i_delete.png" 
                                                                                                        Width="20px" ToolTip="Delete" OnClick="Imgbtndelet_Click" />
                                                                                                </center>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="70px" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                            <EmptyDataTemplate>
                                                                                No Record Exist....!!!!!!!!!!!!!!
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                    <asp:Label ID="lblproposalid" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblclientid" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:RadioButtonList ID="rd_approve" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rd_approve_SelectedIndexChanged">
                                                                        <asp:ListItem Selected="True">Approve</asp:ListItem>
                                                                        <asp:ListItem>Not Approve</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Button ID="btnsubmit" runat="server" Text="Approve" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                                        CssClass="btn bg-blue-active" />
                                                                    &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Back" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>

                                                </asp:Panel>


                                            </asp:View>
                                        </asp:MultiView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

