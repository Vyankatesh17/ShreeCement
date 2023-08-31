using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dash_presents : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["cId"]))
                {
                    BindPresentList();
                }
            }
        }
        if (gv.Rows.Count > 0)
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    private void BindPresentList()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                int cId = Convert.ToInt32(Request.QueryString["cId"]);
                //if (cId == 0)
                //{
                //    var companyinfo = db.CompanyInfoTBs.Where(a => a.TenantId == Session["TenantId"].ToString()).FirstOrDefault();
                //    cId = companyinfo.CompanyId;
                //}

                int companyid = Convert.ToInt32(Session["CompanyID"]);
                if (Session["UserType"].ToString() == "LocationAdmin")
                {
                    cId = companyid;
                }


                string query = "";
                if (cId == 138)
                {
                    query = string.Format(@"SELECT   DISTINCT     E.FName+' '+e.Lname AS EmpName, E.EmployeeNo , E.MachineID ,DAY(AL.ADate) AS Day, D.DeptName,DE.DesigName,
CONVERT(varchar, AL.ADate, 101) AS Date,  DATENAME(weekday, AL.ADate) AS DayofWeek,'Present' AS Status
FROM            DeviceLogsTB AS AL 
INNER JOIN EmployeeTB E ON AL.EmpID=E.EmployeeId
INNER JOIN MasterDeptTB D ON D.DeptID = E.DeptId
INNER JOIN MasterDesgTB DE ON DE.DesigID = E.DesgId
WHERE        E.IsActive=1 AND (CONVERT(date,AL.ADate)=CONVERT(date,'{0}')) AND AL.PunchStatus = 'In'  AND AL.TenantId='{1}' AND AL.CompanyId='{2}'", Request.QueryString["sDate"],
   Convert.ToString(Session["TenantId"]), cId);
                }
                else if(cId == 0)
                {
                    query = string.Format(@"SELECT   DISTINCT     E.FName+' '+e.Lname AS EmpName, E.EmployeeNo , E.MachineID ,DAY(AL.ADate) AS Day, D.DeptName,DE.DesigName,
CONVERT(varchar, AL.ADate, 101) AS Date,  DATENAME(weekday, AL.ADate) AS DayofWeek,'Present' AS Status
FROM            DeviceLogsTB AS AL 
INNER JOIN EmployeeTB E ON AL.EmpID=E.EmployeeId
INNER JOIN MasterDeptTB D ON D.DeptID = E.DeptId
INNER JOIN MasterDesgTB DE ON DE.DesigID = E.DesgId
WHERE      E.RelivingStatus = 0 AND E.IsActive=1 AND (CONVERT(date,AL.ADate)=CONVERT(date,'{0}'))  AND AL.TenantId='{1}'", Request.QueryString["sDate"],
  Convert.ToString(Session["TenantId"]));
                }
                else
                {
                    query = string.Format(@"SELECT   DISTINCT     E.FName+' '+e.Lname AS EmpName, E.EmployeeNo , E.MachineID ,DAY(AL.ADate) AS Day, D.DeptName,DE.DesigName,
CONVERT(varchar, AL.ADate, 101) AS Date,  DATENAME(weekday, AL.ADate) AS DayofWeek,'Present' AS Status
FROM            DeviceLogsTB AS AL 
INNER JOIN EmployeeTB E ON AL.EmpID=E.EmployeeId
INNER JOIN MasterDeptTB D ON D.DeptID = E.DeptId
INNER JOIN MasterDesgTB DE ON DE.DesigID = E.DesgId
WHERE     E.RelivingStatus = 0 AND E.IsActive=1 AND (CONVERT(date,AL.ADate)=CONVERT(date,'{0}'))  AND AL.TenantId='{1}' AND AL.CompanyId='{2}'", Request.QueryString["sDate"],
   Convert.ToString(Session["TenantId"]), cId);
                }

                DataTable dataTable = gen.ReturnData(query);
                gv.DataSource = dataTable;
                gv.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gv.Rows.Count > 0)
        {
            gv.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}