using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;

public partial class LateCommerceReport : System.Web.UI.Page
{
    #region Declaration
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    string sqlquery;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnexport);


            scriptManager.RegisterPostBackControl(this.btnExpPDF);
            scriptManager.RegisterPostBackControl(this.BtnPrint1);



            if (!IsPostBack)
            {
                txtfromdate.Text = (g.GetCurrentDateTime().ToString("MM/dd/yyyy"));

                txttodate.Text = (g.GetCurrentDateTime().ToString("MM/dd/yyyy"));

                BindCompnay(); BindAllData();

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
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
    private void BindAllData()
    {
        sqlquery = "select COUNT(*) [LateMark Count],EmployeeTB.EmployeeId,EmployeeTB.FName+' '+EmployeeTB.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB on LogRecordsDetails.Employee_id = EmployeeTB.EmployeeId where CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  >= '" + txtfromdate.Text + "' and CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  <=  '" + txttodate.Text + "'  and cast(LogRecordsDetails.Log_Date_Time as time)>cast(case when (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') IS null then '9:36' else (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') end   as time)   AND  EmployeeTB.FName+' '+EmployeeTB.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and datepart(dw,Log_Date_Time) not in (1) and EmployeeTB.RelivingStatus is  null group by EmployeeTB.EmployeeId,EmployeeTB.FName,EmployeeTB.Lname having MAX(RIGHT(CONVERT(decimal,LogRecordsDetails.Log_Date_Time, 22), 11))> CONVERT(decimal,9.36,101)  order by COUNT(*) desc";

        if (ddCompnay.SelectedIndex > 0)
        {
            sqlquery = " select COUNT(*) [LateMark Count],EmployeeTB.EmployeeId,EmployeeTB.FName+' '+EmployeeTB.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB on LogRecordsDetails.Employee_id = EmployeeTB.EmployeeId where CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  >= '" + txtfromdate.Text + "' and CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  <=  '" + txttodate.Text + "'  and cast(LogRecordsDetails.Log_Date_Time as time)>cast(case when (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') IS null then '9:36' else (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') end   as time)  and CompanyId='" + ddCompnay.SelectedValue.ToString() + "'  AND  EmployeeTB.FName+' '+EmployeeTB.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and EmployeeTB.RelivingStatus is  null  and datepart(dw,Log_Date_Time) not in (1) group by EmployeeTB.EmployeeId,EmployeeTB.FName,EmployeeTB.Lname having MAX(RIGHT(CONVERT(decimal,LogRecordsDetails.Log_Date_Time, 22), 11))> CONVERT(decimal,9.36,101)  order by COUNT(*) desc";

            if (dddep.SelectedIndex > 0)
            {
                sqlquery = " select COUNT(*) [LateMark Count],EmployeeTB.EmployeeId,EmployeeTB.FName+' '+EmployeeTB.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB on LogRecordsDetails.Employee_id = EmployeeTB.EmployeeId where CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  >= '" + txtfromdate.Text + "' and CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  <=  '" + txttodate.Text + "'  and cast(LogRecordsDetails.Log_Date_Time as time)>cast(case when (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') IS null then '9:36' else (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') end   as time)  and CompanyId='" + ddCompnay.SelectedValue.ToString() + "'  AND  DeptId='" + dddep.SelectedValue.ToString() + "' and  EmployeeTB.FName+' '+EmployeeTB.Lname is not null  and EmployeeTB.RelivingStatus is  null and  cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and datepart(dw,Log_Date_Time) not in (1)  group by EmployeeTB.EmployeeId, EmployeeTB.FName,EmployeeTB.Lname having MAX(RIGHT(CONVERT(decimal,LogRecordsDetails.Log_Date_Time, 22), 11))> CONVERT(decimal,9.36,101)  order by COUNT(*) desc";

                if (ddemp.SelectedIndex > 0)
                {
                    sqlquery = " select COUNT(*) [LateMark Count],EmployeeTB.EmployeeId,EmployeeTB.FName+' '+EmployeeTB.Lname as EmpName,MAX(RIGHT(CONVERT(CHAR(20),LogRecordsDetails.Log_Date_Time, 22), 11)) Intime from LogRecordsDetails  Left outer join EmployeeTB on LogRecordsDetails.Employee_id = EmployeeTB.EmployeeId where CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  >= '" + txtfromdate.Text + "' and CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  <=  '" + txttodate.Text + "'  and cast(LogRecordsDetails.Log_Date_Time as time)>cast(case when (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') IS null then '9:36' else (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') end   as time) and CompanyId='" + ddCompnay.SelectedValue.ToString() + "'  AND  DeptId='" + dddep.SelectedValue.ToString() + "' and  Employee_id='" + ddemp.SelectedValue.ToString() + "'  and EmployeeTB.RelivingStatus is  null and   EmployeeTB.FName+' '+EmployeeTB.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and datepart(dw,Log_Date_Time) not in (1) group by EmployeeTB.EmployeeId, EmployeeTB.FName,EmployeeTB.Lname having MAX(RIGHT(CONVERT(decimal,LogRecordsDetails.Log_Date_Time, 22), 11))> CONVERT(decimal,9.36,101)  order by COUNT(*) desc";
                }
            }
        }

        DataTable dsalladata = g.ReturnData(sqlquery);
        grd_LateCommerce.DataSource = dsalladata;
        grd_LateCommerce.DataBind();

        lblcount.Text = dsalladata.Rows.Count.ToString();
    }

    private void BindPerticularEmpData()
    {
        sqlquery = "select   EmployeeTB.EmployeeId,EmployeeTB.FName+' '+EmployeeTB.Lname as EmpName,cast(LogRecordsDetails.Log_Date_Time as date) date1, cast(LogRecordsDetails.Log_Date_Time as time)  Intime from LogRecordsDetails  Left outer join EmployeeTB on LogRecordsDetails.Employee_id = EmployeeTB.EmployeeId where CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  >=  Convert(varchar,'" + txtfromdate.Text + "',101) and  CONVERT(varchar,LogRecordsDetails.Log_Date_Time,101)  <=  Convert(varchar,'" + txttodate.Text + "',101)  and  EmployeeTB.FName+' '+EmployeeTB.Lname is not null  and cast(LogRecordsDetails.Log_Date_Time as time)>cast(case when (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') IS null then '9:36' else (select substring (latetime,0,6)from EmployeeShiftTB where employeeid='" + ddemp.SelectedValue.ToString() + "') end   as time) AND cast(LogRecordsDetails.Log_Date_Time as time)<cast('14:00' as time) and EmployeeTB.EmployeeId='" + Convert.ToInt32(Session["EmpIDD"]) + "' and datepart(dw,Log_Date_Time) not in (1) group by EmployeeTB.EmployeeId,EmployeeTB.FName,EmployeeTB.Lname,LogRecordsDetails.Log_Date_Time ";

        DataTable dsalladata = g.ReturnData(sqlquery);
        Grd_LateComersEmp.DataSource = dsalladata;
        Grd_LateComersEmp.DataBind();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindAllData();
    }
    protected void ddCompnay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCompnay.SelectedIndex > 0)
        {
            BindDepartment();
            lbldept.Visible = true;
            dddep.Visible = true;
            lblemp.Visible = false;
            ddemp.Visible = false;
        }
        else
        {
            lbldept.Visible = false;
            dddep.Visible = false;
            ddemp.Visible = false;
            lblemp.Visible = false;
            ddemp.Visible = false;
        }
    }
    private void BindEmployee()
    {
        var data = (from dt in HR.EmployeeTBs
                    where dt.RelivingStatus == null && dt.DeptId == Convert.ToInt32(dddep.SelectedValue)
                    select new
                    {
                        dt.EmployeeId,
                        Name = dt.FName + ' ' + dt.Lname
                    }).OrderBy(dt => dt.Name);
        if (data != null && data.Count() > 0)
        {

            ddemp.DataSource = data;
            ddemp.DataTextField = "Name";
            ddemp.DataValueField = "EmployeeId";
            ddemp.DataBind();
            ddemp.Items.Insert(0, "All");
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
    protected void btnexport_Click(object sender, EventArgs e)
    {

        if (grd_LateCommerce.Rows.Count > 0)
        {
            grd_LateCommerce.Columns[4].Visible = false;
            Response.Clear();
            //Grd_LateComersEmp.Columns[4].Visible = false;
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";

            Page.EnableViewState = false;

            Response.AddHeader("Content-Disposition", "attachment;filename=Attendancereport.xls");

            StringWriter strwtr = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(strwtr);

            //turn off paging


            DataBind();
            BindAllData();


            pnl.RenderControl(hw);

            Response.Write(strwtr.ToString());
            grd_LateCommerce.Columns[4].Visible = true;
            Response.End();
            // Grd_LateComersEmp.Columns[4].Visible = true;
        }
        else
        {
            g.ShowMessage(this.Page, "No Data Exists..");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void imgbtnview_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgemp = (ImageButton)sender;
        int EmpId = Convert.ToInt32(imgemp.CommandArgument);
        Session["EmpIDD"] = EmpId;
        BindPerticularEmpData();
        ModelpopupLateCommerce.Show();

    }
    protected void Grd_LateComersEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grd_LateComersEmp.PageIndex = e.NewPageIndex;
        BindPerticularEmpData();
        ModelpopupLateCommerce.Show();
    }
    protected void btnExpPDF_Click(object sender, EventArgs e)
    {
        if (grd_LateCommerce.Rows.Count > 0)
        {
            Grd_LateComersEmp.PagerSettings.Visible = false;
            BindAllData();
            grd_LateCommerce.Columns[4].Visible = false;
            StringWriter sw = new StringWriter();
            pnl.Attributes.Add("style", "width: 1000px; height:50px;");
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            pnl.RenderControl(hw);
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
            Grd_LateComersEmp.PagerSettings.Visible = true;
            grd_LateCommerce.Columns[4].Visible = true;
        }
        else
        {
            g.ShowMessage(this.Page, "No Data Exists..");
        }
    }
    protected void BtnPrint1_Click(object sender, EventArgs e)
    {
        if (grd_LateCommerce.Rows.Count > 0)
        {
            ModelpopupLateCommerce.Show();
            Grd_LateComersEmp.PagerSettings.Visible = false;
            BindPerticularEmpData();

            StringWriter sw = new StringWriter();
            pnl.Attributes.Add("style", "width: 1000px; height:50px;");
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            Grd_LateComersEmp.RenderControl(hw);
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
            Grd_LateComersEmp.PagerSettings.Visible = true;
        }
        else
        {
            g.ShowMessage(this.Page, "No Data Exists..");
        }

    }
}