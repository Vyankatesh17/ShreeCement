using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report_device_punches : System.Web.UI.Page
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
                ddlMonth.SelectedIndex = DateTime.Now.Month;
                BindYear();
                BindCompanyList();
               
                BindSimpleAttendance();

                if (Session["UserType"].ToString() != "User")
                {
                    company.Visible = true;                   
                }
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
            BindSimpleAttendance();
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
    private void BindSimpleAttendance()
    {
        try
        {
           
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {

                DateTime startDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                int days = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                //var WeekOffList = (from d in db.WeeklyOffTBs where d.Effectdate >= startDate.Date select d).Distinct();
                //foreach (var wo in WeekOffList)
                //{

                //}


                string query = string.Format(@"SELECT        LogId, DeviceAccountId, DownloadDate, AttendDate, PunchStatus, EmpID, AccessCardNo, ADate, ATime, CompanyId, TenantId
FROM            DeviceLogsTB 
WHERE        (MONTH(AttendDate) = {0}) AND (YEAR(AttendDate) = {1}) AND TenantId='{2}'", ddlMonth.SelectedIndex, ddlYear.SelectedValue,
Convert.ToString(Session["TenantId"]));

              

                if (ddlCompany.SelectedIndex > 0)
                {
                    query += " AND CompanyId=" + ddlCompany.SelectedValue;
                }
                if (Session["UserType"].ToString() == "User")
                {
                    query += " AND EmpID=" +  Convert.ToInt32(Session["EmpId"]);
                }
                DataTable data = gen.ReturnData(query);

                gvAttendance.DataSource = data;
                gvAttendance.DataBind();

                
            }
        }
        catch (Exception ex)
        {
            
        }
    }
}