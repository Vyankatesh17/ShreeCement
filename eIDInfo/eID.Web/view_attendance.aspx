<%@ Page Title="View Punch Log" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="view_attendance.aspx.cs" Inherits="view_attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {
            //    $('#ContentPlaceHolder1_grd_Emp').DataTable({
            //        "paging": true,
            //        "lengthChange": true,
            //        "searching": true,
            //        "ordering": true,
            //        "info": true,
            //        "autoWidth": true
            //    });

            var table = $('#ContentPlaceHolder1_gvList').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvList_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box">
                        <div class="box-header box-solid">
                            <h3 class="box-title">View Attendance</h3>
                        </div>
                        <div class="box-body">                           
                            <div class="row">
                                <%--<div class="col-md-1 form-group">
                                    <label class="control-label">Year</label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Width="75px"></asp:DropDownList>
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
                                </div>--%>
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
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" DataTextField="EmpName" DataValueField="EmployeeId"></asp:DropDownList>
                                </div>
                                <%--<div class="col-md-2 form-group">
                                    <label class="control-label">Employee Code</label>
                                    <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>--%>
                                <div class="col-md-2 form-group hidden">
                                    <label class="control-label">Branch</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                 <div class="col-md-2 form-group" Visible="false" id="device" runat="server">
                                    <label class="control-label">Devices</label>
                                    <asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>  
                                 <div class="col-md-2 form-group">
                                    <label class="control-label">From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>                                                             
                            </div>
                             <div class="row">
                                <div class="col-md-11 form-group">
                                     </div>
                                 <div class=" col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Show" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-footer no-padding">
                            <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" OnRowDataBound="gvList_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Employee Name" DataField="EmpName" />
                                    <asp:BoundField HeaderText="Emp Code" DataField="MachineID" />
                                    <asp:BoundField HeaderText="Device Code" DataField="EmployeeNo" />
                                    <asp:BoundField HeaderText="Device No" DataField="DeviceSerialNo" />
                                    <asp:BoundField HeaderText="Device Name" DataField="DeviceName" />
                                    <asp:TemplateField HeaderText="Attendance Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Time" DataField="Time" />
                                    <asp:BoundField HeaderText="PunchStatus" DataField="PunchStatus" />
                                    <asp:BoundField HeaderText="Temperature" DataField="CurrentTemp" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

