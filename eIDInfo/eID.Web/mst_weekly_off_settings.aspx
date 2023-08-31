<%@ Page Title="Weekly Off Settings" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="mst_weekly_off_settings.aspx.cs" Inherits="mst_weekly_off_settings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grdweekoffdata').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grdweekoffdata_wrapper .col-sm-6:eq(0)');


            var table = $('#ContentPlaceHolder1_grdemployeeWeeklyoff').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grdemployeeWeeklyoff_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
            <ContentTemplate>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="col-lg-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Week Offs List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnadd" runat="server" Text="Add New" CssClass="btn btn-box-tool bg-orange" OnClick="btnadd_Click" />                                        
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdweekoffdata" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            CssClass="table table-bordered table-striped" OnRowDataBound="grdweekoffdata_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                <asp:BoundField DataField="Days" HeaderText="Days" />
                                                <asp:BoundField DataField="TrackHolidays" HeaderText="Holidays Track" />
                                                <asp:BoundField DataField="Date" HeaderText="Effective Date" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsubmit">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Add Week off</h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label class="control-label">Company <span class="text-red">*</span></label>
                                            <asp:Label ID="lblcompanyid" runat="server" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Off days <span class="text-red">*</span></label>
                                            <asp:DropDownList ID="ddsatoff" runat="server" CssClass="form-control">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>1 &amp; 2</asp:ListItem>
                                                <asp:ListItem>2 &amp; 3</asp:ListItem>
                                                <asp:ListItem>3 &amp; 4</asp:ListItem>
                                                <asp:ListItem>1 &amp; 3 </asp:ListItem>
                                                <asp:ListItem>2 &amp; 4</asp:ListItem>
                                                <asp:ListItem>1 &amp; 4</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Day <span class="text-red">*</span></label>
                                            <asp:DropDownList ID="dddays" runat="server" CssClass="form-control">
                                                <asp:ListItem>--Select--</asp:ListItem>
                                                <asp:ListItem>Monday</asp:ListItem>
                                                <asp:ListItem>Tuesday</asp:ListItem>
                                                <asp:ListItem>Wednesday</asp:ListItem>
                                                <asp:ListItem>Thursday</asp:ListItem>
                                                <asp:ListItem>Friday</asp:ListItem>
                                                <asp:ListItem>Saturday</asp:ListItem>
                                                <asp:ListItem>Sunday</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Effective Date <span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" CssClass="text-red" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="d"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" CausesValidation="False" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:View>


                    <asp:View ID="View3" runat="server">
                        <div class="col-lg-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Employee Weekly Off List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btmEmpAdd" runat="server" Text="Add New" CssClass="btn btn-box-tool bg-orange" OnClick="btmEmpAdd_Click" />
                                        <asp:Button ID="btnImport" runat="server" CssClass="btn btn-box-tool btn-github" Text="Import Weekly Off" OnClick="btnImport_Click" />
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
                                    <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlEmployee"></asp:DropDownList>
                                </div>                               
                                <div class="col-lg-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
                                </div>                                
                            </div>



                                    <div class="table-responsive">
                                        <asp:GridView ID="grdemployeeWeeklyoff" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            CssClass="table table-bordered table-striped" OnRowDataBound="grdemployeeWeeklyoff_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="EmployeeNo" HeaderText="Employee No" />
                                                <asp:BoundField DataField="Days" HeaderText="Days" />
                                                <asp:BoundField DataField="DayId" HeaderText="Day Id"/>  
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%# Eval("WeeklyOffid") %>'
                                                            Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="OnClick_Edit" ToolTip="Edit" />
                                                        <%--<asp:ImageButton ID="Delete" runat="server" CommandArgument='<%# Eval("DeptID") %>'
                                                            Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" OnClick="Delete_Click" ToolTip="Delete" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>


                    <asp:View ID="View4" runat="server">
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="btnsubmit">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Add Employee Week off</h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label class="control-label">Company <span class="text-red">*</span></label>
                                            <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlEmpcompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlEmpcompany_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-red" ControlToValidate="ddlCompany" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label">Department <span class="text-red">*</span></label>
                                            <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlEmpDepartment" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlEmpDepartment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-red" ControlToValidate="ddlEmpDepartment" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlEmpDepartment" InitialValue="0" ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label">Employee <span class="text-red">*</span></label>
                                            <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlEmpEmployee" runat="server" AutoPostBack="True" CssClass="form-control">
                                            </asp:DropDownList>   
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="text-red" ControlToValidate="ddlEmpEmployee" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlEmpEmployee" InitialValue="0" ID="RequiredFieldValidator9" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="d"></asp:RequiredFieldValidator>
                                        </div>                                                                               
                               
                                        <div class="form-group">
                                            <label class="control-label">Day <span class="text-red">*</span></label>
                                            <asp:DropDownList ID="ddlEmpdays" runat="server" CssClass="form-control">
                                                <asp:ListItem>--Select--</asp:ListItem>
                                                <asp:ListItem Value="MONDAY">Monday</asp:ListItem>
                                                <asp:ListItem Value ="TUESDAY">Tuesday</asp:ListItem>
                                                <asp:ListItem Value ="WEDNESDAY">Wednesday</asp:ListItem>
                                                <asp:ListItem Value ="THURSDAY">Thursday</asp:ListItem>
                                                <asp:ListItem Value ="FRIDAY">Friday</asp:ListItem>
                                                <asp:ListItem Value ="SATURDAY">Saturday</asp:ListItem>
                                                <asp:ListItem Value ="SUNDAY">Sunday</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        
                                        <div class="form-group">
                                            <label class="control-label">DayID <span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtDayId" runat="server" CssClass="form-control" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <%--<asp:Button ID="btnEmpCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnEmpCancel_Click" CausesValidation="False" />--%>
                                        <asp:Button ID="btnEmpWeeklyoffSubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right" OnClick="btnEmpWeeklyoffSubmit_Click" />
                                    </div>

                                    <asp:Label ID="lblempWeeklyid" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:View>










                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

