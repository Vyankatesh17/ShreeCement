<%@ Page Title="Department" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="MasterDepartment.aspx.cs" Inherits="MasterDepartment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_Dept').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Dept_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server"><Triggers><asp:PostBackTrigger ControlID="btnsubmit" /></Triggers>
        <ContentTemplate>
            <div class="row">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="col-lg-12">
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">Department List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn btn-box-tool bg-orange" Text="Add New" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grd_Dept" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_Dept_RowDataBound"
                                            CssClass="table table-bordered table-striped" AllowPaging="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                <asp:BoundField DataField="DeptName" HeaderText="Department Name" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%# Eval("DeptID") %>'
                                                            Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="OnClick_Edit" ToolTip="Edit" />
                                                        <asp:ImageButton ID="Delete" runat="server" CommandArgument='<%# Eval("DeptID") %>'
                                                            Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" OnClick="Delete_Click" ToolTip="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <asp:View ID="View2" runat="server">
                        <asp:Panel ID="pan" runat="server" DefaultButton="btnsubmit">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                                <div class="box box-primary">
                                    <div class="box-header">
                                        <h3 class="box-title">Add Department</h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Company Name <span class="text-red">*</span></label><div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-red" ControlToValidate="ddlCompany"
                                                        ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator2" runat="server" CssClass="text-red"
                                                        ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Department Name <span class="text-red">*</span></label><div class="col-sm-7">
                                                    <asp:TextBox ID="txtdeptname" runat="server" CssClass="form-control" placeholder="Enter Department Name" MaxLength="40"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtdeptname" ID="RequiredFieldValidator3" runat="server" CssClass="text-red"
                                                        ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                            CssClass="btn btn-default" />
                                        <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                            CssClass="btn btn-primary pull-right" />
                                        <asp:Label ID="lbldeptid" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-2"></div>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

