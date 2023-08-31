<%@ Page Title="Monthly Roster Details" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="mst_monthly_roster_list.aspx.cs" Inherits="mst_monthly_roster_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div class="col-md-12">
                        <div style="position: relative; left: 0px; top: 0px;" class="box box-info">
                            <div style="cursor: move; border-bottom: 2px solid #C1C1C1" class="box-header">
                                <i class="fa fa-list"></i>
                                <h3 class="box-title">Roster List</h3>
                                <!-- tools box -->
                                <div class="pull-right box-tools">
                                    <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                        Text="Add New" />
                                </div>
                                <!-- /. tools -->
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="pull-right">
                                            <b>
                                                <asp:Label ID="lblrosterid" runat="server" Visible="false">
                                                </asp:Label>
                                                <asp:Label ID="lbl1" runat="server" Text="No. of Records :">
                                                </asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblcnt" runat="server">
                                            </asp:Label></b>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:GridView ID="grd_roster" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10"
                                            OnPageIndexChanging="grd_roster_PageIndexChanging">
                                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Rosterid" HeaderText="Rosterid" Visible="false" />
                                                <asp:BoundField DataField="Name" HeaderText="Employee Name" />

                                                <asp:BoundField DataField="Month" HeaderText="Month" />
                                                <asp:BoundField DataField="Year" HeaderText="Year" />
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%# Eval("Rosterid") %>'
                                                            Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="edit_Click" ToolTip="Edit" />

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
                                <h3 class="box-title">Roster Info</h3>
                                <!-- tools box -->
                                <div class="pull-right box-tools">
                                    <asp:Button ID="btncancel" runat="server" CssClass="btn bg-blue-active" OnClick="btncancel_Click" Text="Back" />
                                </div>
                                <!-- /. tools -->
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-xs-4">
                                                        <label>Company<span style="color: red;"> *</span></label>
                                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="ddlCompany" Display="Dynamic" ErrorMessage="Select Company" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="S" ValueToCompare="--Select--" CssClass="text-red"></asp:CompareValidator>
                                                        
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <label>Department <span style="color: red;">*</span></label>
                                                        <asp:DropDownList ID="ddldept" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="dddept_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddldept" Display="Dynamic" ErrorMessage="Select Department" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="S" ValueToCompare="--Select--" CssClass="text-red"></asp:CompareValidator>
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <label>Designation<span style="color: red;"> *</span></label>
                                                        <asp:DropDownList ID="ddldesg" runat="server" CssClass="form-control" OnSelectedIndexChanged="dddesg_SelectedIndexChanged" AutoPostBack="True">
                                                        </asp:DropDownList>

                                                        <asp:CompareValidator ID="cmp6" runat="server" ControlToValidate="ddldesg" Display="Dynamic" ErrorMessage="Select Designation" Operator="NotEqual" SetFocusOnError="True" ValidationGroup="S" ValueToCompare="--Select--" CssClass="text-red"></asp:CompareValidator>
                                                        
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-4">
                                                        <label>Month</label>
                                                        <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlmonth"
                                                            Display="Dynamic" ErrorMessage="Select Month" Operator="NotEqual" SetFocusOnError="True"
                                                            ValidationGroup="S" ValueToCompare="0" CssClass="text-red"></asp:CompareValidator>
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <label>Year</label>
                                                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlyear"
                                                            Display="Dynamic" ErrorMessage="Select Year" Operator="NotEqual" SetFocusOnError="True"
                                                            ValidationGroup="S" ValueToCompare="--Select--" CssClass="text-red"></asp:CompareValidator>
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <label>Employee Name</label>
                                                        <asp:HiddenField ID="HiddenFieldempid" runat="server" />
                                                        <asp:DropDownList ID="ddlemployee" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>                                                        
                                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlemployee"
                                                            Display="Dynamic" ErrorMessage="Select Employee" Operator="NotEqual" SetFocusOnError="True"
                                                            ValidationGroup="S" ValueToCompare="--Select--" CssClass="text-red"></asp:CompareValidator>                                                        
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-4"></div>
                                                    <div class="col-xs-4">
                                                        <br />
                                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn bg-blue-active" OnClick="btnsearch_Click" Text="Search" ValidationGroup="S" />
                                                    </div>
                                                    <div class="col-xs-4"></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <hr />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:DataList ID="datalistdisplay" DataKeyField="Days" runat="server" RepeatDirection="Horizontal" OnItemDataBound="datalistdisplay_ItemDataBound" GridLines="Both" RepeatColumns="11">
                                                            <ItemTemplate>
                                                                <table class="table table-responsive table-striped">
                                                                    <tr style="background-color: gray;">
                                                                        <td>
                                                                            <asp:Label ID="lbldays" runat="server" Text='<%# Eval("Days") %>' />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlist" runat="server" CssClass="form-control">
                                                                                <asp:ListItem>First</asp:ListItem>
                                                                                <asp:ListItem>Second</asp:ListItem>
                                                                                <asp:ListItem>Third</asp:ListItem>
                                                                                <asp:ListItem>WO</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-4"></div>
                                                    <div class="col-xs-4">
                                                        <div runat="server" id="div" visible="false">
                                                            <br />
                                                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn bg-blue-active" OnClick="btnsubmit_Click" Text="Save" ValidationGroup="w" />
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-4"></div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            </td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

