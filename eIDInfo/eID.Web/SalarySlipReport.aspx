<%@ Page Title="Saalry Slip Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SalarySlipReport.aspx.cs" Inherits="SalarySlipReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%-- <asp:UpdatePanel ID="utc" runat="server">
        <ContentTemplate>--%>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%">


                            <tr>
                                <section class="content-header">
                                    <caption>
                                        <div style="float: left">


                                            <h3>Salary Slip Report </h3>
                                        </div>
                                    </caption>
                                </section>
                            </tr>
                            <tr>
                                <td>
                                    <div class="col-md-12">
                                        <asp:Panel ID="pann" runat="server" DefaultButton="btnsearch">
                                            <div class="box box-primary">

                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <fieldset id="field">
                                                            <legend></legend>

                                                            <table width="65%">
                                                                <tr>
                                                                    <td>Employee Name
                                                                    </td>
                                                                    <td>Employee ID
                                                                    </td>
                                                                    <td>Month
                                                                    </td>
                                                                    <td>Year
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFirstNameSearch" runat="server" Width="270px" CssClass="form-control"></asp:TextBox>
                                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtFirstNameSearch"
                                                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetCountries"
                                                                            UseContextKey="True">
                                                                        </asp:AutoCompleteExtender>
                                                                    </td>

                                                                    <td>

                                                                        <asp:TextBox ID="txtempidsearch" runat="server" Width="270px" CssClass="form-control"></asp:TextBox>
                                                                    </td>

                                                                    <td>

                                                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="270px" CssClass="form-control"
                                                                            AutoPostBack="True">


                                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">Octomber</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="ddlMonth"
                                                                            Display="None" ErrorMessage="Select Month" Operator="NotEqual"
                                                                            ValidationGroup="S" ValueToCompare="--Select--"></asp:CompareValidator>



                                                                        <asp:ValidatorCalloutExtender ID="cmp2_ValidatorCalloutExtender" runat="server"
                                                                            Enabled="True" TargetControlID="cmp2">
                                                                        </asp:ValidatorCalloutExtender>

                                                                    </td>

                                                                    <td>

                                                                        <asp:DropDownList ID="ddlYear" runat="server" Width="270px" CssClass="form-control" CausesValidation="True" ValidationGroup="S">
                                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                                            <asp:ListItem>2012</asp:ListItem>
                                                                            <asp:ListItem>2013</asp:ListItem>
                                                                            <asp:ListItem>2014</asp:ListItem>
                                                                            <asp:ListItem>2015</asp:ListItem>
                                                                            <asp:ListItem>2016</asp:ListItem>
                                                                            <asp:ListItem>2017</asp:ListItem>
                                                                            <asp:ListItem>2018</asp:ListItem>
                                                                            <asp:ListItem>2019</asp:ListItem>
                                                                            <asp:ListItem>2020</asp:ListItem>
                                                                            <asp:ListItem>2021</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlYear"
                                                                            Display="None" ErrorMessage="Select Year" Operator="NotEqual"
                                                                            ValidationGroup="S" ValueToCompare="--Select--"></asp:CompareValidator>

                                                                        <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender"
                                                                            runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                                                        </asp:ValidatorCalloutExtender>


                                                                    </td>

                                                                </tr>
                                                            </table>

                                                            <table width="80">

                                                                <tr>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                                                    <td>
                                                                        <div style="margin-top: 15px;">
                                                                            <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" OnClick="btnsearch_Click"
                                                                                Text="Search" Width="150px" ValidationGroup="S" CausesValidation="true" />
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div style="margin-top: 15px;">
                                                                            <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel" Width="150px" OnClick="btncancel_Click" />
                                                                        </div>
                                                                    </td>
                                                                    <td>

                                                                        <div style="margin-top: 15px;">

                                                                            <asp:Button ID="btnPrintCurrent" runat="server" CssClass="btn bg-blue-active"
                                                                                Text="Cancel" OnClick="btnPrintCurrent_Click" Width="150px" Visible="False" />
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div style="margin-top: 15px;">
                                                                            <asp:Button ID="btnExpPDF" runat="server" CssClass="btn bg-blue-active" OnClick="btnExpPDF_Click"
                                                                                Text="Export to PDF" Width="150px" />
                                                                        </div>
                                                                    </td>
                                                                    <td>

                                                                        <div style="margin-top: 15px;">
                                                                            <asp:Button ID="btnExpExcel" runat="server" CssClass="btn bg-blue-active"
                                                                                Text="Export to Excel" OnClick="btnExpExcel_Click" Width="150px" />
                                                                        </div>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="float: right" align="right">

                                                                        <b>
                                                                            <div style="margin-left: 700px">
                                                                                <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>
                                                                                <asp:Label ID="lblcnt" runat="server">0</asp:Label>
                                                                        </b>
                                                    </div>
                                                    </asp:Panel>
                                        </div>
                                
                                        </td>
                            </tr>
                            <table id="tbl_t1" runat="server" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pan" runat="server">
                                            <asp:GridView ID="grd_Emp" runat="server" AllowPaging="true" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="EmployeeId" OnPageIndexChanging="grd_Emp_PageIndexChanging" OnSelectedIndexChanged="grd_Emp_SelectedIndexChanged" PageSize="10">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtdetails" runat="server" ReadOnly="true" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <div style="text-align: center;">
                                                                <asp:Label ID="lblempid" runat="server" ReadOnly="true" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Salary Account No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblemailid" runat="server" ReadOnly="true" Text='<%# Eval("SalaryAccountNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Month">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcntct" runat="server" ReadOnly="true" Text='<%# Eval("Month") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOJ" runat="server" ReadOnly="true" Text='<%# Eval("Year") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Working Days">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldept" runat="server" ReadOnly="true" Text='<%# Eval("WorkingDays") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Net payble days">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpan" runat="server" ReadOnly="true" Text='<%# Eval("Netpaybledays") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PF Account No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPassport" runat="server" ReadOnly="true" Text='<%# Eval("PFAccountNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gross Salary">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpan" runat="server" ReadOnly="true" Text='<%# Eval("GrossSalary") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Net Salary">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpan1" runat="server" ReadOnly="true" Text='<%# Eval("NetSlary") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPassport" runat="server" ReadOnly="true" Text='<%# Eval("BankName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    !!!!!!! No Data Exist !!!!!!!!!!!!!
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
                    </td>
                </tr>

            </table>
           
    <%-- </contenttemplate>
    </asp:updatepanel>--%>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
   
</asp:Content>

