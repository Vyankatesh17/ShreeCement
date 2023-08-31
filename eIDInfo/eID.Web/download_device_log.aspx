<%@ Page Title="Download Device Log" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="download_device_log.aspx.cs" Inherits="download_device_log" %>

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



            var table = $('#ContentPlaceHolder1_gvDataList').DataTable({
                order: [],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }],
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis'],
                "lengthMenu": [10, 25, 50, 100, 500]

            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvDataList_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Download Device Log</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2">
                                    <label class="control-label">Company Name <span class="text-red">*</span></label>
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
                                    <label class="control-label">From Date <span class="text-red">*</span></label>
                                    <div class="">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label">To Date <span class="text-red">*</span></label>
                                    <div class="">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label">Device <span class="text-red">*</span></label>
                                    <%--<div class="">
                                        <asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ControlToValidate="ddlDevice" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDevice" InitialValue="0" ID="RequiredFieldValidator4" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>--%>

                                     <div class="">
                                        <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ControlToValidate="lstFruits" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                </div>
                                <div class="row">
                                 <div class="form-group col-md-2">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <div class="">
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show Downloaded Log" ValidationGroup="A" OnClick="btnShow_Click" />
                                    </div>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <div class="">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Download Log" ValidationGroup="A" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                 </div>
                               <div class="row">
                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:GridView ID="gvDataList" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvDataList_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Company Name" DataField="CompanyName" />
                                    <asp:BoundField HeaderText="Employee Name" DataField="EmpName" />
                                    <asp:BoundField HeaderText="Machine ID" DataField="employeeNoString" />
                                    <asp:BoundField HeaderText="Access Card No" DataField="cardNo" />
                                    <asp:BoundField HeaderText="Device Account Id" DataField="DeviceAccountId" />
                                    <asp:BoundField HeaderText="Punch Date" DataField="time" DataFormatString = "{0:dd/MM/yyyy}"/>
                                    <asp:BoundField HeaderText="Punch Time" DataField="time" DataFormatString = "{0:hh:mm:tt}"/>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

