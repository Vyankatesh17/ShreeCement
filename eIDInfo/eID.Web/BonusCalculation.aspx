<%@ Page Title="Bonus Calculation" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="BonusCalculation.aspx.cs" Inherits="BonusCalculation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updd" runat="server">
        <ContentTemplate>
            <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group col-sm-4">Select Company 
                                        <asp:Label ID="lblabsolute" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblProposed" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblporposedInc" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblTotalInc" runat="server" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control"
                                            Width="350px" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList></div>
                        <div class="form-group col-sm-4"> Frm Date : <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"   AutoPostBack="True" BackColor="White" OnTextChanged="txtfromdate_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="txtfromdate"
                                            Display="None" ErrorMessage="Enter From Date" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="req3_ValidatorCalloutExtender" runat="server" Enabled="True"
                                            TargetControlID="req3">
                                        </asp:ValidatorCalloutExtender>
                                        <asp:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txtfromdate">
                                        </asp:CalendarExtender></div>
                        <div class="form-group col-sm-4">
                                        To Date : <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control"   AutoPostBack="True" BackColor="White" OnTextChanged="txtTodate_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTodate"
                                            Display="None" ErrorMessage="Enter Date To Date" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                            TargetControlID="RequiredFieldValidator2">
                                        </asp:ValidatorCalloutExtender>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtTodate">
                                        </asp:CalendarExtender>
                            
                            
                        </div>
                        <div class="box-footer"></div>
                        <div class="box-footer no-padding">
                            <table width="100%">
                                <tr id="trchecked" runat="server" visible="false">
                                    <td>&nbsp;&nbsp;&nbsp;Select All 
                                      <br />
                                        <asp:CheckBox ID="chkall" runat="server" Checked="true" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" CssClass="form-control"  />
                                    </td>
                                    <td align="right">
                                        <b>
                                            <asp:Label ID="Label1" runat="server" Text="Today Date:"> </asp:Label>

                                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                                            &nbsp; &nbsp;
                                            <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>

                                            <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="grd_employee" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                            CssClass="table table-bordered table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Checkbox">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkemp" Checked="true" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="DeptName" HeaderText="Department" />
                                                <asp:BoundField DataField="BasicAmt" HeaderText="Basic Salary" />
                                                <asp:BoundField DataField="GrossSalary" HeaderText="Gross Salary" />
                                                <asp:BoundField DataField="Absolute" HeaderText="Asolute" />
                                                <asp:BoundField DataField="Proposed" HeaderText="Proposed" />
                                                <asp:TemplateField HeaderText="Proposed Inc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblproposedincgrd" runat="server" Text='<%#Eval("ProposedInc") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- <asp:BoundField DataField="ProposedInc" HeaderText="Proposed Inc" />--%>



                                                <asp:TemplateField HeaderText="Additional" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAdditional" AutoPostBack="true" OnTextChanged="txtAdditional_TextChanged" runat="server" Text='<%#Eval("Additional") %>'></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="flt1" runat="server" TargetControlID="txtAdditional" FilterType="Custom" ValidChars=".0123456789"></asp:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="txtAdditional" ForeColor="Red"
                                                            ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="intValidator" runat="server" ControlToValidate="txtAdditional"
                                                            Operator="DataTypeCheck" Type="Double" ErrorMessage="Please Insert Correct Value"
                                                            ForeColor="Red" ValidationGroup="S" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Inc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalIncgrd" runat="server" Text='<%#Eval("TotalInc") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="TotalInc" HeaderText="Total Inc" />--%>
                                            </Columns>
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle />
                                            <HeaderStyle />
                                            <PagerStyle HorizontalAlign="Right" />
                                            <RowStyle />
                                            <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle />
                                            <SortedAscendingHeaderStyle />
                                            <SortedDescendingCellStyle />
                                            <SortedDescendingHeaderStyle />
                                            <EmptyDataTemplate>
                                                No Data Exists....
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>

                                <tr id="trbtn" runat="server" visible="false">
                                    <td align="center" colspan="3">
                                        <asp:Button ID="btncalculate" Text="Calculate" Visible="false" runat="server" CssClass="btn bg-blue-active" ValidationGroup="S" OnClick="btncalculate_Click" />
                                        <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn bg-blue-active" OnClick="btnSave_Click"  ValidationGroup="a" />

                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                </div>
            </div>



                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

