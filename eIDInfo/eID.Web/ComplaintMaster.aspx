<%@ Page Title="Complaints" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ComplaintMaster.aspx.cs" Inherits="ComplaintMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
     <script>
         $(document).ready(function () {
             var rows;
             var coldata;
             $('#txtSearch').keyup(function () {
                 var data = $('#txtSearch').val();

                 $('#<%=grd_Complaint.ClientID%>').find('tr:gt(0)').hide();

                 var len = data.length;
                 if (len > 0) {
                     $('#<%=grd_Complaint.ClientID%>').find('tbody tr').each(function () {
                        coldata = $(this).children().eq(1);
                        var temp = coldata.text().toUpperCase().indexOf(data.toUpperCase());
                        if (temp === 0) {
                            $(this).show();
                        }
                    });
                } else {
                    $('#<%=grd_Complaint.ClientID%>').find('tr:gt(0)').show();
                }

             });
         });
    </script>
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
                                        <td style="padding-left: 18px">
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
                                                        <td>
                                                            <span>Enter the Code : </span><input type="text" id="txtSearch" />
                                                        </td>
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
                                                            <asp:GridView ID="grd_Complaint" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                CssClass="table table-bordered table-striped" OnPageIndexChanging="grd_Complaint_PageIndexChanging" AllowPaging="true" PageSize="10">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ComplaintCode" HeaderText="Complaint Code" />
                                                                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                    <asp:BoundField DataField="Title" HeaderText="Topic" />
                                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                    <asp:BoundField DataField="HRRemark" HeaderText="HR Remark" />

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
                                                                    No Data Exists.....
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
                                <section class="content-header">
                                    <h3>Complaint Information </h3>
                                </section>
                                <section class="content">
                                    <div class="col-md-7">
                                        <div class="box box-primary">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <!-- left column -->
                                                    <asp:Panel ID="pan" runat="server" DefaultButton="btnsubmit">
                                                        <table cellpadding="8px" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td>Complaint Code :</td>
                                                                <td>
                                                                    <asp:Label ID="lblcode" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Date :<span style="color: red;"> *</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>

                                                                    <br />

                                                                    <asp:CalendarExtender ID="txtbirtdate_CalendarExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtdate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:Label ID="lblComplaintid" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Title :<span style="color: red;"> *</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txttitle" runat="server" CssClass="form-control" placeholder="Enter Title" AutoPostBack="True" OnTextChanged="txttitle_TextChanged"></asp:TextBox>

                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                        ControlToValidate="txttitle" ForeColor="Red"
                                                                        ErrorMessage="Enter Title" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Description :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtdescription" runat="server" CssClass="form-control" placeholder="Enter Description" TextMode="MultiLine"></asp:TextBox>


                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Document Name :</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtdocname" runat="server" CssClass="form-control" placeholder="Enter Title"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                        ControlToValidate="txtdocname" ForeColor="Red"
                                                                        ErrorMessage="Enter Document Name" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Attachment :
                                                                </td>
                                                                <td>
                                                                    <asp:FileUpload ID="FileUploadDocu" runat="server"
                                                                        CssClass="form-control" Width="320px" TabIndex="6" />
                                                                    <asp:Label ID="lblpath" runat="server"></asp:Label>
                                                                    <asp:Button ID="BtnAddDocument" runat="server" CssClass="btn bg-blue-active"
                                                                        Text="Add" ValidationGroup="a" OnClick="BtnAddDocument_Click" TabIndex="7" />
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
                                                                <td colspan="2">
                                                                    <asp:GridView ID="GridViewUpload" runat="server"
                                                                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                        Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sr.No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="srno" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="DocumentName" HeaderText="Document Name" />
                                                                            <asp:BoundField DataField="Documentpath" HeaderText="Document Path" />
                                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                                        Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" ToolTip="Edit" OnClick="imgDocedit_Click" />

                                                                                    <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                                        Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" ToolTip="Delete" OnClick="imgDocdelete_Click" />
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
                                                                <td></td>
                                                                <td>
                                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                                                        CssClass="btn bg-blue-active" />
                                                                    &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </asp:Panel>




                                                </div>
                                            </div>
                                        </div>
                                </section>

                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

