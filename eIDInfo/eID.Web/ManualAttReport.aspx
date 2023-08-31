<%@ Page Title="Manual Attendance Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ManualAttReport.aspx.cs" Inherits="ManualAttReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
   
                                    <div class="col-md-12">
                                        <asp:Panel ID="pann" runat="server" DefaultButton="btnSearch">
                                            <div class="box box-primary">

                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <table width="65%">
                                                            <tr>
                                                                <td>Select Company
                                                                </td>
                                                                <td>Employee Name
                                                                </td>
                                                                <td>Month
                                                                </td>
                                                                <td>Year</td>
                                                            </tr>
                                                            <tr>
                                                                <td>

                                                                    <asp:DropDownList ID="ddlCompany" runat="server" Width="165px" AutoPostBack="True" CssClass="form-control"
                                                                        OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>

                                                                <td>
                                                                    <asp:DropDownList ID="ddEmp" runat="server" Width="165px" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddEmp_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddEmp"
                                                                        Display="Dynamic" ErrorMessage="Select Employee" Operator="NotEqual" SetFocusOnError="True"
                                                                        ValidationGroup="a" ValueToCompare="--Select--" ForeColor="Red"></asp:CompareValidator>

                                                                </td>

                                                                <td>
                                                                    <asp:DropDownList ID="ddmonth" runat="server" Width="165px" ValidationGroup="a" CssClass="form-control">
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
                                                                    <asp:DropDownList ID="ddyear" runat="server"  Width="165px" CssClass="form-control">
                                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                                        <asp:ListItem>2013</asp:ListItem>
                                                                        <asp:ListItem>2014</asp:ListItem>
                                                                        <asp:ListItem>2015</asp:ListItem>
                                                                        <asp:ListItem>2016</asp:ListItem>
                                                                        <asp:ListItem>2017</asp:ListItem>
                                                                        <asp:ListItem>2018</asp:ListItem>
                                                                        <asp:ListItem>2019</asp:ListItem>
                                                                        <asp:ListItem>2020</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddyear" Display="Dynamic" ErrorMessage="Select Year" ForeColor="Red" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Search" OnClick="btnSearch_Click"
                                                                        ValidationGroup="a" />
                                                                </td>
                                                            </tr>
                                                        </table>



                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView Width="100%" runat="server" ID="gv" CssClass="table table-bordered table-striped">
                                                                    </asp:GridView>

                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                              
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

