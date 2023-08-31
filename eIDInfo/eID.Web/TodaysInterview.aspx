<%@ Page Title="Today Interview" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="TodaysInterview.aspx.cs" Inherits="TodaysInterview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <!-- left column -->
        <div class="col-md-12">
            <div class="box box-primary">
                &nbsp;
               
                <asp:Panel ID="pann" runat="server">
                    <div style="margin-left: 940px">
                        <b>
                            <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>
                            <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                    </div>
                    <asp:GridView ID="grd_Interview" runat="server" BorderStyle="None" AutoGenerateColumns="false" OnPageIndexChanging="grd_Interview_PageIndexChanging"
                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Candidate Name" />
                            <asp:BoundField DataField="Title" HeaderText="Position Name" />
                            <asp:BoundField DataField="FromTime" HeaderText="Schedule Time" ItemStyle-HorizontalAlign="Center"  />
                            <asp:BoundField DataField="Date" HeaderText="Schedule Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" />
                           <asp:BoundField DataField="Interviewername" HeaderText="Interview By" />
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
    

</asp:Content>

