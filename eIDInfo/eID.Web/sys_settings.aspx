<%@ Page Title="System Settings" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="sys_settings.aspx.cs" Inherits="sys_settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">System Settings</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-2 form-group">
                                    <label class="control-label">
                                        Company
                                    </label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ErrorMessage="This field is required" ID="RequiredFieldValidator3" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator4" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                                </div>
                                <div class="col-lg-2 form-group">
                                    <label class="control-label">
                                        Latemarks Allowed
                                    </label>
                                    <asp:TextBox ID="txtLatemarkAllowed" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 form-group">
                                    <label class="control-label">
                                        Consider Latemark As
                                    </label>
                                    <asp:RadioButtonList ID="rbCLA" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="H" Selected="True">Half Day</asp:ListItem>
                                        <asp:ListItem Value="F">Full Day</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-lg-3 form-group">
                                     <label class="control-label">
                                        Attendance Calculation
                                    </label>
                                    <asp:RadioButtonList ID="rbAttendance" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="ShiftGroup" Selected="True">Shift Group</asp:ListItem>
                                        <asp:ListItem Value="RoasterGroup" >Roaster Group</asp:ListItem>                                        
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-lg-3 form-group">
                                     <label class="control-label">
                                        Weeklyoff Calculation
                                    </label>
                                    <asp:RadioButtonList ID="rbWeeklyoff" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="CompanyWise" Selected="True">Company Wise</asp:ListItem>
                                        <asp:ListItem Value="EmployeeWise" >Employee Wise</asp:ListItem>                                        
                                    </asp:RadioButtonList>
                                </div>
                                 </div>
                                <div class="row">
                                <div class="col-lg-2 form-group">
                                     <label class="control-label">
                                        OT Calculation
                                    </label>
                                    <asp:RadioButtonList ID="rbOtCalculation" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="OTInTime" Selected="True">OT</asp:ListItem>
                                        <asp:ListItem Value="OTInDays" >OT in Days</asp:ListItem>                                        
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-lg-2 form-group">
                                     <label class="control-label">
                                        Half Day OT Hours
                                    </label>
                                   <asp:TextBox ID="txthalfdayhours" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                     <%--<asp:RequiredFieldValidator ControlToValidate="txthalfdayhours" ErrorMessage="Required field" ID="RequiredFieldValidator10" runat="server"
                                        ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>  --%>                     
                                </div>
                                <div class="col-lg-2 form-group">
                                     <label class="control-label">
                                        Full Day OT  Hours
                                    </label>
                                   <asp:TextBox ID="txtfulldayhours" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                     <%--<asp:RequiredFieldValidator ControlToValidate="txtfulldayhours" ErrorMessage="Required field" ID="RequiredFieldValidator1" runat="server"
                                        ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>--%>                       
                                </div>
                                <div class="col-lg-2 form-group">
                                     <label class="control-label">
                                            <asp:CheckBox ID="chkqrcode" runat="server" Text="&nbsp;Scan QR Code" /></label>       
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary pull-right" Text="Save" ValidationGroup="A" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

