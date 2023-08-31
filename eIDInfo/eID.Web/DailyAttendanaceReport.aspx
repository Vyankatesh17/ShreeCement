<%@ Page Title="Employee Attendance Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="DailyAttendanaceReport.aspx.cs" Inherits="DailyAttendanaceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 161px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%">

                            <tr>
                                <td>
                                    <div class="col-md-12">
                                        <asp:Panel ID="pann" runat="server" DefaultButton="btnshow">
                                            <div class="box box-primary">

                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <table width="40%">

                                                            <tr>
                                                                <td>From Date 
                                                                </td>
                                                                <td>To Date
                                                                </td>
                                                                <td>Select Company
                                                                </td>
                                                                <td><asp:Label ID="lbldept" runat="server" Text="Select Department" Visible="false"></asp:Label>
                                                                </td>
                                                                <td><asp:Label ID="lblemp" runat="server" Text="Select Employee" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtfromdate" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="txtfromdate_TextBoxWatermarkExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtfromdate" WatermarkText="From Date">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtfromdate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="rq1" runat="server" ValidationGroup="S" ControlToValidate="txtfromdate"
                                                                        Display="Static" ErrorMessage="RequiredField" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>

                                                                <td>
                                                                    <br />
                                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="txttodate_TextBoxWatermarkExtender" WatermarkText="To Date"
                                                                        runat="server" Enabled="True" TargetControlID="txttodate">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txttodate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="rq2" runat="server" ControlToValidate="txttodate"
                                                                        Display="Static" ErrorMessage="RequiredField" ValidationGroup="S" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    <br />
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtfromdate"
                                                                        ControlToValidate="txttodate" SetFocusOnError="True" ForeColor="#A00D11" Font-Size="Small"
                                                                        ErrorMessage="select proper date" Operator="GreaterThanEqual"
                                                                        Type="Date" ValidationGroup="a"></asp:CompareValidator>
                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="ddCompnay" runat="server" Width="250px" AutoPostBack="True" CssClass="form-control"
                                                                        OnSelectedIndexChanged="ddCompnay_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="dddep" runat="server" Width="200px" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="dddep_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="ddemp" runat="server" Width="230px" CssClass="form-control" AutoPostBack="True" Visible="False">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="4">&nbsp;
                                                                </td>
                                                                <td colspan="2">&nbsp;
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Button ID="btnshow" runat="server" CssClass="btn bg-blue-active" Text="Search" ValidationGroup="S"
                                                                        OnClick="btnshow_Click" />
                                                                    <asp:Button ID="btnexport" runat="server" CssClass="btn bg-blue-active" Text="Export to Excel"
                                                                        ValidationGroup="S" OnClick="btnexport_Click" />
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%">
                                                            <tr>
                                                                <td class="style1">&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="No Of Present Data : ">
                                                                    </asp:Label>
                                                                    <asp:Label ID="lblcount" Font-Bold="True" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1">&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="No Of Absent Data : ">
                                                                    </asp:Label>
                                                                    <asp:Label ID="lblabscentdata" Font-Bold="True" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <div style="overflow: scroll; height: 300px;">
                                                                        <asp:Panel ID="pnl" runat="server">
                                                                            <asp:GridView ID="grddata" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                                                                                CssClass="table table-bordered table-striped" Caption="Daily Present Report"
                                                                                OnDataBound="grddata_DataBound" OnRowDataBound="grddata_RowDataBound">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                                                                            <asp:Label ID="lblColor" runat="server" Text='<%# Eval("color") %>' Visible="false" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center" />
                                                                                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                                                                    <asp:BoundField DataField="color" HeaderText="color" ItemStyle-HorizontalAlign="Center" Visible="false" />

                                                                                    <asp:BoundField DataField="intime" HeaderText="INTime" ItemStyle-HorizontalAlign="Center" />


                                                                                    <asp:BoundField DataField="outtime" HeaderText="OutTime" ItemStyle-HorizontalAlign="Center" />
                                                                                    <asp:BoundField DataField="DesigName" HeaderText="Desigation Name" ItemStyle-HorizontalAlign="Center" />
                                                                                </Columns>
                                                                                <FooterStyle HorizontalAlign="Center" BackColor="Wheat" Font-Bold="true" />
                                                                                <PagerStyle HorizontalAlign="Right" />
                                                                                <EmptyDataTemplate>
                                                                                    No Data Exist!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>

                                                                            <asp:GridView ID="grdabscent" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                                                                                CssClass="table table-bordered table-striped" Caption="Daily Absent Report">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Name" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center" />

                                                                                    <asp:BoundField DataField="Absent" HeaderText="Absent" ItemStyle-HorizontalAlign="Center" />
                                                                                    <asp:BoundField DataField="DesigName" HeaderText="Desigation Name" ItemStyle-HorizontalAlign="Center" />
                                                                                </Columns>
                                                                                <FooterStyle HorizontalAlign="Center" BackColor="Wheat" Font-Bold="true" />
                                                                                <EmptyDataTemplate>
                                                                                    No Data Exist!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                </td>

                            </tr>
                        </table>

                        </fieldset>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                    </td>
                </tr>
            </table>

            </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
