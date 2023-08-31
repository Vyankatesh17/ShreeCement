using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class salary_process_ngp : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    HrPortalDtaClassDataContext context = new HrPortalDtaClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindDepartmentList();

                //txtFromDate.Attributes.Add("readonly", "readonly");
                //txtToDate.Attributes.Add("readonly", "readonly");
            }
        }
    }



    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
    }
    private void BindCompanyList()
    {
        try
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
            ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex) { }
    }
    private void BindDepartmentList()
    {
        ddlDepartment.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterDeptTBs where d.Status == 1 && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select new { d.DeptName, d.DeptID }).Distinct();

            if (data.Count() > 0)
            {
                ddlDepartment.DataSource = data;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }
            ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        //DateTime firstDayOfWeek = DateExtensions.FirstDateOfWeek(year, week, CultureInfo.CurrentCulture);
        //// 11/12/2012  
        //DateTime lastDayOfWeek = DateExtensions.FirstDateOfWeek(year, week + 1, CultureInfo.CurrentCulture).AddDays(-1);
        DateTime firstDayOfWeek = Convert.ToDateTime(txtFromDate.Text);
        // 11/12/2012  
        DateTime lastDayOfWeek = Convert.ToDateTime(txtToDate.Text);
        int counter = 0, totalEmployees = 0;
        var chkExists = context.EmployeeSalaryNgpTBs.Where(d => d.FromDate.Value.Date ==Convert.ToDateTime( txtFromDate.Text).Date
        &&d.ToDate.Value.Date==Convert.ToDateTime(txtToDate.Text).Date
        && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)
        ).Distinct();

        if (chkExists.Count() > 0)
        {
            gen.ShowMessage(this.Page, "Salary already prossessed for selected dates.. ");
        }
        else
        {
            //string[] parts1 = txtWeek.Text.Replace("-W", "-").Split('-');

            //var year = Convert.ToInt32(parts1[0]);
            //var week = Convert.ToInt32(parts1[1]);
            

            var desigSalData = (from d in context.DesigwiseSalaryTBs
                                join d1 in context.MasterDesgTBs on d.DesigId equals d1.DesigID
                                where d.TenantId == Convert.ToString(Session["TenantId"]) && d1.DeptID == Convert.ToInt32(ddlDepartment.SelectedValue)
                                && d.IsActive == true
                                select new
                                {
                                    d.SalaryPerDay,
                                    d.Id,
                                    d.DesigId
                                }
                               ).Distinct();

            //var attenData = GetAttendanceData(firstDayOfWeek, lastDayOfWeek);
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.AttendaceLogTBs
                            join emp in db.EmployeeTBs on d.EmployeeId equals emp.EmployeeId
                            where d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && emp.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)
                           && d.AttendanceDate.Date >= firstDayOfWeek.Date && d.AttendanceDate.Date <= lastDayOfWeek.Date

                            select d).Distinct();
                var empData = db.EmployeeTBs.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.IsActive == true).Distinct();
                totalEmployees = empData.Count();
                foreach (var item in empData)
                {
                    var desigSalary = desigSalData.Where(d => d.DesigId == item.DesgId).FirstOrDefault();
                    if (desigSalary != null)
                    {

                        var perDaySal = desigSalary.SalaryPerDay;

                        var presentDays = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "P").Count();
                        var absentDays = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "A").Count();
                        var holiDays = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "H").Count();
                        var holiPresentDays = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "HP").Count();
                        var outdoorDays = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "OD").Count();
                        var outdoorPresentDays = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "P(OD)").Count();
                        var manPresentDays = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "MP").Count();
                        var weeklyOffs = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "WO").Count();
                        var weekOffPresentDays = data.Where(d => d.EmployeeId == item.EmployeeId && (d.Status == "WP" || d.Status == "WOP")).Count();
                        var leaves = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "L").Count();
                        var workHome = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "WOH").Count();
                        var weekOffHalfDay = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "WO½P").Count();
                        var halfDay = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "½ P").Count();
                        var holidayHalfDay = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "H½P").Count();
                        var odHalfDay = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "½P(OD)").Count();

                        var OTHalfDays = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "P½P").Count();
                        var OTFullDays = data.Where(d => d.EmployeeId == item.EmployeeId && d.Status == "PP").Count();

                        if (OTHalfDays > 0)
                        {
                            double otindays = OTHalfDays * 1.5;
                            var otdata = otindays / 0.5;

                            halfDay = halfDay + Convert.ToInt32(otdata);
                        }


                        if (OTFullDays > 0)
                        {
                            presentDays = presentDays + OTFullDays * 2;
                        }



                        var fullDays = presentDays + holiDays + weeklyOffs + leaves + workHome + manPresentDays + outdoorDays + outdoorPresentDays;
                        var totalHalfDays = weekOffHalfDay + halfDay + holidayHalfDay + odHalfDay;
                        var otherFullDays = holiPresentDays + weekOffPresentDays;
                        var halfDaysCount = totalHalfDays > 0 ? totalHalfDays / 2 : 0;
                        var totalPresentDays = fullDays + otherFullDays + halfDaysCount;

                        EmployeeSalaryNgpTB employeeSalary = new EmployeeSalaryNgpTB();
                        employeeSalary.CompanyId = item.CompanyId;
                        employeeSalary.TenantId = item.TenantId;
                        employeeSalary.EmployeeId = item.EmployeeId;
                        employeeSalary.DeptId = item.DeptId;
                        employeeSalary.FromDate = firstDayOfWeek;
                        employeeSalary.ToDate = lastDayOfWeek;
                        //employeeSalary.WeekNo = txtWeek.Text;
                        employeeSalary.PresentDays = totalPresentDays;
                        employeeSalary.SalaryPerDay = perDaySal;
                        employeeSalary.TotalSalary = (perDaySal * totalPresentDays);
                        db.EmployeeSalaryNgpTBs.InsertOnSubmit(employeeSalary);
                        db.SubmitChanges();
                        counter++;
                    }
                }

                gen.ShowMessage(this.Page, counter + " employees out of " + totalEmployees + " from " + ddlDepartment.SelectedItem.Text + " department salary generated ..");
            }
        }
    }

    //private  IEnumerable<DateTime> GetDatesOfWeek(this DateTime date, CultureInfo ci)
    //{
    //    Int32 firstDayOfWeek = (Int32)ci.DateTimeFormat.FirstDayOfWeek;
    //    Int32 dayOfWeek = (Int32)date.DayOfWeek;
    //    DateTime startOfWeek = date.AddDays(firstDayOfWeek - dayOfWeek);
    //    var valuesDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<Int32>();
    //    return valuesDaysOfWeek.Select(v => startOfWeek.AddDays(v));
    //}
    //private  DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
    //{
    //    DateTime jan1 = new DateTime(year, 1, 1);
    //    int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
    //    DateTime firstWeekDay = jan1.AddDays(daysOffset);
    //    int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
    //    if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
    //    {
    //        weekOfYear -= 1;
    //    }
    //    return firstWeekDay.AddDays(weekOfYear * 7);
    //}

}
public static class DateExtensions
{
    // this method is borrowed from http://stackoverflow.com/a/11155102/284240
    public static int GetIso8601WeekOfYear(DateTime time)
    {
        DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        {
            time = time.AddDays(3);
        }

        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
    {
        DateTime jan1 = new DateTime(year, 1, 1);
        int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
        DateTime firstWeekDay = jan1.AddDays(daysOffset);
        int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
        {
            weekOfYear -= 1;
        }
        return firstWeekDay.AddDays(weekOfYear * 7);
    }
}