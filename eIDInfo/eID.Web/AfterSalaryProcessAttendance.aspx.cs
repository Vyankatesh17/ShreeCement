using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Threading;

public partial class AttendanceReport : System.Web.UI.Page
{
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                tblleave.Visible = false;
                tblAttend.Visible = false;
                bindemp();
                clear();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    private void bindemp()
    {
        var dt = from p in hr.EmployeeTBs select new { p.EmployeeId, name = p.FName + ' ' + p.MName + ' ' + p.Lname };
        ddEmp.DataSource = dt;
        ddEmp.DataTextField = "name";
        ddEmp.DataValueField = "EmployeeId";
        ddEmp.DataBind();
        ddEmp.Items.Insert(0, "--Select--");
    }

    private void clear()
    {
        foreach (var item in Page.Controls)
        {
            if (item is TextBox)
            {
                ((TextBox)item).Text = "";
            }
        }
    }

    private void fillyear()
    {
        DateTime dt = new DateTime();
        dt = System.DateTime.Now;
        DateTime newyear = dt.AddYears(10);
        ddyear.DataSource = newyear.ToString();
        // ddyear.DataTextField = ""; ddyear.DataValueField = "";
        ddyear.DataBind();

    }

    private string GetNumberOfSundays(Int32 Month, Int32 Year)
    {

        DateTime StartDate = Convert.ToDateTime(Month.ToString() + "/01/" + Year.ToString());

        Int32 iDays = DateTime.DaysInMonth(Year, Month);

        DateTime EndDate = StartDate.AddDays(iDays - 1);

        Int32 Count = 0;

        Int32 SundayCount = 0;

        while (StartDate.DayOfWeek != DayOfWeek.Sunday)
        {

            StartDate = StartDate.AddDays(1);

        }

        SundayCount = 1;

        StartDate = StartDate.AddDays(7);

        while (StartDate <= EndDate)
        {
            SundayCount += 1; StartDate = StartDate.AddDays(7);

        }

        return SundayCount.ToString();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        //cal days in month
        DataTable dtmon = g.ReturnData("Declare @month varchar(50) set @month='" + ddmonth.SelectedItem.Text + "'select case when @month='January' then '31' when @month='February' then '28' when @month='March' then '31' when @month='April' then '30' when @month='May' then '31' when @month='June' then '30' when @month='July' then '31' when @month='August' then '31' when @month='September' then '30' when @month='October' then '31'  when @month='November' then '30' when @month='December' then '31'   end as mon ");
        if (dtmon.Rows.Count > 0)
        {
            string days = dtmon.Rows[0]["mon"].ToString();
            if ((days) != "")
            {
                int cnt = Convert.ToInt32(days);
                for (int i = 1; i <= cnt; i++)
                {
                    #region Header 
                    //1st TD Header STyle.....
                    tddisp.Visible = true;
                    TextBox l = new TextBox();
                    l.Attributes.Add("style", "max-width:26px; repeat-x scroll center top #E8E8E8; border: 1px solid #CCCCCC; border-radius: 4px 4px 4px 4px;     display: block;    float: left;    margin: 0.17px;    padding: 5px 5px 4px;");
                    //l.Attributes.Add("style", "max-width:20px;");
                    l.Text = i.ToString();
                    l.ReadOnly = true;
                    l.ForeColor = Color.Black;
                    plc.Controls.Add(l);
                    #endregion

                    //Retriving Present Days of Employee
                    DataTable dt = g.ReturnData("select (DATEPART(DAY,Log_Date_Time)) as Present from LogRecordsDetails where Employee_id='" + ddEmp.SelectedValue + "' and  ((DATEPART(MONTH,Log_Date_Time)))='" + ddmonth.SelectedValue + "' and  ((DATEPART(year,Log_Date_Time)))='" + ddyear.SelectedValue + "'  and ((DATEPART(DAY,Log_Date_Time)))='" + i + "' ");
                    if (dt.Rows.Count > 0)
                    {
                        #region Present
                        DataSet ds = g.ReturnData1("SELECT distinct  EmployeeTB.FName + ' ' + EmployeeTB.Lname AS name, MonthlyAttendenceTB.WorkingDays, MonthlyAttendenceTB.PresentDays,                       MonthlyAttendenceTB.ApprovedLeaves, MonthlyAttendenceTB.AbsentDays, MonthlyAttendenceTB.Month, MonthlyAttendenceTB.year FROM         EmployeeTB INNER JOIN                      MonthlyAttendenceTB ON EmployeeTB.EmployeeId = MonthlyAttendenceTB.Employee_Id                       where MonthlyAttendenceTB.Employee_Id='" + ddEmp.SelectedValue + "' and MonthlyAttendenceTB.Month='" + ddmonth.SelectedValue + "' and MonthlyAttendenceTB.year='" + ddyear.SelectedValue + "'");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblempname.Text = ds.Tables[0].Rows[0]["name"].ToString();
                            lblPresentDays.Text = ds.Tables[0].Rows[0]["PresentDays"].ToString();
                            lblwDays.Text = ds.Tables[0].Rows[0]["WorkingDays"].ToString();
                            lblAbDays.Text = ds.Tables[0].Rows[0]["AbsentDays"].ToString();
                        }
                        var EmpData = (from d in hr.EmployeeTBs
                                       join n in hr.EmployeeSalarySettingsTBs on d.EmployeeId equals n.Empid
                                       where n.Empid == int.Parse(ddEmp.SelectedValue)
                                       select new
                                       {
                                           Name = d.FName + " " + d.MName + " " + d.Lname,
                                           d.FName,
                                           d.Lname,
                                           d.EmployeeId,
                                           n.EmpSalaryid,
                                           d.PFAccountNo,
                                           d.ESICAccountNo,
                                           d.Salarytype,
                                           d.BankName,
                                           d.SalaryAccountNo,
                                           GrossSalary = n.GrossSalary,
                                           Deduction = n.deductions,
                                           netpay1 = n.netpay

                                       });


                        int year = int.Parse(ddyear.SelectedItem.ToString());
                        int month = ddmonth.SelectedIndex;

                        int newdays = System.DateTime.DaysInMonth(year, month);
                        int a = 0;
                        string NoOfSundays = GetNumberOfSundays(month, year);
                        var NoOfApprovedLesves = (from m in hr.tblLeaveApplications
                                                  where m.employeeID == EmpData.First().EmployeeId && m.approval == "Approved"
                                                  && m.StartDate.Value.Date.Month == month
                                                  select m.AprovedDays).ToList();
                        foreach (var item in NoOfApprovedLesves)
                        {
                            a = a + item.Value;
                        }


                        //total sundays and approved
                        int TotalApproved = int.Parse(NoOfSundays) + a;
                        int TotalWorkingDays = newdays - TotalApproved;
                        int workingdays = newdays - int.Parse(NoOfSundays);

                        var totalpresentdays = (from m in hr.LogRecordsDetails
                                                where m.Log_Date_Time.Value.Month == month && m.Employee_id == EmpData.First().EmployeeId
                                                select m).Count();
                        int decductiondays = TotalWorkingDays - totalpresentdays;
                        lblDeduction.Text = decductiondays.ToString();
                        lblLeave.Text = a.ToString();
                        ViewState["workingdays"] = workingdays;
                        ViewState["PresentDays"] = totalpresentdays;
                        ViewState["AbsentDays"] = decductiondays;




                        tblAttend.Visible = true;
                        tblleave.Visible = true;
                        //Retireving Leave Details of Emp
                        // DataTable dtempDetails = g.ReturnData("SELECT     EmployeeTB.FName+' '+ EmployeeTB.MName+' '+ EmployeeTB.Lname EmpName, MonthlyAttendenceTB.WorkingDays, MonthlyAttendenceTB.PresentDays,                       MonthlyAttendenceTB.ApprovedLeaves, MonthlyAttendenceTB.AbsentDays FROM         EmployeeTB INNER JOIN                      LogRecordsDetails ON EmployeeTB.EmployeeId = LogRecordsDetails.Employee_id INNER JOIN                      MonthlyAttendenceTB ON EmployeeTB.EmployeeId = MonthlyAttendenceTB.Employee_Id where LogRecordsDetails.Employee_id='"+ddEmp.SelectedValue+"'");

                        TextBox lbl = new TextBox();
                        //lbl.Attributes.Add("style", "max-width:20px;");
                        lbl.Attributes.Add("style", "max-width:20px; repeat-x scroll center top #E8E8E8; border: 1px solid #CCCCCC; border-radius: 4px 4px 4px 4px;     display: block;    float: left;    margin: 0.17px;    padding: 5px 5px 4px;");
                        PlaceHolder1.Controls.Add(lbl);
                        lbl.Text = "P";
                        lbl.ReadOnly = true;
                        lbl.BackColor = Color.Green;
                        lbl.ForeColor = Color.Black;
                        lbl.ToolTip = i + " Present";
            #endregion Present
                    }
                  
                    else
                    {
                        #region Absent / Leave /Sundays
                        TextBox lbl = new TextBox();
                        //lbl.Attributes.Add("style", "max-width:20px;");
                        lbl.Attributes.Add("style", "max-width:25px; repeat-x scroll center top #E8E8E8; border: 1px solid #CCCCCC; border-radius: 4px 4px 4px 4px;     display: block;    float: left;    margin: 0.17px;    padding: 5px 5px 4px;");



                        //Leave Details..................
                        DataTable dtdetails = g.ReturnData("select  DATENAME(WEEKDAY,LeaveApllicationDate) as date , DATEPART(DAY, StartDate) as st,StartDate,EndDate from tblLeaveApplication where employeeID='" + ddEmp.SelectedValue + "'  and approval='Approved' and DATEPART(MONTH,tblLeaveApplication.StartDate)='" + ddmonth.SelectedValue + "'");
                        
                            int year = int.Parse(ddyear.SelectedItem.ToString());
                            int month = ddmonth.SelectedIndex;
                            int newdays = System.DateTime.DaysInMonth(year, month);
                            string NoOfSundays = GetNumberOfSundays(month, year);

                            //Textbox Leave
                            List<DateTime> dtfinal = new List<DateTime>();
                            List<DateTime> dates = new List<DateTime>();
                            int year1 = Convert.ToInt32(ddyear.SelectedValue);
                            int month1 = Convert.ToInt32(ddmonth.SelectedValue);
                            DayOfWeek day = DayOfWeek.Sunday;
                            System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                            for (int k = 1; k <= currentCulture.Calendar.GetDaysInMonth(year1, month1); k++)
                            {
                                DateTime d = new DateTime(year1, month1, k);
                                if (d.DayOfWeek == day)
                                {
                                    dates.Add(d);
                                }
                            }

                            if (dates.Count != null)
                            {
                                for (int t = 0; t < dates.Count; t++)
                                {
                                    string date = dates[t].Day.ToString();
                                    if (Convert.ToInt32(date) == i)
                                    {

                                        lbl.Text = "S";
                                        lbl.ReadOnly = true;
                                        lbl.BackColor = Color.Orange;
                                        lbl.ForeColor = Color.Black;
                                        lbl.ToolTip = i + " Sunday";
                                    }



                                }
                            }
                            if (dtdetails.Rows.Count > 0)
                            {

                            //list of leave days
                            DataTable ds = g.ReturnData("DECLARE @Start DATETIME, @End DATETIME SELECT @Start='" + dtdetails.Rows[0]["StartDate"] + "' , @End = '" + dtdetails.Rows[0]["EndDate"] + "'; WITH DateList AS(SELECT @Start [Date] UNION ALL SELECT [Date] +1 FROM DateList WHERE [Date] <@End) SELECT CONVERT(VARCHAR(10), [Date],120) [List of Date] FROM DateList");
                           
                                List<DateTime> leavedates = new List<DateTime>();


                                for (int p = 0; p < ds.Rows.Count; p++)
                                {
                                    leavedates.Add(DateTime.Parse(ds.Rows[p][0].ToString()));
                                }
                                if (dates.Count != null)
                                {
                                    for (int t = 0; t < leavedates.Count; t++)
                                    {
                                        string leavedates1 = leavedates[t].Day.ToString();
                                        if (Convert.ToInt32(leavedates1) == i)
                                        {
                                            lbl.Text = "L";
                                            lbl.ReadOnly = true;
                                            lbl.BackColor = Color.Yellow;
                                            lbl.ForeColor = Color.Black;
                                            lbl.ToolTip = i + " Leave";
                                        }
                                    }
                                }

                                for (int p = 0; p < ds.Rows.Count; p++)
                                {
                                    dtfinal.Add(DateTime.Parse(ds.Rows[p][0].ToString()));
                                }
                            }
                                
                                //for (int st = 0; st < cnt; st++)
                                //{


                                for (int k = 1; k <= currentCulture.Calendar.GetDaysInMonth(year1, month1); k++)
                                {
                                    DateTime d = new DateTime(year1, month1, k);
                                    if (d.DayOfWeek == day)
                                    {
                                        dtfinal.Add(d);
                                    }
                                }

                             
                                DataTable dt1 = g.ReturnData("select Log_Date_Time as Present from LogRecordsDetails where Employee_id='" + ddEmp.SelectedValue + "' and  ((DATEPART(MONTH,Log_Date_Time)))='" + ddmonth.SelectedValue + "' and  ((DATEPART(year,Log_Date_Time)))='" + ddyear.SelectedValue + "' ");

                                for (int p = 0; p < dt1.Rows.Count; p++)
                                {
                                    dtfinal.Add(DateTime.Parse(dt1.Rows[p][0].ToString()));
                                }

                                for (int t = 0; t < dtfinal.Count; t++)
                                {
                                    string presentdays = dtfinal[t].Day.ToString();
                                    if (Convert.ToInt32(presentdays) != i)
                                    {

                                        if (lbl.Text != "S" && lbl.Text != "L" && lbl.Text != "P")
                                        {
                                            //Absent
                                            lbl.Text = "A";
                                            lbl.ReadOnly = true;
                                            lbl.BackColor = Color.Red;
                                            lbl.ForeColor = Color.Black;
                                            lbl.ToolTip = i + " Absent";
                                        }


                                    }
                                }

                                PlaceHolder1.Controls.Add(lbl);
                           
                        
                        #endregion
                    }

                }
            }
        }
        //remove textbox 
        TextBox txt = new TextBox();
        PlaceHolder1.Controls.Remove(txt);
    }

}