<%@ Page Title="Manual Attendance Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="manual_attendance_report.aspx.cs" Inherits="manual_attendance_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <div class="box">
                        <div class="box-header box-solid">
                            <h3 class="box-title">Manual Attendance Report</h3>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
