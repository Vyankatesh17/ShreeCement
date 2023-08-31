<%@ Page Title="Interview Schedule" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ScheduleInterview.aspx.cs" EnableEventValidation="false" Inherits="Recruitment_ScheduleInterview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updaa" runat="server">
       <ContentTemplate>


    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table width="100%">
                <tr>
                    <td style="padding-left: 18px">
                        <asp:Button ID="btnadd" runat="server" CssClass="btn bg-blue-active" Text="Add New" OnClick="btnadd_Click" />
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
                                        <td style="width: 70px">Search By :
                                        </td>
                                        <td style="width: 120px">
                                            <asp:DropDownList ID="ddlsortby" runat="server" CssClass="form-control" Width="170px" AutoPostBack="True" OnSelectedIndexChanged="ddlsortby_SelectedIndexChanged">
                                                <asp:ListItem>ALL</asp:ListItem>
                                                <asp:ListItem>Company-Wise</asp:ListItem>
                                                <asp:ListItem>Position-Wise</asp:ListItem>
                                                <asp:ListItem>Candidate-Wise</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px">&nbsp;
                                        <asp:Label ID="lblname" runat="server" Text="Name" Visible="false"></asp:Label>
                                        </td>
                                        <td style="width: 350px">
                                            <asp:TextBox ID="txtcompanywise" runat="server" CssClass="form-control" Visible="false" OnTextChanged="txtcompanywise_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtcompanywise_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                                Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtcompanywise"
                                                CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetCompletionListCompany">
                                            </asp:AutoCompleteExtender>


                                            <asp:TextBox ID="txtpositionwise" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtpositionwise_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                                Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtpositionwise"
                                                CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetCompletionListPosition">
                                            </asp:AutoCompleteExtender>



                                            <asp:TextBox ID="txtcandidatewise" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtcandidatewise_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                                Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtcandidatewise"
                                                CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetCompletionListCandidate">
                                            </asp:AutoCompleteExtender>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn bg-blue-active" OnClick="btnsearch_Click" />
                                            <asp:Label ID="lblcompID" runat="server" Visible="false"></asp:Label>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdschedule" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-striped" AllowPaging="True" OnPageIndexChanging="grdschedule_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="companyname" HeaderText="Company Name" />
                                                <asp:BoundField DataField="position" HeaderText="Position" />
                                                <asp:BoundField DataField="candidatename" HeaderText="Candidate" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="FromTime" HeaderText="From Time" />
                                                <asp:BoundField DataField="ToTime" HeaderText="To Time" />
                                                <asp:BoundField DataField="Vanue" HeaderText="Venue" />
                                                <asp:BoundField DataField="Status" HeaderText="Interview Status" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("ScheduleId") %>' ImageUrl="~/Recruitment/Images/i_edit.png" OnClick="imgedit_Click" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>No Record Exists...!!!</EmptyDataTemplate>
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
   
            <section class="content">


                <div class="col-md-12">
                    <div class="box box-primary">

                        <div class="box-body">
                            <div class="form-group">
                                <table width="100%">
                                    <tr>
                                        <td valign="top">
                                            <table width="450px" cellspacing="8px">
                                                <tr>
                                                    <td valign="top">Company :
                                                    <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged"></asp:DropDownList>

                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlcompany" Display="Dynamic"
                                                            ErrorMessage="Select Company" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Company" ForeColor="Red" ControlToValidate="ddlcompany" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td valign="top">Venue :
                                                    <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtvanue" runat="server" CssClass="form-control" TextMode="MultiLine" Style="resize: vertical;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter Venu" ForeColor="Red" ControlToValidate="txtvanue" ValidationGroup="S"></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table width="450px" cellspacing="8px">
                                                <tr>
                                                    <td valign="top">Date :
                                                    <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" MaxLength="60" AutoPostBack="True" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Date" ForeColor="Red" ControlToValidate="txtdate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                        <asp:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtdate">
                                                        </asp:CalendarExtender>

                                                    </td>
                                                </tr>




                                                <tr>
                                                    <td valign="top">Employee :
                                                    <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlemployee" runat="server" CssClass="form-control"></asp:DropDownList>

                                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlemployee" Display="Dynamic"
                                                            ErrorMessage="Select Employee" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Select Employee" ForeColor="Red" ControlToValidate="ddlemployee" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="btnAddemp" runat="server" CssClass="btn bg-blue-active" Text="Add" ValidationGroup="a" OnClick="btnAddemp_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:ListBox ID="listemp" runat="server" CssClass="form-control" Visible="false"></asp:ListBox>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnRemoveEmp" runat="server" Visible="false" CssClass="tn btn-primary btn-xs" data-widget="remove" Text="X" OnClick="btnRemoveEmp_Click" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                    </tr>

                                </table>


                                <div class="box-body">
                                    <div class="form-group">
                                        <fieldset>
                                            <legend></legend>
                                            <table width="100%" colspan="4">
                                                <tr>
                                                    <td valign="top">
                                                        <table width="100%">
                                                            <tr>
                                                                <td valign="top">Position :
                                                                <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlposition" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlposition_SelectedIndexChanged"></asp:DropDownList>

                                                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="ddlposition" Display="Dynamic"
                                                                        ErrorMessage="Select Position" Operator="NotEqual" ValidationGroup="b" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Position" ForeColor="Red" ControlToValidate="ddlposition" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                                    <asp:Label ID="lblschedulid" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">Time :
                                                                <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>

                                                                <td colspan="2" valign="top" height="50px">
                                                                    <asp:DropDownList ID="ddhh" runat="server" CausesValidation="True" Width="75px" TabIndex="34">
                                                                        <asp:ListItem Value="HH">HH</asp:ListItem>
                                                                        <asp:ListItem Value="01">01</asp:ListItem>
                                                                        <asp:ListItem Value="02">02</asp:ListItem>
                                                                        <asp:ListItem Value="03">03</asp:ListItem>
                                                                        <asp:ListItem Value="04">04</asp:ListItem>
                                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                                        <asp:ListItem Value="06">06</asp:ListItem>
                                                                        <asp:ListItem Value="07">07</asp:ListItem>
                                                                        <asp:ListItem Value="08">08</asp:ListItem>
                                                                        <asp:ListItem Value="09">09</asp:ListItem>
                                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                                    </asp:DropDownList>



                                                                    <asp:DropDownList ID="ddmm" runat="server" CausesValidation="True" Width="75px" TabIndex="35">
                                                                        <asp:ListItem Value="MM">MM</asp:ListItem>
                                                                        <asp:ListItem Value="00">00</asp:ListItem>
                                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                                        <asp:ListItem Value="35">35</asp:ListItem>
                                                                        <asp:ListItem Value="40">40</asp:ListItem>
                                                                        <asp:ListItem Value="45">45</asp:ListItem>
                                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                                        <asp:ListItem Value="55">55</asp:ListItem>
                                                                    </asp:DropDownList>


                                                                    &nbsp;<asp:DropDownList ID="ddampm" runat="server"
                                                                        Width="75px" TabIndex="36">
                                                                        <asp:ListItem Selected="True" Value="AM">AM</asp:ListItem>
                                                                        <asp:ListItem Value="PM">PM</asp:ListItem>
                                                                    </asp:DropDownList><br />
                                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddhh"
                                                                        ForeColor="Red" ErrorMessage="Select Hour" Operator="NotEqual" ValidationGroup="b"
                                                                        ValueToCompare="HH"></asp:CompareValidator>
                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddmm"
                                                                        ForeColor="Red" ErrorMessage="Select Minutes" Operator="NotEqual" ValidationGroup="b"
                                                                        ValueToCompare="MM"></asp:CompareValidator>
                                                                    &nbsp;<asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                                                                </td>

                                                            </tr>

                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table width="100%">
                                                            <tr>
                                                                <td valign="top">Add Candidate:
                                                                <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlcandidate" runat="server" CssClass="form-control"></asp:DropDownList>

                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlcandidate" Display="Dynamic"
                                                                        ErrorMessage="Select Candidate" Operator="NotEqual" ValidationGroup="b" ValueToCompare="--Select Candidate--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Candidate" ForeColor="Red" ControlToValidate="ddlcandidate" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">To :
                                                                <asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                </td>

                                                                <td colspan="2" valign="top" height="50px">
                                                                    <asp:DropDownList ID="ddhhT" runat="server" CausesValidation="True" Width="75px" TabIndex="34">
                                                                        <asp:ListItem Value="HH">HH</asp:ListItem>
                                                                        <asp:ListItem Value="01">01</asp:ListItem>
                                                                        <asp:ListItem Value="02">02</asp:ListItem>
                                                                        <asp:ListItem Value="03">03</asp:ListItem>
                                                                        <asp:ListItem Value="04">04</asp:ListItem>
                                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                                        <asp:ListItem Value="06">06</asp:ListItem>
                                                                        <asp:ListItem Value="07">07</asp:ListItem>
                                                                        <asp:ListItem Value="08">08</asp:ListItem>
                                                                        <asp:ListItem Value="09">09</asp:ListItem>
                                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                                    </asp:DropDownList>



                                                                    <asp:DropDownList ID="ddmmT" runat="server" CausesValidation="True" Width="75px" TabIndex="35">
                                                                        <asp:ListItem Value="MM">MM</asp:ListItem>
                                                                        <asp:ListItem Value="00">00</asp:ListItem>
                                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                                        <asp:ListItem Value="35">35</asp:ListItem>
                                                                        <asp:ListItem Value="40">40</asp:ListItem>
                                                                        <asp:ListItem Value="45">45</asp:ListItem>
                                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                                        <asp:ListItem Value="55">55</asp:ListItem>
                                                                    </asp:DropDownList>


                                                                    &nbsp;<asp:DropDownList ID="ddampmT" runat="server"
                                                                        Width="75px" TabIndex="36">
                                                                        <asp:ListItem Selected="True" Value="AM">AM</asp:ListItem>
                                                                        <asp:ListItem Value="PM">PM</asp:ListItem>
                                                                    </asp:DropDownList><br />
                                                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="ddhhT"
                                                                        ForeColor="Red" ErrorMessage="Select Hour" Operator="NotEqual" ValidationGroup="b"
                                                                        ValueToCompare="HH"></asp:CompareValidator>
                                                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="ddmmT"
                                                                        ForeColor="Red" ErrorMessage="Select Minutes" Operator="NotEqual" ValidationGroup="b"
                                                                        ValueToCompare="MM"></asp:CompareValidator>
                                                                    &nbsp;<asp:Label ID="Label10" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnaddcandidate" runat="server" CssClass="btn bg-blue-active" Text="Add" ValidationGroup="b" OnClick="btnaddcandidate_Click" />
                                                                </td>

                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan="4">
                                                        <asp:GridView ID="grdCan" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" Width="99%">
                                                            <Columns>
                                                                <asp:BoundField DataField="VacancyName" HeaderText="Position" />
                                                                <asp:BoundField DataField="CandidateName" HeaderText="Candidate Name" />
                                                                <asp:BoundField DataField="FromTime" HeaderText="Time From" />

                                                                <asp:BoundField DataField="ToTime" HeaderText="Time To" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("VacancyId")+","+Eval("CandidateId") %>'
                                                                            Height="20px" ImageUrl="~/Images/i_edit.png" OnClick="imgeditmult_Click" Width="20px" />
                                                                        <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%# Eval("VacancyId")+","+Eval("CandidateId") %>'
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

                                <table width="100%" align="Center">
                                    <tr>

                                        <td align="center">
                                            <%--<asp:UpdatePanel ID="UpdatePanel_ActionButtons" runat="server">
                                                <ContentTemplate>--%>
                                                    <asp:Button ID="btnsave" runat="server" CssClass="btn bg-blue-active" Text="Save" ValidationGroup="S" OnClick="btnsave_Click"  UseSubmitBehavior="false" />
                                                   <%-- <asp:Button ID="Button1" runat="server" Text="Approve" OnClick="Proccess_Click1" OnClientClick="this.disabled = true; this.value = 'Processing';" UseSubmitBehavior="false" />--%>
                                               <asp:Button ID="btnCancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel" OnClick="btnCancel_Click" />
                                                    
                                            <%--         </ContentTemplate>
                                            </asp:UpdatePanel>--%>

                                            
                                     

                                           <%-- <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel_ActionButtons">
                                                <ProgressTemplate>
                                                    <div style="background-color: Gray; filter: alpha(opacity=80); opacity: 0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 800px;"></div>
                                                    <div style="filter: alpha(opacity=100); position: fixed; z-index: 100001; left: 720px; top: 105px;">
                                                        <div style="width: 50px; height: 50px; padding: 50px; background-color: white; border: solid 1px black;">
                                                            <img alt="progress" src="~/img/ajax-loader.gif" />
                                                       
                                                        </div>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>--%>
                                        </td>
                                    </tr>
                                </table>
                                <div style="display: none">
                                    <asp:GridView ID="grddisp" runat="server">
                                    </asp:GridView>

                                    <asp:HiddenField ID="HiddenFieldScheduledDetailsId" runat="server" />
                                    <asp:HiddenField ID="HiddenScheduledID" runat="server" />
                                    <asp:HiddenField ID="HiddenFieldCandidateID" runat="server" />
                                </div>
                                <div style="display: none">
                                    <asp:GridView ID="grdSendmailtoInterviewer" runat="server">
                                    </asp:GridView>


                                </div>

                            </div>
                        </div>
                    </div>


                </div>

            </section>
</asp:View>
</asp:MultiView>

    
       </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

