using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Simple_MonthlyAttandanceReport : System.Web.UI.Page
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
                txtFromDate.Text = txtToDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                //ddlMonth.SelectedIndex = DateTime.Now.Month;
                BindYear();
                fillcompany();
                //BindSimpleAttendance();

                if (Session["UserType"].ToString() != "User")
                {
                    company.Visible = true;
                    department.Visible = true;
                    employee.Visible = true;
                    Manager.Visible = true;
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
            litBody.Text = litHeaders.Text = "";
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {

                DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
                DateTime endDate = Convert.ToDateTime(txtToDate.Text);

                var days = (endDate - startDate).TotalDays + 1;
                //int days = DateTime.DaysInMonth(startDate.Year, startDate.Month);


                //string query = string.Format(@"EXEC sp_month_simple_attendance '{0}','{1}','{2}','{3}'", startDate.ToString("MM/dd/yyyy"), endDate.ToString("MM/dd/yyyy"), ddlCompany.SelectedValue,
                //    Convert.ToString(Session["TenantId"]));

                string query = string.Format(@"EXEC sp_month_simple_attendance '{0}','{1}','{2}','{3}'", startDate.ToString("MM/dd/yyyy"), endDate.ToString("MM/dd/yyyy"), ddlCompany.SelectedValue,
                    Convert.ToString(Session["TenantId"]));
                DataTable data = gen.ReturnData(query);


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
                        int day = -3, holidayCnt = 0, WOCnt = 0, leaveCnt = 0, aDays = 0;
                        double pdays = 0;
                        litBody.Text = litBody.Text + "<tr>";
                        DateTime dateTime = startDate.Date;
                        foreach (var item in row.ItemArray)
                        {
                            if (day == -3)
                                empid = Convert.ToInt32(item);
                            string stat = day == -3 ? eData.EmployeeNo : item.ToString();
                            day++;
                            Console.WriteLine(item);
                            if (day > 0 && day <= days)
                            {
                                if (empid == 2)
                                {
                                }
                                //DateTime dateTime = new DateTime(startDate.Year, startDate.Month, day);

                                dateTime = dateTime.AddDays(1);

                                var chkHoliday = (from d in db.HoliDaysMasters where d.CompanyId == eData.CompanyId && d.TenantId == eData.TenantId && d.Date == dateTime.Date select d).FirstOrDefault();
                                if (chkHoliday != null)
                                {
                                    stat = "H";
                                    holidayCnt++;
                                }

                                var rosterdata = (from d in db.BeforeSalaryRosterDetails(day, endDate.Month, Convert.ToInt32(endDate.Year), empid) select d).ToList();

                                if (rosterdata.Count() > 0)
                                {
                                    stat = "WO";
                                    WOCnt++;
                                }

                                if (stat == "L") { leaveCnt++; }
                                if (stat == "WO½P" || stat == "½ P" || stat == "WOH½P" || stat == "H½P" || stat == "WO½P(OD)" || stat == "H½P(OD)" || stat == "WOH½P(OD)") { pdays = pdays + 0.5; }
                                if (stat == "P" || stat == "WOP" || stat == "WOHP" || stat == "HP" || stat == "WOP(OD)" || stat == "HP(OD)" || stat == "WOHP(OD)") { pdays++; }
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
                                else if (day == days + 6)
                                {
                                    stat = (pdays + WOCnt + holidayCnt + leaveCnt).ToString();
                                }
                            }

                            litBody.Text = litBody.Text + "<td>" + stat + "</td>";

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
        //ddlYear.Items.Clear();
        //int year = DateTime.Now.AddYears(-75).Year;
        //for (int i = year; i < DateTime.Now.AddYears(2).Year; i++)
        //{
        //    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        //}
        //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }

    private void BindManagerList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlManager.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var managerdata = db.EmployeeTBs.Where(a => a.ManagerID != 0 && a.CompanyId == cId).Select(a => a.ManagerID).Distinct().ToList();
            List<EmployeeTB> emplist = new List<EmployeeTB>();
            foreach (var item in managerdata)
            {
                EmployeeTB emp = new EmployeeTB();
                var empdata = db.EmployeeTBs.Where(a => a.EmployeeId == item.Value).FirstOrDefault();

                emp.EmployeeId = empdata.EmployeeId;
                emp.FName = empdata.FName + " " + empdata.Lname;
                emplist.Add(emp);
            }


            ddlManager.DataSource = emplist;
            ddlManager.DataTextField = "FName";
            ddlManager.DataValueField = "EmployeeId";
            ddlManager.DataBind();

            ddlManager.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }




    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList(); BindManagerList(); /*BindEmployeeList();*/
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
            DateTime todaysdt = DateTime.Now;
            var month = todaysdt.Month;
            ddlEmployee.Items.Clear();

            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.RelivingDate, dt.RelivingStatus }).OrderBy(d => d.name);
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

                        if (relivingmonth == month)
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
            ddlEmployee.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
    }

    protected void ddlManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeByManagerList();
    }


    private void BindEmployeeByManagerList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            DateTime todaysdt = DateTime.Now;
            var month = todaysdt.Month;
            ddlEmployee.Items.Clear();

            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int MId = ddlManager.SelectedIndex > 0 ? Convert.ToInt32(ddlManager.SelectedValue) : 0;

            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.ManagerID == MId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.RelivingDate, dt.RelivingStatus }).OrderBy(d => d.name);
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

                        if (relivingmonth == month)
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
            ddlEmployee.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
    }
}