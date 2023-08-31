<%@ Page Title="Employee Calender" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="EmpCalendar.aspx.cs" Inherits="EmpCalendar" %>

<%@ Register Namespace="DataControls" Assembly="DataCalendar" TagPrefix="dc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  
        <div class="col-md-12">
            <div class="box box-primary">
                <dc:DataCalendar ID="cal1" runat="server" Width="100%" DayField="EventDay"
                    CssClass="myCalendar" PrevMonthText="<img border='0' src='Images/navigate-left.png'>"
                    NextMonthText="<img border='0' src='Images/navigate-right.png'>">
                    <SelectedDayStyle BackColor="DarkOrange" ForeColor="White" />
                    <DayStyle CssClass="myCalendarDay" HorizontalAlign="Right" VerticalAlign="Top" />
                    <SelectedDayStyle Font-Bold="True" Font-Size="12px" />
                    <SelectorStyle CssClass="myCalendarSelector" />
                    <NextPrevStyle CssClass="myCalendarNextPrev" />
                    <TitleStyle CssClass="myCalendarTitle" />
                    <DayHeaderStyle CssClass="th" />
                    <OtherMonthDayStyle BackColor="NavajoWhite" Font-Bold="False" ForeColor="DarkGray" />

                    <itemtemplate>
                   <br />
                   <div style="text-align:left;font-size:12px; width:100%;">
                   <fieldset class="fieldset">
                   <b>Date : &nbsp;<asp:Label ID="lbluser" runat="server" Text='<%# Container.DataItem["EventDay"] %>'></asp:Label></b>
                    <b>Training Subject :&nbsp;<asp:Label id="lblTitle" runat="server"
                               Font-Size="8" Font-Name="Arial"  Text='<%# Container.DataItem["Training_Topic"] %>' ForeColor="Maroon"  CssClass="pendek"/></b>
                    
                    <br /> <b>Training Time :     <asp:Label id="lbltime" runat="server"
                               Font-Size="8" Font-Name="Arial"  Text='<%# Container.DataItem["TrainingTime"] %>'  /></b>
        <b>Company Name :     <asp:Label id="Label2" runat="server" Font-Size="8" Font-Name="Arial"  Text='<%# Container.DataItem["CopmanyName"] %>' /></b>
         <b>Department Name :     <asp:Label id="Label1" runat="server" Font-Size="8" Font-Name="Arial"  Text='<%# Container.DataItem["DeptName"] %>' /></b>
       </fieldset>
                   </div>
                    </itemtemplate>
                </dc:DataCalendar>
            </div>
        </div>
    
</asp:Content>

