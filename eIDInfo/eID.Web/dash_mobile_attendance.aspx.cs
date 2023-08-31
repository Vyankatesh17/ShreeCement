using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class dash_mobile_attendance : System.Web.UI.Page
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
                if (!string.IsNullOrEmpty(Request.QueryString["cId"]))
                    BindDataGridView();
            }
        }
        BindJqFunctions();
    }
    
    private void BindDataGridView()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int cId = Convert.ToInt32(Request.QueryString["cId"]);
            DateTime dateTime = Convert.ToDateTime(Request.QueryString["sDate"]);
            var data = (from d in db.AppAttendanceTBs
                        join e in db.EmployeeTBs on d.EmpId equals e.EmployeeId
                        join c in db.CompanyInfoTBs on e.CompanyId equals c.CompanyId
                        where e.IsActive == true && e.TenantId==Convert.ToString(Session["TenantId"])&& d.PunchDate.Value.Date == dateTime
                        select new
                        {
                            e.CompanyId,
                            EmpName = e.FName + " " + e.Lname,
                            e.EmployeeNo,
                            e.EmployeeId,
                            e.MachineID,
                            Date = d.PunchDate.Value,
                            d.PunchType,
                            Time = d.PunchTime.Value,
                            d.Location,
                            d.Longitude,
                            d.Latitude,
                            Company=c.CompanyName
                        }).Distinct();

            if (cId > 0)
            {
                data = data.Where(d => d.CompanyId == cId).Distinct();
            }
            gvList.DataSource = data;
            gvList.DataBind();

        }
    }
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvList.Rows.Count > 0)
        {
            gvList.UseAccessibleHeader = true;
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}