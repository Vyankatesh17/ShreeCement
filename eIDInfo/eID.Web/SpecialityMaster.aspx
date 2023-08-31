<%@ Page Title="Speciality Master" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="SpecialityMaster.aspx.cs" Inherits="Recruitment_SpecialityMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table>
                <tr>
                    <td style="padding-left: 18px">
                        <asp:Button ID="btnadd" runat="server" CssClass="btn bg-blue-active" Text="Add New" OnClick="btnadd_Click" />
                        <br />
                        <br>
                    </td>
                </tr>
            </table>

            <div class="col-md-12">
                <div class="box box-primary">

                    <div class="box-body">
                        <div class="form-group">
                            <%--   <fieldset>
                                <legend>Speciality Details</legend>
                            </fieldset>--%>


                            <table width="100%">
                                <tr>
                                    <td style="float:right;">
                                        <b>No. of Counts : <asp:Label ID="lblCount" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdSpeciality" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-striped" AllowPaging="True" OnPageIndexChanging="grdSpeciality_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
>
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SpecialityName" HeaderText="Speciality Name" />

                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgedit" ToolTip="Edit" runat="server" CommandArgument='<%# Eval("SpecialityId") %>' ImageUrl="~/Recruitment/Images/i_edit.png" OnClick="imgedit_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>


            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">



            <div class="col-md-7">
                <div class="box box-primary">

                    <div class="box-body">
                        <div class="form-group">


                            <table align="center" width="100%" cellspacing="8px">
                                <tr>
                                    <td valign="top">Speciality Name :
                                        <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtspeciality" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtspeciality_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtspeciality" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                        </asp:FilteredTextBoxExtender>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Speciality Name" ForeColor="Red" ControlToValidate="txtspeciality" ValidationGroup="S"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblspicalId" runat="server" Visible="false"></asp:Label>
                                        </td>
                                   </tr>
                                   <tr>
                                    <td></td>
                                    <td colspan="2">
                                        <asp:Button ID="btnsave" runat="server" CssClass="btn bg-blue-active" OnClick="btnsave_Click" Text="Save" ValidationGroup="S" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn bg-blue-active" OnClick="btnCancel_Click" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>


            </div>











        </asp:View>



    </asp:MultiView>
</asp:Content>

