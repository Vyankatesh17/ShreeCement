<%@ Page Title="Biometric Device" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="add_device.aspx.cs" Inherits="add_device" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

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
    <div class="row">
        <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
            <asp:View ID="View1" runat="server">
                <div class="col-lg-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Device List</h3>
                            <div class="box-tools">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btn btn-warning btn-sm" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnImport" runat="server" CssClass="btn btn-box-tool btn-github" Text="Import Device" OnClick="btnImport_Click" />
                            </div>
                        </div>
                        <div class="box-body">
                            <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered table-striped table-responsive" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvList_RowDataBound">
                                <columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <itemtemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:BoundField HeaderText="Company Name" DataField="CompanyName" />--%>
                                    <asp:BoundField HeaderText="Device Name" DataField="DeviceName" />
                                    <asp:BoundField HeaderText="Serial No" DataField="DeviceSerialNo" />
                                    <asp:BoundField HeaderText="Web IP Address" DataField="IPAddress" />
                                    <asp:BoundField HeaderText="Port No" DataField="PortNo" />
                                    <asp:BoundField HeaderText="Location" DataField="DeviceLocation" />
                                    <asp:BoundField HeaderText="Account Id" DataField="DeviceAccountId" />
                                    <asp:BoundField HeaderText="Direction" DataField="DeviceDirection" />
                                    <asp:BoundField HeaderText="Device Model" DataField="DeviceModel" />
                                    <asp:TemplateField HeaderText="Status">
                                        <itemtemplate>
                                            <span class='<%# Eval("Status").Equals("online")?"label label-success":"label label-warning" %>'>
                                                <%#Eval("Status") %>
                                            </span>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <itemtemplate>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#Eval("DeviceId") %>' OnClick="lbtnEdit_Click" CssClass="btn btn-sm btn-info">
                                                <i class="fa fa-edit"></i>Edit
                                            </asp:LinkButton>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                </columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div class="col-lg-12">
                    <div class="form-horizontal">
                        <asp:UpdatePanel runat="server" ID="Panel1">
                            <Triggers>                       
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <asp:PostBackTrigger ControlID="btnsettime" />
                        <asp:PostBackTrigger ControlID="btnReset" />
                    </Triggers>
                            <ContentTemplate>
                            <div class="box">
                                <div class="box-header box-solid">
                                    <h3 class="box-title">Add Device</h3>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Company Name <span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ControlToValidate="ddlCompany"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A">
                                                </asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator4" runat="server" CssClass="text-red"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Device Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtDeviceName" ErrorMessage="Device name required" ID="RequiredFieldValidator1" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Serial No</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtSerialNo" ErrorMessage="Serial no required" ID="RequiredFieldValidator2" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Account ID</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDeviceId" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtDeviceId" ErrorMessage="Device id required" ID="RequiredFieldValidator6" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                                <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="A" CssClass="text-red" ControlToValidate="txtDeviceId" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{4,}$" runat="server" ErrorMessage="Minimum 4 characters required."></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Web IP Address</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtIpAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtIpAddress" ErrorMessage="IP address required" ID="RequiredFieldValidator7" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Port Number</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPortNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtPortNo" ErrorMessage="Port no required" ID="RequiredFieldValidator8" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                                <asp:RegularExpressionValidator ControlToValidate="txtPortNo" ValidationExpression="\d+" ErrorMessage="Only numbers are allowed" CssClass="text-red" runat="server" ValidationGroup="A"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Location</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Direction</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlDirection" runat="server" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="InOut">In/Out</asp:ListItem>
                                                    <asp:ListItem Value="In">In</asp:ListItem>
                                                    <asp:ListItem Value="Out">Out</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlDirection" ErrorMessage="Direction required" ID="RequiredFieldValidator5" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Device IP</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDeviceIp" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label class="control-label col-sm-4">Device Model</label>
                                            <div class="col-sm-8">
                                                <%--<asp:TextBox ID="txtDeviceModel" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddldeviceModel" runat="server" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="0">Select Device Model</asp:ListItem>
                                                    <asp:ListItem Value="Hikvision">Hikvision</asp:ListItem>
                                                    <asp:ListItem Value="Others">Others</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="SetTime" visible="false">
                                        <div class="form-group col-md-12">
                                            <label class="control-label col-sm-3">Set Device DateTime</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtsetdatefordevice"  TextMode="Date" CausesValidation="True" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3">
                                                <asp:TextBox ID="txtsettimefordevice"  TextMode="Time" CausesValidation="True" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="btnsettime" runat="server" CssClass="btn btn-primary" Text="Set time" OnClick="btnSetTime_Click" />                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer">
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnReset_Click" />
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary pull-right" Text="Save" OnClick="btnSave_Click" ValidationGroup="A" />
                                    <asp:HiddenField ID="hfKey" runat="server" />
                                </div>
                            </div>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>

