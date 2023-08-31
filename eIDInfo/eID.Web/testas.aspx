﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testas.aspx.cs" Inherits="testas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>  
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"  
        rel="stylesheet" type="text/css" />  
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>  
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"  
        rel="stylesheet" type="text/css" />  
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"  
        type="text/javascript"></script>  
</head>
<body>
     <form id="form1" runat="server">  
    <script type="text/javascript">  
        $(function () {  
            $('[id*=lstEmployee]').multiselect({  
                includeSelectAllOption: true  
            });  
        });  
    </script>  
   Employee : <asp:ListBox ID="lstEmployee" runat="server" SelectionMode="Multiple">  
        <asp:ListItem Text="Nikunj Satasiya" Value="1" />  
        <asp:ListItem Text="Dev Karathiya" Value="2" />  
        <asp:ListItem Text="Hiren Dobariya" Value="3" />  
        <asp:ListItem Text="Vivek Ghadiya" Value="4" />  
        <asp:ListItem Text="Pratik Pansuriya" Value="5" />  
    </asp:ListBox>  
    <asp:Button Text="Submit" runat="server" OnClick="Submit" />  
    </form>  
</body>
</html>
