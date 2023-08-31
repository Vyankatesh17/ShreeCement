<%@ Page Title="Weekly of Settings" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="mst_weekoff_setting.aspx.cs" Inherits="mst_weekoff_setting" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grdweekoffdata').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grdweekoffdata_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="col-lg-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Week Offs List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnadd" runat="server" Text="Add New" CssClass="btn btn-box-tool bg-orange" OnClick="btnadd_Click" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdweekoffdata" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            CssClass="table table-bordered table-striped" OnRowDataBound="grdweekoffdata_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                <asp:BoundField DataField="Days" HeaderText="Days" />
                                                <asp:BoundField DataField="TrackHolidays" HeaderText="Holidays Track" />
                                                <asp:BoundField DataField="Date" HeaderText="Effective Date" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                            <div class="col-lg-12">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Add Week off</h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="form-group col-sm-3">
                                                <label class="control-label">Company <span class="text-red">*</span></label>
                                                <asp:Label ID="lblcompanyid" runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-sm-2">
                                                <label class="control-label">Off days <span class="text-red">*</span></label>
                                                <asp:DropDownList ID="ddsatoff" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="All">All</asp:ListItem>
                                                    <asp:ListItem Value="1 & 3">1 &amp; 3 </asp:ListItem>
                                                    <asp:ListItem Value="2 & 4">2 &amp; 4</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-sm-3">
                                                <label class="control-label">Day <span class="text-red">*</span></label>
                                                <asp:DropDownList ID="dddays" runat="server" CssClass="form-control">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem>Monday</asp:ListItem>
                                                    <asp:ListItem>Tuesday</asp:ListItem>
                                                    <asp:ListItem>Wednesday</asp:ListItem>
                                                    <asp:ListItem>Thursday</asp:ListItem>
                                                    <asp:ListItem>Friday</asp:ListItem>
                                                    <asp:ListItem>Saturday</asp:ListItem>
                                                    <asp:ListItem>Sunday</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-sm-3">
                                                <label class="control-label">Effective Date <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" CssClass="text-red" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-sm-1">
                                                <label class="control-label"></label>
                                                <asp:ImageButton ID="ImgCitAdd" runat="server" ImageUrl="~/Images/i_add.png" CssClass="form-control" Style="height: 37px; width: 37px" ValidationGroup="d" ToolTip="Add" OnClick="ImgCitAdd_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                            CssClass="table table-bordered table-striped">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" Visible="false" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" />
                                                <asp:BoundField DataField="TrackHolidays" HeaderText="Type" />
                                                <asp:BoundField DataField="Days" HeaderText="Days" />
                                                <asp:BoundField DataField="Date" HeaderText="Effective Date" />
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                            ImageUrl="~/Images/i_edit.png" ToolTip="Edit" OnClick="Edit_Click" />
                                                        <asp:ImageButton ID="Delete" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                            ImageUrl="~/Images/i_delete.png" ToolTip="Delete" OnClick="Delete_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" />
                                        </asp:GridView>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" CausesValidation="False" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
