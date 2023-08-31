using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report_api_monitor : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (gvAttendance.Rows.Count > 0)
        {
            gvAttendance.UseAccessibleHeader = true;
            gvAttendance.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
               
                BindApiTransactions();
                
            }
            BindJqFunctions();
        }
    }

    protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvAttendance.Rows.Count > 0)
        {
            gvAttendance.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvAttendance.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindApiTransactions();
        }
        catch (Exception ex) { }
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
    
    private void BindApiTransactions()
    {
        try
        {
           
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {

                var data = (from d in db.MstApiAuditTBs
                            where d.TenantId==Convert.ToString(Session["TenantId"])
                            select new
                            {
                                d.Api,
                                d.ApiAction,
                                d.AuditDate,
                                d.AuditId,
                                d.CompanyId,
                                d.Description,
                                d.ExecuteBy,
                                d.ExecutePage,
                                d.FullApi,
                                d.Status,
                                d.TenantId
                            }).Distinct();

                if (ddlCompany.SelectedIndex > 0)
                {
                    data = data.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).Distinct();
                }
             
                gvAttendance.DataSource = data;
                gvAttendance.DataBind();

                
            }
        }
        catch (Exception ex)
        {
            
        }
    }
}