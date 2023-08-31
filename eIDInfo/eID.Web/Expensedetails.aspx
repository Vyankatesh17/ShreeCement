<%@ Page Title="Expense Details" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="Expensedetails.aspx.cs" Inherits="AddExpense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table>
                <tr>
                    <td style="padding-left:18px">
                        <asp:Button ID="addnew" runat="server" Text="Add New" OnClick="addnew_Click" CssClass="btn bg-blue-active" />
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
                                        <div style="margin-left: 940px">
                                            <b>
                                                <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>
                                                <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                        </div>
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" PagerStyle-CssClass="pgr"
                                            CssClass="table table-striped table-bordered bootstrap-datatable datatable" OnPageIndexChanging="GridView1_PageIndexChanging"
                                            AllowPaging="true" PageSize="10" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Name" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ManagerStatus" HeaderText="Manager Status" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="DepartmentHeadStatus" HeaderText="Department Head Status"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="HRStatus" HeaderText="HR Status" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <PagerStyle  HorizontalAlign="Right" />
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
                <h3>Expense Details </h3>
            </section>

            <section class="content">
                <%-- <div class="row">--%>
                <div class="box-body">
                    <div class="form-group">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="upp1" runat="server">
                                        <ContentTemplate>

                                            <div class="col-md-6">
                                                <div class="box box-primary">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>Expense Date:<span style="color: red;">*</span>

                                                            </td>
                                                            <td>&nbsp;
                                                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" ReadOnly="true" Width="270px"></asp:TextBox>
                                                                <asp:CalendarExtender ID="cal" runat="server" TargetControlID="txtdate" Enabled="true"
                                                                    Format="MM/dd/yyyy">
                                                                </asp:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rq1" runat="server" ValidationGroup="a" ControlToValidate="txtdate"
                                                                    Display="None" ErrorMessage="Enter Expense Date" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="rq1_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                                    TargetControlID="rq1">
                                                                </asp:ValidatorCalloutExtender>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Expense Type:<span style="color: red;">*</span>

                                                            </td>
                                                            <td>

                                                                <asp:DropDownList ID="ddexptype" runat="server" CssClass="form-control" Width="270px">
                                                                </asp:DropDownList>
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddexptype"
                                                                    Display="None" ErrorMessage="Select Expense Type" Operator="NotEqual" SetFocusOnError="True"
                                                                    ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                                <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" runat="server"
                                                                    Enabled="True" TargetControlID="CompareValidator1">
                                                                </asp:ValidatorCalloutExtender>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Amount:<span style="color: red;">*</span>

                                                            </td>
                                                            <td>

                                                                <asp:TextBox ID="txtamount" runat="server" placeholder="Enter Amount" CssClass="form-control" Width="270px"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="filter" runat="server" TargetControlID="txtamount"
                                                                    FilterType="Numbers">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtamount"
                                                                    Display="None" ErrorMessage=" Enter Amount" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                                </asp:ValidatorCalloutExtender>
                                                                <asp:Label ID="lbldesid1" runat="server" Visible="False"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Remark:
                                                                
                                                            </td>
                                                            <td>

                                                                <asp:TextBox ID="txtremark" runat="server" placeholder="Enter Amount" TextMode="MultiLine" CssClass="form-control" Width="270px"></asp:TextBox>
                                                                &nbsp;
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Upload Document :<asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>

                                                                <asp:Label ID="lblpath" runat="server"></asp:Label>
                                                            </td>
                                                            <td>

                                                                <asp:FileUpload ID="FileUploadDocu" runat="server" CssClass="form-control" Width="270px" />
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <table>
                                                        <tr>

                                                            <td>&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td>
                                                                <div style="margin-left: 200px">
                                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" ValidationGroup="a" OnClick="btnsubmit_Click"
                                                                        CssClass="btn bg-blue-active" />
                                                                    &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                        CssClass="btn bg-blue-active" />
                                                                </div>

                                                            </td>
                                                        </tr>
                                                    </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>

                <%-- </div>--%>
            </section>
        </asp:View>
    </asp:MultiView>

</asp:Content>
