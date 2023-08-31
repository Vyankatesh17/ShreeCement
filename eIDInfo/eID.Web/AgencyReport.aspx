<%@ Page Title="Agency Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="AgencyReport.aspx.cs" Inherits="Recruitment_AgencyReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <div class="col-md-12">
        <div class="box box-primary">

            <div class="box-body">
                <div class="form-group">
                   
                    <table width="100%">
                        <tr>
                            <td>
                                <table width="100%">

                                    <tr>
                                        <td> <div class="form-group">From Date :<span style="color:red;"> * </span>  </div>
                                        </td>
                                        <td> <div class="form-group">
                                            <asp:TextBox ID="txtfromdate" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                            <asp:CalendarExtender ID="dtprelivedate_CalendarExtender" runat="server" Enabled="True"
                                                 TargetControlID="txtfromdate" Format="MM/dd/yyyy">
                                            </asp:CalendarExtender>
                                            </div>
                                        </td>

                                        <td> <div class="form-group">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                ControlToValidate="txtfromdate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:CompareValidator ID="CompareValidator2" runat="server"
                                                 ErrorMessage="InValid Date"  ControlToCompare="txtfromdate" 
                                                ControlToValidate="txtTodate" ForeColor="Red" Operator="GreaterThanEqual" 
                                                Type="Date" ValidationGroup="S" Font-Size="9pt" Display="Dynamic"></asp:CompareValidator>
                                            </div> 

                                        </td>

                                        <td> <div class="form-group">To Date :<span style="color:red;"> *</span> </div>
                                        </td>
                                        <td> <div class="form-group">
                                            <asp:TextBox ID="txtTodate" runat="server" Width="200px" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                              TargetControlID="txtTodate" Format="MM/dd/yyyy">
                                            </asp:CalendarExtender> </div>
                                            </td>

                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red"
                                                ControlToValidate="txtTodate" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            <br />
                                     
                                            </td>

                                        <td>  <div class="form-group">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" Text="Search" ValidationGroup="S" OnClick="btnSearch_Click" />
                                       </div> </td>
                                        <td><div class="form-group">
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel" OnClick="btnCancel_Click" />
                                          </div>  </td>
                                        <td><div class="form-group">
                                            <asp:Label ID="lblagencyId" runat="server" Visible="false"></asp:Label>
                                       </div> </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table><br />
                    <table width="100%">
                        <tr>
                            <td align="right" style="padding-right: 30px">
                              <b>  <asp:Label ID="Label14" runat="server" Text="No. of Count: " Font="Bold"></asp:Label>
                                &nbsp;
                               <asp:Label ID="lblcount" runat="server" Font="Bold">0</asp:Label>
</b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pangrid" runat="server" ScrollBars="Vertical" Height="500px">
                                    <asp:GridView ID="grdAgencyReport" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdAgencyReport_RowDataBound"
                                        CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="10" Width="100%" OnPageIndexChanging="grdAgencyReport_PageIndexChanging">
                                        <Columns>
                                            <%-- <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Position" HeaderText="Position Name" />
                                            <asp:BoundField DataField="Selected" HeaderText="Selected" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Rejected" HeaderText="Rejected" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Hold" HeaderText="Hold" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Offer" HeaderText="Offer" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="ScheduleCnt" HeaderText="Total Interview" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />

                                            <%--<asp:BoundField DataField="Email_Address" HeaderText="Email Id" />--%>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No Record Exist....!!!!!!!!!!!!!!
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>


    </div>

</asp:Content>

