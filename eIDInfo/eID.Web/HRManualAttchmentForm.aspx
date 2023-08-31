<%@ Page Title="HR Manual Attachment" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="HRManualAttchmentForm.aspx.cs" Inherits="HRManualAttchmentForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">

        <ContentTemplate>

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <asp:Panel ID="pnal" runat="server" DefaultButton="BtnAddDocument">
                                <table width="100%" cellspacing="8px">
                                    <tr>
                                        <td colspan="7">&nbsp;
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td valign="top">Attachment :<span style="color: red;"> *</span>
                                        </td>
                                        <td valign="top">
                                            <asp:FileUpload ID="FileUploadDocu" runat="server"
                                              CssClass="form-control" Width="250px"  TabIndex="6"/>
                                            <asp:Label ID="lblpath" runat="server"></asp:Label>
                                        </td>
                                        <td valign="top">Description :<span style="color: red;"> *</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdescrip" runat="server" CssClass="form-control" TextMode="MultiLine"  TabIndex="5"></asp:TextBox>
                                        </td>
                                        <td valign="top">
                                            Revise Date :<span style="color: red;"> *</span>
                                        </td>

                                        <td valign="top">
                                             <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" MaxLength="60" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter revise Date" ForeColor="Red" ControlToValidate="txtdate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    <asp:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtdate">
                                                    </asp:CalendarExtender>

                                        </td>

                                        <td valign="top">

                                            
                                            <asp:Button ID="BtnAddDocument" runat="server" CssClass="btn bg-blue-active"
                                                Text="Add" ValidationGroup="S" OnClick="BtnAddDocument_Click" TabIndex="7" />
                                            <asp:UpdatePanel ID="ww" runat="server">
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="BtnAddDocument" />
                                                </Triggers>
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            &nbsp;
                                        </td>
                                    </tr>
                                  <tr>
                                        <td colspan="7">
                                            <asp:GridView ID="GridViewUpload" runat="server" AllowPaging="true"
                                                AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" PageSize="5"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:BoundField DataField="filepath" HeaderText="Document Path" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                     <asp:BoundField DataField="Revisedate" HeaderText="Revise Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                    

                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" ToolTip="Edit" OnClick="imgExpedit_Click" />

                                                            <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" ToolTip="Delete" OnClick="imgExpdelete_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle />
                                                <HeaderStyle />
                                                <PagerStyle HorizontalAlign="left" />
                                                <RowStyle />
                                                <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle />
                                                <SortedAscendingHeaderStyle />
                                                <SortedDescendingCellStyle />
                                                <SortedDescendingHeaderStyle />
                                                <RowStyle CssClass="odd" />
                                                <AlternatingRowStyle CssClass="even" />
                                            </asp:GridView>


                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>

                                        <td colspan="5" align="center">
                                            <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click"
                                                CssClass="btn bg-blue-active" TabIndex="8" />
                                            &nbsp
                                                                <asp:Button ID="Button1" runat="server" Text="Clear" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" TabIndex="9" />
                                            <br />
                                        </td>
                                    </tr>

                                </table>
                                    </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

               

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

