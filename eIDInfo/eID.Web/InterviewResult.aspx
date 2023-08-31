<%@ Page Title="Interview Result" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="InterviewResult.aspx.cs" Inherits="Recruitment_InterviewResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <div class="col-md-12">
        <div class="box box-primary">

            <div class="box-body">
                <div class="form-group">
                    <asp:Panel ID="pnalset" runat="server" DefaultButton="btnsearch">
                            <table width="70%">
                                      <tr>
                                    <td style="width:70px">
                                        Search By :
                                    </td>
                                    <td style="width:120px">
                                        <asp:DropDownList ID ="ddlsortby" runat="server" CssClass="form-control" Width="170px" AutoPostBack="True" OnSelectedIndexChanged="ddlsortby_SelectedIndexChanged">
                                            <asp:ListItem>ALL</asp:ListItem>
                                               <asp:ListItem>Company-Wise</asp:ListItem>
                                               <asp:ListItem>Position-Wise</asp:ListItem>
                                               <asp:ListItem>Candidate-Wise</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:100px">
                                        &nbsp;
                                        <asp:Label ID="lblname" runat="server" Text="Name" Visible="false"></asp:Label>
                                    </td>
                                    <td style="width:350px">
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
                                <asp:GridView ID="grdschedule" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-striped" OnPageIndexChanging="grdschedule_PageIndexChanging">
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
                                                <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("ScheduleId")+","+Eval("ScheduleDetailsId") %>'
                                                     ImageUrl="~/Recruitment/Images/i_edit.png" OnClick="imgedit_Click" ToolTip="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>No Data Found !!!!!!!!!!!!!!!</EmptyDataTemplate>
                                </asp:GridView>
                                <asp:Label ID="lblschudleid" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblschudleDetailsId" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table id="Table1" class="modalPopup" width="600px" style="border-radius: 0 0 20px 20px; box-shadow: 10px 10px 10px; border-width: 1px; background-color: #F0F2ED; height: 300px;display:none">
                        <tr style="background-color: #CFA071">

                            <td align="center" style="color: Black; background-color: White;">
                                <div style="text-align: right; height: 30px; float: right;">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Close.jpg" Height="25px"
                                        Width="25px" OnClick="ImageButton2_Click" />
                                </div>
                                <div style="font-family: 'COMic Sans MS'; font-size: large;">Interview Result Details</div>
                                <div style="height: 370px;">
                                    <div class="col-md-12" style="height: 350px;">
                                        <div class="box box-primary">

                                            <div class="box-body">
                                                <div class="form-group">

                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <table width="450px" cellspacing="8px">
                                                                    <tr>
                                                                        <td valign="top">Company :
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlcompany" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlcompany" Display="Dynamic"
                                                                                ErrorMessage="*" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">Candidate :
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlCandidate" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>

                                                                            <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="ddlCandidate" Display="Dynamic"
                                                                                ErrorMessage="*" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>


                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">Status :
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                                                <asp:ListItem>Selected</asp:ListItem>
                                                                                <asp:ListItem>Hold</asp:ListItem>
                                                                                <asp:ListItem>Rejected</asp:ListItem>
                                                                                <asp:ListItem>Recomand for other Job</asp:ListItem>
                                                                                <asp:ListItem>Next Round</asp:ListItem>
                                                                            </asp:DropDownList>




                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trdate" runat="server" visible="false">
                                                                        <td valign="top">Next Date :
                                                                        </td>
                                                                        <td><div class="input-group" id="calimagedob">
                                                                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"   AutoPostBack="True" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                                            
                                                                                <div class="input-group-addon" runat="server" style="width:80px">
                                                                                    <i class="fa fa-calendar"></i>
                                                                                </div>

                                                                            </div>

                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtdate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                            <asp:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                                                                TargetControlID="txtdate"  PopupButtonID="calimagedob" Format="MM/dd/yyyy">
                                                                            </asp:CalendarExtender>

                                                                            <asp:Label ID="lblInterviewResultID" runat="server" Visible="false"></asp:Label>

                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trtime" runat="server" visible="false">
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
                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddmm"
                                                                                ForeColor="Red" ErrorMessage="Select Minutes" Operator="NotEqual" ValidationGroup="b"
                                                                                ValueToCompare="MM"></asp:CompareValidator>
                                                                            &nbsp;<asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                                                                        </td>

                                                                    </tr>


                                                                    <tr id="trjobrecom" runat="server" visible="false">
                                                                        <td valign="top">Position :
                                                                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:DropDownList ID="ddlpositionforRecoJob" runat="server" CausesValidation="True" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlpositionforRecoJob" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table width="450px" cellspacing="8px">
                                                                    <tr>
                                                                        <td valign="top">Vacancy :
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlvacancy" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>

                                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlvacancy" Display="Dynamic"
                                                                                ErrorMessage="*" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">Description :
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtdesc" runat="server" CssClass="form-control" TextMode="MultiLine" Style="resize: vertical; height: 55px;"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtdesc" ValidationGroup="S"></asp:RequiredFieldValidator>

                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tremp" runat="server" visible="false">
                                                                        <td valign="top">Interviewer :
                                                                            <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlemployee" runat="server" CssClass="form-control"></asp:DropDownList>

                                                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlemployee" Display="Dynamic"
                                                                                ErrorMessage="Select Employee" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Select Employee" ForeColor="Red" ControlToValidate="ddlemployee" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                                        </td>

                                                                    </tr>

                                                                </table>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                    <table width="100%" align="Center">
                                                        <tr>

                                                            <td align="center">
                                                                <asp:Button ID="btnsave" runat="server" CssClass="btn bg-blue-active" Text="Save" ValidationGroup="S" OnClick="btnsave_Click" />&nbsp;&nbsp;
                                     <asp:Button ID="btnCancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel" OnClick="btnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </td>
                        </tr>

                    </table>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                    <asp:ModalPopupExtender ID="modnopo" runat="server" TargetControlID="Label1"
                        PopupControlID="Table1">
                    </asp:ModalPopupExtender>
                </div>
            </div>
        </div>


    </div>


</asp:Content>

