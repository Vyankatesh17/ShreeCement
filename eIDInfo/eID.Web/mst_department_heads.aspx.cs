using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mst_department_heads : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                BindCompanyList();
                BindAllSource();
                BindEmployeeList();
                BindDepartmentList();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        if (grd_Deptmaster.Rows.Count > 0)
            grd_Deptmaster.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
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
    private void BindEmployeeList()
    {
        ddlempname.Items.Clear();
        var data = (from dt in HR.EmployeeTBs
                    where dt.IsActive == true && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
                    select new { EmpName = dt.FName + ' ' + dt.Lname, dt.EmployeeId }).Distinct();
        if (data != null && data.Count() > 0)
        {

            ddlempname.DataSource = data;
            ddlempname.DataTextField = "EmpName";
            ddlempname.DataValueField = "EmployeeId";
            ddlempname.DataBind();
        }
        ddlempname.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void BindDepartmentList()
    {
        ddldeptname.Items.Clear();
        var data = (from dt in HR.MasterDeptTBs where dt.Status==1 && dt.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && dt.TenantId==Convert.ToString(Session["TenantId"])
                    select dt).OrderBy(d => d.DeptName);
        if (data != null && data.Count() > 0)
        {
            ddldeptname.DataSource = data;
            ddldeptname.DataTextField = "DeptName";
            ddldeptname.DataValueField = "DeptID";
            ddldeptname.DataBind();
        }
        ddldeptname.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void BindAllSource()
    {

        var Deptmaster = (from s in HR.DepartmentHeadTBs
                          join d in HR.MasterDeptTBs on s.DeptID equals d.DeptID
                          join e in HR.EmployeeTBs on s.EmpID equals e.EmployeeId
                          join c in HR.CompanyInfoTBs on s.CompanyId equals c.CompanyId
                         where e.IsActive == true && s.TenantId== Convert.ToString(Session["TenantId"])
                         select new { s.HeadId, Fname = e.FName + ' ' + e.Lname, d.DeptName,c.CompanyName }).ToList();
        if (Deptmaster.Count() > 0)
        {
            grd_Deptmaster.DataSource = Deptmaster;
            grd_Deptmaster.DataBind();

        }


    }

    public void Clear()
    {
        ddlCompany.SelectedIndex = ddldeptname.SelectedIndex = ddlempname.SelectedIndex = 0;
        BindAllSource();
        MultiView1.ActiveViewIndex = 0;

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        int deptid = Convert.ToInt32(ddldeptname.SelectedValue);
        if (btnsubmit.Text == "Save")
        {
            var dt = from p in HR.DepartmentHeadTBs.Where(d =>d.DeptID==deptid) select p;
            if (dt.Count() > 0)
            {
                g.ShowMessage(this.Page, "Dept Head Already Assigned");
            }
            else
            {

                DepartmentHeadTB mtb = new DepartmentHeadTB();
                mtb.DeptID = Convert.ToInt32(ddldeptname.SelectedValue);
                mtb.EmpID = Convert.ToInt32(ddlempname.SelectedValue);
                mtb.TenantId = Convert.ToString(Session["TenantId"]);
                mtb.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                HR.DepartmentHeadTBs.InsertOnSubmit(mtb);
                HR.SubmitChanges();
            }
            
            g.ShowMessage(this.Page, "Add Data Successfully");
            Clear();
        }
        else
        {
             var dt = from p in HR.DepartmentHeadTBs.Where(d =>d.DeptID==deptid && d.HeadId== Convert.ToInt32(lblheadid.Text)) select p;
            if (dt.Count() > 0)
            {

                DepartmentHeadTB mt = HR.DepartmentHeadTBs.Where(s => s.HeadId == Convert.ToInt32(lblheadid.Text)).First();
                mt.DeptID = Convert.ToInt32(ddldeptname.SelectedValue);
                mt.EmpID = Convert.ToInt32(ddlempname.SelectedValue);
                mt.TenantId = Convert.ToString(Session["TenantId"]);
                mt.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

                HR.SubmitChanges();

                g.ShowMessage(this.Page, "Updated Successfully");
                btnsubmit.Text = "Save";
                Clear();
            }
            else
            {
                dt = from p in HR.DepartmentHeadTBs.Where(d => d.DeptID == deptid) select p;
                if (dt.Count() > 0)
                {
                    g.ShowMessage(this.Page, "Dept Head Already Assigned");
                }
                else
                {
                    DepartmentHeadTB mt = HR.DepartmentHeadTBs.Where(s => s.HeadId == Convert.ToInt32(lblheadid.Text)).First();
                    mt.DeptID = Convert.ToInt32(ddldeptname.SelectedValue);
                    mt.EmpID = Convert.ToInt32(ddlempname.SelectedValue);
                    mt.TenantId = Convert.ToString(Session["TenantId"]);
                    mt.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    HR.SubmitChanges();

                    g.ShowMessage(this.Page, "Updated Successfully");
                    btnsubmit.Text = "Save";
                    Clear();
                }
            }
        }
    }
           
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        
        LinkButton Lnk = (LinkButton)sender;
        string HeadID = Lnk.CommandArgument;
        lblheadid.Text = HeadID;

        //int deptid = lbldeptid.Text;
        DepartmentHeadTB mt = HR.DepartmentHeadTBs.Where(s => s.HeadId == Convert.ToInt32(HeadID)).First();
        ddlempname.SelectedValue = Convert.ToInt32(mt.EmpID).ToString();
        ddldeptname.SelectedValue = Convert.ToInt32(mt.DeptID).ToString();
       
        btnsubmit.Text = "Update";
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void grd_Deptmaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Deptmaster.Rows.Count > 0)
        {
            grd_Deptmaster.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Deptmaster.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
        BindDepartmentList();
    }
}