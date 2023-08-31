<%@ Page Title="Training Approval" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ApprovalFormTraining.aspx.cs" Inherits="ApprovalFormTraining" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <section class="content">
        <!-- left column -->
        <div class="col-md-12">
            <div class="box box-primary">
                &nbsp;
                <table align="center">
                    <tr>
                        <td align="left">
                            <div class="form-group">Status&nbsp;&nbsp; </div>
                        </td>

                        <td align="right">
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:DropDownList ID="rdpending" runat="server" AutoPostBack="True"
                                        CssClass="form-control" OnSelectedIndexChanged="rdpending_SelectedIndexChanged">
                                        <asp:ListItem Text="Pending" />
                                        <asp:ListItem Text="Approved" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pann" runat="server">
                    <div style="margin-left: 940px">
                        <b>
                            <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>
                            <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                    </div>
                    <asp:GridView ID="grd_Training" runat="server" BorderStyle="None" AutoGenerateColumns="false"
                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-bordered table-striped">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="name" HeaderText="Employee" />
                            <asp:BoundField DataField="DeptName" HeaderText="Department Name" />
                            <asp:BoundField DataField="TrainingDate" HeaderText="Training Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" />
                            <asp:BoundField DataField="TrainingTime" HeaderText="Training Time" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Trainer" HeaderText="Trainer" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Edit" runat="server" Text="Approved" OnClick="OnClick_Edit" CommandArgument='<%# Eval("trainID") %>'
                                        CssClass="linkbutton1" />
                                    |
                                                          <asp:LinkButton ID="LeaveDenie" runat="server" Text="Denied" CommandArgument='<%# Eval("trainID") %>'
                                                              CssClass="linkbutton1" OnClick="LeaveDenie_Click" />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
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
                            No data Exists!!!!!!!!!!!!!!!!
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </section>


</asp:Content>

