<%@ Page Title="Shiftwise In/Out Time Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="rpt_shift_in_out_time.aspx.cs" Inherits="rpt_shift_in_out_time" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h1 class="box-title">Shiftwise In / Out Time
                            </h1>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ErrorMessage="This field is required" ID="RequiredFieldValidator2" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <label class="control-label">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtFromDate" ErrorMessage="This field is required" ID="RequiredFieldValidator1" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />
                                </div>
                                <div class="col-md-2 form-group">
                                    <label class="control-label">End Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtToDate" ErrorMessage="This field is required" ID="RequiredFieldValidator5" ValidationGroup="A" Display="Dynamic" CssClass="text-red" runat="server" />

                                </div>
                                <div class="col-md-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" ValidationGroup="A" Text="Show" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                         <div class="box-body">
                            <div class="table-responsive">
                                <asp:Repeater ID="rptrTables" runat="server" OnItemDataBound="rptrTables_ItemDataBound">
                                    <HeaderTemplate>
                                        <div class="col-md-12">
                                            <div class="card text-center card-info">
                                                <div class="card-header">
                                                    <asp:Label ID="lblHeader" runat="server" CssClass="h3"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-12">
                                            <div class="card card-info">
                                                <div class="card-header with-border" style="border-bottom: 1px solid #f4f4f4 !important;">
                                                    <h4 class="box-title">Department : <%# Eval("DeptName") %>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; <span class="pull-right">Date : <%# Eval("ADate","{0:MM/dd/yyyy}") %></span></h4>
                                                    
                                                    <asp:Label ID="lblDeptId" Visible="false" CssClass="hidden" runat="server" Text='<%# Eval("DeptId") %>' />
                                                    <asp:Label ID="lblDate" Visible="false" CssClass="hidden" runat="server" Text='<%# Eval("ADate") %>' />
                                                </div>
                                                <div class="card-body no-padding">
                                                    <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="true" AllowPaging="false"
                                                        CssClass="table table-condensed table-bordered" GridLines="Both" Width="100%" OnRowDataBound="grdOrder_RowDataBound">
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="ExportToExcel" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

