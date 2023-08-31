<%@ Page Title="Salary Process" Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMaster.master"
    CodeFile="Salaryprocess.aspx.cs" Inherits="Salaryprocess" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .style1 {
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View ID="View1" runat="server">
                                        <fieldset id="field">
                                            <%-- <fieldset>
                            <legend>Salary Details</legend>--%>
                                            <table width="100%" cellspacing="8px">
                                                <tr>
                                                    <td>
                                                        <table width="100%" class="bordered" style="border: none;">
                                                            <tr>
                                                                <td></td>
                                                                <td>Select Company
                                                                    <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                                                    <br />
                                                                    <asp:DropDownList CssClass="form-control" ID="ddlCompany" runat="server" AutoPostBack="True" 
                                                                        OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                                    </asp:DropDownList>


                                                                    <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddlCompany" Display="Dynamic"
                                                                        ErrorMessage="" Operator="NotEqual" ValidationGroup="b" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>


                                                                </td>
                                                                <td>Department
                                                                   <br />
                                                                    <asp:DropDownList CssClass="form-control" ID="ddldepartment" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>

                                                                <td>Designation
                                                                   <br />
                                                                    <asp:DropDownList CssClass="form-control" ID="ddldesignation" runat="server" AutoPostBack="True" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>&nbsp;&nbsp;&nbsp;Select Month
                                                    <br />
                                                                    <asp:DropDownList CssClass="form-control" ID="ddlMonths" runat="server" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged" >
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
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>&nbsp;&nbsp;&nbsp; Select Year
                                                    <br />
                                                                    <asp:DropDownList CssClass="form-control" ID="ddlYears" runat="server" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>


                                                                    <div style="margin-top: 15px;">
                                                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" Text="Search" OnClick="btnsearch_Click" />

                                                                    </div>

                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>

                                            <fieldset>

                                                <table width="100%">

                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;Select All 
                                                               <br />
                                                            <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" CssClass="form-control"  />
                                                        </td>
                                                        <td align="right">

                                                            <b>
                                                                <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                                                </asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblcnt" runat="server">
                                            </asp:Label></b>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="3">

                                                            <asp:GridView ID="grd_Emp" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                BorderWidth="1px" ForeColor="Black" GridLines="Vertical" CssClass="table table-bordered table-striped" AllowPaging="true"
                                                                PageSize="10" OnPageIndexChanging="grd_Emp_PageIndexChanging">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Checkbox">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkemp" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtdetails" ReadOnly="true" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employee ID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <div style="text-align: center;">
                                                                                <asp:Label ID="lblempid" ReadOnly="true" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="EmployeeId" HeaderText="Employee ID" Visible="false" />
                                                                    <asp:TemplateField HeaderText="GrossSalary">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblemailid" ReadOnly="true" runat="server" Text='<%# Eval("GrossSalary") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Deduction">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcntct" ReadOnly="true" runat="server" Text='<%# Eval("deductions") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Earning">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcntct1" ReadOnly="true" runat="server" Text='<%# Eval("Earning") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Net Pay">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDOJ" ReadOnly="true" runat="server" Text='<%# Eval("netpay") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="View">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="Edit123" runat="server" Text="View Details" CommandArgument='<%# Eval("EmployeeId") %>'
                                                                                OnClick="Edit_Click" Visible='<%# (Convert.ToString(Eval("vis"))!="")?Convert.ToBoolean("true"):Convert.ToBoolean("false") %>' />
                                                                            <asp:Label ID="lblDateField" runat="server" Text='Not Processed' Visible='<%# (Convert.ToString(Eval("vis"))!="")?Convert.ToBoolean("false"):Convert.ToBoolean("true") %>'>
                                                                            </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>No Record Exists....!!</EmptyDataTemplate>
                                                            </asp:GridView>

                                                        </td>
                                                    </tr>
                                                </table>


                                            </fieldset>
                                            <div>
                                                <div style="margin-top: 15px; float: right;">
                                                    <asp:Button ID="BtnProcess" runat="server" CssClass="btn bg-blue-active" Text="Process" OnClick="BtnProcess_Click" />

                                                </div>
                                            </div>
                                        </fieldset>
                                        <table width="600px" id="tblpopup" style="background-color: Silver;">
                                            <tr>
                                                <td align="center" style="font-family: 'COMic Sans MS'; font-size: large;">
                                                    <div style="text-align: right; height: 30px; float: right;">
                                                        <asp:ImageButton ID="imgclose" runat="server" ImageUrl="~/Images/Close.jpg" Height="30px"
                                                            Width="30px" />
                                                    </div>
                                                    Deduction Details :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="overflow: scroll; height: 150px;">
                                                        <asp:GridView ID="grddeduction" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" Width="100%" BackColor="White">
                                                            <Columns>
                                                                <asp:BoundField DataField="ComponentType" HeaderText="ComponentType" />
                                                                <asp:BoundField DataField="Componentid" HeaderText="Component Name" />
                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="font-family: 'COMic Sans MS'; font-size: large;">Earning Details :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="overflow: scroll; height: 150px;">
                                                        <asp:GridView ID="grdearning" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" Width="100%" BackColor="White">
                                                            <Columns>
                                                                <asp:BoundField DataField="ComponentType" HeaderText="ComponentType" />
                                                                <asp:BoundField DataField="Componentid" HeaderText="Component Name" />
                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                        <asp:ModalPopupExtender ID="mod1" runat="server" TargetControlID="lbl" CancelControlID="imgclose"
                                            PopupControlID="tblpopup">
                                        </asp:ModalPopupExtender>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <uc1:Time ID="modpop" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
