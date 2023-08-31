<%@ Page Title="Birthday List" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="birthday_list.aspx.cs" Inherits="birthday_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="container-fluid">
            <div class="box box-default color-palette-box">
                <div class="box-header">
                    <h3 class="box-title">Birthday List</h3>
                </div>
                <div class="box-body">
                    <asp:GridView ID="gvBirthdayList" runat="server" CssClass="table table-bordered table-striped table-responsive"
                        AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvBirthdayList_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Emp Name" DataField="EmpName" />
                            <asp:BoundField HeaderText="Emp No" DataField="MAchineID"/>
                            <asp:BoundField HeaderText="Device Code" DataField="EmpNo"/>
                            <asp:BoundField HeaderText="BirthDate" DataField="BirthDate" />
                            <asp:BoundField HeaderText="Department" DataField="DeptName" />
                            <asp:BoundField HeaderText="Desgnation" DataField="DesigName" />
                        </Columns>
                        <EmptyDataTemplate>
                            no data found...
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
</asp:Content>

