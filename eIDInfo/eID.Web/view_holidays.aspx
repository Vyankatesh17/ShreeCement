<%@ Page Title="View Holidays" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="view_holidays.aspx.cs" Inherits="view_holidays" %>

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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
        <ContentTemplate>
    <div class="row">
        <div class="col-lg-12">
            <div class="box">
                <div class="box-header box-solid">
                    <h3 class="box-title">View Holidays</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2 form-group">
                            <label class="control-label">Company</label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-1 form-group">
                            <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Show" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
                <div class="box-footer no-padding">
                    <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" OnRowDataBound="gvList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Holiday Name" DataField="HoliDaysName" />
                            <asp:TemplateField HeaderText="Holiday Date">
                                <ItemTemplate>
                                     <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date", "{0:MM/dd/yyyy}")%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div></ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

