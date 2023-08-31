<%@ Page Title="Upload Persons In Device" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="device_import_persons.aspx.cs" Inherits="device_import_persons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css">--%>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <link href="admin_theme/dist/css/bootstrap_multiselect.css" rel="stylesheet" />
    <script src="admin_theme/dist/js/bootstrap_multiselect.js"></script>
    <script type="text/javascript">
        function jqFunctions() {

            $('#ContentPlaceHolder1_lstFruits').multiselect({

                includeSelectAllOption: true,
                enableFiltering: true,
                maxHeight: 450,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
                dropRight: true,
                onDropdownShow: function (event) {
                    $(this).closest('select').css('width', '500px')
                }
            });

            //$('[id*=lstFruits]').multiselect({
            //    includeSelectAllOption: true,
            //    enableFiltering: true,
            //    maxHeight: 450,
            //    filterPlaceholder: 'Search',
            //    enableCaseInsensitiveFiltering: true,
            //    dropRight: true
            //});


            var table = $('#ContentPlaceHolder1_gvEmployeeList').DataTable({
                order: [],
                columnDefs: [{
                    'targets': [0], /* column index [0,1,2,3]*/
                    'orderable': false, /* true or false */
                }],
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis'],
                "lengthMenu": [10, 25, 50, 100, 500]
               
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvEmployeeList_wrapper .col-sm-6:eq(0)');


        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>

    <script>
        function validateHeader(check) {
            if (check.checked) { console.log('checked'); checkAll(); }
            else {
                console.log('un-checked');
                uncheckAll();
            }
        }
        function validate(selCb) {
            if (selCb.checked) {
                console.log("checked");
            } else {
                var x = $("#ContentPlaceHolder1_gvEmployeeList_checkAll").is(":checked");

                if (x == true) {
                    $("#ContentPlaceHolder1_gvEmployeeList_checkAll").prop('checked', false);
                }
                else
                    console.log("You didn't check it! Let me check it for you.")
            }
        }


        //create checkall function
        function checkAll() {
            $("#ContentPlaceHolder1_gvEmployeeList").find("[type='checkbox']").prop('checked', true);
        }
        //create uncheckall function
        function uncheckAll() {
            $("#ContentPlaceHolder1_gvEmployeeList").find("[type='checkbox']").prop('checked', false);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnImport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Upload Persons</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2">
                                    <label class="control-label">Company Name <span class="text-red">*</span></label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>                                
                                <div class="form-group col-md-2">
                                    <label class="control-label">Department Name</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-red" ControlToValidate="ddlDepartment" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" InitialValue="0" ID="RequiredFieldValidator2" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label">Location</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select Location</asp:ListItem>
                                            <asp:ListItem Value="BEAWAR">BEAWAR</asp:ListItem>
                                            <asp:ListItem Value="BIHAR">BIHAR</asp:ListItem>
                                            <asp:ListItem Value="BULANDSHAHAR">BULANDSHAHAR</asp:ListItem>
                                            <asp:ListItem Value="GUNTUR">GUNTUR</asp:ListItem>
                                            <asp:ListItem Value="GURUGRAM">GURUGRAM</asp:ListItem>
                                            <asp:ListItem Value="JAIPUR">JAIPUR</asp:ListItem>
                                            <asp:ListItem Value="JHARKHAND">JHARKHAND</asp:ListItem>
                                            <asp:ListItem Value="KHUSHKHERA">KHUSHKHERA</asp:ListItem>
                                            <asp:ListItem Value="KODLA">KODLA</asp:ListItem>
                                            <asp:ListItem Value="NAWALGARH">NAWALGARH</asp:ListItem>
                                            <asp:ListItem Value="ODISHA">ODISHA</asp:ListItem>
                                            <asp:ListItem Value="PANIPAT">PANIPAT</asp:ListItem>
                                            <asp:ListItem Value="PUNE">PUNE</asp:ListItem>
                                            <asp:ListItem Value="PURULIA">PURULIA</asp:ListItem>
                                            <asp:ListItem Value="RAS">RAS</asp:ListItem>
                                            <asp:ListItem Value="ROORKEE">ROORKEE</asp:ListItem>
                                            <asp:ListItem Value="SURATGARH">SURATGARH</asp:ListItem>
                                            <asp:ListItem Value="RAIPUR">RAIPUR</asp:ListItem>
                                            <asp:ListItem Value="CCR">CCR</asp:ListItem>
                                            <asp:ListItem Value="Main Gate">Main Gate</asp:ListItem>
                                        </asp:DropDownList>                                        
                                    </div>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label">Employee Name</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-red" ControlToValidate="ddlEmployee" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlEmployee" InitialValue="0" ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="form-group col-md-1">
                                    <label class="control-label">Face</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlFace" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFace_SelectedIndexChanged">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-red" ControlToValidate="ddlDepartment" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" InitialValue="0" ID="RequiredFieldValidator2" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>

                                  <div class="form-group col-md-1">
                                    <label class="control-label">Card</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlCard" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCard_SelectedIndexChanged1">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>                                       
                                    </div>
                                </div>

                                 <div class="form-group col-md-1">
                                    <label class="control-label">FingerPrint</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlFingerprint" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFingerprint_SelectedIndexChanged">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>                                       
                                    </div>
                                </div>

                                <div class="form-group col-md-1">
                                    <label class="control-label">Gender</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>FeMale</asp:ListItem>
                                        </asp:DropDownList>                                        
                                    </div>
                                </div>

                                <div class="form-group col-md-2">
                                    <label class="control-label">Device <span class="text-red">*</span></label>
                                    <div class="">
                                        <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ControlToValidate="lstFruits" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-md-1">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;</label>
                                    <div class="">
                                        <asp:Button ID="btnImport" runat="server" CssClass="btn btn-primary" ValidationGroup="A" Text="Upload" OnClick="btnImport_Click" />
                                        <asp:HiddenField ID="hfTenant" runat="server" />
                                        <%--<asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="A" OnClick="btnSearch_Click" />--%>
                                    </div>
                                </div>
                                <div class="form-group col-md-1">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;</label>
                                    <div class="">
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" ValidationGroup="A" Text="Update" OnClick="btnUpdate_Click" />                                       
                                        
                                    </div>
                                </div>
                                <div class="form-group col-md-4 hidden">
                                    <label class="control-label">Select File</label>
                                    <div class="input-group">

                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                        <div class="input-group-btn">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="box-footer no-padding">
                            <asp:GridView ID="gvEmployeeList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvEmployeeList_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select All">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" CssClass="case" runat="server" onclick="validate(this);" />
                                            <asp:Label ID="lblEmployeeNo" runat="server" Text='<%#Eval("EmployeeNo") %>' CssClass="hidden"></asp:Label>
                                            <asp:Label ID="lblAccessCardNo" runat="server" Text='<%#Eval("AccessCardNo") %>' CssClass="hidden"></asp:Label>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("EmployeeId") %>' CssClass="hidden"></asp:Label>
                                            <asp:Label ID="lblExDate" runat="server" Text='<%#Eval("ExDate", "{0:yyyy-MM-dd}") %>' CssClass="hidden"></asp:Label>
                                            <asp:Label ID="lblFace" runat="server" Text='<%#Eval("IsFace") %>' CssClass="hidden"></asp:Label>
                                            <asp:Label ID="lblFingerprint" runat="server" Text='<%#Eval("IsFingerPrintPresent") %>' CssClass="hidden"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" Text="&nbsp;All" onclick="validateHeader(this);" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Employee Name" DataField="EmpName" />
                                    <asp:BoundField HeaderText="Employee No" DataField="EmployeeNo"/>
                                    <asp:BoundField HeaderText="Gender" DataField="Gender" />
                                    <asp:BoundField HeaderText="Face" DataField="IsFace" />
                                    <asp:BoundField HeaderText="Access Card" DataField="IsAccessCard" />
                                     <asp:BoundField HeaderText="Fingerprint" DataField="IsFingerPrintPresent" />
                                    <asp:BoundField HeaderText="Expiry Date" DataField="ExDate" DataFormatString="{0:MM/dd/yyyy}" />
                                </Columns>
                            </asp:GridView>


                        </div>
                        <div class="box-footer no-padding">
                            <asp:GridView ID="gvDataList" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvDataList_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Employee No" DataField="employeeNo" />
                                    <asp:BoundField HeaderText="Employee Name" DataField="name" />
                                    <asp:BoundField HeaderText="Type" DataField="userType" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="box-footer">
                            <asp:Literal ID="litErrors" runat="server"></asp:Literal>
                        </div>

                        
                         <div class="box-footer">
                            <ul class="pagination" style="margin:0px !important">
                                <asp:Repeater ID="rptPager" runat="server">
                                    <ItemTemplate>
                                        <li class="page-item">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page-link" : "page-link page_enabled" %>'
                                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                            </ul>
                            <a href="javascript:void(0)" class="btn btn-sm btn-success btn-flat pull-right"><i class="fa fa-arrow-left"></i>&nbsp; active page no is <asp:Label ID="lblPageNo" runat="server"></asp:Label></a>
                        </div>



                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

