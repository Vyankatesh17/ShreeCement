using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TrainingAttendance : System.Web.UI.Page
{
    /// <summary>
    /// Developer Name : Manasi
    /// Training Attendance Page
    /// 17 Dec 14
    /// </summary>
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    int TrainingID = 0, EmpID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindAttendanceData();
                BindPendingData();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    #region METHODS
    private void BindPendingData()
    {
        bool admin = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        if (admin)
        {
            DataSet ds = g.ReturnData1(@"SELECT DISTINCT TrainingScheduleEmpoyeeTB.TraingID,   TrainingTB.Title, TrainingTB.CourseContent, TrainingTB.Description, TrainingTB.Type, TrainingTB.ReqDate
FROM            TrainingScheduleEmpoyeeTB INNER JOIN
                         TrainingTB ON TrainingScheduleEmpoyeeTB.TraingID = TrainingTB.TraingID
WHERE        (TrainingScheduleEmpoyeeTB.EmployeeId NOT IN (SELECT        EmpID FROM            TrainingAttendanceTB))");

            if (ds.Tables[0].Rows.Count>0)
            {
                grdTraining.DataSource = ds.Tables[0];
                grdTraining.DataBind();
                lblcnt.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                grdTraining.DataSource = ds.Tables[0];
                grdTraining.DataBind();
                lblcnt.Text = "0";

            }
          
        }
        else
        {
            DataSet ds = g.ReturnData1(@"SELECT DISTINCT TrainingScheduleEmpoyeeTB.TraingID,   TrainingTB.Title, TrainingTB.CourseContent, TrainingTB.Description, TrainingTB.Type, TrainingTB.ReqDate
FROM            TrainingScheduleEmpoyeeTB INNER JOIN
                         TrainingTB ON TrainingScheduleEmpoyeeTB.TraingID = TrainingTB.TraingID
WHERE        (TrainingScheduleEmpoyeeTB.EmployeeId NOT IN (SELECT distinct EmpID FROM   TrainingAttendanceTB)) and  TrainingScheduleEmpoyeeTB.EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdTraining.DataSource = ds.Tables[0];
                grdTraining.DataBind();
                lblcnt.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                grdTraining.DataSource = ds.Tables[0];
                grdTraining.DataBind();
                lblcnt.Text = "0";

            }
          
         
         
        }
    }
    private void BindAttendanceData()
    {
        bool admin = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        if (admin)
        {
            var dt = (from p in HR.TrainingScheduleEmpoyeeTBs
                      join s in HR.TrainingTBs on p.TraingID equals s.TraingID
                      join emp in HR.EmployeeTBs on p.EmployeeId equals emp.EmployeeId
                      join k in HR.TrainingAttendanceTBs on p.EmployeeId equals k.EmpID
                      where emp.RelivingStatus==null
                      select new
                      {
                          Name = emp.FName + " " + emp.MName + " " + emp.Lname,
                          s.ReqDate,
                          s.TraingID,
                          s.Type,
                          s.Title,
                          s.CourseContent,
                          s.Description,
                          s.Trainer,
                          s.Location,
                          s.StartTime,
                          s.EndTime,
                          k.Status
                      }).Distinct();
            if (dt.Count() > 0)
            {
                grdAttend.DataSource = dt;
                grdAttend.DataBind();
                lblTotalCnt.Text = dt.Count().ToString();
            }
            else
            {
                lblTotalCnt.Text = "0";
                grdAttend.DataSource = null;
                grdAttend.DataBind();
            }
        }
        else
        {
            var dt = (from p in HR.TrainingScheduleEmpoyeeTBs
                      join s in HR.TrainingTBs on p.TraingID equals s.TraingID
                      join emp in HR.EmployeeTBs on p.EmployeeId equals emp.EmployeeId
                      join k in HR.TrainingAttendanceTBs on p.EmployeeId equals k.EmpID
                      where k.EmpID == Convert.ToInt32(Session["UserId"]) 
                      && emp.RelivingStatus == null
                      select new
                      {
                          Name = emp.FName + " " + emp.MName + " " + emp.Lname,
                          s.ReqDate,
                          s.TraingID,
                          s.Type,
                          s.Title,
                          s.CourseContent,
                          s.Description,
                          s.Trainer,
                          s.Location,
                          s.StartTime,
                          s.EndTime,
                          k.Status
                      }).Distinct();

            if (dt.Count() > 0)
            {
                grdAttend.DataSource = dt;
                grdAttend.DataBind();
                lblTotalCnt.Text = dt.Count().ToString();
            }
            else
            {
                lblTotalCnt.Text = "0";
                grdAttend.DataSource = null;
                grdAttend.DataBind();
            }
        }
    }
    private void BindAllEmp()
    {
        var dt = from p in HR.TrainingScheduleEmpoyeeTBs
                 join s in HR.TrainingTBs on p.TraingID equals s.TraingID
                 join k in HR.EmployeeTBs on p.EmployeeId equals k.EmployeeId
                 where p.TraingID == TrainingID
                 select new { s.ReqDate, s.TraingID, s.Type, s.Title, s.CourseContent, p.TrainingScheduleId, s.Description, p.EmployeeId, Name = k.FName + " " + k.MName + " " + k.Lname };
        if (dt.Count() > 0)
        {
            ViewState["Date"] = dt.First().ReqDate.ToString();
            grdEMP.DataSource = dt;
            grdEMP.DataBind();

        }
        else
        {
            grdTraining.DataSource = null;
            grdTraining.DataBind();
        }
    }
    #endregion
    #region EVENTS
    protected void imgatt_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        TrainingID = int.Parse(btn.CommandArgument);
        ViewState["TrainingID"] = int.Parse(btn.CommandArgument);
        divmod.Style.Add("display", "Block");
        BindAllEmp();
        modpop.Show();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TrainingID = int.Parse(ViewState["TrainingID"].ToString());

        var dtExists = from p in HR.TrainingScheduleEmpoyeeTBs

                       join s in HR.TrainingTBs on p.TraingID equals s.TraingID
                       join h in HR.TrainingAttendanceTBs on s.TraingID equals h.TrainingID
                       join k in HR.EmployeeTBs on p.EmployeeId equals k.EmployeeId
                       where p.TraingID == TrainingID
                       && s.ReqDate == Convert.ToDateTime(ViewState["Date"].ToString())
                       select new { s.ReqDate, s.EmpID };
        if (dtExists.Count() > 0)
        {
            g.ShowMessage(this.Page, "Training Attendance Already Done....!!");
        }
        else
        {//SAVE

            for (int i = 0; i < grdEMP.Rows.Count; i++)
            {

                var dt = from p in HR.TrainingScheduleEmpoyeeTBs
                         join s in HR.TrainingTBs on p.TraingID equals s.TraingID
                         join k in HR.EmployeeTBs on p.EmployeeId equals k.EmployeeId
                         where p.TraingID == TrainingID && k.FName + ' ' + k.MName + ' ' + k.Lname == grdEMP.Rows[i].Cells[3].Text
                         select new { p.TraingID, p.EmployeeId };
                DropDownList ddlStatus = (DropDownList)grdEMP.Rows[i].FindControl("ddlStatus");
                TrainingAttendanceTB TB = new TrainingAttendanceTB();
                TB.Date = Convert.ToDateTime(grdEMP.Rows[i].Cells[1].Text);
                TB.EmpID = Convert.ToInt32(dt.First().EmployeeId);
                //TB.EmpID = Convert.ToInt32(grdEMP.Rows[i].Cells[4].Text);
                TB.Status = ddlStatus.SelectedItem.Text;
                TB.TrainingID = TrainingID;
                // TB.ScheduleID = SchID;
                HR.TrainingAttendanceTBs.InsertOnSubmit(TB);
                HR.SubmitChanges();
            }
            g.ShowMessage(this.Page, "Data Saved Successfully");
            BindPendingData();
            BindAttendanceData();

        }


    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingAttendance.aspx");
    }
    protected void grdTraining_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTraining.PageIndex = e.NewPageIndex;
        BindPendingData();
       
    }
    protected void grdAttend_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttend.PageIndex = e.NewPageIndex;
        BindAttendanceData();
    }
    protected void btnclose_Click(object sender, ImageClickEventArgs e)
    {
        modpop.Hide();
    }
    protected void grdEMP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        divmod.Style.Add("display", "Block");
        TrainingID = int.Parse(ViewState["TrainingID"].ToString());
        grdEMP.PageIndex = e.NewPageIndex;
        BindAllEmp();
        modpop.Show();
    }
    #endregion
}