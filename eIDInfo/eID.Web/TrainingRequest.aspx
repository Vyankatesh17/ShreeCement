<%@ Page Title="Training Request" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="TrainingRequest.aspx.cs" Inherits="TrainingRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


                        <div class="col-md-12">
                        <div class="box box-primary">

                            <div class="box-body">
                                <div class="form-group">
                                    <asp:MultiView ID="mult" ActiveViewIndex="0" runat="server">
                                        <asp:View ID="v1" runat="server">
   

                                            <asp:Button ID="btnAddNew" CssClass="btn bg-blue-active" runat="server" Text="Add New" OnClick="btnAddNew_Click" />
                                            <br />
                                         <div style="padding-left: 80%;"> <b>No of Records :
                                           <asp:Label Text="0" ID="lblcnt" runat="server" /></b>
                                            </div><br />
                                            
                                            <asp:GridView ID="grdTraining" runat="server" AutoGenerateColumns="False" 
                                                DataKeyNames="TraingID" 
                                                EmptyDataText="There are no data records to display." CssClass="table table-bordered table-striped" AllowPaging="True" OnPageIndexChanging="grdTraining_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No">
                                                        <ItemTemplate>

                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="TraingID" HeaderText="TraingID" Visible="false" ReadOnly="True" SortExpression="TraingID" />
                                                    <asp:BoundField DataField="ReqDate" HeaderText="Training Date" SortExpression="ReqDate" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <asp:BoundField DataField="Title" HeaderText="Training Title" SortExpression="Title" />
                                                    <asp:BoundField DataField="Name" HeaderText="Employee Name"   SortExpression="Name" />
                                                    <asp:TemplateField  HeaderText="Course Content">
                                                        <ItemTemplate>
                                                            
                                                            <asp:TextBox CssClass="form-control" ReadOnly="true" TextMode="MultiLine" Width="150px" Height="50px" Text='<%#Eval("CourseContent") %>' ID="txtCourseContent" runat="server" />
                                                       </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField  HeaderText="Description">
                                                        <ItemTemplate>
                                                          
                                                            <asp:TextBox  CssClass="form-control" TextMode="MultiLine" Width="150px" Height="50px" ReadOnly="true" Text='<%#Eval("Description") %>' ID="txtDes" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    
                                                   
                                                    <asp:BoundField DataField="Type" HeaderText="Training Type" SortExpression="Type" />
                                                </Columns>
                                                <EmptyDataTemplate>No Record Exists........!!</EmptyDataTemplate>
                                            </asp:GridView>
                                            
                                            

                                        </asp:View> 
                                         <asp:View ID="v2" runat="server">
                                            <table  width="100%">
                            <tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
                            <tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
                            <tr>
                                <td> <div class="form-group">Training Date:<span style="color:red;">*</span>
                             </div>   </td>
                                <td>
                                   <div class="form-group">
                                    <asp:TextBox ID="txtDate" runat="server"  ReadOnly="True" CssClass="form-control" Width="270px" AutoPostBack="True" Enabled="False"></asp:TextBox>
                                  


                                    <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                        Enabled="True" TargetControlID="txtDate">
                                    </asp:CalendarExtender>
                                    </div>
                                </td>
                                <td><div class="form-group">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" Display="None" ErrorMessage="Enter Training Date" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </asp:ValidatorCalloutExtender> </div>
                                </td>
                            </tr>
                            <tr>
                                <td> <div class="form-group">Training Title:<span style="color:red;">*</span> </div></td>
                                <td> <div class="form-group">
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Width="270px" MaxLength="50"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtTitle_FilteredTextBoxExtender" runat="server" 
                                        Enabled="True" TargetControlID="txtTitle" ValidChars=".-=#%&abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                    </asp:FilteredTextBoxExtender>
                                    </div>
                                </td>
                                <td><div class="form-group"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtTitle" Display="None"
                                        ErrorMessage="Enter Training Title" SetFocusOnError="True"
                                        ValidationGroup="S"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </asp:ValidatorCalloutExtender> </div></td>
                            </tr>
                            <tr>
                                <td> <div class="form-group"> Course Content :</div> </td>
                                <td>
                                    <div class="form-group"> 
                                        <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" Height="50px" TextMode="MultiLine" Width="270px"></asp:TextBox>
                                    </div></td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td><div class="form-group"> Description :</div></td>
                                <td>
                                    <div class="form-group"><asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" Width="270px"></asp:TextBox>
</div></td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td><div class="form-group"> Training Required For :<span style="color:red;">*</span> </div></td>
                                <td>
                                    <div class="form-group"> 
                                        <asp:RadioButton ID="rbPersonal" runat="server" Checked="True" Text="Personal" GroupName="a" />
&nbsp;<asp:RadioButton ID="rbTeam" runat="server" Text="Team" GroupName="a" />
                                    </div></td>
                                <td>
                                    &nbsp;</td>
                            </tr>

                             <tr>  <td>
                                    <div class="form-group">
                                    </div>
                                 </td>
                                <td><div class="form-group"> <asp:Button ID="btnSubmit" runat="server" Text="Request"
                                        OnClick="btnSubmit_Click1" ValidationGroup="S"   CssClass="btn bg-blue-active"/>
                                    
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"  CssClass="btn bg-blue-active" />
                      </div></td>
                                <td>
                                    </td>
                              
                            </tr>

                            </table>
                                        </asp:View>
                                    </asp:MultiView>

                         </div>
                </div>
                    </div>
                </div>


</asp:Content>

