<%@ Page Title="Map Employee with Device" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="mst_employee_device_map.aspx.cs" Inherits="mst_employee_device_map" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script type="text/javascript">
        function jqFunctions() {
            $('[id*=lstFruits]').multiselect({
                includeSelectAllOption: true
            });
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
    <asp:updatepanel id="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnImport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Map Employee with Device</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label class="control-label">Company Name <span class="text-red">*</span></label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="control-label">Department Name <span class="text-red">*</span></label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-red" ControlToValidate="ddlDepartment" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" InitialValue="0" ID="RequiredFieldValidator2" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="control-label">Employee Name <span class="text-red">*</span></label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-red" ControlToValidate="ddlEmployee" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlEmployee" InitialValue="0" ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label">Device <span class="text-red">*</span></label>
                                    <div class="">                                        
                                        <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple" CssClass="form-control">
                                        </asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ControlToValidate="lstFruits" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-md-1">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;</label>
                                    <div class="">
                                         <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" ValidationGroup="A" Text="Show" OnClick="btnShow_Click" />
                                        <asp:Button ID="btnImport" runat="server" CssClass="btn btn-success" ValidationGroup="A" Text="Map" OnClick="btnImport_Click" />
                                        <asp:HiddenField ID="hfTenant" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                        <div class="box">
                            <div class="box-header with-border">
                                <h1 3 class="box-title">Mapped List</h1>
                            </div>
                            <div class="box-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" OnRowDataBound="gvList_RowDataBound">
                                        <Columns>
                                            <asp:BoundField HeaderText="Employee Name" DataField="EmpName" />
                                            <asp:BoundField HeaderText="Device Code" DataField="DeviceCode" />
                                            <asp:BoundField HeaderText="Employee No" DataField="EmployeeNo" />
                                            <asp:BoundField HeaderText="Device Name" DataField="DeviceName" />
                                            <asp:BoundField HeaderText="Account Id" DataField="DeviceAccountId" />
                                            <asp:BoundField HeaderText="Serial No" DataField="DeviceSerialNo" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>

