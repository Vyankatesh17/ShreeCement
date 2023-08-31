<%@ Page Title="Training Attendance" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="TrainingAttendance.aspx.cs" Inherits="TrainingAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   






</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
        <!-- left column -->
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="form-group">


                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#tab_1" data-toggle="tab"><b>Attendance Pending</b></a></li>
                                <li><a href="#tab_2" data-toggle="tab"><b>Training Attendance </b></a></li>
                            </ul>

                            <div class="tab-content">
                                <div class="tab-pane active" id="tab_1">
                                    <div class="row">
                                        <div class="col-lg-12">




                                            <div style="padding-left: 80%;">
                                                <b>No of Records :
                                           <asp:Label Text="0" ID="lblcnt" runat="server" /></b>
                                            </div>
                                            <br />

                                            <asp:GridView ID="grdTraining" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="TraingID"
                                                EmptyDataText="There are no data records to display." CssClass="table table-bordered table-striped" OnPageIndexChanging="grdTraining_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No">
                                                        <ItemTemplate>

                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="TraingID" HeaderText="TraingID" Visible="false" ReadOnly="True" SortExpression="TraingID" />
                                                    <asp:BoundField DataField="ReqDate" HeaderText="Training Date" SortExpression="ReqDate" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <asp:BoundField DataField="Title" HeaderText="Training Title" SortExpression="Title" />
                                                    <%--<asp:BoundField DataField="Name" HeaderText="Employee Name" Visible="false"   SortExpression="Name" />--%>
                                                    <asp:TemplateField HeaderText="Course Content">
                                                        <ItemTemplate>

                                                            <asp:TextBox CssClass="form-control" ReadOnly="true" TextMode="MultiLine" Width="150px" Height="50px" Text='<%#Eval("CourseContent") %>' ID="txtCourseContent" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>

                                                            <asp:TextBox CssClass="form-control" TextMode="MultiLine" Width="150px" Height="50px" ReadOnly="true" Text='<%#Eval("Description") %>' ID="txtDes" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Type" HeaderText="Training Type" SortExpression="Type" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <div class="input-group">


                                                                <asp:ImageButton CommandArgument='<%#Eval("TraingID") %>' OnClick="imgatt_Click" ToolTip="Training Attendance" runat="server" ImageUrl="~/Images/a.png" ID="imgatt"
                                                                    Height="50px" Width="50px" />

                                                            </div>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                </Columns>
                                                <EmptyDataTemplate>No Record Exists........!!</EmptyDataTemplate>
                                            </asp:GridView>


                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane" id="tab_2">
                                    <div class="row">
                                        <div class="col-lg-12">




                                            <div style="padding-left: 80%;">
                                                <b>No of Records :
                                           <asp:Label Text="0" ID="lblTotalCnt" runat="server" /></b>
                                            </div>
                                            <asp:GridView ID="grdAttend" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowPaging="True" CssClass="table table-bordered table-striped" OnPageIndexChanging="grdAttend_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No">
                                                        <ItemTemplate>

                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ReqDate" HeaderText="Attendance Date" SortExpression="ReqDate" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <asp:BoundField DataField="Title" HeaderText="Training Title" SortExpression="Title" />
                                                    <asp:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="Name" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                                    <asp:BoundField DataField="CourseContent" HeaderText="Course Content" SortExpression="CourseContent" />
                                                    <asp:BoundField DataField="Trainer" HeaderText="Trainer" SortExpression="Trainer" />
                                                    <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                                                    <asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime" />
                                                    <asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="EndTime" />
                                                </Columns>
                                                <EmptyDataTemplate>No Record Exists........!!</EmptyDataTemplate>
                                            </asp:GridView>

                                            <br />


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>


                  
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <asp:ModalPopupExtender ID="modpop"
                    runat="server" DynamicServicePath="" PopupControlID="pann"
                    Enabled="True" TargetControlID="Label1">
                </asp:ModalPopupExtender>
                <br />
                <div runat="server" style="display: none" id="divmod">
                    <asp:Panel BorderColor="Black" BorderStyle="Solid" BackColor="White" ID="pann" aria-hidden="true" runat="server">
                        <table cellpadding="8" width="100%">
                            <caption>
                                <div class="panel-title">
                                    <h2>Training Attendance Details </h2>
                                </div>
                            </caption>
                            <tr>
                                <td style="float: right">
                                    <asp:ImageButton Height="30px" Width="30px" ImageUrl="~/Images/closebtn.png" ID="btnclose" runat="server" OnClick="btnclose_Click" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdEMP" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="TraingID"   
                                        EmptyDataText="There are no data records to display." CssClass="table table-bordered table-striped" AllowPaging="True" OnPageIndexChanging="grdEMP_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>

                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="ReqDate" HeaderText="Training Date" SortExpression="ReqDate" DataFormatString="{0:MM/dd/yyyy}" />
                                            <asp:BoundField DataField="Title" HeaderText="Training Title" SortExpression="Title" />
                                            <asp:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="Name" />
                                            <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId Name" Visible="false" SortExpression="EmployeeId" />

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>

                                                    <asp:DropDownList CssClass="form-control" ID="ddlStatus" runat="server">
                                                        <asp:ListItem Text="Present">Present</asp:ListItem>
                                                        <asp:ListItem Text="Absent">Absent</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>




                                        </Columns>
                                        <EmptyDataTemplate>No Record Exists........!!</EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <center>
                    <asp:Button ID="btnSave" CssClass="btn bg-blue-active"  runat="server" Text="Save" OnClick="btnSave_Click" />
                     <asp:Button ID="btncancel" CssClass="btn bg-blue-active"  runat="server" Text="cancel" OnClick="btncancel_Click" />
              </center>
                                </td>

                            </tr>
                        </table>

                    </asp:Panel>

                </div>
                          </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    
</asp:Content>

