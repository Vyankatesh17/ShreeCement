
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class rpt_attendance_summary_report : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindYear();
                BindCompanyList();
                BindDepartmentList();
                BindEmployeeList();
                ddmonth.SelectedIndex = DateTime.Now.Month;
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
    
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        BindEmployeeList();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        DateTime from = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddmonth.SelectedIndex, 1);
        int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), ddmonth.SelectedIndex);
        DateTime toDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddmonth.SelectedIndex, days);


        string query = string.Format(@"DECLARE @SQL VARCHAR(MAX) = ''; 
DECLARE @start_date Datetime='{0}'--'2022-12-01'
DECLARE @end_date Datetime='{1}'--'2022-12-31'
DECLARE @columns AS NVARCHAR(MAX)
SELECT @columns = ISNULL(@columns + ',' , '') + QUOTENAME(AttendanceDate) FROM (SELECT CONVERT(date, DateString) as AttendanceDate FROM dbo.DateRange_To_Table(@start_date,@end_date)) as AttendanceDate

SELECT @sql='SELECT       *
FROM            (SELECT        A.EmployeeId, E.FName + '' '' + E.Lname AS EmpName, E.EmployeeNo, D1.DeptName, D2.DesigName, A.AttendanceDate, A.temp, A.Stat, A.SortLevel
                          FROM            (SELECT        EmployeeId, AttendanceDate, CONVERT(varchar, InTime) AS temp, ''In Time'' AS Stat, 1 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar, OutTime) AS temp, ''Out Time'' AS Stat, 2 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar, Status) AS temp, ''Status'' AS Stat, 3 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar, EarlyBy) AS temp, ''Early By'' AS Stat, 4 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar, LateBy) AS temp, ''Late By'' AS Stat, 5 AS SortLevel
                                                    FROM            AttendaceLogTB
                                                    UNION ALL
                                                    SELECT        EmployeeId, AttendanceDate, CONVERT(varchar(5), DATEADD(MINUTE, 60*Duration,0),108) AS temp, ''Duration'' AS Stat, 6 AS SortLevel
                                                    FROM            AttendaceLogTB) AS A INNER JOIN
                                                    EmployeeTB AS E ON A.EmployeeId = E.EmployeeId INNER JOIN
                                                    MasterDeptTB AS D1 ON E.DeptId = D1.DeptID INNER JOIN
                                                    MasterDesgTB AS D2 ON E.DesgId = D2.DesigID
                          WHERE     E.IsActive=1 AND   (A.AttendanceDate BETWEEN ''{0}'' AND ''{1}'')) AS D
PIVOT (MAX(temp) FOR AttendanceDate IN ( '+@columns+')) p order by EmployeeId,SortLevel;'; 
PRINT @sql;  
EXEC(@SQL);", from.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"));


        DataTable data = g.ReturnData(query);

        //All Com Data..
        //var empdata = (from d in HR.EmployeeTBs
        //               join d1 in HR.MasterDeptTBs on d.DeptId equals d1.DeptID where d.TenantId == Convert.ToString(Session["TenantId"]) select new {
        //                   EmpName=d.FName+" "+d.Lname,
        //                   EmpNo=d.EmployeeNo,
        //                   DeptName=d1.DeptName,
        //                   EmpId=d.EmployeeId
        //               }).Distinct();

        //var Atdlog = (from d in HR.AttendaceLogTBs where d.TenantId==Convert.ToString(Session["TenantId"]) select d).Distinct();




        //DataTable Atddt = new DataTable();
        //Atddt.Clear();
        //Atddt.Columns.Add("ColumnName");
        //for (var i = 1; i <= 31; i++)
        //{
        //    Atddt.Columns.Add("Day" + i + "");
        //}
        //Atddt.Columns.Add("ColumnMonthDate");

        //string reportHeader = string.Format("Report for : {0} {1}", ddmonth.SelectedItem.Text, ddlYear.SelectedValue);
        //ReportParameter[] param = new ReportParameter[1];

        //param[0] = new ReportParameter("ReportHeader", reportHeader, true);



        //ReportViewer1.LocalReport.SetParameters(param);

        for (int i = 7; i < data.Columns.Count; i++)
        {
            DateTime myDateTime = Convert.ToDateTime(data.Columns[i].ToString());
            string day = myDateTime.Day.ToString();
            data.Columns[i].ColumnName = "D"+day;
        }

        Session["dtInfo"] = data;

        DataTable dt = data.DefaultView.ToTable(true, "EmpName");

        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.Reset();
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report.rdlc");
        ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
         ReportViewer1.LocalReport.Refresh();//.ReportRefresh();
    }
    private void BindEmployeeList()
    {
        ddlEmployee.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from dt in db.EmployeeTBs
                        where (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId==dId
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
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
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
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }

    void LocalReport_SubreportProcessing(
             object sender, SubreportProcessingEventArgs e)
    {

        // get empID from the parameters

        int iOrderNo = Convert.ToInt32(e.Parameters[0].Values[0]);

        DataTable dataTable = (DataTable)Session["dtInfo"];


        // remove all previously attached Datasources, since we want to attach a

        // new one

        e.DataSources.Clear();



        // Retrieve employeeFamily list based on EmpID

        //var lines = BLL.GetOrders().Single(m => m.OrderNo == iOrderNo).Lines;

        var lines= dataTable.Select("EmployeeId = " + iOrderNo);

        // add retrieved dataset or you can call it list to data source

        e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource()

        {

            Name = "DataSet1",

            Value = lines

        });

    }

}