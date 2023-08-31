<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login_30_09_21.aspx.cs" Inherits="login_30_09_21" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <title>Log in</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- bootstrap 3.0.2 -->
    <link href="admin_theme/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <style>
        html {
        }

        body {
            background: url(images/banner.jpg) no-repeat center center fixed;
            background-size: cover;
            height: 100%;
            overflow: hidden;
        }

        .panel-default {
            opacity: 0.9;
            margin-top: 30px;
        }

        .form-group.last {
            margin-bottom: 0px;
        }
    </style>

    <style>
        .col-md-offset-8 {
            margin-left: 71.666667%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <br />
                <br />
            </div>
            <div class="row">
                <br />
                <br />
            </div>
            <div class="row">
                <br />
                <br />
            </div>
            <div class="row">
                <br />
                <br />
            </div>

            <div class="row">
                <div class="col-md-4 col-sm-6 col-sm-offset-8">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <span class="glyphicon glyphicon-lock"></span>&nbsp;Login
                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal" role="form">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">
                                        Email</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtusername" AutoCompleteType="None" CssClass="form-control" placeholder="User Name" MaxLength="40" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputPassword3" class="col-sm-3 control-label">
                                        Password</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtpassword" AutoCompleteType="None" CssClass="form-control" placeholder="Password" runat="server" MaxLength="20" TextMode="Password" />
                                    </div>
                                </div>
                                <div class="form-group last">
                                    <div class="col-sm-offset-4 col-sm-9">
                                        <asp:Button ID="btnlogin" runat="server" CssClass="btn btn-primary btn-sm" Text="Sign In" OnClick="btnlogin_Click" />
                                        &nbsp;&nbsp;&nbsp;  <a href="company_register.aspx" class="btn btn-info btn-sm">Join us</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer text-center">
                            <strong>Copyright &copy; 2021 <a href="#">eIDinfo</a>.</strong> All rights reserved.
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- jQuery 2.0.2 -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="admin_theme/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</body>
</html>
