using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class attendance_report : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (gvAttendance.Rows.Count > 0)
        {
            //gvAttendance.UseAccessibleHeader = true;
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
                txtFromDate.Text = txtToDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                //ddlMonth.SelectedIndex = DateTime.Now.Month;
                BindYear();
                BindCompanyList();
                BindDepartmentList();
                //BindEmployeeList();
                //BindSimpleAttendance();

                ddlEmployee.SelectedValue = Convert.ToString(Session["UserId"]);
                if(Convert.ToString(Session["UserId"]).Equals("User"))
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
        catch (Exception ex) { litBody.Text = ex.Message; }
    }
    private void BindEmployeeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            DateTime todaysdt = DateTime.Now;
            var todaysmonth = todaysdt.Month;
            var todaysyear = todaysdt.Year;
            ddlEmployee.Items.Clear();

            DateTime todate = Convert.ToDateTime(txtToDate.Text);
            var tomonth = todate.Month;
            var toyear = todate.Year;

            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue):0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue):0;
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true 
                        && dt.TenantId==Convert.ToString(Session["TenantId"]) && dt.CompanyId==cId && dt.DeptId==dId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.RelivingDate, dt.RelivingStatus}).OrderBy(d => d.name);

            List<EmployeeTB> emplist = new List<EmployeeTB>();

            if (data != null && data.Count() > 0)
            {
                foreach (var item in data)
                {
                    EmployeeTB emp = new EmployeeTB();
                    if (item.RelivingStatus == 1)
                    {
                        DateTime relivingdate = Convert.ToDateTime(item.RelivingDate);
                        var relivingmonth = relivingdate.Month;
                        var relivingyear = relivingdate.Year;

                        if (relivingmonth == todaysmonth && relivingyear == todaysyear)
                        {
                            emp.EmployeeId = item.EmployeeId;
                            emp.FName = item.name;
                            emplist.Add(emp);
                        }
                        else if(todate <= relivingdate || tomonth == relivingmonth && toyear == relivingyear)
                        {
                            emp.EmployeeId = item.EmployeeId;
                            emp.FName = item.name;
                            emplist.Add(emp);
                        }
                    }
                    else
                    {
                        emp.EmployeeId = item.EmployeeId;
                        emp.FName = item.name;
                        emplist.Add(emp);
                    }
                }

                ddlEmployee.DataSource = emplist;
                ddlEmployee.DataTextField = "FName";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0,new ListItem( "--SELECT--","0"));
        }
        
    }
    private void BindYear()
    {
        //ddlYear.Items.Clear();
        //int year = DateTime.Now.AddYears(-75).Year;
        //for (int i = year; i < DateTime.Now.AddYears(2).Year; i++)
        //{
        //    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        //}
        //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }
    private void BindSimpleAttendance()
    {
        try
        {
            litBody.Text =  "";
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                string query = string.Format(@"SELECT        E.FName+' '+e.Lname AS EmpName, E.EmployeeNo, E.CompanyId, CI.CompanyName ,E.DeptId, D.DeptName,DE.DesigName, E.MachineID , DAY(AL.AttendanceDate) AS Day, CONVERT(datetime, AL.AttendanceDate, 101) AS Date, DATENAME(weekday, AL.AttendanceDate) AS DayofWeek, S.Shift AS ShiftName, AL.InTime AS PunchIn, AL.OutTime AS PunchOut, 
                         CONVERT(varchar(5), DATEADD(minute, DATEDIFF(MINUTE, S.Intime, S.Outtime), 0), 8) AS StanardWorkHours, CONVERT(varchar(5), DATEADD(minute, DATEDIFF(MINUTE, AL.InTime, AL.OutTime), 0), 8) AS ActualHours, 
                         S.Shifthours AS MinHours, AL.LateBy, AL.Status, AL.Remarks
FROM            AttendaceLogTB AS AL LEFT JOIN
                         MasterShiftTB AS S ON AL.ShiftId = S.ShiftID 
INNER JOIN EmployeeTB E ON AL.EmployeeId=E.EmployeeId 
INNER JOIN MasterDeptTB D ON D.DeptID = E.DeptId
INNER JOIN MasterDesgTB DE ON DE.DesigID = E.DesgId
INNER JOIN CompanyInfoTB CI ON CI.CompanyId = E.CompanyId
WHERE   (CONVERT(date,AL.AttendanceDate) BETWEEN CONVERT(date,'{0}') AND CONVERT(date,'{1}'))  AND AL.TenantId='{2}'", txtFromDate.Text, txtToDate.Text,
Convert.ToString(Session["TenantId"]));

                if (ddlEmployee.SelectedIndex > 0)
                {
                    query += " AND Al.EmployeeId=" + ddlEmployee.SelectedValue;
                }

                if (ddlCompany.SelectedIndex > 0)
                {
                    query += " AND Al.CompanyId=" + ddlCompany.SelectedValue;
                }
                if (ddlDepartment.SelectedIndex > 0)
                {
                    query += " AND E.DeptId=" + ddlDepartment.SelectedValue;
                }
                if (ddlShift.SelectedIndex > 0)
                {
                    query += " AND S.ShiftID=" + ddlShift.SelectedValue;
                }
                if (!string.IsNullOrEmpty(txtEmpCode.Text))
                {
                    query += " AND E.EmployeeNo='" + txtEmpCode.Text + "'";
                }
                if (Convert.ToInt32(Session["IsDeptHead"]) == 1)
                {
                    int manId = Convert.ToInt32(Session["EmpId"]);
                    query += " AND (E.ReportingStatus=" + manId + " OR E.EmployeeId=" + manId + ")";
                }

                int companyid = Convert.ToInt32(Session["CompanyID"]);
                if (Session["UserType"].ToString() == "LocationAdmin")
                {
                    query += " AND Al.CompanyId=" + companyid; 
                }

                query += " ORDER BY AL.AttendanceDate";
                DataTable data = gen.ReturnData(query);

                data.DefaultView.Sort = "CompanyId ASC, DeptName ASC, EmpName ASC";

                gvAttendance.DataSource = data;
                gvAttendance.DataBind();

                var holidays = GetCounts("H");
                var absents = GetCounts("A");
                var halfDay = GetCounts("½ P");
                var PaidLeaves = GetCounts("L");
                var OD = GetCounts("OD");
                var presents = GetCounts("P");
                var weekoffs = GetCounts("WO");
                var payabledays = 0;// DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex);
                var latemarks = GetLatemarkCounts();
                var lop = GetLOPCounts();

                var hday = halfDay / 2;
                var payDays = (payabledays) - (absents +hday);

                litBody.Text = string.Format(@"<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td>{10}</td>",
                    absents, halfDay, holidays, latemarks, PaidLeaves, lop, OD, presents, weekoffs, payabledays,payDays);
            }            
        }
        catch (Exception ex)
        {
            litBody.Text = ex.Message;
        }
    }
    private int GetCounts(string status)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var result = db.AttendaceLogTBs.Where(d => d.Status.Equals(status) && d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue) && d.AttendanceDate.Date >= Convert.ToDateTime(txtFromDate.Text) && d.AttendanceDate.Date <= Convert.ToDateTime(txtToDate.Text)).Count();
                return result;
            }
        }
        catch(Exception ex)
        {
            return 0;
        }
    }
    private double GetLOPCounts()
    {try
        {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var result = db.LeaveApplicationsTBs.Where(d => d.HrStatus == "Approve" && d.IsLossofPay == 1 && d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue)
            && (d.LeaveStartDate.Value.Date >=Convert.ToDateTime(txtFromDate.Text)) 
            && (d.LeaveEndDate.Value.Date <= Convert.ToDateTime(txtToDate.Text)))
            .Sum(s=>s.Duration).Value;

            return result;
        }
        }
        catch(Exception ex)
        {
            return 0;
        }
    }
    private double GetLatemarkCounts()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var result = db.AttendaceLogTBs.Where(d => Convert.ToInt32(d.LateBy) > 0 && d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue)
                && (d.AttendanceDate.Date >= Convert.ToDateTime(txtFromDate.Text))
                && (d.AttendanceDate.Date <= Convert.ToDateTime(txtToDate.Text)))
                .Count();

                return result;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
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
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        BindShiftList();
        //BindEmployeeList();
    }
    private void BindDepartmentList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlDepartment.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterDeptTBs
                        where d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeptName, d.DeptID }).Distinct();
            if (data != null && data.Count() > 0)
            {
                ddlDepartment.DataSource = data;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }
            ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    private void BindShiftList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlShift.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterShiftTBs
                        where d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.Shift, d.ShiftID }).Distinct();
            if (data != null && data.Count() > 0)
            {
                ddlShift.DataSource = data;
                ddlShift.DataTextField = "Shift";
                ddlShift.DataValueField = "ShiftID";
                ddlShift.DataBind();
            }
            ddlShift.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }



    private void BindCompanyList()
    {
        ddlCompany.Items.Clear();
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
        if (data != null && data.Count() > 0)
        {
            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
        }
        ddlCompany.Items.Insert(0, new ListItem("--All--", "0"));
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();       
    }
}