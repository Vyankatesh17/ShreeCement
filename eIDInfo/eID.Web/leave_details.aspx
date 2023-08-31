<%@ Page Title="Leave Details" Language="C#" AutoEventWireup="true" CodeFile="~/leave_details.aspx.cs" MasterPageFile="~/UserMaster.master"
    Inherits="leave_details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_EmpLeave').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_EmpLeave_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="row">
        <div class="col-md-12">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">Leave Details</h3>
                </div>
                <div class="box-body no-padding">
                    <asp:GridView ID="grd_EmpLeave" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                        AllowPaging="false" PageSize="10" OnRowDataBound="grd_EmpLeave_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Leave Application Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblleaveappdate" ReadOnly="true" runat="server" Text='<%# Eval("ApplicationDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblname" ReadOnly="true" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblleaveid" ReadOnly="true" runat="server" Text='<%# Eval("LeaveName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblstartdate" ReadOnly="true" runat="server" Text='<%# Eval("LeaveStartDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" ReadOnly="true" runat="server" Text='<%# Eval("LeaveEndDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duration" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblduration" ReadOnly="true" runat="server" Text='<%# Eval("Duration") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purpose">
                                <ItemTemplate>
                                    <asp:Label ID="lblpurpose" ReadOnly="true" runat="server" Text='<%# Eval("LeaveReason") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manager Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" ReadOnly="true" runat="server" Text='<%# Eval("ManagerStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HR Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblHRstatus" ReadOnly="true" runat="server" Text='<%# Eval("HRStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <div class='<%# Eval("ManagerStatus").Equals("Approve")?"hidden":"" %>'>
                                        <asp:LinkButton ID="lbtnEvent" runat="server" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("LeaveApplicationId") %>' OnClick="lbtnEvent_Click"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
