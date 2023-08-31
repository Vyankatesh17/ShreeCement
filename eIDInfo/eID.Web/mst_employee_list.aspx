<%@ Page Title="Employee List" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="mst_employee_list.aspx.cs" Inherits="mst_employee_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">

    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_Emp').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Emp_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
    <style>
        .wb-border-orange {
            border: 1px solid #f39c12 !important;
        }

        .wb-border-blue {
            border: 1px solid #273972 !important;
        }

        .bg-wb-orange {
            background-color: #f39c12 !important;
        }

        .bg-wb-blue {
            background-color: #273972 !important;
        }

        .wu-username-custom {
            margin-left: 15px !important;
            font-size: 20px !important;
            color: #f6f6f6 !important;
        }
    </style>
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
    <div class="row">
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div class="col-lg-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Employee List</h3>
                            <div class="box-tools">
                                <asp:Button ID="btnadd" runat="server" CssClass="btn btn-box-tool bg-orange" Text="Add New" OnClick="btnadd_Click" />
                                <asp:Button ID="btnImport" runat="server" CssClass="btn btn-box-tool btn-github" Text="Import Employee" OnClick="btnImport_Click" />
                                <a href="device_download_persons_face.aspx" class="btn btn-box-tool btn-bitbucket">Download Faces</a>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-2 form-group">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList runat="server" ID="ddlCompanyList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyList_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 form-group">
                                    <label class="control-label">Department</label>
                                    <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3 form-group">
                                    <label class="control-label">Employee</label>
                                    <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlEmployeeList"></asp:DropDownList>
                                </div>
                                <div class="col-lg-2 form-group">
                                    <label class="control-label">Employee Code</label>
                                    <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control" placeholder="Employee Code"></asp:TextBox>
                                </div>
                                <div class="col-lg-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-primary" OnClick="btnsearch_Click" Text="Search" />
                                </div>
                                <div class="col-lg-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label><asp:Button ID="btnReset" runat="server" CssClass="btn btn-info" Text="Reset" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-body no-padding">
                            <div class="table-responsive">
                                <asp:GridView ID="grd_Emp" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-responsive" AllowPaging="false"
                                    OnRowDataBound="grd_Emp_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="txtdetails" ReadOnly="true" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Code">
                                            <ItemTemplate>
                                                <asp:Label ID="txtempno" ReadOnly="true" runat="server" Text='<%# Eval("MachineID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Device Code">
                                            <ItemTemplate>
                                                <asp:Label ID="txtdno" ReadOnly="true" runat="server" Text='<%# Eval("EmployeeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office Mail">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemailid" ReadOnly="true" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcntct" ReadOnly="true" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOJ" ReadOnly="true" runat="server" Text='<%# Eval("DOJ1","{0:MM/dd/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expiry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExD" ReadOnly="true" runat="server" Text='<%# Eval("DExpiryDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldept" ReadOnly="true" runat="server" Text='<%# Eval("DeptName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RelivingDate" HeaderText="Relive Date" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group" style="min-width: 100px;">
                                                    <asp:LinkButton ID="lbtnView" runat="server" OnClick="lbtnView_Click" Visible="false" CommandArgument='<%#Eval("EmployeeId") %>' CssClass="btn btn-sm btn-info"><i class="fa fa-eye"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="lbtnEdit_Click" CommandArgument='<%#Eval("EmployeeId") %>' CssClass="btn btn-sm btn-warning"  data-toggle="tooltip" data-placement="top" title="Edit Employee"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnRelease" runat="server" OnClick="lbtnRelease_Click" CommandArgument='<%#Eval("EmployeeId") %>' CssClass="btn btn-sm btn-danger" Visible="false"><i class="fa fa-remove"></i></asp:LinkButton>
                                                    <a href='device_modify_employee.aspx?empId=<%#Eval("EmployeeId") %>' class="btn btn-sm btn-instagram" data-toggle="tooltip" data-placement="top" title="Modify in Device"><i class="fa fa-mobile"></i></a>
                                                    <asp:LinkButton ID="btnDisable" runat="server" OnClick="btnDisable_Click" CommandArgument='<%#Eval("EmployeeId") %>' OnClientClick="return confirm('are you sure to delete employee?');" CssClass="btn btn-sm btn-flickr" data-toggle="tooltip" data-placement="top" title="Delete Employee"><i class="fa fa-remove"></i></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:UpdatePanel ID="update22" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddldept" />
                        <asp:AsyncPostBackTrigger ControlID="dddesg" />
                        <asp:AsyncPostBackTrigger ControlID="ddlsalarytype" />
                        <asp:PostBackTrigger ControlID="btnaddexp" />
                        <asp:PostBackTrigger ControlID="btnconP1" />
                        <asp:PostBackTrigger ControlID="btnconP2" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="col-lg-12">
                            <div class="box">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Add New Employee</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnconP2" ValidationGroup="a" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="btnconP1_Click" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="box box-widget widget-user-2">
                                                <div class="widget-user-header bg-wb-blue" style="padding: 5px !important">

                                                    <h4 class="widget-user-username wu-username-custom">Personal Information</h4>
                                                </div>
                                                <div class="box-body wb-border-blue">
                                                    <div class="row">
                                                        <div class="form-group col-lg-4">
                                                            <label class="control-label">Device Code <span class="text-red">*</span></label>
                                                            <div>
                                                                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ControlToValidate="txtEmpCode" ID="RequiredFieldValidator1" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-4">
                                                            <label class="control-label">Employee Code <span class="text-red">*</span></label>
                                                            <div>
                                                                <asp:TextBox ID="txtBioId" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ControlToValidate="txtBioId" ID="RequiredFieldValidator2" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-4">
                                                            <label class="control-label">Access Card <%--<span class="text-red">*</span>--%></label>
                                                            <div>
                                                                <asp:TextBox ID="txtAccessCard" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ControlToValidate="txtAccessCard" ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Company <span class="text-red">*</span></label>
                                                            <div>
                                                                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ID="RequiredFieldValidator4" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="3" ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">First Name <span class="text-red">*</span></label>
                                                            <div>
                                                                <div class="input-group">
                                                                    <div class="input-group-btn">
                                                                        <asp:DropDownList ID="ddlsalitude" runat="server" CssClass="form-control" Width="70px">
                                                                            <asp:ListItem Selected="True">Mr</asp:ListItem>
                                                                            <asp:ListItem>Ms</asp:ListItem>
                                                                            <asp:ListItem>Mrs</asp:ListItem>
                                                                            <asp:ListItem>Dr.</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <asp:TextBox ID="txtfname" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                </div>
                                                                <asp:RequiredFieldValidator ControlToValidate="txtfname" ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Middle Name</label>
                                                            <asp:TextBox ID="txtmname" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Last Name <span class="text-red">*</span></label>
                                                            <div>
                                                                <asp:TextBox ID="txtlname" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                <asp:Label ID="lblempid" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                <asp:RequiredFieldValidator ControlToValidate="txtlname" ID="RequiredFieldValidator14" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Department <span class="text-red">*</span></label>
                                                            <div>
                                                                <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="dddept_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ControlToValidate="ddldept" ID="RequiredFieldValidator16" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ControlToValidate="ddldept" InitialValue="0" ID="RequiredFieldValidator15" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Designation</label>
                                                            <asp:DropDownList ID="dddesg" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Reporting Head <%--<span class="text-red">*</span>--%></label>
                                                            <div>
                                                                <asp:DropDownList ID="ddlReportingHead" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ControlToValidate="ddlReportingHead" ID="RequiredFieldValidator17" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Joining Date</label>
                                                            <asp:TextBox ID="txtcurrentjoiningdate" TextMode="Date" runat="server" CausesValidation="True" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Device Expiry Date</label>
                                                            <asp:TextBox ID="txtDExDate" TextMode="Date" runat="server" CausesValidation="True" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Employee Type</label>
                                                            <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Value="None">Select</asp:ListItem>
                                                                <asp:ListItem Value="Permanent">Permanent</asp:ListItem>
                                                                <asp:ListItem Value="Temporary">Temporary</asp:ListItem>
                                                                <asp:ListItem Value="Trainee">Trainee</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Work Location <span class="text-red">*</span></label>
                                                            <asp:TextBox ID="txtWorkLocation" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ControlToValidate="txtWorkLocation" ID="RequiredFieldValidator13" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="box box-widget widget-user-2">
                                                <div class="widget-user-header bg-wb-orange" style="padding: 5px !important">
                                                    <h4 class="widget-user-username wu-username-custom">Contact Information</h4>
                                                </div>
                                                <div class="box-body wb-border-orange">
                                                    <div class="row">
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Mobile <span class="text-red">*</span></label>
                                                            <div>
                                                                <asp:TextBox ID="txtcontactno" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ControlToValidate="txtcontactno" ID="RequiredFieldValidator19" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Alternate</label>
                                                            <asp:TextBox ID="txtaltcontact" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Work Mail <span class="text-red">*</span></label>
                                                            <div>
                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ControlToValidate="txtEmail" ID="RequiredFieldValidator20" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-6">
                                                            <label class="control-label">Personal Mail <%--<span class="text-red">*</span>--%></label>
                                                            <div>
                                                                <asp:TextBox ID="txtpersonalEmail" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ControlToValidate="txtpersonalEmail" ID="RequiredFieldValidator21" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-sm-6">
                                                            <label class="control-label">Birth Date</label>
                                                            <asp:TextBox ID="txtbirtdate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-sm-6">
                                                            <label class="control-label">Gender</label>
                                                            <asp:RadioButtonList ID="RbGender" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Value="Male">Male</asp:ListItem>
                                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-sm-12">
                                                            <label class="control-label">Address</label>
                                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-sm-6">
                                                            <label class="control-label">Pincode</label>
                                                            <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-sm-6">
                                                            <label class="control-label">Image</label>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" accept="image/*" EnableTheming="True"
                                                                onchange=" this.form.submit();" />
                                                            <asp:Label ID="lblAttachPath" runat="server" Visible="False"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-sm-6">
                                                            <asp:Image ID="image1" runat="server" Height="100px" Width="150px" Visible="False" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-sm-6">
                                                            <a href="#" class="btn btn-default"  id="viewMod" onclick="modalShow();" data-backdrop="static" data-keyboard="false" data-target=".bs-example-modal-lg">Capture Photo</a>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="box box-widget widget-user-2">
                                                <div class="widget-user-header bg-wb-blue" style="padding: 5px !important">
                                                    <h4 class="widget-user-username wu-username-custom">Account Information</h4>
                                                </div>
                                                <div class="box-body wb-border-blue">
                                                    <div class="row">
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">PF Number</label>
                                                            <asp:TextBox ID="txtpfaccount" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">ESIC</label>
                                                            <asp:TextBox ID="txtesicaccnumber" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Salary Mode</label>
                                                            <asp:DropDownList Width="145px" ID="ddlsalarytype" runat="server" CssClass="form-control">
                                                                <asp:ListItem>---Select---</asp:ListItem>
                                                                <asp:ListItem>Cheque</asp:ListItem>
                                                                <asp:ListItem>Salary Account</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Bank Name</label>
                                                            <asp:TextBox ID="txtbankname" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">IFSC</label>
                                                            <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Account Number</label>
                                                            <asp:TextBox ID="txtaccount" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="box box-widget widget-user-2">
                                                <div class="widget-user-header bg-wb-orange" style="padding: 5px !important">
                                                    <h4 class="widget-user-username wu-username-custom">Experience Information</h4>
                                                </div>
                                                <div class="box-body wb-border-orange">
                                                    <div class="row">
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Company</label>
                                                            <div>
                                                                <asp:TextBox ID="txtcompanyname" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtcompanyname"
                                                                    Display="Dynamic" ErrorMessage="Enter Company Name" CssClass="text-red" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Location</label>
                                                            <div>
                                                                <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtlocation"
                                                                    CssClass="text-red" Display="Dynamic" ErrorMessage="EnterLocation" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Department</label>
                                                            <div>
                                                                <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDept"
                                                                    CssClass="text-red" Display="Dynamic" ErrorMessage="Enter Department" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Designation</label>
                                                            <div>
                                                                <asp:TextBox ID="txtDesig" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDesig"
                                                                    CssClass="text-red" Display="Dynamic" ErrorMessage="Enter Designation" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Joinging</label>
                                                            <asp:TextBox ID="txtjoiningdate" runat="server" CssClass="form-control" TextMode="Date"> </asp:TextBox>
                                                            <div>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtjoiningdate"
                                                                    CssClass="text-red" Display="Dynamic" ErrorMessage="Enter Joining Date" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Relieving</label>
                                                            <div>
                                                                <asp:TextBox ID="txtreldate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtreldate"
                                                                    CssClass="text-red" Display="Dynamic" ErrorMessage="Enter  Reliving Date" ValidationGroup="U"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Reference Name</label>
                                                            <asp:TextBox ID="txtref" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-2 form-group">
                                                            <label class="control-label">Number</label>
                                                            <asp:TextBox ID="txtrefcontactno" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-1 form-group">
                                                            <label class="control-label">&nbsp;&nbsp;&nbsp;</label>
                                                            <asp:Button ID="btnaddexp" runat="server" CssClass="btn bg-blue-active" Text="ADD" ValidationGroup="U"
                                                                OnClick="btnaddexp_Click1" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
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
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="box-footer">
                                    <asp:Button ID="btncancel" ValidationGroup="S" runat="server" CssClass="btn btn-default"
                                        Text="Cancel" OnClick="btncancel_Click" CausesValidation="False" />
                                    <asp:Label ID="lblSummary" runat="server" CssClass="text-red" Visible="false" Text="Please enter all required fields.."></asp:Label>
                                    <asp:Button ID="btnconP1" ValidationGroup="S" runat="server" CssClass="btn btn-primary pull-right"
                                        Text="Save" OnClick="btnconP1_Click" />
                                </div>
                            </div>
                        </div>


                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
        </asp:MultiView>
    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" id="onload_iframeModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Capture Photo</h4>
                </div>
                <div class="modal-body">
                    <iframe runat="server" frameborder="0" id="frmExample" style="width: 100%; min-height: 350px;  overflow: hidden;"></iframe>
                </div>
            </div>
        </div>
    </div>

    <div id="MyDialog">
    </div>
    <script>
        function modalShow() {
            //e.preventDefault();
            if ($("[id*=ContentPlaceHolder1_ddlCompany]").val() !== "0") {

                var sel = document.getElementById("ContentPlaceHolder1_ddlCompany");
                var text = sel.options[sel.selectedIndex].text;
                console.log(text);
                // here 1 is online device and 0 for offline device 
                var url = "capture_employee_photo.aspx?cId=" + text;
                console.log(url);
                $("#<%=frmExample.ClientID%>").attr("src", url);
                $('#onload_iframeModal').modal('show');
            }
            else {
                alert('Please select company first..');
                $('#onload_iframeModal').modal('hide');
            }
        }

    </script>
</asp:Content>
