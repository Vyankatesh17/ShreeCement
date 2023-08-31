<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_newuser.aspx.cs" Inherits="Add_newuser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html class="bg-black">
<head>
    <meta charset="UTF-8">
    <title>HR PORTAL | Log in</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- bootstrap 3.0.2 -->    
    <link href="admin_theme/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
   
    <link href="admin_theme/dist/css/style.css" rel="stylesheet" type="text/css" />

    <style>
        .btnlft {
            float: right;
            margin-top: -44px;
        }

        .auto-style1 {
            height: 51px;
        }
    </style>
</head>
<body>
    <form id="form1" autocomplete="off" runat="server">
     

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="form-box" id="login-box">
            <div class="header bg-navy">Employee Registration Information</div>

            <div class="body bg-gray">
               
                <table style="color: #444;">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group" style="font-size: 12px">Activation<br />Code&nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label> </div>
                        </td>

                         <td></td>
                        <td>
                            <div class="form-group">
                                <asp:TextBox runat="server" Width="230px" CssClass="form-control" ID="txtempcode"  MaxLength="20"/>
                            </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtempcode" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="l" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="form-group" style="font-size: 12px">
                                User<br />Name&nbsp;&nbsp;
                            <asp:Label ID="lbluser" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </div>
                        </td>
                         <td></td>
                        <td>
                            <div class="form-group">
                                <asp:TextBox ID="txtusername1" Width="230px" AutoCompleteType="None" runat="server" CausesValidation="True" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtusername1"
                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                    ValidationGroup="l" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group" style="font-size: 11px">
                                Password
                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </div>
                        </td>  <td></td>
                        <td>
                            <div class="form-group">
                                <asp:TextBox ID="txtPassword1" Width="230px" runat="server" MaxLength="20" TextMode="Password"
                                    CausesValidation="True" CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="txtPassword1" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="l" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group" style="font-size: 11px">
                                Security<br />Question
                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </div>
                        </td>  <td></td>
                        <td>
                            <div class="form-group">
                                <asp:DropDownList Width="230px" ID="ddlsecurity" runat="server"
                                   
                                    CausesValidation="True" CssClass="form-control">

                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                    <asp:ListItem>What is Your Favourite Teacher Name ?</asp:ListItem>
                                    <asp:ListItem>What is your Nick Name ?</asp:ListItem>
                                    <asp:ListItem>What is your Favourite Subject ?</asp:ListItem>

                                </asp:DropDownList>
                            </div>

                        </td>
                        <td>
                            <div class="form-group">
                                <asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="ddlsecurity" Display="Dynamic" ErrorMessage="*" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="l" ValueToCompare="--Select--" ForeColor="Red"></asp:CompareValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group" style="font-size: 12px">
                                Security<br />Answer
                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
                            </div>
                        </td>  <td></td>
                        <td>
                            <div class="form-group">
                                <asp:TextBox ID="txtSecurity" Width="230px" runat="server" CausesValidation="True" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>


                        </td>
                        <td>
                            <div class="form-group">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="txtSecurity" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="l" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr id="tt1" runat="server" visible="false">
                        <td>
                            <div class="form-group" style="font-size: 12px">Status: </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:RadioButtonList ID="rd_status" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Active" Value="0" Selected="True">
                                  
                                    </asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="1">
                                  
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="footer bg-gray">
                <asp:Button ID="btnsubmit" CssClass="btn bg-orange btn-block" runat="server" Text="Submit" ValidationGroup="l"
                    OnClick="btnsubmit_Click" />

                <asp:Button ID="Button1" CssClass="btn bg-orange btn-block" runat="server" Text="Log In" OnClick="Button1_Click" />


            </div>
        </div>
    </form>
    <!-- jQuery 2.0.2 -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="../../js/bootstrap.min.js" type="text/javascript"></script>

</body>
</html>
