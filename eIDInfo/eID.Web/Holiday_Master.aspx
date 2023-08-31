<%@ Page Title="Holiday Master" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="Holiday_Master.aspx.cs" Inherits="Holiday_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_Holiday').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Holiday_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="col-lg-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Holiday List</h3>
                            <div class="box-tools">
                                <asp:Button ID="btnadd" runat="server" Text="Add New" CssClass="btn btn-box-tool bg-orange" OnClick="btnadd_Click" />
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Sort By</label>
                                        <asp:DropDownList ID="ddlsort" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlsort_SelectedIndexChanged">
                                            <asp:ListItem>ALL</asp:ListItem>
                                            <asp:ListItem>Year-Wise</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label">Year</label>
                                        <asp:TextBox ID="txtyear" runat="server" Visible="false" MaxLength="4" CssClass="form-control" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RegularExpressionValidator122" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtyear" ForeColor="Red" ValidationGroup="N"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtyear" ValidationExpression="\d{4}" ForeColor="Red" ValidationGroup="N"></asp:RegularExpressionValidator>
                                        <asp:FilteredTextBoxExtender ID="txtyear_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txtyear" ValidChars="0123456789">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <label class="control-label">&nbsp;&nbsp;&nbsp;</label>
                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" OnClick="btnsearch_Click" ValidationGroup="N" Text="Search" CausesValidation="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="table-responsive">
                                <asp:GridView ID="grd_Holiday" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped" OnRowDataBound="grd_Holiday_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="HolidaysId" HeaderText="Country Id" Visible="false" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                        <asp:BoundField DataField="HoliDaysName" HeaderText="Holiday Name" />
                                        <asp:BoundField DataField="Date" HeaderText="Holiday Date" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk" runat="server" CommandArgument='<%#Eval("HolidaysId")%>'
                                                    ImageUrl="~/Images/i_edit.png" ToolTip="Edit" OnClick="Unnamed1_Click" />&nbsp;&nbsp;<asp:ImageButton ID="Delete" runat="server" CommandArgument='<%# Eval("HolidaysId") %>'
                                                        OnClick="Delete_Click" Text="Delete" ImageUrl="~/Images/i_delete.png"
                                                        ToolTip="Delete" /><asp:ConfirmButtonExtender ID="Delete_ConfirmButtonExtender"
                                                            runat="server" ConfirmText="Do you Really want to delete ..?" Enabled="True"
                                                            TargetControlID="Delete">
                                                        </asp:ConfirmButtonExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div class="col-lg-2"></div>
                <div class="col-lg-8">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnsubmit" />
                        </Triggers>
                        <ContentTemplate>
                    <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                        <div class="form-horizontal">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Add Holiday</h3>
                                </div>
                                <div class="box-body">
                                    <div class="form-group">
                                            <label class="control-label col-sm-4">Company Name <span class="text-red">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Holiday Name</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtholidayname" runat="server" CausesValidation="True" ValidationGroup="w" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtholidayname" CssClass="text-red"
                                                ErrorMessage="Enter Holiday Name" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Holiday Date</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="dtpholydate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dtpholydate" CssClass="text-red" ErrorMessage="Select Date" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer">
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" CssClass="btn btn-default" />
                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="a" CssClass="btn btn-primary pull-right" />
                                    <asp:Button ID="btnTest" runat="server" Text="Test" OnClick="btnTest_Click" CssClass="btn btn-primary pull-right hidden" />
                                    <asp:Label ID="lblid" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:Panel></ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
