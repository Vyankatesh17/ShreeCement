<%@ Page Title="Device Statuses" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="dash_device_status.aspx.cs" Inherits="dash_device_status" %>

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
        <div class="col-lg-12">
            <div class="box">
                <div class="box-body">
                    <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered table-striped table-responsive" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Company Name" DataField="CompanyName" />
                            <asp:BoundField HeaderText="Device Name" DataField="DeviceName" />
                            <asp:BoundField HeaderText="Serial No" DataField="DeviceSerialNo" />
                            <asp:BoundField HeaderText="Web IP Address" DataField="IPAddress" />
                            <asp:BoundField HeaderText="Port No" DataField="PortNo" />
                            <asp:BoundField HeaderText="Location" DataField="DeviceLocation" />
                            <asp:BoundField HeaderText="Account Id" DataField="DeviceAccountId" />
                            <asp:BoundField HeaderText="Direction" DataField="DeviceDirection" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <span class='<%# Eval("Status").Equals("online")?"label label-success":"label label-warning" %>'>
                                        <%#Eval("Status") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

