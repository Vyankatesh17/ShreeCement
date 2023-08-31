using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterDepartment : System.Web.UI.Page
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
                BindAllDept();
                fillcompany();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    private void fillcompany()
    {
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());

        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select--");
        }

        //var data = (from dt in HR.CompanyInfoTBs
        //           where dt.Status == 1 && dt.TenantId==Session["TenantId"].ToString()
        //           select dt).OrderBy(d=>d.CompanyName);
        //if (data != null && data.Count() > 0)
        //{

        //    ddlCompany.DataSource = data;
        //    ddlCompany.DataTextField = "CompanyName";
        //    ddlCompany.DataValueField = "CompanyId";
        //    ddlCompany.DataBind();
        //    ddlCompany.Items.Insert(0, "--Select--");
        //}
    }

    public void BindAllDept()
    {
        
        int companyid = Convert.ToInt32(Session["CompanyID"]);
        if (Session["UserType"].ToString() == "LocationAdmin")
        {
           var deptDataLOcationAdmin = (from d in HR.MasterDeptTBs
                            join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                            where c.TenantId == Session["TenantId"].ToString() && d.CompanyId == companyid && d.Status == 1
                                        select new
                            {
                                d.DeptID,
                                d.DeptName,
                                d.CompanyId,
                                c.CompanyName,
                                Status = d.Status == 1 ? "Active" : "In Active"
                            }).OrderBy(d => d.CompanyName).ThenBy(d => d.DeptName);
            if (deptDataLOcationAdmin.Count() > 0)
            {
                grd_Dept.DataSource = deptDataLOcationAdmin;
                grd_Dept.DataBind();
            }
        }
        else
        {
             var deptData = (from d in HR.MasterDeptTBs
                            join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                             where c.TenantId == Session["TenantId"].ToString() && d.Status == 1
                             select new
                            {
                                d.DeptID,
                                d.DeptName,
                                d.CompanyId,
                                c.CompanyName,
                                Status = d.Status == 1 ? "Active" : "In Active"
                            }).OrderBy(d => d.CompanyName).ThenBy(d => d.DeptName);
            if (deptData.Count() > 0)
            {
                grd_Dept.DataSource = deptData;
                grd_Dept.DataBind();
            }
        }           
    }

    //protected void OnClick_Edit(object sender, EventArgs e)
    //{
    // Change by Abdul rahman 2/12/2014
    protected void OnClick_Edit(object sender, ImageClickEventArgs e)
    {

        ImageButton Lnk = (ImageButton)sender;
        string DeptId = Lnk.CommandArgument;
        lbldeptid.Text = DeptId;
        MultiView1.ActiveViewIndex = 1;
        MasterDeptTB MT = HR.MasterDeptTBs.Where(d => d.DeptID == Convert.ToInt32(DeptId)).First();
        txtdeptname.Text = MT.DeptName;
        ddlCompany.SelectedValue = MT.CompanyId.ToString();
        btnsubmit.Text = "Update";

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
            var dte = from p in HR.MasterDeptTBs.Where(d => d.DeptName == txtdeptname.Text && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)) select p;
             if (dte.Count() > 0)
             {
                 g.ShowMessage(this.Page, "Department Details Already Exist");
                 //modpop.ShowPopUp();

             }
             else
             {
                 MasterDeptTB MTB = new MasterDeptTB();
                 MTB.DeptName = txtdeptname.Text;
                 MTB.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                MTB.TenantId = Convert.ToString(Session["TenantId"]);
                 MTB.Status = 1;

                 HR.MasterDeptTBs.InsertOnSubmit(MTB);
                 HR.SubmitChanges();

                 //modpop.Message = "Submitted Successfully";
                 //modpop.ShowPopUp();

                 g.ShowMessage(this.Page, "Department Details Saved Successfully");
                 Clear();
             }
        }
        else
        {
            // Changes by Abdul Rahman 1/12/2014 update code check duplicate
            var dte = from p in HR.MasterDeptTBs.Where(d => d.DeptName == txtdeptname.Text && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.DeptID == Convert.ToInt32(lbldeptid.Text)) select p;
            if (dte.Count() > 0)
            {
                updatecode();
                //g.ShowMessage(this.Page, "Department Name Already Exist");
                //MultiView1.ActiveViewIndex = 1;
                //modpop.ShowPopUp();

            }
            else
            {
                var dte1 = from p in HR.MasterDeptTBs.Where(d => d.DeptName == txtdeptname.Text && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.DeptID != Convert.ToInt32(lbldeptid.Text)) select p;
                if (dte1.Count() > 0)
                {

                    g.ShowMessage(this.Page, "Department Details Already Exist");
                     MultiView1.ActiveViewIndex = 1;

                }
                else
                {
                    updatecode();
                }
            }
            
               
           
        }
    }

    private void updatecode()
    {
        try
        {
            MasterDeptTB MT = HR.MasterDeptTBs.Where(d => d.DeptID == Convert.ToInt32(lbldeptid.Text)).First();
            MT.DeptName = txtdeptname.Text;
            MT.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            MT.Status = 1;
            MT.TenantId = Session["TenantId"].ToString();
            HR.SubmitChanges();
            g.ShowMessage(this.Page, "Department Details Updated Successfully");

            //modpop.Message = "Updated Successfully";
            //modpop.ShowPopUp();
            btnsubmit.Text = "Save";
            Clear();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    public void Clear()
    {
        txtdeptname.Text = null;
        lbldeptid.Text = "";
        BindAllDept();
        fillcompany();
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

    }

    protected void grd_Dept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Dept.PageIndex = e.NewPageIndex;
        BindAllDept();
        grd_Dept.DataBind();
    }

    protected void grd_Dept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Dept.Rows.Count > 0)
        {
            grd_Dept.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Dept.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void Delete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string DeptId = Lnk.CommandArgument;
        lbldeptid.Text = DeptId;
       
        MasterDeptTB MT = HR.MasterDeptTBs.Where(d => d.DeptID == Convert.ToInt32(DeptId)).First();

        MT.Status = 0;
        HR.SubmitChanges();        
        g.ShowMessage(this.Page, "Department Delete Successfully.....");
        Response.Redirect("MasterDepartment.aspx");
    }
}