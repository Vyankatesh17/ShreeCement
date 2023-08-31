<%@ Page Title="Department Heads" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="mst_department_heads.aspx.cs" Inherits="mst_department_heads" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_Deptmaster').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Deptmaster_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            <div class="row">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="col-lg-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Department Head List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnadd" runat="server" Text="Add New" CssClass="btn btn-box-tool bg-orange" OnClick="btnadd_Click" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grd_Deptmaster" runat="server" AutoGenerateColumns="False"  CssClass="table table-bordered table-striped" OnRowDataBound="grd_Deptmaster_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="HeadId" HeaderText="Head Id" Visible="false" />
                                                <asp:BoundField DataField="FName" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="DeptName" HeaderText=" Department Name" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton Text="Edit" runat="server" CommandArgument='<%#Eval("HeadId")%>' ID="lnk" OnClick="Unnamed1_Click" />
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
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                                <div class="form-horizontal">
                                    <div class="box box-primary">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Add Department Head</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Company Name <span class="text-red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ControlToValidate="ddlCompany"
                                                        ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator6" runat="server" CssClass="text-red"
                                                        ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Employee Name <span class="text-red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlempname" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblheadid" runat="server" Visible="False"></asp:Label>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddlempname" ErrorMessage="This field is required" CssClass="text-red" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddlempname" InitialValue="0" ErrorMessage="This field is required" CssClass="text-red" ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Department Name <span class="text-red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddldeptname" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddldeptname" ErrorMessage="This field is required" CssClass="text-red" ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddldeptname" InitialValue="0" ErrorMessage="This field is required" CssClass="text-red" ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="box-footer">
                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" CssClass="btn btn-default" />
                                            <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="A" CssClass="btn btn-primary pull-right" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

