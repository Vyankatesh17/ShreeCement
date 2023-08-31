<%@ Page Title="Training Calender" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="TrainingCalender.aspx.cs" Inherits="TrainingCalender" %>

<%@ Register Namespace="DataControls" Assembly="DataCalendar" TagPrefix="dc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /* 
        Set the Style for parent CSS Class 
        of Calendar control 
        Parent [CssClass] = myCalendar 
    */
        .myCalendar {
            background-color: #efefef;
            width: 200px;
        }

            /* 
        Common style declaration for hyper linked text 
    */
            .myCalendar a {
                text-decoration: none;
            }

            /* 
        Styles declaration for top title 
        [TitleStyle] [CssClass] = myCalendarTitle 
    */
            .myCalendar .myCalendarTitle {
                font-weight: bold;
                background: url(../img/input-bg.png) repeat-x scroll center top #E8E8E8;
            }

            /* 
        Styles declaration for date cells 
        [DayStyle] [CssClass] = myCalendarDay 
    */
            .myCalendar td.myCalendarDay {
                font-size: 15px;
                font-weight: bold;
                border: solid 2px #fff;
                border-left: 0;
                border-top: 0;
            }

        td.myCalendarDay:hover {
            background: #ccc;
        }
        /* 
        Styles declaration for next/previous month links 
        [NextPrevStyle] [CssClass] = myCalendarNextPrev 
    */
        .myCalendar .myCalendarNextPrev {
            text-align: center;
        }

        /* 
        Styles declaration for Week/Month selector links cells 
        [SelectorStyle] [CssClass] = myCalendarSelector 
    */
        .myCalendar td.myCalendarSelector {
            background-color: #dddddd;
        }

        .myCalendar .myCalendarDay a, .myCalendar .myCalendarSelector a, .myCalendar .myCalendarNextPrev a {
            display: block;
            line-height: 18px;
        }

            .myCalendar .myCalendarDay a:hover, .myCalendar .myCalendarSelector a:hover {
                background-color: #cccccc;
            }

            .myCalendar .myCalendarNextPrev a:hover {
                background-color: #fff;
            }

        .headCal {
            width: 175px;
            padding: 10px 0 10px 20px;
            font-size: 12px;
            font-weight: bold;
            text-shadow: 1px 1px #fff;
            text-align: left;
            height: 20px;
            border-bottom: 1px solid #dcdcdc;
            background: url(img/headerbg.jpg) repeat left center;
            -moz-border-radius-topleft: 4px;
            -moz-border-radius-topright: 4px;
            -webkit-border-top-left-radius: 4px;
            -webkit-border-top-right-radius: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <div class="col-md-12">
            <div class="box box-primary">


                <dc:DataCalendar ID="cal1" runat="server" Width="100%" DayField="EventDay"
                    CssClass="table table-bordered table-striped" PrevMonthText="<img border='0' src='Images/navigate-left.png'>"
                    NextMonthText="<i class='fa fa-forward' border='0' src='Images/navigate-right.png'>">
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
        <a href="ViewTrainingRequestForm.aspx?id=<%# Container.DataItem["Training_Shedule_ID"] %>">Edit Appointment</a> 
            </fieldset>
                   </div>
                    </itemtemplate>
                </dc:DataCalendar>
            </div>
        </div>
</asp:Content>

