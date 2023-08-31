<%@ Page Title="Clearence Certificate" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ClearanceCerticateDepartmentWise.aspx.cs" Inherits="ClearanceCerticateDepartmentWise" %>


<%@ Register Src="Controls/wucPopupMessageBox.ascx" TagName="Time" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>

            <asp:MultiView ID="MultiView1" runat="server">


                <asp:View ID="View1" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Button ID="btnadd" runat="server" Text="Add New" OnClick="btnadd_Click" CssClass="btn bg-blue-active" />
                            </td>
                        </tr>
                    </table>
                    <br />

                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <table width="100%">
                                        <tr>
                                            <td align="right">
                                                <b>
                                                    <asp:Label ID="lbl1" runat="server" Text="No. of Counts :"> </asp:Label>

                                                    <asp:Label ID="lblcnt" runat="server">0</asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <div style="overflow: scroll; height: 500px;">
                                                    <asp:GridView ID="grdalldata" runat="server" BorderStyle="none" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                                        Width="100%"
                                                        CssClass="table table-bordered table-striped">
                                                        <AlternatingRowStyle BackColor="white" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ClearCertificatemasterID" HeaderText="SR. No" Visible="false" />
                                                            <asp:BoundField DataField="EnterDate" HeaderText="Enter Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Name" HeaderText="Employee Name" />
                                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />

                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkApproved" runat="server" Text="Approved" CommandArgument='<%# Eval("ClearCertificatemasterID") %>'
                                                                        OnClick="lnkApproved_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" />
                                                        <EmptyDataTemplate>
                                                            no data exists!!!!!!!!!!!!!!!!
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                </asp:View>
                </div>
                    </div>
                </section>

                <asp:View ID="View2" runat="server">
                    <section class="content-header">
                        <h3>Clearence Certificate</h3>
                    </section>
                    <section class="content">
                        <!-- left column -->
                        <div class="col-md-7">
                            <div class="box box-primary">
                                <table width="100%">
                                    <tr>
                                        <td>Company Name :
                                        </td>
                                        <td>&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                    </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Employee Name :<span style="color: red;">*</span>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddEmp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddEmp_SelectedIndexChanged" CssClass="form-control">
                                    </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddEmp"
                                                Display="None" ErrorMessage="Select Employee" Operator="NotEqual" SetFocusOnError="True"
                                                ValidationGroup="d" ValueToCompare="--Select--"></asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" runat="server"
                                                TargetControlID="CompareValidator1">
                                            </asp:ValidatorCalloutExtender>
                                            <asp:Label ID="lblclearid" runat="server" Text="Label" Visible="false"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Select Department :<span style="color: red;">*</span>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                    <asp:DropDownList ID="dddepartment" runat="server" AutoPostBack="True" CssClass="form-control"
                                        OnSelectedIndexChanged="dddepartment_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem>Department Head</asp:ListItem>
                                        <asp:ListItem>Accounts &amp; Finance</asp:ListItem>
                                        <asp:ListItem>Admin</asp:ListItem>
                                        <asp:ListItem>IT</asp:ListItem>
                                        <asp:ListItem>HR</asp:ListItem>
                                    </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="dddepartment"
                                                Display="None" ErrorMessage="Select Department" Operator="NotEqual" SetFocusOnError="True"
                                                ValidationGroup="d" ValueToCompare="--Select--"></asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                TargetControlID="CompareValidator2">
                                            </asp:ValidatorCalloutExtender>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Description :
                                        </td>
                                        <td>
                                            <asp:GridView ID="grdheaddepartment" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered table-striped" ShowFooter="False">
                                                <Columns>
                                                    <asp:BoundField DataField="A" HeaderText="Description" />
                                                    <asp:TemplateField HeaderText="Comment">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcomments" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddstatus" runat="server">
                                                                <asp:ListItem>Pending</asp:ListItem>
                                                                <asp:ListItem>Approved</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle HorizontalAlign="Center" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                            </asp:GridView>
                                        </td>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn bg-blue-active"
                                                    OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="d" />
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                                    CssClass="btn bg-blue-active" OnClick="btnCancel_Click" Text="Cancel" />
                                            </td>
                                        </tr>
                                </table>
                            </div>
                        </div>
                    </section>
                </asp:View>
            </asp:MultiView>
            </fieldset>
            <uc1:Time ID="modpop" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

