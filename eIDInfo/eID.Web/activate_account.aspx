<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activate_account.aspx.cs" Inherits="activate_account" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>Activate Account</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />
    <!-- bootstrap 3.0.2 -->
    <link href="admin_theme/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="admin_theme/dist/css/AdminLTE.min.css" rel="stylesheet" />

    <style>
        body {
            background-color: #263973 !important;
        }

        .modal-content {
            position: relative;
            background-color: #ffffff;
            border: 1px solid #999999;
            border: 1px solid rgba(0, 0, 0, 0.2);
            border-radius: 0;
            outline: none;
            -webkit-box-shadow: 0 3px 9px rgba(0, 0, 0, 0.5);
            box-shadow: 0 3px 9px rgba(0, 0, 0, 0.5);
            background-clip: padding-box;
        }

        .modal-header {
            border-bottom: 1px solid #eb7f25;
            background: #eb7f25;
            min-height: 16.4286px;
            padding: 10px 15px;
        }


        .modal-body {
            position: relative;
            padding: 20px;
        }

        .modal-footer {
            padding: 19px 20px 20px;
            margin-top: 0;
            text-align: right;
            border-top: 1px solid #e5e5e5;
        }
    </style>
</head>
<body class="hold-transition register-page">
    <form id="form1" runat="server">
        <%--<div class="container">
            <div class="row">
                <div class="modal-content">

                    <div class="modal-header">
                        
                        <h4 class="modal-title"><span style="color:#f6f6f6">Activate your account</span></h4>
                    </div>

                    <div action="" method="post">
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="userEmail">Email Address</label>
                                <input type="text" class="form-control" required="" name="userEmail" value="">
                                <span class="help-block">Your email address is also used to log in.</span>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="password">Password</label>
                                        <input type="password" class="form-control" required="" name="password" value="">
                                        <span class="help-block">Choose a password for your new account.</span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="passwordr">Repeat Password</label>
                                        <input type="password" class="form-control" required="" name="passwordr" value="">
                                        <span class="help-block">Type the password again. Passwords must match.</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <input type="hidden" name="isEmpty" value="">
                            <button type="input" name="submit" value="newAccount" class="btn btn-success btn-icon"><i class="fa fa-check"></i>Create My Account</button>
                            <button type="button" class="btn btn-default btn-icon" data-dismiss="modal"><i class="fa fa-times-circle"></i>Cancel</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>--%>
        <div class="register-box">


            <div class="register-box-body">
                <p class="login-box-msg">Activate you account</p>


                <div class="form-group has-feedback">
                    <asp:TextBox type="text" class="form-control" placeholder="Company name" id="txtCompanyName" runat="server"></asp:TextBox>
                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox type="email" class="form-control" placeholder="Email" id="txtEmail" runat="server"></asp:TextBox>
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox type="password" class="form-control" placeholder="Old Password" id="txtOldPassword" runat="server"></asp:TextBox>
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtPassword" runat="server" type="password" class="form-control" placeholder="New Password"></asp:TextBox>
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtConfirmPass" runat="server" type="password" class="form-control" placeholder="Confirm password"></asp:TextBox>
                    <span class="glyphicon glyphicon-log-in form-control-feedback"></span>
                </div>
                <div class="row">
                    <!-- /.col -->
                     <div class="col-xs-4"></div>
                    <div class="col-xs-4">
                        <asp:Button ID="btnActivate" runat="server" ValidationGroup="A" OnClick="btnActivate_Click" type="submit" class="btn btn-google btn-block btn-flat" Text="Activate" />
                    </div>
                    <!-- /.col -->
                </div>
                 <div class="row"><hr /></div>

                

                <center><a href="login.aspx" class="text-center">I already have a membership</a></center>
            </div>
            <!-- /.form-box -->
        </div>
    </form>
</body>
</html>
