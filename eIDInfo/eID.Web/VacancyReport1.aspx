<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="VacancyReport1.aspx.cs" EnableEventValidation="false" Inherits="Recruitment_VacancyReport1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="box box-primary">

            <div class="box-body">
                <div class="form-group">
                    <fieldset>
                        <legend>Vacancy Report</legend>
                    </fieldset>
                    
                                <table width="100%" cellspacing="8px">

                                    <tr>
                                        <td valign="top">From Date :
                                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                        
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" MaxLength="60" AutoPostBack="True" OnTextChanged="txtfromdate_TextChanged" BackColor="White"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter From Date" ForeColor="Red" ControlToValidate="txtfromdate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                           <%-- <asp:MaskedEditExtender ID="chkOut_MaskedEditExtender" runat="server"
                                                Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                TargetControlID="txtfromdate" UserDateFormat="MonthDayYear">
                                            </asp:MaskedEditExtender>--%>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtfromdate">
                                            </asp:CalendarExtender>

                                        </td>
                                        <td valign="top">To Date :
                                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                        
                                            <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" MaxLength="60" AutoPostBack="True" OnTextChanged="txtTodate_TextChanged" BackColor="White"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter To Date" ForeColor="Red" ControlToValidate="txtTodate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            <%--<asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                TargetControlID="txtTodate" UserDateFormat="MonthDayYear">
                                            </asp:MaskedEditExtender>--%>
                                            <asp:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                                TargetControlID="txtTodate">
                                            </asp:CalendarExtender>

                                        </td>
                                        <td valign="top">Company Name
                                            <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control" OnTextChanged="txtcompany_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtcompany_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                Enabled="True" ServiceMethod="GetCompletionListCompany" ServicePath="" EnableCaching="true"
                                                TargetControlID="txtcompany" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                            </asp:AutoCompleteExtender>
                                            <asp:Label ID="lblcompaniId" runat="server" Visible="false"></asp:Label>
                                        </td>
                                   
                                        <td valign="top">Position
                                            <asp:TextBox ID="txtposition" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtposition_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtenderPosition" runat="server" DelimiterCharacters=""
                                                Enabled="True" ServiceMethod="GetCompletionListPosition" ServicePath="" EnableCaching="true"
                                                TargetControlID="txtposition" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                            </asp:AutoCompleteExtender>
                                            <asp:Label ID="lblvacancyID" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>



                        <tr>
                            <td colspan="4"><br />
                                <center><asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Search" OnClick="btnSearch_Click" ValidationGroup="S" /></center>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">

                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pangrid" runat="server" ScrollBars="Vertical" Height="500px">
                                    <asp:GridView ID="grdVacancy" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-striped" OnRowDataBound="grdVacancy_RowDataBound" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="position" HeaderText="Position" />

                                            <asp:TemplateField HeaderText="Quota">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuota" runat="server" Text='<%#Eval("Quota")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotQuota" runat="server" Text="" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Selected">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblselected" runat="server" Text='<%# Eval("Selected") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="#0066FF" OnClick="lnkSelect_Click"></asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotSel" runat="server" Text="" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rejected">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblRejected" runat="server" Text='<%# Eval("Rejected") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="#0066FF" OnClick="lnkRejected_Click"></asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotRej" runat="server" Text="" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Hold">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblHold" runat="server" Text='<%# Eval("Hold") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="#0066FF" OnClick="lnkHold_Click"></asp:LinkButton>
                                                   
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotHold" runat="server" Text="" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Offer">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblOffer" runat="server" Text='<%# Eval("Offer") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="#0066FF" OnClick="lnkOffer_Click"></asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotOffer" runat="server" Text="" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Interview Done">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblIntSched" runat="server" Text='<%# Eval("InterviewDone") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="#0066FF" OnClick="lnkInterviewDone_Click"></asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotIntSched" runat="server" Text="" />
                                                </FooterTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Interview Lineup">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblTotInt" runat="server" Text='<%# Eval("InterviewLineup") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="#0066FF" OnClick="lnkInterviewLineup_Click"></asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGrandTotInt" runat="server" Text="" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataTemplate>No Data Found !!!!!!!!!!</EmptyDataTemplate>
                                        <FooterStyle BackColor="#999999" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table id="Table1" class="modalPopup" width="600px" style="border-radius: 0 0 20px 20px; box-shadow: 10px 10px 10px; border-width: 1px; background-color: #F0F2ED; height: 100px; overflow-y: scroll;">
                        <tr style="background-color: #CFA071">

                            <td align="center" style="color: Black; background-color: White;">
                                <div style="text-align: right; height: 30px; float: right;">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Close.jpg" Height="25px"
                                        Width="25px" OnClick="ImageButton2_Click" />
                                </div>
                                <div style="font-family: 'COMic Sans MS'; font-size: large;">
                                    <asp:Label ID="lbltitle" runat="server" Text="Selected Candidate Details"></asp:Label>
                                </div>
                                <div style="height: 500px;">
                                    <div class="col-md-12" style="height: 500px; overflow-y: scroll">
                                        <div class="box box-primary">

                                            <div class="box-body">
                                                <div class="form-group">

                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <br />
                                                                <center>
                                                <asp:GridView  runat="server" ID="grdcandidate" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                                                   <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                       <asp:BoundField DataField="Name"  HeaderText="Candidate Name" />
                                                       <asp:BoundField DataField="Position"  HeaderText="Position" />
                                                       <asp:BoundField DataField="DOB"  HeaderText="DOB" DataFormatString="{0:MM/dd/yyyy}"/>
                                                       <asp:BoundField DataField="Contact_No"  HeaderText="Contact No" />
                                                       <asp:BoundField DataField="Email_Address"  HeaderText="Email Id" />
                                                       <asp:BoundField DataField="InterviewStatus"  HeaderText="Interview Status" />
                                                   </Columns>
                                                     <EmptyDataTemplate>
                                                        No Data Exist....!!!
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                </center>

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
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                    <asp:ModalPopupExtender ID="modnopo" runat="server" TargetControlID="Label1"
                        PopupControlID="Table1">
                    </asp:ModalPopupExtender>

                </div>
            </div>
        </div>


    </div>
</asp:Content>

