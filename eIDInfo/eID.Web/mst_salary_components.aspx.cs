using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// developer Name : Shrikant Patil
/// Date : 29th Sept 2015
/// Description : To save, update and display salary component details
/// Page Name : AddSalaryComponent.aspx
/// Table Name : SalaryComponentTB
/// </summary>
public partial class mst_salary_components : System.Web.UI.Page
{
    #region Declaration of variables
    HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    #endregion

    #region Page load method
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["TenantId"])))
        {
            if (!IsPostBack)
            {
                fillCompany();
                MultiView1.ActiveViewIndex = 0;
                BindAllcomponent();// call method for display all components on page load first time
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        if (grd_Salary_Component.Rows.Count > 0)
            grd_Salary_Component.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }
    #endregion    

    #region Edit button click event for fetch and display selected component from gridview for display and edit component information
    protected void OnClick_Edit(object sender, EventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string cptId = Lnk.CommandArgument;
        lblCompid.Text = cptId;
        MultiView1.ActiveViewIndex = 1;
        SalaryComponentTB MT = db.SalaryComponentTBs.Where(d => d.Componentid == Convert.ToInt32(cptId)).FirstOrDefault();
        ddlComponent.Text = MT.ComponentType;
        txtComponentname.Text = MT.ComponentName;
        ddlCompany.SelectedValue = string.IsNullOrEmpty(Convert.ToString(MT.CompanyId)) ? "0" : MT.CompanyId.Value.ToString();
        btnsubmit.Text = "Update";
    }
    #endregion

    #region Save/Update button click event for save/update component information
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            var chkExists = (from d in db.SalaryComponentTBs where d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)&& d.ComponentName==txtComponentname.Text&&d.ComponentType==ddlComponent.SelectedValue
                             select d).Distinct();
            
            if (btnsubmit.Text == "Save")// for save component details
            {
                if (chkExists.Count() > 0)
                {
                    g.ShowMessage(this.Page, "Companent already present..");
                }
                else
                {
                    SalaryComponentTB MTB = new SalaryComponentTB();
                    MTB.ComponentName = txtComponentname.Text;
                    MTB.ComponentType = ddlComponent.SelectedValue.ToString();
                    MTB.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    MTB.TenantId = Convert.ToString(Session["TenantId"]);
                    db.SalaryComponentTBs.InsertOnSubmit(MTB);
                    db.SubmitChanges();
                    g.ShowMessage(this.Page, "Salary Component Details Saved Successfully");
                    Clear();
                }
            }
            else// for update component details
            {
                chkExists = chkExists.Where(d => d.Componentid != Convert.ToInt32(lblCompid.Text)).Distinct();
                if (chkExists.Count() > 0)
                {
                    g.ShowMessage(this.Page, "Companent already present..");
                }
                else
                {
                    SalaryComponentTB MT = db.SalaryComponentTBs.Where(d => d.Componentid == Convert.ToInt32(lblCompid.Text)).First();
                    MT.ComponentName = txtComponentname.Text;
                    MT.ComponentType = ddlComponent.SelectedValue.ToString();
                    MT.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    MT.TenantId = Convert.ToString(Session["TenantId"]);
                    db.SubmitChanges();
                    g.ShowMessage(this.Page, "Salary Component Details Updated Successfully");
                    Clear();
                }
            }
        }
    }
    #endregion

    #region Cancel button click event for clear all fields and display component list
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    #endregion

    #region Add new button click event for display fields for add new component
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    #endregion

    #region gridview page index changing event
    protected void grd_Dept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Salary_Component.PageIndex = e.NewPageIndex;
        BindAllcomponent();        
    }
    #endregion

    #region Method for fetch and bind all components to gridview for display component list
    public void BindAllcomponent()
    {
        var cptData = (from d in db.SalaryComponentTBs
                       join c in db.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                       where d.TenantId==Convert.ToString(Session["TenantId"])  
                       select new {
                           d.Componentid,
                           d.ComponentName,
                           d.ComponentType,
                           d.FixedValue,
                           d.PercenComponentType,
                           d.PercentageValue,
                           d.Status,
                           d.ValueType,
                           c.CompanyName
                       }).ToList();
        if (cptData.Count() > 0)
        {
            grd_Salary_Component.DataSource = cptData;
            grd_Salary_Component.DataBind();
           
        }
    }
    #endregion

    #region Method for clear all controls on page
    public void Clear()
    {
        txtComponentname.Text = null;
        lblCompid.Text = "";
        ddlComponent.SelectedIndex = ddlCompany.SelectedIndex = 0;
        //txtvalue.Text = null;
        BindAllcomponent();
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";
    }
    private void fillCompany()
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
    #endregion

    protected void grd_Salary_Component_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Salary_Component.Rows.Count > 0)
        {
            grd_Salary_Component.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Salary_Component.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}