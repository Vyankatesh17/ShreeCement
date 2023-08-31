<%@ Page Title="Employee" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="EmployeeOld.aspx.cs" Inherits="EmployeeOld" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
   
    <script type="text/javascript">
        function jqFunctions() {

             var table = $('#ContentPlaceHolder1_grd_Emp').DataTable({
                lengthChange: false,
                buttons: ['pageLength','copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Emp_wrapper .col-sm-6:eq(0)');
        }
        
        $(document).ready(function () {
            jqFunctions();
        });
    </script>
    <style type="text/css">
        .form input[type="file"] {
            z-index: 999;
            line-height: 0;
            font-size: 50px;
            position: absolute;
            opacity: 0;
            filter: alpha(opacity = 0);
            -ms-filter: "alpha(opacity=0)";
            cursor: pointer;
            _cursor: hand;
            margin: 0;
            padding: 0;
            left: 0;
        }

        .add-photo-btn {
            position: absolute;
            overflow: hidden;
            cursor: pointer;
            text-align: center;
            background-color: #8bc804;
            color: #fff;
            display: block;
            width: 160px;
            height: 31px;
            font-size: 18px;
            line-height: 35px;
            float: left;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
        }

        input[type="text"] {
            float: left;
        }
    </style>
    <style type="text/css">
        .fancy-green .ajax__tab_header {
            background: url(Images/blue_bg.gif) repeat-x;
            cursor: pointer;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer {
            background: url(Images/blue_left.gif) no-repeat left top;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner {
            background: url(Images/blue_right.gif) no-repeat right top;
        }

        .fancy .ajax__tab_header {
            font-size: 13px;
            font-weight: bold;
            color: red;
            font-family: sans-serif;
        }

            .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer {
                height: 46px;
            }

            .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner {
                height: 46px;
                margin-left: 16px; /* offset the width of the left image */
            }

            .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab {
                margin: 16px 16px 0px 0px;
            }

        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab {
            color: #fff;
        }

        .fancy .ajax__tab_body {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }

        .style1 {
            width: 606px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="update22" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
            <asp:AsyncPostBackTrigger ControlID="ddlState" />
            <asp:AsyncPostBackTrigger ControlID="ddlBloodGroup" />
            <asp:AsyncPostBackTrigger ControlID="ddldept" />
            <asp:AsyncPostBackTrigger ControlID="dddesg" />
            <asp:AsyncPostBackTrigger ControlID="ddlsalarytype" />
            <asp:AsyncPostBackTrigger ControlID="ddlCity" />
            <asp:AsyncPostBackTrigger ControlID="ddlCountryP" />
            <asp:AsyncPostBackTrigger ControlID="ddlStateP" />
            <asp:AsyncPostBackTrigger ControlID="ddlCityp" />
            <asp:PostBackTrigger ControlID="btnaddexp" />
            <asp:PostBackTrigger ControlID="btnaddEdu" />
        </Triggers>
        <ContentTemplate>

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">



                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <table id="t1" style="border: none;" runat="server" width="95%">
                                        <tr>
                                            <td></td>

                                            <td>Select Company:
                                                <br />
                                                <asp:DropDownList runat="server" ID="ddlCompanyList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyList_SelectedIndexChanged"></asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                                    ControlToValidate="ddlCompanyList" Display="Dynamic"
                                                    ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="S" ForeColor="Red" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>Department Name:
                                <br />
                                                <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                            <td>Employee Name:
                                <br />
                                                <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlEmployeeList"></asp:DropDownList>
                                            </td>
                                            <td>Employee Code
                                <br />
                                                <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control" placeholder="Employee Code"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>

                                                <div style="margin-top: 15px; margin-left: 10px;">
                                                    <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" OnClick="btnsearch_Click"
                                                        Text="Search" />
                                                </div>
                                            </td>
                                            <td>
                                                <div style="margin-top: 15px;">
                                                    <asp:Button ID="btnReset" runat="server" CssClass="btn bg-blue-active" Text="Reset" OnClick="btnReset_Click" />
                                                </div>
                                            </td>
                                            <td>
                                                <div style="margin-top: 15px;">
                                                    <asp:Button ID="btnadd" runat="server" CssClass="btn bg-blue-active" Text="Add New" OnClick="btnadd_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grd_Emp" runat="server" AutoGenerateColumns="False" 
                                            CssClass="table table-bordered table-striped table-responsive"
                                           PageSize="10"
                                            OnRowDataBound="grd_Emp_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtdetails" ReadOnly="true" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtempno" ReadOnly="true" runat="server" Text='<%# Eval("EmployeeNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee ID">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblempid" ReadOnly="true" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemailid" ReadOnly="true" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Personal Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="personalEmail" ReadOnly="true" runat="server" Text='<%# Eval("personalEmail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcntct" ReadOnly="true" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Of Joining">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDOJ" ReadOnly="true" runat="server" Text='<%# Eval("DOJ1","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldept" ReadOnly="true" runat="server" Text='<%# Eval("DeptName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PAN Number" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpan" ReadOnly="true" runat="server" Text='<%# Eval("PanNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RelivingDate" HeaderText="Relive Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:TemplateField HeaderText="Passport Number" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPassport" ReadOnly="true" runat="server" Text='<%# Eval("PassportNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/Images/i_edit.png" CommandArgument='<%# Eval("Employeeid") %>'
                                                            OnClick="OnClick_Edit" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel runat="server" ID="up1">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="LnkReliveDate" runat="server" CommandArgument='<%# Eval("Employeeid") %>'
                                                                    Text="Reliving" OnClick="LnkReliveDate_Click" />
                                                                <%-- <asp:LinkButton ID="LnkRejoin" runat="server" CommandArgument='<%# Eval("Employeeid") %>'
                                                                                            Text="Relieved" OnClick="LnkRejoin_Click" />--%>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="Edit" />
                                                            </Triggers>

                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Profile">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel runat="server" ID="up2">
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="imgbtnViewProfile" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnViewProfile" runat="server" CommandArgument='<%#Eval("Employeeid")%>'
                                                                    ImageUrl="~/Images/View.png" BorderStyle="None" Height="30px" ToolTip="View Profile"
                                                                    Width="30px" OnClick="imgbtnViewProfile_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="box">
                                <div class="box-header">
                                    <h3 class="box-title"></h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnconP2" ValidationGroup="a" runat="server" CssClass="btn bg-blue-active"
                                            Text="Save"  OnClick="btnconP1_Click" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">

                                        <div class="col-md-6">
                                            <div class="box box-primary">
                                                <fieldset>
                                                    <legend>Personal Information</legend>
                                                </fieldset>
                                                <div class="box-body">
                                                    <div class="form-group">

                                                        <table width="100%">
                                                            <tr>
                                                                <td>Employee Code</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Device Code</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBioId" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Name:<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <div style="float: left; width: 80px">
                                                                            <asp:DropDownList ID="ddlsalitude" runat="server" Width="70px" CssClass="form-control">
                                                                                <asp:ListItem Selected="True">Mr</asp:ListItem>
                                                                                <asp:ListItem>Ms</asp:ListItem>
                                                                                <asp:ListItem>Mrs</asp:ListItem>
                                                                                <asp:ListItem>Dr.</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div style="float: left; width: 85px">
                                                                            <asp:TextBox ID="txtfname" runat="server" CssClass="form-control" Width="80px" MaxLength="40"></asp:TextBox>
                                                                        </div>
                                                                        <div style="float: left; width: 85px">
                                                                            <asp:TextBox ID="txtmname" runat="server" Width="80px" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                        </div>

                                                                        <div style="float: left; width: 85px">
                                                                            <asp:TextBox ID="txtlname" runat="server" CssClass="form-control" Width="80px" MaxLength="40"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />

                                                                    <asp:FilteredTextBoxExtender ID="txtfname_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtfname" ValidChars="ab cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtfname"
                                                                        Display="None" ErrorMessage="Enter First Name" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="req1_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                        TargetControlID="req1">
                                                                    </asp:ValidatorCalloutExtender>
                                                                    <asp:TextBoxWatermarkExtender ID="txtfname_TextBoxWatermarkExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtfname" WatermarkText="First Name">
                                                                    </asp:TextBoxWatermarkExtender>

                                                                    <asp:FilteredTextBoxExtender ID="txtmname_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtmname" ValidChars="ab cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:TextBoxWatermarkExtender ID="txtmname_TextBoxWatermarkExtender" WatermarkText="Middle Name"
                                                                        runat="server" Enabled="True" TargetControlID="txtmname">
                                                                    </asp:TextBoxWatermarkExtender>

                                                                    <asp:FilteredTextBoxExtender ID="txtlname_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtlname" ValidChars="ab cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlname"
                                                                        Display="None" ErrorMessage="Enter First Name" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    <asp:TextBoxWatermarkExtender ID="txtlname_TextBoxWatermarkExtender" WatermarkText="Last Name"
                                                                        runat="server" Enabled="True" TargetControlID="txtlname">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:Label ID="lblempid" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Date Of Birth:
                                                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtbirtdate" runat="server" CssClass="form-control"></asp:TextBox>

                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="txtbirtdate"
                                                                        Display="None" ErrorMessage="Enter Date of Birth" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="req3_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                        TargetControlID="req3">
                                                                    </asp:ValidatorCalloutExtender>
                                                                    <asp:CalendarExtender ID="txtbirtdate_CalendarExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtbirtdate">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <tr>
                                                                    <td>Gender:&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="RbGender" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Selected="True" Value="0">Male</asp:ListItem>
                                                                            <asp:ListItem Value="1">Female</asp:ListItem>
                                                                        </asp:RadioButtonList>

                                                                    </td>
                                                                </tr>
                                                            <tr>
                                                                <td>Marital Status:<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>

                                                                    <asp:RadioButtonList ID="rbMaritalStatus" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Selected="True" Value="Single">Single</asp:ListItem>
                                                                        <asp:ListItem Value="Married">Married</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Blood Group:&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlBloodGroup" runat="server" AutoPostBack="false" CssClass="form-control">
                                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                                        <asp:ListItem>A+</asp:ListItem>
                                                                        <asp:ListItem>A-</asp:ListItem>
                                                                        <asp:ListItem>B+</asp:ListItem>
                                                                        <asp:ListItem>B-</asp:ListItem>
                                                                        <asp:ListItem>O+</asp:ListItem>
                                                                        <asp:ListItem>O-</asp:ListItem>
                                                                        <asp:ListItem>AB+</asp:ListItem>
                                                                        <asp:ListItem>AB-</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                </td>
                                                                <%-- <asp:TextBox ID="txtbloodgroup" runat="server"></asp:TextBox>--%>

                                                            <tr>
                                                                <td>Mobile No:<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtcontactno" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <%-- <asp:FilteredTextBoxExtender ID="txtcontactno_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtcontactno" ValidChars="0123456789 ">
                                                    </asp:FilteredTextBoxExtender>--%>

                                                                    <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter Valid Mobile No."
                                                        ValidationGroup="a" ControlToValidate="txtcontactno" Display="None" ForeColor="Red"
                                                        ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender ID="RangeValidator1_ValidatorCalloutExtender" runat="server"
                                                        Enabled="True" TargetControlID="RegularExpressionValidator3">
                                                    </asp:ValidatorCalloutExtender>--%>


                                                                    <%-- <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Invalid Mobile" ControlToValidate="txtcontactno" Display="None" MaximumValue="10" MinimumValue="10" ValidationGroup="a"></asp:RangeValidator>

                                                    <asp:ValidatorCalloutExtender ID="RangeValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                                    </asp:ValidatorCalloutExtender>--%>

                                                                    <asp:RequiredFieldValidator ID="req4" runat="server" ControlToValidate="txtcontactno"
                                                                        Display="None" ErrorMessage="Enter Contact No" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="req4_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                        TargetControlID="req4">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>Alt Contact No:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtaltcontact" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:FilteredTextBoxExtender ID="txtaltcontact_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtaltcontact" ValidChars="0123456789">
                                                                    </asp:FilteredTextBoxExtender>

                                                                    <asp:RegularExpressionValidator Display="None" ControlToValidate="txtaltcontact" ID="RegularExpressionValidator4"
                                                                        ValidationExpression="^[\s\S]{6,15}$" runat="server" ValidationGroup="a"
                                                                        ErrorMessage="Minimum 6 and Maximum 15 characters required."></asp:RegularExpressionValidator>
                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                                                        TargetControlID="RegularExpressionValidator4">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>
                                                            </tr>
                                                            <tr style="visibility: hidden">
                                                                <td>Labour Card Number:<span style="color: red;"> *</span>
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtpannumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtpannumber"
                                                        Display="None" ErrorMessage="Enter Pan Number" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator5_ValidatorCalloutExtender"
                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                                    </asp:ValidatorCalloutExtender>--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Grade:&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList Style="margin-right: 7px;" ID="ddlGrade" runat="server" CssClass="form-control"
                                                                        AutoPostBack="false">
                                                                        <%-- <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>B</asp:ListItem>
                                                        <asp:ListItem>C</asp:ListItem>
                                                        <asp:ListItem>D</asp:ListItem>
                                                        <asp:ListItem>E</asp:ListItem>
                                                        <asp:ListItem>F</asp:ListItem>
                                                        <asp:ListItem>G</asp:ListItem>
                                                        <asp:ListItem>H</asp:ListItem>--%>
                                                                    </asp:DropDownList>

                                                                    <%--   <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlGrade" Display="None"
                                                                ErrorMessage="Select Department" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"
                                                                SetFocusOnError="True"></asp:CompareValidator>--%>
                                                                    <br />
                                                                    <%-- <asp:TextBox ID="txtbloodgroup" runat="server"></asp:TextBox>--%>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>Passport No:&nbsp;
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtpassportnumber" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Passport Expiry Date:
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtExpiryDate"></asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Visa No:&nbsp;
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtVisaNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Visa Expiry Date:
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtVisaExpiryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtVisaExpiryDate"></asp:CalendarExtender>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Email:<span style="color: red;"> *</span>
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                                        Display="None" ErrorMessage="Enter Email Id in proper format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                        ValidationGroup="t" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Personal Email:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtpersonalEmail" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                                    <br />
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtpersonalEmail"
                                                                        Display="None" ErrorMessage="Enter Email Id in proper format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                        ValidationGroup="t"></asp:RegularExpressionValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RegularExpressionValidator2_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RegularExpressionValidator2">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="box box-primary">
                                                <fieldset>
                                                    <legend>Contact Information</legend>
                                                </fieldset>
                                                <div class="box-body">
                                                    <div class="form-group">

                                                        <table width="90%">
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <div style="margin-left: 150px">
                                                                        <asp:Image ID="image1" runat="server" Height="100px" Width="150px" Visible="False" />
                                                                        <asp:UpdatePanel ID="up1" runat="server">
                                                                            <ContentTemplate>
                                                                            </ContentTemplate>
                                                                            <Triggers></Triggers>
                                                                        </asp:UpdatePanel>
                                                                        <br />
                                                                        <label class="add-photo-btn">
                                                                            Add Image<span><asp:FileUpload ID="FileUpload1" runat="server" accept="image/*" EnableTheming="True"
                                                                                onchange=" this.form.submit();" /></span>
                                                                        </label>
                                                                        <asp:Label ID="lblAttachPath" runat="server" Visible="False"></asp:Label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <%-- <tr>
                                                <td>
                                                    <div style="float: right; margin-right: 152px;">
                                                    </div>
                                                </td>
                                            </tr>--%>
                                                        </table>
                                                        <table width="100%">
                                                            <caption>
                                                                <br />
                                                                <br />
                                                                <tr>
                                                                    <td>Company:<span style="color: red;"> *</span> </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <br />
                                                                        <asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="ddlCompany" Display="None" ErrorMessage="Select Company" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="cmp2"></asp:ValidatorCalloutExtender>
                                                                    </td>
                                                                </tr>
                                                            </caption>
                                                            <tr>
                                                                <td>Department:<span style="color: red;"> *</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="dddept_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    &nbsp;
                                                            <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddldept" Display="None"
                                                                ErrorMessage="Select Department" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"
                                                                SetFocusOnError="True"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True"
                                                                        TargetControlID="cmp1">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Designation:

                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="dddesg" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    &nbsp;
                                                     
                                                                    <%--<div style="width: 315px; float: left; margin-top: 4px;">
                                                            <div style="float: left; padding-left: 34px;">
                                                                <%--    Grade:&nbsp; <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*">
                                                            </asp:Label>
                                                            </div>
                                                            <div style="float: right;">
                                                                <%--  <asp:DropDownList Width="145px" ID="ddlgreade" runat="server">
                                                     </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                        ControlToValidate="ddlgreade" Display="None"
                                                    ErrorMessage="Select Grade" Operator="NotEqual" ValidationGroup="a" 
                                                    ValueToCompare="--Select--" SetFocusOnError="True">
                                                    </asp:CompareValidator>

                                                    <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" 
                                                        runat="server" Enabled="True" TargetControlID="CompareValidator1"></asp:ValidatorCalloutExtender>--%>
                                                          
                                                                </td>
                                                                <%--<td>
                                                        <asp:ImageButton ID="imgadd" runat="server" ImageUrl="~/Images/i_add.png" OnClick="imgadd_Click"
                                                            Style="height: 16px" Visible="false" />
                                                    </td>--%>
                                                            </tr>
                                                            <%--         Reporting Status --%>
                                                            <tr>
                                                                <td>Reporting Head :<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="ddlReportingHead" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    &nbsp;
                                                        <asp:CompareValidator ID="cmdm" runat="server" ControlToValidate="ddlReportingHead"
                                                            Display="None" ErrorMessage="Select Reporting Head" Operator="NotEqual" ValidationGroup="a"
                                                            ValueToCompare="--Select--" SetFocusOnError="True"></asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" ControlToValidate="ddlReportingHead" Display="Dynamic" ForeColor="Red" InitialValue=" " SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                                                        TargetControlID="cmdm">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>Employee ID:&nbsp;
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtempid" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <%-- <div style="width: 330px; float: left; margin-top: 4px;">
                                                <div style="float: left; padding-left: 40px;">
                                                    Machine ID:&nbsp;</div>
                                                <div style="float: right;">
                                                    <asp:TextBox ID="txtmachinid" runat="server"></asp:TextBox>
                                                </div>
                                            </div>--%>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td>Labour Card No:&nbsp;
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtCardNo" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <%-- <div style="width: 330px; float: left; margin-top: 4px;">
                                                <div style="float: left; padding-left: 40px;">
                                                    Machine ID:&nbsp;</div>
                                                <div style="float: right;">
                                                    <asp:TextBox ID="txtmachinid" runat="server"></asp:TextBox>
                                                </div>
                                            </div>--%>
                                                                </td>
                                                            </tr>



                                                            <tr>
                                                                <td>Joining Date:&nbsp;
                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtcurrentjoiningdate" runat="server" CausesValidation="True" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtcurrentjoiningdate"
                                                                        WatermarkText="Enter MM/dd/yyyy">
                                                                    </asp:TextBoxWatermarkExtender>


                                                                    <asp:CalendarExtender ID="joiningcalender" runat="server" Enabled="True" TargetControlID="txtcurrentjoiningdate"></asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Probation Period:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtprobation" runat="server" CssClass="form-control" OnTextChanged="txtprobation_TextChanged"
                                                                        AutoPostBack="True" MaxLength="20"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:FilteredTextBoxExtender ID="ffftxtprobation" runat="server" Enabled="True" TargetControlID="txtprobation"
                                                                        ValidChars="0123456789 ABCDEFGHIGKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz ">
                                                                    </asp:FilteredTextBoxExtender>



                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Confirmation Date:

                                                                </td>
                                                                <td>

                                                                    <asp:TextBox ID="txtcnfrmdate" runat="server" OnTextChanged="txtcnfrmdate_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtcnfrmdate"
                                                                        WatermarkText="Enter MM/dd/yyyy">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="calcnfrm" runat="server" Enabled="True" TargetControlID="txtcnfrmdate"></asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr class="hidden">
                                                                <td>Net Salary:<span style="color: red;"> *</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtsalary" runat="server" CssClass="form-control" MaxLength="9"></asp:TextBox>
                                                                    <br />
                                                                    <asp:FilteredTextBoxExtender ID="txtsalary_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtsalary" ValidChars="0123456789.">
                                                                    </asp:FilteredTextBoxExtender>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <fieldset>
                                                    <legend>Account Details</legend>
                                                </fieldset>
                                                <div class="box-body">
                                                    <div class="form-group">

                                                        <table width="100%">

                                                            <tr align="left">
                                                                <td>PF AC. Number:
                                                                </td>
                                                                <td>ESIC AC. Number:
                                                                </td>
                                                                <td>Salary Type:
                                                                </td>
                                                                <td>Bank Name:
                                                                </td>
                                                                <td>Salary Ac. No:</td>
                                                            </tr>


                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtpfaccount" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtesicaccnumber" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList Width="145px" ID="ddlsalarytype" runat="server" CssClass="form-control">
                                                                        <asp:ListItem>---Select---</asp:ListItem>
                                                                        <asp:ListItem>Cheque</asp:ListItem>
                                                                        <asp:ListItem>Salary Account</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtbankname" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtaccount" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtaccount_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtaccount" ValidChars="0123456789">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="box box-primary">
                                                <fieldset>
                                                    <legend>Current Address</legend>
                                                </fieldset>
                                                <div class="box-body">
                                                    <div class="form-group">

                                                        <table width="100%">

                                                            <tr>
                                                                <td>Country:<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="form-control"
                                                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:CompareValidator ID="cmp3" runat="server" ControlToValidate="ddlCountry" Display="None"
                                                                        ErrorMessage="Select Country" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="cmp3_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                        TargetControlID="cmp3">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>
                                                            </tr>
                                                            <tr id="trstetper" runat="server" visible="false">
                                                                <td>State:<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" CssClass="form-control"
                                                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:CompareValidator ID="cmp4" runat="server" ControlToValidate="ddlState" Display="None"
                                                                        ErrorMessage="Select State" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="cmp4_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                        TargetControlID="cmp4">
                                                                    </asp:ValidatorCalloutExtender>
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" />

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>City:<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:ImageButton ID="ImgCitAdd" runat="server" ImageUrl="~/Images/i_add.png" Style="height: 16px"
                                                                        OnClick="ImgCitAdd_Click" />
                                                                    <br />
                                                                    <asp:CompareValidator ID="cmp5" runat="server" ControlToValidate="ddlCity" Display="None"
                                                                        ErrorMessage="Select City" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="cmp5_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                        TargetControlID="cmp5">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>Pin Code:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtpin" runat="server" MaxLength="6" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:FilteredTextBoxExtender ID="txtpin_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtpin" ValidChars="0123456789">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Land Mark:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtlandmark" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="font-weight: bold; color: #333333;">If rental,please provide details of the Landlord
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Name :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContact0" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:FilteredTextBoxExtender ID="txtContact0_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtContact0" ValidChars="ab cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Contact No:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtContact_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtContact" ValidChars="0123456789">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="box box-primary">
                                                <fieldset>
                                                    <legend>Permanent Address</legend>
                                                </fieldset>
                                                <div class="box-body">
                                                    <div class="form-group">

                                                        <table width="100%">
                                                            <tr>
                                                                <td>Same:
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chksame" runat="server" AutoPostBack="True" OnCheckedChanged="chksame_CheckedChanged" />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Country:<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="ddlCountryP" runat="server" AutoPostBack="True" CssClass="form-control"
                                                                        OnSelectedIndexChanged="ddlCountryP_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:CompareValidator ID="cmp7" runat="server" ControlToValidate="ddlCountryP" Display="None"
                                                                        ErrorMessage="Select Country" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="cmp7_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                        TargetControlID="cmp7">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>
                                                            </tr>
                                                            <tr id="trstatetemp" runat="server" visible="false">
                                                                <td>State:<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="ddlStateP" runat="server" AutoPostBack="True" CssClass="form-control"
                                                                        OnSelectedIndexChanged="ddlStateP_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:CompareValidator ID="cmp8" runat="server" ControlToValidate="ddlStateP" Display="None"
                                                                        ErrorMessage="Select State" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="cmp8_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                        TargetControlID="cmp8">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>City:<span style="color: red;"> *</span>

                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="ddlCityP" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCityP_SelectedIndexChanged">
                                                                    </asp:DropDownList>

                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/i_add.png" Style="height: 16px"
                                                                        OnClick="ImageButton1_Click" />
                                                                    <br />
                                                                    <asp:CompareValidator ID="cmp9" runat="server" ControlToValidate="ddlCityP" Display="None"
                                                                        ErrorMessage="Select City" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="cmp9_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                        TargetControlID="cmp9">
                                                                    </asp:ValidatorCalloutExtender>

                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>Pin Code:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtpinP" runat="server" MaxLength="6" CssClass="form-control"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:FilteredTextBoxExtender ID="txtpinP_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtpinP" ValidChars="0123456789">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Land Mark:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtlandmarkP" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="font-weight: bold; color: #333333;">If rental,please provide details of the Landlord
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Name
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContact1" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                    <asp:FilteredTextBoxExtender ID="txtContact1_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtContact1" ValidChars="ab cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Contact No:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContactP" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtContactP_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtContactP" ValidChars="0123456789">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <fieldset>
                                                    <legend>Education</legend>
                                                </fieldset>
                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <table width="100%">
                                                            <tr align="left">
                                                                <td>Degree/Diploma
                                                                </td>
                                                                <td>Name of School/College
                                                                </td>
                                                                <td>University
                                                                </td>
                                                                <td>Year of Passing
                                                                </td>
                                                                <td>Percentage (%)
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td>
                                                                    <asp:DropDownList ID="ddeducation" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:CompareValidator ID="cmmeducation" runat="server" ControlToValidate="ddeducation"
                                                                        Display="None" ErrorMessage="Select Education" Operator="NotEqual" ValidationGroup="b"
                                                                        ValueToCompare="--Select--"></asp:CompareValidator>
                                                                    <asp:ValidatorCalloutExtender ID="cmmeducation_ValidatorCalloutExtender" runat="server"
                                                                        Enabled="True" TargetControlID="cmmeducation">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtcollegename" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="reqcole" runat="server" ControlToValidate="txtcollegename"
                                                                        Display="None" ErrorMessage="Enter College Name" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="reqcole_ValidatorCalloutExtender" runat="server"
                                                                        Enabled="True" TargetControlID="reqcole">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtUniversity" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtUniversity_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtUniversity" ValidChars="ab cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="tt" runat="server" ControlToValidate="txtUniversity"
                                                                        Display="None" ErrorMessage="Enter University Name" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    <%--<asp:TextBox ID="TextBox1" runat="server"  CssClass="form-control"></asp:TextBox>--%>
                                                                    <asp:TextBox ID="txtYearOfPassing" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtYearOfPassing_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtYearOfPassing" ValidChars="0123456789">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtYearOfPassing"
                                                                        Display="None" ErrorMessage="Enter Year of Passing" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator4_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtObtainperc" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtObtainperc_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtObtainperc" ValidChars="0123456789.">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtObtainperc"
                                                                        Display="None" ErrorMessage="Enter Marks Obtained" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator6_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnaddEdu" runat="server" Text="ADD" CssClass="btn bg-blue-active" ValidationGroup="b"
                                                                        OnClick="btnaddEdu_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                    <br />
                                                                    <asp:GridView ID="grd" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="EducationId" HeaderText="EducationId" Visible="false" />
                                                                            <asp:BoundField DataField="EducationName" HeaderText="Education" />
                                                                            <asp:BoundField DataField="College_School" HeaderText="Name of School/College" />
                                                                            <asp:BoundField DataField="University" HeaderText="University" />
                                                                            <asp:BoundField DataField="YearOfPassing" HeaderText="Year Of Passing" />
                                                                            <asp:BoundField DataField="Perc" HeaderText="Obtain Percent" DataFormatString="{0:0.00}" />
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("EducationId") %>'
                                                                                        Height="20px" ImageUrl="~/Images/i_edit.png" OnClick="imgedit_Click" Width="30px" /><asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%# Eval("EducationId") %>'
                                                                                            Height="20px" ImageUrl="~/Images/i_delete.png" OnClick="imgdelete_Click" Width="30px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>No Data Exists.......</EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <fieldset>
                                                    <legend>Experience</legend>
                                                </fieldset>
                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <table width="100%">
                                                            <tr align="left">
                                                                <td>Company Name
                                                                </td>
                                                                <td>Location
                                                                </td>
                                                                <td>Joining Date
                                                                </td>
                                                                <td>Reliving Date
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td>
                                                                    <asp:TextBox ID="txtcompanyname" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtcompanyname"
                                                                        Display="None" ErrorMessage="Enter Company Name" SetFocusOnError="True" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator7_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtlocation_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtlocation" ValidChars="ab cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtlocation"
                                                                        Display="None" ErrorMessage="EnterLocation" SetFocusOnError="True" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator8_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtjoiningdate" runat="server" CssClass="form-control"> </asp:TextBox>

                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtjoiningdate"
                                                                        WatermarkText="Enter MM/dd/yyyy">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="txtjoiningdate_CalendarExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtjoiningdate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtjoiningdate"
                                                                        Display="None" ErrorMessage="Enter Joining Date" SetFocusOnError="True" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator11_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator11">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtreldate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtreldate"
                                                                        WatermarkText="Enter MM/dd/yyyy">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="txtreldate_CalendarExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtreldate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtreldate"
                                                                        Display="None" ErrorMessage="Enter  Reliving Date" SetFocusOnError="True" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator12_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator12">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td>Ref Person
                                                                </td>
                                                                <td>Contact No
                                                                </td>
                                                                <td>Department
                                                                </td>
                                                                <td>Designation
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td>
                                                                    <asp:TextBox ID="txtref" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtref_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtref" ValidChars="ab cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtrefcontactno" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="txtrefcontactno_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtrefcontactno" ValidChars="0123456789">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDept"
                                                                        Display="None" ErrorMessage="Enter Department" SetFocusOnError="True" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDesig" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDesig"
                                                                        Display="None" ErrorMessage="Enter Designation" SetFocusOnError="True" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator10_ValidatorCalloutExtender"
                                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnaddexp" runat="server" CssClass="btn bg-blue-active" Text="ADD" ValidationGroup="U"
                                                                        OnClick="btnaddexp_Click1" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                    <br />

                                                                    <asp:GridView ID="grdExperience" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                                                            <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                            <asp:BoundField DataField="RelivingDate" HeaderText="Reliving Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                            <asp:BoundField DataField="RefPerson" HeaderText="Ref Person" />
                                                                            <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                                                            <asp:BoundField DataField="Department" HeaderText="Department" />
                                                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgeditExpr" runat="server" CommandArgument='<%# Eval("CompanyName") %>'
                                                                                        Height="20px" ImageUrl="~/Images/i_edit.png" OnClick="imgeditExpr_Click" Width="30px" /><asp:ImageButton ID="imgdeleteExpr" runat="server" CommandArgument='<%# Eval("CompanyName") %>'
                                                                                            Height="20px" ImageUrl="~/Images/i_delete.png" OnClick="imgdeleteExpr_Click"
                                                                                            Width="30px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>No Data Exists.......</EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <fieldset>
                                                    <legend>Document Details</legend>
                                                </fieldset>
                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grddocument" runat="server" CssClass="table table-bordered table-striped"
                                                                        DataKey="DocumentPath" AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <%-- <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                                                                            <asp:BoundField DataField="DocumentName" HeaderText="Document Name" />
                                                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                            <%-- <asp:TemplateField HeaderText="Document File" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageDoc" runat="server" Height="100px" 
                                                                        OnClick="ImageDoc_Click" ImageUrl='<%# Eval("DocumentPath") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                                            <asp:BoundField DataField="DocumentPath" HeaderText="Document Path" />

                                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:UpdatePanel ID="upddocss" runat="server">
                                                                                        <Triggers>
                                                                                            <asp:PostBackTrigger ControlID="imgdocview" />
                                                                                        </Triggers>
                                                                                        <ContentTemplate>
                                                                                            <asp:ImageButton ID="imgdocview" runat="server" CommandArgument='<%# Eval("DocumentPath") %>'
                                                                                                OnClick="lnkDownload_Click" Height="20px" ImageUrl="~/Images/load.png" Width="30px" ToolTip="Download File" />
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>No Data Exists.......</EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">

                                            <div class="box-body">
                                                <div class="form-group">
                                                    <table width="50%">
                                                        <tr>
                                                            <td width="314px"></td>
                                                            <td align="right">
                                                                <asp:Button ID="btnconP1" ValidationGroup="a" runat="server" CssClass="btn bg-blue-active"
                                                                    Text="Save" Height="35px" OnClick="btnconP1_Click" />
                                                            </td>
                                                            <td align="right">
                                                                <asp:Button ID="btncancel" ValidationGroup="S" runat="server" CssClass="btn bg-blue-active"
                                                                    Text="Cancel" Height="35px" OnClick="btncancel_Click" CausesValidation="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </asp:View>
            </asp:MultiView>
            <table id="tblReliveingEmployee" runat="server" visible="false" style="background-color: gray; height: 200px; width: 200px">


                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" Height="200px" CssClass="panel_popup" Width="400px">
                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-body">
                                        <div class="form-group">

                                            <asp:HiddenField ID="hdcafid" runat="server" />
                                            <fieldset style="border-color: #800000; margin-left: 10px; margin-right: 10px;">
                                                <table style="width: 100%">


                                                    <tr style="background-color: #FFFFFF">
                                                        <td style="color: Black; font-size: 16px; font-weight: bold; background-color: #FFFFFF;"
                                                            align="center" class="style10">Add Relive Date:
                                                        </td>
                                                        <td style="float: right; background-color: #FFFFFF;">
                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Close.jpg" OnClick="ImageButton2_Click"
                                                                Width="23px" />
                                                        </td>
                                                    </tr>

                                                </table>
                                                <table border="0" width="100%" align="center">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="dtprelivedate" runat="server" CausesValidation="True" CssClass="form-control" BackColor="White" ReadOnly="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="dtprelivedate_CalendarExtender" runat="server" Enabled="True"
                                                                TargetControlID="dtprelivedate">
                                                            </asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="dtprelivedate"
                                                                Display="None" ErrorMessage="*" ValidationGroup="k" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <%-- <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator13">
                                                            </asp:ValidatorCalloutExtender>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <br />
                                                            <asp:Button Text="Save" runat="server" ID="btnpopsave" OnClick="btnpopsave_Click1" CssClass="btn bg-blue-active"
                                                                ValidationGroup="k" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </caption>
                                
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </fieldset>
                        </asp:Panel>
                        <br />
                        <asp:Label ID="Labelpanb" runat="server" Text=""></asp:Label>
                        <asp:ModalPopupExtender ID="modRelive" runat="server" TargetControlID="Labelpanb"
                            PopupControlID="Panel1">
                        </asp:ModalPopupExtender>
                    </td>
                </tr>

            </table>
            <asp:Panel ID="pan" CssClass="modpann" runat="server" BackColor="#EAEAEA" Style="display: none"
                Width="40%">
                <br />
                <table border="0" cellpadding="0" cellspacing="10" align="center" width="80%" bgcolor="#CCCCCC"
                    class="modalPopup">
                    <tr>
                        <td style="font-family: 'Comic Sans MS'; font-size: medium;" colspan="2" align="center">Grade Details
                        </td>
                        <td align="center">
                            <asp:Image ID="imgclose" runat="server" Height="30px" ImageUrl="~/Images/Close.jpg"
                                Width="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td>Add Grade :
                        </td>
                        <td>
                            <asp:TextBox ID="txtaddgrade" runat="server" Width="200px" CssClass="form-control" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtaddgrade"
                                Display="None" ErrorMessage="Enter Grade Name" SetFocusOnError="True" ValidationGroup="c">
                            </asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                TargetControlID="RequiredFieldValidator3">
                            </asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="center" colspan="2">&nbsp;
                            <asp:Button ID="btnsavecity" runat="server" Text="Add" CssClass="btn bg-blue-active" OnClick="btnsavecity_Click"
                                ValidationGroup="c" />
                            &nbsp;
                            <asp:Button ID="btncan" runat="server" Text="Cancel" CssClass="btn bg-blue-active" OnClick="btncan_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </asp:Panel>
            <asp:Label ID="Label19" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="modNewTax" runat="server" Enabled="True" PopupControlID="pan"
                CancelControlID="imgclose" TargetControlID="Label19">
            </asp:ModalPopupExtender>


            <asp:Panel ID="PanCity" CssClass="modpann" runat="server" Width="40%"
                Style="display: none">
                <br />
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <table border="0" cellpadding="0" cellspacing="10" align="center" width="80%" bgcolor="#CCCCCC"
                                    class="modalPopup">
                                    <tr>
                                        <td style="font-family: 'Comic Sans MS'; font-size: medium;" colspan="2" align="center">City Details
                                        </td>
                                        <td align="center">
                                            <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="~/Images/Close.jpg"
                                                Width="30px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%-- <asp:Panel ID="PnlAdd" runat="server" Width="500px">--%>
                                            <table cellpadding="2" cellspacing="2" width="100%">
                                                <%--  <tr>
                                                            <td>
                                                                Country Name:</td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList1" runat="server" style="margin-left: 12px" 
                                                                    onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                 <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                                             ControlToValidate="ddlCountry" Display="None"
                                                                    
                                                             ErrorMessage="Select Country" Operator="NotEqual" ValidationGroup="w" 
                                                            ValueToCompare="--Select--"></asp:CompareValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                                                    TargetControlID="CompareValidator1">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                State Name:</td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList2" runat="server" style="margin-left: 12px"   AutoPostBack="false"
                                                                    onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                 <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                                             ControlToValidate="ddlstate" Display="None"
                                                                    
                                                             ErrorMessage="Select State" Operator="NotEqual" ValidationGroup="w" 
                                                            ValueToCompare="--Select--"></asp:CompareValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                                                    TargetControlID="cmp2">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>--%>
                                                <tr>
                                                    <td>City Name:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtcityname" runat="server" CausesValidation="True" ValidationGroup="w" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        <asp:Label ID="lblcityid" runat="server" Visible="False"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="reqcitt" runat="server" ControlToValidate="txtcityname"
                                                            Display="None" ErrorMessage="Enter City Name" ValidationGroup="w"></asp:RequiredFieldValidator>
                                                        <asp:ValidatorCalloutExtender ID="reqcitt_ValidatorCalloutExtender" runat="server"
                                                            Enabled="True" TargetControlID="reqcitt">
                                                        </asp:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%--  Status:--%>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rd_status" runat="server" RepeatDirection="Horizontal" Visible="false">
                                                            <asp:ListItem Text="Active" Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="InActive" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="w"
                                                            CssClass="btn bg-blue-active" />
                                                        &nbsp
                                        <asp:Button ID="Button1" runat="server" Text="Cancel" OnClick="Button1_Click" CssClass="btn bg-blue-active" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--  </asp:Panel>--%>
                                            <%-- <asp:RoundedCornersExtender ID="rn" runat="server" TargetControlID="PnlAdd" Radius="10"
                                                Corners="All" BorderColor="#333">
                                            </asp:RoundedCornersExtender>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <br />
            </asp:Panel>
            <asp:Label ID="Label20" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="ModelPopUpCity" runat="server" Enabled="True" PopupControlID="PanCity"
                CancelControlID="Image2" TargetControlID="Label20">
            </asp:ModalPopupExtender>







            <%-- <asp:Panel ID="PanalCity12" CssClass="modpann" runat="server" BackColor="#EAEAEA"   Width="40%"  style="display:none">
                              
                <br />
                <table border="0" cellpadding="0" cellspacing="10" align="center" width="80%" bgcolor="#CCCCCC"
                    class="modalPopup">
                                          <tr>
                                            <td style="font-family: 'Comic Sans MS'; font-size: medium;" colspan="2" align="center">
                                               City Details</td>
                                            <td align="center">
                                                <asp:Image ID="Image3" runat="server" Height="30px" ImageUrl="~/Images/Close.jpg"
                                                    Width="30px" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                City Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCityName11" runat="server" CausesValidation="True" 
                                                    ValidationGroup="w"></asp:TextBox>
                                                <asp:Label ID="Label22" runat="server" Visible="False"></asp:Label>
                                                <asp:RequiredFieldValidator ID="errr" runat="server" 
                                                    ControlToValidate="txtCityName11" Display="None" ErrorMessage="Enter City Name" 
                                                    ValidationGroup="w"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vvgggg" 
                                                    runat="server" Enabled="True" TargetControlID="errr">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                             
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="reqqq" runat="server" RepeatDirection="Horizontal" Visible="false">
                                                    <asp:ListItem Text="Active" Value="0" Selected="True">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="InActive" Value="1">
                                  
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnAADDCity" runat="server" Text="Save"  ValidationGroup="w"
                                                    CssClass="add" CausesValidation="False" onclick="BtnAADDCity_Click" />
                                                &nbsp
                                                <asp:Button ID="BtnCancelCity" runat="server" Text="Cancel" 
                                                    CssClass="cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                            
                                            </td>
                                        </tr>
                              

                </table>
                <br />
            </asp:Panel>
                       <asp:Label ID="Label23" runat="server"></asp:Label>
                                        <asp:ModalPopupExtender ID="ModalPopupCity22" runat="server" Enabled="True" PopupControlID="PanalCity12"
                                            CancelControlID="Image3" TargetControlID="Label23">
                                        </asp:ModalPopupExtender>
            --%>
            <asp:Panel CssClass="panel_popup" runat="server" ID="panel2" BackColor="White" ForeColor="#333"
                Width="86%" Height="590px">
                <div style="float: right">
                    <asp:Image ID="Image5" runat="server" Height="30px" ImageUrl="~/Images/Close.jpg"
                        Width="30px" />
                </div>
                <iframe name="iframe_a1" onload="ResizeIframe('iframe_a')" scrolling="no" class="autoHeight"
                    height="500%" scrolling="no" marginheight="0" frameborder="0" frameborder="1"
                    width="100%" src="EmployeeDetails1.aspx"></iframe>
            </asp:Panel>

            <asp:Label ID="Label64" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="ModelPoUpEmployeeView" runat="server" Enabled="True"
                PopupControlID="panel2" TargetControlID="Label64">
            </asp:ModalPopupExtender>

            <%--  <asp:UpdateProgress ID="updtprg" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="blur">
                <asp:Image ID="imgloading" runat="server" ImageUrl="Images/ajax-loader.gif" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
