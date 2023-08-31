using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rpt_attend_summary : System.Web.UI.Page
{
    DataTable dtInfo;
    Genreal gen = new Genreal();
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
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList(); BindEmployeeList();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                db.CommandTimeout = 15 * 60;

                DateTime from = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, 1);
                int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex);
                DateTime toDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, days);

                /* query for new report
                 DECLARE @SQL VARCHAR(MAX) = ''; 
        DECLARE @start_date Datetime='2021-12-01'
        DECLARE @end_date Datetime='2021-12-31'
        DECLARE @columns AS NVARCHAR(MAX)
        SELECT @columns = ISNULL(@columns + ',' , '') + QUOTENAME(AttendanceDate) FROM (SELECT CONVERT(date, DateString) as AttendanceDate FROM dbo.DateRange_To_Table(@start_date,@end_date)) as AttendanceDate

        SELECT @sql='SELECT       *
        FROM            (SELECT        E.CompanyId,E.DeptId,A.EmployeeId, E.FName + '' '' + E.Lname AS EmpName, E.EmployeeNo, E.MachineID, D1.DeptName, D2.DesigName, A.AttendanceDate, A.temp, A.Stat, A.SortLevel
                                  FROM            (
                                  select EmployeeId, AttendanceDate, Case when Status IN (''P'',''½ P'',''HP'',''WOP'',''WP'') then CONVERT(varchar(5), InTime)+'' ''+ CONVERT(varchar(5), OutTime)  else status end as Temp, ''Att'' AS STat, 1 AS SortLevel
                                  FROM AttendaceLogTB
                                  ) AS A INNER JOIN
                                                            EmployeeTB AS E ON A.EmployeeId = E.EmployeeId INNER JOIN
                                                            MasterDeptTB AS D1 ON E.DeptId = D1.DeptID INNER JOIN
                                                            MasterDesgTB AS D2 ON E.DesgId = D2.DesigID
                                  WHERE        (A.AttendanceDate BETWEEN ''2021-12-01'' AND ''2021-12-31'')) AS D
        PIVOT (MAX(temp) FOR AttendanceDate IN ( '+@columns+')) p order by EmployeeId,SortLevel;'; 
        PRINT @sql;  
        EXEC(@SQL); 
                 */

                string query = string.Format(@"DECLARE @SQL VARCHAR(MAX) = ''; 
DECLARE @start_date Datetime='{0}'--'2022-12-01'
DECLARE @end_date Datetime='{1}'--'2022-12-31'
DECLARE @columns AS NVARCHAR(MAX)
SELECT @columns = ISNULL(@columns + ',' , '') + QUOTENAME(AttendanceDate) FROM (SELECT CONVERT(date, DateString) as AttendanceDate FROM dbo.DateRange_To_Table(@start_date,@end_date)) as AttendanceDate

SELECT @sql='SELECT       *
FROM            (SELECT        E.CompanyId,E.DeptId,A.EmployeeId, E.FName + '' '' + E.Lname AS EmpName, E.EmployeeNo, E.MachineID, D1.DeptName, D2.DesigName, A.AttendanceDate, A.temp, A.Stat, A.SortLevel
                          FROM            (SELECT        EmployeeId, AttendanceDate, CONVERT(varchar(5), InTime) AS temp, ''In'' AS Stat, 2 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar(5), OutTime) AS temp, ''Out'' AS Stat, 3 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, S.Shift AS temp, ''Shift'' AS Stat, 1 AS SortLevel
                                                    FROM            AttendaceLogTB AL INNER JOIN MAsterShiftTB S ON Al.ShiftId=S.ShiftId
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar, Status) AS temp, ''Status'' AS Stat, 4 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar(5),DATEADD(MINUTE, convert(int,EarlyBy), ''19000101''), 108) AS temp, ''EarlyBy'' AS Stat, 5 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar(5),DATEADD(MINUTE, convert(int,LateBy), ''19000101''), 108) AS temp, ''LateBy'' AS Stat, 6 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar(5),DATEADD(MINUTE, convert(int,OverTime), ''19000101''), 108) AS temp, ''OT (Min)'' AS Stat, 7 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar(5), DATEADD(MINUTE, 60*Duration,0),108) AS temp, ''Duration'' AS Stat, 8 AS SortLevel
                                                    FROM            AttendaceLogTB) AS A INNER JOIN
                                                    EmployeeTB AS E ON A.EmployeeId = E.EmployeeId INNER JOIN
                                                    MasterDeptTB AS D1 ON E.DeptId = D1.DeptID INNER JOIN
                                                    MasterDesgTB AS D2 ON E.DesgId = D2.DesigID
                          WHERE     E.IsActive=1 AND   (A.AttendanceDate BETWEEN ''{0}'' AND ''{1}'')) AS D
