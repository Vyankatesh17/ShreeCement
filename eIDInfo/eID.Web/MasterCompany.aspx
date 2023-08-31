<%@ Page Title="Company Information" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="MasterCompany.aspx.cs" Inherits="MasterCompany" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_Company').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Company_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 150px;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="box">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Company List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn btn-box-tool bg-orange" Text="Add New" />
                                    </div>
                                </div>
                                <div class="box-body no-padding">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grd_Company" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" AllowPaging="false" OnRowDataBound="grd_Company_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                                <asp:BoundField DataField="Website" HeaderText="Website" />
                                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />

                                                <asp:BoundField DataField="Address" HeaderText="Address" />

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%# Eval("CompanyId") %>'
                                                            Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="OnClick_Edit" ToolTip="Edit" />
                                                        <asp:ImageButton ID="Delete" runat="server" CommandArgument='<%# Eval("CompanyId") %>'
                                                            Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" OnClick="Delete_Click" ToolTip="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Add Company</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Company Name <span class="text-red">*</span></label><div class="col-sm-7">
                                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" MaxLength="40" placeholder="Enter Company Name" TabIndex="1"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="txtname_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtCompanyName" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ.()">
                                                </asp:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rq9" runat="server" CssClass="text-red" ControlToValidate="txtCompanyName"
                                                    ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Short Name <span class="text-red">*</span></label><div class="col-sm-7">
                                                <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtShortName" ID="RequiredFieldValidator1" runat="server"
                                                    ErrorMessage="This field is required" CssClass="text-red" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Email <span class="text-red">*</span></label><div class="col-sm-7">
                                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" MaxLength="40" placeholder="Enter E-mail" TabIndex="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rq8" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="This field is required" CssClass="text-red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Contact <span class="text-red">*</span></label><div class="col-sm-7">
                                                <asp:TextBox ID="txtContact" runat="server" MaxLength="10" CssClass="form-control" placeholder="Enter Contact Number" TextMode="Number"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContact"
                                                    FilterType="Numbers" Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rq4" runat="server" ControlToValidate="txtContact" ErrorMessage="This field is required"
                                                    Display="Dynamic" CssClass="text-red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Website</label><div class="col-sm-7">
                                                <asp:TextBox ID="txtWebsite" runat="server" Text="http://" CssClass="form-control" MaxLength="40" placeholder="Enter Website Address" TabIndex="2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Landline</label><div class="col-sm-7">
                                                <asp:TextBox ID="txtLandline" runat="server" CssClass="form-control" MaxLength="13" placeholder="Enter Landline Number" TabIndex="4"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Country</label><div class="col-sm-7">
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Address</label><div class="col-sm-7">
                                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="250" placeholder="Enter Address"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Pincode</label><div class="col-sm-7">
                                                <asp:TextBox ID="txtPincode" runat="server" MaxLength="6" CssClass="form-control" placeholder="Enter Pin Code"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Logo</label><div class="col-sm-7">
                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                                <asp:Label ID="lbllogo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                CssClass="btn btn-default" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                CssClass="btn btn-primary pull-right" />
                            <asp:Label ID="lblCompanyid" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>

                </asp:View>
            </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>

