<%@ Page Language="C#" AutoEventWireup="true" CodeFile="capture_employee_photo.aspx.cs" Inherits="capture_employee_photo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="admin_theme/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="admin_theme/dist/css/AdminLTE.css" rel="stylesheet" />
   <%-- <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <%--//<script src="admin_theme/scripts/webcam.js"></script>--%>
     <script src="admin_theme/webcamjs/webcam.js"></script>

   <script src="admin_theme/webcamjs/webcam.min.js"></script>
    
     

   

   
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-6 col-xs-6">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Live Camera</h3>
                    </div>
                    <div class="box-body no-padding">
                        <div id="LiveCamera"></div>
                    </div>
                    <div class="box-footer">                        
                        <input type="button" id="btnCapture" value="Capture" class="btn btn-primary" />
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-xs-6">
                <div class="box box-success">
                    <div class="box-header with-border">
                        <h3 class="box-title">Captured Picture</h3>
                    </div>
                    <div class="box-body no-padding">
                        <input type="image" id="imgCapture" runat="server" />
                    </div>
                    <div class="box-footer">
                        <input type="button" id="btnUpload" value="Upload" disabled="disabled" class="btn btn-success" />
                    </div>
                </div>
            </div>

          <asp:HiddenField ID="hfCompany" runat="server" />
             <asp:HiddenField ID="hfemployeecode" runat="server" />
        </div>

        <script src="http://code.jquery.com/jquery-3.4.1.min.js"></script>
<%--<script src="http://code.jquery.com/jquery-plugins.js"></script>--%>
         <script type="text/javascript">
             Webcam.set({
                 width: 250,
                 height: 185,
                 image_format: 'jpeg',
                 jpeg_quality: 100
             });
             Webcam.attach('#LiveCamera');
         </script>

         <script type="text/javascript">
             //$(function () {
                 
                 //Webcam.set({
                 //    width: 250,
                 //    height: 185,
                 //    image_format: 'jpeg',
                 //    jpeg_quality: 90
                 //});
                 //Webcam.attach('#LiveCamera');


             $(document).ready(function () {

                 $("#btnCapture").click(function () {

                     Webcam.snap(function (data_uri) {

                         //$("#imgCapture").attr('src', data_uri);
                         $("#imgCapture")[0].src = data_uri;
                         $("#btnUpload").removeAttr("disabled");
                     });
                 });

                 $("#btnUpload").click(function () {
                     var hidderValue = document.getElementById('hfCompany').value;
                     var EmphidderValue = document.getElementById('hfemployeecode').value;
                     console.log(hidderValue);

                     $.ajax({
                         type: "POST",
                         url: "capture_employee_photo.aspx/SaveCapturedImage",
                         //data: "{data: '" + $("#imgCapture")[0].src + "'}",
                         data: "{data: '" + $("#imgCapture")[0].src + "', Company:'" + hidderValue + "', EmployeeCode:'" + EmphidderValue + "'}",
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function (r) { alert('Captured image uploaded successfully..'); window.parent.$('#onload_iframeModal').modal('hide'); }
                     });


                 });

             });
               


            //});
         </script>




        <script type="text/javascript">
            //$(function () {
                $('#btnPopModal').on('click', function (e) {
                    //  here's the fun part, pay attention!
                    console.log('hide triggered');
                    window.parent.$('#onload_iframeModal').modal('hide');
                });
            //});
        </script>


    </form>
</body>






</html>
