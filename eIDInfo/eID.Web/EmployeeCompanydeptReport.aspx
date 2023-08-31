<%@ Page Title="Companywise Employee Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="EmployeeCompanydeptReport.aspx.cs" Inherits="EmployeeCompanydeptReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="utc" runat="server">
        <ContentTemplate>--%>
         
                        <table width="100%">


                            <tr>
                                <section class="content-header">
                                    <caption>
                                        <div style="float: left">


                                            <h3>Company Wise Employee Report</h3>
                                        </div>
                                    </caption>
                                </section>
                            </tr>
                            <tr>
                                <td>
                                    <div class="col-md-12">
                                        <asp:Panel ID="pann" runat="server" DefaultButton="btnsearch">
                                            <div class="box box-primary">

                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <fieldset>
                                                            <legend></legend>
                                                            <table width="60%">

                                                                <tr>
                                                                    <td>From Date</td>
                                                                    <td>To Date</td>
                                                                    <td>Company Name</td>
                                                                    <td> &nbsp;&nbsp;</td>
                                                                    </tr>
                                                                <tr>

                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtfdate" ValidationGroup="a" Width="180px" CssClass="form-control" />
                                                                        <asp:CalendarExtender ID="txtfdate_CalendarExtender" runat="server"
                                                                            Enabled="True" TargetControlID="txtfdate">
                                                                        </asp:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                            ControlToValidate="txtfdate" Display="None" ErrorMessage="Required Field"
                                                                            SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                        <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                                            runat="server" TargetControlID="RequiredFieldValidator1">
                                                                        </asp:ValidatorCalloutExtender>
                                                                    </td>

                                                                   

                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txttdate" ValidationGroup="a" Width="180px" CssClass="form-control" />
                                                                        <asp:CalendarExtender ID="txttdate_CalendarExtender" runat="server"
                                                                            Enabled="True" TargetControlID="txttdate">
                                                                        </asp:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                            ControlToValidate="txttdate" Display="None" ErrorMessage="Required Field"
                                                                            SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                        <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                                                            runat="server" TargetControlID="RequiredFieldValidator2">
                                                                        </asp:ValidatorCalloutExtender>
                                                                    </td>
                                                               
                                                                    <td><asp:DropDownList ID="ddcomp" runat="server" Width="180px" CssClass="form-control"></asp:DropDownList>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                                            ControlToValidate="ddcomp" Display="None" ErrorMessage="Required Field"
                                                                            Operator="NotEqual" SetFocusOnError="True" ValidationGroup="a"
                                                                            ValueToCompare="--Select--"></asp:CompareValidator>
                                                                        <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender"
                                                                            runat="server" TargetControlID="CompareValidator1">
                                                                        </asp:ValidatorCalloutExtender>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;&nbsp;
                                                                    </td>

                                                                      </tr>
                                                                </table>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <div style="margin-top: 15px;">
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        </div>
                                                                    </td>

                                                                    <td >
                                                                        <div style="margin-top: 15px;">
                                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn bg-blue-active"
                                                                            OnClick="btnSearch_Click" ValidationGroup="a" />
                                                                            </div>
                                                                        </td>
                                                                    <td>
                                                                        <div style="margin-top: 15px;">
                                                                        <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel"
                                                                            CausesValidation="False" />
                                                                            </div>
                                                                       </td>
                                                                    <td>
                                                                        <div style="margin-top: 15px;">
                                                                        <asp:Button ID="btnPrintCurrent" runat="server" CssClass="btn bg-blue-active" Text="Print Current Page"
                                                                            OnClick="PrintCurrentPage" ValidationGroup="a" Visible="False" />
                                                                            </div>
                                                                        </td>
                                                                    <td>
                                                                        <div style="margin-top: 15px;">
                                                                        <asp:Button ID="btnExpPDF" runat="server" CssClass="btn bg-blue-active" OnClick="btnExpPDF_Click"
                                                                            Text="Export to PDF" ValidationGroup="a" />
                                                                            </div>
                                                                        </td>
                                                                    <td>
                                                                        <div style="margin-top: 15px;">
                                                                        <asp:Button ID="btnExpExcel" runat="server" CssClass="btn bg-blue-active" Text="Export to Excel"
                                                                            OnClick="btnExpExcel_Click" ValidationGroup="a" />
                                                                            </div>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                            <table id="tbldisp" runat="server" width="100%">
                                                                <tr>
                                                                    <td style="float: right" align="right">

                                                                        <b>
                                                                            <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>
                                                                            <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <asp:GridView ID="gv" CssClass="table table-bordered table-striped" runat="server"
                                                                            AutoGenerateColumns="False" AllowPaging="True"
                                                                            OnPageIndexChanging="gv_PageIndexChanging" OnSorting="gv_Sorting">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sr.No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text="<%#Container.DataItemIndex+1 %>" ID="lbl" runat="server" />
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>

                                                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name"
                                                                                    SortExpression="EmployeeName" />
                                                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name"
                                                                                    SortExpression="CompanyName" />
                                                                                <asp:BoundField DataField="DesigName" HeaderText="Designation Name"
                                                                                    SortExpression="DesigName" />
                                                                                <asp:BoundField DataField="DeptName" HeaderText="Department Name" SortExpression="DeptName" />
                                                                                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                                                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                                                <asp:BoundField DataField="ContactNo" HeaderText="ContactNo"
                                                                                    SortExpression="ContactNo" />


                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                No Data Exist!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                                            </EmptyDataTemplate>

                                                                        </asp:GridView>

                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </fieldset>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>

                
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

