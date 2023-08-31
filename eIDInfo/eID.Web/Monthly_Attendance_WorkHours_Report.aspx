<%@ Page Title="Monthly Attendance Work Hours Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Monthly_Attendance_WorkHours_Report.aspx.cs" Inherits="Monthly_Attendance_WorkHours_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    <script type="text/javascript">
        function jqFunctions() {
            generateTableHeaders();
            var table = $('#table_attendance').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#table_attendance_wrapper .col-sm-6:eq(0)');
            

        }

        $(document).ready(function () {

            jqFunctions();
        });

        function generateTableHeaders() {

            var ddlM = $("[id*=ddlMonth]");
            var ddlY = $("[id*=ddlYear]");

            var date = new Date();
            var month = ddlM.val();
            var year = ddlY.val();
            var days = daysInMonth(month, year);
            var tbl = document.getElementById("tblTest");

            var tdId = document.createElement("td");
            tdId.innerHTML = "ID";
            tbl.appendChild(tdId);

            var tdName = document.createElement("td");
            tdName.innerHTML = "NAME";
            tbl.appendChild(tdName);

            var tdDesig = document.createElement("td");
            tdDesig.innerHTML = "DEPARTMENT";
            tbl.appendChild(tdDesig);
            console.log(days,month,year,date);
            for (var j = 1; j <= days; j++) {
                var td = document.createElement("td");
                td.innerHTML = j;
                tbl.appendChild(td);
            }

            

            var tdP = document.createElement("td");
            tdP.innerHTML = "P";
            tbl.appendChild(tdP);

            var tdA = document.createElement("td");
            tdA.innerHTML = "A";
            tbl.appendChild(tdA);
            

            var tdL = document.createElement("td");
            tdL.innerHTML = "L";
            tbl.appendChild(tdL);


            var tdH = document.createElement("td");
            tdH.innerHTML = "H";
            tbl.appendChild(tdH);

            var tdWO = document.createElement("td");
            tdWO.innerHTML = "WO";
            tbl.appendChild(tdWO);

            var tdWO = document.createElement("td");
            tdWO.innerHTML = "WFH";
            tbl.appendChild(tdWO);

            var tdTotal = document.createElement("td");
            tdTotal.innerHTML = "TOTAL";
            tbl.appendChild(tdTotal);
            
        }
        function daysInMonth(month, year) {
            return new Date(year, month, 0).getDate();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Monthly Attendance Work Hours Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Year</label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 form-group">
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
                                </div>
                                <div class="col-md-2 form-group" Visible="false" id="company" runat="server">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group" Visible="false" id="department" runat="server">
                                    <label class="control-label">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group" Visible="false" id="employee" runat="server">
                                    <label class="control-label">Employee Name</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Employee Code</label>
                                    <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Show" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-body no-padding">
                            <div class="table-responsive">
                                <table class="table" id="table_attendance1">
                                    <tr id="tblTest1">
                                    </tr>
                                </table>
                                            <asp:Literal ID="litHeaders" runat="server" Visible="false"></asp:Literal>
                                <table class="table table-bordered table-striped" id="table_attendance">
                                    <thead>
                                        <tr id="tblTest">
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Literal ID="litBody" runat="server"></asp:Literal>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Literal ID="litTest" runat="server"></asp:Literal>
</asp:Content>

