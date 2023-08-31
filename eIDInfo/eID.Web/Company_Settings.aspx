<%@ Page Title="Company Settings" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Company_Settings.aspx.cs" Inherits="Company_Sttings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="admin_theme/dist/css/bootstrap_multiselect.css" rel="stylesheet" />
    <script src="admin_theme/dist/js/bootstrap_multiselect.js"></script>

    <script type="text/javascript">
        function jqFunctions() {

            var table = $('#ContentPlaceHolder1_gvDataList').DataTable({
                lengthChange: false,
                buttons: ['pageLength', 'copy', 'excel', 'pdf', 'csv', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#ContentPlaceHolder1_gvDataList_wrapper .col-sm-6:eq(0)');
        }

        $(document).ready(function () {
            jqFunctions();
        });
    </script>

    <script>
        function validateHeader(check) {
            if (check.checked) { console.log('checked'); checkAll(); }
            else {
                console.log('un-checked');
                uncheckAll();
            }
        }
        function validate(selCb) {
            if (selCb.checked) {
                console.log("checked");
            } else {
                var x = $("#ContentPlaceHolder1_gvDataList_checkAll").is(":checked");

                if (x == true) {
                    $("#ContentPlaceHolder1_gvDataList_checkAll").prop('checked', false);
                }
                else
                    console.log("You didn't check it! Let me check it for you.")
            }
        }


        //create checkall function
        function checkAll() {
            $("#ContentPlaceHolder1_gvDataList").find("[type='checkbox']").prop('checked', true);
        }
        //create uncheckall function
        function uncheckAll() {
            $("#ContentPlaceHolder1_gvDataList").find("[type='checkbox']").prop('checked', false);
        }

    </script>


    <style>
        .BtnInfo {
            margin: 0px 5px 0px 5px;
        }
    </style>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnActive" />
            <asp:PostBackTrigger ControlID="btnInactive" />
        </Triggers>--%>
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="box">
                            <div class="box-header with-border">
                                <h3 class="box-title">Company Settings</h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 form-group">
                                        <label class="control-label">Status</label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Selected="True">Select Status</asp:ListItem>
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>InActive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1 form-group">
                                        <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-primary" OnClick="btnsearch_Click" Text="Search" />
                                    </div>
                                    <%--<div class="col-lg-1 form-group">
                                    <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label><asp:Button ID="btnReset" runat="server" CssClass="btn btn-info" Text="Reset" OnClick="btnReset_Click" />
                                </div>--%>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:GridView ID="gvDataList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowDataBound="gvDataList_RowDataBound">
                                    <Columns>
                                      <%--  <asp:TemplateField HeaderText="Select All">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect1" CssClass="case" runat="server" onclick="validate(this);" />
                                                <asp:Label ID="lblCompanyId" runat="server" Text='<%#Eval("CompanyId") %>' CssClass="hidden"></asp:Label>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' CssClass="hidden"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" Text="&nbsp;All" onclick="validateHeader(this);" />
                                            </HeaderTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" Visible="false" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="ContactNo" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <%--<asp:BoundField DataField="IsActive" HeaderText="IsActive" />    
                                                <asp:BoundField DataField="IsDisabled" HeaderText="IsDisabled" />  --%>
                                        <asp:TemplateField HeaderText="Action">                                               
                                                    <ItemTemplate> 
                                                        <div class="btn-group" style="min-width: 100px;">
                                                    <asp:LinkButton ID="lbtnActive" runat="server" OnClick="lbtnActive_Click"  CommandArgument='<%#Eval("CompanyId") %>' CssClass="btn btn-sm btn-info BtnInfo" data-toggle="tooltip" data-placement="top" title="Active"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDeActive" runat="server" OnClick="lbtnDeActive_Click" CommandArgument='<%#Eval("CompanyId") %>' CssClass="btn btn-sm btn-warning BtnInfo" data-toggle="tooltip" data-placement="top" title="DeActive"><i class="fa fa-remove"></i></asp:LinkButton>
                                                    </div>
                                                            </ItemTemplate>
                                                </asp:TemplateField>      
                                    </Columns>
                                </asp:GridView>
                               <%-- <br />
                                <div class="row">
                                    <div class="form-group col-md-1">
                                        <label class="control-label">&nbsp;&nbsp;&nbsp;</label>
                                        <asp:Button ID="btnActive" runat="server" CssClass="btn btn-sm btn-info" ValidationGroup="A" Text="Active" OnClick="btnActive_Click" />

                                    </div>
                                    <div class="form-group col-md-1">
                                        <label class="control-label">&nbsp;&nbsp;&nbsp;</label>
                                        <asp:Button ID="btnInactive" runat="server" CssClass="btn btn-sm btn-warning" ValidationGroup="A" Text="InActive" OnClick="btnInActive_Click" />

                                    </div>
                                </div>--%>
                            </div>


                        </div>

                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

