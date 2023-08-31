<%@ Page Title="Designationwise Salary Setting" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="setup_salary_desigwise.aspx.cs" Inherits="setup_salary_desigwise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function jqFunctions() {

         
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
                <asp:Panel ID="pnl" runat="server" DefaultButton="btnsubmit">
                    <div class="col-lg-12">
                        <div class="box box-primary">
                            <div class="box-header">
                                <h3 class="box-title">Add Designationwise Salary</h3>
                            </div>
                            <div class="box-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Designation <span class="text-red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:Label ID="lbldesid" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="txtdesigname" runat="server" CssClass="form-control" placeholder="Enter Designation Name"></asp:Label>
                                             </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Daily Salary <span class="text-red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtDailySalary" TextMode="Number" runat="server" CssClass="form-control" placeholder="Enter Daily Salary Amount"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="txtDailySalary" ID="RequiredFieldValidator1" runat="server" CssClass="text-red"
                                                ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="S"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer">
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                    CssClass="btn btn-default" />
                                <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="S"
                                    CssClass="btn btn-primary pull-right" />
                                <asp:HiddenField ID="hfKey" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

