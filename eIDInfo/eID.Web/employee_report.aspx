<%@ Page Title="Employee Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="employee_report.aspx.cs" Inherits="employee_report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="box">
                        <div class="box-header box-solid">
                            <h3 class="box-title">Employee Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-sm-2 form-group">
                                    Company Name
                        <asp:DropDownList runat="server" ID="ddlCompanyList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyList_SelectedIndexChanged"></asp:DropDownList>

                                </div>
                                <div class="col-sm-2 form-group">
                                    Department Name 
                        <asp:DropDownList runat="server" ID="ddldept" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldept_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2 form-group">
                                    Employee Name
                        <asp:DropDownList runat="server" ID="ddlemp" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldesign_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2 form-group">
                                    Employee Code
                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-sm-2 form-group">
                                    Employee Type
                         <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="Permanent">Permanent</asp:ListItem>
                                                                <asp:ListItem Value="Temporary">Temporary</asp:ListItem>
                                                                <asp:ListItem Value="Trainee">Trainee</asp:ListItem>
                                                                <asp:ListItem Value="Contract">Contract</asp:ListItem>
                                                                <asp:ListItem Value="Probation">Probation</asp:ListItem>
                                                            </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 form-group">
                                    Employee Status
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                            <asp:ListItem Value="0">Relieving</asp:ListItem>
                            <asp:ListItem Value="1">Working</asp:ListItem>
                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 form-group">
                                    <div style="margin-top: 18px;">
                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" OnClick="btnsearch_Click"
                                            Text="Search" />

                                        <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" OnClick="btncancel_Click"
                                            Text="Cancel" />

                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="box-footer no-padding">
                            <asp:GridView ID="grd_Emp" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                OnRowDataBound="grd_Emp_RowDataBound" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="EmpName">
                                        <ItemTemplate>
                                            <asp:Label ID="txtdetails" ReadOnly="true" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Code">
                                        <ItemTemplate>
                                            <div style="text-align: center;">
                                                <asp:Label ID="lblmachineid" ReadOnly="true" runat="server" Text='<%# Eval("MachineID") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Device Code">
                                        <ItemTemplate>
                                            <div style="text-align: center;">
                                                <asp:Label ID="lblempid" ReadOnly="true" runat="server" Text='<%# Eval("EmployeeNo") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
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
                                            <asp:Label ID="lblDOJ" ReadOnly="true" runat="server" Text='<%# Eval("DOJ","{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" SortExpression="DeptName">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldept" ReadOnly="true" runat="server" Text='<%# Eval("DeptName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" SortExpression="DesigName">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldept" ReadOnly="true" runat="server" Text='<%# Eval("DesigName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAN Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpan" ReadOnly="true" runat="server" Text='<%# Eval("PanNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Passport Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPassport" ReadOnly="true" runat="server" Text='<%# Eval("PassportNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRelivingStatus" ReadOnly="true" runat="server" Text='<%# Eval("RelivingStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

