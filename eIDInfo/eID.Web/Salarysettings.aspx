    <%@ Page Title="Salary Settings Information"  Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMaster.master"
    CodeFile="Salarysettings.aspx.cs" Inherits="Salarysettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css" rel="stylesheet">
.ajax__calendar {
 position : absolute;
 }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <fieldset>
                                    <legend>Personal Details</legend>
                                    <table width="100%" cellspacing="8px">
                                        <tr>
                                            <td>Employee ID:<asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtempid" runat="server" AutoPostBack="True"  OnTextChanged="txtempid_TextChanged" CssClass="form-control"></asp:TextBox>

                                                  <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" EnableCaching="true"
                                                    Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtempid"
                                                    CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetEmployeeIDList">
                                                </asp:AutoCompleteExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtempid" Display="None" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Employee Name:<asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtname_TextChanged"></asp:TextBox>

                                                <asp:AutoCompleteExtender ID="txtagencywise_AutoCompleteExtender" runat="server" DelimiterCharacters="" EnableCaching="true"
                                                    Enabled="True" ServicePath="" UseContextKey="True" TargetControlID="txtname"
                                                    CompletionInterval="1" MinimumPrefixLength="1" ServiceMethod="GetEmployeeList">
                                                </asp:AutoCompleteExtender>

                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtname" Display="None" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Company:<asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcompanyname" runat="server" CssClass="form-control"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtcompanyname" Display="None" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Designation:<asp:Label ID="Label5" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesg" runat="server" CssClass="form-control"></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtdesg" Display="None" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <br />
                                            </td>
                                        </tr>




                                    </table>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <fieldset>
                                    <legend>Machine ID / Date Of Joining Details</legend>
                                    <table width="100%" cellspacing="8px">

                                        <tr>
                                            <td>Machine ID:
                                       
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtmachinid" runat="server" CssClass="form-control"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>Date Of Joining:<asp:Label ID="Label6" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                                        
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                    TargetControlID="txtDOJ">
                                                </asp:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDOJ" Display="None" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>Net Salary:<asp:Label ID="Label7" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                                   
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtsalary" runat="server" CssClass="form-control"></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtsalary" Display="None" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>Department:<asp:Label ID="Label8" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                                       
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdept" runat="server" CssClass="form-control"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtdept" Display="None" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <br />
                                            </td>
                                        </tr>


                                    </table>
                                </fieldset>

                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <fieldset>
                                    <legend>Salary Account Details</legend>

                                    <table width="100%" cellspacing="8px">

                                        <tr>
                                            <td>PF AC. Number:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtpfaccount" runat="server" CssClass="form-control"></asp:TextBox>
                                                <br />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Salary Type:
                                            </td>
                                            <td>

                                                <asp:DropDownList ID="ddlsalarytype" runat="server" CssClass="form-control">
                                                    <asp:ListItem>---Select---</asp:ListItem>
                                                    <asp:ListItem>Cash</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                    <asp:ListItem>Salary Account</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="ddlsalarytype" Display="Dynamic"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>


                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bank Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtbankname" runat="server" CssClass="form-control"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>

                                    </table>

                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                <fieldset>
                                    <legend>ESIC AC. / PAN Details</legend>
                                    <table width="100%" cellspacing="8px">

                                        <tr>
                                            <td>ESIC AC. Number:
                                                   
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtesicaccnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                <br />
                                            </td>

                                        </tr>

                                        <tr>

                                            <td>PAN No:
                                       

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPan1" runat="server" CssClass="form-control"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Salary Ac. No:
                                      <td>
                                          <asp:TextBox ID="txtaccount" runat="server" CssClass="form-control"></asp:TextBox>
                                          <br />
                                      </td>

                                            </td>
                                        </tr>
                                    </table>

                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="form-group">
                            <fieldset>
                                <legend>Salary Details</legend>

                                <table width="100%" cellspacing="8px">
                                    <tr align="center">
                                        <td>
                                            <asp:GridView ID="grd_salary" runat="server" CssClass="table table-bordered table-striped" ShowFooter="false" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="Component" HeaderText="Component" />
                                                    <asp:BoundField DataField="percentageValue" HeaderText="Value" />
                                                    <asp:BoundField DataField="ComponentType" HeaderText="Component Type" />
                                                    <%--   <asp:BoundField DataField="amount" HeaderText="amount" />--%>
                                                    <asp:TemplateField HeaderText="amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtamount" runat="server" AutoPostBack="True" OnTextChanged="txtamount_TextChanged" Text='<%# Eval("amount") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    
                                </table>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div>
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <table width="100%" cellspacing="8px">
                                                        <tr>
                                                            <td>Gross salary:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgross" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtgross" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <br />
                                                            </td>

                                                            <%--</tr>
                                         <tr>--%>
                                                            <td>&nbsp;&nbsp;&nbsp;&nbsp; Deductions
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtdeductions" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtdeductions" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <br />
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>Net Pay
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtnetpay" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtnetpay" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <%--</tr>
                                         <tr>--%>
                                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;Salary Date
                                                                <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                                            </td>
                                                            <td>
                                                                
                                                                <asp:TextBox ID="txtsalarydate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                 
                                                                
                                                                <asp:CalendarExtender ID="txtsalarydate_CalendarExtender" runat="server" Enabled="True"
                                                                    TargetControlID="txtsalarydate" PopupPosition="TopLeft" Format="MM/dd/yyyy">
                                                                </asp:CalendarExtender>
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtsalarydate" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>

                                                        </tr>
                                                        <tr><td><br /><br /><br /><br /></td></tr>
                                                        <tr>
                                        <td>
                                           
                                        </td>
                                    </tr>
                                                    </table>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                
                               
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
<div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="row">
                                    <div class="col-md-12">
                                        <div>
                                            <div class="box-body">
                                                <div class="form-group">
                                                    
                                                     <table width="100%">
                                                        
                                                        <tr>
                                                            <td>
                                                                <center>
                                                                <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn bg-blue-active" OnClick="BtnSave_Click" ValidationGroup="S"/>

                                                                <asp:Button ID="btnAmount" runat="server" Text="Calculate" CssClass="btn bg-blue-active" OnClick="btnAmount_Click" />
                                                                    </center>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div></div></div>

            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
