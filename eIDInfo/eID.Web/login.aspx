<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link href="admin_theme/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: #ebedec;
        }

        .login {
            background: #75797b !important;
            border-bottom: 7px solid #ee302e;
            height: 450px;
            margin-top: 150px;
            border-top: 7px solid #ee302e;
        }

        .logo img {
            width: 338px;
            margin: auto;
            margin-top: -98px;
        }

        .employee img {
            width: 200px;
            margin-top: -64px;
        }

        .login-btn {
            background: #ee302e;
            color: #fff;
            border-radius: 0px !important;
            border: none;
        }

        .login-img img {
            margin-top: -70px;
            border-radius: 10px;
            box-shadow: 1px 3px 11px 0px rgba(0,0,0,0.75);
            -webkit-box-shadow: 1px 3px 11px 0px rgba(0,0,0,0.75);
            -moz-box-shadow: 1px 3px 11px 0px rgba(0,0,0,0.75);
        }

        .form-control {
            border-radius: 0px !important;
            height: 53px;
        }

        .login-form {
            margin-top: 140px;
            width: 390px;
            margin-left: 160px;
        }

        @media only screen and (max-width: 480px) {

            .login {
                background: #75797b !important;
                border-bottom: 7px solid #ee302e;
                height: 650px;
                margin-top: 150px;
                border-top: 7px solid #ee302e;
            }

            .login-img img {
                margin-top: 0px;
            }

            .login-form {
                margin-top: 45px;
                width: 100%;
                margin-left: 0px;
            }
        }

        @media only screen and (max-width: 420px) {

            .login {
                background: #75797b !important;
                border-bottom: 7px solid #ee302e;
                height: 650px;
                margin-top: 150px;
                border-top: 7px solid #ee302e;
            }

            .login-img img {
                margin-top: 0px;
            }

            .login-form {
                margin-top: 45px;
                width: 100%;
                margin-left: 0px;
            }
        }

        @media only screen and (max-width: 375px) {

            .login {
                background: #75797b !important;
                border-bottom: 7px solid #ee302e;
                height: 650px;
                margin-top: 150px;
                border-top: 7px solid #ee302e;
            }

            .login-img img {
                margin-top: 0px;
            }

            .login-form {
                margin-top: 45px;
                width: 100%;
                margin-left: 0px;
            }
        }

        @media only screen and (max-width: 320px) {

            .login {
                background: #75797b !important;
                border-bottom: 7px solid #ee302e;
                height: 650px;
                margin-top: 150px;
                border-top: 7px solid #ee302e;
            }

            .login-img img {
                margin-top: 0px;
            }

            .login-form {
                margin-top: 45px;
                width: 100%;
                margin-left: 0px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="login">
            <div class="container">
                <div class="row">
                    <div class="col-lg-8">
                    </div>
                    <div class="col-lg-4">
                        <div class="logo">
                            <img src="images/logo.jpeg" class="img-responsive">
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="login-img">
                            <img src="images/login.png" class="img-responsive">
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="login-form">
                            <div class="form-group">
                                <asp:TextBox ID="txtusername" AutoCompleteType="None" CssClass="form-control" placeholder="User Name" MaxLength="40" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtpassword" AutoCompleteType="None" CssClass="form-control" placeholder="Password" runat="server" MaxLength="20" TextMode="Password" />
                            </div>

                            <asp:Button ID="btnlogin" runat="server" CssClass="btn btn-default login-btn" Text="Sign In" OnClick="btnlogin_Click" />
                            <a href="company_register.aspx" class="btn btn-default login-btn pull-right">Join us</a>

                        </div>
                        <br />
                        <br />
                        <br />
                        <span style="color: #f6f6f6" class="pull-right"><strong>Copyright &copy;<a href="#" style="color: #f6f6f6">Vania Solutions Pvt. Ltd</a>.</strong> All rights reserved.</span>
                    </div>
                </div>
                
            </div>
        </section>
    </form>
</body>
</html>
