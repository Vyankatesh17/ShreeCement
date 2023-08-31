using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rpt_late_coming_report : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    string sqlquery;
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
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (grd_LateCommerce.Rows.Count > 0)
            grd_LateCommerce.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            string query = string.Format(@"SELECT        AL.LogId, AL.AttendanceDate, E.EmployeeId, E.EmployeeNo, E.FName + ' ' + E.Lname AS EmpName, D.DeptName, C.CompanyName, D1.DesigName, AL.InTime, AL.LateBy, MS.Shift
FROM            AttendaceLogTB AS AL INNER JOIN
                         EmployeeTB AS E ON AL.EmployeeId = E.EmployeeId INNER JOIN
                         MasterDeptTB AS D ON E.DeptId = D.DeptID INNER JOIN
                         CompanyInfoTB AS C ON AL.CompanyId = C.CompanyId LEFT OUTER JOIN
                         MasterDesgTB AS D1 ON E.DesgId = D1.DesigID INNER JOIN
                         MasterShiftTB AS MS ON AL.ShiftId = MS.ShiftID
WHERE     E.IsActive=1 AND  ((AL.LateBy IS NOT NULL) OR (AL.LateBy > 0)) AND (AL.TenantId = '{0}')", Convert.ToString(Session["TenantId"]));

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
                query += " AND E.EmployeeId='" + Convert.ToInt32(Session["EmpId"])+"'";
            }
            DateTime fromDate = string.IsNullOrEmpty(txtFromDate.Text) ? DateTime.Now : Genreal.GetDate(txtFromDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.Now : Genreal.GetDate(txtToDate.Text);

            query += string.Format(" AND (CONVERT(date, AL.AttendanceDate) BETWEEN CONVERT(date, '{0}') AND CONVERT(date, '{1}'))", txtFromDate.Text, txtToDate.Text);

            DataTable dataTable = g.ReturnData(query);
            grd_LateCommerce.DataSource = dataTable;
            grd_LateCommerce.DataBind();

        }
        catch (Exception ex)
        {
            //throw ex;
        }

    }
   
    protected void grd_LateCommerce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_LateCommerce.Rows.Count > 0)
        {
            grd_LateCommerce.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_LateCommerce.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
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