<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Attendance_Report_new.aspx.cs" Inherits="Attendance_Report_new" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <!DOCTYPE html>
    <head>
        <title>
            <h3>Manual Attendance Report </h3>
        </title>
    </head>
    <body>

        <div class="row">
            <div class="col-lg-12">
                <asp:Panel ID="pann" runat="server" DefaultButton="btnSearch">
                    <div class="box box-primary">

                        <div class="box-body">
                            <div class="form-group">
                                <table>
                                    <tr>
                                        <td>Month</td>
                                        <td>Year</td>
                                        <td>&nbsp;Company Name</td>
                                        <td>Employee Name
                                        </td>
                                    </tr>
                                   
                                    <tr>


                                        <td>
                                            <asp:DropDownList ID="ddmonth" runat="server" ValidationGroup="a" CssClass="form-control">
                                                <asp:ListItem>--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddmonth"
                                                Display="Dynamic" ErrorMessage="Select Month" Operator="NotEqual" SetFocusOnError="True"
                                                ValidationGroup="a" ValueToCompare="--Select--" ForeColor="Red"></asp:CompareValidator>

                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddyear" runat="server" CssClass="form-control">
                                                <asp:ListItem>--Select--</asp:ListItem>
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
                                                <asp:ListItem>2026</asp:ListItem>
                                                <asp:ListItem>2027</asp:ListItem>
                                                <asp:ListItem>2028</asp:ListItem>
                                                <asp:ListItem>2029</asp:ListItem>
                                                <asp:ListItem>2030</asp:ListItem>


                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddyear" Display="Dynamic" ErrorMessage="Select Year" ForeColor="Red" Operator="NotEqual"
                                                SetFocusOnError="True" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                        </td>


                                        <td>

                                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddEmp" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddEmp_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>

                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" ValidationGroup="a" />
                                            <asp:Button  ID="pddfdowload" runat="server" Text="Download"  CssClass="btn btn-primary"  OnClick="pddfdowload_Click"/>
                                            <asp:Label ID="lblcompId" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="row">
             <div class="col-lg-12" style="text-align:center;font-size:large">
                 <label>Attendance Summary Report</label>
            </div>
        </div>
        <br/>
        <div class="row">            
            <div class="col-lg-10" style="overflow-x:scroll ;width:100%; height:10%">
                <asp:GridView Id="GridView1" runat="server"  CssClass="table table-striped table-bordered bootstrap-datatable datatable" > </asp:GridView>  
            <%--<rsweb:ReportViewer ID="ReportViewer1" Width="100%" runat="server"></rsweb:ReportViewer>--%>
            </div>
        </div>

    </body>
    </html>


</asp:Content>

