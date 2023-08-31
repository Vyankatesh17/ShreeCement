<%@ Page Title="Employee Documents" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="EmployeeDocument.aspx.cs" Inherits="EmployeeDocument" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">

        <ContentTemplate>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <asp:Panel ID="pnal" runat="server" DefaultButton="BtnAddDocument">
                                <table width="100%" cellspacing="8px">
                                    <tr>
                                        <td colspan="5">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Company :<span style="color: red;"> *</span></td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" Width="320px"
                                                AutoPostBack="True" TabIndex="1">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="ddlCompany"
                                                Display="None" ErrorMessage="Select Company" Operator="NotEqual"
                                                ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                Enabled="True" TargetControlID="cmp2">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                        <td valign="top" style="padding-left: 2%">Department:&nbsp;
                                                            <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="*">
                                                            </asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddldept" CssClass="form-control" runat="server" AutoPostBack="True" Width="320px"
                                                OnSelectedIndexChanged="dddept_SelectedIndexChanged" TabIndex="2">
                                            </asp:DropDownList>
                                            &nbsp;
                                                            <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddldept" Display="None"
                                                                ErrorMessage="Select Department" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"
                                                                SetFocusOnError="True">
                                                            </asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True"
                                                TargetControlID="cmp1">
                                            </asp:ValidatorCalloutExtender>

                                        </td>

                                    </tr>
                                    <%-- <tr>
                                        
                                    </tr>--%>

                                    <tr>
                                        <td valign="top">Employee  :<span style="color: red;"> *</span></td>
                                        <td colspan="2" valign="top">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" Width="320px" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" ErrorMessage="Select Employee" Operator="NotEqual"
                                                ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                Enabled="True" TargetControlID="CompareValidator1">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                        <td valign="top" style="padding-left: 2%">Document Name :<span style="color: red;"> * </span>
                                            &nbsp;</td>
                                        <td colspan="2" valign="top">
                                            <asp:DropDownList ID="ddldocument" runat="server" CssClass="form-control" Width="320px" TabIndex="4"></asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddldocument" Display="None"
                                                ErrorMessage="Select Document" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"
                                                SetFocusOnError="True">
                                            </asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                                TargetControlID="CompareValidator2">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                       
                                    </tr>--%>
                                    <tr>
                                        <td valign="top">Description :<span style="color: red;"> *</span>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtdescrip" runat="server" CssClass="form-control" TextMode="MultiLine" Width="320px" TabIndex="5" Style="resize:none"></asp:TextBox>
                                        </td>
                                        <td valign="top" style="padding-left: 2%">Attachment :<span style="color: red;"> *</span>
                                        </td>
                                        <td valign="top">
                                            <asp:FileUpload ID="FileUploadDocu" runat="server"
                                              CssClass="form-control" Width="320px"  TabIndex="6"/>
                                            <asp:Label ID="lblpath" runat="server"></asp:Label>
                                        </td>
                                        <td valign="top">

                                            
                                            <asp:Button ID="BtnAddDocument" runat="server" CssClass="btn bg-blue-active"
                                                Text="Add" ValidationGroup="a" OnClick="BtnAddDocument_Click" TabIndex="7" />
                                            <asp:UpdatePanel ID="ww" runat="server">
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="BtnAddDocument" />
                                                </Triggers>
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            &nbsp;
                                        </td>
                                    </tr>
                                  <tr>
                                        <td colspan="5">
                                            <asp:GridView ID="GridViewUpload" runat="server" AllowPaging="true"
                                                AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" PageSize="5"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Document_ID" HeaderText="Document_ID"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="DocumentName" HeaderText="Document Name" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                    <asp:BoundField DataField="Documentpath" HeaderText="Document Path" />
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" ToolTip="Edit" OnClick="imgExpedit_Click" />

                                                            <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" ToolTip="Delete" OnClick="imgExpdelete_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle />
                                                <HeaderStyle />
                                                <PagerStyle HorizontalAlign="left" />
                                                <RowStyle />
                                                <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle />
                                                <SortedAscendingHeaderStyle />
                                                <SortedDescendingCellStyle />
                                                <SortedDescendingHeaderStyle />
                                                <RowStyle CssClass="odd" />
                                                <AlternatingRowStyle CssClass="even" />
                                            </asp:GridView>


                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>

                                        <td colspan="5" align="center">
                                            <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click"
                                                CssClass="btn bg-blue-active" TabIndex="8" />
                                            &nbsp
                                                                <asp:Button ID="Button1" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" TabIndex="9" />
                                            <br />
                                        </td>
                                    </tr>

                                </table>
                                    </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

             


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

