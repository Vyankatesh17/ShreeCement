<%@ Page Title="Leave Details" Language="C#" AutoEventWireup="true" CodeFile="~/LeaveDetails.aspx.cs" MasterPageFile="~/UserMaster.master"
    Inherits="LeaveDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-body">

                <div class="form-group">
                  



                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grd_EmpLeave" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-bordered table-striped"
                                            AllowPaging="true" PageSize="10" OnPageIndexChanging="grd_EmpLeave_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <%#Container.DataItemIndex+1 %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Leave Application Date">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblleaveappdate" ReadOnly="true" runat="server" Text='<%# Eval("LeaveApllicationDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblname" ReadOnly="true" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Type">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblleaveid" ReadOnly="true" runat="server" Text='<%# Eval("leavename") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Date">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblstartdate" ReadOnly="true" runat="server" Text='<%# Eval("StartDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Date">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblEndDate" ReadOnly="true" runat="server" Text='<%# Eval("EndDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Duration" Visible="false">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblduration" ReadOnly="true" runat="server" Text='<%# Eval("Duration") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approved Days">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblapprovedduration" ReadOnly="true" runat="server" Text='<%# Eval("AprovedDays") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purpose">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblpurpose" ReadOnly="true" runat="server" Text='<%# Eval("Purpose") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Manager Status">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblstatus" ReadOnly="true" runat="server" Text='<%# Eval("ManagerStatus") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HR Status">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Label ID="lblHRstatus" ReadOnly="true" runat="server" Text='<%# Eval("HRStatus") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>No Record Exists........!!</EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>

                      
                </div>
            </div>
        </div>
    </div>
</asp:Content>
