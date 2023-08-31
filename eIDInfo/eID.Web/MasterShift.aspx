<%@ Page Title="Shift Master" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="MasterShift.aspx.cs" Inherits="MasterShift" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_grd_Empshift').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_grd_Empshift_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsubmit" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="col-md-12">
                            <div class="box">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Shift List</h3>
                                    <div class="box-tools">
                                        <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" CssClass="btn btn-primary btn-sm" Text="Add New" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <asp:GridView ID="grd_Empshift" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CssClass="table table-bordered table-striped" OnRowDataBound="grd_Empshift_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" Visible="false" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="Shift" HeaderText="Employee Shift" />
                                            <asp:BoundField DataField="Intime" HeaderText=" In Time" />
                                            <asp:BoundField DataField="Outtime" HeaderText="Out Time" />
                                            <asp:BoundField DataField="Latemark" HeaderText="Late Time" />
                                            <asp:BoundField DataField="IsDefault" HeaderText="Is Default" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Edit" runat="server" CommandArgument='<%# Eval("ShiftID") %>'
                                                        Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" ToolTip="Edit" OnClick="Edit_Click" />
                                                     <asp:ImageButton ID="Delete" runat="server" CommandArgument='<%# Eval("ShiftID") %>'
                                                        Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" ToolTip="Delete" OnClick="Delete_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <div class="col-lg-12">
                            <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                                <div class="box">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Shift Add</h3>
                                        <div class="box-tools">
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <div class="row form-horizontal" style="margin:5px">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Company Name</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:Label ID="lblheadid" runat="server" Visible="False"></asp:Label>
                                                            <asp:RequiredFieldValidator ControlToValidate="ddlCompany" InitialValue="0" ErrorMessage="Required field" ID="RequiredFieldValidator12" runat="server"
                                                                ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                            <asp:Label ID="lbldefualttime" runat="server" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Shift</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="ddlshift" runat="server" CssClass="form-control">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ControlToValidate="ddlshift" ErrorMessage="Required field" ID="RequiredFieldValidator11" runat="server"
                                                                ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Start Time</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtintimehours" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ControlToValidate="txtintimehours" ErrorMessage="Required field" ID="RequiredFieldValidator10" runat="server"
                                                                ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">End Time</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtouttime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ControlToValidate="txtouttime" ErrorMessage="Required field" ID="RequiredFieldValidator9" runat="server"
                                                                ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                    <label class="control-label col-sm-4">
                                                        Shift Begin
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtBeginTime" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                            <div class="input-group-addon">In Minutes</div>
                                                        </div>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtBeginTime" ErrorMessage="Required field" ID="RequiredFieldValidator8" runat="server"
                                                            ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                    <label class="control-label col-sm-4">
                                                        Shift End
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                            <div class="input-group-addon">In Minutes</div>
                                                        </div>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtEndTime" ErrorMessage="Required field" ID="RequiredFieldValidator7" runat="server"
                                                            ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                </div>
                                            </div>

                                            
                                            
                                            <div class="row">
                                                <div class="col-md-6">
                                                        <div class="form-group">
                                                    <label class="control-label col-sm-4">
                                                        Late Mark Start At
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtlatemarks" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtlatemarks" ErrorMessage="Required field" ID="RequiredFieldValidator3" runat="server"
                                                            ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                    <label class="control-label col-sm-4">
                                                        Late Mark End At
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtLateMarkEnd" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtLateMarkEnd" ErrorMessage="Required field" ID="RequiredFieldValidator2" runat="server"
                                                            ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                </div>
                                            </div>

                                             <div class="row">
                                                <div class="col-md-6">
                                                      <div class="form-group">
                                                    <label class="control-label col-sm-4">
                                                        Early Mark Start At
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtearlymarkstart" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtearlymarkstart" ErrorMessage="Required field" ID="RequiredFieldValidator13" runat="server"
                                                            ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                </div>
                                                <div class="col-md-6">
                                                      <div class="form-group">
                                                    <label class="control-label col-sm-4">
                                                        Early Mark End At
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtearlyend" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtearlyend" ErrorMessage="Required field" ID="RequiredFieldValidator15" runat="server"
                                                            ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-6">
                                                     <div class="form-group">
                                                    <label class="control-label col-sm-4">
                                                        Minimum Shift Hours
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <%--<div class="input-group">--%>
                                                            <asp:TextBox ID="txtshifthours" runat="server" CssClass="form-control" placeholder="Enter shift hours" TextMode="Time"></asp:TextBox>
                                                           <%-- <div class="input-group-addon">In Hours</div>--%>
                                                        <%--</div>--%>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="Required Field" ForeColor="Red" ControlToValidate="txtshifthours" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                </div>
                                                <div class="col-md-6">
                                                     <div class="form-group">
                                                    <label class="control-label col-sm-4">
                                                        OT Starts After
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtOTHours" runat="server" CssClass="form-control" placeholder="OT Start After" TextMode="Time"></asp:TextBox>
                                                        <%--<div class="input-group-addon">In Hours</div>--%>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" runat="server" ErrorMessage="Required Field" ForeColor="Red" ControlToValidate="txtOTHours" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                </div>
                                            </div>

                                             <div class="row">                                               
                                                <div class="col-md-6">
                                                      <div class="form-group">
                                                    <label class="control-label col-sm-4">
                                                        Default Shift
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <div class="checkbox">
                                                            <asp:Label ID="txtDefaultMsg" runat="server" CssClass="text-red text-bold"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                            </div>









                                            <div class="col-md-6">                                               
                                                <table width="100%" class="hidden">

                                                    <tr>
                                                        <td>Late Marks Time:
                                                        </td>
                                                        <td>&nbsp;
                                                   
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Over Time:
                                                        </td>
                                                        <td>&nbsp;
                                                            <asp:TextBox ID="txtovertime" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:MaskedEditExtender ID="MaskedEditExtender4" TargetControlID="txtovertime" Mask="99:99"
                                                                MaskType="Time" CultureName="en-us" MessageValidatorTip="true" ErrorTooltipEnabled="True"
                                                                AcceptAMPM="true" runat="server">
                                                            </asp:MaskedEditExtender>

                                                            <asp:UpdatePanel ID="update4" runat="server" UpdateMode="Always">
                                                                <ContentTemplate>
                                                                    <asp:MaskedEditValidator ID="MasketEditValidator1" ControlExtender="MaskedEditExtender4" ForeColor="Red"
                                                                        acceptampm="true" ControlToValidate="txtovertime" IsValidEmpty="false" MaximumValue="12"
                                                                        MinimumValue="01" EmptyValueMessage="Time is Required" EmptyValueBlurredText="Time is Required"
                                                                        InvalidValueBlurredMessage="Time is Invalid" InvalidValueMessage="Time is Invalid" runat="server"></asp:MaskedEditValidator>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Difference Time (Minutes) <span style="color: red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdifftime" runat="server" CssClass="form-control" placeholder="Enter in Minutes" MaxLength="8" TabIndex="7"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filterdiiftime" runat="server" FilterType="Numbers" TargetControlID="txtdifftime"></asp:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtdifftime" ValidationGroup="g"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>Less than Difference time (Minutes) <span style="color: red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtlessdifftime" runat="server" CssClass="form-control" placeholder="Enter in Minutes" MaxLength="8" TabIndex="8"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filterlessdifftime" runat="server" FilterType="Numbers" TargetControlID="txtlessdifftime"></asp:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtlessdifftime" ValidationGroup="g"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>Amount  <span style="color: red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtamount" runat="server" CssClass="form-control" placeholder="Enter Amount" MaxLength="10" TabIndex="9"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filteramount" runat="server" ValidChars="0123456789." TargetControlID="txtamount"></asp:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtamount" ValidationGroup="g"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </div>

                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel1_Click" CausesValidation="false"
                                            CssClass="btn btn-default" />
                                        <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="A"
                                            CssClass="btn btn-primary pull-right" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
