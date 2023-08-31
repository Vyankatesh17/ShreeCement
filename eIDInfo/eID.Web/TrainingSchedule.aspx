<%@ Page Title="Training Schedule" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="TrainingSchedule.aspx.cs" Inherits="TrainingSchedule" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

                <asp:MultiView ID="MultViewTraining" runat="server"
                    ActiveViewIndex="0">
                    <asp:View ID="ViewTrainingDetail" runat="server">

                        <asp:Panel ID="pannn" runat="server" DefaultButton="btnadd">
                            <table width="100%">
                                <tr>
                                    <td style="padding-left: 18px">
                                        <asp:Button ID="btnadd" runat="server" CssClass="btn bg-blue-active"
                                            Text="Add New" OnClick="btnadd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <div class="form-group">


                                        <div class="nav-tabs-custom">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#tab_1" data-toggle="tab">Pending Training Schedule By HR</a></li>
                                                <li><a href="#tab_2" data-toggle="tab">Scheduled Training</a></li>
                                            </ul>

                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tab_1">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right">
                                                                        <b>
                                                                            <asp:Label ID="Label1" runat="server" Text="No. of Counts :"> </asp:Label>
                                                                            <asp:Label ID="lblcnt1" runat="server">0</asp:Label></b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="grdTrainingDetail" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                            CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="CountryId" HeaderText="Country Id" Visible="false" />
                                                                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                                                                <asp:BoundField DataField="ReqDate" HeaderText="Request Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                                <%--    <asp:BoundField DataField="ReqDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                            <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}" />--%>
                                                                                <asp:BoundField DataField="Trainer" HeaderText="Trainer Name" />

                                                                                <asp:TemplateField HeaderText="Training Schedule">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ImageUrl="~/Images/a.png" Height="30px" Width="30px" ID="imgApprove" runat="server" Text="Approve" CommandArgument='<%# Eval("TraingID") %>'
                                                                                            ToolTip="Schedule Training" OnClick="imgApprove_Click" ForeColor="Blue" />

                                                                                        <%--                                                                    <asp:ConfirmButtonExtender ID="Edit_ConfirmButtonExtender" runat="server" ConfirmText="Schedule Training....?" Enabled="True" TargetControlID="imgApprove">
                                                                    </asp:ConfirmButtonExtender>--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <FooterStyle />
                                                                            <HeaderStyle />
                                                                            <PagerStyle HorizontalAlign="Right" />
                                                                            <RowStyle />
                                                                            <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle />
                                                                            <SortedAscendingHeaderStyle />
                                                                            <SortedDescendingCellStyle />
                                                                            <SortedDescendingHeaderStyle />
                                                                            <EmptyDataTemplate>
                                                                                No Data Exists....
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                        <asp:Label runat="server" ID="lbTrainingId"  Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="tab-pane" id="tab_2">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right">
                                                                        <b>
                                                                            <asp:Label ID="Label2" runat="server" Text="No. of Counts :"> </asp:Label>
                                                                            <asp:Label ID="lbScheduleTrainingCnt" runat="server">0</asp:Label></b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="grdScheduleTraining" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                            CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="CountryId" HeaderText="Country Id" Visible="false" />
                                                                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                                                                <asp:BoundField DataField="ReqDate" HeaderText="Schedule Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}" />

                                                                                <asp:BoundField DataField="StartTime" HeaderText="Start Time" DataFormatString="{0:MM/dd/yyyy}" />
                                                                                <asp:BoundField DataField="EndTime" HeaderText="End Time" DataFormatString="{0:MM/dd/yyyy}" />

                                                                                <asp:BoundField DataField="Trainer" HeaderText="Trainer Name" />
                                                                            </Columns>
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <FooterStyle />
                                                                            <HeaderStyle />
                                                                            <PagerStyle HorizontalAlign="Right" />
                                                                            <RowStyle />
                                                                            <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle />
                                                                            <SortedAscendingHeaderStyle />
                                                                            <SortedDescendingCellStyle />
                                                                            <SortedDescendingHeaderStyle />
                                                                            <EmptyDataTemplate>
                                                                                No Data Exists....
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>




                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="ViewAddNewTraining" runat="server">
                        <%--  <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                        --%>
                        <section class="content-header">
                            <h3>Training Schedule </h3>
                        </section>
                        <section class="content">
                            <div class="col-md-6">

                                <div class="box box-primary">
                                    <table width="90%">
                                        <caption>
                                            <br />
                                            <tr>
                                                <td valign="top">Training Date: </td>
                                                <td>
                                                    <asp:TextBox ID="txtTrainingDate" runat="server" CausesValidation="True" CssClass="form-control" MaxLength="40" ValidationGroup="w"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtTrainingDate">
                                                    </asp:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTrainingDate" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Training Title:<span style="color: red;"> *</span> </td>
                                                <td>
                                                    <asp:TextBox ID="txtTrainingTitle" runat="server" CausesValidation="True" CssClass="form-control" MaxLength="40" ValidationGroup="w"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTrainingTitle" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Cource Content: </td>
                                                <td>
                                                    <asp:TextBox ID="txtCource" runat="server" CausesValidation="True" CssClass="form-control" MaxLength="40" TextMode="MultiLine" ValidationGroup="w"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCource" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Required Reading: </td>
                                                <td>
                                                    <asp:TextBox ID="txtRequiredReading" runat="server" CausesValidation="True" CssClass="form-control" MaxLength="40" ValidationGroup="w"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRequiredReading" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Recommand Reading: </td>
                                                <td>
                                                    <asp:TextBox ID="txtRecommand" runat="server" CausesValidation="True" CssClass="form-control" MaxLength="40" ValidationGroup="w"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRecommand" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </caption>
                                    </table>
                                    <table width="95%">
                                        <tr>
                                            <td valign="top">Trainer Name:</td>
                                            <td>
                                                <asp:TextBox ID="txtTeacherName" runat="server" CausesValidation="True" CssClass="form-control" ValidationGroup="w" MaxLength="40"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRecommand" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator></td>
                                            <td valign="top" style="padding-left: 10px">Location:</td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtLocation" runat="server" CausesValidation="True" CssClass="form-control" ValidationGroup="w" MaxLength="40"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLocation" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator></td>
                                        </tr>

                                        <tr>
                                            <td valign="top">Start Date:</td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtStartDate" runat="server" CausesValidation="True" CssClass="form-control" ValidationGroup="w" MaxLength="40"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                    TargetControlID="txtStartDate">
                                                </asp:CalendarExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRecommand" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator></td>
                                            <td valign="top" style="padding-left: 10px">End Date:</td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtEndDate" runat="server" CausesValidation="True" CssClass="form-control" ValidationGroup="w" MaxLength="40"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
                                                    TargetControlID="txtEndDate">
                                                </asp:CalendarExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLocation" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator></td>
                                        </tr>

                                        <tr>
                                            <td valign="top" style="">Timing From :</td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtFromTime" runat="server" CausesValidation="True" CssClass="form-control" ValidationGroup="w" MaxLength="40"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtFromTime" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator></td>
                                            <td valign="top" style="padding-left: 10px">To:</td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtToTime" runat="server" CausesValidation="True" CssClass="form-control" ValidationGroup="w" MaxLength="40"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtToTime" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="w"></asp:RequiredFieldValidator></td>
                                        </tr>

                                    </table>

                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Button runat="server" Text="Add Employee" CssClass="btn bg-blue-active" ID="btnAddEmployee" OnClick="btnAddEmployee_Click" />
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </div>
                            <%-- </section>

                        <section class="content">--%>
                            <div runat="server" id="divEmployeeList" visible="false">
                                <div class="col-md-6">

                                    <div class="box box-primary">



                                        <table whdth="100%">

                                            <caption>
                                                <br />
                                                <tr>
                                                    <td valign="top">Company Name: </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>

                                                        <asp:AutoCompleteExtender ID="txtcompany_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                            Enabled="True" ServiceMethod="GetCompletionListCompany" ServicePath="" EnableCaching="true"
                                                            TargetControlID="txtCompanyName" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                        </asp:AutoCompleteExtender>

                                                        <br />
                                                        <br />
                                                    </td>
                                                    <td valign="top">Department: </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                            Enabled="True" ServiceMethod="GetCompletionListDepartment" ServicePath="" EnableCaching="true"
                                                            TargetControlID="txtDepartment" UseContextKey="True" CompletionInterval="1" MinimumPrefixLength="1">
                                                        </asp:AutoCompleteExtender>
                                                        <br />
                                                        <br />
                                                    </td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr style="width:300px">
                                                    <td>
                                                        <asp:Button ID="btnSerchEmployee" runat="server" CssClass="btn bg-blue-active" Text="Search" OnClick="btnSerchEmployee_Click" />
                                                        <asp:Button ID="btnReset" runat="server" CssClass="btn bg-blue-active" Text="Reset" />
                                                    </td>

                                                </tr>
                                            </caption>
                                        </table>

                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    <b>
                                                        <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>
                                                        <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdEmployeeList" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                        CssClass="table table-bordered table-striped" PageSize="10" OnPageIndexChanging="grdEmployeeList_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select Employee" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ToolTip='<%#Eval("EmployeeId") %>' ID="chkEmployee" runat="server" AutoPostBack="True" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id" Visible="false" />
                                                            <asp:BoundField DataField="emnae" HeaderText="Employee Name" />
                                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                            <asp:BoundField DataField="DeptName" HeaderText="Department" />
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <FooterStyle />
                                                        <HeaderStyle />
                                                        <PagerStyle HorizontalAlign="Right" />
                                                        <RowStyle />
                                                        <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle />
                                                        <SortedAscendingHeaderStyle />
                                                        <SortedDescendingCellStyle />
                                                        <SortedDescendingHeaderStyle />
                                                        <EmptyDataTemplate>
                                                            No Data Exists....
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button runat="server" Text="Save" CssClass="btn bg-blue-active" ID="btnSave" OnClick="btnSave_Click" />
                                                    <asp:Button runat="server" Text="Cancel" CssClass="btn bg-blue-active" ID="btnCancel" OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </section>

                    </asp:View>
                </asp:MultiView>
</asp:Content>

