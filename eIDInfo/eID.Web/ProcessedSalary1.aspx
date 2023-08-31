<%@ Page Title="Salary Details" Language="C#" AutoEventWireup="true" CodeFile="ProcessedSalary1.aspx.cs"
    MasterPageFile="~/UserMaster.master" Inherits="ProcessedSalary1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    <style type="text/css">
        .table1 {
            border: 1px solid;
            width: 100%;
            border-color: Black;
        }

        .thborder {
            border-bottom: 1px;
            border-bottom-style: solid;
            border-color: Black;
        }

        .lefttd {
            border-left: 1px;
            border-left-style: solid;
            padding-left: 5px;
        }

        .lefttd1 {
            padding-left: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="row">

        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="form-group">
                        <%--       <fieldset>
                            <legend>Salary Details</legend>--%>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server" >
                                <asp:Panel ID="pn1" runat="server" DefaultButton="btnsearch">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%" class="bordered lefttd" style="border: none;">
                                                <tr>
                                                    <td></td>
                                                    <td>&nbsp;&nbsp;&nbsp;Last Name
                                        <br />
                                                        <asp:TextBox ID="txtLastNameSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;First Name
                                        <br />
                                                        <asp:TextBox ID="txtFirstNameSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;Employee ID
                                        <br />
                                                        <asp:TextBox ID="txtempidsearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <%--<asp:FilteredTextBoxExtender ID="filterempid" runat="server" FilterMode="ValidChars" ValidChars="0123456789" TargetControlID="txtempidsearch"></asp:FilteredTextBoxExtender>--%>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;Month
                                        <br />
                                                        <asp:DropDownList CssClass="form-control" ID="ddlMonths" runat="server" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
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
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;Year
                                        <br />
                                                        <%--<asp:TextBox ID="txtYear" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlYear"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <div style="margin-top: 15px;">
                                                            <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" OnClick="btnsearch_Click"
                                                                Text="Search" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <br />

                                            <asp:GridView ID="grd_Emp" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-bordered table-striped"
                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="grd_Emp_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtdetails" ReadOnly="true" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <div style="text-align: center;">
                                                                <asp:Label ID="lblempid" ReadOnly="true" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Month Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblemailid" ReadOnly="true" runat="server" Text='<%# Eval("MonthName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcntct" ReadOnly="true" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gross Salary">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOJ" ReadOnly="true" runat="server" Text='<%# Eval("GrossSalary") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NetPay">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldept" ReadOnly="true" runat="server" Text='<%# Eval("netpay") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Salary Slip">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsalprocessid"  ReadOnly="true" runat="server" Text='<%# Eval("SalProcessId") %>'></asp:Label>
                                                            <asp:ImageButton ImageUrl="~/Images/print.jpg" Height="30px" Width="30px"
                                                                ID="Edit" runat="server" Text="Pay Slip" ToolTip="Pay Slip" CommandArgument='<%# Eval("SalProcessId") %>'
                                                                CssClass="linkbutton1" OnClick="Edit_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                    </asp:Panel>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <fieldset>
                                    <table style="float: right" class="bordered">
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Button ID="btnPDF" runat="server" CssClass="btn bg-blue-active" Text="Print" OnClick="btnPDF_Click" />
                                                    <asp:Button ID="btnExcel" runat="server" CssClass="btn bg-blue-active" Text="Export To Excel"
                                                        OnClick="btnExcel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <table id="tblpayslip" runat="server" width="80%" style="border: 1px; border-style: solid; border-color: Black"
                                    border="1">
                                    <tr>
                                        <th class="thborder" align="center" colspan="6" style="border: thin solid #000000">Payment Slip
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="border: thin solid #000000">
                                            <table id="tbl1" runat="server" width="100%">
                                                <tr>
                                                    <td>Employee Name :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblempname" runat="server"></asp:Label>
                                                    </td>
                                                    <td>Pay Slip For Month :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblmonth" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Employee Id :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblempcode" runat="server"></asp:Label>
                                                    </td>
                                                    <td>No .of working Days :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblworkingday" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Grade :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblgrade" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="lefttd">Net Payable Days :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblnetpaybleday" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>designation :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldesg" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="lefttd">No of LOP Days :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllopdays" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Department :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldept" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="lefttd">PF No. :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblpfacc" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>PAN :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblpan" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="lefttd">ESI No. :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblesino" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Date of Joining :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldoj" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="lefttd">Mode:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblmode" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Gross Salary
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblgrosssalary" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="lefttd">Bank :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblbankname" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td class="lefttd">Account No. :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblaccountno" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="border: thin solid #000000">
                                            <table style="height: 50px; width: 350px; border: 1px; border-style: solid; border-color: Black;"
                                                runat="server">
                                                <tr id="T1" runat="server">
                                                    <th class="thborder" width="90px">Earnings
                                                    </th>
                                                    <th class="thborder" width="50px">P.M
                                                    </th>
                                                    <th class="thborder" width="50px">Amount
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:GridView ID="grd_Earning" runat="server" AutoGenerateColumns="False" BorderStyle="None" CssClass="table table-bordered table-striped"
                                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowPaging="true"
                                                            PageSize="10" Width="100%">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <%-- <asp:TemplateField  HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                     <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="ComponentName" />
                                                                <asp:BoundField DataField="amount" />
                                                                <%--  <RowStyle />
                                                    <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle />
                                                    <SortedAscendingHeaderStyle />
                                                    <SortedDescendingCellStyle />
                                                    <SortedDescendingHeaderStyle />--%>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr id="Tr1" runat="server" visible="false">
                                                    <td>BASIC SALARY
                                                    </td>
                                                    <td></td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblbasic" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr2" runat="server" visible="false">
                                                    <td>H.R.A.
                                                    </td>
                                                    <td></td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblhra" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr3" runat="server" visible="false">
                                                    <td>CONVEYANCE ALLOWANCE
                                                    </td>
                                                    <td></td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblCONVEYANCE" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr4" runat="server" visible="false">
                                                    <td>MEDICAL ALLOWANCE
                                                    </td>
                                                    <td></td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblMEDICAL" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr5" runat="server" visible="false">
                                                    <td class="thborder">SPEC. ALLOWANCE
                                                    </td>
                                                    <td class="thborder"></td>
                                                    <td class="thborder" style="text-align: center">
                                                        <asp:Label ID="lblspecial" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total
                                                    </td>
                                                    <td></td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblearningtotal1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="3" valign="top" style="border: 1px; border-style: solid; border-color: Black"
                                            border="1">
                                            <table style="height: 50px; width: 350px; border: 1px; border-style: solid; border-color: Black;">
                                                <tr id="Tr6" runat="server">
                                                    <th class="thborder" width="50px">Deductions
                                                    </th>
                                                    <th class="thborder" width="50px">scale
                                                    </th>
                                                    <th class="thborder" width="50px">Amount
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:GridView ID="GridViewDeduction" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowPaging="true" CssClass="table table-bordered table-striped"
                                                            PageSize="10" Width="100%">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <%-- <asp:TemplateField  HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                     <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="ComponentName" />
                                                                <asp:BoundField DataField="amount" />
                                                                <%--  <RowStyle />
                                                    <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle />
                                                    <SortedAscendingHeaderStyle />
                                                    <SortedDescendingCellStyle />
                                                    <SortedDescendingHeaderStyle />--%>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr id="Tr7" runat="server" visible="false">
                                                    <td>P.F.
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblpf2" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblpf1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr8" runat="server" visible="false">
                                                    <td>PROFESSIONAL TAX
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblpt1" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblpt2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr9" runat="server" visible="false">
                                                    <td>TDS
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="Label8" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr10" runat="server" visible="false">
                                                    <td>ADVANCE
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="Label9" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="Label4" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ABSENT
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblabsentscal" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblabsentamt" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr11" runat="server" visible="false">
                                                    <td>MOB. ALLOWANCE
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="Label11" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="Label5" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr12" runat="server" visible="false">
                                                    <td class="thborder">Other Deductions
                                                    </td>
                                                    <td class="thborder" style="text-align: center">
                                                        <asp:Label ID="lblOther1" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="thborder" style="text-align: center">
                                                        <asp:Label ID="lblOther2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lbldeductiontotal1" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lbldeductiontotal2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="border: thin solid #000000">
                                            <table class="table1">
                                                <tr>
                                                    <td>Net Pay :
                                        <asp:Label ID="lblnetpay" runat="server"></asp:Label><br />
                                                        <br />
                                                        In Words :
                                        <asp:Label ID="lblwords" runat="server" CssClass="form-control"></asp:Label><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
