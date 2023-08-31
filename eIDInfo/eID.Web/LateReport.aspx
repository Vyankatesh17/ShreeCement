<%@ Page Title="Late Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="LateReport.aspx.cs" Inherits="LateReport" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>

            <table width="100%">
                <tr>
                    <td>
                        <div class="col-md-12">
                            <asp:Panel ID="pann" runat="server" DefaultButton="btnshow">
                                <div class="box box-primary">

                                    <div class="box-body">
                                        <div class="form-group">
                                            <table width="50%">

                                                <tr>
                                                    <td>Month
                                                    </td>
                                                    <td>Year
                                                    </td>
                                                    <td>Select Company
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldept" runat="server" Text="Select Department"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblemp" runat="server" Text="Select Employee"></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td valign="top">

                                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="200px" CssClass="form-control">


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

                                                    <td valign="top">

                                                        <asp:DropDownList ID="ddlYear" runat="server" Width="200px" CssClass="form-control" CausesValidation="True" ValidationGroup="S">                                                        
                                                        </asp:DropDownList>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlYear"
                                                            Display="None" ErrorMessage="Select Year" Operator="NotEqual"
                                                            ValidationGroup="S" ValueToCompare="--Select--"></asp:CompareValidator>

                                                        <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender"
                                                            runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                                        </asp:ValidatorCalloutExtender>


                                                    </td>
                                                    <td valign="top">
                                                        <asp:DropDownList ID="ddCompnay" runat="server" Width="200px" AutoPostBack="True" CssClass="form-control"
                                                            OnSelectedIndexChanged="ddCompnay_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:DropDownList ID="dddep" runat="server" Width="200px" AutoPostBack="True" CssClass="form-control"
                                                            OnSelectedIndexChanged="dddep_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <br />

                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtemployee" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="txtemployee_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                                            Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtemployee"
                                                            CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmployee">
                                                        </asp:AutoCompleteExtender>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnshow" runat="server" CssClass="btn bg-blue-active" Text="Search" ValidationGroup="S"
                                                            OnClick="btnshow_Click"  />

                                                        <asp:Button ID="btnprint" runat="server" CssClass="btn bg-blue-active" OnClick="btnprint_Click"
                                                            Text="Print" ValidationGroup="a" />
                                                        <asp:Label ID="lblempid" runat="server" Visible="false"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                       

                            <table width="100%">
                                <tr>
                                    <td class="style1">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="No Of Data : ">
                                        </asp:Label>
                                        <asp:Label ID="lblcount" Font-Bold="True" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="max-height: 400px;">
                                            <asp:Panel ID="pnl" runat="server" Width="90%" cellpadding="3" cellspacing="3">
                                                <asp:GridView ID="grd_LateCommerce" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                                                    CssClass="table table-bordered table-striped" Caption="Late-comers Report" AllowPaging="true" OnPageIndexChanging="grd_LateCommerce_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DeptName" HeaderText="Department Name"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EmployeeNo" HeaderText="Employee Id"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="LateMark Count">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="upd12" runat="server">
                                                                    <Triggers>
                                                                        <%--<asp:PostBackTrigger ControlID="lnkcount" />--%>
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        
                                                                           <%#Eval("LateMarkCount")%>
                                                                           <%--<asp:LinkButton ID="lnkcount" runat="server" CommandArgument='<%#Eval("EmployeeId")%>' Text='<%#Eval("LateMarkCount")%>' OnClick="lnkcount_Click"></asp:LinkButton></u>--%>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Year">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="upd13" runat="server">
                                                                    <Triggers>
                                                                        <%--<asp:PostBackTrigger ControlID="lnkyear" />--%>
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        
                                                                            <%#Eval("year")%>
                                                                            <%--<asp:LinkButton ID="lnkyear" runat="server" CommandArgument='<%#Eval("EmployeeId")%>' Text='<%#Eval("year")%>' OnClick="lnkyear_Click"></asp:LinkButton></u>--%>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle HorizontalAlign="Center" BackColor="Wheat" Font-Bold="true" />
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <EmptyDataTemplate>
                                                        No Data Exist!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                                             </div>
                                    </div>
                                </div>
                            </asp:Panel>
                    </td>

                </tr>
            </table>


            <table id="Table1" runat="server" visible="false" class="modalPopup" width="600px" style="border-radius: 0 0 20px 20px;display:none; overflow: scroll; box-shadow: 10px 10px 10px; background-color: #F0F2ED; height: 300px">
                <tr style="background-color: #CFA071">
                    <td align="center" style="color: Black; background-color: White;">
                        <div style="text-align: right; height: 30px; float: right;">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Close.jpg" Height="25px"
                                Width="25px" />
                        </div>
                        <div style="font-family: 'COMic Sans MS'; font-size: large;">Late-comers Details</div>
                        <div style="max-height: 530px; overflow: scroll;">
                            <div class="col-md-12" style="max-height: 530px;">
                                <div class="box box-primary">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="5">
                                                        <asp:GridView ID="Grd_LateComersEmp" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                            AllowPaging="false" PageSize="6"
                                                            CellPadding="4" ForeColor="Black" 
                                                             CssClass="mGrid"
                                                            Width="100%">

                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Log_Date_Time" HeaderText="Log Date" DataFormatString="{0:dd/MM/yyyy}"
                                                                    ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CompanyName" HeaderText="Company"
                                                                    ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DeptName" HeaderText="Department"
                                                                    ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DesigName" HeaderText="Designation"
                                                                    ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Intime" HeaderText="In Time" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Outtime" HeaderText="Out Time" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ActIntime" HeaderText="Actual In Time" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ActOuttime" HeaderText="Actual Out Time" ItemStyle-HorizontalAlign="Center" Visible="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LateMins" HeaderText="Late Minutes" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblpopupid" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="modnopo" runat="server" TargetControlID="lblpopupid"
                PopupControlID="Table1">
            </asp:ModalPopupExtender>
     <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

