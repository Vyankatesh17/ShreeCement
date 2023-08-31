<%@ Page Title="Employee Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EmployeeReport.aspx.cs" Inherits="EmployeeReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function PrintGridData(str) {
            var prtGrid = document.getElementById('<%= grd_Emp.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
    </script>


    <div class="row">
        <asp:Panel ID="pann" runat="server" DefaultButton="btnsearch">
            <div class="box box-primary">

                <div class="box-body">
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
                        Employee Status
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                            <asp:ListItem Value="0">Relieving</asp:ListItem>
                            <asp:ListItem Value="1">Working</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4 form-group">
                        <div style="margin-top: 18px;">
                            <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" OnClick="btnsearch_Click"
                                Text="Search" />

                            <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" OnClick="btncancel_Click"
                                Text="Cancel" />

                            <asp:Button ID="btnPrint" runat="server" CssClass="btn bg-blue-active"
                                Text="Print" OnClick="PrintAllPages" />

                            <asp:Button ID="btnExpPDF" runat="server" CssClass="btn bg-blue-active" OnClick="btnExpPDF_Click"
                                Text="Export to PDF" />

                            <asp:Button ID="btnExpExcel" runat="server" CssClass="btn bg-blue-active"
                                Text="Export to Excel" OnClick="btnExpExcel_Click" />
                        </div>
                    </div>
                    <div class="form-group">






                        <table id="tbldisp" runat="server" width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pan" runat="server">
                                        <div style="margin-left: 850px">
                                            <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                            </asp:Label>&nbsp;&nbsp;
                                                                                      <asp:Label ID="lblcnt" runat="server"></asp:Label>
                                        </div>
                                        <asp:GridView ID="grd_Emp" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" ShowFooter="false"
                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" CssClass="table table-bordered table-striped"
                                            OnPageIndexChanging="grd_Emp_PageIndexChanging" AllowPaging="true"
                                            PageSize="10" OnSelectedIndexChanged="grd_Emp_SelectedIndexChanged" Caption="Employee Report">
                                            <AlternatingRowStyle BackColor="White" />
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
                                                <asp:TemplateField HeaderText="Employee ID">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblempid" ReadOnly="true" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemailid" ReadOnly="true" runat="server" Text='<%# Eval("EMailID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="contact Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcntct" ReadOnly="true" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Of Joining">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDOJ" ReadOnly="true" runat="server" Text='<%# Eval("DOJ","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department" SortExpression="DeptName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldept" ReadOnly="true" runat="server" Text='<%# Eval("DeptName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PAN Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpan" ReadOnly="true" runat="server" Text='<%# Eval("PanNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Passport Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPassport" ReadOnly="true" runat="server" Text='<%# Eval("PassportNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRelivingStatus" ReadOnly="true" runat="server" Text='<%# Eval("RelivingStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Data Exist.....!!!!!!
                                            </EmptyDataTemplate>
                                            <FooterStyle />
                                            <HeaderStyle />
                                            <PagerStyle HorizontalAlign="Right" />
                                            <RowStyle />
                                            <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle />
                                            <SortedAscendingHeaderStyle />
                                            <SortedDescendingCellStyle />
                                            <SortedDescendingHeaderStyle />
                                        </asp:GridView>
                                    </asp:Panel>

                                </td>

                            </tr>
                        </table>


                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

