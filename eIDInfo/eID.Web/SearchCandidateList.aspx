<%@ Page Title="Candidate List" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="SearchCandidateList.aspx.cs" Inherits="Recruitment_SearchCandidateList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-body">
                <div class="form-group">
                 
                    <asp:Panel ID="pnalset" runat="server" DefaultButton="btnsearch">
                        <table width="100%">
                            <tr>
                                <td>Position
                                <asp:Label ID="lbll" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td>Experience
                                </td>
                                <td>Salary
                                </td>
                                <td>Skills
                                </td>
                                <td>Company
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlposition" runat="server" CssClass="form-control"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtexpereince" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtexpereince_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                        Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtexpereince"
                                        CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetCompletionList">
                                    </asp:AutoCompleteExtender>

                                    <asp:FilteredTextBoxExtender ID="txtexpereince_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="txtexpereince" ValidChars="0123456789">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsalary" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtsalary_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                        Enabled="True" ServiceMethod="GetCompletionListsalary" ServicePath="" EnableCaching="true"
                                        TargetControlID="txtsalary" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                    </asp:AutoCompleteExtender>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        Enabled="True" TargetControlID="txtsalary" ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>



                                </td>
                                <td>
                                    <asp:TextBox ID="txtskills" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtskills_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                        Enabled="True" ServiceMethod="GetCompletionListSkills" ServicePath="" EnableCaching="true"
                                        TargetControlID="txtskills" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                    </asp:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control" OnTextChanged="txtcompany_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtcompany_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                        Enabled="True" ServiceMethod="GetCompletionListCompany" ServicePath="" EnableCaching="true"
                                        TargetControlID="txtcompany" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                    </asp:AutoCompleteExtender>
                                    <asp:Label ID="lblcompaniId" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active"
                                        Text="Search" OnClick="btnsearch_Click" UseSubmitBehavior="False" />
                                </td>
                                <td>
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn bg-blue-active"
                                        Text="Reset" OnClick="btnReset_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                <asp:Label ID="lbup" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table width="100%" cellspacing="8px">
                        <tr>
                            <td align="right" style="padding-right: 20px">
                                <asp:Label ID="Label14" runat="server" Text="No. Of Count :" Font="Bold"></asp:Label>
                                &nbsp;
                                    <asp:Label ID="lblcount" runat="server" Font="Bold">0</asp:Label>
                                <asp:Label ID="lblcandidateID" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblviewId" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdcandidatelist" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" Width="99%" DataKeyNames="Candidate_ID" OnPageIndexChanging="grdcandidatelist_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                        <%-- <asp:BoundField DataField="Entry_Date" HeaderText="Entry Date" DataFormatString="{0:MM/dd/yyyy}" />--%>
                                        <asp:BoundField DataField="Vacancy" HeaderText="Position" />
                                        <asp:BoundField DataField="Name" HeaderText="Candidate Name" />
                                        <%-- <asp:BoundField DataField="CandidateId_No" HeaderText="CandidateId No." />--%>
                                        <asp:BoundField DataField="DOB" HeaderText="DOB" DataFormatString="{0:MM/dd/yyyy}" />
                                        <%--<asp:BoundField DataField="Religion" HeaderText="Religion" />
                                            <asp:BoundField DataField="Nationality" HeaderText="Nationality" />--%>
                                        <asp:BoundField DataField="Contact_No" HeaderText="Contact No." />
                                        <asp:BoundField DataField="Email_Address" HeaderText="Email Id" />

                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="Imagedownloadfile" runat="server" CommandArgument='<%# Eval("Candidate_ID") %>'
                                                    Height="20px" ImageUrl="~/Images/load.png" Width="20px" ToolTip="Download File" OnClick="Imagedownloadfile_Click" />

                                            </ItemTemplate>
                                            <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="Imageviewmoredetails" runat="server" CommandArgument='<%# Eval("Candidate_ID") %>'
                                                    Height="25px" ImageUrl="~/Images/View.png" Width="25px" ToolTip="View more details" OnClick="Imageviewmoredetails_Click" />

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
                

                    <table id="Table1" class="modalPopup" width="600px" style="border-radius: 0 0 20px 20px; box-shadow: 10px 10px 10px; border-width: 1px; background-color: #F0F2ED; height: 100px; overflow-y: scroll">
                        <tr style="background-color: #CFA071">

                            <td align="center" style="color: Black; background-color: White;">
                                <div style="text-align: right; height: 30px; float: right;">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Close.jpg" Height="25px"
                                        Width="25px" OnClick="ImageButton2_Click" />
                                </div>
                                <div style="font-family: 'COMic Sans MS'; font-size: large;">Details Of Candidate</div>
                                <div style="height: 300px;">
                                    <div class="col-md-12" style="height: 550px; overflow-y: scroll">
                                        <div class="box box-primary">

                                            <div class="box-body">
                                                <div class="form-group">

                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                               <%-- <fieldset>
                                                                    <legend>Candidate Info</legend>--%>
                                                                    <table id="tbl1" runat="server" width="350px" cellspacing="8" cellpadding="5">

                                                                        <tr>
                                                                            <td>Company Name
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblcompanyname" runat="server" Text="---------------"></asp:Label>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>Vacancy 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblposition" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Candidate Name 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblname" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>DOB 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblDob" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Gender
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblgender" runat="server" Text="---------------"></asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Contact No. 
                                     
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblcontactno" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Candidate Id
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblcandidateidview" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Marital Status
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblmeritalstatus" runat="server" Text="---------------"></asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Religion 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblreligion" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Nationality
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblnationality" runat="server" Text="---------------"></asp:Label>
                                                                                <br />

                                                                            </td>
                                                                        </tr>
                                                                        
                                                                       

                                                                    </table>
                                                               <%-- </fieldset>--%>
                                                            </td>

                                                            <td valign="top">
                                                                <table width="100%" cellspacing="8px" cellpadding="5px">
                                                                   <%-- <tr>
                                                                        <td colspan="2">&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">&nbsp;
                                                                        </td>
                                                                    </tr>--%>
                                                                    <tr>
                                                                        <td>State
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCstate" runat="server" Text="---------------"></asp:Label>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>City
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCcity" runat="server" Text="---------------"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Zip Code
                                       

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCzipcode" runat="server" Text="---------------"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Landmark

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblClandmark" runat="server" Text="---------------"></asp:Label>
                                                                        </td>
                                                                    </tr>


                                                                    <tr>
                                                                        <td>State
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPstate" runat="server" Text="---------------"></asp:Label>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>City
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPcity" runat="server" Text="---------------"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Zip Code
                                       

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPzipcode" runat="server" Text="---------------"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Landmark

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPlandmark" runat="server" Text="---------------"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                            <td>Email 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblemail" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>

                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <fieldset>
                                                                    <legend>Profession</legend>
                                                                    <table width="350px" cellspacing="8px" cellpadding="5px">
                                                                        <tr>
                                                                            <td>Total Experience 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblyearexp" runat="server" Text="---------------"></asp:Label>
                                                                                Year
                                                
                                             <asp:Label ID="lblmonthexp" runat="server" Text="---------------"></asp:Label>
                                                                                Month
                                                
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Relevant Experience 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblrelYear" runat="server" Text="---------------"></asp:Label>
                                                                                Year
                                                
                                              <asp:Label ID="lblrelmonth" runat="server" Text="---------------"></asp:Label>
                                                                                Month
                                                
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>Notice Period(In days)
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblnoticeperiod" runat="server" Text="---------------"></asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Skills
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblskills" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>


                                                                    </table>
                                                                </fieldset>
                                                            </td>

                                                            <td valign="top">
                                                                <fieldset>
                                                                    <legend>Other Info</legend>
                                                                    <table width="350px" cellspacing="8px" cellpadding="5px">
                                                                        <tr>
                                                                            <td>Current CTC
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblCTC" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Expected CTC
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblexpectedctc" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Reference By
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblrefrenceby" runat="server" Text="---------------"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <table width="100%">
                                                        <tr>
                                                            <td>


                                                                <fieldset>
                                                                    <legend>Education Details</legend>
                                                                    <table width="100%" cellspacing="8px">
                                                                        <tr>

                                                                            <td colspan="3">
                                                                                <asp:Panel ID="pnl" runat="server" ScrollBars="Vertical">
                                                                                    <asp:GridView ID="grdeducationview" runat="server"
                                                                                        AutoGenerateColumns="false" DataKeyNames="Candidate_ID" CssClass="table table-bordered table-striped" Width="99%">

                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="Education_Name" HeaderText="Education" />
                                                                                            <asp:BoundField DataField="Passing_Year" HeaderText="Year Of Passing" />
                                                                                            <asp:BoundField DataField="University" HeaderText="University" />
                                                                                            <asp:BoundField DataField="Percentage" DataFormatString="{0:0.00}" HeaderText="Percent" />
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




</asp:Content>

