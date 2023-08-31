using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_holidays : System.Web.UI.Page
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
                BindCompanyList();
                BindDataGridView();
            }
        }
        if (gvList.Rows.Count > 0)
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        BindDataGridView();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try {
            BindDataGridView();
        }
        catch(Exception ex) { }
    }
    private void BindDataGridView()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            var data = (from d in db.HoliDaysMasters
                        where d.Status==1
                        && d.TenantId==Convert.ToString(Session["TenantId"])
                        select new
                        {
                            d.CompanyId,
                            d.HolidaysID,
                            d.Date,
                            d.HoliDaysName,
                            d.HolidayType
                        }).Distinct();

            if (cId > 0)
            {
                data = data.Where(d => d.CompanyId == cId).Distinct();
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
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvList.Rows.Count > 0)
        {
            gvList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvList.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }
    
}