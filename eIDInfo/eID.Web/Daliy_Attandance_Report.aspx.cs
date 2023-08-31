using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Daliy_Attandance_Report : System.Web.UI.Page
{
    DataTable dtInfo;
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
                //ddlMonth.SelectedIndex = DateTime.Now.Month;
                //BindYear();
                fillcompany();
                //BindSimpleAttendance();
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList(); /*BindEmployeeList();*/
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
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
            DateTime todaysdt = DateTime.Now;
            var todaysmonth = todaysdt.Month;
            var todaysyear = todaysdt.Year;
            ddlEmployee.Items.Clear();

            DateTime todate = Convert.ToDateTime(txtToDate.Text);
            var tomonth = todate.Month;
            var toyear = todate.Year;

            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.RelivingDate, dt.RelivingStatus }).OrderBy(d => d.name);


            List<EmployeeTB> emplist = new List<EmployeeTB>();

            if (data != null && data.Count() > 0)
            {
                foreach(var item in data)
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
                        else if (todate <= relivingdate || tomonth == relivingmonth && toyear == relivingyear)
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

    //private void BindYear()
    //{
    //    ddlYear.Items.Clear();
    //    int year = DateTime.Now.AddYears(-75).Year;
    //    for (int i = year; i < DateTime.Now.AddYears(2).Year; i++)
    //    {
    //        ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //    }
    //    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    //}


    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Daily_Attandance_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        rptrTables.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        rptrTables.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());


        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A2.Rotate(), 10f, 10f, 100f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }

    protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[2].Visible = e.Row.Cells[3].Visible = e.Row.Cells[4].Visible = e.Row.Cells[5].Visible = e.Row.Cells[6].Visible = e.Row.Cells[7].Visible  = false;
            //e.Row.Cells[0].Visible = false;

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //DateTime from = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, 1);
        //int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex);
        //DateTime toDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, days);
       // DateTime fromdt = Genreal.GetDate(txtdate.Text);

        string query = string.Format(@"SELECT        E.CompanyId,E.DeptId,A.EmployeeId, E.FName + ' ' + E.Lname AS EmpName, E.EmployeeNo, E.MachineID, D1.DeptName, D2.DesigName, DAY(A.AttendanceDate) As AttDate,
                                CONVERT(varchar(5), A.InTime) As Intime  , CONVERT(varchar(5), A.OutTime) As Outtime,S.Shift AS Shift, CONVERT(varchar(5), S.InTime) As SIntime,CONVERT(varchar(5), S.OutTime) As SOuttime,
CONVERT(varchar(5), DATEADD(MINUTE, 60*Duration,0),108) AS WorkDur,CONVERT(varchar(5),DATEADD(MINUTE, convert(int,A.OverTime), '19000101'), 108) AS OT,CONVERT(varchar(5), DATEADD(MINUTE, 60*Duration,0),108) AS TotalDur,
CONVERT(varchar(5),DATEADD(MINUTE, convert(int,LateBy), '19000101'), 108) AS LateBy,CONVERT(varchar(5),DATEADD(MINUTE, convert(int,EarlyBy), '19000101'), 108) AS EarlyGoingBy, CONVERT(varchar, A.Status) AS Status,(CONVERT(varchar(5), A.InTime)+ ': in' +','+ CONVERT(varchar(5), A.OutTime) +':out')  As PunchRecords
                                                        FROM   AttendaceLogTB AS A INNER JOIN
                                                    EmployeeTB AS E ON A.EmployeeId = E.EmployeeId INNER JOIN
                                                    MasterDeptTB AS D1 ON E.DeptId = D1.DeptID INNER JOIN
                                                    MasterDesgTB AS D2 ON E.DesgId = D2.DesigID INNER JOIN
                                                    MAsterShiftTB S ON A.ShiftId=S.ShiftId
                          WHERE     E.IsActive=1 AND   ((A.AttendanceDate BETWEEN '{0}' AND '{1}')) Order By A.AttendanceDate, E.DeptId, EmpName", txtFromDate.Text, txtToDate.Text);

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

        data.DefaultView.Sort= "DeptName ASC, EmpName ASC";
       
        ViewState["dtInfo"] = data;
        DataTable distData = data.DefaultView.ToTable(true, "EmployeeId", "EmpName", "EmployeeNo", "MachineID", "DeptName", "DesigName");

        rptrTables.DataSource = distData;
        rptrTables.DataBind();        
    }

    protected void rptrTables_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Header)
        {
            DateTime from = Convert.ToDateTime(txtFromDate.Text);
            string fromdt = (from).ToString("dd-MMM-yyyy");


            DateTime to = Convert.ToDateTime(txtToDate.Text);
            string todt = (to).ToString("dd-MMM-yyyy");

            var x = e.Item.FindControl("lblHeader") as Label;
            x.Text = "Daily Attendance Report of " + fromdt + " to " + todt;
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
                    
            gv.DataSource = dt;
            gv.DataBind();

            if (dt.Rows.Count > 0)
            {

                string query = string.Format(@"SELECT        COUNT(*) AS CNT, 'P' AS STAT
FROM            AttendaceLogTB
WHERE        (AttendanceDate between  '{0}' AND '{1}') AND (EmployeeId = {2}) AND (Status IN ('P', 'WOP', 'WOHP', 'HP', 'WOP(OD)', 'HP(OD)', 'WOHP(OD)'))
UNION ALL
SELECT        COUNT(*) AS CNT, 'A' AS STAT
FROM            AttendaceLogTB
WHERE        (AttendanceDate between  '{0}' AND '{1}') AND (EmployeeId = {2}) AND (Status IN ('A'))
UNION ALL
SELECT        COUNT(*) AS CNT, 'L' AS STAT
FROM            AttendaceLogTB
WHERE        (AttendanceDate between  '{0}' AND '{1}') AND (EmployeeId = {2}) AND (Status IN ('L'))
UNION ALL
SELECT        COUNT(*) AS CNT, 'WO' AS STAT
FROM            AttendaceLogTB
WHERE        (AttendanceDate between  '{0}' AND '{1}') AND (EmployeeId = {2}) AND (Status IN ('WO'))
UNION ALL
SELECT        CAST(SUM(Duration) AS decimal(10, 2)) AS CNT, 'D' AS STAT
FROM            AttendaceLogTB
WHERE        (AttendanceDate between  '{0}' AND '{1}') AND (EmployeeId = {2})
UNION ALL
SELECT        COUNT(*) AS CNT, '½P' AS STAT
FROM            AttendaceLogTB
WHERE        (AttendanceDate between  '{0}' AND '{1}') AND (EmployeeId = {2}) AND (Status IN ('½ P', 'WO½P', 'WOH½P', 'H½P', 'WO½P(OD)', 'H½P(OD)', 'WOH½P(OD)'))", txtFromDate.Text, txtToDate.Text, empId);

                DataTable dataTable = gen.ReturnData(query);
                string foot = "";

                string present = dataTable.Rows[0]["CNT"].ToString();
                string halfpresent = dataTable.Rows[5]["CNT"].ToString();

                string totalPresent = "0.00";
                if (dataTable.Rows[5]["CNT"] != DBNull.Value || halfpresent != "0.00")
                {
                    double half = Convert.ToDouble(dataTable.Rows[5]["CNT"]) / 2;
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
                    foot = string.Format(@"<b>Total Duration : </b> {0} &emsp; <b>Total Present : </b> {1} &emsp; <b>Total Absents : </b> {2} &emsp; <b> Week Offs : </b> {3} &emsp; <b>Leaves : </b> {4}",
                        totaltime, totalPresent, dataTable.Rows[1]["CNT"].ToString(), dataTable.Rows[3]["CNT"].ToString(), dataTable.Rows[2]["CNT"].ToString());
                }
                else
                {
                    foot = string.Format(@"<b>Total Duration : </b> {0} &emsp; <b>Total Present : </b> {1} &emsp; <b>Total Absents : </b> {2} &emsp; <b> Week Offs : </b> {3} &emsp; <b>Leaves : </b> {4}",
                        0, 0, 0, 0, 0);
                }
                litFooter.Text = foot;
             
            }
        }
    }




}