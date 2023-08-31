<%@ Page Title="Process Employees Salary" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="salary_process.aspx.cs" Inherits="salary_process" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding: 10px 20px; z-index: 999999; font-size: 16px; font-weight: 600;" class="bg-red">
        <a class="float-right" href="#" data-toggle="tooltip" data-placement="left" title="Never show me this!" style="color: rgb(255, 255, 255); font-size: 20px;"><i class="fa fa-times"></i>&nbsp; </a><span class="">Ensure attendance is generated against selected month before you proceed!</span> <a class="btn btn-default btn-sm pull-right" href="calculate_attendance.aspx" style="margin-top: -5px; border: 0px; box-shadow: none; color: rgb(243, 156, 18); font-weight: 600; background: rgb(255, 255, 255);">Calculate attendance!</a>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="box">
                            <div class="box-header with-border">
                                <h3 class="box-title">Generate Salary</h3>
                                <div class="box-tools">
                                    <a href="mst_employee_list_parabhani.aspx" class="btn btn-primary btn-sm">Back to List</a>
                                </div>
                            </div>
                            <div class="box-body" style="border-bottom: 1px solid #d2d6de;">
                                <div class="row">
                                    <div class="col-lg-2 form-group">
                                        <label class="control-label">Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompanyList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3 form-group">
                                        <label class="control-label">Department</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3 form-group">
                                        <label class="control-label">Employee</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlEmployeeList"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3 form-group">
                                        <label class="control-label">Month / Year</label>
                                        <div class="input-group">
                                            <div class="input-group-btn">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMonth">
                                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="input-group-btn">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlYear"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 form-group">
                                        <label class="control-label"></label>
                                        <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-primary" Text="Generate Salary" ValidationGroup="S" />
                                    </div>
                                </div>
                            </div>

                            <div class="box-footer">
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" />
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary pull-right" Text="Save" ValidationGroup="A" OnClick="btnSave_Click" />
                                <asp:HiddenField ID="hfEmpId" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

