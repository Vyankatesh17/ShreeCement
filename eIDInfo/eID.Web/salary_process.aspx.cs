using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class salary_process : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal gen=new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                ddlMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();
                FillCompanyList();
                BindDepartmentList();
                BindEmployeeList();
                BindYear();
            }
        }
    }

    protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindDepartmentList();
        }
        catch(Exception ex)
        {

        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindEmployeeList();
        }
        catch(Exception ex) { }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    #region Methods
    private void FillCompanyList()
    {
        ddlCompanyList.Items.Clear();
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
        if (data != null && data.Count() > 0)
        {
            ddlCompanyList.DataSource = data;
            ddlCompanyList.DataTextField = "CompanyName";
            ddlCompanyList.DataValueField = "CompanyId";
            ddlCompanyList.DataBind();
        }
        ddlCompanyList.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindDepartmentList()
    {
        int companyid = Convert.ToInt32(ddlCompanyList.SelectedValue);
        ddlDepartment.Items.Clear();
        var data = (from dt in HR.MasterDeptTBs
                    where dt.Status == 1 && dt.CompanyId == companyid
                    select dt).OrderBy(d => d.DeptName);
        if (data != null && data.Count() > 0)
        {

            ddlDepartment.DataSource = data;
            ddlDepartment.DataTextField = "DeptName";
            ddlDepartment.DataValueField = "DeptID";
            ddlDepartment.DataBind();
        }
        ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindEmployeeList()
    {
        try
        {
            ddlEmployeeList.Items.Clear();
            int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from d in HR.EmployeeTBs
                        where (d.RelivingStatus == null || d.RelivingStatus == 0) && (d.IsActive == null || d.IsActive == true)
                        select new
                        {
                            d.EmployeeId,
                            Name = d.FName + ' ' + d.Lname,
                            d.CompanyId,
                            d.DeptId
                        }).OrderBy(d => d.Name).Distinct();
            if (cId > 0)
            {
                data = data.Where(d => d.CompanyId == cId).Distinct();
            }
            if (dId > 0)
            {
                data = data.Where(d => d.DeptId == dId).Distinct();
            }

            if (data != null && data.Count() > 0)
            {
                ddlEmployeeList.DataSource = data;
                ddlEmployeeList.DataTextField = "Name";
                ddlEmployeeList.DataValueField = "EmployeeId";
                ddlEmployeeList.DataBind();
            }
            ddlEmployeeList.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        catch (Exception ex) { }
    }
    private void BindYear()
    {
        ddlYear.Items.Clear();
        int year = DateTime.Now.AddYears(-5).Year;
        for (int i = year; i < DateTime.Now.AddYears(2).Year; i++)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }
    #endregion
}