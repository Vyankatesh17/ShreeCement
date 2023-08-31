using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LateReport : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    string sqlquery;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnprint);
        if (!IsPostBack)
        {
          //  ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
            fillYear();

            if (Session["EmpName1"] != "" && Session["EmpName1"] != null)
            {
                txtemployee.Text= Session["EmpName1"].ToString();
            }
            if (Session["year"] != "" && Session["year"] != null)
            {
                ddlYear.SelectedValue = Session["year"].ToString();
            }
            if (Session["month"] != "" && Session["month"] != null)
            {
                ddlMonth.SelectedValue = Session["month"].ToString();
            }
            BindCompnay();
            dddep.Items.Insert(0, "All");
            if (Session["compid"] != "" && Session["compid"] != null)
            {
                ddCompnay.SelectedValue = Session["compid"].ToString();
                if (Session["depid"] != "" && Session["depid"] != null)
                {
                    BindDepartment();
                    dddep.SelectedValue = Session["depid"].ToString();
                }
            }
            BindGrid();
           
            Session["CompSelectedIndex"] = ddCompnay.SelectedIndex;
            Session["DeptSelectedIndex"] = dddep.SelectedIndex;
            Session["compid"] = "";
            Session["depid"] = "";
            Session["year"] = "";
            Session["month"] = "";
            Session["EmpName1"] = "";
        }

    }
    private void fillYear()
    {
        DataTable Dtyears = new DataTable();
        DataColumn years = new DataColumn("years");
        Dtyears.Columns.Add(years);

        int i = int.Parse(DateTime.Now.AddYears(-1).Date.Year.ToString());
        for (int j = 0; j <= 11; j++)
        {
            DataRow DR = Dtyears.NewRow();
            DR[0] = i.ToString();
            Dtyears.Rows.Add(DR);
            i++;

        }

        ddlYear.DataSource = Dtyears;
        ddlYear.DataTextField = "years";
        ddlYear.DataValueField = "years";
        ddlYear.DataBind();
        ddlYear.Items.FindByText(g.GetCurrentDateTime().Year.ToString()).Selected = true;
    }
    private void BindDepartment()
    {
        var data = (from dt in HR.MasterDeptTBs
                    where dt.Status == 0 && dt.CompanyId == Convert.ToInt32(ddCompnay.SelectedValue)
                    select dt).OrderBy(d => d.DeptName);
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
            dddep.Items.Insert(0, "All");
        }
    }
    private void BindCompnay()
    {
        var data = (from dt in HR.CompanyInfoTBs
                    where dt.Status == 0
                    select dt).OrderBy(dt => dt.CompanyName);
        if (data != null && data.Count() > 0)
        {

            ddCompnay.DataSource = data;
            ddCompnay.DataTextField = "CompanyName";
            ddCompnay.DataValueField = "CompanyId";
            ddCompnay.DataBind();
            ddCompnay.Items.Insert(0, "All");
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListEmployee(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> Employee = new List<string>();
        if (HttpContext.Current.Session["CompSelectedIndex"].ToString() == "0" && HttpContext.Current.Session["DeptSelectedIndex"].ToString() == "0")
        {
            var dt = (from d in hr.EmployeeTBs

                      where (d.FName + " " + d.Lname).Contains(prefixText) && d.RelivingStatus == null
                      select new { Name = d.FName + " " + d.Lname }).Distinct();

            if (dt.Count() > 0)
            {
                foreach (var item in dt)
                {
                    Employee.Add(item.Name);
                }
            }
        }
        if (HttpContext.Current.Session["CompSelectedIndex"].ToString() != "0" && HttpContext.Current.Session["DeptSelectedIndex"].ToString() == "0")
        {
            var dt = (from d in hr.EmployeeTBs

                      where (d.FName + " " + d.Lname).Contains(prefixText) && d.RelivingStatus == null && d.CompanyId == Convert.ToInt32(HttpContext.Current.Session["CompSelectedValue"].ToString())
                      select new { Name = d.FName + " " + d.Lname }).Distinct();

            if (dt.Count() > 0)
            {
                foreach (var item in dt)
                {
                    Employee.Add(item.Name);
                }
            }
        }
        if (HttpContext.Current.Session["CompSelectedIndex"].ToString() != "0" && HttpContext.Current.Session["DeptSelectedIndex"].ToString() != "0")
        {
            var dt = (from d in hr.EmployeeTBs

                      where (d.FName + " " + d.Lname).Contains(prefixText) && d.RelivingStatus == null && d.CompanyId == Convert.ToInt32(HttpContext.Current.Session["CompSelectedValue"].ToString()) && d.DeptId == Convert.ToInt32(HttpContext.Current.Session["DeptSelectedValue"].ToString())
                      select new { Name = d.FName + " " + d.Lname }).Distinct();

            if (dt.Count() > 0)
            {
                foreach (var item in dt)
                {
                    Employee.Add(item.Name);
                }
            }
        }

        return Employee;
    }
    protected void dddep_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dddep.SelectedIndex > 0)
        {
            Session["DeptSelectedValue"] = dddep.SelectedValue;
        }
        else
        {
            Session["DeptSelectedValue"] = "";
        }
        Session["DeptSelectedIndex"] = dddep.SelectedIndex;
        txtemployee.Text = "";
    }
    protected void ddCompnay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCompnay.SelectedIndex > 0)
        {
            BindDepartment();
            Session["CompSelectedValue"] = ddCompnay.SelectedValue;

        }
        else
        {
            dddep.Items.Clear();
            dddep.Items.Insert(0, "All");
            Session["CompSelectedValue"] = "";
        }
        Session["CompSelectedIndex"] = ddCompnay.SelectedIndex;
        txtemployee.Text = "";
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        try
        {
            if (ddlMonth.SelectedIndex > 0 && ddlYear.SelectedIndex > 0)
            {

                sqlquery = "select COUNT(*) [LateMarkCount],EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName,'EMP'+Convert(varchar,EM.EmployeeId) as EmployeeNo,EM.EmployeeId,EM.FName+' '+EM.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB EM on LogRecordsDetails.Employee_id = EM.EmployeeId left outer join CompanyInfoTB CM on EM.CompanyId=CM.CompanyId left outer join MasterDeptTB DT on EM.DeptId=DT.DeptID  where DATEPART(yy, LogRecordsDetails.Log_Date_Time)   = '" + ddlYear.SelectedItem.Text + "' and datename(MM, LogRecordsDetails.Log_Date_Time)  =  '" + ddlMonth.SelectedItem.Text + "' and (LogRecordsDetails.Status is null or LogRecordsDetails.Status='MP' or LogRecordsDetails.Status='P' or LogRecordsDetails.Status='0' or LogRecordsDetails.Status='MH')  and (Convert(time, LogRecordsDetails.Log_Date_Time) > (select top 1 substring (Latemark,0,6) from MasterShiftTB where CompanyId=CompanyId )  AND  EM.FName+' '+EM.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time))  and RelivingStatus is null group by EM.EmployeeId,EM.FName,EM.Lname,EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName";

                if (ddCompnay.SelectedIndex == 0 && dddep.SelectedIndex == 0 && txtemployee.Text == "")
                {
                    sqlquery = "select COUNT(*) [LateMarkCount],EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName,'EMP'+Convert(varchar,EM.EmployeeId) as EmployeeNo,EM.EmployeeId,EM.FName+' '+EM.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB EM on LogRecordsDetails.Employee_id = EM.EmployeeId left outer join CompanyInfoTB CM on EM.CompanyId=CM.CompanyId left outer join MasterDeptTB DT on EM.DeptId=DT.DeptID  where DATEPART(yy, LogRecordsDetails.Log_Date_Time)   = '" + ddlYear.SelectedItem.Text + "' and datename(MM, LogRecordsDetails.Log_Date_Time)  =  '" + ddlMonth.SelectedItem.Text + "' and (LogRecordsDetails.Status is null or LogRecordsDetails.Status='MP' or LogRecordsDetails.Status='P' or LogRecordsDetails.Status='0' or LogRecordsDetails.Status='MH')  and (Convert(time, LogRecordsDetails.Log_Date_Time) > (select top 1 substring (Latemark,0,6) from MasterShiftTB where CompanyId=CompanyId )  AND  EM.FName+' '+EM.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time)) and RelivingStatus is null group by EM.EmployeeId,EM.FName,EM.Lname,EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName";
                }
                if (ddCompnay.SelectedIndex != 0 && dddep.SelectedIndex == 0 && txtemployee.Text == "")
                {
                    sqlquery = "select COUNT(*) [LateMarkCount],EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName,'EMP'+Convert(varchar,EM.EmployeeId) as EmployeeNo,EM.EmployeeId,EM.FName+' '+EM.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB EM on LogRecordsDetails.Employee_id = EM.EmployeeId left outer join CompanyInfoTB CM on EM.CompanyId=CM.CompanyId left outer join MasterDeptTB DT on EM.DeptId=DT.DeptID  where DATEPART(yy, LogRecordsDetails.Log_Date_Time)   = '" + ddlYear.SelectedItem.Text + "' and datename(MM, LogRecordsDetails.Log_Date_Time)  =  '" + ddlMonth.SelectedItem.Text + "'  and (LogRecordsDetails.Status is null or LogRecordsDetails.Status='MP' or LogRecordsDetails.Status='P' or LogRecordsDetails.Status='0' or LogRecordsDetails.Status='MH')  and EM.CompanyId='" + Convert.ToInt32(ddCompnay.SelectedValue) + "' and (Convert(time, LogRecordsDetails.Log_Date_Time) > (select top 1 substring (Latemark,0,6) from MasterShiftTB where CompanyId=CompanyId )  AND  EM.FName+' '+EM.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and EM.RelivingStatus is  null) group by EM.EmployeeId,EM.FName,EM.Lname,EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName";
                }
                if (ddCompnay.SelectedIndex != 0 && dddep.SelectedIndex != 0 && txtemployee.Text == "")
                {
                    sqlquery = "select COUNT(*) [LateMarkCount],EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName,'EMP'+Convert(varchar,EM.EmployeeId) as EmployeeNo,EM.EmployeeId,EM.FName+' '+EM.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB EM on LogRecordsDetails.Employee_id = EM.EmployeeId left outer join CompanyInfoTB CM on EM.CompanyId=CM.CompanyId left outer join MasterDeptTB DT on EM.DeptId=DT.DeptID  where DATEPART(yy, LogRecordsDetails.Log_Date_Time)   = '" + ddlYear.SelectedItem.Text + "' and datename(MM, LogRecordsDetails.Log_Date_Time)  =  '" + ddlMonth.SelectedItem.Text + "'  and (LogRecordsDetails.Status is null or LogRecordsDetails.Status='MP' or LogRecordsDetails.Status='P' or LogRecordsDetails.Status='0' or LogRecordsDetails.Status='MH')  and EM.CompanyId='" + Convert.ToInt32(ddCompnay.SelectedValue) + "'  and EM.DeptId='" + Convert.ToInt32(dddep.SelectedValue) + "' and (Convert(time, LogRecordsDetails.Log_Date_Time) > (select top 1 substring (Latemark,0,6) from MasterShiftTB where CompanyId=CompanyId )  AND  EM.FName+' '+EM.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and EM.RelivingStatus is  null) group by EM.EmployeeId,EM.FName,EM.Lname,EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName";
                }
                if (ddCompnay.SelectedIndex == 0 && dddep.SelectedIndex == 0 && txtemployee.Text != "")
                {
                    sqlquery = "select COUNT(*) [LateMarkCount],EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName,'EMP'+Convert(varchar,EM.EmployeeId) as EmployeeNo,EM.EmployeeId,EM.FName+' '+EM.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB EM on LogRecordsDetails.Employee_id = EM.EmployeeId left outer join CompanyInfoTB CM on EM.CompanyId=CM.CompanyId left outer join MasterDeptTB DT on EM.DeptId=DT.DeptID  where DATEPART(yy, LogRecordsDetails.Log_Date_Time)   = '" + ddlYear.SelectedItem.Text + "' and datename(MM, LogRecordsDetails.Log_Date_Time)  =  '" + ddlMonth.SelectedItem.Text + "' and (LogRecordsDetails.Status is null or LogRecordsDetails.Status='MP' or LogRecordsDetails.Status='P' or LogRecordsDetails.Status='0' or LogRecordsDetails.Status='MH')  and EM.FName+' '+EM.Lname='" + txtemployee.Text + "' and (Convert(time, LogRecordsDetails.Log_Date_Time) > (select top 1 substring (Latemark,0,6) from MasterShiftTB where CompanyId=CompanyId )  AND  EM.FName+' '+EM.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and EM.RelivingStatus is  null) group by EM.EmployeeId,EM.FName,EM.Lname,EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName";
                }
                if (ddCompnay.SelectedIndex != 0 && dddep.SelectedIndex == 0 && txtemployee.Text != "")
                {
                    sqlquery = "select COUNT(*) [LateMarkCount],EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName,'EMP'+Convert(varchar,EM.EmployeeId) as EmployeeNo,EM.EmployeeId,EM.FName+' '+EM.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB EM on LogRecordsDetails.Employee_id = EM.EmployeeId left outer join CompanyInfoTB CM on EM.CompanyId=CM.CompanyId left outer join MasterDeptTB DT on EM.DeptId=DT.DeptID  where DATEPART(yy, LogRecordsDetails.Log_Date_Time)   = '" + ddlYear.SelectedItem.Text + "' and datename(MM, LogRecordsDetails.Log_Date_Time)  =  '" + ddlMonth.SelectedItem.Text + "'  and (LogRecordsDetails.Status is null or LogRecordsDetails.Status='MP' or LogRecordsDetails.Status='P' or LogRecordsDetails.Status='0' or LogRecordsDetails.Status='MH')  and EM.CompanyId='" + Convert.ToInt32(ddCompnay.SelectedValue) + "' and EM.FName+' '+EM.Lname='" + txtemployee.Text + "' and (Convert(time, LogRecordsDetails.Log_Date_Time) > (select top 1 substring (Latemark,0,6) from MasterShiftTB where CompanyId=CompanyId )  AND  EM.FName+' '+EM.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and EM.RelivingStatus is  null) group by EM.EmployeeId,EM.FName,EM.Lname,EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName";
                }
                if (ddCompnay.SelectedIndex != 0 && dddep.SelectedIndex != 0 && txtemployee.Text != "")
                {
                    sqlquery = "select COUNT(*) [LateMarkCount],EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName,'EMP'+Convert(varchar,EM.EmployeeId) as EmployeeNo,EM.EmployeeId,EM.FName+' '+EM.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB EM on LogRecordsDetails.Employee_id = EM.EmployeeId left outer join CompanyInfoTB CM on EM.CompanyId=CM.CompanyId left outer join MasterDeptTB DT on EM.DeptId=DT.DeptID  where DATEPART(yy, LogRecordsDetails.Log_Date_Time)   = '" + ddlYear.SelectedItem.Text + "' and datename(MM, LogRecordsDetails.Log_Date_Time)  =  '" + ddlMonth.SelectedItem.Text + "'  and (LogRecordsDetails.Status is null or LogRecordsDetails.Status='MP' or LogRecordsDetails.Status='P' or LogRecordsDetails.Status='0' or LogRecordsDetails.Status='MH')  and EM.CompanyId='" + Convert.ToInt32(ddCompnay.SelectedValue) + "' and EM.DeptId='" + Convert.ToInt32(dddep.SelectedValue) + "' and EM.FName+' '+EM.Lname='" + txtemployee.Text + "' and (Convert(time, LogRecordsDetails.Log_Date_Time) > (select top 1 substring (Latemark,0,6) from MasterShiftTB where CompanyId=CompanyId )  AND  EM.FName+' '+EM.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and EM.RelivingStatus is  null) group by EM.EmployeeId,EM.FName,EM.Lname,EM.CompanyId,CM.CompanyName,EM.DeptId,DT.DeptName";
                }
                DataTable dtalladata = g.ReturnData(sqlquery);
                dtalladata.Columns.Add("year");
                for (int i = 0; i < dtalladata.Rows.Count; i++)
                {
                    dtalladata.Rows[i][9] = ddlYear.SelectedItem.Text;
                }
                if (dtalladata.Rows.Count > 0)
                {
                    grd_LateCommerce.DataSource = dtalladata;
                    grd_LateCommerce.DataBind();
                }
                else
                {
                    grd_LateCommerce.DataSource = null;
                    grd_LateCommerce.DataBind();
                }

                lblcount.Text = dtalladata.Rows.Count.ToString();
            }
        }
        catch
        {

        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        if (grd_LateCommerce.Rows.Count > 0)
        {
            grd_LateCommerce.AllowPaging = false;
            BindGrid();
            grd_LateCommerce.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grd_LateCommerce.RenderControl(hw);
            //    gvnested.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            grd_LateCommerce.AllowPaging = true;
            grd_LateCommerce.DataBind();
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    //protected void lnkcount_Click(object sender, EventArgs e)
    //{
    //    Table1.Visible = true;
    //    LinkButton lnk = (LinkButton)sender;
    //    lblempid.Text = lnk.CommandArgument;
    //    //bindpopupgrid();
    //    //modnopo.Show();
    //}

    private void bindpopupgrid()
    {
      //  sqlquery = " Select distinct Convert(varchar,LRD.Log_Date_Time,101) as Log_Date_Time,CM.CompanyName,DT.DeptName,EM.FName+' '+EM.Lname as EmpName, MasterDesgTB.DesigName,(select substring (Intime,0,6) from MasterShiftTB where CompanyId=EM.CompanyId) as Intime,(select substring (Outtime,0,6) from MasterShiftTB where CompanyId=EM.CompanyId) as Outtime,( select top 1 MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11))  from LogRecordsDetails where  Employee_id='" + Convert.ToInt32(lblempid.Text) + "' and LogRecordsDetails.Log_Date_Time=LRD.Log_Date_Time) as ActIntime,( select top 1 MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11))  from LogRecordsDetails where  Employee_id='" + Convert.ToInt32(lblempid.Text) + "' and LogRecordsDetails.Log_Date_Time=LRD.Log_Date_Time group by Log_id order by Log_id desc) as ActOuttime from LogRecordsDetails LRD Left outer join EmployeeTB EM on LRD.Employee_id = EM.EmployeeId left outer join MasterCompanyTB CM on EM.CompanyId=CM.CompanyId left outer join MasterDeptTB DT on EM.DeptId=DT.DeptID left outer join MasterDesgTB on EM.DesgId=MasterDesgTB.DesigID where Employee_id='" + Convert.ToInt32(lblempid.Text) + "' and   DATEPART(yy, LRD.Log_Date_Time)   = '" + ddlYear.SelectedItem.Text + "' and datename(MM, LRD.Log_Date_Time)  ='" + ddlMonth.SelectedItem.Text + "' and cast(LRD.Log_Date_Time as time)<cast('14:00' as time)  and convert(time, ( select top 1 LogRecordsDetails.Log_Date_Time  from LogRecordsDetails where  Employee_id='" + Convert.ToInt32(lblempid.Text) + "' and LogRecordsDetails.Log_Date_Time=LRD.Log_Date_Time)) 	> convert(time,(select substring (Latemark,0,6) from MasterShiftTB where CompanyId=EM.CompanyId))";
        sqlquery = "Select X.Log_Date_Time,X.COMPANYNAME,X.DEPTNAME,X.EMPNAme,X.DesigName,X.INtime,x.Outtime,X.ActIntime,X.ActOuttime,CONVERT(varchar,(X.latemin),108)";
         sqlquery +="LateMins from (Select distinct Convert(varchar,LRD.Log_Date_Time,101) as Log_Date_Time,CM.CompanyName,DT.DeptName,EM.FName+' '+EM.Lname as EmpName,";
         sqlquery += "MasterDesgTB.DesigName,(select substring (Intime,0,6) from MasterShiftTB where CompanyId=EM.CompanyId) as Intime,(select substring (Outtime,0,6) ";
         sqlquery += "from MasterShiftTB where CompanyId=EM.CompanyId) as Outtime,( select top 1 MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) ";
         sqlquery += "from LogRecordsDetails where  Employee_id='" + Convert.ToInt32(lblempid.Text) + "' and (LogRecordsDetails.Status is null or LogRecordsDetails.Status='MP' ";
         sqlquery += "or LogRecordsDetails.Status='P' or LogRecordsDetails.Status='0') and LogRecordsDetails.Log_Date_Time=LRD.Log_Date_Time) as ActIntime,";
         sqlquery += "( select top 1 MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11))  from LogRecordsDetails ";
         sqlquery += "where  Employee_id='" + Convert.ToInt32(lblempid.Text) + "' and (day(LogRecordsDetails.Log_Date_Time)=day(LRD.Log_Date_Time)and ";
         sqlquery += "month(LogRecordsDetails.Log_Date_Time)=month(LRD.Log_Date_Time)and year(LogRecordsDetails.Log_Date_Time)=year(LRD.Log_Date_Time)) ";
         sqlquery += "group by Log_id order by Log_id desc) as ActOuttime,(Convert(datetime,( select top 1 MAX(RIGHT(CONVERT(CHAR(20),";
         sqlquery += "LogRecordsDetails.Log_Date_Time, 22), 11))  from LogRecordsDetails where  Employee_id='" + Convert.ToInt32(lblempid.Text) + "' and ";
         sqlquery += "LogRecordsDetails.Log_Date_Time=LRD.Log_Date_Time))-Convert(datetime,(select substring (Intime,0,6) from MasterShiftTB where CompanyId=EM.CompanyId)))  ";
         sqlquery += "as latemin from LogRecordsDetails LRD Left outer join EmployeeTB EM on LRD.Employee_id = EM.EmployeeId left outer join CompanyInfoTB CM on ";
         sqlquery += "EM.CompanyId=CM.CompanyId left outer join MasterDeptTB DT on EM.DeptId=DT.DeptID left outer join MasterDesgTB on EM.DesgId=MasterDesgTB.DesigID ";
         sqlquery += "where Employee_id='" + Convert.ToInt32(lblempid.Text) + "' and  DATEPART(yy, LRD.Log_Date_Time)   = '" + ddlYear.SelectedItem.Text + "' and ";
         sqlquery += "datename(MM, LRD.Log_Date_Time)  ='" + ddlMonth.SelectedItem.Text + "' and cast(LRD.Log_Date_Time as time)<cast('14:00' as time)  and ";
         sqlquery += "convert(time, ( select top 1 LogRecordsDetails.Log_Date_Time  from LogRecordsDetails where  Employee_id='" + Convert.ToInt32(lblempid.Text) + "' and ";
         sqlquery += "(LogRecordsDetails.Status is null or LogRecordsDetails.Status='MP' or LogRecordsDetails.Status='P' or LogRecordsDetails.Status='0' or ";
         sqlquery += "LogRecordsDetails.Status='MH') and LogRecordsDetails.Log_Date_Time=LRD.Log_Date_Time)) 	> convert(time,(select top 1 substring (Latemark,0,6) ";
         sqlquery += "from MasterShiftTB where CompanyId=EM.CompanyId)) ) as  X";


        DataTable dtalladata = g.ReturnData(sqlquery);
        if (dtalladata.Rows.Count > 0)
        {
            Grd_LateComersEmp.DataSource = dtalladata;
            Grd_LateComersEmp.DataBind();
        }
        else
        {
            Grd_LateComersEmp.DataSource = null;
            Grd_LateComersEmp.DataBind();
        }
    }
    //protected void lnkyear_Click(object sender, EventArgs e)
    //{
    //    LinkButton lnkyear = (LinkButton)sender;
    //    string styear = lnkyear.Text;
    //    string stempid = lnkyear.CommandArgument;
    //    Session["EmpName1"] = txtemployee.Text;
    //    Session["month"] = ddlMonth.SelectedValue;
    //    Session["year"] = ddlYear.SelectedValue;
    //    if (ddCompnay.SelectedIndex > 0)
    //    {
    //        Session["compid"] = ddCompnay.SelectedValue;
    //    }
    //    if (dddep.SelectedIndex>0)
    //    {
    //        Session["depid"] = dddep.SelectedValue;
    //    }
    //  //  Response.Redirect("LateReportDetails.aspx?id=" + stempid + "&year=" + styear);
    //}

    protected void grd_LateCommerce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_LateCommerce.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}