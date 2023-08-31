<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="WPS_Report.aspx.cs" Inherits="WPS_Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="utc" runat="server">
        <ContentTemplate>--%>
    <table width="97%">
        <tr>
            <td>
                <div style="float: right">
                    <h4><small><a href="admin_dashboard.aspx">Dashboard</a>&nbsp; ><a href="#"> Reports</a>&nbsp; > &nbsp;<a href="WPS_Report.aspx">WPS Report</a>&nbsp;</small></h4>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">


        <tr>
            <section class="content-header">
                <caption>
                    <div style="float: left">


                        <h3>WPS Report</h3>
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
                                        <table width="55%">

                                            <tr>

                                                <td>Month:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlmonth" runat="server" Width="180px" CssClass="form-control">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>Jan</asp:ListItem>
                                                        <asp:ListItem>Feb</asp:ListItem>
                                                        <asp:ListItem>March</asp:ListItem>
                                                        <asp:ListItem>April</asp:ListItem>
                                                        <asp:ListItem>May</asp:ListItem>
                                                        <asp:ListItem>June</asp:ListItem>
                                                        <asp:ListItem>July</asp:ListItem>
                                                        <asp:ListItem>August</asp:ListItem>
                                                        <asp:ListItem>Sept</asp:ListItem>
                                                        <asp:ListItem>Oct</asp:ListItem>
                                                        <asp:ListItem>Nov</asp:ListItem>
                                                        <asp:ListItem>Dec</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                                <td>Year
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlyear" runat="server" Width="180px" CssClass="form-control">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>2012</asp:ListItem>
                                                        <asp:ListItem>2013</asp:ListItem>
                                                        <asp:ListItem>2014</asp:ListItem>
                                                        <asp:ListItem>2015</asp:ListItem>
                                                        <asp:ListItem>2016</asp:ListItem>
                                                        <asp:ListItem>2017</asp:ListItem>
                                                        <asp:ListItem>2018</asp:ListItem>
                                                        <asp:ListItem>2019</asp:ListItem>
                                                        <asp:ListItem>2020</asp:ListItem>
                                                        <asp:ListItem>2021</asp:ListItem>
                                                        <asp:ListItem>2022</asp:ListItem>
                                                        <asp:ListItem>2023</asp:ListItem>
                                                        <asp:ListItem>2024</asp:ListItem>
                                                        <asp:ListItem>2025</asp:ListItem>
                                                    </asp:DropDownList>

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

                                                <td>
                                                    <div style="margin-top: 15px;">
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn bg-blue-active"
                                                            ValidationGroup="a" OnClick="btnSearch_Click" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="margin-top: 15px;">
                                                        <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel"
                                                            OnClick="btncancel_Click" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="margin-top: 15px;">
                                                        <asp:Button ID="btnPrintCurrent" runat="server" CssClass="btn bg-blue-active" Text="Print Current Page"
                                                            ValidationGroup="a" Visible="False" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="margin-top: 15px;">
                                                        <asp:Button ID="btnExpPDF" runat="server" CssClass="btn bg-blue-active"
                                                            Text="Export to PDF" OnClick="btnExpPDF_Click" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="margin-top: 15px;">
                                                        <asp:Button ID="btnExpExcel" runat="server" CssClass="btn bg-blue-active" Text="Export to Excel"
                                                            OnClick="btnExpExcel_Click" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="margin-top: 15px;">
                                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn bg-blue-active" Text="Print" OnClick="btnPrint_Click" />
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
                                                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No">
                                                                <ItemTemplate>
                                                                    <asp:Label Text="<%#Container.DataItemIndex+1 %>" ID="lbl" runat="server" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name"
                                                                SortExpression="EmployeeName" />
                                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name"
                                                                SortExpression="CompanyName" />
                                                            <asp:BoundField DataField="DeptName" HeaderText="Department Name" SortExpression="DeptName" />
                                                            <asp:BoundField DataField="DesigName" HeaderText="Designation Name"
                                                                SortExpression="DesigName" />

                                                            <asp:BoundField DataField="workingdays" HeaderText="No Of Working Days" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="SalaryAccountNo" HeaderText="SalaryAccountNo" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="netpay" HeaderText="Salary"
                                                                SortExpression="netpay" ItemStyle-HorizontalAlign="Center" />
                                                             <asp:BoundField DataField="EmpCardID" HeaderText="Labour Card No."
                                                                 ItemStyle-HorizontalAlign="Center" />



                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" />
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

