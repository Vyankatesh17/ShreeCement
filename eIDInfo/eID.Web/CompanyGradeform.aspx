<%@ Page Title="Company Grade" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="CompanyGradeform.aspx.cs" Inherits="CompanyGradeform" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updd" runat="server">
      <%--  <Triggers>
            <asp:PostBackTrigger ControlID="btnsubmit" />
        </Triggers>--%>
        <ContentTemplate>
            
            <table width="100%">

                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server"
                            ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <asp:Panel ID="pannn" runat="server" DefaultButton="btnadd">
                                    <table width="100%">
                                        <tr>
                                            <td style="padding-left: 18px">
                                                <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                                    Text="Add New" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <br />
                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-body">
                                            <div class="form-group">

                                                <table width="100%">
                                                    <tr>
                                                        <td align="right">
                                                            <b>
                                                                <asp:Label ID="lbl1" runat="server" Text="No. of Count :"> </asp:Label>

                                                                <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grd_companyGrad" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="grd_companyGrad_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                                                    <asp:BoundField DataField="Grade_Type" HeaderText="Bonus Type" />
                                                                    <asp:BoundField DataField="Grade_In_Percentage" HeaderText="Bonus In %" />
                                                                    <asp:BoundField DataField="Grade_In_Value" HeaderText="Bonus In Value" />

                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%# Eval("Grade_ID") %>'
                                                                                Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" ToolTip="Edit" OnClick="Edit_Click" />

                                                                            <%--<asp:LinkButton Text="Edit" runat="server" CommandArgument='<%#Eval("CountryId")%>' ID="lnk" OnClick="Unnamed1_Click" />--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <FooterStyle />
                                                                <HeaderStyle />
                                                                 <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                                              <%--  <PagerStyle HorizontalAlign="Right" />--%>
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
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                                    <section class="content-header">
                                        <h3>Bonus Information </h3>

                                    </section>
                                    <section class="content">

                                        <!-- left column -->
                                        <div class="col-md-7">

                                            <div class="box box-primary">

                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <%-- <asp:Panel ID="PnlAdd" runat="server" Width="500px">--%>
                                                            <table width="100%">
                                                                <caption>
                                                                    <br />
                                                                    <tr>
                                                                        <td valign="top">Company:<span style="color: red;"> *</span></td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" TabIndex="1"
                                                                                AutoPostBack="True">
                                                                            </asp:DropDownList>

                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" SetFocusOnError="true"
                                                                                ControlToValidate="ddlcompany" ForeColor="Red"
                                                                                ErrorMessage="*" Operator="NotEqual" ValidationGroup="w"
                                                                                ValueToCompare="--Select--"></asp:CompareValidator>
                                                                            <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                                            TargetControlID="CompareValidator1">
                                                                        </asp:ValidatorCalloutExtender>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">Grade:<span style="color: red;"> *</span> </td>
                                                                        <td>
                                                                           
                                                                              <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control" TabIndex="2"
                                                                                AutoPostBack="True">
                                                                            </asp:DropDownList>

                                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" SetFocusOnError="true"
                                                                                ControlToValidate="ddlGrade" ForeColor="Red"
                                                                                ErrorMessage="*" Operator="NotEqual" ValidationGroup="w"
                                                                                ValueToCompare="--Select--"></asp:CompareValidator>
                                                                            <asp:Label ID="lblEditid" runat="server" Visible="False"></asp:Label>
                                                                          
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" style="width: 230px">Grade Type: </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rd_gradeType" runat="server" RepeatDirection="Horizontal" TabIndex="3"
                                                                                OnSelectedIndexChanged="rd_gradeType_SelectedIndexChanged" AutoPostBack="True">
                                                                                <asp:ListItem Selected="True" Text="% Of Basic" Value="0">
                                                                                </asp:ListItem>
                                                                                <asp:ListItem Text="% Of Gross" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Absolute Value" Value="2">
                                                                                </asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" visible="false" id="trgradePercentage">
                                                                        <td valign="top" style="width: 230px">Grade In %:<span style="color: red;"> *</span> </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtgradeInPercentage" runat="server" CausesValidation="True" MaxLength="4"
                                                                                CssClass="form-control" placeholder="Enter Grade Percentage" TabIndex="4"
                                                                                ValidationGroup="w" AutoPostBack="True" OnTextChanged="txtgradeInPercentage_TextChanged"></asp:TextBox>

                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                                TargetControlID="txtgradeInPercentage" ValidChars="0123456789.">
                                                                            </asp:FilteredTextBoxExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtgradeInPercentage"
                                                                                ErrorMessage="*" ValidationGroup="w" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtgradeInPercentage"
                                                                                Operator="DataTypeCheck" Type="Double" ErrorMessage="Please Enter Correct Percentage Value"
                                                                                ForeColor="Red" ValidationGroup="w" />
                                                                        </td>
                                                                    </tr>

                                                                    <tr runat="server" visible="false" id="trgradevalue">
                                                                        <td valign="top" style="width: 230px">Grade In Value:<span style="color: red;"> *</span> </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtgradevalue" runat="server" CausesValidation="True" MaxLength="7" CssClass="form-control"
                                                                                placeholder="Enter Grade Value" ValidationGroup="w" AutoPostBack="True" TabIndex="5"
                                                                                OnTextChanged="txtgradevalue_TextChanged"></asp:TextBox>

                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="txtgradevalue" ValidChars="0123456789.">
                                                                            </asp:FilteredTextBoxExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtgradevalue"
                                                                                ErrorMessage="*" ValidationGroup="w" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            <asp:CompareValidator ID="intValidator" runat="server" ControlToValidate="txtgradevalue"
                                                                                Operator="DataTypeCheck" Type="Double" ErrorMessage="Please Enter Correct Value"
                                                                                ForeColor="Red" ValidationGroup="w" />
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td valign="top">Status: </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rd_status" runat="server" RepeatDirection="Horizontal" TabIndex="6">
                                                                                <asp:ListItem Selected="True" Text="Active" Value="0">
                                                                                </asp:ListItem>
                                                                                <asp:ListItem Text="Inactive" Value="1">
                                                                                </asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td>
                                                                            <asp:Button ID="btnsubmit" runat="server" TabIndex="7" CssClass="btn bg-blue-active" 
                                                                                OnClick="btnsubmit_Click" Text="Save" ValidationGroup="w" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btncancel" runat="server" TabIndex="8" CssClass="btn bg-blue-active" OnClick="btncancel_Click" Text="Cancel" />
                                                                        </td>
                                                                    </tr>
                                                                </caption>
                                                            </table>
                                                            <%--  </asp:Panel>--%>
                                                            <%-- <asp:RoundedCornersExtender ID="rn" runat="server" TargetControlID="PnlAdd" Radius="10"
                                                Corners="All" BorderColor="#333">
                                            </asp:RoundedCornersExtender>--%>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </div>
                                        </div>
                                    </section>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

