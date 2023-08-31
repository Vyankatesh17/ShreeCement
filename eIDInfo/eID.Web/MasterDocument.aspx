<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="MasterDocument.aspx.cs" Inherits="MasterDocument" %>

<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Grd_Employee_Documents" />

        </Triggers>
        <ContentTemplate>
            <fieldset id="field">
                <legend></legend>
                <table width="100%">

                    <tr>
                        <td>
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                                <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn bg-blue-active"
                                                    Text="AddNew" />
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
                                                                <asp:GridView ID="grd_DOCUMENT" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                    CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10"
                                                                    OnPageIndexChanging="grd_DOCUMENT_PageIndexChanging" Width="100%">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                                        <asp:BoundField DataField="Name" HeaderText="Employee Name" />

                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="Edit" runat="server" Text="Edit" OnClick="OnClick_Edit" CommandArgument='<%# Eval("EmployeeId") %>'
                                                                                    CssClass="linkbutton1" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="View Documents">
                                                                            <ItemTemplate>
                                                                                <center>
                                                                    <asp:ImageButton ID="imgbtnviewDocuments" runat="server" CommandArgument='<%#Eval("EmployeeId")%>'
                                                                        CommandName="cmdview" ImageUrl="~/Images/View.png" BorderStyle="None" Height="30px"
                                                                        Width="30px" onclick="imgbtnviewDocuments_Click"  />
                                                                </center>
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
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                                        <section class="content-header">
                                            <h3>Document Information </h3>
                                        </section>

                                        <section class="content">
                                            <%-- <div class="row">--%>
                                            <div class="col-md-12">
                                                <div class="box box-primary">
                                                    <div class="box-header">
                                                        <h3 class="box-title"></h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="form-group">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:UpdatePanel ID="upp1" runat="server">
                                                                            <ContentTemplate>
                                                                                <fieldset class="fieldset">
                                                                                    <legend class="legend"></legend>
                                                                                    <div class="col-md-6">
                                                                                        <div class="box box-primary">
                                                                                            <div class="box-header">

                                                                                                <table width="100%">

                                                                                                    <tr>
                                                                                                        <td>Company :<span style="color: red;">*</span></td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlCompany" runat="server" Style="margin-left: 10px" CssClass="form-control"
                                                                                                                Width="190px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                                                                                                AutoPostBack="True">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="ddlCompany"
                                                                                                                Display="None" ErrorMessage="Select Company" Operator="NotEqual"
                                                                                                                ValidationGroup="S" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                                                                Enabled="True" TargetControlID="cmp2">
                                                                                                            </asp:ValidatorCalloutExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Department:&nbsp;
                                                            <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="*">
                                                            </asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList Width="190px" Style="margin-left: 10px" ID="ddldept" CssClass="form-control" runat="server" AutoPostBack="True"
                                                                                                                OnSelectedIndexChanged="dddept_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                            &nbsp;
                                                            <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddldept" Display="None"
                                                                ErrorMessage="Select Department" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"
                                                                SetFocusOnError="True">
                                                            </asp:CompareValidator>
                                                                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True"
                                                                                                                TargetControlID="cmp1">
                                                                                                            </asp:ValidatorCalloutExtender>

                                                                                                        </td>
                                                                                                    </tr>

                                                                                                    <tr>
                                                                                                        <td>Employee  :<span style="color: red;">*</span></td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlEmployee" runat="server" Style="margin-left: 10px" CssClass="form-control"
                                                                                                                Width="190px">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCompany"
                                                                                                                Display="None" ErrorMessage="Select Company" Operator="NotEqual"
                                                                                                                ValidationGroup="S" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                                                                Enabled="True" TargetControlID="cmp2">
                                                                                                            </asp:ValidatorCalloutExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="Tr1" runat="server">
                                                                                                        <td class="style3">
                                                                                                            <label>
                                                                                                                Passport Documents:
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <br />
                                                                                                            <asp:FileUpload ID="FileUploadDocu" runat="server"
                                                                                                                onchange=" this.form.submit();" />
                                                                                                            &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                                                          <asp:Label ID="lblpath" runat="server"></asp:Label>
                                                                                                            <asp:Label ID="Label22" runat="server" Visible="False"></asp:Label>
                                                                                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="btnUpload_Click" Text="Upload" Visible="false" Width="70px" />
                                                                                                            <asp:Button ID="BtnAddDocument" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="BtnAddDocument_Click" Text="Add" ValidationGroup="d" Width="70px" />
                                                                                                            <asp:UpdatePanel ID="ww" runat="server">
                                                                                                                <Triggers>
                                                                                                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                                                                                                </Triggers>
                                                                                                                <ContentTemplate>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                            <div id="Parent">
                                                                                                            </div>
                                                                                                            <label>
                                                                                                            </label>
                                                                                                            <br />
                                                                                                            &nbsp;
                                                                          <asp:GridView ID="GridViewUpload" runat="server" AllowPaging="true"
                                                                              AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" PageSize="20"
                                                                              Width="80%">
                                                                              <Columns>
                                                                                  <asp:TemplateField HeaderText="Sr.No">
                                                                                      <ItemTemplate>
                                                                                          <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                                      </ItemTemplate>
                                                                                  </asp:TemplateField>
                                                                                  <asp:BoundField DataField="Document_ID" HeaderText="Document_ID"
                                                                                      Visible="false" />
                                                                                  <asp:BoundField DataField="DocumentName" HeaderText="Document Upload" />
                                                                                  <asp:TemplateField HeaderText="Action">
                                                                                      <ItemTemplate>
                                                                                          <asp:LinkButton ID="lnkDelete" runat="server"
                                                                                              CommandArgument='<%#Eval("DocumentName")%>' OnClick="lnkDelete_Click"
                                                                                              Text="Delete" />
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
                                                                                                    <tr id="Tr2" runat="server">
                                                                                                        <td class="style3">
                                                                                                            <label>
                                                                                                                Visa Attachment :
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <br />
                                                                                                            <asp:FileUpload ID="FileUpload2" runat="server"
                                                                                                                onchange=" this.form.submit();" />
                                                                                                            &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                                                          <asp:Label ID="lblPath1" runat="server"></asp:Label>
                                                                                                            <asp:Label ID="Label24" runat="server" Visible="False"></asp:Label>
                                                                                                            <asp:Button ID="BtnUpload1" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="Button2_Click" Text="Upload" Visible="false" Width="70px" />
                                                                                                            <asp:Button ID="BtnAddDocument1" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="BtnAddDocument1_Click" Text="Add" ValidationGroup="d" Width="70px" />
                                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                <Triggers>
                                                                                                                    <asp:PostBackTrigger ControlID="BtnUpload1" />
                                                                                                                </Triggers>
                                                                                                                <ContentTemplate>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                            <div id="Div1">
                                                                                                            </div>
                                                                                                            <label>
                                                                                                            </label>
                                                                                                            <br />
                                                                                                            &nbsp;
                                                                          <asp:GridView ID="Grd_Upload1" runat="server" AllowPaging="true"
                                                                              AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" PageSize="20"
                                                                              Width="80%">
                                                                              <Columns>
                                                                                  <asp:TemplateField HeaderText="Sr.No">
                                                                                      <ItemTemplate>
                                                                                          <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                                      </ItemTemplate>
                                                                                  </asp:TemplateField>
                                                                                  <asp:BoundField DataField="Document_ID" HeaderText="Document_ID"
                                                                                      Visible="false" />
                                                                                  <asp:BoundField DataField="DocumentName" HeaderText="Document Upload" />
                                                                                  <asp:TemplateField HeaderText="Action">
                                                                                      <ItemTemplate>
                                                                                          <asp:LinkButton ID="lnkDelete1" runat="server"
                                                                                              CommandArgument='<%#Eval("DocumentName")%>' OnClick="lnkDelete1_Click"
                                                                                              Text="Delete" />
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
                                                                                                       <tr id="Tr3" runat="server">
                                                                                                        <td class="style3">
                                                                                                            <label>
                                                                                                                CV Attachment :
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <br />
                                                                                                            <asp:FileUpload ID="FileUpload3" runat="server"
                                                                                                                onchange=" this.form.submit();" />
                                                                                                            &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                                                          <asp:Label ID="lblPath2" runat="server"></asp:Label>
                                                                                                            <asp:Label ID="Label26" runat="server" Visible="False"></asp:Label>
                                                                                                            <asp:Button ID="BtnUpload2" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="Button2_Click" Text="Upload" Visible="false" Width="70px" />
                                                                                                            <asp:Button ID="BtnAddDocument2" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="BtnAddDocument2_Click" Text="Add" ValidationGroup="d" Width="70px" />
                                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                                <Triggers>
                                                                                                                    <asp:PostBackTrigger ControlID="BtnUpload2" />
                                                                                                                </Triggers>
                                                                                                                <ContentTemplate>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                            <div id="Div2">
                                                                                                            </div>
                                                                                                            <label>
                                                                                                            </label>
                                                                                                            <br />
                                                                                                            &nbsp;
                                                                          <asp:GridView ID="Grd_Upload2" runat="server" AllowPaging="true"
                                                                              AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" PageSize="20"
                                                                              Width="80%">
                                                                              <Columns>
                                                                                  <asp:TemplateField HeaderText="Sr.No">
                                                                                      <ItemTemplate>
                                                                                          <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                                      </ItemTemplate>
                                                                                  </asp:TemplateField>
                                                                                  <asp:BoundField DataField="Document_ID" HeaderText="Document_ID"
                                                                                      Visible="false" />
                                                                                  <asp:BoundField DataField="DocumentName" HeaderText="Document Upload" />
                                                                                  <asp:TemplateField HeaderText="Action">
                                                                                      <ItemTemplate>
                                                                                          <asp:LinkButton ID="lnkDelete2" runat="server"
                                                                                              CommandArgument='<%#Eval("DocumentName")%>' OnClick="lnkDelete2_Click"
                                                                                              Text="Delete" />
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
                                                                                                </table>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>



                                                                                    <div class="col-md-6">
                                                                                        <div class="box box-primary">
                                                                                            <div class="box-header">

                                                                                                <table width="100%">
                                                                                                 
                                                                                                    <tr id="Tr4" runat="server">
                                                                                                        <td class="style3">
                                                                                                            <label>
                                                                                                                Labour Card Attachment :
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <br />
                                                                                                            <asp:FileUpload ID="FileUpload4" runat="server"
                                                                                                                onchange=" this.form.submit();" />
                                                                                                            &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                                                          <asp:Label ID="lblPath3" runat="server"></asp:Label>
                                                                                                            <asp:Label ID="Label28" runat="server" Visible="False"></asp:Label>
                                                                                                            <asp:Button ID="BtnUpload3" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="Button2_Click" Text="Upload" Visible="false" Width="70px" />
                                                                                                            <asp:Button ID="BtnAddDocument3" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="BtnAddDocument3_Click" Text="Add" ValidationGroup="d" Width="70px" />
                                                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                                <Triggers>
                                                                                                                    <asp:PostBackTrigger ControlID="BtnUpload3" />
                                                                                                                </Triggers>
                                                                                                                <ContentTemplate>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                            <div id="Div3">
                                                                                                            </div>
                                                                                                            <label>
                                                                                                            </label>
                                                                                                            <br />
                                                                                                            &nbsp;
                                                                          <asp:GridView ID="Grd_Upload3" runat="server" AllowPaging="true"
                                                                              AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" PageSize="20"
                                                                              Width="80%">
                                                                              <Columns>
                                                                                  <asp:TemplateField HeaderText="Sr.No">
                                                                                      <ItemTemplate>
                                                                                          <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                                      </ItemTemplate>
                                                                                  </asp:TemplateField>
                                                                                  <asp:BoundField DataField="Document_ID" HeaderText="Document_ID"
                                                                                      Visible="false" />
                                                                                  <asp:BoundField DataField="DocumentName" HeaderText="Document Upload" />
                                                                                  <asp:TemplateField HeaderText="Action">
                                                                                      <ItemTemplate>
                                                                                          <asp:LinkButton ID="lnkDelete3" runat="server"
                                                                                              CommandArgument='<%#Eval("DocumentName")%>' OnClick="lnkDelete3_Click"
                                                                                              Text="Delete" />
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
                                                                                                    <tr id="Tr5" runat="server">
                                                                                                        <td class="style3">
                                                                                                            <label>
                                                                                                                Other Attachment :
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <br />
                                                                                                            <asp:FileUpload ID="FileUpload5" runat="server"
                                                                                                                onchange=" this.form.submit();" />
                                                                                                            &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                                                          <asp:Label ID="lblPath4" runat="server"></asp:Label>
                                                                                                            <asp:Label ID="Label30" runat="server" Visible="False"></asp:Label>
                                                                                                            <asp:Button ID="BtnUpload4" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="Button2_Click" Text="Upload" Visible="false" Width="70px" />
                                                                                                            <asp:Button ID="BtnAddDocument4" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="BtnAddDocument4_Click" Text="Add" ValidationGroup="d" Width="70px" />
                                                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                                <Triggers>
                                                                                                                    <asp:PostBackTrigger ControlID="BtnUpload4" />
                                                                                                                </Triggers>
                                                                                                                <ContentTemplate>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                            <div id="Div4">
                                                                                                            </div>
                                                                                                            <label>
                                                                                                            </label>
                                                                                                            <br />
                                                                                                            &nbsp;
                                                                          <asp:GridView ID="Grd_Upload4" runat="server" AllowPaging="true"
                                                                              AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" PageSize="20"
                                                                              Width="80%">
                                                                              <Columns>
                                                                                  <asp:TemplateField HeaderText="Sr.No">
                                                                                      <ItemTemplate>
                                                                                          <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                                      </ItemTemplate>
                                                                                  </asp:TemplateField>
                                                                                  <asp:BoundField DataField="Document_ID" HeaderText="Document_ID"
                                                                                      Visible="false" />
                                                                                  <asp:BoundField DataField="DocumentName" HeaderText="Document Upload" />
                                                                                  <asp:TemplateField HeaderText="Action">
                                                                                      <ItemTemplate>
                                                                                          <asp:LinkButton ID="lnkDelete4" runat="server"
                                                                                              CommandArgument='<%#Eval("DocumentName")%>' OnClick="lnkDelete4_Click"
                                                                                              Text="Delete" />
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
                                                                                                    <tr id="Tr6" runat="server">
                                                                                                        <td class="style3">
                                                                                                            <label>
                                                                                                                Other Attachment :
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <br />
                                                                                                            <asp:FileUpload ID="FileUpload6" runat="server"
                                                                                                                onchange=" this.form.submit();" />
                                                                                                            &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                                                          <asp:Label ID="lblPath5" runat="server"></asp:Label>
                                                                                                            <asp:Label ID="Label32" runat="server" Visible="False"></asp:Label>
                                                                                                            <asp:Button ID="BtnUpload5" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="Button2_Click" Text="Upload" Visible="false" Width="70px" />
                                                                                                            <asp:Button ID="BtnAddDocument5" runat="server" CssClass="btn bg-blue-active"
                                                                                                                OnClick="BtnAddDocument5_Click" Text="Add" ValidationGroup="d" Width="70px" />
                                                                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                                                <Triggers>
                                                                                                                    <asp:PostBackTrigger ControlID="btnUpload5" />
                                                                                                                </Triggers>
                                                                                                                <ContentTemplate>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                            <div id="Div5">
                                                                                                            </div>
                                                                                                            <label>
                                                                                                            </label>
                                                                                                            <br />
                                                                                                            &nbsp;
                                                                          <asp:GridView ID="Grd_Upload5" runat="server" AllowPaging="true"
                                                                              AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" PageSize="20"
                                                                              Width="80%">
                                                                              <Columns>
                                                                                  <asp:TemplateField HeaderText="Sr.No">
                                                                                      <ItemTemplate>
                                                                                          <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                                      </ItemTemplate>
                                                                                  </asp:TemplateField>
                                                                                  <asp:BoundField DataField="Document_ID" HeaderText="Document_ID"
                                                                                      Visible="false" />
                                                                                  <asp:BoundField DataField="DocumentName" HeaderText="Document Upload" />
                                                                                  <asp:TemplateField HeaderText="Action">
                                                                                      <ItemTemplate>
                                                                                          <asp:LinkButton ID="lnkDelete5" runat="server"
                                                                                              CommandArgument='<%#Eval("DocumentName")%>' OnClick="lnkDelete5_Click"
                                                                                              Text="Delete" />
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
                                                                                                </table>
                                                                                                   <table width="100%" runat="server">
                                                                <tr>
                                                                    <td></td>
                                                                    <td align="center">
                                                                        <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                                            CssClass="btn bg-blue-active" />
                                                                        &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                 
                                                                                </fieldset>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%-- </div>--%>
                                        </section>

                                    </asp:Panel>

                                </asp:View>
                            </asp:MultiView>

                            <%--  <asp:Panel ID="PanDocumentList" CssClass="modpann" runat="server" BackColor="Gray" Width="70%" Height="750px" Visible="true">--%>
                            <asp:Panel CssClass="panel_popup" runat="server" ID="PanDocumentList" BackColor="White" ForeColor="#333" Width="70%" Height="650px">
                                <br />
                                <table border="0" cellpadding="0" cellspacing="10" align="center" width="100%" bgcolor="#CCCCCC"
                                    class="modalPopup">
                                    <tr>
                                        <td></td>
                                        <td style="font-family: 'Comic Sans MS'; font-size: 17px; color: #FF0000; font-weight: bold;"
                                            colspan="4" align="center">Documents Details
                                        </td>
                                        <td align="right">
                                            <asp:Image ID="Image5" runat="server" Height="30px" ImageUrl="~/Images/Close.jpg"
                                                Width="30px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <fieldset>
                                                <legend class="leg1">Employee Documents Details </legend>
                                                <table align="left">
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:GridView ID="Grd_Employee_Documents" runat="server"
                                                                AutoGenerateColumns="False" BorderStyle="None"
                                                                AllowPaging="true" PageSize="6"
                                                                BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                                GridLines="Vertical" CssClass="table table-bordered table-striped"
                                                                Width="100%" OnDataBound="Grd_Employee_Documents_DataBound">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId" Visible="false" />
                                                                    <asp:BoundField DataField="Status" HeaderText="Document Type" />

                                                                    <asp:BoundField DataField="DocumentName" HeaderText="Document Name" />

                                                                    <asp:TemplateField HeaderText="Document Name">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageDoc" runat="server" Height="100px" 
                                                                                OnClick="ImageDoc_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton Text="Download" runat="server" CommandArgument='<%#Eval("DocumentName")%>'
                                                                                ID="lnkDownload" OnClick="lnkDownload_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>


                                </table>
                                <br />
                            </asp:Panel>
                            <asp:Label ID="Label64" runat="server"></asp:Label>
                            <asp:ModalPopupExtender ID="ModelPoUpDocumentDetails" runat="server" Enabled="True" PopupControlID="PanDocumentList"
                                CancelControlID="Image5" TargetControlID="Label64">
                            </asp:ModalPopupExtender>

                        </td>
                    </tr>
                </table>
            </fieldset>
            <uc1:Time ID="modpop" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

