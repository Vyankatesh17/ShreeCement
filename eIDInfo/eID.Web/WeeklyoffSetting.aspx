<%@ Page Title="Weekly of Settings" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="WeeklyoffSetting.aspx.cs" Inherits="WeeklyoffSetting" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .style1 {
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
           
            <table width="100%">

                <tr>
                    <td>

                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnadd" runat="server" Text="Add New" CssClass="btn bg-blue-active" OnClick="btnadd_Click" />
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
                                                                <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>

                                                                <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grdweekoffdata" runat="server"  AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  OnPageIndexChanging="grdweekoffdata_PageIndexChanging"
                                                                 Width="100%"
                                                                CssClass="table table-bordered table-striped">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                                    <asp:BoundField DataField="Days" HeaderText="Days" />
                                                                    <asp:BoundField DataField="TrackHolidays" HeaderText="Holidays Track"/>
                                                                    <asp:BoundField DataField="Date" HeaderText="Effective Date" />


                                                                </Columns>
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
                                                                    No data Exists!!!!!!!!!!!!!!!!
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
                                        <h3>Weekly Off Information </h3>

                                    </section>
                                    <section class="content">

                                        <!-- left column -->
                                        <div class="col-md-9" style="height:450px">

                                            <div class="box box-primary">
                                                <br />
                                                
                                                <table width="80%">
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;Select Company<span style="color: red;"> *</span>
                                                            <asp:Label ID="lblcompanyid" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;Select offs<span style="color: red;"></span>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;Select Days<span style="color: red;"></span>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;Effective Date<span style="color: red;"> *</span>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control" Width="230px"
                                                                OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                            </asp:DropDownList>

                                                           <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCompany"
                                                                ErrorMessage="*" Operator="NotEqual" SetFocusOnError="True" ForeColor="Red"
                                                                ValidationGroup="d" ValueToCompare="All"></asp:CompareValidator>--%>
                                                          <%--  <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" runat="server"
                                                                TargetControlID="CompareValidator1">
                                                            </asp:ValidatorCalloutExtender>--%>
                                                        </td>


                                                        <td>&nbsp;
                <asp:DropDownList ID="ddsatoff" runat="server" CssClass="form-control" Width="130px">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>1 &amp; 2</asp:ListItem>
                    <asp:ListItem>2 &amp; 3</asp:ListItem>
                    <asp:ListItem>3 &amp; 4</asp:ListItem>
                    <asp:ListItem>1 &amp; 3 </asp:ListItem>
                    <asp:ListItem>2 &amp; 4</asp:ListItem>
                      <asp:ListItem>1 &amp; 4</asp:ListItem>
                </asp:DropDownList> 
                                                           <%-- <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddsatoff"
                                                                ErrorMessage="*" Operator="NotEqual" SetFocusOnError="True" ForeColor="Red"
                                                                ValidationGroup="d" ValueToCompare="All"></asp:CompareValidator>--%>
                                                            <br />
                                                        </td>


                                                        <td>&nbsp;
              <asp:DropDownList ID="dddays" runat="server" CssClass="form-control" Width="130px">
                  <asp:ListItem>--Select--</asp:ListItem>
                   <asp:ListItem>Monday</asp:ListItem>
                  <asp:ListItem>Tuesday</asp:ListItem>
                  <asp:ListItem>Wednesday</asp:ListItem>
                  <asp:ListItem>Thursday</asp:ListItem>
                  <asp:ListItem>Friday</asp:ListItem>
                  <asp:ListItem>Saturday</asp:ListItem>
                  <asp:ListItem>Sunday</asp:ListItem>
              </asp:DropDownList>
                                                             <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="dddays"
                                                                ErrorMessage="*" Operator="NotEqual" SetFocusOnError="True" ForeColor="Red"
                                                                ValidationGroup="d" ValueToCompare="--Select--"></asp:CompareValidator>
                                                            <br />
                                                            <%--  <asp:GridView ID="grdoff" runat="server" CssClass="mGrid" Width="100%" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                           
                            <ItemTemplate>
                                <asp:CheckBox ID="chkentry" runat="server" OnCheckedChanged="Oncheckentry_Changed"
                                    AutoPostBack="true" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DayN" HeaderText="Days" />
                    </Columns>
                </asp:GridView>--%>
                                                        </td>



                                                        <td>&nbsp;
                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtFromDate" ForeColor="Red"
                                                                ErrorMessage="*" SetFocusOnError="True"
                                                                ValidationGroup="d"></asp:RequiredFieldValidator>

                                                           <%-- <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                            </asp:ValidatorCalloutExtender>--%>
                                                        </td>
                                                        <td>
                                                            <div style="margin-left:50px">
                                                            <asp:ImageButton ID="ImgCitAdd" runat="server" ImageUrl="~/Images/i_add.png" CssClass="form-control" style="height:37px; width:37px" ValidationGroup="d" ToolTip="Add" OnClick="ImgCitAdd_Click" />
                                                                </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                                                CssClass="table table-bordered table-striped">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" Visible="false"/>
                                                                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" />
                                                                     <asp:BoundField DataField="TrackHolidays" HeaderText="Type" />
                                                                    <asp:BoundField DataField="Days" HeaderText="Days" />
                                                                    <asp:BoundField DataField="Date" HeaderText="Effective Date" />
                                                                     <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <center>
                                                                            <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                                 ImageUrl="~/Images/i_edit.png"  ToolTip="Edit" OnClick="Edit_Click"  />
                                                                                <asp:ImageButton ID="Delete" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                                 ImageUrl="~/Images/i_delete.png"  ToolTip="Delete" OnClick="Delete_Click"  />
                                                                                </center>
                                                                            </ItemTemplate>
                                                                         <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle HorizontalAlign="Right" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <br />
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn bg-blue-active"
                                                                OnClick="btnSubmit_Click" />

                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn bg-blue-active"
                                                                OnClick="btnCancel_Click" CausesValidation="False" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
