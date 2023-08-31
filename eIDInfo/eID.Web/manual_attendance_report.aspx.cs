using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manual_attendance_report : System.Web.UI.Page
{
    Genreal g = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (grdAtt.Rows.Count > 0)
        {
            grdAtt.UseAccessibleHeader = true;
            grdAtt.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindAllDataGridview();
                fillcompany();
                BindEmployeeSearchList();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        BindJqFunctions();
    }

    protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
        BindDepartmentSearch(cId);
        BindEmployeeSearchList();
    }

    protected void ddlDepartmentList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeSearchList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAllDataGridview();
    }

    protected void grdAtt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdAtt.Rows.Count > 0)
        {
            grdAtt.UseAccessibleHeader = true;
            grdAtt.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    private void BindAllDataGridview()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                DateTime dateTime = string.IsNullOrEmpty(txtAttDate.Text) ? DateTime.Now : Convert.ToDateTime(txtAttDate.Text);

                var data = (from d in db.ManualAttendanceTBs
                            join e in db.EmployeeTBs on d.EmpId equals e.EmployeeId
                            join c in db.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                            join d1 in db.MasterDeptTBs on e.DeptId equals d1.DeptID
                            where d.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.AttendanceDate.Value.Date == dateTime.Date
                            select new
                            {
                                d.AttendanceDate,
                                d.CompanyId,
                                d.CreatedBy,
                                d.CreatedDate,
                                d.EmpId,
                                d.InTime,
                                d.IsActive,
                                d.MAttendID,
                                d.ModifiedBy,
                                d.ModifiedDate,
                                d.OuTime,
                                d.Remarks,
                                d.Status,
                                d.TenantId,
                                EmpName = e.FName + " " + e.Lname,
                                DeviceCode = e.EmployeeNo,
                                EmployeeNo = d.MAttendID,
                                d1.DeptName,
                                c.CompanyName,
                                d1.DeptID
                            }
                           ).Distinct();

                if (ddlCompanyList.SelectedIndex > 0)
                {
                    data = data.Where(d => d.CompanyId == Convert.ToInt32(ddlCompanyList.SelectedValue));
                }
                if (ddlDepartmentList.SelectedIndex > 0)
                {
                    data = data.Where(d => d.DeptID == Convert.ToInt32(ddlDepartmentList.SelectedValue));
                }
                if (ddlEmployeeList.SelectedIndex > 0)
                {
                    data = data.Where(d => d.EmpId == Convert.ToInt32(ddlEmployeeList.SelectedValue));
                }
                grdAtt.DataSource = data;
                grdAtt.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    private void fillcompany()
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
    private void BindDepartmentSearch(int companyid)
    {
        ddlDepartmentList.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from dt in db.MasterDeptTBs
                        where dt.Status == 1 && dt.CompanyId == companyid
                        select dt).OrderBy(d => d.DeptName);
            if (data != null && data.Count() > 0)
            {
                ddlDepartmentList.DataSource = data;
                ddlDepartmentList.DataTextField = "DeptName";
                ddlDepartmentList.DataValueField = "DeptID";
                ddlDepartmentList.DataBind();
            }
        }
        ddlDepartmentList.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindEmployeeSearchList()
    {
        ddlEmployeeList.Items.Clear();
        int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
        int dId = ddlDepartmentList.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartmentList.SelectedValue) : 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true && (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { EmpName = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.ManagerID }).Distinct();
            if (Session["UserType"].ToString().Equals("User"))
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            if (data != null && data.Count() > 0)
            {
                if (Convert.ToInt32(Session["IsDeptHead"]) == 1)
                {
                    int manId = Convert.ToInt32(Session["EmpId"]);
                    data = data.Where(d => d.ManagerID == manId || d.EmployeeId == manId).Distinct();
                }
                ddlEmployeeList.DataSource = data;
                ddlEmployeeList.DataTextField = "EmpName";
                ddlEmployeeList.DataValueField = "EmployeeId";
                ddlEmployeeList.DataBind();
            }
            ddlEmployeeList.Items.Insert(0, new ListItem("--Select--", "0"));
            if (Session["UserType"].ToString().Equals("User"))
            {
                ddlEmployeeList.SelectedValue = Session["EmpId"].ToString();
            }
        }
    }
}