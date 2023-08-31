<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="EmployeeDashBoard.aspx.cs" Inherits="EmployeeDashBoard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
        


              <div class="tab-content">
                        <div class="tab-pane active" id="tab_1">
                            <div class="row">

                    <div class="col-md-6">
                        <div class="box box-info">
                              <div class="box-header" style="cursor: move;">
                                            <i class="fa fa-eye"></i>
                                            <h3 class="box-title">Holiday Details</h3>
                                        </div>
                            <div class="box-body">
                                <div class="form-group">

                                    

                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Calendar ID="calHoli" Width="100%" runat="server" BackColor="White" 
                                            BorderColor="#999999" ForeColor="Black" OnSelectionChanged="CalHoli_SelectionChanged"
                                            DayStyle-BorderWidth="1" DayStyle-Height="40px" 
                                            OnDayRender="OnApt_Cal_DayRender" Font-Names="Verdana" Font-Size="8pt" 
                                            Height="300px" CellPadding="10">
                                            <DayHeaderStyle Font-Bold="True" Font-Size="7pt" BackColor="#CCCCCC" />
                                            <DayStyle BorderWidth="1px" Height="40px" />
                                            <NextPrevStyle VerticalAlign="Bottom" />
                                            <OtherMonthDayStyle ForeColor="#808080" />
                                            <SelectedDayStyle BackColor="#666666" ForeColor="White" Font-Bold="True" />
                                            <SelectorStyle BackColor="#CCCCCC" />
                                            <TitleStyle BackColor="#999999" Font-Bold="True" BorderColor="Black" />
                                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <WeekendDayStyle BackColor="#FFFFCC" />
                                        </asp:Calendar>
                                                <asp:GridView ID="gvcalender" runat="server" Visible="False">
                                                </asp:GridView>
                                                <br />
                                            </td>
                                        </tr>
                                    
                                     
                                    </table>
                                    <br /><br /><br /><br />

                                </div>
                            </div>
                        </div>
                    </div>
                                   <div class="col-lg-6">
                                    <div class="box box-info">
                                        <div class="box-header" style="cursor: move;">
                                            <i class="fa fa-eye"></i>
                                            <h3 class="box-title">Salary Slip Details</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="form-group">
                                                    <table width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%" class="bordered lefttd" style="border: none;">
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;Month
                                        <br />
                                                        <asp:DropDownList CssClass="form-control" ID="ddlMonths" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Jan</asp:ListItem>
                                                            <asp:ListItem Value="2">Feb</asp:ListItem>
                                                            <asp:ListItem Value="3">Mar</asp:ListItem>
                                                            <asp:ListItem Value="4">Apr</asp:ListItem>
                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                            <asp:ListItem Value="6">Jun</asp:ListItem>
                                                            <asp:ListItem Value="7">Jul</asp:ListItem>
                                                            <asp:ListItem Value="8">Aug</asp:ListItem>
                                                            <asp:ListItem Value="9">Sep</asp:ListItem>
                                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;Year
                                        <br />
                                                      
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlYear"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <div style="margin-top: 15px;">
                                                            <asp:Button ID="btnSearchSalary" runat="server" CssClass="btn bg-blue-active" 
                                                                Text="Search" OnClick="btnSearchSalary_Click" />
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
                                                AllowPaging="true" PageSize="10" >
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
                                                    <asp:TemplateField HeaderText="NetPay" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldept" ReadOnly="true" runat="server" Text='<%# Eval("netpay") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Salary Slip">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsalprocessid" Visible="false" ReadOnly="true" runat="server" Text='<%# Eval("SalProcessId") %>'></asp:Label>
                                                            <asp:ImageButton ImageUrl="~/Images/salary slip.jpg" Height="30px" Width="30px"
                                                                ID="Edit" runat="server" Text="Pay Slip" ToolTip="Pay Slip" CommandArgument='<%# Eval("SalProcessId") %>'
                                                                CssClass="linkbutton1" OnClick="Edit_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>No Data Found !!!!!!!!!!!!!!!</EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                <div class="col-lg-12">
                                    <div class="box box-info">
                                        <div class="box-header" style="cursor: move;">
                                            <i class="fa fa-eye"></i>
                                            <h3 class="box-title">Employee Attendance Report Details</h3>
                                        </div>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel2" runat="server" >
                                                <table width="100%">


                           
                            <tr>
                                <td>
                                 
                                        <asp:Panel ID="pann" runat="server" DefaultButton="btnSearch">
                                       
                                                        <table width="95%">
                                                            <tr>
                                                                
                                                                <td align="center">Month
                                                                </td>
                                                                <td align="center">Year</td>
                                                               
                                                            </tr>
                                                            <tr>
                                                                

                                                                <td align="center">
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
                                                                    <asp:Label ID="lblcompaniId" runat="server" Visible="false"></asp:Label>
                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddmonth"
                                                                        Display="Dynamic" ErrorMessage="Select Month" Operator="NotEqual" SetFocusOnError="True"
                                                                        ValidationGroup="a" ValueToCompare="--Select--" ForeColor="Red"></asp:CompareValidator>

                                                                </td>

                                                                <td align="center">
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
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Search" OnClick="btnSearch_Click"
                                                                        ValidationGroup="a" />
                                                                     <asp:Label ID="lblcompId" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    <table width="100%" style="overflow:scroll">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="panlla" runat="server"  Width="90%">
                                                                    <asp:GridView Width="100%" runat="server" ID="gv" CssClass="table table-bordered table-striped">
                                                                    </asp:GridView>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                        <asp:Panel ID="panalcount" runat="server" Visible="false">
                                                        <table width="60%" align="Center" cellspacing="4px" cellpadding="4px" border="1" style="border: 8px solid #FFFFFF">
                                                            <tr style="background-color:#0099CC;" >
                                                                <td align="center">Days</td>
                                                                <td align="center">Count</td>
                                                            </tr>
                                                            <tr style="background-color:silver;">
                                                                <td align="center">Present Days</td>
                                                                <td align="center"><asp:Label ID="lblPDay" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr style="background-color:silver;">
                                                                <td align="center">Absent Days</t>
                                                                <td align="center"><asp:Label ID="lblADay" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr style="background-color:silver;">
                                                                <td align="center">Weekly Off</td>
                                                                <td align="center"><asp:Label ID="lblWO" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr style="background-color:silver;">
                                                                <td align="center">Holiday's</td>
                                                                <td align="center"><asp:Label ID="lblHoliday" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr style="background-color:silver;">
                                                                <td align="center">Leaves</td>
                                                                <td align="center"><asp:Label ID="lblLeave" runat="server"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                            </asp:Panel>
                               

                       
                                               
                                        </asp:Panel>
                                 
                                     </td>

                            </tr>
                        </table>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                             
</div>
                            </div>
                  </div>

           
    <table id="tblpayslip" runat="server" width="80%" style="border: 1px; border-style: solid; border-color: Black" visible="false"
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

        </ContentTemplate>
    </asp:UpdatePanel>








</asp:Content>

