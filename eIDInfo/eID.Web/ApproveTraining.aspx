<%@ Page Title="Training Approval" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ApproveTraining.aspx.cs" Inherits="ApproveTraining" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


                        
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <div class="form-group">
                                   <b>
                                           <div style="float:right">No. of Records :
                                                <asp:Label Text="0" ID="lblcnt" runat="server" />
                                                </div>
                                               </b>
                                           <br />
                                  <br />
                                <table width="100%">
                                   <tr> 
                                       <td>
                                       

                                        </td>

                                   </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdTraining" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="TraingID" EmptyDataText="There are no data records to display.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No">
                                                        <ItemTemplate>

                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TraingID" HeaderText="TraingID" ReadOnly="True" SortExpression="TraingID" Visible="false" />
                                                    <asp:BoundField DataField="ReqDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Training Date" SortExpression="ReqDate" />
                                                    <asp:BoundField DataField="Title" HeaderText="Training Title" SortExpression="Title" />
                                                    <asp:BoundField DataField="Name" HeaderText="Employee Name" Visible="false" SortExpression="Name" />
                                                    <asp:TemplateField HeaderText="Course Content">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCourseContent" runat="server" CssClass="form-control" ReadOnly="true" Text='<%#Eval("CourseContent") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDes" runat="server" CssClass="form-control" ReadOnly="true" Text='<%#Eval("Description") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Type" HeaderText="Training Type" SortExpression="Type" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                               <asp:ImageButton ImageUrl="~/Images/approved.jpg" Height="30px" Width="30px" ID="imgApprove" runat="server" Text="Approve" CommandArgument='<%# Eval("TraingID") %>'
                                                       ToolTip="Approve Training"  OnClick="OnClick_Edit"   ForeColor="Blue" />
                                                    
                                                    <asp:ConfirmButtonExtender ID="Edit_ConfirmButtonExtender" runat="server" ConfirmText="Approve Training....?" Enabled="True" TargetControlID="imgApprove">
                                                    </asp:ConfirmButtonExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                    
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Record Exists........!!
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
              

</asp:Content>

