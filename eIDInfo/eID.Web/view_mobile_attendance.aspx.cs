using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class view_mobile_attendance : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                ddlMonth.SelectedIndex = DateTime.Now.Month;
                BindYear();
                BindEmployeeList();
                BindCompanyList();
                BindDataGridView();

                if (Session["UserType"].ToString() != "User")
                {
                    company.Visible = true;                   
                    employee.Visible = true;
                }
            }
        }
        BindJqFunctions();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindDataGridView();
        }
        catch (Exception ex) { }
    }
    private void BindEmployeeList()
    {
        bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && (d.RelivingStatus == null || d.RelivingStatus == 0) && d.DeptId != null
                        && d.CompanyId == cId
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
    private void BindDataGridView()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int eId = ddlEmployee.SelectedIndex > 0 ? Convert.ToInt32(ddlEmployee.SelectedValue) : 0;
            var data = (from d in db.AppAttendanceTBs
                        join e in db.EmployeeTBs on d.EmpId equals e.EmployeeId
                        join c in db.CompanyInfoTBs on e.CompanyId equals c.CompanyId
                        where e.IsActive == true && e.TenantId==Convert.ToString(Session["TenantId"])&& d.PunchDate.Value.Year == Convert.ToInt32(ddlYear.SelectedValue) && d.PunchDate.Value.Month == ddlMonth.SelectedIndex
                        select new
                        {
                            e.CompanyId,
                            EmpName = e.FName + " " + e.Lname,
                            e.EmployeeNo,
                            e.EmployeeId,
                            e.MachineID,
                            Date = d.PunchDate.Value,
                            d.PunchType,
                            Time = d.PunchTime.Value,
                            d.Location,
                            d.Longitude,
                            d.Latitude,
                            Company=c.CompanyName
                        }).Distinct();

            if (Session["UserType"].ToString() == "User")
            {
                eId = Convert.ToInt32(Session["EmpId"]);
            }
            if (cId > 0)
            {
                data = data.Where(d => d.CompanyId == cId).Distinct();
            }
            if (eId > 0)
            {
                data = data.Where(d => d.EmployeeId == eId).Distinct();
            }
            if (!string.IsNullOrEmpty(txtEmpCode.Text))
            {
                data = data.Where(d => d.EmployeeNo.Contains(txtEmpCode.Text)).Distinct();
            }
            if (AdminStatus == false)
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            gvList.DataSource = data;
            gvList.DataBind();

        }
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
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvList.Rows.Count > 0)
        {
            gvList.UseAccessibleHeader = true;
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }
}