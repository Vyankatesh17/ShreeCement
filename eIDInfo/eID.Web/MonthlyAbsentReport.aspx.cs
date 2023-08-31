using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MonthlyAttendanceReport : System.Web.UI.Page
{
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
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
                BindCompanyList();
                BindDepartmentList();
                BindEmployeeList();
                BindDataGridView();

                if (Session["UserType"].ToString() != "User")
                {
                    company.Visible = true;
                    department.Visible = true;
                    employee.Visible = true;
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (gv.Rows.Count > 0)
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }
    
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindDataGridView();


    }

    private void BindDataGridView()
    {
        try
        {
            bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
            litMessage.Text = string.Format("<tr><td>Before Conversion Dates</td><td>From Date : {0} </td><td> To Date : {1}</td></tr>",txtFromDate.Text,txtToDate.Text);
            string query = string.Format(@"SELECT       e.FName+' '+e.Lname AS EmpName , E.EmployeeNo , E.MachineID ,DAY(AL.AttendanceDate) AS Day, CONVERT(varchar, AL.AttendanceDate, 101) AS Date, DATENAME(weekday, AL.AttendanceDate) AS DayofWeek, S.Shift AS ShiftName, AL.InTime AS PunchIn, AL.OutTime AS PunchOut, 
                         CONVERT(varchar(5), DATEADD(minute, DATEDIFF(MINUTE, S.Intime, S.Outtime), 0), 8) AS StanardWorkHours, CONVERT(varchar(5), DATEADD(minute, DATEDIFF(MINUTE, AL.InTime, AL.OutTime), 0), 8) AS ActualHours, 
                         S.Shifthours AS MinHours, AL.LateBy, AL.Status, AL.Remarks
FROM            AttendaceLogTB AS AL INNER JOIN
                         EmployeeTB AS E ON AL.EmployeeId = E.EmployeeId LEFT OUTER JOIN
                         MasterShiftTB AS S ON AL.ShiftId = S.ShiftID
WHERE     (E.IsActive=1) AND   (AL.Status IN ('A')) AND (CONVERT(date,AL.AttendanceDate) BETWEEN CONVERT(date,'{0}') AND CONVERT(date,'{1}'))  AND AL.TenantId='{2}'", txtFromDate.Text, txtToDate.Text,
Convert.ToString(Session["TenantId"]));
            int companyid = Convert.ToInt32(Session["CompanyID"]);
            if (ddlCompany.SelectedIndex > 0)
            {
                query += " AND (AL.CompanyId = '" + ddlCompany.SelectedValue + "')";
            }
            if (ddlDepartment.SelectedIndex > 0)
            {
                query += " AND (E.DeptID = '" + ddlDepartment.SelectedValue + "')";
            }
            if (ddlEmployee.SelectedIndex > 0)
            {
                query += " AND (E.EmployeeId = '" + ddlEmployee.SelectedValue + "')";
            }
            if (Session["UserType"].ToString() == "User")
            {
                query += " AND (E.EmployeeId = '" + Convert.ToInt32(Session["EmpId"]) + "')";
            }
            else if (Session["UserType"].ToString() == "LocationAdmin")
            {
                query += " AND (AL.CompanyId = '" + companyid + "')";
            }
            if (AdminStatus == false)
            {
                query += " AND E.EmployeeId=" + Convert.ToInt32(Session["EmpId"]);
            }





            litMessage.Text+= string.Format("<tr><td>Before Conversion Query</td><td colspan='2'>{0}</td></tr>", query);
            DateTime fromDate = string.IsNullOrEmpty(txtFromDate.Text) ? DateTime.Now : Genreal.GetDate(txtFromDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.Now : Genreal.GetDate(txtToDate.Text);
            litMessage.Text+= string.Format("<tr><td>After Conversion Dates</td><td>From Date : {0} </td><td> To Date : {1}</td></tr>", fromDate, toDate);
            //query += string.Format(" AND (CONVERT(date, AL.AttendanceDate) BETWEEN CONVERT(date, '{0}') AND CONVERT(date, '{1}'))", txtFromDate.Text, txtToDate.Text);
            litMessage.Text += string.Format("<tr><td>After Conversion Query</td><td colspan='2'>{0}</td></tr>", query);

            DataTable dataTable = g.ReturnData(query);
            gv.DataSource = dataTable;
            gv.DataBind();

        }
        catch (Exception ex)
        {
            Console.WriteLine(txtToDate.Text);
        }

    }

    

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gv.Rows.Count > 0)
        {
            gv.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gv.HeaderRow.TableSection =            TableRowSection.TableHeader;
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        BindEmployeeList();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
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
          
        }
        ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindEmployeeList()
    {
        bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && (d.RelivingStatus == null || d.RelivingStatus == 0) && d.DeptId != null
                        && d.CompanyId == cId & d.DeptId == dId
                        select new
                        {
                            d.EmployeeId,
                            EmpName = d.FName + " " + d.Lname
                        }).Distinct();

            if (AdminStatus == false)
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }

            ddlEmployee.DataSource = data;
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, "--Select--");
        }
    }
}