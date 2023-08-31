using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Globalization;

public partial class AttendanceReport : System.Web.UI.Page
{
    #region Variable Declartion
    Genreal g = new Genreal();
    string date = "";
    DataTable dtd = new DataTable();

    DataTable dtMonthData;
    int year = 0, leaves = 0, presentdays = 0, absentdays = 0, holi = 0, sun = 0, leavedays = 0, WOCount = 0;

    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                tblAttend.Visible = false;

                fillcompany();
                clear();
                ddyear.SelectedIndex = -1;
                ddyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;

            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    private void bindemp()
    {
        ddEmp.Items.Clear();
        var dt = from p in hr.EmployeeTBs where p.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select new { p.EmployeeId, name = p.FName + ' ' + p.MName + ' ' + p.Lname };
        ddEmp.DataSource = dt;
        ddEmp.DataTextField = "name";
        ddEmp.DataValueField = "EmployeeId";
        ddEmp.DataBind();
        ddEmp.Items.Insert(0, "--Select--");
    }
    private void fillcompany()
    {
        var data = from dt in hr.CompanyInfoTBs
                   where dt.Status == 0
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }
    }
    private void clear()
    {
        lblLeave.Text = "";

        ddlCompany.SelectedIndex = 0;
        ddEmp.SelectedIndex = 0;
        ddmonth.SelectedIndex = 0;
        ddyear.SelectedIndex = 0;
        txtabsentdays.Text = "0";
        txtpresentdays.Text = "0";
        txtsundayandholiday.Text = "0";
        tblAttend.Visible = false;

        dtMonthData = null;
        datalistdisplay.DataSource = dtMonthData;
        datalistdisplay.DataBind();
        ViewState["dtmonthdata"] = null;
    }

    private void fillyear()
    {
        int i = int.Parse(DateTime.Now.AddYears(-1).Date.Year.ToString());

        DataTable dtadd = new DataTable();
        dtadd.Columns.Add("Year");

        for (int j = 0; j <= 10; j++)
        {
            DataRow dr = dtadd.NewRow();
            dr[0] = i.ToString();

            i++;
            dtadd.Rows.Add(dr);

        }

        ddyear.DataSource = dtadd;
        ddyear.DataTextField = "Year";
        ddyear.DataValueField = "Year";
        ddyear.DataBind();
        ddyear.Items.Insert(0, "--Select--");

    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["chk"] = "1";
        year = Convert.ToInt32(ddyear.SelectedValue);
        date = ddmonth.SelectedValue + '/' + 1 + '/' + year;
        tblAttend.Visible = true;

        calculateAttendance();

    }
    int weekoffs = 0, pDays = 0, Holdays = 0, Leaves = 0, aDays = 0;
    private void calculateAttendance()
    {
        try
        {
            #region
            dtMonthData = new DataTable();

            dtMonthData = new DataTable();
            DataColumn drmondays = new DataColumn("Days");
            dtMonthData.Columns.Add(drmondays.ToString());
            int k = System.DateTime.DaysInMonth(Convert.ToInt32(ddyear.SelectedValue), Convert.ToInt32(ddmonth.SelectedValue));
            for (int j = 1; j <= k; j++)
            {
                DataRow dr = dtMonthData.NewRow();
                dr[0] = j;
                dtMonthData.Rows.Add(dr);
            }

            datalistdisplay.DataSource = dtMonthData;
            datalistdisplay.DataBind();

            ViewState["dtmonthdata"] = dtMonthData;


            var attendancedetailsdata = (from d in hr.AttendanceDetailsTBs where d.MonthID == Convert.ToInt32(ddmonth.SelectedValue) && d.Year == Convert.ToInt32(ddyear.SelectedValue) && d.EmpID == Convert.ToInt32(ddEmp.SelectedValue) select d).ToList();
            if (attendancedetailsdata.Count() > 0)
            {
                int i = 0;

                foreach (DataListItem dlistItem in datalistdisplay.Items)
                {
                    DropDownList ddl = (DropDownList)datalistdisplay.Items[dlistItem.ItemIndex].FindControl("ddlist");

                    ddl.SelectedValue = attendancedetailsdata.ElementAt(i).Type.ToString();
                    i++;
                }
                BindDaysCount();
            }
            else
            {
                bindAttendanceStatus();
            }
            #endregion
        }
        catch
        {

        }
    }

    private void bindAttendanceStatus()
    {
        try
        {
            CultureInfo ci = new CultureInfo("en-US");
            year = Convert.ToInt32(ddyear.SelectedValue);
            date = ddmonth.SelectedValue + '/' + 1 + '/' + year;
            int month = Convert.ToInt32(ddmonth.SelectedValue);


            dtMonthData = (DataTable)ViewState["dtmonthdata"];
            for (int i = 1; i <= dtMonthData.Rows.Count; i++)
            {
                DropDownList ddl = (DropDownList)datalistdisplay.Items[i - 1].FindControl("ddlist");


               // DataSet dsP = g.ReturnData1("select * from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + i + "'  and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "' and DATEPART(Year, Log_Date_Time)='" + year + "'   and status is null  and Employee_id='" + ddEmp.SelectedValue + "'");
                var dataP = (from d in hr.BeforeSalaryPresentySp(Convert.ToInt32(ddEmp.SelectedValue), i, month, year, 1) select d).ToList();  
                if (dataP.Count > 0)
                {//Present

                    if (ddl.SelectedValue.ToString() == "--Select--")
                    {
                        ddl.SelectedValue = "P";
                        ddl.BackColor = Color.Green;
                    }
                    else
                    {
                        ddl.SelectedValue = "HW";
                        ddl.BackColor = Color.Orange;
                    }

                }
             //   DataSet dsMP = g.ReturnData1("select * from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + i + "'  and status='MP' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and DATEPART(Year, Log_Date_Time)='" + year + "'  and Employee_id='" + ddEmp.SelectedValue + "'");
                var dataMP = (from d in hr.BeforeSalaryPresentySp(Convert.ToInt32(ddEmp.SelectedValue), i, month, year, 2) select d).ToList();
                if (dataMP.Count > 0)               
                {//MP
                    if (ddl.SelectedValue.ToString() == "--Select--")
                    {
                        ddl.SelectedValue = "MP";
                        ddl.BackColor = Color.Green;
                    }
                    else
                    {
                        ddl.SelectedValue = "HW";
                        ddl.BackColor = Color.Orange;
                    }

                }
               // DataSet dsMH = g.ReturnData1("select * from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + i + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MH' and DATEPART(Year, Log_Date_Time)='" + year + "' and Employee_id='" + ddEmp.SelectedValue + "'");
                var dataMH = (from d in hr.BeforeSalaryPresentySp(Convert.ToInt32(ddEmp.SelectedValue), i, month, year, 3) select d).ToList();
                if (dataMH.Count > 0)     
                {//MH
                    if (ddl.SelectedValue.ToString() == "--Select--")
                    {
                        ddl.SelectedValue = "MH";
                        ddl.BackColor = Color.Green;
                    }
                    else
                    {
                        ddl.SelectedValue = "HW";
                        ddl.BackColor = Color.Orange;
                    }
                }

                DataSet dsHolidays = g.ReturnData1("select CONVERT(varchar,Date,101) from HoliDaysMaster where datepart(month,Date)='" + ddmonth.SelectedValue + "' and datepart(Year,Date)='" + year + "' and datepart(day,Date)='" + i + "'");

                if (dsHolidays.Tables[0].Rows.Count > 0)
                {//HOLIDAYS
                    if (ddl.SelectedValue.ToString() == "--Select--")
                    {
                        ddl.SelectedValue = "H";
                        ddl.BackColor = Color.Orange;
                    }
                    else
                    {
                        ddl.SelectedValue = "HW";
                        ddl.BackColor = Color.Orange;
                    }
                }
            }
            for (int i = 1; i <= dtMonthData.Rows.Count; i++)
            {
                DropDownList ddl = (DropDownList)datalistdisplay.Items[i - 1].FindControl("ddlist");
                DataSet dsFrm = g.ReturnData1(@"SELECT        LeaveApplicationID, LeaveID, LeaveApllicationDate, StartDate, EndDate, Duration, AprovedDays, Purpose, employeeID, approval, LeaveApplicationID1, 
                         ManagerStatus, CancelDateFrom, CancelDateTo, CancelDuration, CancelReason, CancelDATE, HRDirectApprove, EntryBy
FROM            tblLeaveApplication AS l
WHERE        ('" + new DateTime(year, month, i).Date.ToString("MM/dd/yyyy") + "' BETWEEN StartDate AND EndDate) AND (DATEPART(month, StartDate) = '" + month + "') AND (DATEPART(Year, StartDate) = '" + year + "') AND (DATEPART(month,  EndDate) = '" + month + "') AND (DATEPART(Year, EndDate) = '" + year + "') AND (employeeID = '" + ddEmp.SelectedValue + "')  AND (managerstatus='Approved' or HRDirectApprove='Approved')");
                if (dsFrm.Tables[0].Rows.Count > 0)
                {//LEAVES
                    ddl.SelectedValue = "L";
                    ddl.BackColor = Color.Yellow;
                }
                else
                {
                    if (ddl.SelectedValue.ToString() == "--Select--")
                    {
                        ddl.SelectedValue = "A";
                        ddl.BackColor = Color.Red;
                    }
                }
            }

            // using roster master we get Weekly off 
            for (int i = 1; i <= dtMonthData.Rows.Count; i++)
            {
                DropDownList ddl = (DropDownList)datalistdisplay.Items[i - 1].FindControl("ddlist");
                

                var rosterdata = (from d in hr.BeforeSalaryRosterDetails(i, month, year, Convert.ToInt32(ddEmp.SelectedValue)) select d).ToList();
                if (rosterdata.Count() > 0)
                {
                    ddl.SelectedValue = "WO";
                    ddl.BackColor = Color.YellowGreen;
                }
                else
                {

                    DataTable dtcheck = g.ReturnData("select empid from rosterMastertb where empid=" + Convert.ToInt32(ddEmp.SelectedValue) + " and monthid="+month+" and year='"+year+"'");

                    if (dtcheck.Rows.Count == 0)
                    {
                        string weeklyOffs = @"Select distinct Days,TrackHolidays from weeklyofftb where companyid='" + Convert.ToInt32(ddlCompany.SelectedValue) + "'";

                        DataSet dsholidays = g.ReturnData1(weeklyOffs);

                        for (int offs = 0; offs < dsholidays.Tables[0].Rows.Count; offs++)
                        {
                            string day = dsholidays.Tables[0].Rows[offs]["days"].ToString();
                            string offdays = dsholidays.Tables[0].Rows[offs]["TrackHolidays"].ToString();
                            setHolidayStatus(day, offdays);
                        }
                    }
                }
            }

            BindDaysCount();

        }
        catch
        {

        }
    }
    private void setHolidayStatus(string day, string offdays)
    {
        try
        {
            CultureInfo ci = new CultureInfo("en-US");
            year = Convert.ToInt32(ddyear.SelectedValue);
            date = ddmonth.SelectedValue + '/' + 1 + '/' + year;
            int month = Convert.ToInt32(ddmonth.SelectedValue);
            dtMonthData = (DataTable)ViewState["dtmonthdata"];


            for (int i = 1; i <= dtMonthData.Rows.Count; i++)
            {
                DropDownList ddl = (DropDownList)datalistdisplay.Items[i - 1].FindControl("ddlist");
                if (new DateTime(year, month, i).DayOfWeek.ToString() == day)
                {
                    if (offdays == "All")
                    {
                        weekoffs++;
                        ddl.SelectedValue = "WO";
                        ddl.BackColor = Color.YellowGreen;
                    }
                    else if (offdays == "1 & 2")
                    {

                        DateTime dtNow = new DateTime(year, month, i);
                        string[] days = GetDatesOfSundays(year, month, day).Split(',');
                        if (Convert.ToDateTime(days[0]).Date == dtNow.Date || Convert.ToDateTime(days[1]).Date == dtNow.Date)
                        {
                            weekoffs++;
                            ddl.SelectedValue = "WO";
                            ddl.BackColor = Color.YellowGreen;
                        }
                        //if()
                    }
                    else if (offdays == "2 & 3")
                    {
                        DateTime dtNow = new DateTime(year, month, i);
                        string[] days = GetDatesOfSundays(year, month, day).Split(',');
                        if (Convert.ToDateTime(days[1]).Date == dtNow.Date || Convert.ToDateTime(days[2]).Date == dtNow.Date)
                        {
                            weekoffs++;
                            ddl.SelectedValue = "WO";
                            ddl.BackColor = Color.YellowGreen;
                        }
                    }
                    else if (offdays == "3 & 4")
                    {
                        DateTime dtNow = new DateTime(year, month, i);
                        string[] days = GetDatesOfSundays(year, month, day).Split(',');
                        if (Convert.ToDateTime(days[2]).Date == dtNow.Date || Convert.ToDateTime(days[3]).Date == dtNow.Date)
                        {
                            weekoffs++;
                            ddl.SelectedValue = "WO";
                            ddl.BackColor = Color.YellowGreen;
                        }
                    }
                    else if (offdays == "1 & 3")
                    {
                        DateTime dtNow = new DateTime(year, month, i);
                        string[] days = GetDatesOfSundays(year, month, day).Split(',');
                        if (Convert.ToDateTime(days[0]).Date == dtNow.Date || Convert.ToDateTime(days[2]).Date == dtNow.Date)
                        {
                            weekoffs++;
                            ddl.SelectedValue = "WO";
                            ddl.BackColor = Color.YellowGreen;
                        }
                    }
                    else if (offdays == "2 & 4")
                    {
                        DateTime dtNow = new DateTime(year, month, i);

                        string[] days = GetDatesOfSundays(year, month, day).Split(',');
                        if (Convert.ToDateTime(days[1]).Date == dtNow.Date || Convert.ToDateTime(days[3]).Date == dtNow.Date)
                        {
                            weekoffs++;
                            ddl.SelectedValue = "WO";
                            ddl.BackColor = Color.YellowGreen;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }


    #region getDayNumbers

    protected string GetDatesOfSundays(int year, int month, string dayName)
    {
        string sReturn = "";
        CultureInfo ci = new CultureInfo("en-US");
        for (int i = 1; i <= ci.Calendar.GetDaysInMonth(year, month); i++)
        {
            if (new DateTime(year, month, i).DayOfWeek.ToString() == dayName)
            {
                // Response.Write(i.ToString() + ",");
                if (sReturn.Length > 0) sReturn += ",";
                sReturn += new DateTime(year, month, i).ToShortDateString();
            }
        }
        return sReturn;
    }


    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {

            int count = 0;
            foreach (DataListItem item in datalistdisplay.Items)
            {
                DropDownList ddlist = (DropDownList)datalistdisplay.Items[item.ItemIndex].FindControl("ddlist");
                if (ddlist.SelectedValue.ToString() == "--Select--")
                {
                    count = 1;
                    break;
                }
            }         


            if (count == 0)
            {
                int daycount = System.DateTime.DaysInMonth(int.Parse(ddyear.SelectedItem.Text), (ddmonth.SelectedIndex));
                var checkdataExist = (from m in hr.BeforeSalProcessTBs
                                      where m.empid == Convert.ToInt32(ddEmp.SelectedValue) && m.month == (ddmonth.SelectedValue) && (m.year == ddyear.SelectedValue)
                                      select m).ToList();

                if (checkdataExist.Count() == 0)
                {
                    BeforeSalProcessTB tb = new BeforeSalProcessTB();
                    tb.empid = Convert.ToInt32(ddEmp.SelectedValue);
                    tb.month = (ddmonth.SelectedValue);
                    tb.ApprovedLeaves = lblLeave.Text;
                    tb.year = ddyear.SelectedValue;

                    tb.MonthDays = daycount.ToString();
                    tb.SundayHolidays = txtsundayandholiday.Text;
                    tb.absentdays = txtabsentdays.Text;
                    tb.presentdays = txtpresentdays.Text;
                    tb.workingdays = Convert.ToString(Convert.ToInt32(txtabsentdays.Text) + Convert.ToInt32(txtpresentdays.Text));

                    hr.BeforeSalProcessTBs.InsertOnSubmit(tb);
                    hr.SubmitChanges();

                    MonthlyAttendenceTB MTB = new MonthlyAttendenceTB();
                    MTB.Employee_Id = Convert.ToInt32(ddEmp.SelectedValue);
                    MTB.Month = (ddmonth.SelectedValue);
                    MTB.year = ddyear.SelectedValue;
                    MTB.WorkingDays = tb.workingdays.ToString();
                    MTB.PresentDays = tb.presentdays.ToString();
                    //  MTB.ApprovedLeaves = a.ToString();
                    MTB.AbsentDays = tb.absentdays.ToString();

                    hr.MonthlyAttendenceTBs.InsertOnSubmit(MTB);
                    hr.SubmitChanges();

                    foreach (DataListItem item in datalistdisplay.Items)
                    {
                        Label objLabel = item.FindControl("lbldays") as Label;

                        DropDownList subobjHD = item.FindControl("ddlist") as DropDownList;

                        AttendanceDetailsTB AtDetails = new AttendanceDetailsTB();

                        AtDetails.MonthID = Convert.ToInt32(ddmonth.SelectedValue);
                        AtDetails.Year = Convert.ToInt32(ddyear.SelectedValue);
                        AtDetails.EmpID = Convert.ToInt32(ddEmp.SelectedValue);
                        AtDetails.Day = Convert.ToInt32(objLabel.Text.ToString());
                        AtDetails.Type = subobjHD.Text.ToString();

                        hr.AttendanceDetailsTBs.InsertOnSubmit(AtDetails);
                        hr.SubmitChanges();

                    }


                    g.ShowMessage(this.Page, "Data saved successfully....!!!");
                    clear();
                }
                else
                {
                    // clear();
                    g.ShowMessage(this.Page, "Record already Exists ....!!!");

                }
            }
            else
            {
                g.ShowMessage(this.Page, "Please select days status");
            }
        }
        catch
        {

        }
    }

    protected void ddlist_selectedindexchanged(object sender, EventArgs e)
    {

        BindDaysCount();

    }

    // Here we get all the days calculation
    private void BindDaysCount()
    {
        try
        {
            int presentDays = 0, absentDays = 0, holidayworkingDays = 0, leaveDays = 0;
            foreach (DataListItem item in datalistdisplay.Items)
            {
                DropDownList ddlist = (DropDownList)datalistdisplay.Items[item.ItemIndex].FindControl("ddlist");

                if (ddlist.SelectedValue.ToString() == "P" || ddlist.SelectedValue.ToString() == "MP" || ddlist.SelectedValue.ToString() == "HW")
                {
                    ddlist.BackColor = Color.Green;
                    presentDays = presentDays + 1;
                }
                else if (ddlist.SelectedValue.ToString() == "MH" || ddlist.SelectedValue.ToString() == "H" || ddlist.SelectedValue.ToString() == "WO")
                {
                    ddlist.BackColor = Color.Orange;
                    holidayworkingDays = holidayworkingDays + 1;
                }
                else if (ddlist.SelectedValue.ToString() == "L")
                {
                    ddlist.BackColor = Color.Yellow;
                    leaveDays = leaveDays + 1;
                }
                else if (ddlist.SelectedValue.ToString() == "A")
                {
                    ddlist.BackColor = Color.Red;
                    absentDays = absentDays + 1;
                }

            }
            txtabsentdays.Text = absentDays.ToString();
            txtpresentdays.Text = presentDays.ToString();
            txtsundayandholiday.Text = holidayworkingDays.ToString();
            lblLeave.Text = leaveDays.ToString();
        }
        catch
        {

        }
    }

    protected void ddEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        // lblDeductions.Text = "0";
        lblLeave.Text = "0";
        txtpresentdays.Text = "0";
        txtabsentdays.Text = "0";
        txtsundayandholiday.Text = "0";

        dtMonthData = null;
        datalistdisplay.DataSource = dtMonthData;
        datalistdisplay.DataBind();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            bindemp();
        }
        lblLeave.Text = "0";
        txtpresentdays.Text = "0";
        txtabsentdays.Text = "0";
        txtsundayandholiday.Text = "0";

        dtMonthData = null;
        datalistdisplay.DataSource = dtMonthData;
        datalistdisplay.DataBind();
    }
    protected void txtpresentdays_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int daycount = System.DateTime.DaysInMonth(int.Parse(ddyear.SelectedItem.Text), (ddmonth.SelectedIndex));
            if (!string.IsNullOrEmpty(txtabsentdays.Text) && !string.IsNullOrEmpty(txtpresentdays.Text))
            {
                ViewState["chk"] = "0";


                int totaldayscount = Convert.ToInt32(txtpresentdays.Text) + Convert.ToInt32(txtsundayandholiday.Text);
                if (daycount >= totaldayscount)
                {
                    txtabsentdays.Text = Convert.ToString(daycount - (Convert.ToInt32(txtpresentdays.Text) + Convert.ToInt32(txtsundayandholiday.Text)));
                    if (Convert.ToInt32(txtabsentdays.Text) < 0)
                    {
                        ViewState["chk"] = "1";

                    }
                }
                else
                {
                    txtpresentdays.Text = Convert.ToString(daycount - (Convert.ToInt32(txtabsentdays.Text) + Convert.ToInt32(txtsundayandholiday.Text)));
                }
            }
        }
        catch
        {

        }
    }
    protected void txtabsentdays_TextChanged(object sender, EventArgs e)
    {
        int daycount = System.DateTime.DaysInMonth(int.Parse(ddyear.SelectedItem.Text), (ddmonth.SelectedIndex));
        if (!string.IsNullOrEmpty(txtabsentdays.Text) && !string.IsNullOrEmpty(txtpresentdays.Text))
        {
            ViewState["chk"] = "0";
            // DisplayAllData();

            int totaldayscount = Convert.ToInt32(txtabsentdays.Text) + Convert.ToInt32(txtsundayandholiday.Text);
            if (daycount >= totaldayscount)
            {
                txtpresentdays.Text = Convert.ToString(daycount - (Convert.ToInt32(txtabsentdays.Text) + Convert.ToInt32(txtsundayandholiday.Text)));
                if (Convert.ToInt32(txtpresentdays.Text) < 0)
                {
                    ViewState["chk"] = "1";
                    // DisplayAllData();
                }
            }
            else
            {
                txtabsentdays.Text = Convert.ToString(daycount - (Convert.ToInt32(txtpresentdays.Text) + Convert.ToInt32(txtsundayandholiday.Text)));

            }
        }
    }
    protected void ddmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblLeave.Text = "0";
        txtpresentdays.Text = "0";
        txtabsentdays.Text = "0";
        txtsundayandholiday.Text = "0";

        dtMonthData = null;
        datalistdisplay.DataSource = dtMonthData;
        datalistdisplay.DataBind();
    }
    protected void ddyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblLeave.Text = "0";
        txtpresentdays.Text = "0";
        txtabsentdays.Text = "0";
        txtsundayandholiday.Text = "0";

        dtMonthData = null;
        datalistdisplay.DataSource = dtMonthData;
        datalistdisplay.DataBind();
    }
}