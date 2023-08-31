<%@ Page Title="Clearance Certificate" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ClearanceCertificateFinal.aspx.cs" Inherits="ClearanceCertificateFinal" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>



            <asp:MultiView ID="MultiView1" runat="server">



                <asp:View ID="View1" runat="server">


                   
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <table width="100%">
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="overflow: scroll; height: 500px; width:97%">
                                                <asp:GridView ID="grdalldata" runat="server" BorderStyle="none" AutoGenerateColumns="false"
                                                    Width="100%"
                                                    CssClass="table table-bordered table-striped">
                                                    <AlternatingRowStyle BackColor="white" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" Visible="false" />
                                                        <asp:BoundField DataField="Ename" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="DH" HeaderText="Department Head" />
                                                        <asp:BoundField DataField="DeparmentAllStatus" HeaderText="DeparmentAllStatus" />
                                                        <asp:BoundField DataField="AF" HeaderText="Accounts & Finance" />
                                                        <asp:BoundField DataField="AccountsAllStatus" HeaderText="AccountsAllStatus" />
                                                        <asp:BoundField DataField="admin" HeaderText="Admin" />
                                                        <asp:BoundField DataField="AdminAllStatus" HeaderText="AdminAllStatus" />
                                                        <asp:BoundField DataField="IT" HeaderText="IT" />
                                                        <asp:BoundField DataField="ITAllStatus" HeaderText="ITAllStatus" />
                                                        <asp:BoundField DataField="HR" HeaderText="HR" />
                                                        <asp:BoundField DataField="HRAllStatus" HeaderText="HRAllStatus" />

                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkApproved" runat="server" Text="Print" CommandArgument='<%# Eval("EmployeeID") %>'
                                                                    OnClick="lnkApproved_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        no data exists!!!!!!!!!!!!!!!!
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    
                </asp:View>

            </asp:MultiView>

            </fieldset>
            <uc1:Time ID="modpop" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

