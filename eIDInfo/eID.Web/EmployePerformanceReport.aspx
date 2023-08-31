<%@ Page Title="Employee Performance Report " Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="EmployePerformanceReport.aspx.cs" Inherits="EmployePerformanceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdOutGoRegReport" runat="server">
        <ContentTemplate>



                <!-- left column -->
                <div class="col-md-12">

                    <div class="box box-primary">

                        <div class="panel-body">
                            <div class="box-content padded">
                                <div class="tab-content">
                                    <!----TABLE LISTING STARTS--->
                                    <div class="tab-pane  active" id="list">
                                        <div class="box-content" style="">
                                            <table width="100%">
                                                <%-- <tr>
                                            <td colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>--%>

                                                <tr>
                                                    <td>From Date :<asp:TextBox ID="txtfromdate" runat="server" Width="184px" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>








                                                        <asp:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtfromdate">
                                                        </asp:CalendarExtender>



                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="txtfromdate" ErrorMessage="Select Date" Font-Size="9pt"
                                                            ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>



                                                    </td>
                                                    <td>To Date :<asp:TextBox ID="txttodate" runat="server" Width="171px" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>


                                                        <asp:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                            Enabled="True" TargetControlID="txttodate">
                                                        </asp:CalendarExtender>


                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                            ControlToValidate="txttodate" ErrorMessage="Select Date" Font-Size="9pt"
                                                            ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>

                                                <tr>

                                                    <td colspan="2">
                                                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn bg-blue-active"
                                                            ValidationGroup="a" OnClick="btnsearch_Click" />

                                                        &nbsp; &nbsp;
                                                <asp:Button ID="btncancel" runat="server" Text="Cancel"
                                                    OnClick="btncancel_Click" CssClass="btn bg-blue-active" />
                                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                                <asp:Button ID="btnPrint" runat="server" Text="Print To PDF"
                                                    CssClass="btn bg-blue-active" OnClick="btnPrint_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" runat="server" id="tbldisplay" align="left" style="padding-right: 200px; padding-top: 20px">
                                                <tr>
                                                    <td align="right">
                                                        <div id="div3" runat="server" visible="false">
                                                            <table align="center" width="30%">
                                                                <tr>
                                                                    <td align="center">
                                                                        <b>
                                                                            <asp:Label ID="Label2" runat="server" Text="Performance Report" Font-Size="16pt"></asp:Label>&nbsp;
                                                                        </b>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div runat="server" id="div2" visible="false">
                                                            <b>From Date :</b>
                                                            <asp:Label ID="lblFromdate" runat="server">
                                                            </asp:Label>
                                                            <b>To Date :</b>
                                                            <asp:Label ID="lblTodate" runat="server">
                                                            </asp:Label>
                                                        </div>
                                                        <div id="div1" runat="server" style="float: right;">
                                                            <b>
                                                                <asp:Label ID="Label1" runat="server" Text="No. of Counts :"> </asp:Label>
                                                                <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                                        </div>



                                                        <asp:Panel ID="panelss" runat="server" ScrollBars="Auto">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grd_EmpPerformance" runat="server"
                                                                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                        AllowPaging="true" PageSize="10" OnPageIndexChanging="grd_EmpPerformance_PageIndexChanging"
                                                                        OnSelectedIndexChanged="grd_EmpPerformance_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sr.No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId" Visible="false" />
                                                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                                            <asp:BoundField DataField="DateofEval" HeaderText="Date of Evaluation" DataFormatString="{0:d}" />
                                                                            <asp:BoundField DataField="EmpPosition" HeaderText="Employe Position" />
                                                                            <asp:BoundField DataField="EvaluatorName" HeaderText="Evaluator Name" />
                                                                            <asp:BoundField DataField="EvalType" HeaderText="Type Of Evaluation" />


                                                                        </Columns>
                                                                        <EmptyDataTemplate>No Record Exists......!!</EmptyDataTemplate>
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <FooterStyle />
                                                                        <HeaderStyle />
                                                                        <PagerStyle HorizontalAlign="Left" />
                                                                        <RowStyle />
                                                                        <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                                                        <SortedAscendingCellStyle />
                                                                        <SortedAscendingHeaderStyle />
                                                                        <SortedDescendingCellStyle />
                                                                        <SortedDescendingHeaderStyle />
                                                                    </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <!----TABLE LISTING ENDS - modal_add_mentee.aspx->
           		           	<!----CREATION FORM STARTS---->
                                    <div class="tab-pane box" id="add" style="padding: 5px;">
                                        <div class="box-content" style="">
                                        </div>
                                    </div>
                                    <!----CREATION FORM ENDS--->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