PIVOT (MAX(temp) FOR AttendanceDate IN ( '+@columns+')) p order by EmployeeId,SortLevel;'; 
PRINT @sql;  
EXEC(@SQL);", from.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"));

                DataTable data = gen.ReturnData(query);

                if (ddlCompany.SelectedIndex > 0)
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "CompanyId= '" + ddlCompany.SelectedValue + "'";
                    data = dv1.ToTable();
                }
                if (ddlDepartment.SelectedIndex > 0)
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "DeptId= '" + ddlDepartment.SelectedValue + "'";
                    data = dv1.ToTable();
                }
                if (ddlEmployee.SelectedIndex > 0)
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "EmployeeId= '" + ddlEmployee.SelectedValue + "'";
                    data = dv1.ToTable();
                }
                if (Session["UserType"].ToString() == "User")
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "EmployeeId= '" + Convert.ToInt32(Session["EmpId"]) + "'";
                    data = dv1.ToTable();
                }
                if (!string.IsNullOrEmpty(txtEmpCode.Text))
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "EmployeeNo= '" + txtEmpCode.Text + "'";
                    data = dv1.ToTable();
                }

                ViewState["dtInfo"] = data;

                DataTable distData = data.DefaultView.ToTable(true, "EmployeeId", "EmpName", "EmployeeNo", "MachineID", "DeptName", "DesigName");

                rptrTables.DataSource = distData;
                rptrTables.DataBind();
                //using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                //{

                //        dtInfo = new DataTable();
                //        dtInfo.Columns.Add(new DataColumn("Status", typeof(string)));

                //        for (var day = from.Date; day.Date <= toDate.Date; day = day.AddDays(1))
                //        {
                //            string no = day.Day.ToString();
                //            dtInfo.Columns.Add(new DataColumn(no, typeof(string)));
                //        }
                //        ViewState["dtInfo"] = dtInfo;



                //    var data = (from d in db.EmployeeTBs
                //                join c in db.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                //                join d1 in db.MasterDeptTBs on d.DeptId equals d1.DeptID
                //                join d2 in db.MasterDesgTBs on d.DesgId equals d2.DesigID
                //                where d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                //                select new {EmpName = d.FName + " " + d.Lname, d.EmployeeNo, d1.DeptName, d2.DesigName, c.CompanyName,d.EmployeeId,d.DeptId,d.DesgId }).Distinct();

                //    if (ddlDepartment.SelectedIndex > 0)
                //    {
                //        data = data.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)).Distinct();
                //    }
                //    if (ddlEmployee.SelectedIndex > 0)
                //    {
                //        data = data.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue)).Distinct();
                //    }
                //    if (!string.IsNullOrEmpty(txtEmpCode.Text))
                //    {
                //        data = data.Where(d => d.EmployeeNo == txtEmpCode.Text).Distinct();
                //    }
                //    rptrTables.DataSource = data;
                //    rptrTables.DataBind();
                //}
            }
        }
        catch (Exception ex)
        {

        }
    }



    protected void rptrTables_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {

            if (e.Item.ItemType == ListItemType.Header)
            {
                var x = e.Item.FindControl("lblHeader") as Label;
                x.Text = "Report for month of " + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedValue;
            }
            else
            {

                //dtInfo.Rows.Clear();
                RepeaterItem item = e.Item;
                string lblEmpIdId = (item.FindControl("lblEmpIdId") as Label).Text;
                GridView gv = e.Item.FindControl("grdOrder") as GridView;
                Literal litFooter = e.Item.FindControl("litFooter") as Literal;
                int empId = Convert.ToInt32(lblEmpIdId);
                DataTable data = (DataTable)ViewState["dtInfo"];
                DataView dv1 = data.DefaultView;
                dv1.RowFilter = "EmployeeId= '" + empId + "'";
                DataTable dt = dv1.ToTable();


              

                //string[] statusArray = { "In Time", "Out Time", "Status", "Total" };
                //using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                //{

                //    for (int i = 0; i < statusArray.Length; i++)
                //    {
                //        DateTime from = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, 1);
                //        int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex);
                //        DateTime toDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, days);
                //        DataRow dr = dtInfo.NewRow();
                //        dr["Status"] = statusArray[i];

                //        for (var day = from.Date; day.Date <= toDate.Date; day = day.AddDays(1))
                //        {
                //            var data = db.AttendaceLogTBs.Where(d => d.EmployeeId == empId && d.AttendanceDate.Date == day.Date).FirstOrDefault();

                //            string val = "";
                //            if (data != null)
                //            {
                //                if (statusArray[i] == "In Time")
                //                {
                //                    val = string.IsNullOrEmpty(data.InTime) ? "" : data.InTime.Substring(0, data.InTime.LastIndexOf(':')); ;
                //                }
                //                else if (statusArray[i] == "Out Time")
                //                {
                //                    val = string.IsNullOrEmpty(data.OutTime) ? "" : data.OutTime.Substring(0, data.OutTime.LastIndexOf(':'));
                //                }
                //                else if (statusArray[i] == "Status")
                //                {
                //                    val = string.IsNullOrEmpty(data.Status) ? "" : data.Status.ToString();
                //                }
                //                else if (statusArray[i] == "Total")
                //                {
                //                    val = string.IsNullOrEmpty(data.Duration.HasValue.ToString()) ? "" : String.Format("{0:0.00}", data.Duration);
                //                }
                //            }
                //            string no = day.Day.ToString();
                //            dr[no] = val.ToString();
                //        }
                //        dtInfo.Rows.Add(dr);
                //    }
                //}


                gv.DataSource = dt;
                gv.DataBind();

                if (dt.Rows.Count > 0)
                {
                    string query = string.Format(@"SELECT        COUNT(*) AS CNT, 'P' AS STAT
FROM            AttendaceLogTB
WHERE        (MONTH(AttendanceDate) = {0}) AND (YEAR(AttendanceDate) = {1}) AND (EmployeeId = {2}) AND (Status IN ('P', 'WOP', 'WOHP', 'HP', 'WOP(OD)', 'HP(OD)', 'WOHP(OD)'))
UNION ALL
SELECT        COUNT(*) AS CNT, 'A' AS STAT
FROM            AttendaceLogTB
WHERE        (MONTH(AttendanceDate) = {0}) AND (YEAR(AttendanceDate) = {1}) AND (EmployeeId = {2}) AND (Status IN ('A'))
UNION ALL
SELECT        COUNT(*) AS CNT, 'L' AS STAT
FROM            AttendaceLogTB
WHERE        (MONTH(AttendanceDate) = {0}) AND (YEAR(AttendanceDate) = {1}) AND (EmployeeId = {2}) AND (Status IN ('L'))
UNION ALL
SELECT        COUNT(*) AS CNT, 'WO' AS STAT
FROM            AttendaceLogTB
WHERE        (MONTH(AttendanceDate) = {0}) AND (YEAR(AttendanceDate) = {1}) AND (EmployeeId = {2}) AND (Status IN ('WO'))
UNION ALL
SELECT        CAST(SUM(Duration) AS decimal(10, 2)) AS CNT, 'D' AS STAT
FROM            AttendaceLogTB
WHERE        (MONTH(AttendanceDate) = {0}) AND (YEAR(AttendanceDate) = {1}) AND (EmployeeId = {2})
UNION ALL
SELECT        COUNT(*) AS CNT, 'OT' AS STAT
FROM            AttendaceLogTB
WHERE        (MONTH(AttendanceDate) = {0}) AND (YEAR(AttendanceDate) = {1}) AND (EmployeeId = {2}) AND (OverTime > 0)
UNION ALL
SELECT        COUNT(*) AS CNT, '½P' AS STAT
FROM            AttendaceLogTB
WHERE        (MONTH(AttendanceDate) = {0}) AND (YEAR(AttendanceDate) = {1}) AND (EmployeeId = {2}) AND (Status IN ('½ P', 'WO½P', 'WOH½P', 'H½P', 'WO½P(OD)', 'H½P(OD)', 'WOH½P(OD)'))", ddlMonth.SelectedIndex, Convert.ToInt32(ddlYear.SelectedValue), empId);

                    DataTable dataTable = gen.ReturnData(query);
                    string foot = "";

                    string present = dataTable.Rows[0]["CNT"].ToString();
                    string halfpresent = dataTable.Rows[6]["CNT"].ToString();

                    string totalPresent = "0.00";
                    if (dataTable.Rows[6]["CNT"] != DBNull.Value || halfpresent != "0.00")
                    {
                        double half = Convert.ToDouble(dataTable.Rows[6]["CNT"]) / 2;
                        double total = half + Convert.ToDouble(present);
                        totalPresent = total.ToString();
                    }
                    else
                    {
                        totalPresent = present + ".00";
                    }

                        TimeSpan time = TimeSpan.Parse("00:00");
                    string totaltime = "0:00";
                    string strtotal = dataTable.Rows[4]["CNT"].ToString();
                    if (dataTable.Rows[4]["CNT"] != DBNull.Value)
                    {
                        double totaldouble = Convert.ToDouble(dataTable.Rows[4]["CNT"]);

                        time = TimeSpan.FromHours(totaldouble);

                        double days = time.TotalDays; // total number of days
                        double hour = time.TotalHours; // total number of hours
                        int day = time.Days; // breaks out just days
                        int hr = time.Hours; // breaks out just hours
                        int m = time.Minutes; // breaks out just minutes

                        int totalhour = day * 24 + hr;

                        if (m >= 10)
                        {
                            totaltime = Convert.ToString(totalhour + ":" + m);
                        }
                        else
                        {
                            totaltime = Convert.ToString(totalhour + ":0" + m);
                        }
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        foot = string.Format(@"<b>Total Duration : </b> {0} &emsp; <b>Total Present : </b> {1} &emsp; <b>Total Absents : </b> {2} &emsp; <b> Week Offs : </b> {3} &emsp; <b>Leaves : </b> {4}&emsp; <b>Total OT : </b> {5}",
                            totaltime, totalPresent, dataTable.Rows[1]["CNT"].ToString(), dataTable.Rows[3]["CNT"].ToString(), dataTable.Rows[2]["CNT"].ToString(), dataTable.Rows[5]["CNT"].ToString());
                    }
                    else
                    {
                        foot = string.Format(@"<b>Total Duration : </b> {0} &emsp; <b>Total Present : </b> {1} &emsp; <b>Total Absents : </b> {2} &emsp; <b> Week Offs : </b> {3} &emsp; <b>Leaves : </b> {4}&emsp; <b>Total OD : </b> {5}", 0, 0, 0, 0, 0, 0);
                    }
                    litFooter.Text = foot;
                    //gv.HeaderRow.Cells[2].Visible = false;
                    //gv.Columns[0].Visible = false;
                    //gv.Columns[1].Visible = false;
                    //gv.Columns[2].Visible = false;
                    //gv.Columns[3].Visible = false;
                    //gv.Columns[4].Visible = false;
                    //gv.Columns[6].Visible = false;
                }
            }
        }
        catch(Exception ex)
        {

        }
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
            ddlEmployee.Items.Clear();
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


    protected void ExportToExcel(object sender, EventArgs e)
    {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Monthly_Summary_Report.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            rptrTables.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }

    protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.Header||e.Row.RowType==DataControlRowType.DataRow)
        {
           
            e.Row.Cells[0].Visible= e.Row.Cells[1].Visible= e.Row.Cells[2].Visible= e.Row.Cells[3].Visible = e.Row.Cells[4].Visible =  e.Row.Cells[5].Visible  = e.Row.Cells[6].Visible= e.Row.Cells[7].Visible= e.Row.Cells[9].Visible= false;
            //e.Row.Cells[0].Visible = false;
        }
    }
}