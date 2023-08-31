using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dash_leave_report : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["cId"]))

                    BindLeaveSummaryDetails();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        BindJqFunctions();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindLeaveSummaryDetails();
    }
 
    private void BindLeaveSummaryDetails()
    {

        try
        {
            int cId = Convert.ToInt32(Request.QueryString["cId"]);
            string query = @"SELECT        L.LeaveTypeId, L.ApplicationDate, L.LeaveReason, L.LeaveStartDate, L.LeaveEndDate, L.StartHalf, L.EndHalf, L.Duration, L.ManagerStatus, L.HrStatus, L.IsLossofPay, L.ManRemark, L.HrRemark, L.ApproveDate, M.LeaveName,
                          E.EmployeeNo, E.FName+' '+E.Lname AS EmpName
FROM            LeaveApplicationsTB AS L INNER JOIN
                         EmployeeTB AS E ON L.EmployeeId = E.EmployeeId LEFT OUTER JOIN
                         masterLeavesTB AS M ON L.LeaveTypeId = M.LeaveID
						 WHERE E.IsActive=1 AND E.TenantId='"+Convert.ToString(Session["TenantId"])+"' AND E.CompanyId='"+cId+"' AND (CONVERT(date,'"+Request.QueryString["sDate"]+"') BETWEEN CONVERT(date,LeaveStartDate) AND CONVERT(date,LeaveEndDate)) ";

           
            DataTable data = gen.ReturnData(query);

            gvDataList.DataSource = data;
            gvDataList.DataBind();
        }
        catch(Exception ex) { }
      
    }
  
    protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    
}