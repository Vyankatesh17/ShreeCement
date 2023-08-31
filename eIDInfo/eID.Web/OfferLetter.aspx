<%@ Page Title="Offer Letter" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="OfferLetter.aspx.cs" Inherits="Recruitment_OfferLetter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .fancy-green .ajax__tab_header
        {
            background: url(img/blue_bg.gif) repeat-x;
            cursor: pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
            background: url(img/blue_left.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
            background: url(img/blue_right.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
            font-size: 13px;
            font-weight: bold;
            color: red;
            font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
            height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
            height: 46px;
            margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
            margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
            color: #fff;
        }
        .fancy .ajax__tab_body
        {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-12">
        <div class="box box-primary">

            <div class="box-body">
                <div class="form-group">
                    <fieldset>
                        <legend><%--Schedule Interview Details--%></legend>
                    </fieldset>
                       <asp:Panel ID="pnalset" runat="server" DefaultButton="btnsearch">
                                 <table width="100%">
                        <tr>
                            <td>Company
                            </td>
                            <td>Position
                                <asp:Label ID="lbll" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <asp:Label ID="lblpositionId" runat="server" Visible="False"></asp:Label>
                            </td>
                            
                            <td>Candidate Name
                            </td>
                            <td>Skills
                            </td>
                            <td>&nbsp;
                            </td>

                        </tr>
                        <tr>
                             <td>
                                <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control" OnTextChanged="txtcompany_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txtcompany_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServiceMethod="GetCompletionListCompany" ServicePath=""
                                    TargetControlID="txtcompany" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                </asp:AutoCompleteExtender>
                                <asp:Label ID="lblcompaniId" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td style="width:170px">
                                <asp:DropDownList ID="ddlposition" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlposition_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
                            </td>
                           
                            <td>
                                <asp:TextBox ID="txtcandidatename" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txtcandidatename_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServiceMethod="GetCompletionListCandidateName" ServicePath=""
                                    TargetControlID="txtcandidatename" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                </asp:AutoCompleteExtender>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    Enabled="True" TargetControlID="txtcandidatename" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtskills" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txtskills_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServiceMethod="GetCompletionListSkills" ServicePath=""
                                    TargetControlID="txtskills" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                </asp:AutoCompleteExtender>
                            </td>

                        <%--</tr>
                        <tr>--%>
                            <td>
                                <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active"
                                    Text="Search" OnClick="btnsearch_Click" UseSubmitBehavior="False" />
                                &nbsp;
                                <asp:Label ID="lbup" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                            </asp:Panel>
                                <br />
                    <asp:TabContainer ID="tabco" runat="server" ActiveTabIndex="0" CssClass="fancy fancy-green">
                        <asp:TabPanel ID="tabcon1" runat="server" HeaderText="Selected">
                            <ContentTemplate>
                          
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSid" runat="server" Visible="False"></asp:Label>
                                            <asp:GridView ID="grdselectedcandidate" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True"
                                                CssClass="table table-bordered table-striped" OnPageIndexChanging="grdselectedcandidate_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="Position" HeaderText="Position" />
                                                    <asp:BoundField DataField="Name" HeaderText="Candidate Name" />
                                                    <asp:BoundField DataField="Contact_No" HeaderText="Contact No." />
                                                    <asp:BoundField DataField="Date" HeaderText="Interview Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <asp:BoundField DataField="FromTime" HeaderText="From Time" />
                                                    <asp:BoundField DataField="ToTime" HeaderText="To Time" />
                                                    <asp:TemplateField HeaderText="Option">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="upd" runat="server">
                                                                <%--<Triggers>
                                                                    <asp:PostBackTrigger ControlID="lnkSendOffer" />
                                                                </Triggers>--%>
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lnkSendOffer" runat="server" Text="Send Offer" CommandArgument='<%# Eval("ScheduleDetailsId") %>' OnClick="lnkSendOffer_Click"></asp:LinkButton>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>No Data Found !!!!!!!!!!!!!!!</EmptyDataTemplate>
                                            </asp:GridView>
                                            <asp:Label ID="lblschudleid" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Offer Send">
                            <ContentTemplate>
                                <table width="100%">

                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdoffered" runat="server" AutoGenerateColumns="false" Width="100%"
                                                CssClass="table table-bordered table-striped" OnPageIndexChanging="grdoffered_PageIndexChanging" AllowPaging="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="Title" HeaderText="Position" />
                                                    <asp:BoundField DataField="Name" HeaderText="Candidate Name" />
                                                    <asp:BoundField DataField="Joining_Date" HeaderText="Joining Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <asp:BoundField DataField="offer_Date" HeaderText="Offer Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <asp:TemplateField HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="upd" runat="server">
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="imgVeiw" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="imgVeiw" runat="server" CommandArgument='<%# Eval("CadidateOffer_Id") %>'
                                                                        OnClick="imgVeiw_Click" ImageUrl="~/Recruitment/Images/View.png" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Option">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="upderwqrt" runat="server">
                                                                <%--<Triggers>
                                                                    <asp:PostBackTrigger ControlID="lnkSendOffer" />
                                                                </Triggers>--%>
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lnkRejectOffer" runat="server" Text="RejectOffer" CommandArgument='<%# Eval("CadidateOffer_Id") %>' OnClick="lnkRejectOffer_Click"></asp:LinkButton>
                                                                    &nbsp;
                                                                    <asp:LinkButton ID="lnkJoin" runat="server" Text="Join" CommandArgument='<%# Eval("CadidateOffer_Id") %>' OnClick="lnkJoin_Click"></asp:LinkButton>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>No Data Found !!!!!!!!!!!!!!!</EmptyDataTemplate>
                                            </asp:GridView>
                                            <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                    <table id="Table1" class="modalPopup" width="600px" style="border-radius: 0 0 20px 20px; box-shadow: 10px 10px 10px; border-width: 1px; background-color: #F0F2ED; height: 300px;">
                        <tr style="background-color: #CFA071">

                            <td align="center" style="color: Black; background-color: White;">
                                <div style="text-align: right; height: 30px; float: right;">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Close.jpg" Height="25px"
                                        Width="25px" OnClick="ImageButton2_Click" />
                                </div>
                                <div style="font-family: 'COMic Sans MS'; font-size: large;">Details Of Candidate</div>
                                <div style="height: 300px;">
                                    <div class="col-md-12" style="height: 530px;">
                                        <div class="box box-primary">

                                            <div class="box-body">
                                                <div class="form-group">

                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <fieldset>
                                                                    <legend>Candidate Info</legend>

                                                                    <table width="650px" cellspacing="8px" cellpadding="8px">

                                                                        <tr>
                                                                            <td>Name :
                                                                                <asp:Label ID="lblscheduleid" runat="server" Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblcandidateIdtosave" runat="server" Visible="false" Text="Label"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblname" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                            <td>Interview Date :
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblinterviewdate" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Company Name :
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblcompanyname" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                            <td>Position :
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblposition" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Contact No.: 
                                     
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblcontactno" runat="server" Text="---------------"></asp:Label>
                                                                            </td>

                                                                            <td>E-mail :
                                       

                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblemail" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>




                                                                    </table>

                                                                </fieldset>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <fieldset>
                                                                    <legend>Add Offer Details</legend>
                                                                    <table width="700px" cellspacing="8px">
                                                                        <tr>
                                                                            <td valign="top">Department :
                                                                                <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                                                <br />
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddldepartment" runat="server" TabIndex="1" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <%-- <asp:CompareValidator ID="cmp3" runat="server" ControlToValidate="ddldepartment" Display="Dynamic"
                                                                                    ForeColor="Red" ErrorMessage="Select Department" Operator="NotEqual" ValidationGroup="a"
                                                                                    ValueToCompare="--Select--"></asp:CompareValidator>--%>


                                                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddldepartment"
                                                                                    ErrorMessage="Select Department" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"
                                                                                    ForeColor="Red" Font-Size="9pt"></asp:CompareValidator>
                                                </div>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Department" Display="Static"
                                                    ForeColor="Red" ControlToValidate="ddldepartment" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>

                            <td valign="top">
                                <asp:Label ID="lblcompany_ID" runat="server" Visible="false"></asp:Label>
                                Designation :
                                                                            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                <br />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddldesig" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddldesig" Display="Static"
                                    ForeColor="Red" ErrorMessage="Select Designation" Operator="NotEqual" ValidationGroup="a"
                                    ValueToCompare="--Select--"></asp:CompareValidator>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidatodasdr" runat="server" ErrorMessage="Select Designation" Display="Static"
                                    ForeColor="Red" ControlToValidate="ddldesig" ValidationGroup="a"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Joining Date : <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                <br />
                            </td>
                            <td>
                                <div class="input-group" id="calimagedob">
                                    <asp:TextBox ID="txtJoiningdate" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtJoiningdate_TextChanged"></asp:TextBox>
                                    <div class="input-group-addon" runat="server" style="width: 80px">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                </div>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                    PopupButtonID="calimagedob" TargetControlID="txtJoiningdate" Format="MM/dd/yyyy">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorGFYUF1" runat="server" ErrorMessage="Enter Joining date" ForeColor="Red" ControlToValidate="txtJoiningdate" ValidationGroup="a"></asp:RequiredFieldValidator>



                            </td>
                            <td valign="top">Reporting :
                                                                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                <br />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlreporting" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlreporting" Display="Static"
                                    ForeColor="Red" ErrorMessage="Select Reporting Head" Operator="NotEqual" ValidationGroup="a"
                                    ValueToCompare="--Select--"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Salary : 
                                                                                <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>

                                <br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtsalary" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    Enabled="True" TargetControlID="txtsalary" ValidChars="0123456789.">
                                </asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Salary" ForeColor="Red" ControlToValidate="txtsalary" ValidationGroup="a"></asp:RequiredFieldValidator>

                            </td>
                            <td valign="top">Probation Period (month):
                                                                            <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                <br />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlprobetion" runat="server" CssClass="form-control">
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
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Remark :
                                                                                <br />
                            </td>
                            <td>
                                <asp:Label ID="lblvacancyid" runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </td>

                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnsendoffer" runat="server" CssClass="btn bg-blue-active" Text="Send Offer"
                                    ValidationGroup="a" OnClick="btnsendoffer_Click" CausesValidation="true" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
                            </td>
                        </tr>


                    </table>
                    </fieldset>
                                                            </td>


                                                        </tr>
                                                    </table>




                </div>
            </div>
        </div>
    </div>


                    <asp:Label ID="lblpopupid" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="modnopo" runat="server" TargetControlID="lblpopupid"
        PopupControlID="Table1">
    </asp:ModalPopupExtender>





    <table id="Table2" class="modalPopup" width="660px" style="border-radius: 0 0 20px 20px; box-shadow: 10px 10px 10px; border-width: 1px; background-color: #F0F2ED; height: 300px;">
        <tr style="background-color: #CFA071">

            <td align="center" style="color: Black; background-color: White;">
                <div style="text-align: right; height: 30px; float: right">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Close.jpg" Height="25px"
                        Width="25px" OnClick="ImageButton1_Click" />
                </div>
                <div style="font-family: 'COMic Sans MS'; font-size: large;">Offer Details</div>
                <div style="height: 250px;">
                    <div class="col-md-12" style="height: 430px;">
                        <div class="box box-primary">

                            <div class="box-body">
                                <div class="form-group">

                                    <table width="100%" align="center">
                                        <tr>
                                            <td valign="top">
                                                <fieldset>
                                                    <legend>Offer Details</legend>

                                                    <table width="630px" cellspacing="7px" cellpadding="5px" cssclass="table table-bordered table-striped">

                                                        <tr>
                                                            <td>Name :<br />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbloffercandidatename" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>
                                                            <td>Interview Date :
                                                                                <br />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblofferinterViewdate" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Company Name :
                                                                            <br />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblofferoffercompany" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>
                                                            <td>Position :
                                                                                <br />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblofferposition" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Contact No.: 
                                     
                                                                                <br />

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbloffercontactno" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>

                                                            <td>E-mail :
                                       
                                                                                <br />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblofferemail" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Department :
                                                                                
                                                                                
                                                                                <br />


                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDept" runat="server" Text="---------------"></asp:Label>

                                                                <br />

                                                            </td>

                                                            <td>Designation :
                                                                            
                                                                                <br />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDesig" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Offer Date :
                                                                               
                                                                               
                                                                                <br />


                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblofferdate" runat="server" Text="---------------"></asp:Label>

                                                                <br />

                                                            </td>

                                                            <td>Joining Date :
                                                                               <br />

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbljoiningDate" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>



                                                        </tr>

                                                        <tr>
                                                            <td>Reporting :
                                                                            
                                                                              
                                                                                <br />


                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblreporting" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>
                                                            <td>Salary : 
                                                                                <br />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsalary" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>

                                                        </tr>

                                                        <tr>
                                                            <td>Probation Period (month):
                                                                            
                                                                              
                                                                                <br />


                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblprobationperiod" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                            </td>
                                                            <td>Remark :
                                                                                <br />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblremark" runat="server" Text="---------------"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblidviewoffer" runat="server" Visible="false"></asp:Label>
                                                            </td>


                                                        </tr>


                                                    </table>

                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--<table width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <fieldset>
                                                                    <legend>Add Offer Details</legend>
                                                                    <table width="800px" cellspacing="8px">
                                                                       


                                                                    </table>
                                                                </fieldset>
                                                            </td>


                                                        </tr>
                                                    </table>--%>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </td>
        </tr>


    </table>
    <asp:Label ID="Label23" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="modpopup2" runat="server" TargetControlID="Label23"
        PopupControlID="Table2">
    </asp:ModalPopupExtender>




    <table id="TableReject" class="modalPopup" width="650px" style="border-radius: 0 0 20px 20px; box-shadow: 10px 10px 10px; border-width: 1px; background-color: #F0F2ED; height: 300px;">
        <tr style="background-color: #CFA071">

            <td align="center" style="color: Black; background-color: White;">
                <div style="text-align: right; height: 30px; float: right">
                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Close.jpg" Height="25px"
                        Width="25px" OnClick="ImageButton3_Click" />
                </div>
                <div style="font-family: 'COMic Sans MS'; font-size: large;">Reject Offer</div>
                <div style="height: 250px;">
                    <div class="col-md-12" style="height: 300px;">
                        <div class="box box-primary">

                            <div class="box-body">
                                <div class="form-group">

                                    <table width="100%" align="center">
                                        <tr>
                                            <td valign="top">
                                                <fieldset>
                                                    <legend>Reject Offer</legend>

                                                    <table width="100%" cellspacing="7px" cellpadding="5px" cssclass="table table-bordered table-striped">

                                                        <tr>
                                                            <td>Reject Date :
                                                              <br />
                                                            </td>
                                                            <td style="width:150px">
                                                                <asp:Label ID="lblrejectDate" runat="server" ></asp:Label>
                                                                <br />
                                                            </td>
                                                            <td>Remark :
                                                              <br />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtrejectRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                 <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Remark" ForeColor="Red" ControlToValidate="txtrejectRemark" ValidationGroup="rj"></asp:RequiredFieldValidator>

                                                               
                                                                <asp:Label ID="Label22" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="center">
                                                                <asp:Button ID="btnreject" runat="server" Text="Reject" CssClass="btn bg-blue-active" ValidationGroup="rj" OnClick="btnreject_Click" />
                                                                <asp:Button ID="btncancelreject" runat="server" Text="Cancel" CssClass="btn bg-blue-active" OnClick="btncancelreject_Click" />
                                                            </td>
                                                        </tr>

                                                    </table>

                                                </fieldset>
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
    <asp:Label ID="Label24" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="ModalPopupRejectoffer" runat="server" TargetControlID="Label24"
        PopupControlID="TableReject">
    </asp:ModalPopupExtender>




    <table id="Tablejoin" class="modalPopup" width="750px" style="border-radius: 0 0 20px 20px; box-shadow: 10px 10px 10px; border-width: 1px; background-color: #F0F2ED; height: 300px;">
        <tr style="background-color: #CFA071">

            <td align="center" style="color: Black; background-color: White;">
                <div style="text-align: right; height: 30px; float: right">
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Close.jpg" Height="25px"
                        Width="25px" OnClick="ImageButton4_Click" />
                </div>
                <div style="font-family: 'COMic Sans MS'; font-size: large;">Join Offer</div>
                <div style="height: 250px;">
                    <div class="col-md-12" style="height: 300px;">
                        <div class="box box-primary">

                            <div class="box-body">
                                <div class="form-group">

                                    <table width="100%" align="center">
                                        <tr>
                                            <td valign="top">
                                                <fieldset>
                                                    <legend>Join Offer</legend>

                                                    <table width="700px" cellspacing="7px" cellpadding="5px" cssclass="table table-bordered table-striped">

                                                        <tr>
                                                            <td valign="top">Join Date :
                                                                <asp:Label ID="Labeldfsgdfg14" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <div class="input-group" id="calimagedob1">
                                                                    <asp:TextBox ID="txtjoindate" runat="server" CssClass="form-control" Width="200px" AutoPostBack="True" OnTextChanged="txtjoindate_TextChanged"></asp:TextBox>
                                                                    <div class="input-group-addon" runat="server" style="width: 70px">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </div>

                                                                </div>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate" runat="server" ErrorMessage="Enter Joining Date" ForeColor="Red" ControlToValidate="txtjoindate" ValidationGroup="jn"></asp:RequiredFieldValidator>
                                                                <asp:CalendarExtender ID="txtjoindate_CalendarExtender" runat="server" Enabled="True"
                                                                    PopupButtonID="calimagedob1" TargetControlID="txtjoindate" Format="MM/dd/yyyy">
                                                                </asp:CalendarExtender>



                                                            </td>
                                                            <td valign="top">Remark :  <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                              <br />
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="txtjoinremark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                 <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Remark" ForeColor="Red" ControlToValidate="txtjoinremark" ValidationGroup="jn"></asp:RequiredFieldValidator>
                                                               
                                                                <asp:Label ID="Label9" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="center">
                                                                <asp:Button ID="btnjoin" runat="server" Text="Join" CssClass="btn bg-blue-active" ValidationGroup="jn" OnClick="btnjoin_Click" />
                                                                <asp:Button ID="btnjoincancel" runat="server" Text="Cancel" CssClass="btn bg-blue-active" OnClick="btnjoincancel_Click" />
                                                                <asp:HiddenField ID="HiddenFieldSchedulID" runat="server" />
                                                                <asp:HiddenField ID="HiddenFieldSchedulDetailsID" runat="server" />
                                                                <asp:HiddenField ID="HiddenFieldCandidateId" runat="server" />
                                                                <asp:HiddenField ID="HiddenFieldCompanyID" runat="server" />
                                                                <asp:HiddenField ID="HiddenFieldVacancyID" runat="server" />
                                                                <asp:HiddenField ID="HiddenFieldofferDate" runat="server" />
                                                            </td>
                                                        </tr>

                                                    </table>

                                                </fieldset>
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
    <asp:Label ID="Label10" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="ModalPopupJoin" runat="server" TargetControlID="Label10"
        PopupControlID="Tablejoin">
    </asp:ModalPopupExtender>


    <div style="display: none">
        <asp:GridView ID="grddisp" runat="server">
        </asp:GridView>
    </div>

    </div>
            </div>
        </div>


    </div>
    
</asp:Content>

