<%@ Page Title="HR Grievance" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="HRGrievance.aspx.cs" Inherits="HRGrievance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                        coldata = $(this).children().eq(2);
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>

            <table width="100%">
                <tr>
                    <td>
                              
                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <span>Enter the Complaint Code : </span><input type="text" id="txtSearch" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <b>
                                                                <asp:Label ID="lbl1" runat="server" Text="No. of Counts :">
                                                                </asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblcnt" runat="server">
                                            </asp:Label></b>
                                                              <asp:Label ID="lblid" runat="server" Visible="false"></asp:Label>
                                            
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
                                                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                                                    <asp:BoundField DataField="ComplaintCode" HeaderText="Complaint Code" />
                                                                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"/>
                                                                    <asp:BoundField DataField="Title" HeaderText="Topic"/>
                                                                     <asp:TemplateField HeaderText="Description" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                         <asp:Label ID="lbldesc" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="50px" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Status" HeaderText="Status"/>
                                                                    <asp:BoundField DataField="HRRemark" HeaderText="HR Remark"/>
                                                                     
                                                                     <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/Images/i_edit.png" CommandArgument='<%# Eval("ComplaintId") %>'
                                                                                OnClick="OnClick_Edit" ToolTip="Edit" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="50px" />
                                                                        <ItemStyle HorizontalAlign="Center" />
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
                          
                    </td>
                </tr>
            </table>

            <table id="tblStatus" runat="server" visible="false" style="background-color: gray; height: 200px; width: 200px">


                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" Height="200px" CssClass="panel_popup" Width="400px">
                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-body">
                                        <div class="form-group">

                                            <asp:HiddenField ID="hrcompId" runat="server" />
                                            <fieldset style="border-color: #800000; margin-left: 10px; margin-right: 10px;">
                                                <table style="width: 100%">


                                                    <tr style="background-color: #FFFFFF">
                                                        <td style="color: Black; font-size: 16px; font-weight: bold; background-color: #FFFFFF;"
                                                            align="center" class="style10">Complaint Status
                                                        </td>
                                                        <td style="float: right; background-color: #FFFFFF;">
                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Close.jpg" 
                                                                Width="23px" />
                                                        </td>
                                                    </tr>

                                                </table>
                                                <table border="0" width="100%" align="center">
                                                    <tr>
                                                      <td>Remark :<span style="color: red;"> *</span></td>
                                                        <td>
                                                            <asp:TextBox ID="txtremark" runat="server" CausesValidation="True" CssClass="form-control" BackColor="White" TextMode="MultiLine"></asp:TextBox>
                                                           
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtremark"
                                                                Display="Static" ErrorMessage="Enter Remark" ValidationGroup="k" ForeColor="Red"></asp:RequiredFieldValidator>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Status :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlstatus" runat="server">
                                                                <asp:ListItem>Resolved</asp:ListItem>
                                                                <asp:ListItem>On Hold</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <br />
                                                            <asp:Button Text="Save" runat="server" ID="btnpopsave" OnClick="btnpopsave_Click1" CssClass="btn bg-blue-active"
                                                                ValidationGroup="k" OnClientClick="alert('HR Grievance Saved Successfully');"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </caption>
                                  </fieldset>
                                        </div>
                                    </div>
                                </div>
                            </div>
                          
                        </asp:Panel>
                        <br />
                        <asp:Label ID="Labelpanb" runat="server" Text=""></asp:Label>
                        <asp:ModalPopupExtender ID="modRelive" runat="server" TargetControlID="Labelpanb"
                            PopupControlID="tblStatus">
                        </asp:ModalPopupExtender>
                    </td>
                </tr>

            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

