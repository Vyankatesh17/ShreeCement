using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class attendance_latemark_report : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (gvAttendance.Rows.Count > 0)
        {
            gvAttendance.UseAccessibleHeader = true;
            gvAttendance.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                ddlMonth.SelectedIndex = DateTime.Now.Month;
                BindYear();
                BindEmployeeList();
                BindSimpleAttendance();

                ddlEmployee.SelectedValue = Convert.ToString(Session["UserId"]);
                if (Convert.ToUInt32(Convert.ToString(Session["UserId"])) > 1)
                {
                    ddlEmployee.Enabled = false;
                }
                else { ddlEmployee.Enabled = true; }
            }
            BindJqFunctions();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindSimpleAttendance();
        }
        catch (Exception ex) {  }
    }
    protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvAttendance.Rows.Count > 0)
        {
            gvAttendance.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvAttendance.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }
    private void BindEmployeeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true && dt.RelivingStatus == null
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId }).OrderBy(d => d.name);
            if (data != null && data.Count() > 0)
            {
                ddlEmployee.DataSource = data;
                ddlEmployee.DataTextField = "name";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }

    }
    private void BindYear()
    {
        ddlYear.Items.Clear();
        int year = DateTime.Now.AddYears(-75).Year;
        for (int i = year; i < DateTime.Now.AddYears(2).Year; i++)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }
    private void BindSimpleAttendance()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {

                DateTime startDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                int days = DateTime.DaysInMonth(startDate.Year, startDate.Month);

                string query = string.Format(@"SELECT        DAY(AL.AttendanceDate) AS Day, CONVERT(varchar, AL.AttendanceDate, 101) AS Date, DATENAME(weekday, AL.AttendanceDate) AS DayofWeek, S.Shift AS ShiftName, AL.InTime AS PunchIn, AL.OutTime AS PunchOut, 
                         CONVERT(varchar(5), DATEADD(minute, DATEDIFF(MINUTE, S.Intime, S.Outtime), 0), 8) AS StanardWorkHours, CONVERT(varchar(5), DATEADD(minute, DATEDIFF(MINUTE, AL.InTime, AL.OutTime), 0), 8) AS ActualHours, 
                         S.Shifthours AS MinHours, AL.LateBy, AL.Status, AL.Remarks
FROM            AttendaceLogTB AS AL LEFT OUTER JOIN
                         MasterShiftTB AS S ON AL.ShiftId = S.ShiftID
WHERE        (AL.InTime > S.LateMarkStart) AND  (MONTH(AL.AttendanceDate) = {0}) AND (YEAR(AL.AttendanceDate) = {1})", ddlMonth.SelectedIndex, ddlYear.SelectedValue);

                if (ddlEmployee.SelectedIndex > 0)
                {
                    query += " AND Al.EmployeeId=" + ddlEmployee.SelectedValue;
                }
                DataTable data = gen.ReturnData(query);

                gvAttendance.DataSource = data;
                gvAttendance.DataBind();

            }

        }
        catch (Exception ex)
        {

        }
    }
}