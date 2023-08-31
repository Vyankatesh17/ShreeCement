<%@ Page Title="Process Salary List" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="salary_process_list_ngp.aspx.cs" Inherits="salary_process_list_ngp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_gvList').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvList_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <Triggers>
         <asp:PostBackTrigger ControlID="btnShow" />                                                                  
        </Triggers>
    <ContentTemplate>                         
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Processed Salary List</h3>
                            <div class="box-tools">
                                <a href="salary_process_ngp.aspx" class="btn btn-warning btn-sm">Process Salary</a>
                            </div>
                </div>
                <div class="box-body">
                    <asp:Panel runat="server" ID="Panel1" DefaultButton="btnShow">
                        <div class="row">
                            <div class="form-group col-md-2">
                                <label class="control-label">Company </label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ErrorMessage="This field is required" ID="RequiredFieldValidator3" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator4" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />

                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label">Department</label>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ControlToValidate="ddlDepartment" ErrorMessage="This field is required" ID="RequiredFieldValidator1" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="ddlDepartment" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator2" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />--%>

                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label">Employee</label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ControlToValidate="ddlEmployee" ErrorMessage="This field is required" ID="RequiredFieldValidator5" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="ddlEmployee" InitialValue="0" ErrorMessage="This field is required" ID="RequiredFieldValidator6" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />--%>
                            </div>
                             <div class="form-group col-md-2">
                                        <label class="control-label">From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtFromDate" ErrorMessage="This field is required" ID="RequiredFieldValidator7" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>
                            <div class="form-group col-md-2">
                                        <label class="control-label">To Date</label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtToDate" ErrorMessage="This field is required" ID="RequiredFieldValidator8" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                    </div>



                            <div class="form-group col-md-2" style="padding-top:25px;">
                                <label class="control-label"></label>
                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show List" ValidationGroup="A" OnClick="btnShow_Click" />
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-lg-4"></div>
                           <div class="col-lg-4">
                               <span>
                                   <b style="font-size:30px;align-content:center">
                                      Total Amount - <%= ((decimal)Context.Items["TotalAmount"]) %>
                                   </b>
                               </span>
                           </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered table-striped table-responsive" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvList_RowDataBound">
                                <columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <itemtemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Company Name" DataField="CompanyName" />
                                    <asp:BoundField HeaderText="Dept Name" DataField="DeptName" />
                                    <asp:BoundField HeaderText="Desig Name" DataField="DesigName" />
                                    <asp:BoundField HeaderText="Employee Name" DataField="EmpName" />
                                    <asp:BoundField HeaderText="Emp No" DataField="EmployeeNo" />
                                    <asp:BoundField HeaderText="From Date" DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField HeaderText="To Date" DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Per Day Salary" DataField="SalaryPerDay" />
                                    <asp:BoundField HeaderText="Present Days" DataField="PresentDays" />
                                    <asp:BoundField HeaderText="Total Salary" DataField="TotalSalary" />
                                </columns>
                            </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

