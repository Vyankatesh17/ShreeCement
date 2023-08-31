<%@ Page Title="JOB Description, KRA and KPIs Information " Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
     CodeFile="KPIResposiblityMaster.aspx.cs" Inherits="KPIResposiblityMaster" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0"  >
                             
                                <asp:View ID="View1" runat="server">
                            <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset id="field">
                        <legend>JOB Description, KRA and KPIs Information </legend>
                                    <table width="100%" cellspacing="8px">
                                         <tr>
                                            <td>
                                                <asp:Button ID="btnadd" runat="server" CssClass="btn bg-blue-active" 
                                                    Text="AddNew" onclick="btnadd_Click" />
                                            </td>
                                        </tr>


                                        <tr>
                                            <td> 
                                            

                                                       <asp:GridView ID="GridView1" runat="server" AllowPaging="true" 
                                                                    AutoGenerateColumns="False" BorderStyle="Solid"  BorderWidth="1px" CssClass="table table-bordered table-striped"
                                                                    CellPadding="4"  ForeColor="Black" GridLines="Both" 
                                                                   PageSize="10">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="srno0" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                   <%--     <asp:BoundField DataField="KRPId" HeaderText="KRA Id" Visible="false" />--%>
                                                                        <asp:BoundField DataField="Role" HeaderText="Role" />
                                                                        <asp:BoundField DataField="Resposiblity"   HeaderText="Responsiblity" />
                                                                          
                                                                        <asp:BoundField DataField="KeyResultAreas" HeaderText="Key Result Areas (KRA)" />
                                                                        <asp:BoundField DataField="KeyPerformance" HeaderText="Key Performance Indicator (KPI)" />
                                                                        <asp:BoundField DataField="Target" HeaderText="Work Target" />
                                                                          <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                                      
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                               <asp:ImageButton ID="imgedit1" runat="server" CommandArgument='<%# Eval("Role") %>'
                                                                            Height="20px" ImageUrl="~/Images/i_edit.png" Width="30px" 
                                                                                    onclick="imgedit1_Click" />
                                                                       <%-- <asp:ImageButton ID="imgdelete1" runat="server" CommandArgument='<%# Eval("Role") %>'
                                                                            Height="20px" ImageUrl="~/Images/i_delete.png" Width="30px" 
                                                                                    onclick="imgdelete1_Click" />--%>
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
                                           <%--  </td>
                                            </tr>
                                            </table>--%>

                                            </td>
                                        </tr>
                                    </table>
                                        </fieldset>


                                </div>
                            </div>
                        </div>
                    </div>
                                    </asp:View>

                <asp:View ID="View2" runat="server">
                     <div class="col-md-8">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset id="field">
                        <legend>JOB Description, KRA and KPIs Information </legend>
                                  <table cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td>
                                               <%-- <asp:Panel ID="PnlAdd" runat="server" Width="500px">--%>
                                                    <table cellpadding="2" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td>
                                                                Role:&nbsp;  <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*">
                                                    </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRole" runat="server" 
                                                                    CausesValidation="True" 
                                                                    ValidationGroup="w"  TextMode="MultiLine" CssClass="form-control" ></asp:TextBox>
                                                                <asp:Label ID="lblexpenseid" runat="server" Visible="False"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                    ControlToValidate="txtRole" Display="None" ErrorMessage="Enter Your Role" 
                                                                    ValidationGroup="w"></asp:RequiredFieldValidator>
                                                                
                                                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                                </asp:ValidatorCalloutExtender>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Weightage (In %) : <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*">
                                                    </asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtResposibility" runat="server" CausesValidation="True" CssClass="form-control"
                                                                    ValidationGroup="w" TextMode="MultiLine"></asp:TextBox>
                                                             
                                                                <asp:FilteredTextBoxExtender ID="txtResposibility_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="txtResposibility" 
                                                                    ValidChars="0123456789">
                                                                </asp:FilteredTextBoxExtender>
                                                             
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                    ControlToValidate="txtResposibility" Display="None" ErrorMessage="Enter Your Responsibility" 
                                                                    ValidationGroup="w">
                                                                  </asp:RequiredFieldValidator>
                                                               
                                                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                                </asp:ValidatorCalloutExtender>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Responsibility / KEY Result AREAS (KRA) : <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*">
                                                    </asp:Label> </td>
                                                            <td>
                                                                <asp:TextBox ID="txtkeyResultAreas" runat="server" CausesValidation="True" CssClass="form-control"
                                                                    ValidationGroup="w" TextMode="MultiLine" ></asp:TextBox>
                                                              
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                    ControlToValidate="txtkeyResultAreas" Display="None" ErrorMessage="Select KEY Result Areas" 
                                                                    ValidationGroup="w"></asp:RequiredFieldValidator>
                                                              
                                                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                                                </asp:ValidatorCalloutExtender>
                                                              
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                KEY Performance Indicator (KPI): <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*">
                                                    </asp:Label>
                                                                </td>
                                                            <td>
                                                                <asp:TextBox ID="txtKeyPerformanceIndicator" runat="server" 
                                                                    TextMode="MultiLine" CssClass="form-control">
                                                                    </asp:TextBox>
                                                                      <asp:RequiredFieldValidator ID="rettee" runat="server" 
                                                                    ControlToValidate="txtKeyPerformanceIndicator" Display="None" ErrorMessage="Enter Your Key Performance Indicator" 
                                                                    ValidationGroup="w"></asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="rettee_ValidatorCalloutExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="rettee">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td>
                                                                Target :</td>
                                                            <td>
                                                                <asp:TextBox ID="txtWorkTarget" runat="server" 
                                                                    TextMode="MultiLine" CssClass="form-control">
                                                                    </asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="ywyye" runat="server" 
                                                                    ControlToValidate="txtWorkTarget" Display="None" ErrorMessage="Enter Your Work Target" 
                                                                    ValidationGroup="w"></asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="ywyye_ValidatorCalloutExtender" 
                                                                    runat="server" Enabled="True" TargetControlID="ywyye">
                                                                </asp:ValidatorCalloutExtender>
                                                                &nbsp;
                                                                 <asp:Label ID="Label1" runat="server" Visible="true" Text="e.g. Quality, Testing"></asp:Label>
                                                                </td>
                                                                 
                                                        </tr>
                                                         <tr id="try1" runat="server" visible="false">
                                                            <td>
                                                               Remark:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtRemark" runat="server" 
                                                                    TextMode="MultiLine" CssClass="form-control">
                                                                    </asp:TextBox>
                                                                    <%--  <asp:RequiredFieldValidator ID="jsdj" runat="server" 
                                                                    ControlToValidate="txtRemark" Display="None" ErrorMessage="Enter Your Remark" 
                                                                    ValidationGroup="w"></asp:RequiredFieldValidator>

                                                                <asp:ValidatorCalloutExtender ID="jsdj_ValidatorCalloutExtender" runat="server" 
                                                                    Enabled="True" TargetControlID="jsdj">
                                                                </asp:ValidatorCalloutExtender>
