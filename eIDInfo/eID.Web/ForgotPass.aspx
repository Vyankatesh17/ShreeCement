<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ForgotPass.aspx.cs" Inherits="ForgotPass" %>

<!DOCTYPE html>
<html class="bg-logo">
<head>
    <meta charset="UTF-8">
    <title>HR Portal | Forgot Password</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- bootstrap 3.0.2 -->    
    <link href="admin_theme/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
   
    <link href="admin_theme/dist/css/style.css" rel="stylesheet" type="text/css" />
    <style>
        .btnlft {
            float: right;
            margin-top: -44px;
        }
        .bg-logo{
            background-color:#273972 !important;
        }
    </style>
</head>
<body class="bg-logo">
    <form id="form1" runat="server">
        <div>

            <div class="form-box" id="login-box">
                <div class="header bg-gray text-navy">Account Information</div>

                <div class="body bg-gray">



                    <div class="form-group">
                        E-mail Address :<br />
                        <asp:TextBox ID="txtemail" CssClass="form-control" placeholder="E-mail Address" runat="server" Width="270px" MaxLength="60"></asp:TextBox>
                    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtemail"
                            ErrorMessage="Please Enter your email *" Display="Dynamic" ValidationGroup="SS" ForeColor="Red"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail"
                            Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="S" SetFocusOnError="True" Font-Size="10pt"></asp:RegularExpressionValidator>

                    </div>

                    <%--   <div class="form-group">
                        <input type="checkbox" name="remember_me" />
                        Remember me
                   
                    </div>--%>
                </div>
                <div class="footer">


                    <asp:Button ID="btnsubmit" CssClass="btn bg-orange btn-block" runat="server" Text="Submit" ValidationGroup="SS"
                        OnClick="btnsubmit_Click" />

                    <asp:Button ID="Button1" CssClass="btn bg-orange btn-block" runat="server" Text="Log In" OnClick="Button1_Click" />

                    <%--<p><a href="#">I forgot my password</a></p>--%>
                </div>


                <%--  <div class="margin text-center">
                    <span>Excellence IT Solution Pune</span>
                    <br />


                </div>--%>
            </div>

        </div>
    </form>
    <!-- jQuery 2.0.2 -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="../../js/bootstrap.min.js" type="text/javascript"></script>

</body>
</html>
