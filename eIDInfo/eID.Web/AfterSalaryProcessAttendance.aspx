<%@ Page Title="Final Attendance" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="AfterSalaryProcessAttendance.aspx.cs" Inherits="AttendanceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="Styles/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CSSTableGenerator {
            margin: 0px;
            padding: 0px;
            box-shadow: 5px 5px 5px #888888;
            border: 1px solid #000000;
            text-align: center;
            -moz-border-radius-bottomleft: 0px;
            -webkit-border-bottom-left-radius: 0px;
            border-bottom-left-radius: 0px;
            -moz-border-radius-bottomright: 0px;
            -webkit-border-bottom-right-radius: 0px;
            border-bottom-right-radius: 0px;
            -moz-border-radius-topright: 0px;
            -webkit-border-top-right-radius: 0px;
            border-top-right-radius: 0px;
            -moz-border-radius-topleft: 0px;
            -webkit-border-top-left-radius: 0px;
            border-top-left-radius: 0px;
        }

            .CSSTableGenerator table {
                margin: 0px;
                padding: 0px;
            }

            .CSSTableGenerator tr:nth-child(odd) {
                background-color: #e5e5e5;
            }

            .CSSTableGenerator tr:nth-child(even) {
                background-color: #ffffff;
            }

            .CSSTableGenerator td {
                vertical-align: middle;
                border: 1px solid #000000;
                border-width: 0px 1px 1px 0px;
                text-align: left;
                padding: 7px;
                font-size: 12px;
                font-family: Arial;
                font-weight: normal;
                color: #000000;
            }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%">


                            <tr>
                             
                            </tr>
                            <tr>
                                <td>
                                    <div class="col-md-12">
                                        <asp:Panel ID="pann" runat="server" DefaultButton="btnSearch">
                                            <div class="box box-primary">

                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <table width="60%">
                                                            <tr>
                                                                <td>Employee Name</td>
                                                                <td>Month</td>
                                                                <td>Year</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="ddEmp" runat="server" AutoPostBack="True" Width="165px" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                                        ControlToValidate="ddEmp" Display="None" ErrorMessage="Select Employee"
                                                                        Operator="NotEqual" SetFocusOnError="True" ValidationGroup="a"
                                                                        ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender"
                                                                        runat="server" TargetControlID="CompareValidator1">
                                                                    </asp:ValidatorCalloutExtender>
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

                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server"
                                                                        ControlToValidate="ddmonth" Display="None" ErrorMessage="Select Month"
                                                                        Operator="NotEqual" SetFocusOnError="True" ValidationGroup="a"
                                                                        ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="CompareValidator3_ValidatorCalloutExtender"
                                                                        runat="server" TargetControlID="CompareValidator3">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>


                                                                <td>
                                                                    <asp:DropDownList ID="ddyear" runat="server" Width="165px" CssClass="form-control">
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

                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server"
                                                                        ControlToValidate="ddyear" Display="None" ErrorMessage="Select Year"
                                                                        Operator="NotEqual" SetFocusOnError="True" ValidationGroup="a"
                                                                        ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="CompareValidator2_ValidatorCalloutExtender"
                                                                        runat="server" TargetControlID="CompareValidator2">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Search"
                                                                        OnClick="btnSearch_Click" ValidationGroup="a" />
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>

                                                        </table>
                                                        <br />
                                                        <table border="1" cellpadding="0" runat="server" id="tblleave" cellspacing="0"
                                                            style="font-weight: bold;"
                                                            class="CSSTableGenerator" width="70%">
                                                            <tr>
                                                                <td style="padding-left: 5%; font-weight: bold;">Employee Name :</td>

                                                                <td style="padding-left: 5%">
                                                                    <asp:Label runat="server" ID="lblempname" />
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style="padding-left: 5%; font-weight: bold;">Working Days :</td>
                                                                <td style="padding-left: 5%">
                                                                    <asp:Label ID="lblwDays" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style="padding-left: 5%; font-weight: bold;">Present Days :</td>
                                                                <td style="padding-left: 5%">
                                                                    <asp:Label ID="lblPresentDays" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style="padding-left: 5%; font-weight: bold;">Absent days :</td>
                                                                <td style="padding-left: 5%">
                                                                    <asp:Label ID="lblAbDays" runat="server"></asp:Label>
                                                                </td>


                                                            </tr>

                                                            <tr>
                                                                <td style="padding-left: 5%; font-weight: bold;">Leave Details
              :</td>
                                                                <td style="padding-left: 5%;">
                                                                    <asp:Label Text="" ID="lblLeave" runat="server" />
                                                                </td>
                                                            </tr>

                                                            <%--<asp:TextBox Text="text" ID="plc" runat="server" />--%>

                                                            <tr>
                                                                <td style="padding-left: 5%; font-weight: bold;">Deductions :</td>
                                                                <td style="padding-left: 5%;">
                                                                    <asp:Label ID="lblDeduction" runat="server" />
                                                                </td>
                                                            </tr>

                                                        </table>

                                                        <br />

                                                        <table border="0" class="CSSTableGenerator"
                                                            width="70%">
                                                            <tr>
                                                                <td>Present Days</td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox1" runat="server" BackColor="#009900" Width="35px"></asp:TextBox>
                                                                </td>
                                                                <td>Sundays</td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox2" runat="server" BackColor="Orange" Width="35px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Leave Days</td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox3" runat="server" BackColor="Yellow" Width="35px"></asp:TextBox>
                                                                </td>
                                                                <td>Absent Days</td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox4" runat="server" BackColor="Red" Width="35px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>


                                                        <br />
                                                        <table border="1" id="tblAttend" runat="server" cellpadding="0" cellspacing="0" width="100%" align="left"
                                                            class="CSSTableGenerator">

                                                            <tr>
                                                                <td style="color: #800000; font-weight: bold; background-color: #CCCCCC;"
                                                                    class="style1" width="80px">Days :</td>
                                                                <td id="tddisp" runat="server" align="left"
                                                                    style="background-color: #CCCCCC; font-weight: bold;" class="style1">
                                                                    <asp:PlaceHolder ID="plc" runat="server" />
                                                                    <%--  <asp:TextBox Text="text" ID="PlaceHolder1" runat="server" />--%>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td style="color: #800000; font-weight: bold;" width="80px">Status :</td>
                                                                <td id="tddisp1" runat="server" align="left">
                                                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                                                                    <%--  <asp:TextBox Text="text" ID="PlaceHolder1" runat="server" />--%>
                                                                </td>

                                                            </tr>

                                                        </table>

                                                        <br />
                                                        <br />
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

