<%@ Page Title="City" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="MasterCity.aspx.cs" Inherits="MasterCity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
           
            <table width="100%">

                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="padding-left: 18px">
                                            <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                                Text="Add New" />
                                        </td>
                                    </tr>
                                </table>
                                <br />

                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <table width="100%">
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
                                                            <asp:GridView ID="grd_City" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10"
                                                                OnPageIndexChanging="grd_City_PageIndexChanging" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="CityId" HeaderText="City Id" Visible="false" />
                                                                    <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                                                    <asp:BoundField DataField="StateName" HeaderText="State Name"/>
                                                                    <asp:BoundField DataField="CityName" HeaderText="City Name" />
                                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%# Eval("CityId") %>'
                                                                                Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="Unnamed1_Click" ToolTip="Edit" />
                                                                            <%--  <asp:LinkButton Text="Edit" runat="server" CommandArgument='<%#Eval("CityId")%>' ID="lnk" OnClick="Unnamed1_Click" />--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <FooterStyle />
                                                                <HeaderStyle />
                                                               <%-- <PagerStyle HorizontalAlign="Right" />--%>
                                                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                                                <RowStyle />
                                                                <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                                <SortedAscendingCellStyle />
                                                                <SortedAscendingHeaderStyle />
                                                                <SortedDescendingCellStyle />
                                                                <SortedDescendingHeaderStyle />
                                                                <EmptyDataTemplate>
                                                                    No Data Exists.......
                                                                </EmptyDataTemplate>
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
                                <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                                    <section class="content-header">
                                        <h3>City Information </h3>

                                    </section>
                                    <section class="content">

                                        <!-- left column -->
                                        <div class="col-md-7">

                                            <div class="box box-primary">
                                              
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <%-- <asp:Panel ID="PnlAdd" runat="server" Width="500px">--%>
                                                            <table width="100%">
                                                                <br />
                                                                <tr>
                                                                    <td valign="top">Country Name:<span style="color: red;"> *</span></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"
                                                                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                                                            AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                        
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" SetFocusOnError="true"
                                                                            ControlToValidate="ddlCountry" ForeColor="Red" 
                                                                            ErrorMessage="*" Operator="NotEqual" ValidationGroup="w"
                                                                            ValueToCompare="--Select--"></asp:CompareValidator>
                                                                        <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                                            TargetControlID="CompareValidator1">
                                                                        </asp:ValidatorCalloutExtender>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trstate" runat="server">
                                                                    <td valign="top">State Name:<span style="color: red;"> *</span></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                        
                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                            ControlToValidate="ddlstate" Display="None" ErrorMessage="Select State"
                                                                            ValidationGroup="w"></asp:RequiredFieldValidator>

                                                                        <asp:CompareValidator ID="cmp2" runat="server" SetFocusOnError="true"
                                                                            ControlToValidate="ddlstate" ForeColor="Red"
                                                                            ErrorMessage="*" Operator="NotEqual" ValidationGroup="w"
                                                                            ValueToCompare="--Select--"></asp:CompareValidator>
                                                                        <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                                                            TargetControlID="cmp2">
                                                                        </asp:ValidatorCalloutExtender>--%>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td valign="top">City Name:<span style="color: red;"> *</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtcityname" runat="server" CausesValidation="True" CssClass="form-control"
                                                                            ValidationGroup="w" placeholder="Enter City Name" MaxLength="40"></asp:TextBox>
                                                                       

                                                                        <asp:Label ID="lblcityid" runat="server" Visible="False"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" 
                                                                            ControlToValidate="txtcityname"  ErrorMessage="*" ForeColor="Red"
                                                                            ValidationGroup="w"></asp:RequiredFieldValidator>
                                                                       <%-- <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                                        </asp:ValidatorCalloutExtender>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">Status:
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rd_status" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="Active" Value="0" Selected="True">
                                  
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Inactive" Value="1">
                                  
                                                                            </asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td>
                                                                        <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="w"
                                                                            CssClass="btn bg-blue-active" />
                                                                        &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <%--  </asp:Panel>--%>
                                                            <%-- <asp:RoundedCornersExtender ID="rn" runat="server" TargetControlID="PnlAdd" Radius="10"
                                                Corners="All" BorderColor="#333">
                                            </asp:RoundedCornersExtender>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                                   
                                            </div>
                                        </div>
                                    </section>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
            </fieldset>
             
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

