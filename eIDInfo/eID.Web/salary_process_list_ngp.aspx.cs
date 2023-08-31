using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class salary_process_list_ngp : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindDepartmentList();
                decimal dc = 0;
                HttpContext.Current.Items["TotalAmount"] = dc;
                //BindEmployeeList();
                //BindGridview();
                //txtFromDate.Attributes.Add("readonly", "readonly");
                //txtToDate.Attributes.Add("readonly", "readonly");
            }
        }
        BindJqFunctions();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal dc = 0;
        HttpContext.Current.Items["TotalAmount"] = dc;
        BindDepartmentList();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal dc = 0;
        HttpContext.Current.Items["TotalAmount"] = dc;
        BindEmployeeList();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGridview();
    }
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvList.Rows.Count > 0)
        {
            gvList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    private void BindCompanyList()
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
        catch (Exception ex) { }
    }
    private void BindDepartmentList()
    {
        ddlDepartment.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterDeptTBs where d.Status == 1 && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select new { d.DeptName, d.DeptID }).Distinct();

            if (data.Count() > 0)
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
        ddlEmployee.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
                        && d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)
                        select new { EmpName = d.FName + " " + d.Lname + "(" + d.EmployeeNo + ")", d.EmployeeId }).Distinct();

            if (data.Count() > 0)
            {
                ddlEmployee.DataSource = data;
                ddlEmployee.DataTextField = "EmpName";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void BindGridview()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            decimal dc = 0;
            HttpContext.Current.Items["TotalAmount"] = dc;
            var data = (from d in db.EmployeeSalaryNgpTBs
                        join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                        join d1 in db.MasterDeptTBs on d.DeptId equals d1.DeptID
                        join d2 in db.MasterDesgTBs on e.DesgId equals d2.DesigID
                        join c in db.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                        where d.TenantId == Session["TenantId"].ToString()
                        select new
                        {
                            d.SalaryId,
                            d.SalaryPerDay,
                            d.PresentDays,
                            d.TotalSalary,
                            d.FromDate,
                            d.ToDate,
                            d.WeekNo,
                            EmpName = e.FName + " " + e.Lname,
                            e.EmployeeNo,
                            e.EmployeeId,
                            e.ContactNo,
                            e.Email,
                            d.DeptId,
                            d1.DeptName,
                            d2.DesigName,
                            d.CompanyId,
                            c.CompanyName
                        }).OrderByDescending(d => d.SalaryId).ToList();

            if (ddlCompany.SelectedIndex > 0)
                data = data.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).ToList();
            if(ddlDepartment.SelectedIndex>0)
                data = data.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)).ToList();
            if (ddlEmployee.SelectedIndex > 0)
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue)).ToList();
            if (txtFromDate.Text != "")
                data = data.Where(d => d.FromDate == Convert.ToDateTime(txtFromDate.Text)).ToList();
            if (txtToDate.Text != "")
                data = data.Where(d => d.ToDate == Convert.ToDateTime(txtToDate.Text)).ToList();


           
            if (data.Count > 0)
            {
                var total = data.Select(a => a.TotalSalary).Sum();
                HttpContext.Current.Items["TotalAmount"] = total;
            }



            gvList.DataSource = data;
            gvList.DataBind();
        }
    }
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
}