<%@ Page Title="Employee Shift" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="EmployeeShift.aspx.cs" Inherits="EmployeeShift" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">
                                        Assigned Shift List
                                    </h3>
                                    <div class="box-tools">
                                         <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" 
                                        CssClass="btn btn-primary btn-sm"   Text="AddNew" />
                                    </div>
                                </div>
                                <div class="box-body">
                                   <asp:GridView ID="grd_Empshift" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-bordered table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId" Visible="false" />
                                        <asp:BoundField DataField="name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="intime" HeaderText=" In Time" />
                                        <asp:BoundField DataField="outtime" HeaderText="Out Time" />
                                        <asp:BoundField DataField="Latemark" HeaderText="Late Time" />

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Edit" runat="server" CommandArgument='<%#Eval("EmployeeShiftID")%>'
                                                    ID="lnk" OnClick="lnk_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="White" />
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
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Assign Shift</h3>
                            </div>
                            <div class="box-body">
                                <div class="row form-horizontal">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Employee Name</label>
                                            <div class="col-md-7">
                                                <asp:DropDownList ID="ddlempname" runat="server" CssClass="form-control" CausesValidation="True"
                                                    ValidationGroup="A">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblheadid" runat="server" Visible="False"></asp:Label>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlempname"
                                                    Display="None" ErrorMessage="Select Emplyee Name" Operator="NotEqual" ValidationGroup="g"
                                                    ValueToCompare="--Select--"></asp:CompareValidator>
                                                <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" runat="server"
                                                    Enabled="True" TargetControlID="CompareValidator1">
                                                </asp:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3">Shift</label>
                                            <div class="col-md-7">
                                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-3"></label>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="A"
                                                    CssClass="btn bg-blue-active" />
                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel1_Click" CausesValidation="false"
                                                    CssClass="btn bg-blue-active" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2"></div>
                                </div>
                            </div>
                        </div>


                        <section class="content hidden">

                            <!-- left column -->
                            <div class="col-md-8">

                                <div class="box box-primary">

                                    <table width="100%">
                                        <tr>
                                            <td>Out Time:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtouttime" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtouttime" Mask="99:99"
                                                    MaskType="Time" CultureName="en-us" MessageValidatorTip="true" ErrorTooltipEnabled="True"
                                                    AcceptAMPM="true" runat="server">
                                                </asp:MaskedEditExtender>
                                                <%--    <asp:DropDownList ID="ddloutampm" runat="server" Height="26px" Width="57px" 
                                                              >
                                                                  <asp:ListItem>AM</asp:ListItem>
                                                                  <asp:ListItem>PM</asp:ListItem>
                                                            </asp:DropDownList>--%>
                                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                                    <ContentTemplate>
                                                        <asp:MaskedEditValidator ID="MaskedEditValidator123" ControlExtender="MaskedEditExtender2"
                                                            acceptampm="true" ControlToValidate="txtouttime" IsValidEmpty="False" MaximumValue="12"
                                                            MinimumValue="01" EmptyValueMessage="Time is required" EmptyValueBlurredText="Time is required"
                                                            InvalidValueBlurredMessage="Time is invalid" InvalidValueMessage="Time is invalid"
                                                            TooltipMessage="" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>LateMarks Time:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtlatemarks" runat="server" MaxLength="2" CssClass="form-control"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender3" TargetControlID="txtlatemarks" Mask="99:99"
                                                    MaskType="Time" CultureName="en-us" MessageValidatorTip="true" ErrorTooltipEnabled="True"
                                                    AcceptAMPM="true" runat="server">
                                                </asp:MaskedEditExtender>
                                                <%-- <asp:DropDownList ID="ddllateampm" runat="server" Height="26px" Width="57px" 
                                                              >
                                                                  <asp:ListItem>AM</asp:ListItem>
                                                                  <asp:ListItem>PM</asp:ListItem>
                                                            </asp:DropDownList>--%>
                                                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
                                                    <ContentTemplate>
                                                        <asp:MaskedEditValidator ID="MaskedEditValidator133" ControlExtender="MaskedEditExtender3"
                                                            acceptampm="true" ControlToValidate="txtlatemarks" IsValidEmpty="False" MaximumValue="12"
                                                            MinimumValue="01" EmptyValueMessage="Time is required" EmptyValueBlurredText="Time is required"
                                                            InvalidValueBlurredMessage="Time is invalid" InvalidValueMessage="Time is invalid"
                                                            TooltipMessage="" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <br />

                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                        </section>
                    </asp:Panel>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
