using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class LeaveDetails : System.Web.UI.Page
{  

    #region Vaiable Declaration
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    #endregion

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

    }
    public void BindLeaveDetails()
    {//Admin Data
        bool AdminStatus = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        if (AdminStatus == true)
        {
            DataSet dsLeaveData = g.ReturnData1(@"SELECT DISTINCT 
                         EmployeeTB.FName + ' ' + EmployeeTB.MName + ' ' + EmployeeTB.Lname AS Name, tblLeaveApplication.LeaveApllicationDate, tblLeaveApplication.StartDate, 
                         tblLeaveApplication.EndDate, tblLeaveApplication.Duration, LeaveAppTB.ApproveDays AprovedDays, tblLeaveApplication.Purpose, tblLeaveApplication.HRDirectApprove, 
                         masterLeavesTB.LeaveName, LeaveAppTB.ManagerStatus  , LeaveAppTB.HRStatus  
FROM            tblLeaveApplication INNER JOIN
                         LeaveAppTB ON tblLeaveApplication.LeaveApplicationID = LeaveAppTB.LeaveAppID LEFT OUTER JOIN
                         masterLeavesTB ON tblLeaveApplication.LeaveID = masterLeavesTB.LeaveID LEFT OUTER JOIN
                         EmployeeTB ON tblLeaveApplication.employeeID = EmployeeTB.EmployeeId");
//             DataSet dsLeaveData = g.ReturnData1(@"SELECT distinct  EmployeeTB.FName+' '+ EmployeeTB.MName+' '+EmployeeTB.Lname Name,
// tblLeaveApplication.LeaveApllicationDate, tblLeaveApplication.StartDate, tblLeaveApplication.EndDate, tblLeaveApplication.Duration, tblLeaveApplication.AprovedDays, tblLeaveApplication.Purpose, tblLeaveApplication.approval, 
//                         tblLeaveApplication.ManagerStatus, tblLeaveApplication.HRDirectApprove,masterLeavesTB.LeaveName FROM            tblLeaveApplication LEFT OUTER JOIN
//                         masterLeavesTB ON tblLeaveApplication.LeaveID = masterLeavesTB.LeaveID LEFT OUTER JOIN
//                         EmployeeTB ON tblLeaveApplication.employeeID = EmployeeTB.EmployeeId");
            
            
            
            if (dsLeaveData.Tables[0].Rows.Count > 0)
            {
                grd_EmpLeave.DataSource = dsLeaveData.Tables[0];
                grd_EmpLeave.DataBind();
            }
            else
            {
                
                grd_EmpLeave.DataSource = null;
                grd_EmpLeave.DataBind();
            }

        }
        else
        {//Employee Wise
            DataSet dsLeaveData = g.ReturnData1(@"SELECT DISTINCT 
                         EmployeeTB.FName + ' ' + EmployeeTB.MName + ' ' + EmployeeTB.Lname AS Name, tblLeaveApplication.LeaveApllicationDate, tblLeaveApplication.StartDate, 
                         tblLeaveApplication.EndDate, tblLeaveApplication.Duration, LeaveAppTB.ApproveDays AprovedDays, tblLeaveApplication.Purpose, tblLeaveApplication.HRDirectApprove, 
                         masterLeavesTB.LeaveName, LeaveAppTB.ManagerStatus, LeaveAppTB.HRStatus
FROM            tblLeaveApplication INNER JOIN
                         LeaveAppTB ON tblLeaveApplication.LeaveApplicationID = LeaveAppTB.LeaveAppID LEFT OUTER JOIN
                         masterLeavesTB ON tblLeaveApplication.LeaveID = masterLeavesTB.LeaveID LEFT OUTER JOIN
                         EmployeeTB ON tblLeaveApplication.employeeID = EmployeeTB.EmployeeId  where tblLeaveApplication.EmployeeID='" + Convert.ToInt32(Session["UserId"]) + "'");
            if (dsLeaveData.Tables[0].Rows.Count > 0)
            {
                grd_EmpLeave.DataSource = dsLeaveData.Tables[0];
                grd_EmpLeave.DataBind();
            }
            else
            {
                grd_EmpLeave.DataSource = null;
                grd_EmpLeave.DataBind();
            }
        }
    }

    protected void grd_EmpLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {//Page Index for Grid
        grd_EmpLeave.PageIndex = e.NewPageIndex;
        BindLeaveDetails();
        grd_EmpLeave.DataBind();
    }

}