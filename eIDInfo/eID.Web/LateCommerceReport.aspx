<%@ Page Title="Late Comers Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="LateCommerceReport.aspx.cs" EnableEventValidation="false" Inherits="LateCommerceReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
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
                                                                <td>From Date 
                                                                </td>
                                                                <td>To Date
                                                                </td>
                                                                <td>
                                                                   Select Company
                                                                </td>
                                                                <td><asp:label ID="lbldept" runat="server" Visible="false" Text="Select Department"></asp:label>
                                                                </td>
                                                                <td><asp:label ID="lblemp" runat="server" Visible="false" Text="Select Employee"></asp:label>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtfromdate" runat="server" Width="160px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="txtfromdate_TextBoxWatermarkExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtfromdate" WatermarkText="From Date">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True" Format="MM/dd/yyyy"
                                                                        TargetControlID="txtfromdate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="rq1" runat="server" ValidationGroup="S" ControlToValidate="txtfromdate"
                                                                        Display="Static" ErrorMessage="RequiredField" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>

                                                                <td>
                                                                    <br />
                                                                    <asp:TextBox ID="txttodate" runat="server" Width="160px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="txttodate_TextBoxWatermarkExtender" WatermarkText="To Date"
                                                                        runat="server" Enabled="True" TargetControlID="txttodate">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True" Format="MM/dd/yyyy"
                                                                        TargetControlID="txttodate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="rq2" runat="server" ControlToValidate="txttodate"
                                                                        Display="Static" ErrorMessage="RequiredField" ValidationGroup="S" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    <br />
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtfromdate"
                                                                        ControlToValidate="txttodate" SetFocusOnError="True" ForeColor="#A00D11" Font-Size="Small"
                                                                        ErrorMessage="select proper date" Operator="GreaterThanEqual"
                                                                        Type="Date" ValidationGroup="a"></asp:CompareValidator>
                                                                </td>
                                                                <td>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddCompnay" runat="server" Width="250px" AutoPostBack="True" CssClass="form-control"
                            OnSelectedIndexChanged="ddCompnay_SelectedIndexChanged">
                        </asp:DropDownList>
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="dddep" runat="server" Width="180px" Visible="false" AutoPostBack="True" CssClass="form-control"
                                                                        OnSelectedIndexChanged="dddep_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <br />

                                                                </td>
                                                                <td>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddemp" runat="server" AutoPostBack="True" Visible="False" CssClass="form-control"
                            Width="170px">
                        </asp:DropDownList>
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
                                                                    <asp:Button ID="btnexport" runat="server" CssClass="btn bg-blue-active"
                                                                        Text="Export to Excel" Width="130px" OnClick="btnexport_Click"
                                                                        CausesValidation="False" />
                                                                    <asp:Button ID="btnExpPDF" runat="server" CssClass="btn bg-blue-active" OnClick="btnExpPDF_Click"
                                                                        Text="Print" ValidationGroup="a" />

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
                    <td class="style1">&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="right"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="overflow: scroll; height: 300px;">
                            <asp:Panel ID="pnl" runat="server" Width="100%" cellpadding="3" cellspacing="3">
                                <asp:GridView ID="grd_LateCommerce" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                                    CssClass="table table-bordered table-striped" Caption="Late-comers Report">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="EmployeeId" HeaderText="Employee Name"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LateMark Count" HeaderText="LateMark Count"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <center>
                                                                    <asp:ImageButton ID="imgbtnview" runat="server" CommandArgument='<%#Eval("EmployeeId")%>'
                                                                        CommandName="cmdview" ImageUrl="~/Images/View.png" BorderStyle="None" Height="30px"
                                                                        Width="30px" onclick="imgbtnview_Click"  />
                                                                </center>
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

                                                        <asp:Panel ID="PanLateCommerce" CssClass="modpann" runat="server" BackColor="LightGray" Width="70%" Height="450px" Visible="true">
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="10" align="center" width="95%" bgcolor="#CCCCCC"
                                                                class="modalPopup">
                                                                <tr>
                                                                    <td></td>
                                                                    <td style="font-family: 'Comic Sans MS'; font-size: 17px; color: #FF0000; font-weight: bold;"
                                                                        colspan="4" align="center">Late-Comers Details
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Image ID="Image5" runat="server" Height="30px" ImageUrl="~/Images/Close.jpg"
                                                                            Width="30px" />
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td colspan="6">

                                                                        <asp:Button ID="BtnPrint1" runat="server" CssClass="addButton" OnClick="BtnPrint1_Click"
                                                                            Text="Print" ValidationGroup="a" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <asp:GridView ID="Grd_LateComersEmp" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                            AllowPaging="false" PageSize="6"
                                                                            BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                                            GridLines="Both" CssClass="mGrid" ShowFooter="true"
                                                                            Width="100%" OnPageIndexChanging="Grd_LateComersEmp_PageIndexChanging" Caption="Late-comers Report">

                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:BoundField>

                                                                                <asp:BoundField DataField="date1" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Intime" HeaderText="Intime" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>

                                                                    </td>
                                                                </tr>


                                                            </table>
                                
                        </asp:Panel>
               
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                                        <asp:Label ID="Label64" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="ModelpopupLateCommerce" runat="server" Enabled="True" PopupControlID="PanLateCommerce"
                            CancelControlID="Image5" TargetControlID="Label64">
                        </asp:ModalPopupExtender>

 </asp:Panel>   
                                    </div>
                    </td>

                </tr>
            </table>

           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

