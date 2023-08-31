<%@ Page Title="View Employee Roster" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="view_employee_roster.aspx.cs" Inherits="view_employee_roster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="box">
                <div class="box-header box-solid">
                    <h3 class="box-title">View Employee Roster</h3>
                    <div class="box-tools">
                        <a href="mst_roster_list.aspx" class="btn btn-warning btn-sm">Back to Roster List</a>
                    </div>
                </div>
                <div class="box-body no-padding">
                   
                    <asp:DataList ID="datalistdisplay" DataKeyField="Day" runat="server" Width="100%" RepeatDirection="Horizontal" GridLines="Both" RepeatColumns="11">
                        <ItemTemplate>
                            <table class="table table-responsive table-striped">
                                <tr style="background-color: gray;">
                                    <td>
                                        <asp:Label ID="lbldays" runat="server" Text='<%# Eval("Day") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

