<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configure_smtp.aspx.cs" Inherits="configure_smtp" Title="Configure SMTP" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <title>Configure SMTP</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- bootstrap 3.0.2 -->
    <link href="admin_theme/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <style>
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
        .text-red {
            color: #ff0000;
        }
    </style>
</head>
<body>
    <form id="form1" autocomplete="off" runat="server">
        <div class="container">
            <div class="row">
                <br>
                <br>
            </div>
            <div class="row">
                <br>
                <br>
            </div>
            <div class="row">
                <br>
                <br>
            </div>
            
            
            <div class="row">
                <div class="col-md-6 col-sm-6 col-sm-offset-7">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                          <h3 class="panel-title">Configure </h3>  
                            <div class="panel-title pull-right" style="margin-top:-20px;"><a href="Login.aspx" class="text-primary"><span class="text-primary"> click here for login</span></a></div>
                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal" role="form">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">
                                        SMTP Address <span class="text-red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtSmtpAddress" AutoCompleteType="None" CssClass="form-control" placeholder="SMTP Address" MaxLength="100" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtSmtpAddress" ErrorMessage="This field is required" ID="RequiredFieldValidator1" runat="server"  CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">
                                        Port Number <span class="text-red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPortNo" AutoCompleteType="None" CssClass="form-control" placeholder="Port Number" MaxLength="5" TextMode="Number" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtPortNo" ErrorMessage="This field is required" ID="RequiredFieldValidator3" runat="server"  CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">
                                        Email <span class="text-red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtEmail" AutoCompleteType="None" CssClass="form-control" placeholder="Email" MaxLength="100" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="This field is required" ID="RequiredFieldValidator2" runat="server"  CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">
                                        Password <span class="text-red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPassword" AutoCompleteType="None" CssClass="form-control" placeholder="Password" TextMode="Password" MaxLength="100" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtPassword" ErrorMessage="This field is required" ID="RequiredFieldValidator4" runat="server"  CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="col-sm-4 control-label">
                                        SMTP Password <span class="text-red">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtSMTPPassword" AutoCompleteType="None" CssClass="form-control" placeholder="Password" TextMode="Password" MaxLength="100" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtSMTPPassword" ErrorMessage="This field is required" ID="RequiredFieldValidator5" runat="server"  CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group last">
                                    <div class="col-sm-offset-6 col-sm-9">
                                        <asp:Button ID="btnConfigure" runat="server" CssClass="btn btn-primary btn-sm pull" ValidationGroup="A" Text="Configure" OnClick="btnConfigure_Click" />
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
