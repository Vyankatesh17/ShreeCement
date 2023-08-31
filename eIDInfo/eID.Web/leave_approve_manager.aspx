﻿<%@ Page Title="Leave Approve By Manager" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="leave_approve_manager.aspx.cs" Inherits="leave_approve_manager" %>

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

            var table = $('#ContentPlaceHolder1_gvPendingLeaves').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvPendingLeaves_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Pending Leave List</h3>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label class="control-label col-sm-3">Company</label>
                                                <div class="col-sm-9">
                                                     <asp:DropDownList ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-body no-padding">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvPendingLeaves" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee" />
                                                <asp:BoundField DataField="EmployeeNo" HeaderText="Code" />
                                                <asp:BoundField DataField="MachineID" HeaderText="Bio ID" />
                                                <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" />
                                                <asp:BoundField DataField="LeaveStartDate" HeaderText="From Date" />
                                                <asp:BoundField DataField="LeaveEndDate" HeaderText="To Date" />
                                                <asp:BoundField DataField="Duration" HeaderText="Total Days" />
                                                <asp:BoundField DataField="ManagerStatus" HeaderText="Status" />
                                                <asp:BoundField DataField="LeaveReason" HeaderText="Reason" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <asp:LinkButton ID="lbtnApprove" runat="server" CommandArgument='<%#Eval("LeaveApplicationId") %>' OnClick="lbtnApprove_Click" CssClass="btn btn-success btn-xs"><i class="fa fa-thumbs-up"></i> Approve</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnReject" runat="server" CommandArgument='<%#Eval("LeaveApplicationId") %>' OnClick="lbtnReject_Click" CssClass="btn btn-warning btn-xs"><i class="fa fa-thumbs-down"></i> Reject</asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Approve / Reject Leaves</h3>
                                </div>
                                <div class="box-body">
                                    <asp:HiddenField ID="hfKey" runat="server" />
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Employee Name</label>
                                                <asp:TextBox ID="lblEmpName" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Leave Type</label>
                                                <asp:TextBox ID="lblLeaveType" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">From Date</label>
                                                <asp:TextBox ID="lblFromdate" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">To Date</label>
                                                <asp:TextBox ID="lblToDate" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Total Days</label>
                                                <asp:TextBox ID="lblTotaDays" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Leave Reason</label>
                                                <asp:TextBox ID="lblReason" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label class="control-label">
                                                    Remark
                                                </label>
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ErrorMessage="Enter reason" ValidationGroup="A" CssClass="text-red" Display="Dynamic" ControlToValidate="txtReason" runat="server" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success" Text="Approve" ValidationGroup="A" OnClick="btnApprove_Click" />
                                                <asp:Button ID="btnReject" runat="server" CssClass="btn btn-danger" Text="Reject" ValidationGroup="A" OnClick="btnReject_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>

                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

