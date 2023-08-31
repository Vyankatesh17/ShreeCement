<%@ Page Title="Import Data" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ImportData.aspx.cs" Inherits="ImportData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>

            <div class="col-md-6">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">

                            <table width="100%">

                                <tr>
                                    <td>Import : DashBoard <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                    <td class="style7">
                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3"></td>
                                    <td class="style5">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp 

  <asp:Button ID="btnemport" runat="server" Text="ImportData"
      OnClick="btnemport_Click" ValidationGroup="a" CssClass="btn bg-blue-active" />
                                        <asp:Button ID="cancel" runat="server" Text="Cancel"
                                            OnClick="cancel_Click" CssClass="btn bg-blue-active" />

                                    </td>

                                </tr>
                            </table>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div>
                    <div>
                        <div class="form-group">
                        </div>
                    </div>
                </div>
            </div>

            <uc1:Time ID="modpop" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

