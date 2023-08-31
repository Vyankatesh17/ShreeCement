<%@ Page Title="Leave Master" Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMaster.master" CodeFile="mst_leave_info.aspx.cs" Inherits="mst_leave_info" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_Leave').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Leave_wrapper .col-sm-6:eq(0)');

            isCarryForward();
        }

        $(document).ready(function () {
            jqFunctions();
        });


        //$("#color").prop('disabled', true);
        function isCarryForward() {
            if ($('ContentPlaceHolder1_chkCarryforward').checked) {
                $("#ContentPlaceHolder1_txtCarryforwardLimit").prop('disabled', false);
                document.getElementById('ContentPlaceHolder1_txtCarryforwardLimit').value = 10;
            } else {

                $("#ContentPlaceHolder1_txtCarryforwardLimit").prop('disabled', true);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsubmit" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <div class="col-lg-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Leave Type List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnadd" runat="server" Text="Add New" CssClass="btn btn-box-tool bg-orange" OnClick="btnadd_Click" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grd_Leave" runat="server" AutoGenerateColumns="False" 
                                            CssClass="table table-bordered table-striped" OnRowDataBound="grd_Leave_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LeaveName" HeaderText="Leave Name" />
                                                <asp:BoundField DataField="LeaveTypeSName" HeaderText="Short Name" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                <asp:BoundField DataField="YearlyLimit" HeaderText="Yearly Limit" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ToolTip="Edit" runat="server"
                                                            CommandArgument='<%#Eval("LeaveID")%>'
                                                            ID="lnk" OnClick="lnk_Click" ImageUrl="~/Images/i_edit.png" />
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
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                                <div class="form-horizontal">
                                    <div class="box box-primary">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Add Leave Type</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Company Name <span class="text-red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ControlToValidate="ddlCompany"
                                                        ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator4" runat="server" CssClass="text-red"
                                                        ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Leave Type Name <span class="text-red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtleavename" runat="server" CssClass="form-control" MaxLength="35"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="txtleavename_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtleavename" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtleavename" ErrorMessage="This field is required" ValidationGroup="S" CssClass="text-red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Leave Type Short Name <span class="text-red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtShortName" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtShortName" ErrorMessage="This field is required" ValidationGroup="S" CssClass="text-red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Days <span class="text-red">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtmaxday" runat="server" CssClass="form-control" MaxLength="3" TextMode="Number"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="txtmaxday_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtmaxday" ValidChars="0123456789">
                                                    </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtmaxday" ErrorMessage="This field is required" CssClass="text-red" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Is Carryforward</label>
                                                <div class="col-sm-7">
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <asp:CheckBox ID="chkCarryforward" runat="server" onclick="isCarryForward();"/>
                                                        </div>
                                                        <asp:TextBox ID="txtCarryforwardLimit" runat="server" CssClass="form-control" style="" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="box-footer">
                                            <asp:Label ID="lblLeaveid" runat="server" Visible="False"></asp:Label>
                                            <asp:Button ID="btncancel" runat="server" CssClass="btn btn-default" OnClick="btncancel_Click" Text="Cancel" />
                                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary pull-right" OnClick="btnsubmit_Click" Text="Save" ValidationGroup="S" />
                                        </div>
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
