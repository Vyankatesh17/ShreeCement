<%@ Page Title="Branch Information" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="mst_branch_info.aspx.cs" Inherits="mst_branch_info" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_gvDataList').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
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
        <ContentTemplate>
            <div class="row">
                <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="col-lg-12">
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">Branch List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm" OnClick="btnAdd_Click" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <asp:GridView ID="gvDataList" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvDataList_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Company" DataField="CompanyName" />
                                            <asp:BoundField HeaderText="Branch" DataField="BranchName" />
                                            <asp:BoundField HeaderText="Address" DataField="Address" />
                                            <asp:BoundField HeaderText="Contact No" DataField="ContactNo" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEvent" runat="server" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("BranchId") %>' OnClick="lbtnEvent_Click"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
                                <div class="box box-primary">
                                    <div class="box-header">
                                        <h3 class="box-title">Branch Add</h3>
                                        <div class="box-tools">
                                        </div>
                                    </div>
                                    <div class="box-body">
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary pull-right" Text="Save" ValidationGroup="A" OnClick="btnSave_Click" />
                                        <asp:HiddenField ID="hfKey" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="col-lg-2"></div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

