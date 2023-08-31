using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class AddSalaryComponent : System.Web.UI.Page
{
    #region Declaration of variables
    HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    #endregion
    #region Page load method
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            BindAllcomponent();// call method for display all components on page load first time
        }
    }
    #endregion    
    #region Edit button click event for fetch and display selected component from gridview for display and edit component information
    protected void OnClick_Edit(object sender, EventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string cptId = Lnk.CommandArgument;
        lblCompid.Text = cptId;
        MultiView1.ActiveViewIndex = 1;
        SalaryComponentTB MT = db.SalaryComponentTBs.Where(d => d.Componentid == Convert.ToInt32(cptId)).First();
        ddlCompany.Text = MT.ComponentType;
        txtComponentname.Text = MT.ComponentName;
        btnsubmit.Text = "Update";
    }
    #endregion
    #region Save/Update button click event for save/update component information
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")// for save component details
        {
            if (txtComponentname.Text != "")
            {
                SalaryComponentTB MTB = new SalaryComponentTB();
                MTB.ComponentName = txtComponentname.Text;
                MTB.ComponentType = ddlCompany.SelectedValue.ToString();
                db.SalaryComponentTBs.InsertOnSubmit(MTB);
                db.SubmitChanges();
                g.ShowMessage(this.Page, "Salary Component Details Saved Successfully");
                Clear();
            }
        }
        else// for update component details
        {
            if (txtComponentname.Text != null)
            {
                SalaryComponentTB MT = db.SalaryComponentTBs.Where(d => d.Componentid == Convert.ToInt32(lblCompid.Text)).First();
                MT.ComponentName = txtComponentname.Text;
                MT.ComponentType = ddlCompany.SelectedValue.ToString();
                db.SubmitChanges();
                g.ShowMessage(this.Page, "Salary Component Details Updated Successfully");
                Clear();
            }
        }
    }
    #endregion
    #region Cancel button click event for clear all fields and display component list
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
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
                       select d).ToList();
        if (cptData.Count() > 0)
        {
            grd_Salary_Component.DataSource = cptData;
            grd_Salary_Component.DataBind();
            lblcnt.Text = cptData.Count().ToString();
        }
    }
    #endregion
    #region Method for clear all controls on page
    public void Clear()
    {
        txtComponentname.Text = null;
        lblCompid.Text = "";
        //txtvalue.Text = null;
        BindAllcomponent();
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";
    }
    #endregion
}