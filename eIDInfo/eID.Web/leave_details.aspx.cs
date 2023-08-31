using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class leave_details : System.Web.UI.Page
{  
    #region Vaiable Declaration
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    #endregion
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
                BindLeaveDetails();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (grd_EmpLeave.Rows.Count > 0)
            grd_EmpLeave.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }
    public void BindLeaveDetails()
    {//Admin Data
        bool AdminStatus =Session["UserType"].ToString()=="User"?false:true;
        string query = string.Format(@"SELECT        LA.LeaveApplicationId, LA.ApplicationDate, E.FName + ' ' + E.Lname AS EmpName, CASE WHEN LA.IsLossofPay = 1 THEN 'LOP' ELSE ML.LeaveName END AS LeaveName, ML.LeaveTypeSName, LA.LeaveStartDate, LA.LeaveEndDate,
                          LA.Duration, LA.LeaveReason, LA.ManagerStatus, LA.HrStatus
FROM            LeaveApplicationsTB AS LA LEFT OUTER JOIN
                         masterLeavesTB AS ML ON LA.LeaveTypeId = ML.LeaveID INNER JOIN
                         EmployeeTB AS E ON LA.EmployeeId = E.EmployeeId
WHERE     E.IsActive =1 AND   (LA.TenantId = '{0}')", Convert.ToString(Session["TenantId"]));
        if (AdminStatus == false)
        {
            query += " AND LA.EmployeeId=" + Convert.ToInt32(Session["EmpId"]);
        }
        DataTable dataTable = g.ReturnData(query);

        grd_EmpLeave.DataSource = dataTable;
        grd_EmpLeave.DataBind();
    }

    protected void grd_EmpLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {//Page Index for Grid
        grd_EmpLeave.PageIndex = e.NewPageIndex;
        BindLeaveDetails();
        grd_EmpLeave.DataBind();
    }


    protected void grd_EmpLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_EmpLeave.Rows.Count > 0)
        {
            grd_EmpLeave.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_EmpLeave.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void lbtnEvent_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
       

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            LeaveApplicationsTB outDoorEntryTB = db.LeaveApplicationsTBs.Where(d => d.LeaveApplicationId == Convert.ToInt32(linkButton.CommandArgument)).FirstOrDefault();
            db.LeaveApplicationsTBs.DeleteOnSubmit(outDoorEntryTB);
            g.ShowMessage(this.Page, "Leave Entry deleted..");
            BindLeaveDetails();
        }
    }
}