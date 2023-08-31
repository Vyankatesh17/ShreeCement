<%@ Page Title="Grade Master" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="GradeMaster.aspx.cs" Inherits="GradeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server" UpdateMode="Always">
        <ContentTemplate>
 
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <asp:Panel ID="pnal" runat="server" DefaultButton="btnsubmit">
                                    <table width="100%"  >
                                        <tr>
                                            <td>Grade<span style="color: red;"> *</span></td>
                                            <td>
                                                <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control" TabIndex="1" Width="270px">
                                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="ddlGrade" Display="Dynamic" ErrorMessage="Select Grade" ForeColor="Red" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                            </td>

                                            <td>Description</td>
                                            <td>
                                                <asp:TextBox ID="txtdescrip" runat="server" CssClass="form-control" TabIndex="2" TextMode="MultiLine" Width="270px" MaxLength="1000"></asp:TextBox>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>Category <span style="color: red;">*</span> </td>
                                            <td>
                                                <asp:ListBox ID="lstLevel" runat="server" AutoPostBack="True" Width="270px" Height="120px" TabIndex="3">
                                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                                    <asp:ListItem>Labour</asp:ListItem>
                                                    <asp:ListItem>Executive</asp:ListItem>
                                                    <asp:ListItem>Middle Management</asp:ListItem>
                                                    <asp:ListItem>Top Management</asp:ListItem>
                                                </asp:ListBox>
                                                <br />
                                                <asp:CompareValidator ID="cmp3" runat="server" ControlToValidate="lstLevel" Display="Dynamic" ErrorMessage="Select Category" ForeColor="Red" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>

                                            <td align="center">
                                                <asp:Label ID="lblid" runat="server" Text="Label" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn bg-blue-active" TabIndex="4" Text="Save" ValidationGroup="a" OnClick="btnsubmit_Click1" />
                                                &nbsp;
                                                <asp:Button ID="Button1" runat="server" CssClass="btn bg-blue-active" OnClick="btncancel_Click" TabIndex="5" Text="Cancel" />
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="GridViewUpload" runat="server"
                                                    AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="GradeID" HeaderText="Document_ID"
                                                            Visible="false" />

                                                        <asp:BoundField DataField="GradeName"  HeaderStyle-HorizontalAlign="Center" HeaderText="Grade Name" />
                                                        <asp:BoundField DataField="Level"  HeaderStyle-HorizontalAlign="Center" HeaderText="Category" />
                                                       <asp:TemplateField HeaderText="Description">
                                                           <ItemTemplate>
                                                               <asp:TextBox Height="50px" Width="180px" TextMode="MultiLine" runat="server" ReadOnly="true" Text='<%#Eval("Description") %>' />   
                                                           </ItemTemplate>
                                                           <ItemStyle HorizontalAlign="Center" />
                                                       </asp:TemplateField>
                                                        
                                                      


                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%#Eval("GradeID") %>'
                                                                    Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" ToolTip="Edit" OnClick="imgExpedit_Click" />

                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>No Record Exists....!!</EmptyDataTemplate>
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
                                            <td colspan="3">&nbsp;
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

