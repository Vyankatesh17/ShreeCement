<%@ Page Title="Salary Components" Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMaster.master" CodeFile="mst_salary_components.aspx.cs" Inherits="mst_salary_components" %>


<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_Salary_Component').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Salary_Component_wrapper .col-sm-6:eq(0)');
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
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <div class="col-md-12">
                            <div class="box">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Salary Component List</h3>
                                    <div class="pull-right box-tools">
                                        <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn btn-warning" Text="Add New" />
                                    </div>
                                </div>
                                <div class="box-body no-padding">
                                    <asp:GridView ID="grd_Salary_Component" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        OnRowDataBound="grd_Salary_Component_RowDataBound" AllowPaging="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="ComponentName" HeaderText="Component Name" />
                                            <asp:BoundField DataField="ComponentType" HeaderText="Component Type" />
                                            <asp:BoundField DataField="PercentageValue" HeaderText="Percentage Value" Visible="false" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/Images/i_edit.png" CommandArgument='<%# Eval("Componentid") %>'
                                                        OnClick="OnClick_Edit" ToolTip="Edit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <asp:Panel runat="server" ID="pnl" DefaultButton="btnsubmit">
                            <div class="col-md-2"></div>
                            <div class="col-md-8">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Salary Component Info</h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-3">Company Name <span class="text-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ErrorMessage="Required field" ID="RequiredFieldValidator12" runat="server"
                                                        ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-3">Component Mode <span class="text-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlComponent" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="Earning">Earning</asp:ListItem>
                                                        <asp:ListItem Value="Deduction">Deduction</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-3">Component Name <span class="text-red">*</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtComponentname" runat="server" CssClass="form-control" MaxLength="50" placeholder="Enter Component Name"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtComponentname" ErrorMessage="Required field" ID="RequiredFieldValidator2" runat="server"
                                                        ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="Button1" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                            CssClass="btn btn-default" />
                                        <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="A"
                                            CssClass="btn btn-primary pull-right" />
                                        <asp:Label ID="lblCompid" runat="server" Visible="False">
                                        </asp:Label>
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
