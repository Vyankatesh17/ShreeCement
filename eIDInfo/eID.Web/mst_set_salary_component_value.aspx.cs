using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// developer Name : Shrikant Patil
/// Date : 29th Sept 2021
/// Description : To update and display salary component value details

/// </summary>
public partial class mst_set_salary_component_value : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            BindAllcomponent();
            BindAllcomponentonDropDown();
            BindCompDropDown();
        }
        BindJqFunctions();
    }
    #endregion
    #region Edit button click event for fetch and display selected component from gridview for display and edit component value information
    protected void OnClick_Edit(object sender, EventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string cptId = Lnk.CommandArgument;
        lbldeptid.Text = cptId;
        BindAllcomponent();
        MultiView1.ActiveViewIndex = 1;
        SalaryComponentTB MT = db.SalaryComponentTBs.Where(d => d.Componentid == Convert.ToInt32(cptId)).First();
        ddlCompany.Text = MT.ComponentType;
        ddlComponentType.SelectedValue = MT.ComponentName;
        if(MT.ValueType=="Fixed")
        {
            rb_Value_Type.SelectedIndex = 0;
        }
        else
        {
            rb_Value_Type.SelectedIndex = 1;
        }
        if (MT.FixedValue == null || MT.FixedValue == 0)
        {
            txtvalue.Text = MT.PercentageValue;
            if (!string.IsNullOrEmpty(MT.PercenComponentType))
            {
                ddlComponentType1.Visible = true;
                ddlComponentType1.Text = MT.PercenComponentType;
            }
            else
            {
                ddlComponentType1.Visible = false;       
            }
        }
        else
        {
            txtvalue.Text = Convert.ToString(MT.FixedValue);
            ddlComponentType1.Visible = false;
        }
        btnsubmit.Text = "Update";
    }
    #endregion
    #region Save/Update button click event for save/update component value information
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
            var data = from d in db.SalaryComponentTBs
                       where d.ComponentName == ddlComponentType.Text && d.ValueType != null
                       select d;
            if (data.Count() > 0)
            {
                g.ShowMessage(this.Page, "Component Name Already Exists.!!");
            }
            else
            {
                SalaryComponentTB MTB = db.SalaryComponentTBs.Where(d => d.ComponentName == (ddlComponentType.SelectedValue)).First();

                MTB.ComponentName = (ddlComponentType.Text);
                MTB.ComponentType = ddlCompany.SelectedValue.ToString();

                MTB.ValueType = rb_Value_Type.SelectedItem.Text;
                if (rb_Value_Type.SelectedIndex == 0)
                {
                    MTB.FixedValue = Convert.ToDecimal(txtvalue.Text);
                    MTB.PercentageValue = "0";
                }
                if (rb_Value_Type.SelectedIndex == 1)
                {
                    MTB.FixedValue = 0;
                    MTB.PercentageValue = (txtvalue.Text);
                    MTB.PercenComponentType = ddlComponentType1.Text;
                }
                db.SubmitChanges();
                g.ShowMessage(this.Page, "Component Value Details Saved Successfully");
                Clear();
            }
        }
        else
        {
            SalaryComponentTB MT = db.SalaryComponentTBs.Where(d => d.Componentid == Convert.ToInt32(lbldeptid.Text)).First();
            MT.ComponentName = (ddlComponentType.Text);
            MT.ComponentType = ddlCompany.SelectedValue.ToString();
            MT.PercentageValue = txtvalue.Text;
            MT.ComponentType = ddlCompany.SelectedValue.ToString();
            MT.ValueType = rb_Value_Type.SelectedItem.Text;
            if (rb_Value_Type.SelectedIndex == 0)
            {
                MT.FixedValue = Convert.ToDecimal(txtvalue.Text);
                MT.PercentageValue = "0";
            }
            if (rb_Value_Type.SelectedIndex == 1)
            {
                MT.FixedValue = 0;
                MT.PercentageValue = txtvalue.Text;
                MT.PercenComponentType = ddlComponentType1.Text;
            }
            db.SubmitChanges();
            g.ShowMessage(this.Page, "Component Value Details Saved Successfully");
            Clear();
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
    
    #region radio button page index changing event
    protected void rb_Value_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rb_Value_Type.SelectedIndex == 0)
        {
            ddlComponentType1.Visible = false;
        }
        if (rb_Value_Type.SelectedIndex == 1)
        {
            ddlComponentType1.Visible = true;
        }
    }
    #endregion
    #region Method for fetch and bind all components to gridview for display component list   
    public void BindAllcomponent()
    {
        var cptData = (from d in db.SalaryComponentTBs
                       where d.CompanyId==1 && d.TenantId==Convert.ToString(Session["TenantId"])
                       select d).Distinct();
        if (cptData.Count() > 0)
        {
            gvComponentList.DataSource = cptData;
            gvComponentList.DataBind();
        }
    }
    #endregion
    #region Method for fetch and bind components to dropdown
    public void BindCompDropDown()
    {
        var CompData = (from d in db.SalaryComponentTBs
                       select new
                       {
                           d.ComponentName,
                           d.Componentid
                       }).Distinct();
        if (CompData.Count() > 0)
        {
            ddlComponentType.DataSource = CompData;
            ddlComponentType.DataValueField = "ComponentName";
            ddlComponentType.DataTextField = "ComponentName";
            ddlComponentType.DataBind();
            ddlComponentType.Items.Insert(0, "--Select--");
        }
    }
    public void BindAllcomponentonDropDown()
    {
        ddlComponentType1.Items.Clear();
        var CompData = (from d in db.SalaryComponentTBs
                      select new 
                      {
                           d.ComponentName,d.Componentid
                      }).Distinct();
        if (CompData.Count() > 0)
        {
            ddlComponentType1.DataSource = CompData;
            ddlComponentType1.DataValueField = "Componentid";
            ddlComponentType1.DataTextField = "ComponentName";
            ddlComponentType1.DataBind();
            ddlComponentType1.Items.Insert(1, "Net Salary");
        }
    }
    #endregion
    #region Method for clear all controls on page
    public void Clear()
    {
        txtCompname.Text = null;
        lbldeptid.Text = "";
        txtvalue.Text = null;
        BindAllcomponent();
        MultiView1.ActiveViewIndex = 0;
        rb_Value_Type.SelectedIndex = 0;
        btnsubmit.Text = "Save";
    }
    #endregion

    protected void gvComponentList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvComponentList.Rows.Count > 0)
        {
            gvComponentList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvComponentList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}