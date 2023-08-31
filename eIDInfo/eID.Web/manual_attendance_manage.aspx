<%@ Page Title="Manual Attendance" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="manual_attendance_manage.aspx.cs" Inherits="manual_attendance_manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script>
        function jqFunctions() {
            var table = $('#ContentPlaceHolder1_gvPendingLeaves').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvPendingLeaves_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <asp:updatepanel id="updt" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="btnsubmit" />
                </Triggers>
                <ContentTemplate>
                    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Today's Manual Attendance List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                            Text="Add New" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="form-group col-lg-3">
                                            <label class="control-label">Company <span class="text-red">*</span></label>
                                            <asp:DropDownList ID="ddlCompanyList" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyList_SelectedIndexChanged" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3">
                                            <label class="control-label">Department </label>
                                            <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlDepartmentList" OnSelectedIndexChanged="ddlDepartmentList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3">
                                            <label class="control-label">Employee </label>
                                            <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlEmployeeList"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-2">
                                            <label class="control-label">Date </label>
                                            <asp:TextBox ID="txtAttDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-1">
                                            <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp; </label>
                                            <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer no-padding">
                                    <asp:GridView ID="grdAtt" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowDataBound="grdAtt_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="AttendanceDate" HeaderText="Attendance Date" DataFormatString="{0:MM/dd/yyyy}" />
                                            <asp:BoundField DataField="Status" HeaderText="Attendance Status" />
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <%#Eval("Remarks") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Manual Attendance Info</h3>
                                    </div>
                                    <div class="box-body">
                                        <asp:Panel ID="PnlAdd" runat="server" DefaultButton="btnsubmit">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label>Select Company <span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCompany" Display="Dynamic" ErrorMessage="Required Company Name" SetFocusOnError="True" ValidationGroup="A" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCompany" Display="Dynamic" ErrorMessage="Required Company Name" SetFocusOnError="True" ValidationGroup="A" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                                </div>
                                                 <%--<div class="form-group col-md-3">
                                                    <label class="control-label">Device <span class="text-red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="text-red" ControlToValidate="ddlDevice" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ControlToValidate="ddlDevice" InitialValue="0" ID="RequiredFieldValidator11" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>--%>

                                                <div class="col-md-3">
                                                    <label>Department <span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="ddlDepartment" Display="Dynamic" ErrorMessage="Required field" SetFocusOnError="True" ValidationGroup="A" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                        ControlToValidate="ddlDepartment" Display="Dynamic" ErrorMessage="Required field" SetFocusOnError="True" ValidationGroup="A" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Select Employee <span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="True" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlEmployee" Display="Dynamic" ErrorMessage="Required Employee Name" SetFocusOnError="True" ValidationGroup="A" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlEmployee" Display="Dynamic" ErrorMessage="Required Employee Name" SetFocusOnError="True" ValidationGroup="A" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Status <span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="In" Selected="True">In</asp:ListItem>
                                                        <asp:ListItem Value="Out">Out</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label>Attendance Date <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="MM/dd/yyyy">
                                                    </asp:CalendarExtender>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                        ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="Required Attendance Date"
                                                        SetFocusOnError="True" ValidationGroup="A" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Time <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtInTime" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                        ControlToValidate="txtInTime" Display="Dynamic" ErrorMessage="Required In Time"
                                                        SetFocusOnError="True" ValidationGroup="A" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                               <%-- <div class="col-md-3">
                                                    <label>Out Time <span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtOutTime" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                        ControlToValidate="txtOutTime" Display="Dynamic" ErrorMessage="Required Out Time"
                                                        SetFocusOnError="True" ValidationGroup="A" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>--%>
                                                <div class="col-md-3">
                                                    <label>Remarks</label>
                                                    <asp:TextBox runat="server" ID="txtRemrks" TextMode="MultiLine" CssClass="form-control" />
                                                </div>
                                                <div class="col-md-6">
                                                    <br />
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="A"
                                                        CssClass="btn bg-blue-active" />

                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" CausesValidation="false"
                                                        CssClass="btn bg-blue-active" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

    </asp:View>
                    </asp:MultiView>
                 
                </ContentTemplate>
            </asp:updatepanel>
        </div>
    </div>
</asp:Content>

