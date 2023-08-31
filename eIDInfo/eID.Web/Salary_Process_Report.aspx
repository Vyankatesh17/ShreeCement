<%@ Page Title="Shree Warana Sah. Dudh Utpadak Sangh." Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Salary_Process_Report.aspx.cs" Inherits="Salary_Process_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type = "text/javascript">
           function PrintPanel() {
               var panel = document.getElementById("<%=Panel1.ClientID %>");
               var printWindow = window.open('', '', 'height=400,width=800');
               printWindow.document.write('<html><head><title>Attendance Report</title>');
               printWindow.document.write('</head><body >');
               printWindow.document.write(panel.innerHTML);
               printWindow.document.write('</body></html>');
               printWindow.document.close();
               setTimeout(function () {
                   printWindow.print();
               }, 500);
               return false;
           }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            

        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Employee Attendance Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                </div>                              
                               
                                        <div class="col-md-2 form-group">
                                            <label class="control-label">Department</label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                   
                                
                                <div class="col-md-2 form-group">
                                    <label class="control-label">Employee</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div> 
                                <div class="col-md-2 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label">Month <span class="text-red Bold">*</span></label>
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Select Month</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlMonth" ErrorMessage="This field is required" ID="RequiredFieldValidator1" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlMonth" ErrorMessage="This field is required" InitialValue="0" ID="RequiredFieldValidator2" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label">Year</label>
                                            <asp:TextBox ID="txtYear" runat="server" TextMode="Number" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="txtYear" ErrorMessage="This field is required" ID="RequiredFieldValidator3" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="txtYear" ErrorMessage="This field is required" InitialValue="0" ID="RequiredFieldValidator4" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                <div class="col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" ValidationGroup="A" Text="Show" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <Triggers>
                                       <asp:PostBackTrigger ControlID="btnExport" />                                        
                                    </Triggers>
                                    <ContentTemplate>
                        <div class="box-header with-border">                           
                            <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="ExportToExcel" />
                           <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick = "return PrintPanel();" />
                        </div>
                        <div class="box-body no-padding">
                            <div class="table-responsive">  
                                <asp:Panel runat="server" ID="Panel1">
                                    <table id="tbheader" runat="server" visible="false" style="font-size:small; font-weight:bold">
                                        <tr>
                                            <td colspan="3"  align="center">
                                                <asp:Label ID="lblCaption" runat="server" Text="Shree Warana Sah. Dudh Utpadak Sangh."></asp:Label>
                                            </td>           
                                        </tr>
                                         <tr>           
                                            <td colspan="2" align="center">
                                                Department : <asp:Label ID="lbldept" runat="server"></asp:Label>
                                            </td>
                                             <td colspan="4" align="center">
                                                Valid Punch Report For : <asp:Label ID="lbldate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                <asp:GridView ID="gvAttendance" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="gvAttendance_RowDataBound" AutoGenerateColumns="false" AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                       <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="EmployeeNo" HeaderText="Emp No" />
                                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name" />
                                            <asp:BoundField DataField="DesigName" HeaderText="Desig Name" />
                                            <asp:BoundField DataField="MonthName" HeaderText="Month Name" />
                                            <asp:BoundField DataField="ProcessYear" HeaderText="Year" />
                                            <asp:BoundField DataField="TotalDays" HeaderText="Total Days" />
                                            <asp:BoundField DataField="WorkingDays" HeaderText="Work Days" />    
                                            <asp:BoundField DataField="E_BasicSalary" HeaderText="Basic Salary" />
                                            <asp:BoundField DataField="E_HRA" HeaderText="HRA" />
                                            <asp:BoundField DataField="E_DA" HeaderText="DA" />
                                            <asp:BoundField DataField="GrossSalary" HeaderText="Gross Salary" />
                                        <asp:BoundField DataField="NetSalary" HeaderText="Net Salary" />
                                        <asp:TemplateField HeaderText="Preview">
                                            <ItemTemplate>
                                                <a href="preview_salary_slip.aspx?process_id=<%#Eval("ProcessId") %>"  target="_top"> Print</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>                                
                                    </asp:Panel> 
                            </div>
                        </div>
                                         </ContentTemplate>
                                </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        const $btnPrint = document.querySelector("#btnPrint");
        $btnPrint.addEventListener("click", () => {
            window.print();
        });
    </script>
</asp:Content>

