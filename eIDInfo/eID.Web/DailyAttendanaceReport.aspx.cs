using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;

public partial class DailyAttendanaceReport : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnexport);
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                DateTime todaydate = g.GetCurrentDateTime();
                DateTime dtStart = new DateTime(todaydate.Year, todaydate.Month, 1);
                DateTime dtEnd = new DateTime(todaydate.Year, todaydate.Month, DateTime.DaysInMonth(todaydate.Year, todaydate.Month));
                txtfromdate.Attributes.Add("readonly", "readonly");
                txttodate.Attributes.Add("readonly", "readonly");
                txtfromdate.Text = dtStart.ToShortDateString();
                txttodate.Text = dtEnd.ToShortDateString();
                BindCompnay();
                BindAllData();
                lbldept.Visible = false;
                dddep.Visible = false;
                lblemp.Visible = false;
                ddemp.Visible = false;
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    private void BindCompnay()
    {
        var data = (from dt in HR.CompanyInfoTBs
                   where dt.Status == 0
                   select dt).OrderBy(d=>d.CompanyName);
        if (data != null && data.Count() > 0)
        {

            ddCompnay.DataSource = data;
            ddCompnay.DataTextField = "CompanyName";
            ddCompnay.DataValueField = "CompanyId";
            ddCompnay.DataBind();
            ddCompnay.Items.Insert(0, "All");
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindAllData();
    }

    private void BindAllData()
    {
    //    string sqlquery = "SELECT  ET.FName+' '+ Lname Emp_Name,CONVERT(VARCHAR, LR.Log_Date_Time,101 ) Date,RIGHT(CONVERT(VARCHAR(26), LR.Log_Date_Time, 109), 14) logTime,case when cast(Log_Date_Time as time)>cast('9:36' as time) then 'lightgray' else 'white' end as color, LR.Employee_id,ET.EmployeeId,DE.DesigName FROM LogRecordsDetails LR LEFT OUTER JOIN EmployeeTB  ET ON LR.Employee_id=ET.EmployeeId LEFT OUTER JOIN MasterDesgTB  DE ON DE.DesigID=ET.DesgId where ET.FName is not null and CONVERT(VARCHAR, LR.Log_Date_Time,101 ) between CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101) ";
        //string sqlquery = "SELECT Employee_id,empname, case when CONVERT(varchar,Log_Date_Time, 101)>cast('9:36' as varchar) then 'red' else 'white' end as color,DesigName,CompanyId,DeptId,convert(varchar,Log_Date_Time,101) date, MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) Intime,case when  MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11))= MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) then '--' else MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) end outtime  FROM     (         select Employee_id,Log_Date_Time,ET.FName+ ' '+et.Lname empname,DesigName ,ET.CompanyId,ET.DeptId         from    LogRecordsDetails a           LEFT OUTER JOIN EmployeeTB  ET ON a.Employee_id=ET.EmployeeId LEFT OUTER JOIN MasterDesgTB        DE ON DE.DesigID=ET.DesgId         where          (             select count(*)              from LogRecordsDetails as f where f.Log_id = a.Log_id and f.Log_Date_Time <= a.Log_Date_Time         ) <= 2     ) s     where CONVERT(VARCHAR, Log_Date_Time,101 ) between    CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101)   ";

       // string sqlquery = "select *,case when cast(Intime as time)>cast('9:36' as time) then 'red' else 'white' end as color from (SELECT Employee_id,empname, DesigName,CompanyId,DeptId,convert(varchar,Log_Date_Time,101) date, MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) Intime,case when  MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11))= MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) then '--' else MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) end outtime  FROM     (         select Employee_id,Log_Date_Time,ET.FName+ ' '+et.Lname empname,DesigName ,ET.CompanyId,ET.DeptId         from    LogRecordsDetails a           LEFT OUTER JOIN EmployeeTB  ET ON a.Employee_id=ET.EmployeeId LEFT OUTER JOIN MasterDesgTB        DE ON DE.DesigID=ET.DesgId         where          (             select count(*)              from LogRecordsDetails as f where f.Log_id = a.Log_id and f.Log_Date_Time <= a.Log_Date_Time         ) <= 2     ) s     where CONVERT(VARCHAR, Log_Date_Time,101 ) between    CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101)   GROUP BY empname,DesigName,convert(varchar,Log_Date_Time,101),Employee_id,CompanyId,DeptId) as a";



        string sqlquery = "select *,case when cast(Intime as time)>cast(case when (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') IS null then '9:36' else (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') end   as time)  then 'lightgray' else 'white' end as color from (SELECT Employee_id,empname, DesigName,CompanyId,DeptId,convert(varchar,Log_Date_Time,101) date,CONVERT(varchar(15),MIN(cast(Log_Date_Time as time)),100) Intime,case when  CONVERT(varchar(15),MAX(cast(Log_Date_Time as time)),100)= CONVERT(varchar(15),MIN(cast(Log_Date_Time as time)),100) then '--' else CONVERT(varchar(15),MAX(cast(Log_Date_Time as time)),100) end outtime  FROM     (         select Employee_id,Log_Date_Time,ET.FName+ ' '+et.Lname empname,DesigName ,ET.CompanyId,ET.DeptId from    LogRecordsDetails a           LEFT OUTER JOIN EmployeeTB  ET ON a.Employee_id=ET.EmployeeId   LEFT OUTER JOIN MasterDesgTB        DE ON DE.DesigID=ET.DesgId  where          (             select count(*)              from LogRecordsDetails as f where f.Log_id = a.Log_id and f.Log_Date_Time <= a.Log_Date_Time         ) <= 2     ) s  where CONVERT(VARCHAR, Log_Date_Time,101 ) between    CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101) GROUP BY empname,DesigName,convert(varchar,Log_Date_Time,101),Employee_id,CompanyId,DeptId) as a";

        if (ddCompnay.SelectedIndex > 0)
        {
            //sqlquery = sqlquery + " and CompanyId='" + ddCompnay.SelectedValue.ToString() + "'";

       //     sqlquery = "select *,case when cast(Intime as time)>cast('9:36' as time) then 'red' else 'white' end as color from (SELECT Employee_id,empname, DesigName,CompanyId,DeptId,convert(varchar,Log_Date_Time,101) date, MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) Intime,case when  MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11))= MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) then '--' else MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) end outtime  FROM     (         select Employee_id,Log_Date_Time,ET.FName+ ' '+et.Lname empname,DesigName ,ET.CompanyId,ET.DeptId         from    LogRecordsDetails a           LEFT OUTER JOIN EmployeeTB  ET ON a.Employee_id=ET.EmployeeId LEFT OUTER JOIN MasterDesgTB        DE ON DE.DesigID=ET.DesgId         where          (             select count(*)              from LogRecordsDetails as f where f.Log_id = a.Log_id and f.Log_Date_Time <= a.Log_Date_Time         ) <= 2     ) s     where CONVERT(VARCHAR, Log_Date_Time,101 ) between    CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101)     and  CompanyId='" + ddCompnay.SelectedValue.ToString() + "' GROUP BY empname,DesigName,convert(varchar,Log_Date_Time,101),Employee_id,CompanyId,DeptId) as a";

            sqlquery = "select *,case when cast(Intime as time)>cast(case when (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') IS null then '9:36' else (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') end   as time)  then 'lightgray' else 'white' end as color from (SELECT Employee_id,empname, DesigName,CompanyId,DeptId,convert(varchar,Log_Date_Time,101) date,CONVERT(varchar(15),MIN(cast(Log_Date_Time as time)),100) Intime,case when  CONVERT(varchar(15),MAX(cast(Log_Date_Time as time)),100)= CONVERT(varchar(15),MIN(cast(Log_Date_Time as time)),100) then '--' else CONVERT(varchar(15),MAX(cast(Log_Date_Time as time)),100) end outtime  FROM     (         select Employee_id,Log_Date_Time,ET.FName+ ' '+et.Lname empname,DesigName ,ET.CompanyId,ET.DeptId from    LogRecordsDetails a           LEFT OUTER JOIN EmployeeTB  ET ON a.Employee_id=ET.EmployeeId   LEFT OUTER JOIN MasterDesgTB        DE ON DE.DesigID=ET.DesgId  where          (             select count(*)              from LogRecordsDetails as f where f.Log_id = a.Log_id and f.Log_Date_Time <= a.Log_Date_Time         ) <= 2     ) s  where CONVERT(VARCHAR, Log_Date_Time,101 ) between    CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101) and  CompanyId='" + ddCompnay.SelectedValue.ToString() + "' GROUP BY empname,DesigName,convert(varchar,Log_Date_Time,101),Employee_id,CompanyId,DeptId) as a";

            if (dddep.SelectedIndex > 0)
            {
                //sqlquery = "select *,case when cast(Intime as time)>cast('9:36' as time) then 'red' else 'white' end as color from (SELECT Employee_id,empname, DesigName,CompanyId,DeptId,convert(varchar,Log_Date_Time,101) date, MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) Intime,case when  MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11))= MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) then '--' else MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) end outtime  FROM     (         select Employee_id,Log_Date_Time,ET.FName+ ' '+et.Lname empname,DesigName ,ET.CompanyId,ET.DeptId         from    LogRecordsDetails a           LEFT OUTER JOIN EmployeeTB  ET ON a.Employee_id=ET.EmployeeId LEFT OUTER JOIN MasterDesgTB        DE ON DE.DesigID=ET.DesgId         where          (             select count(*)              from LogRecordsDetails as f where f.Log_id = a.Log_id and f.Log_Date_Time <= a.Log_Date_Time         ) <= 2     ) s     where CONVERT(VARCHAR, Log_Date_Time,101 ) between    CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101)     and  CompanyId='" + ddCompnay.SelectedValue.ToString() + "' and  DeptId='" + dddep.SelectedValue.ToString() + "' GROUP BY empname,DesigName,convert(varchar,Log_Date_Time,101),Employee_id,CompanyId,DeptId) as a";

                sqlquery = "select *,case when cast(Intime as time)>cast(case when (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') IS null then '9:36' else (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') end   as time)  then 'lightgray' else 'white' end as color from (SELECT Employee_id,empname, DesigName,CompanyId,DeptId,convert(varchar,Log_Date_Time,101) date,CONVERT(varchar(15),MIN(cast(Log_Date_Time as time)),100) Intime,case when  CONVERT(varchar(15),MAX(cast(Log_Date_Time as time)),100)= CONVERT(varchar(15),MIN(cast(Log_Date_Time as time)),100) then '--' else CONVERT(varchar(15),MAX(cast(Log_Date_Time as time)),100) end outtime  FROM     (         select Employee_id,Log_Date_Time,ET.FName+ ' '+et.Lname empname,DesigName ,ET.CompanyId,ET.DeptId from    LogRecordsDetails a           LEFT OUTER JOIN EmployeeTB  ET ON a.Employee_id=ET.EmployeeId   LEFT OUTER JOIN MasterDesgTB        DE ON DE.DesigID=ET.DesgId  where          (             select count(*)              from LogRecordsDetails as f where f.Log_id = a.Log_id and f.Log_Date_Time <= a.Log_Date_Time         ) <= 2     ) s  where CONVERT(VARCHAR, Log_Date_Time,101 ) between    CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101) and  CompanyId='" + ddCompnay.SelectedValue.ToString() + "' and  DeptId='" + dddep.SelectedValue.ToString() + "' GROUP BY empname,DesigName,convert(varchar,Log_Date_Time,101),Employee_id,CompanyId,DeptId) as a";


                if (ddemp.SelectedIndex > 0)
                {
                   // sqlquery = "select *,case when cast(Intime as time)>cast('9:36' as time) then 'red' else 'white' end as color from (SELECT Employee_id,empname, DesigName,CompanyId,DeptId,convert(varchar,Log_Date_Time,101) date, MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) Intime,case when  MAX(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11))= MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) then '--' else MIN(RIGHT(CONVERT(CHAR(20), Log_Date_Time, 22), 11)) end outtime  FROM     (         select Employee_id,Log_Date_Time,ET.FName+ ' '+et.Lname empname,DesigName ,ET.CompanyId,ET.DeptId         from    LogRecordsDetails a           LEFT OUTER JOIN EmployeeTB  ET ON a.Employee_id=ET.EmployeeId LEFT OUTER JOIN MasterDesgTB        DE ON DE.DesigID=ET.DesgId         where          (             select count(*)              from LogRecordsDetails as f where f.Log_id = a.Log_id and f.Log_Date_Time <= a.Log_Date_Time         ) <= 2     ) s     where CONVERT(VARCHAR, Log_Date_Time,101 ) between    CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101)     and  CompanyId='" + ddCompnay.SelectedValue.ToString() + "' and  DeptId='" + dddep.SelectedValue.ToString() + "' and  Employee_id='" + ddemp.SelectedValue.ToString() + "' GROUP BY empname,DesigName,convert(varchar,Log_Date_Time,101),Employee_id,CompanyId,DeptId) as a";
                    sqlquery = "select *,case when cast(Intime as time)>cast(case when (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') IS null then '9:36' else (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') end   as time)  then 'lightgray' else 'white' end as color from (SELECT Employee_id,empname, DesigName,CompanyId,DeptId,convert(varchar,Log_Date_Time,101) date,CONVERT(varchar(15),MIN(cast(Log_Date_Time as time)),100) Intime,case when  CONVERT(varchar(15),MAX(cast(Log_Date_Time as time)),100)= CONVERT(varchar(15),MIN(cast(Log_Date_Time as time)),100) then '--' else CONVERT(varchar(15),MAX(cast(Log_Date_Time as time)),100) end outtime  FROM     (         select Employee_id,Log_Date_Time,ET.FName+ ' '+et.Lname empname,DesigName ,ET.CompanyId,ET.DeptId from    LogRecordsDetails a           LEFT OUTER JOIN EmployeeTB  ET ON a.Employee_id=ET.EmployeeId   LEFT OUTER JOIN MasterDesgTB        DE ON DE.DesigID=ET.DesgId  where          (             select count(*)              from LogRecordsDetails as f where f.Log_id = a.Log_id and f.Log_Date_Time <= a.Log_Date_Time         ) <= 2     ) s  where CONVERT(VARCHAR, Log_Date_Time,101 ) between    CONVERT(date,'" + txtfromdate.Text + "',101) and CONVERT(date,'" + txttodate.Text + "',101) and  CompanyId='" + ddCompnay.SelectedValue.ToString() + "' and  DeptId='" + dddep.SelectedValue.ToString() + "' and  Employee_id='" + ddemp.SelectedValue.ToString() + "' GROUP BY empname,DesigName,convert(varchar,Log_Date_Time,101),Employee_id,CompanyId,DeptId) as a";

                }
            }
        }

       // sqlquery = sqlquery + " GROUP BY empname,DesigName,convert(varchar,Log_Date_Time,101),Employee_id,CompanyId,DeptId";
        DataTable dsalladata = g.ReturnData(sqlquery);
        grddata.DataSource = dsalladata;
        grddata.DataBind();

        lblcount.Text = dsalladata.Rows.Count.ToString();




        string sqlqueryabscent = "   SELECT FName+' '+Lname 'Name', DesigName,'Absent'  Absent from EmployeeTB EC   LEFT OUTER JOIN MasterDesgTB DE ON DE.DesigID=EC.DesgId  where   EC.EmployeeId  NOT IN   (select l.Employee_id from LogRecordsDetails l where  CONVERT(date, l.Log_Date_Time,101 ) >= CONVERT(date,'" + txtfromdate.Text + "',101) and  CONVERT(date, l.Log_Date_Time,101 ) <= CONVERT(date,'" + txttodate.Text + "',101)  and l.Employee_id IS NOT NULL )   and  MachineID!=0 and EC.RelivingStatus is  null";

      //  string sqlqueryabscent = "   SELECT FName+' '+Lname 'Name', DesigName,'Absent'  Absent from EmployeeTB EC    LEFT OUTER JOIN MasterDesgTB DE ON DE.DesigID=EC.DesgId  where   EC.EmployeeId  NOT IN   (select l.Employee_id from LogRecordsDetails l where  CONVERT(date, l.Log_Date_Time,101 ) =CONVERT(date,'" + txttodate.Text + "',101) and l.Employee_id IS NOT NULL ) and  MachineID!=0 ";
        if (ddCompnay.SelectedIndex > 0)
        {
            sqlqueryabscent = sqlqueryabscent + " and EC.CompanyId='" + ddCompnay.SelectedValue.ToString() + "'";

            if (dddep.SelectedIndex > 0)
            {
                sqlqueryabscent = sqlqueryabscent + " and  EC.DeptId='" + dddep.SelectedValue.ToString() + "'";

                if (ddemp.SelectedIndex > 0)
                {
                    sqlqueryabscent = sqlqueryabscent + " and  EC.Employee_id='" + ddemp.SelectedValue.ToString() + "'";
                }
            }
        }

      
        DataTable dsalladataabscent = g.ReturnData(sqlqueryabscent);
        grdabscent.DataSource = dsalladataabscent;
        grdabscent.DataBind();

        lblabscentdata.Text = dsalladataabscent.Rows.Count.ToString();
    }
    protected void ddCompnay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCompnay.SelectedIndex > 0)
        {
            BindDepartment();
            lbldept.Visible = true;
            dddep.Visible = true;
            ddemp.Visible = false;
        }
        else
        {
            lbldept.Visible = false;
            lblemp.Visible = false;
            dddep.Visible = false;
            ddemp.Visible = false;
        }
    }

    private void BindDepartment()
    {
        var data = (from dt in HR.MasterDeptTBs
                   where dt.Status == 0 && dt.CompanyId == Convert.ToInt32(ddCompnay.SelectedValue)
                   select dt).OrderBy(d=>d.DeptName);
        if (data != null && data.Count() > 0)
        {

            dddep.DataSource = data;
            dddep.DataTextField = "DeptName";
            dddep.DataValueField = "DeptID";
            dddep.DataBind();
            dddep.Items.Insert(0, "All");
        }
        else
        {
            dddep.DataSource = null;
            dddep.DataBind();
            dddep.Items.Clear();
        }
    }
    protected void dddep_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dddep.SelectedIndex > 0)
        {
            BindEmployee();
            lblemp.Visible = true;
            ddemp.Visible = true;
        }
        else
        {
            lblemp.Visible = false;
            ddemp.Visible = false;
        }
    }

    private void BindEmployee()
    {
        var data = (from dt in HR.EmployeeTBs
                   where dt.RelivingStatus==null && dt.DeptId == Convert.ToInt32(dddep.SelectedValue)
                   select new
                   {
                       dt.EmployeeId,
                       Name= dt.FName + ' '+dt.Lname
                   }).OrderBy(d=>d.Name);
        if (data != null && data.Count() > 0)
        {

            ddemp.DataSource = data;
            ddemp.DataTextField = "Name";
            ddemp.DataValueField = "EmployeeId";
            ddemp.DataBind();
            ddemp.Items.Insert(0, "All");
        }
    }
    protected void btnexport_Click(object sender, EventArgs e)
    {
        if (grdabscent.Rows.Count > 0 || grddata.Rows.Count > 0)
        {
            Response.Clear();

            Response.ContentType = "application/ms-excel";

            Response.Charset = "";

            Page.EnableViewState = false;

            Response.AddHeader("Content-Disposition", "attachment;filename=Attendancereport.xls");

            StringWriter strwtr = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(strwtr);



            //turn off paging



            grddata.AllowPaging = false;
            grdabscent.AllowPaging = false;
            DataBind();
            BindAllData();


            grddata.RenderControl(hw);

            grdabscent.RenderControl(hw);

            Response.Write(strwtr.ToString());

            Response.End();
        }
        else
        {
            g.ShowMessage(this.Page, "No Data Exists...");
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void grddata_DataBound(object sender, EventArgs e)
    {
       
    }

    protected void grddata_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblColor");
            string s = lbl.Text;
            e.Row.BackColor = Color.FromName(s); 
            
        }
    }
}
