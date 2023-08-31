<%@ Page Title="Interview Report" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="InterviewReport.aspx.cs" Inherits="HRReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updt" runat="server">
        <ContentTemplate>
                                        <asp:Panel ID="pann" runat="server" DefaultButton="btnsearch">
                                            <div class="box box-primary">

                                                <div class="box-body">
                                                    <div class="form-group">
                            <table width="55%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="From Date"></asp:Label>
                                       <asp:Label ID="Label6" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                     <td>
                                        <asp:Label ID="Label5" runat="server" Text="To Date"></asp:Label>
                                        &nbsp;<asp:Label ID="Label7" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                        &nbsp;</td>
                                     <td>
                                        <asp:Label ID="Label2" runat="server" Text="Company-Wise"></asp:Label>
                                        &nbsp;<asp:Label ID="Label8" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                      <td>
                                        <asp:Label ID="Label3" runat="server" Text="Position-Wise"></asp:Label>
                                        <asp:Label ID="Label9" runat="server" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    </tr>
                                <tr>
                                    <td >
                                        <asp:TextBox ID="txtFdate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtFdate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFdate">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFdate" ErrorMessage="Select Date" SetFocusOnError="True" ValidationGroup="S" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFdate" Display="Dynamic" ErrorMessage="Invalid Date Format" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date" ValidationGroup="S" ValueToCompare="MM/dd/yyyy"></asp:CompareValidator>

                                    </td>
                                   
                                    <td>
                                        <br />
                                        <asp:TextBox ID="txtTdate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtTdate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtTdate">
                                        </asp:CalendarExtender>
                                        &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtFdate" ControlToValidate="txtTdate" Display="Dynamic" ErrorMessage="Invalid Date" ForeColor="Red" Operator="GreaterThanEqual" Type="Date" ValidationGroup="S"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTdate" ErrorMessage="Select Date" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtTdate" Display="Dynamic" ErrorMessage="Invalid Date Format" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date" ValidationGroup="S" ValueToCompare="MM/dd/yyyy"></asp:CompareValidator>

                                    </td>
                                                                      
                                    <td >
                                        <br />
                <asp:DropDownList ID="ddlComp" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlComp_SelectedIndexChanged" ValidationGroup="S" Width="150px " >
                </asp:DropDownList>
                                        <br />
                                    </td>
                                  
                                    <td >
                                        <br />
                <asp:DropDownList ID="ddlDesig" CssClass="form-control" runat="server" AutoPostBack="True" Width="150%" ValidationGroup="S">
                </asp:DropDownList>
                                         <br />
                                    </td>
                                    </tr>
                                    </table>
                            <table width="100%">
                                <tr>
                                       <td  colspan="2">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn bg-blue-active" OnClick="btnSearch_Click" Text="Search" ValidationGroup="S" />
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td colspan="6">

                                        <asp:GridView ID="grd_Interview" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-striped" Width="100%" OnPageIndexChanging="grd_Interview_PageIndexChanging" AllowPaging="true" PageSize="10">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="InterviewDate" HeaderText="Interview Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />

                                                <asp:BoundField DataField="DeptName" HeaderText="Department Name" />
                                                <asp:BoundField DataField="DesigName" HeaderText="Designation Name" />
                                                <asp:BoundField DataField="CandidateName" HeaderText="Interviewer's Name" />
                                                <asp:BoundField DataField="Mobile" HeaderText="Mobile No" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" />


                                            </Columns>
                                            <EmptyDataTemplate>No Record Exists.........!!</EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

