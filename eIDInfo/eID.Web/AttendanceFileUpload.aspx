<%@ Page Title="Upload Attendance" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="AttendanceFileUpload.aspx.cs" Inherits="AttendanceFileUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function changePropertyType(ddlIndex) {
            var selectedIndex = ddlIndex.selectedIndex;
            var newSrc = ddlIndex.options[ddlIndex.selectedIndex].value;
            if (selectedIndex == 1) {
                document.getElementById("divProject").style.display = "block";
            }
            else {
                document.getElementById("divProject").style.display = "none";
            }
        }

        function validate() {
            var flag = false;
            var PropertyType = ContentPlaceHolder1_ddlProertyType.options[ContentPlaceHolder1_ddlProertyType.selectedIndex].value;
            var Project = ContentPlaceHolder1_ddlProject.options[ContentPlaceHolder1_ddlProject.selectedIndex].value;
            var Propertyindex = ContentPlaceHolder1_ddlProertyType.selectedIndex;
            var Projectindex = ContentPlaceHolder1_ddlProject.selectedIndex;

            if (parseInt(Propertyindex) <= 0) {
                //alert("Enter Country Name");
                flag = false;
            }
            else if (parseInt(Propertyindex) == 0) {
                if (parseInt(Projectindex) <= 0) { flag = false; } else {
                    flag = true;
                }
            }
            else {
                flag = true;
            }
            if (flag == false) {
                alert("Please fill required fields.");
            }
            return flag;
        }

        function CheckFile(file) {
            var isValidFile = CheckExtension(file);
            return isValidFile;
        }
        var validFilesTypes = ["xls", "xlsx"];

        function CheckExtension(file) {
            /*global document: false */
            var filePath = file.value;
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
            var isValidFile = false;

            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }

            if (!isValidFile) {
                file.value = null;
                alert("Invalid File. Valid extensions are:\n\n" + validFilesTypes.join(", "));
            }

            return isValidFile;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                    
                        <li class="active"><a href="#tab_10" data-toggle="tab">Upload</a></li>
                     
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane " id="tab_9">
                       
                        </div>
                        <!-- /.tab-pane -->
                        <div class="tab-pane active" id="tab_10">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="box box-primary">
                                        <div class="box-header">
                                            <i class="fa fa-upload"></i>
                                            <h3 class="box-title">Upload Attendance File</h3>
                                        </div>
                                        <div class="box-body">
                                    
                                                <div class="row">
                                                    <div class="form-group col-lg-5">
                                                        <label for="exampleInputFile">Select File</label>
                                                        <asp:FileUpload ID="fuEnquiry" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn bg-blue-active" OnClientClick="return validate();" OnClick="btnUpload_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn bg-blue-active" OnClick="btnCancel_Click" />
                                                </div>
                                           
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.tab-pane -->

                        <div class="tab-pane" id="tab_11">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="box box-primary">
                                        <div class="box-header">
                                            <i class="fa fa-upload"></i>
                                            <h3 class="box-title">Enquiry Upload</h3>
                                        </div>
                                        <div class="box-body">
                                            <form role="form">

                                                <div class="row">
                                                    <div class="form-group col-lg-5">
                                                        <label for="exampleInputFile">Select File</label>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <asp:Button ID="Button1" runat="server" Text="Upload" CssClass="btn bg-blue-active" />
                                                    <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn bg-blue-active" />
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.tab-content -->
                </div>

</asp:Content>

