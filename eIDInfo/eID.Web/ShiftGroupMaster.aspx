<%@ Page Title="Shift Group List" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ShiftGroupMaster.aspx.cs" Inherits="ShiftGroupMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

    <style>
        .BtnInfo {
            margin: 0px 5px 0px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                                    <h3 class="box-title">Shift Group List</h3>
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
                                            <asp:BoundField DataField="ShiftGroupId" HeaderText="ShiftGroupId" Visible="false" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="ShiftGroupName" HeaderText="Shift Group Name" />
                                            <asp:BoundField DataField="Remark" HeaderText="Remark" />   
                                            <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group" style="min-width: 100px;">                                                    
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="lbtnEdit_Click" CommandArgument='<%#Eval("ShiftGroupId") %>' CssClass="btn btn-sm btn-warning" data-toggle="tooltip" data-placement="top" title="Edit Shift Group"><i class="fa fa-edit"></i></asp:LinkButton>       
                                                </div>
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
                                        <h3 class="box-title">Shift Group Add</h3>
                                        <div class="box-tools">
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <div class="row form-horizontal">
                                            <div class="col-md-2"></div>
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
                                                 <div class="form-group">
                                                    <label class="control-label col-sm-4">Shift Group</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtShiftGroup" runat="server" CssClass="form-control">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtShiftGroup" ErrorMessage="Required field" ID="RequiredFieldValidator11" runat="server"
                                                            ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                               <div class="form-group">
                                                    <label class="control-label col-sm-4">Shift</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control" ></asp:DropDownList> 
                                                         <asp:RequiredFieldValidator ControlToValidate="ddlShift" ErrorMessage="Required field" ID="RequiredFieldValidator1" runat="server"
                                                            ValidationGroup="A" Display="Dynamic" CssClass="text-red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>                                               
                                                 <div class="form-group">
                                                    <label class="control-label col-sm-4">Remark</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control">
                                                        </asp:TextBox>                                                       
                                                    </div>
                                                </div>
                                                 <asp:Button ID="btnCancelShiftlist" runat="server" Text="Cancel" OnClick="btnCancelShiftlist_Click" CausesValidation="false"
                                            CssClass="btn btn-default " />
                                                <asp:Button ID="btnAddShiftList" runat="server" Text="Add" OnClick="btnAddShiftList_Click" CausesValidation="false"
                                            CssClass="btn btn-primary pull-right" />
                                            </div>
                                            <div class="col-md-3"></div>
                                            </div> 
                                        </br>
                                            <div class="row form-horizontal">
                                                <div class="col-sm-1"> </div>
                                           <div class="col-sm-10">
                                                 <asp:GridView ID="grdShiftList" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CssClass="table table-bordered table-striped" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>                                            
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="ShiftGroupName" HeaderText="Shift Group Name" />
                                            <asp:BoundField DataField="ShiftId" HeaderText="ShiftId" Visible="false" />
                                            <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                             <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <div class="btn-group" style="min-width: 100px;">                                                    
                                                    <asp:LinkButton ID="lbtEditshift" runat="server" OnClick="lbtnEditShift_Click" CommandArgument='<%#Eval("ShiftId") %>' CssClass="btn btn-sm btn-warning BtnInfo" data-toggle="tooltip" data-placement="top" title="Edit Shift Group"><i class="fa fa-edit"></i></asp:LinkButton>       
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbtnDeleteShift_Click" CommandArgument='<%#Eval("ShiftId") %>' CssClass="btn btn-sm btn-flickr BtnInfo" data-toggle="tooltip" data-placement="top" title="Delete Shift Group"><i class="fa fa-remove"></i></asp:LinkButton>       
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                                </div> 
                                       <div class="col-sm-1"> </div>
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

