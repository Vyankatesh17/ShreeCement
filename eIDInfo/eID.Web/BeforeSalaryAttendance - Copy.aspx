<%@ Page Title="Verify Attendance" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="BeforeSalaryAttendance - Copy.aspx.cs" Inherits="AttendanceReport" %>

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
            <div class="row">

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">


                                <table width="100%" cellspacing="5px">
                                    <tr>
                                        <td>Select Company 
                                        </td>
                                        <td>

                                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" Width="250px"
                                                OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control">
                                            </asp:DropDownList>

                                            <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddlCompany" Display="Dynamic"
                                                ErrorMessage="*" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>

                                        </td>
                                        <td>Employee Name
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddEmp" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddEmp_SelectedIndexChanged" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddEmp"
                                                Display="None" ErrorMessage="Select Employee" Operator="NotEqual" SetFocusOnError="True"
                                                ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" runat="server"
                                                TargetControlID="CompareValidator1">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                        <td>Month
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddmonth" runat="server" ValidationGroup="a" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddmonth_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                                                Display="None" ErrorMessage="Select Month" Operator="NotEqual" SetFocusOnError="True"
                                                ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="CompareValidator3_ValidatorCalloutExtender" runat="server"
                                                TargetControlID="CompareValidator3">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                        <td>Year
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddyear" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddyear_SelectedIndexChanged">
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
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddyear"
                                                Display="None" ErrorMessage="Select Year" Operator="NotEqual" SetFocusOnError="True"
                                                ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="CompareValidator2_ValidatorCalloutExtender" runat="server"
                                                TargetControlID="CompareValidator2">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Show" OnClick="btnSearch_Click"
                                                ValidationGroup="a" />
                                        </td>
                                    </tr>
                                </table>
                             
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Colors</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding">
                            <table class="table table-hover">
                                
                                <tbody>
                                    <tr>
                                        <td>Present Days
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" BackColor="#009900" Width="35px" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>Sundays & Holidays
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox2" runat="server" BackColor="Orange" Width="35px" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>Leave Days
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" BackColor="Yellow" Width="35px" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>Absent Days
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox4" runat="server" BackColor="Red" Width="35px" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Attendance</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding">
                            <asp:DataList ID="datalistdisplay" runat="server" RepeatDirection="Horizontal" GridLines="Both" CssClass="col-lg-12" RepeatColumns="9">
                                    <ItemTemplate>
                                        <div class="bg-aqua">
                                            <strong>
                                                <span class="text-center">
                                                    <center><asp:Label ID="lbldays" runat="server" Text='<%# Eval("Days") %>' /></center>
                                                </span>
                                            </strong>
                                        </div>
                                        <div class="bg-white">
                                            <asp:DropDownList ID="ddlist" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlist_selectedindexchanged" AutoPostBack="true">
                                                <asp:ListItem>--Select--</asp:ListItem>
                                                <asp:ListItem>P</asp:ListItem>
                                                <asp:ListItem>HW</asp:ListItem>
                                                <asp:ListItem>MH</asp:ListItem>
                                                <asp:ListItem>MP</asp:ListItem>
                                                <asp:ListItem>WO</asp:ListItem>
                                                <asp:ListItem>H</asp:ListItem>
                                                <asp:ListItem>L</asp:ListItem>
                                                <asp:ListItem>A</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </ItemTemplate>
                                </asp:DataList>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Statuses</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding">
                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th>Present</th>
                                        <th>Manual Holiday</th>
                                        <th>Manual Attendance</th>
                                        <th>Holiday Working</th>
                                        <th>Weekly off</th>
                                        <th>Holiday</th>
                                        <th>Leave</th>
                                        <th>Absent</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>P
                                        </td>
                                        <td>MH
                                        </td>
                                        <td>MP 
                                        </td>
                                        <td>HW
                                        </td>
                                        <td>WO
                                        </td>
                                        <td>H
                                        </td>
                                        <td>L
                                        </td>
                                        <td>A
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Attendance Counts</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding">
                            <table class="table table-hover" id="tblAttend" runat="server">
                                <thead>
                                    <tr>
                                        <th>Present Days</th>
                                        <th>Absent Days</th>
                                        <th>Leave Details</th>
                                        <th>Sundays and Holidays</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtpresentdays" runat="server" AutoPostBack="True" CssClass="form-control"
                                                OnTextChanged="txtpresentdays_TextChanged"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtabsentdays" runat="server" AutoPostBack="True" CssClass="form-control"
                                                OnTextChanged="txtabsentdays_TextChanged"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblLeave" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsundayandholiday" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </td>
                                         <td>
                                            <asp:Button ID="btnSave" CssClass="btn bg-blue-active" runat="server" Text="Save" OnClick="btnSave_Click"
                                                ValidationGroup="a"  />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