--%>
                                                            </td>
                                                        </tr>
                                                       
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                 <asp:Button ID="BtnADDrole" runat="server" Text="Add"  ValidationGroup="w"
                                                                    CssClass="btn bg-blue-active" onclick="BtnADDrole_Click" />
                                                            </td>
                                                        </tr>

                                                         <tr >
                                                         
                                                            <td colspan="2">
                                                           <%--  <table border="1" id= "tbl_t3" runat="server" >
                                                                 <tr>
                                                                 <td>--%>
                                                                <asp:GridView ID="grd_role" runat="server" AllowPaging="true" 
                                                                    AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" 
                                                                    CellPadding="4" CssClass="table table-bordered table-striped" ForeColor="Black" GridLines="Both"
                                                                   PageSize="10" onpageindexchanging="grd_role_PageIndexChanging">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="srno0" runat="server" Text="<%#Container.DataItemIndex + 1 %>" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                   <%--     <asp:BoundField DataField="KRPId" HeaderText="KRA Id" Visible="false" />--%>
                                                                        <asp:BoundField DataField="Role" HeaderText="Role" />
                                                                        <asp:BoundField DataField="Resposiblity"   HeaderText=" Weightage (%)" />
                                                                          
                                                                        <asp:BoundField DataField="KeyResultAreas" HeaderText="Key Result Areas (KRA)" />
                                                                        <asp:BoundField DataField="KeyPerformance" HeaderText="Key Performance Indicator (KPI)" />
                                                                        <asp:BoundField DataField="Target" HeaderText="Target" />
                                                                      
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                               <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("Role") %>'
                                                                            Height="20px" ImageUrl="~/Images/i_edit.png" OnClick="imgedit_Click" Width="30px" />
                                                                        <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%# Eval("Role") %>'
                                                                            Height="20px" ImageUrl="~/Images/i_delete.png" OnClick="imgdelete_Click" Width="30px" />
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

                                                              <%--   </td>
                                                                </tr>
                                                             </table>--%>
                                                                 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                                &nbsp
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                                                    CssClass="btn bg-blue-active" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                              <%--  </asp:Panel>--%>
                                                <%-- <asp:RoundedCornersExtender ID="rn" runat="server" TargetControlID="PnlAdd" Radius="10"
                                                Corners="All" BorderColor="#333">
                                            </asp:RoundedCornersExtender>--%>
                                            </td>
                                        </tr>
                                    </table>
                                        </fieldset>


                                </div>
                            </div>
                        </div>
                    </div>

                    </asp:View>
                </asp:MultiView>
         
      </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

