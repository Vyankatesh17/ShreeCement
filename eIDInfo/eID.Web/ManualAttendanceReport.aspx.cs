using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManualAttendanceReport : System.Web.UI.Page
{
    /// <summary>
    /// Manual Attendance Report Form
    /// Created By Abdul Rahman
    /// Created Date : 04/12/2014
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
      
    Genreal g = new Genreal();
    int year = 0;
    public int NumberOfDays = 0;
    HrPortalDtaClassDataContext ex = new HrPortalDtaClassDataContext();
    DataTable dtd = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                bindCompany();
                fillEmployee();
                ddmonth.Items.FindByValue(Convert.ToString(DateTime.Now.Month)).Selected = true;
                year = DateTime.Now.Year;
                ddyear.SelectedIndex = -1;
                ddyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    private void fillEmployee()
    {
        try
        {
            var dt = (from k in ex.EmployeeTBs
                         where k.RelivingStatus == null
                         select new { Name = k.FName + ' ' + k.MName + ' ' + k.Lname, k.EmployeeId }).OrderBy(s=> s.Name);
                //  select new { Name = k.FName + ' ' + k.Lname, k.EmployeeId };

                //if (dt.Count() > 0)
                //{
                    ddEmp.DataSource = dt;
                    ddEmp.DataTextField = "Name";
                    ddEmp.DataValueField = "EmployeeId";
                    ddEmp.DataBind();
                    ddEmp.Items.Insert(0, "ALL");
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void bindCompany()
    {
        try
        {
            var dt = (from p in ex.CompanyInfoTBs
                      where p.Status==0
                     select new { p.CompanyName, p.CompanyId }).OrderBy(d=>d.CompanyName);
            //if (dt.Count() > 0)
            //{
                ddlCompany.DataSource = dt;
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataValueField = "CompanyId";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "ALL");
            //}
            //else
            //{
            //    ddlCompany.Items.Clear();
            //    ddlCompany.DataSource = null;
            //    ddlCompany.DataBind();
            //    ddlCompany.Items.Insert(0, "--Select--");
            //}
        }
        catch (Exception ex)
        {
            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string date = "";
        string year = ddyear.SelectedValue;
        date = ddmonth.SelectedValue + '/' + 1 + '/' + year;
        //string year = DateTime.Now.Year.ToString();
         #region
        if (ddlCompany.SelectedIndex==0 && ddEmp.SelectedIndex == 0)
        {//ALL
            #region

            dtd = new DataTable();
            int k = System.DateTime.DaysInMonth(int.Parse(DateTime.Now.Year.ToString()), (ddmonth.SelectedIndex));
            #region
            
            for (int j = 0; j < k + 1; j++)
            {//Columns
                DataColumn drmon = new DataColumn();
                
                if (j == 0)
                {
                    drmon.ColumnName = " ";
                }
                else
                {
                   
                    drmon.ColumnName = j.ToString();
                }
               
                dtd.Columns.Add(drmon.ToString());
            } 
            DataColumn drWD = new DataColumn();
            DataColumn drHoliSun = new DataColumn();
            DataColumn drLeaves = new DataColumn();
            
            drWD.ColumnName = "WD";
            drHoliSun.ColumnName = "Holiday/Sunday";
            drLeaves.ColumnName = "Leaves";
            
            dtd.Columns.Add(drWD.ToString());
            dtd.Columns.Add(drHoliSun.ToString());
            dtd.Columns.Add(drLeaves.ToString());

            DataSet dsEmp = g.ReturnData1(@"SELECT DISTINCT   t1.MachineID,[t1].[value] AS [Name], [t1].[EmployeeId] FROM (SELECT  [t0].MachineID,[t0].[FName] +' '+ [t0].[Mname] +' ' + [t0].[Lname] AS [value], [t0].[EmployeeId], [t0].[RelivingStatus], [t0].[CompanyId]
                    FROM [dbo].[EmployeeTB] AS [t0] ) AS [t1] WHERE ([t1].[RelivingStatus] IS NULL)  and t1.machineID  !=0 AND t1.machineID is not null");
            if (dsEmp.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsEmp.Tables[0].Rows.Count; i++)
                //  for (int i = 0; i < dtd.Columns.Count; i++)
                {//Rows

                    try
                    {
                        DataRow drEmp = dtd.NewRow();
                        drEmp[0] = dsEmp.Tables[0].Rows[i]["Name"];
                        dtd.Rows.Add(drEmp[0].ToString());
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
            gv.DataSource = dtd;
            gv.DataBind();
            #endregion
            try
            {
                for (int kk = 0; kk < gv.Rows.Count; kk++)
                {
                    // for (int i = 1; i <=gv.Rows[kk].Cells.Count-1; i++)
                    for (int i = 1; i < gv.Rows[kk].Cells.Count - 3; i++)
                    {
                        DataSet dsP = g.ReturnData1(@"select [Emp_Name],[Status] from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + i + "'  and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "' and DATEPART(Year, Log_Date_Time)='" + year + "'  and status is null  and Emp_Name='" + gv.Rows[kk].Cells[0].Text + "'");
                        if (dsP.Tables[0].Rows.Count > 0)
                        {//Present
                            gv.Rows[kk].Cells[i].Text = "P";
                            gv.Rows[kk].Cells[i].BackColor = Color.GreenYellow;
                        }
                        else
                        {

                            try
                            {
                                #region
                                DataSet dsMP = g.ReturnData1("select Log_Date_Time from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + i + "'  and status='MP' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and DATEPART(Year, Log_Date_Time)='" + year + "'  and Emp_Name='" + gv.Rows[kk].Cells[0].Text + "'");
                                if (dsMP.Tables[0].Rows.Count > 0)
                                {//MP
                                    gv.Rows[kk].Cells[i].Text = "MP";
                                    gv.Rows[kk].Cells[i].BackColor = Color.GreenYellow;
                                    gv.Rows[kk].Cells[i].ForeColor = Color.Blue;
                                    gv.Rows[kk].Cells[i].Width = 10;

                                }
                                DataSet dsMH = g.ReturnData1("select Log_Date_Time from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + i + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MH' and DATEPART(Year, Log_Date_Time)='" + year + "'  and Emp_Name='" + gv.Rows[kk].Cells[0].Text + "'");
                                if (dsMH.Tables[0].Rows.Count > 0)
                                {//MH
                                    gv.Rows[kk].Cells[i].Text = "MH";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Yellow;
                                    gv.Rows[kk].Cells[i].Width = 100;
                                    gv.Rows[kk].Cells[i].Width = 10;
                                }
                                DataSet dsMA = g.ReturnData1("select Log_Date_Time from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + i + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MA' and DATEPART(Year, Log_Date_Time)='" + year + "'  and Emp_Name='" + gv.Rows[kk].Cells[0].Text + "'");
                                if (dsMA.Tables[0].Rows.Count > 0)
                                {//MA
                                    gv.Rows[kk].Cells[i].Text = "MA";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Turquoise;
                                    gv.Rows[kk].Cells[i].Width = 100;
                                    gv.Rows[kk].Cells[i].Width = 10;
                                }
                                DataSet dsHolidays = g.ReturnData1("select CONVERT(varchar,Date,101) from HoliDaysMaster where datepart(month,Date)='" + ddmonth.SelectedValue + "' and datepart(Year,Date)='" + year + "' and datepart(day,Date)='" + i + "'");
                                if (dsHolidays.Tables[0].Rows.Count > 0)
                                {//HOLIDAYS
                                    gv.Rows[kk].Cells[i].Text = "H";
                                    gv.Rows[kk].Cells[i].ToolTip = "Holiday";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Orange;
                                    gv.Rows[kk].Cells[i].Width = 10;
                                }
                                DataSet dsFrm = g.ReturnData1(@"select distinct  StartDate,EndDate  from tblLeaveApplication  LEFT OUTER JOIN EmployeeTB ON tblLeaveApplication.employeeID = EmployeeTB.EmployeeId where DATEPART(month, tblLeaveApplication.StartDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.StartDate)='" + year + "'  and DATEPART(month, tblLeaveApplication.EndDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.EndDate)='" + year + "'  and   Fname+''+lname='" + gv.Rows[kk].Cells[0].Text + "'");
                                // DataSet dsFrm = g.ReturnData1(@"select  StartDate,EndDate  from tblLeaveApplication  LEFT OUTER JOIN EmployeeTB ON tblLeaveApplication.employeeID = EmployeeTB.EmployeeId where DATEPART(month, tblLeaveApplication.StartDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.StartDate)='" + year + "'  and DATEPART(month, tblLeaveApplication.EndDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.EndDate)='" + year + "'  and EmployeeTB.FName+' '+ EmployeeTB.Lname='" + gv.Rows[kk].Cells[0].Text + "'");
                                if (dsFrm.Tables[0].Rows.Count > 0)
                                {//LEAVES
                                    DataSet dss = g.ReturnData1(@"DECLARE @Start DATETIME, @End DATETIME SELECT @Start='" + dsFrm.Tables[0].Rows[0]["StartDate"].ToString() + "' , @End = '" + dsFrm.Tables[0].Rows[0]["EndDate"].ToString() + "' ;WITH DateList AS (SELECT @Start [Date] UNION ALL SELECT [Date] +1 FROM DateList WHERE [Date] <@End) SELECT CONVERT(VARCHAR(Max), [Date],120) [mon] FROM DateList");
                                    for (int h = 0; h < dss.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dss.Tables[0].Rows[h][0] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[kk].Cells[days].Text = "L";
                                        gv.Rows[kk].Cells[days].ToolTip = "Leave";
                                        gv.Rows[kk].Cells[days].BackColor = Color.Yellow;
                                        gv.Rows[kk].Cells[days].Width = 10;

                                    }
                                }
                                else if (gv.Rows[kk].Cells[i].Text != "S"
                             && gv.Rows[kk].Cells[i].Text != "WO"
                             && gv.Rows[kk].Cells[i].Text != "P"
                             && gv.Rows[kk].Cells[i].Text != "MP"
                             && gv.Rows[kk].Cells[i].Text != "MH"
                             && gv.Rows[kk].Cells[i].Text != "MA"
                             && gv.Rows[kk].Cells[i].Text != "L"
                             && dsHolidays.Tables[0].Rows.Count == 0
                             && dsP.Tables[0].Rows.Count == 0
                             && dsMP.Tables[0].Rows.Count == 0 && dsMH.Tables[0].Rows.Count == 0)
                                //else if (gv.Rows[kk].Cells[i].Text != "S")
                                {
                                    gv.Rows[kk].Cells[i].Text = "A";
                                    gv.Rows[kk].Cells[i].ToolTip = "Absent";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Red;
                                }
                                        var fetchcopId = from d in ex.EmployeeTBs
                                                     where d.FName +" "+ d.MName +" "+ d.Lname == gv.Rows[kk].Cells[0].Text
                                                     select new { d.CompanyId };
                                    if (fetchcopId.Count() > 0)
                                    {
                                        foreach (var item in fetchcopId)
                                        {
                                            lblcompId.Text = Convert.ToString(item.CompanyId);
                                        }

                                    }
                                    else
                                    {
                                        lblcompId.Text = "";
                                    }
                                    if (lblcompId.Text != "")
                                    {
                                        #region Weeklyoff
                                        DataSet dsdays = g.ReturnData1("Select Distinct Days,TrackHolidays from weeklyofftb where companyid='" + Convert.ToInt32(lblcompId.Text) + "' And  DATEPART(Mm,effectdate )<='" + ddmonth.SelectedValue + "' and DATEPART(YYYY,effectdate)<='" + year + "'");
                                if (dsdays.Tables[0].Rows.Count > 0)
                                {
                                    for (int p = 0; p < dsdays.Tables[0].Rows.Count; p++)
                                    {
                                        string checkdays = dsdays.Tables[0].Rows[p]["TrackHolidays"].ToString();
                                       #region 1 and 2nd
                                        if (checkdays == "1 & 2")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,2) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                            }
                                        }
                                        #endregion

                                        #region 3 and 4th
                                        if (checkdays == "3 & 4")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (3,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                            }
                                        }
                                        #endregion

                                        #region 1 and 3th
                                        if (checkdays == "1 & 3")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                            }
                                        }
                                        #endregion

                                        #region 2 and 4th
                                        if (checkdays == "2 & 4")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                            }
                                        }
                                        #endregion

                                        #region 2 and 3th
                                        if (checkdays == "2 & 3")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                            }
                                        }
                                        #endregion

                                        #region 1 and 4th
                                        if (checkdays == "1 & 4")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                            }
                                        }
                                        #endregion

                                    }
                                }
                                #region ALL SUNDAYS
                               
                                    // sun++;
                                    DataSet dsSun = g.ReturnData1(@"DECLARE @date datetime
                                            SELECT @date = '" + date + "' SELECT [1st_sunday], DATENAME(weekday, [1st_sunday]) Daynames,[sunday] = DATEADD(DAY, n * 7, [1st_sunday]) FROM (SELECT [1st_sunday] = [1st_month] + 8 - DATEPART(weekday, [1st_month]) FROM (SELECT [1st_month] = DATEADD(MONTH, DATEDIFF(MONTH, 0, @date), 0)) d ) d CROSS JOIN (SELECT n = 0 UNION ALL SELECT n = 1 UNION ALL SELECT n = 2 UNION ALL SELECT n = 3 UNION ALL SELECT n = 4 ) n WHERE DATEDIFF(MONTH, @date, DATEADD(DAY, n * 7, [1st_sunday])) = 0");
                                    for (int h = 0; h < dsSun.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSun.Tables[0].Rows[h]["sunday"] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[kk].Cells[days].Text = "S";
                                        gv.Rows[kk].Cells[days].ToolTip = "Sunday";
                                        gv.Rows[kk].Cells[days].BackColor = Color.Orange;
                                        gv.Rows[kk].Cells[days].Width = 10;
                                    }
                                
                                #endregion
                                #endregion
                                    }
                                if (gv.Rows[kk].Cells[i].Text != "S"
                               && gv.Rows[kk].Cells[i].Text != "WO"
                               && gv.Rows[kk].Cells[i].Text != "P"
                               && gv.Rows[kk].Cells[i].Text != "MP"
                               && gv.Rows[kk].Cells[i].Text != "MH"
                               && gv.Rows[kk].Cells[i].Text != "MA"
                               && gv.Rows[kk].Cells[i].Text != "L" 
                               && gv.Rows[kk].Cells[i].Text != "A"
                               && dsHolidays.Tables[0].Rows.Count == 0
                               && dsP.Tables[0].Rows.Count == 0 
                               && dsMP.Tables[0].Rows.Count == 0 && dsMH.Tables[0].Rows.Count == 0)
                                //if (gv.Rows[kk].Cells[i].Text != "P")
                                {
                                    gv.Rows[kk].Cells[i].Text = "A";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Red;
                                }


                                #endregion

                            }
                            catch (Exception ex)
                            {
                                g.ShowMessage(this.Page, ex.Message);
                            }
                        }
                    }
                }
                //}

            }
            catch (Exception ex)
            {
                g.ShowMessage(this.Page, ex.Message);
            }
            #endregion

        }
        else if (ddlCompany.SelectedIndex == 0 && ddEmp.SelectedIndex != 0)
        {//Emp

            #region
            dtd = new DataTable();
            int k = System.DateTime.DaysInMonth(int.Parse(DateTime.Now.Year.ToString()), (ddmonth.SelectedIndex));
            #region
            for (int j = 0; j < k + 1; j++)
            {//Columns
                DataColumn drmon = new DataColumn();
                if (j == 0)
                {
                    drmon.ColumnName = "Days";
                }
                else
                {
                    drmon.ColumnName = j.ToString();
                }
                dtd.Columns.Add(drmon.ToString());
            }

            DataColumn drWD = new DataColumn();
            DataColumn drHoliSun = new DataColumn();
            DataColumn drLeaves = new DataColumn();

            drWD.ColumnName = "WD";
            drHoliSun.ColumnName = "Holiday/Sunday";
            drLeaves.ColumnName = "Leaves";

            dtd.Columns.Add(drWD.ToString());
            dtd.Columns.Add(drHoliSun.ToString());
            dtd.Columns.Add(drLeaves.ToString());


            DataSet dsEmp = g.ReturnData1(@"SELECT DISTINCT [t1].[value] AS [Name], [t1].[EmployeeId] FROM (SELECT [t0].[FName] +' '+ [t0].[Mname] +' ' + [t0].[Lname] AS [value], [t0].[EmployeeId], [t0].[RelivingStatus], [t0].[CompanyId]
               FROM [dbo].[EmployeeTB] AS [t0] ) AS [t1] WHERE ([t1].[RelivingStatus] IS NULL)  and t1.EmployeeId='" + Convert.ToInt32(ddEmp.SelectedValue) + "'");
            if (dsEmp.Tables[0].Rows.Count > 0)
            {
                //for (int i = 0; i < dtd.Columns.Count; i++)
                //{//Rows
                try
                {
                    DataRow drEmp = dtd.NewRow();
                    drEmp[0] = dsEmp.Tables[0].Rows[0]["Name"];

                    dtd.Rows.Add(drEmp[0].ToString());

                }
                catch (Exception)
                {

                    throw;
                }

                //}
            }
            gv.DataSource = dtd;
            gv.DataBind();
            #endregion
            try
            {
                
                for (int j = 0; j < dtd.Rows.Count; j++)
                {
                   int countp = 0;
                   
                    gv.Rows[0].Cells[0].Width = 50;
                    for (int kk = 1; kk < gv.Rows[j].Cells.Count-3; kk++)
                    {
                        int countL = 0;
                        int CountS = 0;
                        int countweakOf = 0;
                        DataSet dsP = g.ReturnData1("select * from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + kk + "'  and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "' and DATEPART(Year, Log_Date_Time)='" + year + "'   and status is null  and Employee_id='" + ddEmp.SelectedValue + "'");
                        if (dsP.Tables[0].Rows.Count > 0)
                        {//Present
                            gv.Rows[j].Cells[kk].Text = "P";
                            gv.Rows[j].Cells[kk].BackColor = Color.GreenYellow;
                            countp++;
                            gv.Rows[j].Cells[k + 1].Text = countp.ToString();
                        }
                        else
                        {
                            DataSet dsMP = g.ReturnData1("select * from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + kk + "'  and status='MP' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and DATEPART(Year, Log_Date_Time)='" + year + "'  and Employee_id='" + ddEmp.SelectedValue + "'");
                            if (dsMP.Tables[0].Rows.Count > 0)
                            {//MP
                                gv.Rows[j].Cells[kk].Text = "MP";
                                gv.Rows[j].Cells[kk].BackColor = Color.GreenYellow;
                                gv.Rows[j].Cells[kk].ForeColor = Color.Blue;
                                gv.Rows[j].Cells[kk].Width = 10;

                            }
                            DataSet dsMH = g.ReturnData1("select * from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + kk + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MH' and DATEPART(Year, Log_Date_Time)='" + year + "'  and Employee_id='" + ddEmp.SelectedValue + "'");
                            if (dsMH.Tables[0].Rows.Count > 0)
                            {//MH
                                gv.Rows[j].Cells[kk].Text = "MH";
                                gv.Rows[j].Cells[kk].BackColor = Color.Yellow;
                                gv.Rows[j].Cells[kk].Width = 100;
                                gv.Rows[j].Cells[kk].Width = 10;
                            }
                            DataSet dsMA = g.ReturnData1("select * from LogRecordsDetails where DATEPART(day, Log_Date_Time)='" + kk + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MA' and DATEPART(Year, Log_Date_Time)='" + year + "'  and Employee_id='" + ddEmp.SelectedValue + "'");
                            if (dsMA.Tables[0].Rows.Count > 0)
                            {//MA
                                gv.Rows[j].Cells[kk].Text = "MA";
                                gv.Rows[j].Cells[kk].BackColor = Color.Turquoise;
                                gv.Rows[j].Cells[kk].Width = 100;
                                gv.Rows[j].Cells[kk].Width = 10;
                            }

                            DataSet dsHolidays = g.ReturnData1("select CONVERT(varchar,Date,101) from HoliDaysMaster where datepart(month,Date)='" + ddmonth.SelectedValue + "' and datepart(Year,Date)='" + year + "' and datepart(day,Date)='" + kk + "'");
                            if (dsHolidays.Tables[0].Rows.Count > 0)
                            {//HOLIDAYS
                                gv.Rows[j].Cells[kk].Text = "H";
                                gv.Rows[j].Cells[kk].ToolTip = "Holiday";
                                gv.Rows[j].Cells[kk].BackColor = Color.Orange;
                                gv.Rows[j].Cells[kk].Width = 10;
                                CountS++;
                                gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                            }

                            DataSet dsFrm = g.ReturnData1(@"select  StartDate,EndDate  from tblLeaveApplication where DATEPART(month, tblLeaveApplication.StartDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.StartDate)='" + year + "'  and DATEPART(month, tblLeaveApplication.EndDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.EndDate)='" + year + "'  and employeeID='" + ddEmp.SelectedValue + "'");
                            if (dsFrm.Tables[0].Rows.Count > 0)
                            {//LEAVES
                                DataSet dss = g.ReturnData1(@"DECLARE @Start DATETIME, @End DATETIME SELECT @Start='" + dsFrm.Tables[0].Rows[j]["StartDate"].ToString() + "' , @End = '" + dsFrm.Tables[0].Rows[j]["EndDate"].ToString() + "' ;WITH DateList AS (SELECT @Start [Date] UNION ALL SELECT [Date] +1 FROM DateList WHERE [Date] <@End) SELECT CONVERT(VARCHAR(Max), [Date],120) [mon] FROM DateList");
                                for (int h = 0; h < dss.Tables[0].Rows.Count; h++)
                                {
                                    DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dss.Tables[0].Rows[h][0] + "')");
                                    int days = 0;
                                    days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                    gv.Rows[j].Cells[days].Text = "L";
                                    gv.Rows[j].Cells[days].ToolTip = "Leave";
                                    gv.Rows[j].Cells[days].BackColor = Color.Yellow;
                                    gv.Rows[j].Cells[days].Width = 10;
                                    countL++;
                                    gv.Rows[j].Cells[k + 3].Text = countL.ToString();


                                }
                            }
                            else if (gv.Rows[j].Cells[kk].Text != "S" && gv.Rows[j].Cells[kk].Text != "WO" && gv.Rows[j].Cells[kk].Text != "MP" && gv.Rows[j].Cells[kk].Text != "P" && gv.Rows[j].Cells[kk].Text != "L" && gv.Rows[j].Cells[kk].Text != "MH" && gv.Rows[j].Cells[kk].Text != "H" && gv.Rows[j].Cells[kk].Text != "MA")
                            {
                                gv.Rows[j].Cells[kk].Text = "A";
                                gv.Rows[j].Cells[kk].ToolTip = "Absent";
                                gv.Rows[j].Cells[kk].BackColor = Color.Red;
                            }


                            if (ddEmp.SelectedIndex > 0)
                            {
                                var fetchcopId = from d in ex.EmployeeTBs
                                                 where d.EmployeeId == Convert.ToInt32(ddEmp.SelectedValue)
                                                 select new {d.CompanyId };
                                if (fetchcopId.Count() > 0)
                                {
                                    foreach (var item in fetchcopId)
	                              {
                                      lblcompId.Text = Convert.ToString(item.CompanyId);
	                              }
                                    
                                }
                                else
                                {
                                    lblcompId.Text = "";
                                }
                            }



                            #region Weeklyoff
                            DataSet dsdays = g.ReturnData1("Select Distinct Days,TrackHolidays from weeklyofftb where companyid='" +Convert.ToInt32(lblcompId.Text) + "' and  DATEPART(Mm,effectdate )<='" + ddmonth.SelectedValue + "' and DATEPART(YYYY,effectdate)<='" + year + "'");
                            if (dsdays.Tables[0].Rows.Count > 0)
                            {
                                for (int p = 0; p < dsdays.Tables[0].Rows.Count; p++)
                                {
                                    string checkdays = dsdays.Tables[0].Rows[p]["TrackHolidays"].ToString();
                                  

                                    #region 1 and 2nd
                                    if (checkdays == "1 & 2")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,2) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            CountS++;
                                            gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                            countweakOf++;
                                        }
                                    }
                                    #endregion

                                    #region 3 and 4th
                                    if (checkdays == "3 & 4")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (3,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;

                                            gv.Rows[j].Cells[days].Width = 10;
                                            countweakOf++;
                                            CountS++;
                                            gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                        }
                                    }
                                    #endregion

                                    #region 1 and 3th
                                    if (checkdays == "1 & 3")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            countweakOf++;
                                            CountS++;
                                            gv.Rows[j].Cells[k + 1].Text = CountS.ToString();
                                        }
                                    }
                                    #endregion

                                    #region 2 and 4th
                                    if (checkdays == "2 & 4")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            countweakOf++;
                                            CountS++;
                                            gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                        }
                                    }
                                    #endregion

                                    #region 2 and 3th
                                    if (checkdays == "2 & 3")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            countweakOf++;
                                            CountS++;
                                            gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                        }
                                    }
                                    #endregion

                                    #region 1 and 4th
                                    if (checkdays == "1 & 4")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            countweakOf++;
                                            CountS++;
                                            gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                            
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #region ALL SUNDAYS
                           
                                // sun++;
                                DataSet dsSun = g.ReturnData1(@"DECLARE @date datetime
                                 SELECT @date = '" + date + "' SELECT [1st_sunday], DATENAME(weekday, [1st_sunday]) Daynames,[sunday] = DATEADD(DAY, n * 7, [1st_sunday]) FROM (SELECT [1st_sunday] = [1st_month] + 8 - DATEPART(weekday, [1st_month]) FROM (SELECT [1st_month] = DATEADD(MONTH, DATEDIFF(MONTH, 0, @date), 0)) d ) d CROSS JOIN (SELECT n = 0 UNION ALL SELECT n = 1 UNION ALL SELECT n = 2 UNION ALL SELECT n = 3 UNION ALL SELECT n = 4 ) n WHERE DATEDIFF(MONTH, @date, DATEADD(DAY, n * 7, [1st_sunday])) = 0");
                                for (int h = 0; h < dsSun.Tables[0].Rows.Count; h++)
                                {
                                    DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSun.Tables[0].Rows[h]["sunday"] + "')");
                                    int days = 0;
                                    days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                    gv.Rows[j].Cells[days].Text = "S";
                                    gv.Rows[j].Cells[days].ToolTip = "Sunday";
                                    gv.Rows[j].Cells[days].BackColor = Color.Orange;
                                    gv.Rows[j].Cells[days].Width = 10;
                                    CountS++;
                                    gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                }
                           
                            #endregion

                            #endregion

                                if (gv.Rows[j].Cells[kk].Text != "S"
                                && gv.Rows[j].Cells[kk].Text != "WO"
                                && gv.Rows[j].Cells[kk].Text != "P"
                                && gv.Rows[j].Cells[kk].Text != "MP"
                                && gv.Rows[j].Cells[kk].Text != "MH"
                                && gv.Rows[j].Cells[kk].Text != "MA"
                                && gv.Rows[j].Cells[kk].Text != "L"
                                && dsHolidays.Tables[0].Rows.Count == 0
                                && dsP.Tables[0].Rows.Count == 0
                                && dsMP.Tables[0].Rows.Count == 0 && dsMH.Tables[0].Rows.Count == 0)
                            {//ABSENT
                                // gv.Rows[j].Cells[kk].ForeColor = Color.White;
                                // gv.Rows[j].Cells[kk].Width = 10;
                                //gv.Rows[j].Cells[kk].Font.Size=10;
                                // gv.Rows[j].Cells[kk].Font.Bold = true;
                                gv.Rows[j].Cells[kk].Text = "A";
                                gv.Rows[j].Cells[kk].BackColor = Color.Red;
                               // int TotalP = countp - countweakOf;

                                //gv.Rows[j].Cells[k + 1].Text = countp.ToString();
                                //gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                //gv.Rows[j].Cells[k + 3].Text = countL.ToString();

                                //gv.Rows[j].Cells[k + 1].BackColor = Color.GreenYellow;
                                //gv.Rows[j].Cells[k + 2].BackColor = Color.LightCyan;
                                //gv.Rows[j].Cells[k + 3].BackColor = Color.Yellow;
                            }
                               
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                g.ShowMessage(this.Page, ex.Message);
            }
            #endregion
        }
        else if (ddlCompany.SelectedIndex != 0 && ddEmp.SelectedIndex == 0)
        {//compnay wise all employee
            #region

            dtd = new DataTable();
            int k = System.DateTime.DaysInMonth(int.Parse(DateTime.Now.Year.ToString()), (ddmonth.SelectedIndex));
            #region

            for (int j = 0; j < k + 1; j++)
            {//Columns
                DataColumn drmon = new DataColumn();

                if (j == 0)
                {
                    drmon.ColumnName = " ";
                }
                else
                {

                    drmon.ColumnName = j.ToString();
                }

                dtd.Columns.Add(drmon.ToString());
            }
            DataColumn drWD = new DataColumn();
            DataColumn drHoliSun = new DataColumn();
            DataColumn drLeaves = new DataColumn();

            drWD.ColumnName = "WD";
            drHoliSun.ColumnName = "Holiday/Sunday";
            drLeaves.ColumnName = "Leaves";

            dtd.Columns.Add(drWD.ToString());
            dtd.Columns.Add(drHoliSun.ToString());
            dtd.Columns.Add(drLeaves.ToString());

            DataSet dsEmp = g.ReturnData1(@"SELECT DISTINCT   t1.MachineID,[t1].[value] AS [Name], [t1].[EmployeeId] FROM (SELECT  [t0].MachineID,[t0].[FName] +' '+ [t0].[Mname] +' ' + [t0].[Lname] AS [value], [t0].[EmployeeId], [t0].[RelivingStatus], [t0].[CompanyId]
                    FROM [dbo].[EmployeeTB] AS [t0] ) AS [t1] WHERE ([t1].[RelivingStatus] IS NULL)  and t1.machineID  !=0 AND t1.machineID is not null and t1.CompanyId='" + Convert.ToInt32(ddlCompany.SelectedValue) + "'");
            if (dsEmp.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsEmp.Tables[0].Rows.Count; i++)
                //  for (int i = 0; i < dtd.Columns.Count; i++)
                {//Rows

                    try
                    {
                        DataRow drEmp = dtd.NewRow();
                        drEmp[0] = dsEmp.Tables[0].Rows[i]["Name"];
                        dtd.Rows.Add(drEmp[0].ToString());
                    }
                    catch (Exception ex)
                    {

                        g.ShowMessage(this.Page,ex.Message);
                        
                    }

                }
            }
            gv.DataSource = dtd;
            gv.DataBind();
            #endregion
            try
            {
                for (int kk = 0; kk < gv.Rows.Count; kk++)
                {
                    int countp = 0;
                    // for (int i = 1; i <=gv.Rows[kk].Cells.Count-1; i++)
                    for (int i = 1; i < gv.Rows[kk].Cells.Count - 3; i++)
                    {
                        int countL = 0;
                        int CountS = 0;
                        DataSet dsP = g.ReturnData1(@"select [Emp_Name],[Status] from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + i + "'  and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "' and DATEPART(Year, Log_Date_Time)='" + year + "'  and status is null  and Emp_Name='" + gv.Rows[kk].Cells[0].Text + "' and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "'");
                        if (dsP.Tables[0].Rows.Count > 0)
                        {//Present

                            gv.Rows[kk].Cells[i].Text = "P";
                            gv.Rows[kk].Cells[i].BackColor = Color.GreenYellow;
                            //countp++;
                            //gv.Rows[kk].Cells[k + 1].Text = countp.ToString();
                        }
                        else
                        {

                            try
                            {
                                #region
                                DataSet dsMP = g.ReturnData1("select Log_Date_Time from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + i + "'  and status='MP' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and DATEPART(Year, Log_Date_Time)='" + year + "'  and Emp_Name='" + gv.Rows[kk].Cells[0].Text + "' and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "'");
                                if (dsMP.Tables[0].Rows.Count > 0)
                                {//MP
                                    gv.Rows[kk].Cells[i].Text = "MP";
                                    gv.Rows[kk].Cells[i].BackColor = Color.GreenYellow;
                                    gv.Rows[kk].Cells[i].ForeColor = Color.Blue;
                                    gv.Rows[kk].Cells[i].Width = 10;

                                }
                                DataSet dsMH = g.ReturnData1("select Log_Date_Time from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + i + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MH' and DATEPART(Year, Log_Date_Time)='" + year + "'  and Emp_Name='" + gv.Rows[kk].Cells[0].Text + "' and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "'");
                                if (dsMH.Tables[0].Rows.Count > 0)
                                {//MH
                                    gv.Rows[kk].Cells[i].Text = "MH";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Yellow;
                                    gv.Rows[kk].Cells[i].Width = 100;
                                    gv.Rows[kk].Cells[i].Width = 10;
                                }
                                DataSet dsMA = g.ReturnData1("select Log_Date_Time from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + i + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MA' and DATEPART(Year, Log_Date_Time)='" + year + "'  and Emp_Name='" + gv.Rows[kk].Cells[0].Text + "' and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "'");
                                if (dsMA.Tables[0].Rows.Count > 0)
                                {//MA
                                    gv.Rows[kk].Cells[i].Text = "MA";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Turquoise;
                                    gv.Rows[kk].Cells[i].Width = 100;
                                    gv.Rows[kk].Cells[i].Width = 10;
                                }
                                DataSet dsHolidays = g.ReturnData1("select CONVERT(varchar,Date,101) from HoliDaysMaster where datepart(month,Date)='" + ddmonth.SelectedValue + "' and datepart(Year,Date)='" + year + "' and datepart(day,Date)='" + i + "'");
                                if (dsHolidays.Tables[0].Rows.Count > 0)
                                {//HOLIDAYS
                                    gv.Rows[kk].Cells[i].Text = "H";
                                    gv.Rows[kk].Cells[i].ToolTip = "Holiday";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Orange;
                                    gv.Rows[kk].Cells[i].Width = 10;
                                    //CountS++;
                                    //gv.Rows[kk].Cells[k + 2].Text = CountS.ToString();
                                }
                                DataSet dsFrm = g.ReturnData1(@"select distinct  StartDate,EndDate  from tblLeaveApplication  LEFT OUTER JOIN EmployeeTB ON tblLeaveApplication.employeeID = EmployeeTB.EmployeeId where DATEPART(month, tblLeaveApplication.StartDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.StartDate)='" + year + "'  and DATEPART(month, tblLeaveApplication.EndDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.EndDate)='" + year + "'  and   Fname+''+lname='" + gv.Rows[kk].Cells[0].Text + "' and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "'");
                                // DataSet dsFrm = g.ReturnData1(@"select  StartDate,EndDate  from tblLeaveApplication  LEFT OUTER JOIN EmployeeTB ON tblLeaveApplication.employeeID = EmployeeTB.EmployeeId where DATEPART(month, tblLeaveApplication.StartDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.StartDate)='" + year + "'  and DATEPART(month, tblLeaveApplication.EndDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.EndDate)='" + year + "'  and EmployeeTB.FName+' '+ EmployeeTB.Lname='" + gv.Rows[kk].Cells[0].Text + "'");
                                if (dsFrm.Tables[0].Rows.Count > 0)
                                {//LEAVES
                                    DataSet dss = g.ReturnData1(@"DECLARE @Start DATETIME, @End DATETIME SELECT @Start='" + dsFrm.Tables[0].Rows[0]["StartDate"].ToString() + "' , @End = '" + dsFrm.Tables[0].Rows[0]["EndDate"].ToString() + "' ;WITH DateList AS (SELECT @Start [Date] UNION ALL SELECT [Date] +1 FROM DateList WHERE [Date] <@End) SELECT CONVERT(VARCHAR(Max), [Date],120) [mon] FROM DateList");
                                    for (int h = 0; h < dss.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dss.Tables[0].Rows[h][0] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[kk].Cells[days].Text = "L";
                                        gv.Rows[kk].Cells[days].ToolTip = "Leave";
                                        gv.Rows[kk].Cells[days].BackColor = Color.Yellow;
                                        gv.Rows[kk].Cells[days].Width = 10;
                                        //countL++;
                                        //gv.Rows[kk].Cells[k + 3].Text = countL.ToString();

                                    }
                                }
                                else if (gv.Rows[kk].Cells[i].Text != "S"
                             && gv.Rows[kk].Cells[i].Text != "WO"
                             && gv.Rows[kk].Cells[i].Text != "P"
                             && gv.Rows[kk].Cells[i].Text != "MP"
                             && gv.Rows[kk].Cells[i].Text != "MH"
                             && gv.Rows[kk].Cells[i].Text != "MA"
                             && gv.Rows[kk].Cells[i].Text != "L"
                             && dsHolidays.Tables[0].Rows.Count == 0
                             && dsP.Tables[0].Rows.Count == 0
                             && dsMP.Tables[0].Rows.Count == 0 && dsMH.Tables[0].Rows.Count == 0)
                                //else if (gv.Rows[kk].Cells[i].Text != "S")
                                {
                                    gv.Rows[kk].Cells[i].Text = "A";
                                    gv.Rows[kk].Cells[i].ToolTip = "Absent";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Red;
                                }
                                var fetchcopId = from d in ex.EmployeeTBs
                                                 where d.FName + " " +d.MName + " " + d.Lname == gv.Rows[kk].Cells[0].Text
                                                 select new { d.CompanyId };
                                if (fetchcopId.Count() > 0)
                                {
                                    foreach (var item in fetchcopId)
                                    {
                                        lblcompId.Text = Convert.ToString(item.CompanyId);
                                    }

                                }
                                else
                                {
                                    lblcompId.Text = "";
                                }

                                #region Weeklyoff
                                DataSet dsdays = g.ReturnData1("Select Distinct Days,TrackHolidays from weeklyofftb where companyid='" + Convert.ToInt32(lblcompId.Text) + "' And  DATEPART(Mm,effectdate )<='" + ddmonth.SelectedValue + "' and DATEPART(YYYY,effectdate)<='" + year + "'");
                                if (dsdays.Tables[0].Rows.Count > 0)
                                {
                                    for (int p = 0; p < dsdays.Tables[0].Rows.Count; p++)
                                    {
                                        string checkdays = dsdays.Tables[0].Rows[p]["TrackHolidays"].ToString();
                                         #region 1 and 2nd
                                        if (checkdays == "1 & 2")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,2) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                                //CountS++;
                                                //gv.Rows[kk].Cells[k + 2].Text = CountS.ToString();
                                            }
                                        }
                                        #endregion

                                        #region 3 and 4th
                                        if (checkdays == "3 & 4")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (3,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                                //CountS++;
                                                //gv.Rows[kk].Cells[k + 2].Text = CountS.ToString();
                                            }
                                        }
                                        #endregion

                                        #region 1 and 3th
                                        if (checkdays == "1 & 3")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                                //CountS++;
                                                //gv.Rows[kk].Cells[k + 2].Text = CountS.ToString();
                                            }
                                        }
                                        #endregion

                                        #region 2 and 4th
                                        if (checkdays == "2 & 4")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                                //CountS++;
                                                //gv.Rows[kk].Cells[k + 2].Text = CountS.ToString();
                                            }
                                        }
                                        #endregion

                                        #region 2 and 3th
                                        if (checkdays == "2 & 3")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                                //CountS++;
                                                //gv.Rows[kk].Cells[k + 2].Text = CountS.ToString();
                                            }
                                        }
                                        #endregion

                                        #region 1 and 4th
                                        if (checkdays == "1 & 4")
                                        {

                                            DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                            for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                            {
                                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                                int days = 0;
                                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                                gv.Rows[kk].Cells[days].Text = "WO";
                                                gv.Rows[kk].Cells[days].ToolTip = "Saturday";
                                                gv.Rows[kk].Cells[days].BackColor = Color.YellowGreen;
                                                gv.Rows[kk].Cells[days].Width = 10;
                                                //CountS++;
                                                //gv.Rows[kk].Cells[k + 2].Text = CountS.ToString();
                                            }
                                        }
                                        #endregion

                                    }
                                }
                                #region ALL SUNDAYS
                               
                                    // sun++;
                                    DataSet dsSun = g.ReturnData1(@"DECLARE @date datetime
                                            SELECT @date = '" + date + "' SELECT [1st_sunday], DATENAME(weekday, [1st_sunday]) Daynames,[sunday] = DATEADD(DAY, n * 7, [1st_sunday]) FROM (SELECT [1st_sunday] = [1st_month] + 8 - DATEPART(weekday, [1st_month]) FROM (SELECT [1st_month] = DATEADD(MONTH, DATEDIFF(MONTH, 0, @date), 0)) d ) d CROSS JOIN (SELECT n = 0 UNION ALL SELECT n = 1 UNION ALL SELECT n = 2 UNION ALL SELECT n = 3 UNION ALL SELECT n = 4 ) n WHERE DATEDIFF(MONTH, @date, DATEADD(DAY, n * 7, [1st_sunday])) = 0");
                                    for (int h = 0; h < dsSun.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSun.Tables[0].Rows[h]["sunday"] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[kk].Cells[days].Text = "S";
                                        gv.Rows[kk].Cells[days].ToolTip = "Sunday";
                                        gv.Rows[kk].Cells[days].BackColor = Color.Orange;
                                        gv.Rows[kk].Cells[days].Width = 10;
                                        //CountS++;
                                        //gv.Rows[kk].Cells[k + 2].Text = CountS.ToString();
                                    }
                               
                                #endregion
                                #endregion

                                if (gv.Rows[kk].Cells[i].Text != "S"
                               && gv.Rows[kk].Cells[i].Text != "WO"
                               && gv.Rows[kk].Cells[i].Text != "P"
                               && gv.Rows[kk].Cells[i].Text != "MP"
                               && gv.Rows[kk].Cells[i].Text != "MH"
                               && gv.Rows[kk].Cells[i].Text != "MA"
                               && gv.Rows[kk].Cells[i].Text != "L"
                               && gv.Rows[kk].Cells[i].Text != "A"
                               && dsHolidays.Tables[0].Rows.Count == 0
                               && dsP.Tables[0].Rows.Count == 0
                               && dsMP.Tables[0].Rows.Count == 0 && dsMH.Tables[0].Rows.Count == 0)
                                //if (gv.Rows[kk].Cells[i].Text != "P")
                                {
                                    gv.Rows[kk].Cells[i].Text = "A";
                                    gv.Rows[kk].Cells[i].BackColor = Color.Red;

                                    //gv.Rows[kk].Cells[k + 1].Text = countp.ToString();
                                    //gv.Rows[kk].Cells[k + 2].Text = CountS.ToString();
                                    //gv.Rows[kk].Cells[k + 3].Text = countL.ToString();

                                    //gv.Rows[kk].Cells[k + 1].BackColor = Color.GreenYellow;
                                    //gv.Rows[kk].Cells[k + 2].BackColor = Color.LightCyan;
                                    //gv.Rows[kk].Cells[k + 3].BackColor = Color.Yellow;
                                }


                                #endregion

                            }
                            catch (Exception ex)
                            {
                                g.ShowMessage(this.Page, ex.Message);
                            }
                        }
                    }
                }
                //}

            }
            catch (Exception ex)
            {
                g.ShowMessage(this.Page, ex.Message);
            }
            #endregion
        }
        else if (ddlCompany.SelectedIndex != 0 && ddEmp.SelectedIndex != 0)
        {//compnay wise and employee wise
            #region
            dtd = new DataTable();
            int k = System.DateTime.DaysInMonth(int.Parse(DateTime.Now.Year.ToString()), (ddmonth.SelectedIndex));
            #region
            for (int j = 0; j < k + 1; j++)
            {//Columns
                DataColumn drmon = new DataColumn();
                if (j == 0)
                {
                    drmon.ColumnName = "Days";
                }
                else
                {
                    drmon.ColumnName = j.ToString();
                }
                dtd.Columns.Add(drmon.ToString());
            }

            DataColumn drWD = new DataColumn();
            DataColumn drHoliSun = new DataColumn();
            DataColumn drLeaves = new DataColumn();

            drWD.ColumnName = "WD";
            drHoliSun.ColumnName = "Holiday/Sunday";
            drLeaves.ColumnName = "Leaves";

            dtd.Columns.Add(drWD.ToString());
            dtd.Columns.Add(drHoliSun.ToString());
            dtd.Columns.Add(drLeaves.ToString());


            DataSet dsEmp = g.ReturnData1(@"SELECT DISTINCT [t1].[value] AS [Name], [t1].[EmployeeId] FROM (SELECT [t0].[FName] +' '+ [t0].[Mname] +' ' + [t0].[Lname] AS [value], [t0].[EmployeeId], [t0].[RelivingStatus], [t0].[CompanyId]
               FROM [dbo].[EmployeeTB] AS [t0] ) AS [t1] WHERE ([t1].[RelivingStatus] IS NULL)  and t1.CompanyId='" + Convert.ToInt32(ddlCompany.SelectedValue) + "'  and t1.EmployeeId='" + Convert.ToInt32(ddEmp.SelectedValue) + "'");
            if (dsEmp.Tables[0].Rows.Count > 0)
            {
                //for (int i = 0; i < dtd.Columns.Count; i++)
                //{//Rows
                try
                {
                    DataRow drEmp = dtd.NewRow();
                    drEmp[0] = dsEmp.Tables[0].Rows[0]["Name"];

                    dtd.Rows.Add(drEmp[0].ToString());

                }
                catch (Exception)
                {

                    throw;
                }

                //}
            }
            gv.DataSource = dtd;
            gv.DataBind();
            #endregion
            try
            {

                for (int j = 0; j < dtd.Rows.Count; j++)
                {
                    int countp = 0;

                    gv.Rows[0].Cells[0].Width = 50;
                    for (int kk = 1; kk < gv.Rows[j].Cells.Count - 3; kk++)
                    {
                        int countL = 0;
                        int CountS = 0;
                        int countweakOf = 0;
                        DataSet dsP = g.ReturnData1("select * from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + kk + "'  and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "' and DATEPART(Year, Log_Date_Time)='" + year + "'   and status is null  and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "' and LogRecordsDetails.Employee_id='" + ddEmp.SelectedValue + "'");
                        if (dsP.Tables[0].Rows.Count > 0)
                        {//Present
                            gv.Rows[j].Cells[kk].Text = "P";
                            gv.Rows[j].Cells[kk].BackColor = Color.GreenYellow;
                            //countp++;
                            //gv.Rows[j].Cells[k + 1].Text = countp.ToString();
                        }
                        else
                        {
                            DataSet dsMP = g.ReturnData1("select * from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + kk + "'  and status='MP' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and DATEPART(Year, Log_Date_Time)='" + year + "'  and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "' and LogRecordsDetails.Employee_id='" + ddEmp.SelectedValue + "'");
                            if (dsMP.Tables[0].Rows.Count > 0)
                            {//MP
                                gv.Rows[j].Cells[kk].Text = "MP";
                                gv.Rows[j].Cells[kk].BackColor = Color.GreenYellow;
                                gv.Rows[j].Cells[kk].ForeColor = Color.Blue;
                                gv.Rows[j].Cells[kk].Width = 10;

                            }
                            DataSet dsMH = g.ReturnData1("select * from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + kk + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MH' and DATEPART(Year, Log_Date_Time)='" + year + "'  and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "' and LogRecordsDetails.Employee_id='" + ddEmp.SelectedValue + "'");
                            if (dsMH.Tables[0].Rows.Count > 0)
                            {//MH
                                gv.Rows[j].Cells[kk].Text = "MH";
                                gv.Rows[j].Cells[kk].BackColor = Color.Yellow;
                                gv.Rows[j].Cells[kk].Width = 100;
                                gv.Rows[j].Cells[kk].Width = 10;
                            }
                            DataSet dsMA = g.ReturnData1("select * from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + kk + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MA' and DATEPART(Year, Log_Date_Time)='" + year + "'  and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "' and LogRecordsDetails.Employee_id='" + ddEmp.SelectedValue + "'");
                            if (dsMA.Tables[0].Rows.Count > 0)
                            {//MA
                                gv.Rows[j].Cells[kk].Text = "MA";
                                gv.Rows[j].Cells[kk].BackColor = Color.Turquoise;
                                gv.Rows[j].Cells[kk].Width = 100;
                                gv.Rows[j].Cells[kk].Width = 10;
                            }

                            DataSet dsHolidays = g.ReturnData1("select CONVERT(varchar,Date,101) from HoliDaysMaster where datepart(month,Date)='" + ddmonth.SelectedValue + "' and datepart(Year,Date)='" + year + "' and datepart(day,Date)='" + kk + "'");
                            if (dsHolidays.Tables[0].Rows.Count > 0)
                            {//HOLIDAYS
                                gv.Rows[j].Cells[kk].Text = "H";
                                gv.Rows[j].Cells[kk].ToolTip = "Holiday";
                                gv.Rows[j].Cells[kk].BackColor = Color.Orange;
                                gv.Rows[j].Cells[kk].Width = 10;
                                //CountS++;
                                //gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                            }

                            DataSet dsFrm = g.ReturnData1(@"select  StartDate,EndDate  from tblLeaveApplication left outer join EmployeeTB on tblLeaveApplication.employeeID= EmployeeTB.EmployeeId where DATEPART(month, tblLeaveApplication.StartDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.StartDate)='" + year + "'  and DATEPART(month, tblLeaveApplication.EndDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.EndDate)='" + year + "'  and EmployeeTB.CompanyId='" + ddlCompany.SelectedValue + "' and tblLeaveApplication.employeeID='" + ddEmp.SelectedValue + "'");
                            if (dsFrm.Tables[0].Rows.Count > 0)
                            {//LEAVES
                                DataSet dss = g.ReturnData1(@"DECLARE @Start DATETIME, @End DATETIME SELECT @Start='" + dsFrm.Tables[0].Rows[j]["StartDate"].ToString() + "' , @End = '" + dsFrm.Tables[0].Rows[j]["EndDate"].ToString() + "' ;WITH DateList AS (SELECT @Start [Date] UNION ALL SELECT [Date] +1 FROM DateList WHERE [Date] <@End) SELECT CONVERT(VARCHAR(Max), [Date],120) [mon] FROM DateList");
                                for (int h = 0; h < dss.Tables[0].Rows.Count; h++)
                                {
                                    DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dss.Tables[0].Rows[h][0] + "')");
                                    int days = 0;
                                    days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                    gv.Rows[j].Cells[days].Text = "L";
                                    gv.Rows[j].Cells[days].ToolTip = "Leave";
                                    gv.Rows[j].Cells[days].BackColor = Color.Yellow;
                                    gv.Rows[j].Cells[days].Width = 10;
                                    //countL++;
                                    //gv.Rows[j].Cells[k + 3].Text = countL.ToString();


                                }
                            }
                            else if (gv.Rows[j].Cells[kk].Text != "S" && gv.Rows[j].Cells[kk].Text != "WO" && gv.Rows[j].Cells[kk].Text != "MP" && gv.Rows[j].Cells[kk].Text != "P" && gv.Rows[j].Cells[kk].Text != "L" && gv.Rows[j].Cells[kk].Text != "MH" && gv.Rows[j].Cells[kk].Text != "H" && gv.Rows[j].Cells[kk].Text != "MA")
                            {
                                gv.Rows[j].Cells[kk].Text = "A";
                                gv.Rows[j].Cells[kk].ToolTip = "Absent";
                                gv.Rows[j].Cells[kk].BackColor = Color.Red;
                            }


                            if (ddEmp.SelectedIndex > 0)
                            {
                                var fetchcopId = from d in ex.EmployeeTBs
                                                 where d.EmployeeId == Convert.ToInt32(ddEmp.SelectedValue)
                                                 select new { d.CompanyId };
                                if (fetchcopId.Count() > 0)
                                {
                                    foreach (var item in fetchcopId)
                                    {
                                        lblcompId.Text = Convert.ToString(item.CompanyId);
                                    }

                                }
                                else
                                {
                                    lblcompId.Text = "";
                                }
                            }



                            #region Weeklyoff
                            DataSet dsdays = g.ReturnData1("Select Distinct Days,TrackHolidays from weeklyofftb where companyid='" + Convert.ToInt32(lblcompId.Text) + "' and  DATEPART(Mm,effectdate )<='" + ddmonth.SelectedValue + "' and DATEPART(YYYY,effectdate)<='" + year + "'");
                            if (dsdays.Tables[0].Rows.Count > 0)
                            {
                                for (int p = 0; p < dsdays.Tables[0].Rows.Count; p++)
                                {
                                    string checkdays = dsdays.Tables[0].Rows[p]["TrackHolidays"].ToString();
                                  

                                    #region 1 and 2nd
                                    if (checkdays == "1 & 2")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,2) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            //CountS++;
                                            //gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                            //countweakOf++;
                                        }
                                    }
                                    #endregion

                                    #region 3 and 4th
                                    if (checkdays == "3 & 4")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (3,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;

                                            gv.Rows[j].Cells[days].Width = 10;
                                            //countweakOf++;
                                            //CountS++;
                                            //gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                        }
                                    }
                                    #endregion

                                    #region 1 and 3th
                                    if (checkdays == "1 & 3")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            //countweakOf++;
                                            //CountS++;
                                            //gv.Rows[j].Cells[k + 1].Text = CountS.ToString();
                                        }
                                    }
                                    #endregion

                                    #region 2 and 4th
                                    if (checkdays == "2 & 4")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            //countweakOf++;
                                            //CountS++;
                                           // gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                        }
                                    }
                                    #endregion

                                    #region 2 and 3th
                                    if (checkdays == "2 & 3")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            //countweakOf++;
                                            //CountS++;
                                            //gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                        }
                                    }
                                    #endregion

                                    #region 1 and 4th
                                    if (checkdays == "1 & 4")
                                    {

                                        DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                        for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                        {
                                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                            int days = 0;
                                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                            gv.Rows[j].Cells[days].Text = "WO";
                                            gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                            gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                            gv.Rows[j].Cells[days].Width = 10;
                                            //countweakOf++;
                                            //CountS++;
                                            //gv.Rows[j].Cells[k + 2].Text = CountS.ToString();

                                        }
                                    }
                                    #endregion
                                }
                            }
                            #region ALL SUNDAYS
                            //if (checkdays == "All")
                            //{
                                // sun++;
                                DataSet dsSun = g.ReturnData1(@"DECLARE @date datetime
                                 SELECT @date = '" + date + "' SELECT [1st_sunday], DATENAME(weekday, [1st_sunday]) Daynames,[sunday] = DATEADD(DAY, n * 7, [1st_sunday]) FROM (SELECT [1st_sunday] = [1st_month] + 8 - DATEPART(weekday, [1st_month]) FROM (SELECT [1st_month] = DATEADD(MONTH, DATEDIFF(MONTH, 0, @date), 0)) d ) d CROSS JOIN (SELECT n = 0 UNION ALL SELECT n = 1 UNION ALL SELECT n = 2 UNION ALL SELECT n = 3 UNION ALL SELECT n = 4 ) n WHERE DATEDIFF(MONTH, @date, DATEADD(DAY, n * 7, [1st_sunday])) = 0");
                                for (int h = 0; h < dsSun.Tables[0].Rows.Count; h++)
                                {
                                    DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSun.Tables[0].Rows[h]["sunday"] + "')");
                                    int days = 0;
                                    days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                    gv.Rows[j].Cells[days].Text = "S";
                                    gv.Rows[j].Cells[days].ToolTip = "Sunday";
                                    gv.Rows[j].Cells[days].BackColor = Color.Orange;
                                    gv.Rows[j].Cells[days].Width = 10;
                                    CountS++;
                                    //gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                               // }
                            }
                            #endregion
                            #endregion

                            if (gv.Rows[j].Cells[kk].Text != "S"
                            && gv.Rows[j].Cells[kk].Text != "WO"
                            && gv.Rows[j].Cells[kk].Text != "P"
                            && gv.Rows[j].Cells[kk].Text != "MP"
                            && gv.Rows[j].Cells[kk].Text != "MH"
                            && gv.Rows[j].Cells[kk].Text != "MA"
                            && gv.Rows[j].Cells[kk].Text != "L"
                            && dsHolidays.Tables[0].Rows.Count == 0
                            && dsP.Tables[0].Rows.Count == 0
                            && dsMP.Tables[0].Rows.Count == 0 && dsMH.Tables[0].Rows.Count == 0)
                            {//ABSENT
                                // gv.Rows[j].Cells[kk].ForeColor = Color.White;
                                // gv.Rows[j].Cells[kk].Width = 10;
                                //gv.Rows[j].Cells[kk].Font.Size=10;
                                // gv.Rows[j].Cells[kk].Font.Bold = true;
                                gv.Rows[j].Cells[kk].Text = "A";
                                gv.Rows[j].Cells[kk].BackColor = Color.Red;
                                // int TotalP = countp - countweakOf;

                                //gv.Rows[j].Cells[k + 1].Text = countp.ToString();
                                //gv.Rows[j].Cells[k + 2].Text = CountS.ToString();
                                //gv.Rows[j].Cells[k + 3].Text = countL.ToString();

                                //gv.Rows[j].Cells[k + 1].BackColor = Color.GreenYellow;
                                //gv.Rows[j].Cells[k + 2].BackColor = Color.LightCyan;
                                //gv.Rows[j].Cells[k + 3].BackColor = Color.Yellow;
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                g.ShowMessage(this.Page, ex.Message);
            }
            #endregion
        }
        #endregion
        DispLAYCOUNT();
    }

    private void DispLAYCOUNT()
    {

         int k = System.DateTime.DaysInMonth(int.Parse(DateTime.Now.Year.ToString()), (ddmonth.SelectedIndex));

         for (int i = 0; i < gv.Rows.Count; i++)
         {
             int cntp = 0;
             int cnts = 0;
             int cntL = 0;
             for (int J = 1; J < gv.Rows[i].Cells.Count - 3; J++)
             {
                 if (gv.Rows[i].Cells[J].Text == "P" || gv.Rows[i].Cells[J].Text == "MP")
                 {
                     cntp++;
                    
                 }

                 if (gv.Rows[i].Cells[J].Text == "S")
                 {
                     cnts++;
                    // gv.Rows[i].Cells[k + 2].Text = cnts.ToString();
                 }

                 if (gv.Rows[i].Cells[J].Text == "WO")
                 {
                     cnts++;
                    
                 }

                 if (gv.Rows[i].Cells[J].Text == "L")
                 {
                     cntL++;
                    
                 }

                 gv.Rows[i].Cells[k + 1].Text = cntp.ToString();
                 gv.Rows[i].Cells[k + 2].Text = cnts.ToString();
                 gv.Rows[i].Cells[k + 3].Text = cntL.ToString();

                 gv.Rows[i].Cells[k + 1].BackColor = Color.GreenYellow;
                 gv.Rows[i].Cells[k + 2].BackColor = Color.LightCyan;
                 gv.Rows[i].Cells[k + 3].BackColor = Color.Yellow;

             }
         }

        //int cntp = 0;
        // int k = System.DateTime.DaysInMonth(int.Parse(DateTime.Now.Year.ToString()), (ddmonth.SelectedIndex));
          
        //for (int i = 0; i < gv.Rows.Count; i++)
        //{
        //    for (int J = 1; J < gv.Rows[i].Cells.Count -3; J++)
        //    {
        //        if (gv.Rows[i].Cells[J].Text == "P")
        //        {
        //            cntp++;
        //            gv.Rows[i].Cells[k + 1].Text = cntp.ToString();
        //        }
        //    }
            
        //}
      //  g.ShowMessage(this.Page, cntp.ToString());
    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        if (ddlCompany.SelectedIndex > 0)
        {
           
                var dt = (from k in ex.EmployeeTBs
                         where k.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
                         && k.RelivingStatus == null
                         select new { Name = k.FName + ' ' + k.MName + ' ' + k.Lname, k.EmployeeId }).OrderBy(s=> s.Name);
                //  select new { Name = k.FName + ' ' + k.Lname, k.EmployeeId };

                //if (dt.Count() > 0)
                //{
                    ddEmp.DataSource = dt;
                    ddEmp.DataTextField = "Name";
                    ddEmp.DataValueField = "EmployeeId";
                    ddEmp.DataBind();
                    ddEmp.Items.Insert(0, "ALL");

                //}
                //else
                //{
                //    ddEmp.Items.Clear();
                //    ddEmp.DataSource = null;
                //    ddEmp.DataBind();
                //    // ddEmp.Items.Insert(0, "All");
            }
        else
        {
            var dt = (from k in ex.EmployeeTBs
                      where k.RelivingStatus == null
                      select new { Name = k.FName + ' ' + k.MName + ' ' + k.Lname, k.EmployeeId }).OrderBy(s => s.Name);
            
            //if (dt.Count() > 0)
            //{
            ddEmp.DataSource = dt;
            ddEmp.DataTextField = "Name";
            ddEmp.DataValueField = "EmployeeId";
            ddEmp.DataBind();
            ddEmp.Items.Insert(0, "ALL");
        }

            }
            catch (Exception)
            {
                throw;
            }
       

    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void ddEmp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
}