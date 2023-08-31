<%@ Page Title="Import Employees From Devices" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="import_employees_from_devices.aspx.cs" Inherits="import_employees_from_devices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="admin_theme/dist/css/bootstrap_multiselect.css" rel="stylesheet" />
    <script src="admin_theme/dist/js/bootstrap_multiselect.js"></script>
    <script type="text/javascript">
        function jqFunctions() {

            $('#ContentPlaceHolder1_lstFruits').multiselect({

                includeSelectAllOption: true,
                enableFiltering: true,
                maxHeight: 450,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
                dropRight: true,
                onDropdownShow: function (event) {
                    $(this).closest('select').css('width', '500px')
                }
            });
        }

            $(document).ready(function () {
                jqFunctions();
            });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Download Employees from Device</h3>
                        </div>
                        <div class="box-body">
                            <div class="row ">
                                
                                    <div class="form-group col-md-2">
                                        <label class="control-label">Company <span class="text-red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>          
                                <div class="form-group col-md-2">
                                    <label class="control-label">Location</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select Location</asp:ListItem>
                                            <asp:ListItem Value="BEAWAR">BEAWAR</asp:ListItem>
                                            <asp:ListItem Value="BIHAR">BIHAR</asp:ListItem>
                                            <asp:ListItem Value="BULANDSHAHAR">BULANDSHAHAR</asp:ListItem>
                                            <asp:ListItem Value="GUNTUR">GUNTUR</asp:ListItem>
                                            <asp:ListItem Value="GURUGRAM">GURUGRAM</asp:ListItem>
                                            <asp:ListItem Value="JAIPUR">JAIPUR</asp:ListItem>
                                            <asp:ListItem Value="JHARKHAND">JHARKHAND</asp:ListItem>
                                            <asp:ListItem Value="KHUSHKHERA">KHUSHKHERA</asp:ListItem>
                                            <asp:ListItem Value="KODLA">KODLA</asp:ListItem>
                                            <asp:ListItem Value="NAWALGARH">NAWALGARH</asp:ListItem>
                                            <asp:ListItem Value="ODISHA">ODISHA</asp:ListItem>
                                            <asp:ListItem Value="PANIPAT">PANIPAT</asp:ListItem>
                                            <asp:ListItem Value="PUNE">PUNE</asp:ListItem>
                                            <asp:ListItem Value="PURULIA">PURULIA</asp:ListItem>
                                            <asp:ListItem Value="RAS">RAS</asp:ListItem>
                                            <asp:ListItem Value="ROORKEE">ROORKEE</asp:ListItem>
                                            <asp:ListItem Value="SURATGARH">SURATGARH</asp:ListItem>
                                            <asp:ListItem Value="RAIPUR">RAIPUR</asp:ListItem>
                                            <asp:ListItem Value="CCR">CCR</asp:ListItem>
                                            <asp:ListItem Value="Main Gate">Main Gate</asp:ListItem>
                                        </asp:DropDownList>                                        
                                    </div>
                                </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">Device  <span class="text-red">*</span></label>
                                        <div class="">
                                        <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ControlToValidate="lstFruits" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                        <%--<div class="">
                                            <asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>--%>
                                    </div>
                              
                               
                                    <div class="form-group col-sm-6">
                                        <label class="control-label">&nbsp;&nbsp;&nbsp;</label>
                                        <div class="">
                                            <asp:Button ID="btnDownloadEmployees" runat="server" CssClass="btn btn-primary" ValidationGroup="A" Text="Download Employees" OnClick="btnDownloadEmployees_Click"/>
                                            <asp:Button ID="btndownloadface" runat="server" CssClass="btn btn-box-tool btn-bitbucket" ValidationGroup="A" Text="Download Face" OnClick="btnDownloadEmployeesFace_Click"/>
                                            <asp:Button ID="btndownloadfingerprint" runat="server" CssClass="btn btn-box-tool btn-github" ValidationGroup="A" Text="Download Fingerprint" OnClick="btnDownloadEmployeesFingerprint_Click"/>
                                            <asp:Button ID="btndownloadcard" runat="server" CssClass="btn btn-box-tool bg-orange" ValidationGroup="A" Text="Download Card" OnClick="btnDownloadEmployeesCard_Click"/>
                                            <asp:Button ID="btndownloadpassword" runat="server" CssClass="btn btn-box-tool bg-red" ValidationGroup="A" Text="Download Password" OnClick="btnDownloadEmployeespassword_Click"/>
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

