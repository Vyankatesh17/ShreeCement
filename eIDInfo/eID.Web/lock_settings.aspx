<%@ Page Title="System Lock Settings" Language="C#" MasterPageFile="~/UserMaster1.master" AutoEventWireup="true" CodeFile="lock_settings.aspx.cs" Inherits="lock_settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLock" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Verify Account</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <label class="control-label">Username <span class="text-red">*</span></label>
                                <asp:TextBox ID="txtLUsername" runat="server" CssClass="form-control" placeholder="Username" autocomplete="off"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLUsername" ErrorMessage="Required field" ID="RequiredFieldValidator1" runat="server" ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label class="control-label">Password <span class="text-red">*</span></label>
                                <asp:TextBox ID="txtLPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLPassword" ErrorMessage="Required field" ID="RequiredFieldValidator2" runat="server" ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnRequestCode" runat="server" Text="Generate OTP" CssClass="btn btn-primary" ValidationGroup="A" OnClick="btnRequestCode_Click" />
                            </div>
                            <div class="form-group">
                                <label class="control-label">OTP <span class="text-red">*</span></label>
                                <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" placeholder="OTP" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtOTP" ErrorMessage="Required field" ID="RequiredFieldValidator3" runat="server" ValidationGroup="B" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnValidate" runat="server" Text="Validate" CssClass="btn btn-success" ValidationGroup="B" OnClick="btnValidate_Click" />
                                <asp:HiddenField ID="hfKey" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="Panel1" runat="server" Visible="false">
                    <div class="col-md-6">
                        <div class="box">
                            <div class="box-header with-border">
                                <h3 class="box-title">Lock Settings</h3>
                            </div>
                            <div class="box-body">
                                <div class="form-group">
                                    <label class="control-label">Expiry Date<span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtExpiryDate" ErrorMessage="Required field" ID="RequiredFieldValidator4" runat="server" ValidationGroup="C" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Allowed Devices<span class="text-red">*</span></label>
                                    <asp:TextBox ID="txtAllowedDevices" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtAllowedDevices" ErrorMessage="Required field" ID="RequiredFieldValidator5" runat="server" ValidationGroup="C" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnLock" runat="server" CssClass="btn btn-github" ValidationGroup="C" Text="Set Lock" OnClick="btnLock_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

