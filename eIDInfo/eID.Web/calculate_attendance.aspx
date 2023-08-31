<%@ Page Title="Calculate Attendance" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="calculate_attendance.aspx.cs" Inherits="calculate_attendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCalculate" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Calculate Attendance</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel runat="server" ID="Panel1" DefaultButton="btnCalculate">
                                <div class="row">
                                    <div class="form-group col-md-2">
                                        <label class="control-label">Company </label>

                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ErrorMessage="This field is required" ID="RequiredFieldValidator3" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator4" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />

                                    </div>
                                    <div class="form-group col-md-2 hidden">
                                        <label class="control-label">Branch</label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlBranch" ErrorMessage="This field is required" ID="RequiredFieldValidator5" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />

                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">Department</label>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                        
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">Employee Code</label>
                                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtFromDate" ErrorMessage="This field is required" ID="RequiredFieldValidator1" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                        
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">To Date</label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtToDate" ErrorMessage="This field is required" ID="RequiredFieldValidator2" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="form-group col-md-2">
                                        <label class="control-label">
                                            <asp:CheckBox ID="chkHoliday" runat="server" Text="&nbsp;Holiday Map" Checked="true" /></label>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">
                                            <asp:CheckBox ID="chkWeekOff" runat="server" Text="&nbsp;Week Off Map" Checked="true" /></label>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">
                                            <asp:CheckBox ID="chkLeaves" runat="server" Text="&nbsp;Leaves Map" Checked="true" /></label>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label">
                                            <asp:CheckBox ID="chkOD" runat="server" Text="&nbsp;OD Map" Checked="true" /></label>
                                    </div>
                                    <div class="form-group col-md-2 hidden">
                                        <label class="control-label">
                                            <asp:CheckBox ID="chkMobileApp" runat="server" Text="&nbsp;Mobile App Map" Checked="true" /></label>
                                    </div>
                                    <div class="col-md-2 ">
                                        <label class="control-label">
                                            <asp:CheckBox ID="chkWFH" runat="server" Text="WFH Map" Checked="true" /></label>
                                    </div>
                                      
                                    <div class="col-md-2 ">
                                          <asp:Button ID="btnCalculate" runat="server" CssClass="btn btn-primary" Text="Calculate Attendance" ValidationGroup="A" OnClick="btnCalculate_Click" />
                                    </div>
                                   
                            <%--<asp:Button ID="btnExport" runat="server" Text="Export" OnClick="ExportToExcel" />--%>
                       
                                </div>


                                 
                                <div class="hidden"><asp:Literal ID="litMsg" runat="server"></asp:Literal></div>
                            </asp:Panel>

                            
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
        Sending Mail
    </ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>

