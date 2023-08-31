<%@ Page Title="School Leave Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="LeaveSchoolReport.aspx.cs" Inherits="LeaveSchoolReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_gv').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gv_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
     </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsearch" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box">
                        <div class="box-header box-solid">
                            <h3 class="box-title">School Leave Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2 form-group" Visible="false" id="company" runat="server">
                                    <label class="control-label">Hostel Name</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                </div>                       
                                 <div class="col-md-2 form-group" Visible="false" id="department" runat="server">
                                    <label class="control-label">Building Name</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group" Visible="false" id="employee" runat="server">
                                    <label class="control-label">Student Name</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" DataTextField="EmpName" DataValueField="EmployeeId"></asp:DropDownList>
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
                                    <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" Text="Show" OnClick="btnsearch_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="box-footer no-padding">
                            <asp:GridView ID="gv" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered" OnRowDataBound="gv_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                    <asp:BoundField HeaderText="Admission No" DataField="AccessCardNo" />
                                        <asp:BoundField HeaderText="Student Name" DataField="EmpName" />                                        
                                        <asp:BoundField HeaderText="Biometric Id" DataField="EmployeeNo" />
                                        <asp:BoundField HeaderText="Hostel Name" DataField="CompanyName" />
                                        <asp:BoundField HeaderText="Building Name" DataField="DeptName" />
                                        <asp:BoundField HeaderText="Floor" DataField="WorkLocation" />
                                        <asp:BoundField HeaderText="Room No" DataField="CurPin" />
                                        <asp:BoundField HeaderText="Date" DataField="Date" />
                                       <%-- <asp:BoundField HeaderText="Punching Time" DataField="PunchIn" />--%>
                                        <asp:BoundField HeaderText="Status" DataField="Status" />
                                        <asp:BoundField HeaderText="Student Mobile No" DataField="ContactNo" />
                                        <asp:BoundField HeaderText="Parent Mobile No" DataField="AltContactNo" />
                                        <%--<asp:BoundField HeaderText="Stud. Device Code" DataField="MachineID" />--%>                                        
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Exists.......!!!!
                                </EmptyDataTemplate>
                                <PagerStyle HorizontalAlign="Right" />
                                <EmptyDataRowStyle />
                            </asp:GridView>
                        </div>
                        <div class="box-body">
                            <table class="table table-bordered hidden">
                                <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

