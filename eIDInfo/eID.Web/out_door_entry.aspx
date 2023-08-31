<%@ Page Title="OD Entry" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="out_door_entry.aspx.cs" Inherits="out_door_entry" %>

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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="col-lg-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">OD List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btn btn-warning btn-sm" OnClick="btnAdd_Click" />
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
                                            <asp:BoundField HeaderText="From Date" DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}"/>
                                             <asp:BoundField HeaderText="To Date" DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="Travel Place" DataField="TravelPlace" />
                                            <asp:BoundField HeaderText="TravelReason" DataField="TravelReason" />
                                            <asp:BoundField HeaderText="EmpName" DataField="EmpName" />
                                            <asp:BoundField HeaderText="EmployeeNo" DataField="EmployeeNo" />
                                            <asp:BoundField HeaderText="Is Approve" DataField="ManagerStatus" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class='<%# Eval("ManagerStatus").Equals("Approve")?"hidden":"" %>'>
                                                        <asp:LinkButton ID="lbtnEvent" runat="server" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("OddId") %>' OnClick="lbtnEvent_Click"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                                                    </div>
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
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">OD Entry</h3>
                                </div>
                                <div class="box-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Company Name <span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Department <span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="text-red" ControlToValidate="ddlDepartment" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" InitialValue="0" ID="RequiredFieldValidator12" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Employee Name <span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="text-red" ControlToValidate="ddlEmployee" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlEmployee" InitialValue="0" ID="RequiredFieldValidator9" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">From Date<span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtFromDate" ErrorMessage="This field is required" CssClass="text-red" Display="Dynamic" ValidationGroup="A" ID="RequiredFieldValidator1" runat="server" />
                                               
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label class="control-label col-sm-3">To Date<span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtToDate" ErrorMessage="This field is required" CssClass="text-red" Display="Dynamic" ValidationGroup="A" ID="RequiredFieldValidator10" runat="server" />
                                               
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">From Time<span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFromTime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtFromTime" ErrorMessage="This field is required" CssClass="text-red" Display="Dynamic" ValidationGroup="A" ID="RequiredFieldValidator4" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">To Time<span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtToTime" runat="server" CssClass="form-control" type="Time"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtToTime" ErrorMessage="This field is required" CssClass="text-red" Display="Dynamic" ValidationGroup="A" ID="RequiredFieldValidator5" runat="server" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Travel Reason<span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtTravelReason" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtTravelReason" ErrorMessage="This field is required" CssClass="text-red" Display="Dynamic" ValidationGroup="A" ID="RequiredFieldValidator2" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Travel Place<span class="text-red">*</span></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtTravelPlace" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtTravelPlace" ErrorMessage="This field is required" CssClass="text-red" Display="Dynamic" ValidationGroup="A" ID="RequiredFieldValidator3" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Details</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDetails" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary pull-right" Text="Save" ValidationGroup="A" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
                            <asp:HiddenField ID="hfKey" runat="server" />

            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

