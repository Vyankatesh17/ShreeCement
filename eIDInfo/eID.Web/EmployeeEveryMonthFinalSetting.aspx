<%@ Page Title="Employee Salary Setting" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="EmployeeEveryMonthFinalSetting.aspx.cs" Inherits="EmployeeEveryMonthFinalSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">

                            <div class="row">
                                        <div class="form-group col-md-2">
                                            <label class="control-label">Company Name </label>                                           
                                               <asp:DropDownList CssClass="form-control" ID="ddlCompany" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                </asp:DropDownList>                                             
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label class="control-label">Employee Name</label>                                            
                                               <asp:DropDownList ID="ddEmp" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddEmp_SelectedIndexChanged">
                                                </asp:DropDownList>                                                 
                                        </div>

                                <div class="form-group col-md-2">
                                    <label class="control-label">Month</label>      
                                    <asp:DropDownList ID="ddmonth" runat="server" CssClass="form-control" ValidationGroup="a">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddmonth" Display="Dynamic"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="S" ValueToCompare="--Select--" SetFocusOnError="true" ForeColor="Red"></asp:CompareValidator>

                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddmonth"
                                                    Display="None" ErrorMessage="Select Month" Operator="NotEqual" SetFocusOnError="True"
                                                    ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                <asp:ValidatorCalloutExtender ID="CompareValidator3_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="CompareValidator3">
                                                </asp:ValidatorCalloutExtender>
                                     </div>


                                <div class="form-group col-md-2">
                                    <label class="control-label">Year</label>      
                                    <asp:DropDownList ID="ddyear" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddyear"
                                                    Display="None" ErrorMessage="Select Year" Operator="NotEqual" SetFocusOnError="True"
                                                    ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                                <asp:ValidatorCalloutExtender ID="CompareValidator2_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="CompareValidator2">
                                                </asp:ValidatorCalloutExtender>
                                     </div>

                                <div class="form-group col-md-2">
                                    <label class="control-label"></label>
                                     <asp:Button ID="ShowComponent" runat="server" CssClass="btn bg-blue-active" Text="Show" OnClick="ShowComponent_Click" />
                                    </div>
                                 </div>




                            <div class="form-group" style="height:500px">

                               

                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                                        
                                        <tr>
                                            <td>&nbsp;&nbsp;&nbsp;Select Type<asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList CssClass="form-control" ID="ddearded" runat="server">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem>Earning</asp:ListItem>
                                                    <asp:ListItem>Deduction</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddearded"
                                                    Display="None" ErrorMessage="Select Type" Operator="NotEqual" SetFocusOnError="True"
                                                    ValidationGroup="b" ValueToCompare="--Select--"></asp:CompareValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="CompareValidator4">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                            <td>Component Name:<asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcomponentname" runat="server" CssClass="form-control" placeholder="Enter Component Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcomponentname"
                                                    Display="None" ErrorMessage="Enter Component Name" ValidationGroup="b" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                                    TargetControlID="RequiredFieldValidator1">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                            <td>Amount<asp:Label ID="Label5" runat="server" Font-Size="Medium" ForeColor="Red" Text=" *"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamount" runat="server" CssClass="form-control" placeholder="Enter Amount"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="txtcontactno_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtamount" ValidChars="0123456789.">
                                                </asp:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="req4" runat="server" ControlToValidate="txtamount"
                                                    Display="None" ErrorMessage="Enter amount" ValidationGroup="b" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="req4_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                    TargetControlID="req4">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnadd" runat="server" CssClass="btn bg-blue-active" Text="Add" OnClick="btnadd_Click"
                                                    ValidationGroup="b" />
                                            </td>
                                        </tr>

                                    </table>

                                    <br />
                                    <br />
                                    <div class="col-md-12">
                                        <div>
                                            <div>
                                                <div class="form-group">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:GridView ID="grdadd" runat="server" Width="100%" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Type" HeaderText="Type" />
                                                                        <asp:BoundField DataField="Componentname" HeaderText="Component Name" />
                                                                        <asp:BoundField DataField="Amount" HeaderText="Component Name" />
                                                                        <asp:TemplateField HeaderText="Edit">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%#Eval("Componentname")%>'
                                                                                    ImageUrl="~/Images/i_edit.png" OnClick="imgedit_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%#Eval("Componentname")%>'
                                                                                    ImageUrl="~/Images/i_delete.png" OnClick="imgdelete_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <div style="margin-top: 15px; float: right;">
                                                                    <asp:Button ID="btnsave" runat="server" CssClass="btn bg-blue-active" Text="Save"
                                                                        OnClick="btnsave_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>

                                                    </table>


                                                </div>
                                            </div>
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
