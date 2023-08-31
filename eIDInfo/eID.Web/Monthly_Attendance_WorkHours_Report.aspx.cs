using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Monthly_Attendance_WorkHours_Report : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                ddlMonth.SelectedIndex = DateTime.Now.Month;
                BindYear();
                fillcompany();
                //BindSimpleAttendance();

                if (Session["UserType"].ToString() != "User")
                {
                    company.Visible = true;
                    department.Visible = true;
                    employee.Visible = true;
                }
            }
        }
        //if (gvAttendance.Rows.Count > 0)
        //    gvAttendance.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    private void fillcompany()
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
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindSimpleAttendance();
            //BindSimpleAttendance1();
        }
        catch (Exception ex) { litBody.Text = ex.Message; }
    }

    private void BindSimpleAttendance()
    {
        try
        {
            bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
            //var companyid = Session["CompanyID"].ToString();

            var companyid = ddlCompany.SelectedValue;
            litBody.Text = litHeaders.Text = "";
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                DateTime startDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                int days = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                //var WeekOffList = (from d in db.WeeklyOffTBs where d.Effectdate >= startDate.Date select d).Distinct();
                //foreach (var wo in WeekOffList)
                //{

                //}

                string query = string.Format(@"EXEC sp_month_attendance_Work_Hours '{0}','{1}','{2}','{3}'", startDate.ToString("MM/dd/yyyy"), endDate.ToString("MM/dd/yyyy"), ddlCompany.SelectedValue,
                    Convert.ToString(Session["TenantId"]));
                DataTable data = gen.ReturnData(query);

                var shiftdata = db.MasterShiftTBs.Where(a => a.CompanyId == Convert.ToInt32(companyid) && a.IsDefault == true).FirstOrDefault();

                DataTable dataTable = new DataTable();
                int i = 0, empid = 0;
                int totalCols = data.Columns.Count;
                string[] arrray = data.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
                //litTest.Text = string.Format("columns : {0} and rows : {1} and array : {2}, query : {3}",
                //    data.Columns.Count, data.Rows.Count, arrray.ToList(), query);
                foreach (DataColumn col in data.Columns)
                {
                    string column = col.ColumnName;
                    if (i > 2)
                    {
                        try
                        {
                            column = Convert.ToDateTime(column).Date.Day.ToString();
                        }
                        catch (Exception ex)
                        {
                            column = col.ColumnName;
                        }
                    }
                    i++;
                    dataTable.Columns.Add(column);
                    litHeaders.Text = litHeaders.Text + string.Format(@"<th>" + column + "</th>");
                }

                foreach (DataRow row in data.Rows)
                {
                    bool flag = false;
                    string eId = row.ItemArray[0].ToString();
                    EmployeeTB eData = db.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(eId)).FirstOrDefault();
                    if (ddlDepartment.SelectedIndex > 0)
                    {
                        flag = eData.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue) ? false : true;
                    }
                    if (ddlEmployee.SelectedIndex > 0)
                    {
                        flag = eData.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue) ? false : true;
                    }
                    if (!string.IsNullOrEmpty(txtEmpCode.Text))
                    {
                        flag = eData.EmployeeNo == txtEmpCode.Text ? true : false;
                    }
                    if (AdminStatus == false)
                    {
                        flag = eData.EmployeeId == Convert.ToInt32(Session["EmpId"]) ? false : true;
                    }
                    if (flag == false)
                    {
                        int day = -3, holidayCnt = 0, WOCnt = 0, leaveCnt = 0, aDays = 0, WFHCnt = 0;
                        double pdays = 0; TimeSpan Totaltime = new TimeSpan();
                        litBody.Text = litBody.Text + "<tr>";
                        foreach (var item in row.ItemArray)
                        {
                            string TextColour = "Black";

                            if (day == -3)
                                empid = Convert.ToInt32(item);
                            string stat = day == -3 ? eData.EmployeeNo : item.ToString();
                            day++;
                            Console.WriteLine(item);
                            if (day > 0 && day <= days)
                            {
                                DateTime dateTime = new DateTime(startDate.Year, startDate.Month, day);
                                
                                if (empid == 2)
                                {

                                }
                                
                                var chkHoliday = (from d in db.HoliDaysMasters where d.CompanyId == eData.CompanyId && d.TenantId == eData.TenantId && d.Date == dateTime.Date select d).FirstOrDefault();
                                if (chkHoliday != null)
                                {
                                    stat = "H";
                                    holidayCnt++;
                                }
                                TimeSpan shifttime = TimeSpan.Parse(shiftdata.Shifthours.ToString());
                                var rosterdata = (from d in db.BeforeSalaryRosterDetails(day, ddlMonth.SelectedIndex, Convert.ToInt32(ddlYear.SelectedValue), empid) select d).ToList();

                                if (rosterdata.Count() > 0)
                                {
                                    stat = "WO";
                                    WOCnt++;
                                }

                                if (stat == "L") { leaveCnt++; }
                                if (stat == "WO½P" || stat == "½ P" || stat == "WOH½P" || stat == "H½P" || stat == "WO½P(OD)" || stat == "H½P(OD)" || stat == "WOH½P(OD)") 
                                {
                                    pdays = pdays + 0.5;
                                    var presentattdata = db.AttendaceLogTBs.Where(a => a.EmployeeId == Convert.ToInt32(eId) && a.AttendanceDate == dateTime).FirstOrDefault();
                                    if (presentattdata.Duration != null)
                                    {
                                        string totaltime = "0:00";
                                        double dur = Convert.ToDouble(presentattdata.Duration);
                                        TimeSpan Dhr = TimeSpan.Parse("00:45");
                                        TimeSpan deductionhr = TimeSpan.FromHours(dur);
                                        TimeSpan testtime = TimeSpan.Parse("00:00");
                                        if (Dhr < deductionhr)
                                        {
                                             testtime = deductionhr.Subtract(Dhr);                                            
                                        }
                                        else
                                        {
                                            testtime = deductionhr;
                                        }


                                        if (testtime <= shifttime)
                                        {
                                            TextColour = "Red";
                                            stat = testtime.Hours + ":" + testtime.Minutes;
                                        }
                                        else
                                        {
                                            stat = testtime.Hours + ":" + testtime.Minutes;
                                        }

                                        //stat = testtime.Hours + ":" + testtime.Minutes;
                                        Totaltime += testtime;
                                    }
                                    else
                                    {
                                        stat = "";
                                    }
                                }
                                if (stat == "P" || stat == "WOP" || stat == "WOHP" || stat == "HP" || stat == "WOP(OD)" || stat == "HP(OD)" || stat == "WOHP(OD)" || stat == "WFH") 
                                {
                                    pdays++;
                                    var presentattdata = db.AttendaceLogTBs.Where(a => a.EmployeeId == Convert.ToInt32(eId) && a.AttendanceDate == dateTime).FirstOrDefault();
                                   
                                    if (presentattdata.Duration != null)
                                    {
                                        string totaltime = "0:00";
                                        double dur = Convert.ToDouble(presentattdata.Duration);
                                        TimeSpan Dhr = TimeSpan.Parse("00:45");                                        
                                        TimeSpan deductionhr = TimeSpan.FromHours(dur);
                                        //TimeSpan testtime = deductionhr.Subtract(Dhr);
                                        TimeSpan testtime = TimeSpan.Parse("00:00");

                                        
                                        if (Dhr < deductionhr)
                                        {
                                            testtime = deductionhr.Subtract(Dhr);
                                        }
                                        else
                                        {
                                            testtime = deductionhr;
                                        }
                                        if (testtime <= shifttime)
                                        {
                                            TextColour ="Red";
                                            stat = testtime.Hours + ":" + testtime.Minutes;
                                        }
                                        else
                                        {
                                            stat = testtime.Hours + ":" + testtime.Minutes;
                                        }
                                        
                                        Totaltime += testtime;
                                    }
                                    else
                                    {
                                        stat = "";
                                    }
                                }
                                //TextColour = "Black";
                                if (stat == "A")
                                {
                                    var attdata = db.AttendaceLogTBs.Where(a => a.EmployeeId == Convert.ToInt32(eId) && a.AttendanceDate == dateTime).FirstOrDefault();
                                    if (attdata == null)
                                    {
                                        stat = "";
                                    }
                                    else
                                    {
                                        aDays++;
                                    }
                                }
                                if (stat == "WO") { WOCnt++; }

                                if (stat == "WFH") { WFHCnt++; }
                            }

                            if (day > days)
                            {
                                if (day == days + 1)// set leaves
                                {
                                    stat = pdays.ToString();
                                }
                                else if (day == days + 2) //set holidays
                                {
                                    stat = aDays.ToString();
                                }
                                else if (day == days + 3) // set week offs
                                {
                                    stat = leaveCnt.ToString();
                                }
                                else if (day == days + 4) // set week offs
                                {
                                    stat = holidayCnt.ToString();
                                }
                                else if (day == days + 5) // set week offs
                                {
                                    stat = WOCnt.ToString();
                                }
                                else if (day == days + 6) // set week offs
                                {
                                    stat = WFHCnt.ToString();
                                }
                                else if (day == days + 7)
                                {                                   
                                    int totaldayshours = Totaltime.Days * 24;
                                  int  Totalhours = totaldayshours + Totaltime.Hours;

                                    //stat = (pdays + WOCnt + holidayCnt + leaveCnt).ToString();
                                    stat = Totalhours + ":" + Totaltime.Minutes;
                                }
                            }

                            litBody.Text = litBody.Text + "<td style='color:"+ TextColour + "'>" + stat + "</td>";

                        }
                        litBody.Text += "</tr>";
                    }
                }


            }
            //foreach (DataRow row in data.Rows)
            //{
            //    dataTable.Rows.Add(row);
            //    yourPosition++;
            //}

            BindJqFunctions();
            //gvAttendance.DataSource = data;
            //gvAttendance.DataBind();
        }
        catch (Exception ex)
        {
            litBody.Text = ex.Message;
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



    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList(); BindEmployeeList();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
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
    private void BindEmployeeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from dt in db.EmployeeTBs
                        where (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId == dId
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
















}