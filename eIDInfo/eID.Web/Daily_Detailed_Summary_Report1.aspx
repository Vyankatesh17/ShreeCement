<%@ Page Title="Daily Detailed Summary Report 1" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Daily_Detailed_Summary_Report1.aspx.cs" Inherits="Daily_Detailed_Summary_Report1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .table {
            margin-bottom: 0 !important;
        }

        table.timecard {
            margin: auto;
            border-collapse: collapse;
            border: 1px solid #000000; /*for older IE*/
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
                border-color: #000000 #000000;
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

            table.timecard td {
                width: 40px !important;
            }

                table.timecard td:nth-child(1) {
                    width: 62px !important;
                }
    </style>

    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grdOrder').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grdOrder_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Daily Detailed Summary Report</h3>

                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ErrorMessage="This field is required" ID="RequiredFieldValidator3" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    <%--<asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator4" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />--%>
                                </div>                               
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Employee</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="col-md-2 form-group">
                                    <label class="control-label">From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>

                                <div class="col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" ValidationGroup="A" Text="Show" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="box-body no-padding">
                            <div class="table-responsive">
                                <div class="card text-center card-info">
                                    <div class="card-header">
                                        <asp:Label ID="lblHeader" runat="server" CssClass="h3"></asp:Label>
                                    </div>
                                </div>

                                <div class="card-body no-padding">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="true" AllowPaging="false"
                                                    CssClass="timecard" GridLines="Both"
                                                     Width="100%">
                                                </asp:GridView>
                                            </div>
                                            <div class="form-group">
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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

