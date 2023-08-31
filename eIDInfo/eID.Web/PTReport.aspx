<%@ Page Title="Professional Tax Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="PTReport.aspx.cs" Inherits="PTReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<asp:UpdatePanel ID="utc" runat="server">
        <ContentTemplate>--%>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%">


                            <tr>
                                <section class="content-header">
                                    <caption>
                                        <div style="float: left">


                                            <h3> </h3>
                                        </div>
                                    </caption>
                                </section>
                            </tr>
                            <tr>
                                <td>
                                    <div class="col-md-12">
                                        <asp:Panel ID="pann" runat="server" DefaultButton="btnshow">
                                            <div class="box box-primary">

                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <table width="100%">

                                                            <tr>
                                                                <td>
                                                                    <fieldset id="field">
                                                                        <legend></legend>
                                                                        <table width="50%">
                                                                            <tr>
                                                                                <td>State
                                                                                </td>
                                                                                 <td>Slab From
                                                                                </td>
                                                                                 <td >Slab To
                                                                                </td>
                                                                                <td></td>
                                                                                </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control" CausesValidation="True">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                               
                                                                               
                                                                                <td>
                                                                                    <asp:TextBox ID="txtslabfrom" runat="server" CssClass="form-control" >0.00</asp:TextBox>
                                                                                    <asp:FilteredTextBoxExtender ID="txtslabfrom_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtslabfrom" ValidChars="0123456789.">
                                                                                    </asp:FilteredTextBoxExtender>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtslabfrom"
                                                                                        Display="None" ErrorMessage="Required Field" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                                                                        runat="server" TargetControlID="RequiredFieldValidator1">
                                                                                    </asp:ValidatorCalloutExtender>
                                                                                </td>
                                                                               
                                                                                <td>
                                                                                    <asp:TextBox ID="txtslabto" runat="server" CssClass="form-control">0.00</asp:TextBox>
                                                                                    <asp:FilteredTextBoxExtender ID="txtslabto_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtslabto" ValidChars="0123456789.">
                                                                                    </asp:FilteredTextBoxExtender>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtslabto"
                                                                                        Display="None" ErrorMessage="Required Field" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                                                                        runat="server" TargetControlID="RequiredFieldValidator2">
                                                                                    </asp:ValidatorCalloutExtender>
                                                                                </td>
                                                                                <td></td>
                                                                                
                                                                            </tr>
                                                                            </table>

                                                                        <table width="50">
                                                                            <tr>
                                                                                <td>
                                                                                <div style="margin-top: 15px;">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                </div>
                                                                            </td>
                                                                                <td>
                                                                                    <div style="margin-top:15px;">
                                                                                    <asp:Button ID="btnshow" runat="server" Text="Search" OnClick="btnshow_Click"
                                                                                        CssClass="btn bg-blue-active" ValidationGroup="a"  />
                                                                                        </div>
                                                                                    </td>
                                                                                <td>
                                                                                     <div style="margin-top: 15px;">
                                                                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn bg-blue-active"
                                                                                        OnClick="btnCancel_Click" Text="Cancel"  />
                                                                                         </div>
                                                                                </td>
                                                                                <td>
                                                                                     <div style="margin-top: 15px;">
                                                                                    <asp:Button ID="btnPrintCurrent" runat="server" CssClass="btn bg-blue-active" Text="Print Current Page"
                                                                                        OnClick="PrintCurrentPage" ValidationGroup="a" Width="150px" Visible="False" />
                                                                                         </div>
                                                                                </td>
                                                                                <td>
                                                                                     <div style="margin-top: 15px;">
                                                                                    <asp:Button ID="btnExpPDF" runat="server" CssClass="btn bg-blue-active" OnClick="btnExpPDF_Click"
                                                                                        Text="Export to PDF" ValidationGroup="a"  />
                                                                                         </div>
                                                                                </td>
                                                                                <td>
                                                                                     <div style="margin-top: 15px;">
                                                                                    <asp:Button ID="btnExpExcel" runat="server" CssClass="btn bg-blue-active" Text="Export to Excel"
                                                                                        OnClick="btnExpExcel_Click" ValidationGroup="a" Width="120px" />
                                                                                         </div>
                                                                                </td>
                                                                               
                                                                            </tr>
                                                                        </table>
                                                                        <table id="tbldisp" runat="server" width="100%">
                                                                            <tr>
                                                                                <td colspan="6" align="right" style="padding-right: 2%">
                                                                                    <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="No Of Data: ">
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblcount" Font-Bold="True" runat="server" Text="0"></asp:Label>
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Panel ID="pan" runat="server">
                                                                                    <asp:GridView ID="gv1" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" ShowFooter="false"
                                                                                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnPageIndexChanging="gv1_PageIndexChanging" Caption="Professional Tax Report"
                                                                                        PagerStyle-CssClass="pgr" Width="100%">
                                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblNo" runat="server" Text="<%# Container.DataItemIndex + 1  %>" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="slab" HeaderText="Slab" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Charge" HeaderText="Charges" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="jan" HeaderText="Jan" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="feb" HeaderText="Feb" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="mar" HeaderText="March" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="apr" HeaderText="April" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="may" HeaderText="May" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="jun" HeaderText="Jun" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="jul" HeaderText="July" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Aug" HeaderText="Aug" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="sep" HeaderText="Sept" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="oct" HeaderText="Oct" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="nov" HeaderText="Nov" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="dec" HeaderText="Dec" ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            No Data Exist!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                                                        </EmptyDataTemplate>
                                                                                        <PagerStyle HorizontalAlign="Right" />
                                                                                    </asp:GridView>
                                                                                        </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
