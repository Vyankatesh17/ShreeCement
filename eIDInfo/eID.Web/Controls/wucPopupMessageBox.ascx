<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucPopupMessageBox.ascx.cs"
    Inherits="wucPopupMessageBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:LinkButton ID="LinkFake1" runat="server"></asp:LinkButton>
<asp:Panel ID="PanelMsg" runat="server"  BorderColor="#006699" BorderStyle="Solid"
    BackColor="White" BorderWidth="1px">
    <div style="text-align: left; background-color: #BDD3E1; padding-left: 05px; color: #333333;">
        <span>Message: </span>
    </div>
    <div style="background-image: url('../Images/aero bg.png'); text-align: center;">
        <table width="100%" cellpadding="4">
            <tr>
                <td style="width: 60px" valign="middle" align="center">
                    <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Images/images.jpg" Width="30px" />
                </td>
                <td valign="middle" align="left">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="#333333" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr align="center">
              <td>
                  
                </td>
                <td  align="center">
                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" EnableTheming="false"
                        Width="75px" BackColor="#95B6CE" Font-Bold="True" ForeColor="White" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<asp:ModalPopupExtender ID="PopupMsg" runat="server" DropShadow="false" PopupControlID="PanelMsg"
    TargetControlID="LinkFake1" BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>
