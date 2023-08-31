using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rpt_monthly_department_in_out : System.Web.UI.Page
{
    DataTable dtInfo;
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
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
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DateTime from = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, 1);
        int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex);
        DateTime toDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, days);
        /* new query with Day No instead of Date
                DECLARE @SQL VARCHAR(MAX) = ''; 
DECLARE @start_date Datetime='2022-01-01'--'2022-12-01'
DECLARE @end_date Datetime='2022-01-31'--'2022-12-31'
DECLARE @columns AS NVARCHAR(MAX)
declare @days as nvarchar(max)
SELECT @columns = ISNULL(@columns + ',' , '') + QUOTENAME(AttendanceDate) FROM (SELECT DAY( DateString) as AttendanceDate FROM dbo.DateRange_To_Table(@start_date,@end_date)) as AttendanceDate

SELECT @sql='SELECT       *
        FROM            (SELECT        E.CompanyId,E.DeptId,A.EmployeeId, E.FName + '' '' + E.Lname AS EmpName, E.EmployeeNo, E.MachineID, D1.DeptName, D2.DesigName, Day(A.AttendanceDate) AS DayNo, A.temp, A.Stat, A.SortLevel
                                  FROM            (
                                  select EmployeeId, AttendanceDate, Case when Status IN (''P'',''½ P'',''HP'',''WOP'',''WP'') then CONVERT(varchar(5), InTime)+'' ''+ CONVERT(varchar(5), OutTime)  else status end as Temp, ''Att'' AS STat, 1 AS SortLevel
                                  FROM AttendaceLogTB
                                  ) AS A INNER JOIN
                                                            EmployeeTB AS E ON A.EmployeeId = E.EmployeeId INNER JOIN
                                                            MasterDeptTB AS D1 ON E.DeptId = D1.DeptID INNER JOIN
                                                            MasterDesgTB AS D2 ON E.DesgId = D2.DesigID
                                WHERE        (A.AttendanceDate BETWEEN ''2022-01-01'' AND ''2022-01-31'')) AS D
                                PIVOT (MAX(temp) FOR DayNo IN ( '+@columns+')) p order by EmployeeId,SortLevel;'; 
                                PRINT @sql;  
                                EXEC(@SQL);
                 */
        string query = string.Format(@"DECLARE @SQL VARCHAR(MAX) = ''; 
DECLARE @start_date Datetime='{0}'--'2022-12-01'
DECLARE @end_date Datetime='{1}'--'2022-12-31'
DECLARE @columns AS NVARCHAR(MAX)
SELECT @columns = ISNULL(@columns + ',' , '') + QUOTENAME(AttendanceDate) FROM (SELECT DAY( DateString) as AttendanceDate FROM dbo.DateRange_To_Table(@start_date,@end_date)) as AttendanceDate

SELECT @sql='SELECT       *
       FROM            (SELECT        E.CompanyId,E.DeptId,A.EmployeeId, E.FName + '' '' + E.Lname AS EmpName, E.EmployeeNo AS EmpNo, E.MachineID, D1.DeptName, D2.DesigName, Day(A.AttendanceDate) AS DayNo, A.temp, A.Stat, A.SortLevel
                                  FROM            (
                                  select EmployeeId, AttendanceDate, Case when Status IN (''P'',''½ P'',''HP'',''WOP'',''WP'') then CONVERT(varchar(5), InTime)+'' ''+ CONVERT(varchar(5), OutTime)  else status end as Temp, ''Att'' AS STat, 1 AS SortLevel
                                  FROM AttendaceLogTB
                                  ) AS A INNER JOIN
                                                            EmployeeTB AS E ON A.EmployeeId = E.EmployeeId INNER JOIN
                                                            MasterDeptTB AS D1 ON E.DeptId = D1.DeptID INNER JOIN
                                                            MasterDesgTB AS D2 ON E.DesgId = D2.DesigID
                                WHERE     E.IsActive=1 AND   (A.AttendanceDate BETWEEN ''{0}'' AND ''{1}'')) AS D
                                PIVOT (MAX(temp) FOR DayNo IN ( '+@columns+')) p order by EmployeeId,SortLevel;'; 
                                PRINT @sql;  
                                EXEC(@SQL);", from.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"));

        DataTable data = gen.ReturnData(query);
        int tDays = DateTime.DaysInMonth(from.Year, from.Month);
        int col = 31;
        int actCol = tDays;
        if (actCol < col)
        {
            for (int i = actCol + 1; i <= col; i++)
            {
                data.Columns.Add(i.ToString());
            }
        }
        //data.Columns.Add()
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
        ViewState["dtInfo"] = data;

        DataTable distData = data.DefaultView.ToTable(true, "DeptName", "DeptId");

        rptrTables.DataSource = distData;
        rptrTables.DataBind();

    }

    protected void rptrTables_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            var x = e.Item.FindControl("lblHeader") as Label;
            x.Text = "Monthly Attendance Report with (In/Out) Time </br> For the Month of " + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue;
        }
        else
        {

            //dtInfo.Rows.Clear();
            RepeaterItem item = e.Item;
            string lblDeptId = (item.FindControl("lblDeptId") as Label).Text;
            GridView gv = e.Item.FindControl("grdOrder") as GridView;
            Literal litFooter = e.Item.FindControl("litFooter") as Literal;
            int deptId = Convert.ToInt32(lblDeptId);
            DataTable data = (DataTable)ViewState["dtInfo"];
            DataView dv1 = data.DefaultView;
            dv1.RowFilter = "DeptId= '" + lblDeptId + "'";
            DataTable dt = dv1.ToTable();

            gv.DataSource = dt;
            gv.DataBind();

        }


    }

    protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[2].Visible = e.Row.Cells[5].Visible = e.Row.Cells[6].Visible = e.Row.Cells[7].Visible = e.Row.Cells[8].Visible = e.Row.Cells[9].Visible = false;
            //e.Row.Cells[0].Visible = false;
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        string style = @"<script>.textWrap {white-space: pre-wrap;}</script>";
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=monthly_department_in_out_time_report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        rptrTables.RenderControl(hw);
        Response.Write(style);
        //Response.Output.Write(sw.ToString());
        Response.Write("<table class='textWrap'>");       
        Response.Write(sw.ToString());        
        Response.Write("</table>");
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    #region  Binding Methods
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
    #endregion

}