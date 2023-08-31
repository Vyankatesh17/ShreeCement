<%@ Page Title="Agency-wise Vacancy Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="VacancyReportAgencyWise.aspx.cs" Inherits="Recruitment_VacancyReportAgencyWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-12">
        <div class="box box-primary">

            <div class="box-body">
                <div class="form-group">

                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="450px" cellspacing="8px">

                                    <tr>
                                        <td>
                                            <div class="form-group">From Date : </div>
                                        </td>
                                        <td>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnTextChanged="txtfromdate_TextChanged"></asp:TextBox>

                                                <asp:CalendarExtender ID="dtprelivedate_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtfromdate" Format="MM/dd/yyyy">
                                                </asp:CalendarExtender>


                                            </div>



                                        </td>
                                        <td>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                ControlToValidate="txtfromdate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr id="tragency" runat="server">
                                        <td>
                                            <div class="form-group">Agency Wise :</div>
                                        </td>
                                        <td>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtagencywise" runat="server" CssClass="form-control" Width="250px"
                                                    OnTextChanged="txtagencywise_TextChanged"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txtagencywise_AutoCompleteExtender" runat="server"
                                                    DelimiterCharacters="" EnableCaching="true"
                                                    Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtagencywise"
                                                    CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetCompletionList">
                                                </asp:AutoCompleteExtender>
                                               
                                            </div>
                                        </td>

                                    </tr>






                                </table>
                            </td>
                            <td>
                                <table width="450px" cellspacing="8px">
                                    <tr>
                                        <td>
                                            <div class="form-group">To Date : </div>
                                        </td>
                                        <td>



                                            <div class="form-group">
                                                <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" Width="270px" AutoPostBack="True" OnTextChanged="txtTodate_TextChanged"></asp:TextBox>



                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                    PopupButtonID="calimagedob1" TargetControlID="txtTodate" Format="MM/dd/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </td>
                                        <td>



                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red"
                                                ControlToValidate="txtTodate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Search" OnClick="btnSearch_Click" ValidationGroup="S" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblagencyId" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="padding-right: 30px">
                                <b>
                                    <asp:Label ID="Label14" runat="server" Text="No. Of Count :" Font="Bold"></asp:Label>
                                    &nbsp;
                               <asp:Label ID="lblcount" runat="server" Font="Bold">0</asp:Label>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pangrid" runat="server" ScrollBars="Vertical" Height="500px">
                                    <asp:GridView ID="grdVacancy" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-striped" ShowFooter="True" OnRowDataBound="grdVacancy_RowDataBound" AllowPaging="True" OnPageIndexChanging="grdVacancy_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate><FooterStyle BackColor="LightGray" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CompanyName">
                                                <ItemTemplate>
                                                    <asp:Label ID="CompanyName" runat="server" Text='<%#Eval("CompanyName")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="CompanyName" runat="server" Text="" />
                                                </FooterTemplate>  <FooterStyle BackColor="LightGray" />
                                            </asp:TemplateField>


                                          <%--  <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                         
 <asp:TemplateField HeaderText="Position">
                                                <ItemTemplate>
                                                    <asp:Label ID="position" runat="server" Text='<%#Eval("position")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="position" runat="server" Text="" />
                                                </FooterTemplate>  <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Quota">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuota" runat="server" Text='<%#Eval("Quota")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotQuota" runat="server" Text="" />
                                                </FooterTemplate> <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Selected">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lblselected" runat="server" Text='<%# Eval("Selected") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="Black"></asp:LinkButton>--%>
                                                    <asp:Label ID="lblselected" runat="server" Text='<%#Eval("Selected")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotSel" runat="server" Text="" />
                                                </FooterTemplate>  <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rejected">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lblRejected" runat="server" Text='<%# Eval("Rejected") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="Black"></asp:LinkButton>--%>
                                                    <asp:Label ID="lblRejected" runat="server" Text='<%#Eval("Rejected")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotRej" runat="server" Text="" />
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Hold">
                                                <ItemTemplate>
                                                    <%-- <asp:LinkButton ID="lblHold" runat="server" Text='<%# Eval("Hold") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="Black"></asp:LinkButton>--%>
                                                    <asp:Label ID="lblHold" runat="server" Text='<%#Eval("Hold")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotHold" runat="server" Text="" />
                                                </FooterTemplate>  <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Offer">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lblOffer" runat="server" Text='<%# Eval("Offer") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="Black"></asp:LinkButton>--%>
                                                    <asp:Label ID="lblOffer" runat="server" Text='<%#Eval("Offer")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotOffer" runat="server" Text="" />
                                                </FooterTemplate>  <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Interview Done">
                                                <ItemTemplate>
                                                    <%-- <asp:LinkButton ID="lblIntSched" runat="server" Text='<%# Eval("InterviewDone") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="Black"></asp:LinkButton>--%>
                                                    <asp:Label ID="lblIntSched" runat="server" Text='<%#Eval("InterviewDone")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotIntSched" runat="server" Text="" />
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Interview Lineup">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lblTotInt" runat="server" Text='<%# Eval("InterviewLineup") %>' CommandArgument='<%#Eval("CompanyID") +","+Eval("VacancyId")%>' ForeColor="Black"></asp:LinkButton>--%>
                                                    <asp:Label ID="lblTotInt" runat="server" Text='<%#Eval("InterviewLineup")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGrandTotInt" runat="server" Text="" />
                                                </FooterTemplate> <FooterStyle Font-Bold="true" BackColor="LightGray" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataTemplate>No Data Found....!!!</EmptyDataTemplate>
                                        <FooterStyle BackColor="#CCCCCC" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>


    </div>

</asp:Content>

