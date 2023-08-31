using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApproveTraining : System.Web.UI.Page
{ 
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindGridData();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    private void BindGridData()
    {
        //var data = from d in HR.EmployeeTBs
        //           where d.EmployeeId == Convert.ToInt32(Session["UserId"])
        //           select new { d.ReportingStatus, d.DeptId };
        bool admin = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        if (admin)
        {
            var dt = (from p in HR.TrainingTBs
                      join k in HR.EmployeeTBs on p.EmpID equals k.EmployeeId
                      where p.RHApproval == 0  
                      select new { p.TraingID, p.ReqDate, p.Type, p.RHApproval, p.Title, p.CourseContent, p.Description, Name = k.FName + " " + k.MName + " " + k.Lname }).OrderByDescending(d => d.ReqDate);
            if (dt.Count() > 0)
            {
                grdTraining.DataSource = dt;
                grdTraining.DataBind();
                lblcnt.Text = dt.Count().ToString();
            }
            else
            {
                lblcnt.Text = "0";
                grdTraining.DataSource = null;
                grdTraining.DataBind();
            }

        }
        else
        {
            var dt = (from p in HR.TrainingTBs
                      join k in HR.EmployeeTBs on p.EmpID equals k.EmployeeId
                      where p.RHApproval == 0 && k.ReportingStatus == Convert.ToInt32(Session["UserId"])
                      select new { p.TraingID, p.ReqDate, p.Type, p.RHApproval, p.Title, p.CourseContent, p.Description, Name = k.FName + " " + k.MName + " " + k.Lname }).OrderByDescending(d => d.ReqDate);
            if (dt.Count() > 0)
            {
                grdTraining.DataSource = dt;
                grdTraining.DataBind();
                lblcnt.Text = dt.Count().ToString();
            }
            else
            {
                lblcnt.Text = "0";
                grdTraining.DataSource = null;
                grdTraining.DataBind();
            }
        }
        
    }
    protected void OnClick_Edit(object sender, EventArgs e)
    {//Approve
        ImageButton TrainingID = (ImageButton)sender;
        TrainingTB t= HR.TrainingTBs.Where(d=>d.TraingID==int.Parse(TrainingID.CommandArgument)).First();
        t.RHApproval = 1;
        HR.SubmitChanges();
        g.ShowMessage(this.Page, "Training Details Approved Successfully...!!"); BindGridData();
    }
}