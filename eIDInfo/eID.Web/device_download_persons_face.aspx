<%@ Page Title="Download Face" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="device_download_persons_face.aspx.cs" Inherits="device_download_persons_face" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Download Face of </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-5">
                            <label class="control-label col-sm-3">Company<span class="text-danger">*</span></label>
                            <div class="col-md-8"><asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                        </div></div>
                        <div class="col-lg-5">
                            <div class="form-group">
                                <label class="control-label col-sm-4">Select Device<span class="text-danger">*</span></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control"></asp:DropDownList>


                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ControlToValidate="ddlDevice" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlDevice" InitialValue="0" ID="RequiredFieldValidator4" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="btnDownloadFace" runat="server" CssClass="btn btn-primary" Text="Download Face" ValidationGroup="A" OnClick="btnDownloadFace_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnTest" runat="server" Text="Test APi" CssClass="btn btn-primary" OnClick="btnTest_Click" Visible="false" />
   <div class="hidden"> <asp:Literal ID="litMessage" runat="server" ></asp:Literal></div>
</asp:Content>

