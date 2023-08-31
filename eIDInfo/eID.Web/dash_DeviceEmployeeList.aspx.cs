using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dash_DeviceEmployeeList : System.Web.UI.Page
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
                int companyid = Convert.ToInt32(Session["CompanyID"]);
                if (Session["UserType"].ToString() == "LocationAdmin")
                {
                    cId = companyid;
                }
                string query = "";
                if (cId == 0)
                {
                    query = string.Format(@"SELECT DISTINCT     E.FName+' '+e.Lname AS EmpName, E.EmployeeNo , E.MachineID ,'Absent' AS Status
FROM           EmployeeTB E 
WHERE     E.IsActive=1 AND   E.TenantId='{1}' AND E.RelivingStatus = 0  AND EmployeeId Not IN(select Distinct EmpId from DeviceLogsTB WHERE CONVERT(date,ADate)=CONVERT(date,'{0}') AND TenantId='{1}')", Request.QueryString["sDate"],
  Convert.ToString(Session["TenantId"]));
                }
                else
                {
                    query = string.Format(@"SELECT DISTINCT     E.FName+' '+e.Lname AS EmpName, E.EmployeeNo , E.MachineID ,'Absent' AS Status
FROM           EmployeeTB E 
WHERE     E.IsActive=1 AND   E.TenantId='{1}'AND E.RelivingStatus = 0 AND E.CompanyId='{2}' AND EmployeeId Not IN(select Distinct EmpId from DeviceLogsTB WHERE CONVERT(date,ADate)=CONVERT(date,'{0}') and CompanyId='{2}' AND TenantId='{1}')", Request.QueryString["sDate"],
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