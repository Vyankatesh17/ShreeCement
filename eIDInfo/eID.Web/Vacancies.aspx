<%@ Page Title="Vacancies" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Vacancies.aspx.cs" Inherits="Vacancies" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadDoc" />
        </Triggers>
        <ContentTemplate>


            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <table width="100%">
                        <tr>
                            <td style="padding-left: 18px">
                                <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                    Text="Add New" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>

                    </table>
                    <div class="col-md-12">
                        <div class="box box-primary">

                            <div class="box-body">
                                <div class="form-group">
                                    <table width="100%">
                                        <tr>
                                            <td><b>Company Name </b>

                                            </td>
                                            <td><b>Vacancy Name </b>

                                            </td>
                                            <td><b>Department </b>

                                            </td>


                                            <td></td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control" OnTextChanged="txtcompany_TextChanged" AutoPostBack="True" Width="180px"></asp:TextBox>

                                                <asp:AutoCompleteExtender ID="txtcompany_AutoCompleteExtender" runat="server"
                                                    DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionListCompany" ServicePath="" EnableCaching="true"
                                                    TargetControlID="txtcompany" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                </asp:AutoCompleteExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtvacancy" runat="server" CssClass="form-control" Width="180px"></asp:TextBox>

                                                <asp:AutoCompleteExtender ID="txtvacancy_AutoCompleteExtender" runat="server"
                                                    DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionListVacancy" ServicePath="" EnableCaching="true"
                                                    TargetControlID="txtvacancy" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                </asp:AutoCompleteExtender>




                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdepart" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtdepart_TextChanged" Width="180px"></asp:TextBox>

                                                <asp:AutoCompleteExtender ID="txtdepart_AutoCompleteExtender" runat="server"
                                                    DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionListDepartment" ServicePath="" EnableCaching="true"
                                                    TargetControlID="txtdepart" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                </asp:AutoCompleteExtender>

                                            </td>
                                            <td>
                                                <asp:Button ID="btnsearch" Text="Search" runat="server" CssClass="btn bg-blue-active" OnClick="btnsearch_Click" />
                                                <asp:Button ID="btnReset" Text="Reset" runat="server" CssClass="btn bg-blue-active" OnClick="btnReset_Click" />

                                            </td>


                                            <td style="visibility: hidden">
                                                <asp:TextBox ID="txtdesig" runat="server" CssClass="form-control" OnTextChanged="txtdesig_TextChanged" AutoPostBack="True" Width="180px"></asp:TextBox>

                                                <asp:AutoCompleteExtender ID="txtdesig_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                                                    ServiceMethod="GetCompletionListDesig" ServicePath="" EnableCaching="true"
                                                    TargetControlID="txtdesig" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                </asp:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <asp:Label ID="lbldeptId" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblcompaniId" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lbldesigID" runat="server" Visible="false"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="5">
                                                <b>
                                                    <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                                    </asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblcnt" runat="server">
                                            </asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:GridView ID="grdAtt" runat="server" CssClass="table table-bordered table-striped"
                                                    AutoGenerateColumns="False" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
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
                                                        <%--<asp:BoundField DataField="DesigName" HeaderText="Designation" />--%>
                                                        <asp:BoundField DataField="Quota" HeaderText="Total Vacancy" />
                                                        <asp:BoundField DataField="NOofInterview" HeaderText="Total Interviews" />
                                                        <asp:BoundField DataField="SelectedCandidate" HeaderText="Total Selected Candidate" />


                                                        <%-- <asp:TemplateField HeaderText="Address">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox runat="server" ID="txtre" Text='<%#Eval("Address") %>' ReadOnly="true" TextMode="MultiLine" />

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>--%>
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
       
                            <!-- left column -->
                            <div class="col-md-8">

                                <div class="box box-primary">
                                    <br />
                                    <table cellpadding="0" cellspacing="4" align="center" width="90%">
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Select Company:
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>

                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlComp" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%" TabIndex="1" OnSelectedIndexChanged="ddlComp_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="ddlComp" Display="Dynamic"
                                                        ErrorMessage="Select Company" SetFocusOnError="True"
                                                        ValidationGroup="S" ForeColor="Red" InitialValue="--Select--"></asp:RequiredFieldValidator>--%>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="ddlComp" Display="Dynamic"
                                                        ErrorMessage="Select Company" SetFocusOnError="True"
                                                        ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Select Department:
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%" TabIndex="1" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDept" Display="Dynamic" ErrorMessage="Select Department" ForeColor="Red" InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Select Designation:<asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlDesign" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%" TabIndex="2" OnSelectedIndexChanged="ddlDesign_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDesign" Display="Dynamic" ErrorMessage="Select Designation" ForeColor="Red" InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Vacancy Code:
                                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtCode_TextChanged" TabIndex="4" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCode" Display="Dynamic" ErrorMessage="Enter Vacancy Code" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Vacancy Title:
                                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="Enter Title" ForeColor="Red" InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Required Quota:
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtQuota" runat="server" CssClass="form-control" MaxLength="3" TabIndex="6"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        Enabled="True" TargetControlID="txtQuota" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtQuota" Display="Dynamic" ErrorMessage="Enter Required Quota" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Employee Type:
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td align="left">
                                                <div class="form-group">
                                                    <asp:RadioButtonList ID="rbType" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" TabIndex="7" OnSelectedIndexChanged="rbType_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">Permanent</asp:ListItem>
                                                        <asp:ListItem>Temporary</asp:ListItem>
                                                        <asp:ListItem>Other</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td align="left" runat="server" id="tdOther" visible="false">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtOther" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Qualification:
                                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:ListBox ID="lstQualifi" runat="server" SelectionMode="Multiple"  CssClass="form-control" TabIndex="9">
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

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Skills:
                                                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtSkills" runat="server" CssClass="form-control" TextMode="MultiLine" Style="resize: vertical" TabIndex="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage=" Enter Skills" Display="Dynamic"
                                                        ForeColor="Red" ControlToValidate="txtSkills" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Experience (In Year):
                                                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <div style="float:left;width:45%">
                                                        <asp:TextBox ID="txtExpr" runat="server" CssClass="form-control" MaxLength="3" placeholder="Minimum Year" TabIndex="11"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter Minimum Experience" Display="Dynamic"
                                                            ForeColor="Red" ControlToValidate="txtExpr" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                            Enabled="True" TargetControlID="txtExpr" ValidChars="0123456789.">
                                                        </asp:FilteredTextBoxExtender>
                                                    </div>
                                                    <div style="float:right; width:45%">
                                                        <asp:TextBox ID="txtExprMax" runat="server" CssClass="form-control" MaxLength="3" placeholder="Maximum Year" TabIndex="12"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Enter Maximum Experience" Display="Dynamic"
                                                            ForeColor="Red" ControlToValidate="txtExprMax" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                            Enabled="True" TargetControlID="txtExprMax" ValidChars="0123456789.">
                                                        </asp:FilteredTextBoxExtender>
                                                        <br />
                                                    </div>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="trid" runat="server" visible="false">
                                            <td>
                                                <div class="form-group">Place of Work: </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control" TabIndex="13"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Place of Work: 
                                                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlcity" runat="server" CssClass="form-control" TabIndex="14"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select City" Display="Dynamic"
                                                        ForeColor="Red" ControlToValidate="ddlcity" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Salary:
                                                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" MaxLength="7" CausesValidation="True" ValidationGroup="S" TabIndex="15"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter Salary" Display="Dynamic"
                                                        ForeColor="Red" ControlToValidate="txtSalary" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        Enabled="True" TargetControlID="txtSalary" ValidChars="0123456789.">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator runat="server" ID="rexNumber" ControlToValidate="txtSalary"
                                                        ValidationExpression="^\d+(\.\d\d)?$" ErrorMessage="Invalid Amount" />

                                                    <%--   <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtSalary" ErrorMessage="Invalid Amount" ForeColor="Red" Operator="DataTypeCheck" Type="Double" ValidationGroup="S"></asp:CompareValidator>
                                                    --%>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    Approved By:
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:ListBox ID="lstEmp" runat="server" AutoPostBack="True" SelectionMode="Multiple"  CssClass="form-control" TabIndex="16"></asp:ListBox>
                                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="lstEmp" Display="Dynamic" ErrorMessage="Approved By" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">Description: </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Width="70%"  TabIndex="17"/>

                                                </div>

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
                                                    <asp:TextBox ID="txtdocname" runat="server" TextMode="MultiLine" TabIndex="18"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                        TargetControlID="txtdocname" ValidChars="0123456789abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                    </asp:FilteredTextBoxExtender>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtdocname"
                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Document Name" SetFocusOnError="True" EnableClientScript="false"
                                                        ValidationGroup="d"></asp:RequiredFieldValidator>
                                                    <%-- <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True"
                                                                                TargetControlID="RequiredFieldValidator14">
                                                                            </asp:ValidatorCalloutExtender>--%>
                                                </td>
                                                <td>Upload Document
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="FileUploadDocu" runat="server"  TabIndex="19"/>
                                                    <asp:Label ID="lblpath" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnUploadDoc" runat="server" CssClass="btn bg-blue-active" AutoPostBack="False"
                                                        Text="Add" ValidationGroup="d" OnClientClick="this.disabled = true; this.value = 'Submitting...';" UseSubmitBehavior="false" OnClick="btnUploadDoc_Click" TabIndex="20" />
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
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                        CssClass="btn bg-blue-active" TabIndex="21" />
                                                    &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active"  TabIndex="22"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                    </asp:Panel>

                </asp:View>
            </asp:MultiView>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

