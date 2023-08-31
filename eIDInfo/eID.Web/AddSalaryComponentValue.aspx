<%@ Page Title="Salary Component" Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMaster.master" CodeFile="AddSalaryComponentValue.aspx.cs" Inherits="AddSalaryComponent" %>


<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div class="col-md-12">
                        <div style="position: relative; left: 0px; top: 0px;" class="box box-info">
                            <div style="cursor: move; border-bottom: 2px solid #C1C1C1" class="box-header">
                                <i class="fa fa-list"></i>
                                <h3 class="box-title">Salary Component List</h3>
                                <!-- tools box -->
                                <div class="pull-right box-tools">
                                    <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                        Text="Add New"  Visible="false"/>
                                </div>
                                <!-- /. tools -->
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="pull-right">
                                            <b>
                                                <asp:Label ID="lbl1" runat="server" Text="No. of Records :">
                                                </asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblcnt" runat="server">
                                            </asp:Label></b>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:GridView ID="grd_Dept" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-striped" OnPageIndexChanging="grd_Dept_PageIndexChanging" AllowPaging="true" PageSize="10">
                                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ComponentName" HeaderText="Component Name" />
                                                <asp:BoundField DataField="ComponentType" HeaderText="Component Type" />
                                                <asp:BoundField DataField="PercentageValue" HeaderText="Percentage Value" />
                                                <asp:BoundField DataField="FixedValue" HeaderText="Fixed Value" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgview" runat="server" ImageUrl="~/Images/i_edit.png" CommandArgument='<%# Eval("Componentid") %>'
                                                            OnClick="OnClick_Edit" ToolTip="Edit" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Data Exists!!!!
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="col-md-12">
                        <div style="position: relative; left: 0px; top: 0px;" class="box box-primary">
                            <div style="cursor: move; border-bottom: 2px solid #C1C1C1" class="box-header">
                                <i class="fa fa-filter"></i>
                                <h3 class="box-title">Salary Component Info</h3>
                                <!-- tools box -->
                                <div class="pull-right box-tools">
                                </div>
                                <!-- /. tools -->
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td>Type :</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" Enabled="false" required>
                                                        <asp:ListItem>Earning</asp:ListItem>
                                                        <asp:ListItem>Deduction</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Component Name :<asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlComponentType" runat="server" CssClass="form-control" Enabled="false">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddlComponentType" Display="Dynamic"
                                                        ErrorMessage="*" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>

                                                    <br />

                                                    <asp:TextBox ID="txtCompname" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtCompname" Display="None"
                                                        ErrorMessage="Please Enter Component Name" SetFocusOnError="True"
                                                        ValidationGroup="S">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                    </asp:ValidatorCalloutExtender>
                                                    <asp:Label ID="lbldeptid" runat="server" Visible="False">
                                                                
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Value Type :
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rb_Value_Type" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" OnSelectedIndexChanged="rb_Value_Type_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Fixed</asp:ListItem>
                                                        <asp:ListItem Value="1">Percentage</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <br />

                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Value :<asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtvalue" runat="server" CssClass="form-control" placeholder="Enter Value">
                                                    </asp:TextBox>
                                                    <asp:DropDownList ID="ddlComponentType1" runat="server" CssClass="form-control" Visible="False">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtvalue" Display="Dynamic"
                                                        ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="red">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td>
                                                    <br />
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                        CssClass="btn bg-blue-active" />
                                                    &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
