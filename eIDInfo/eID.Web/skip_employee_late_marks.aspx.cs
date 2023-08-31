using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class skip_employee_late_marks : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindGrid();
                BindCompany();
                //BindEmployeeList();
                BindDepartmentList();
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                string list = "";
                foreach (ListItem empitem in lstFruits.Items)
                {
                    if (empitem.Selected)
                    {
                        list += empitem.Text + " " + empitem.Value + ",";

                        var checkExists = (from d in db.SkipEmpLatemarksTBs where d.emp_id == Convert.ToInt32(empitem.Value) select d).Distinct();

                        var empdata = db.EmployeeTBs.Where(a => a.EmployeeId == Convert.ToInt32(empitem.Value)).FirstOrDefault();
                        SkipEmpLatemarksTB skipEmp = new SkipEmpLatemarksTB();
                        string msg = "";
                        if (btnSave.Text == "Update")
                        {
                            checkExists = checkExists.Where(d => d.skip_id != Convert.ToInt32(hfId.Value)).Distinct();
                            if (checkExists.Count() > 0)
                            {
                                gen.ShowMessage(this.Page, "Employee already present..");
                            }
                            else
                            {
                                skipEmp = db.SkipEmpLatemarksTBs.Where(d => d.skip_id == Convert.ToInt32(hfId.Value)).FirstOrDefault();
                                skipEmp.emp_id = Convert.ToInt32(empitem.Value);
                                skipEmp.is_active = rblStatus.SelectedIndex;
                                skipEmp.company_id = Convert.ToInt32(ddlCompany.SelectedValue);
                                skipEmp.TenantId = Convert.ToString(Session["TenantId"]);
                                skipEmp.Single_Punch = rblsinglepunch.SelectedIndex;
                                skipEmp.Department_Id = Convert.ToInt32(ddlDepartment.SelectedValue);

                                empdata.Skip_Latemark = rblStatus.SelectedIndex == 0 ? 1 : 0;
                                empdata.Single_Punch_Present = rblsinglepunch.SelectedIndex == 0 ? 1 : 0;

                                gen.ShowMessage(this.Page, "Employee skip record and Single Punch Present updated successfully");
                            }
                        }
                        else
                        {
                            if (checkExists.Count() > 0)
                            {
                                gen.ShowMessage(this.Page, "Employee already present..");
                            }
                            else
                            {
                                skipEmp.Single_Punch = rblsinglepunch.SelectedIndex;
                                skipEmp.is_active = rblStatus.SelectedIndex;
                                skipEmp.emp_id = Convert.ToInt32(empitem.Value);
                                skipEmp.company_id = Convert.ToInt32(ddlCompany.SelectedValue);
                                skipEmp.Department_Id = Convert.ToInt32(ddlDepartment.SelectedValue);
                                skipEmp.TenantId = Convert.ToString(Session["TenantId"]);
                                db.SkipEmpLatemarksTBs.InsertOnSubmit(skipEmp);

                                empdata.Skip_Latemark = rblStatus.SelectedIndex == 0 ? 1 : 0;
                                empdata.Single_Punch_Present = rblsinglepunch.SelectedIndex == 0 ? 1 : 0;

                                gen.ShowMessage(this.Page, "Employee skip record and Single Punch Present added successfully");
                            }
                        }
                        db.SubmitChanges();
                    }
                }
                BindGrid();
                btnSave.Text = "Save";
                hfId.Value = "";
                rblStatus.SelectedIndex = 0;                        
                    
            }
        }
        catch(Exception ex)
        {

        }
    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
       
        LinkButton lbtnEvent = (LinkButton)sender;
        hfId.Value = lbtnEvent.CommandArgument;

        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            var data = db.SkipEmpLatemarksTBs.Where(d => d.skip_id == Convert.ToInt32(hfId.Value)).FirstOrDefault();
            if (data != null)
            {
                lstFruits.SelectedValue = data.emp_id.Value.ToString();
                rblStatus.SelectedIndex = data.is_active.Value;
                ddlCompany.SelectedValue = data.company_id.ToString();
                if(data.Department_Id != null)
                {
                    ddlDepartment.SelectedValue = data.Department_Id.ToString();
                }                
                if (data.Single_Punch != null)
                {
                    rblsinglepunch.SelectedIndex = data.Single_Punch.Value;
                }
               
                btnSave.Text = "Update";
            }
        }
    }
    private void BindGrid()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.SkipEmpLatemarksTBs
                        join e in db.EmployeeTBs on d.emp_id equals e.EmployeeId
                        select new
                        {
                            d.emp_id,
                            d.skip_id,
                           status= d.is_active==0?"Active":"In Active",
                            SinglePunch = d.Single_Punch == 0? "Active" : "In Active",
                            e.EmployeeNo,
                            EmpName = e.FName + " " + e.Lname
                        }).Distinct();

            gvList.DataSource = data;
            gvList.DataBind();
        }
    }
    private void BindCompany()
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
        try
        {
            //lstFruits.Items.Clear();
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
           {
                int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from d in db.EmployeeTBs
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

            //if (data != null && data.Count() > 0)
            //{
                    lstFruits.DataSource = data;
                    lstFruits.DataTextField = "Name";
                    lstFruits.DataValueField = "EmployeeId";
                    lstFruits.DataBind();
            //}
            //ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        }
        catch (Exception ex) { }
    }



    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        BindEmployeeList();
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





    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }
}