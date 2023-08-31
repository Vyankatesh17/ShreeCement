<%@ Page Title="Salary Component"  Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMaster.master" CodeFile="AddPFValueDynamically.aspx.cs" Inherits="AddSalaryComponent" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">

                    <table width="97%">
             
                        <tr>
                            <td style="padding-left: 15px">
                                <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                    Text="Add New" />
                                <br />
                                <br />
                            </td>
                        </tr>

                    </table>



                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <%-- <fieldset>
                                        <legend>Salary Component Value Information</legend>--%>
                                    <table width="100%" cellspacing="8px">
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <b>
                                                    <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                                    </asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblcnt" runat="server">
                                            </asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grd_Dept" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                    CssClass="table table-bordered table-striped" OnPageIndexChanging="grd_Dept_PageIndexChanging" AllowPaging="true" PageSize="10">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Startlimit" HeaderText="Start Limit" />
                                                        <asp:BoundField DataField="endlimit" HeaderText="End Limit" />
                                                          <asp:BoundField DataField="valuetype" HeaderText="Value Type" />
                                                        <asp:BoundField DataField="PercentageValue" HeaderText="Percentage Value" />
                                                        <asp:BoundField DataField="FixedValue" HeaderText="Fixed Value" />
                                                        <asp:TemplateField HeaderText="Action">                                                        


                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgview" runat="server" ImageUrl="~/Images/i_edit.png" CommandArgument='<%# Eval("pfid") %>'
                                                                    OnClick="OnClick_Edit" ToolTip="Edit" />
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
                                                </asp:GridView>
                                            </td>
                                        </tr>




                                    </table>
                                    <%--</fieldset>--%>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>


                <asp:View ID="View2" runat="server">
                    <table width="97%">
                        <tr>
                            <td>
                                <div style="float: right">
                                    <h4><small><a href="admin_dashboard.aspx">Dashboard</a>&nbsp; ><a href="#"> Salary</a>&nbsp; > &nbsp;<a href="AddSalaryComponentValue.aspx">Add Salary Component Value</a>&nbsp;</small></h4>
                                </div>
                            </td>
                        </tr>
                    </table>

                    <section class="content-header">
                        <h3>Add PF Salary Setting</h3>
                    </section>
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <table width="100%" cellspacing="8px">
                                    
                                       

                                          <tr>
                                            <td>Start Limit :<asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>

                                            <td>
                                                 <asp:Label ID="lbldeptid" runat="server" Visible="False"></asp:Label>
                                                  <asp:TextBox ID="txtstartlimit" runat="server" CssClass="form-control"
                                                   ></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ValidChars="0123456789." TargetControlID="txtstartlimit" ID="fl1" runat="server"></asp:FilteredTextBoxExtender>

                                                  <asp:RequiredFieldValidator ID="rqstart" runat="server"
                                                    ControlToValidate="txtstartlimit" Display="None"
                                                    ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="red">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                          <tr>
                                            <td>End Limit :<asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtendlimit" runat="server"  CssClass="form-control"
                                                   ></asp:TextBox>
                                                  <asp:FilteredTextBoxExtender ValidChars="0123456789." TargetControlID="txtendlimit" ID="FilteredTextBoxExtender1" runat="server"></asp:FilteredTextBoxExtender>

                                                  <asp:RequiredFieldValidator ID="rqend" runat="server"
                                                    ControlToValidate="txtendlimit" Display="None"
                                                    ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="red">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Value Type :
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rb_Value_Type" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="True" OnSelectedIndexChanged="rb_Value_Type_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True">Fixed</asp:ListItem>
                                                    <asp:ListItem Value="1">Percentage</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Value :<asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtvalue" runat="server" CssClass="form-control" placeholder="Enter Value">
                                                </asp:TextBox><br />                                                                                           

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtvalue" Display="None"
                                                    ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="red">
                                                </asp:RequiredFieldValidator>

                                               <%-- <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                </asp:ValidatorCalloutExtender>--%>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td></td>
                                            <td>
                                                <br />
                                                <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                    CssClass="btn bg-blue-active" />
                                                &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                            </td>
                                        </tr>
                                    </table>
                                  
                    </td>
                                        </tr>
                                        </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            <uc1:Time ID="modpop" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
