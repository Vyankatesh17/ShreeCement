<%@ Page Title="Monthly Attendance Report with (In/Out) Time" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="rpt_monthly_department_in_out.aspx.cs" Inherits="rpt_monthly_department_in_out" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
//rptrTables

        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grdOrd').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grdOrd_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>

    <style>
        .table {
            margin-bottom: 0 !important;
        }

        table.timecard {
            margin: auto;
            border-collapse: collapse;
            border: 1px solid #fff; /*for older IE*/
        }

            table.timecard caption {
                background-color: #c7c3c1;
                color: #fff;
                font-size: x-large;
                font-weight: bold;
                letter-spacing: .3em;
            }

            table.timecard thead th {
                padding: 8px;
                background-color: #c7c3c1;
                font-size: large;
            }

                table.timecard thead th#thDay {
                    width: 40%;
                }

                table.timecard thead th#thRegular, table.timecard thead th#thOvertime, table.timecard thead th#thTotal {
                    width: 20%;
                }

            table.timecard th, table.timecard td {
                padding: 3px;
                border-width: 1px;
                border-style: solid;
                border-color: #f4f4f4 #ccc;
            }

            table.timecard td {
                text-align: center;
            }

            table.timecard tbody th {
                text-align: center;
                font-weight: normal;
            }

            table.timecard tfoot {
                font-weight: bold;
                font-size: large;
                background-color: #687886;
                color: #fff;
            }

            table.timecard tr.even {
                background-color: #fde9d9;
            }
            table.timecard tbody tr {
                /*outline: thin solid #727171;*/
            }

            table.timecard td {
                width: 40px !important;
            }

                table.timecard td:nth-child(1) {
                    width: 62px !important;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h1 class="box-title">Attendance with In / Out Time
                            </h1>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ErrorMessage="This field is required" ID="RequiredFieldValidator2" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Month</label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="0">Select month</asp:ListItem>
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
                                    <asp:RequiredFieldValidator ControlToValidate="ddlMonth" ErrorMessage="This field is required" ID="RequiredFieldValidator1" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Year</label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlYear" ErrorMessage="This field is required" ID="RequiredFieldValidator5" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />

                                </div>
                                <div class="col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" ValidationGroup="A" Text="Show" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="table-responsive">
                                <asp:Repeater ID="rptrTables" runat="server" OnItemDataBound="rptrTables_ItemDataBound">
                                    <HeaderTemplate>
                                        <div class="col-md-12">
                                            <div class="card text-center card-info">
                                                <div class="card-header">
                                                    <asp:Label ID="lblHeader" runat="server" CssClass="h3"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-12">
                                            <div class="card card-info">
                                                <div class="card-header with-border" style="border-bottom: 1px solid #f4f4f4 !important;">
                                                    <h4 class="box-title">Department : <%# Eval("DeptName") %></h4>
                                                    <asp:Label ID="lblDeptId" CssClass="hidden" Visible="false" runat="server" Text='<%# Eval("DeptId") %>' />
                                                </div>
                                                <div class="card-body no-padding">
                                                    <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                        CssClass="table table-condensed table-bordered" GridLines="Both" Width="100%" OnRowDataBound="grdOrder_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <%#Eval("EmpName") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp No">
                                                                <ItemTemplate><%#Eval("EmpNo") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="1">
                                                                <ItemTemplate><%#Eval("1").ToString().Replace("\n","<br/>") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="2">
                                                                <ItemTemplate><%#Eval("2") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="3">
                                                                <ItemTemplate><%#Eval("3") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="4">
                                                                <ItemTemplate><%#Eval("4") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="5">
                                                                <ItemTemplate><%#Eval("5") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="6">
                                                                <ItemTemplate><%#Eval("6") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="7">
                                                                <ItemTemplate><%#Eval("7") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="8">
                                                                <ItemTemplate><%#Eval("8") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="9">
                                                                <ItemTemplate><%#Eval("9") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="10">
                                                                <ItemTemplate><%#Eval("10") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="11">
                                                                <ItemTemplate><%#Eval("11") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="12">
                                                                <ItemTemplate><%#Eval("12") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="13">
                                                                <ItemTemplate><%#Eval("13") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="14">
                                                                <ItemTemplate><%#Eval("14") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="15">
                                                                <ItemTemplate><%#Eval("15") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="16">
                                                                <ItemTemplate><%#Eval("16") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="17">
                                                                <ItemTemplate><%#Eval("17") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="18">
                                                                <ItemTemplate><%#Eval("18") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="19">
                                                                <ItemTemplate><%#Eval("19") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="20">
                                                                <ItemTemplate><%#Eval("20") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="21">
                                                                <ItemTemplate><%#Eval("21") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="22">
                                                                <ItemTemplate><%#Eval("22") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="23">
                                                                <ItemTemplate><%#Eval("23") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="24">
                                                                <ItemTemplate><%#Eval("24") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="25">
                                                                <ItemTemplate><%#Eval("25") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="26">
                                                                <ItemTemplate><%#Eval("26") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="27">
                                                                <ItemTemplate><%#Eval("27") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="28">
                                                                <ItemTemplate><%#Eval("28") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="29">
                                                                <ItemTemplate><%#Eval("29") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="30">
                                                                <ItemTemplate><%#Eval("30") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="31">
                                                                <ItemTemplate><%#Eval("31") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        
                        
                        <div class="box-footer">
                            <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="ExportToExcel" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

