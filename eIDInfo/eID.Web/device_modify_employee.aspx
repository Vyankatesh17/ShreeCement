<%@ Page Title="Modify Employee" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="device_modify_employee.aspx.cs" Inherits="device_modify_employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script type="text/javascript">
        function jqFunctions() {
            $('[id*=lstFruits]').multiselect({
                includeSelectAllOption: true
            });

        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnModify" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Modify Employee Record</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Employee Name</label>
                                        <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtStartTime" ErrorMessage="This field is required" ID="RequiredFieldValidator3" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Employee No</label>
                                        <asp:TextBox ID="txtEmployeeNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtStartTime" ErrorMessage="This field is required" ID="RequiredFieldValidator4" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Device <span class="text-red">*</span></label>
                                        <div class="">
                                            <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple" CssClass="form-control"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ControlToValidate="lstFruits" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Begin Time</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtStartTime" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-addon">Time</div>
                                            <asp:TextBox ID="txtStartT" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="txtStartTime" ErrorMessage="This field is required" ID="RequiredFieldValidator1" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtStartT" ErrorMessage="This field is required" ID="RequiredFieldValidator6" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">End Time</label>
                                        <div class="input-group">
                                        <asp:TextBox ID="txtEndTime" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                             <div class="input-group-addon">Time</div>
                                            <asp:TextBox ID="txtEndT" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="txtEndTime" ErrorMessage="This field is required" ID="RequiredFieldValidator2" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtEndT" ErrorMessage="This field is required" ID="RequiredFieldValidator7" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnModify" runat="server" Text="Modify Time" ValidationGroup="A" CssClass="btn btn-primary" OnClick="btnModify_Click" />
                                        <asp:HiddenField ID="hfEmpId" runat="server" />
                                        <asp:HiddenField ID="hfDeviceCode" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

