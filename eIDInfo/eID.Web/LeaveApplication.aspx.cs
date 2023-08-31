using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LeaveApplication : System.Web.UI.Page
{
    #region Variable Declaration
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    int totalSunday = 0, TotalHolidays = 0, TotalSaturDays = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
              
                txtStartDate.Attributes.Add("Readonly", "Readonly");
                txtEndDate.Attributes.Add("Readonly", "Readonly");
                fillpendingLeaves();
                FillLeaveType();
                if (grd_Leavependings.Rows.Count == 0)
                {
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    public void FillLeaveType()
    {//fill Leave Type
        var LeaveTypeData = (from d in HR.masterLeavesTBs
                             join k in HR.LeaveAllocationTBs on d.LeaveID equals k.LeaveID
                             where d.Status == 0
                             select new
                             {
                                 d.LeaveID,
                                 d.LeaveName
                             }).Distinct();
        if (LeaveTypeData.Count() > 0)
        {
            ddlLeaveType.DataSource = LeaveTypeData;
            ddlLeaveType.DataTextField = "LeaveName";
            ddlLeaveType.DataValueField = "LeaveID";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0,new ListItem("Loss of Pay", "0"));
        }
        else
        {
            ddlLeaveType.Items.Clear();
            ddlLeaveType.Items.Insert(0, new ListItem("Loss of Pay", "0"));
        }

    }


    private void fillpendingLeaves()
    { // For Binding pending Leaves to Grid
        int empid =  Convert.ToInt32(Session["UserId"]);
        var pendingLeaves = (from m in HR.LeaveAllocationTBs
                             join n in HR.masterLeavesTBs on m.LeaveID equals n.LeaveID
                             where m.EmployeeID == empid
                             && ((DateTime)m.FromDateAllocation).Year == DateTime.Now.Year
                              && ((DateTime)m.ToDateAllocation).Year == DateTime.Now.Year
                              && m.PendingLeaves != 0
                             select new
                             {
                                 LeaveName = n.LeaveName,
                                 m.LeaveID,
                                 LeaveBalance = m.PendingLeaves,
                                 EligibleLeaves = m.TotalAllocatedLeaves,
                                 TakenLeaves = m.TotalAllocatedLeaves - m.PendingLeaves
                             }).Distinct();
        if (pendingLeaves.Count() > 0)
        {
            grd_Leavependings.DataSource = pendingLeaves;
            grd_Leavependings.DataBind();
        }
        else
        {
            grd_Leavependings.DataSource = null;
            grd_Leavependings.DataBind();
            btnSubmit.Enabled = false;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //CHeck Exists...
        int empid = Convert.ToInt32(Session["UserId"].ToString());
        var ExistData = from d in HR.tblLeaveApplications
                        where d.StartDate == Convert.ToDateTime(txtStartDate.Text)
                            && d.EndDate == Convert.ToDateTime(txtEndDate.Text)
                            && d.employeeID == empid
                        select d;
        if (ExistData.Count() == 0)
        {
            LeaveApplicationsTB leaveApplicationsTB = new LeaveApplicationsTB();
            leaveApplicationsTB.AddedBy = empid;
            leaveApplicationsTB.AddedDate = g.GetCurrentDateTime();
            leaveApplicationsTB.ApplicationDate = g.GetCurrentDateTime();
            leaveApplicationsTB.Duration= Convert.ToInt32(txtDuration.Text);
            leaveApplicationsTB.EmployeeId = empid;
            leaveApplicationsTB.EndHalf = rbToFirst.Checked ? "First" : "Second";
            leaveApplicationsTB.HrStatus = "Pending";
            leaveApplicationsTB.LeaveEndDate = Convert.ToDateTime(txtEndDate.Text);
            leaveApplicationsTB.LeaveReason = txtPurpose.Text;
            leaveApplicationsTB.LeaveStartDate = Convert.ToDateTime(txtStartDate.Text);
            leaveApplicationsTB.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveApplicationsTB.ManagerStatus = "Pending";
            leaveApplicationsTB.StartHalf = rbFromFirst.Checked ? "First" : "Second";
            leaveApplicationsTB.IsLossofPay = ddlLeaveType.SelectedIndex == 0 ? 1 : 0;//
            
            HR.LeaveApplicationsTBs.InsertOnSubmit(leaveApplicationsTB);
            HR.SubmitChanges();

           


            LeaveAppTB app = new LeaveAppTB();
            app.LeaveAppID = leaveApplicationsTB.LeaveApplicationId;
            app.HRStatus = "Pending";
            app.ManagerStatus = "Pending";
            app.ApproveDays =0;
            HR.LeaveAppTBs.InsertOnSubmit(app);
            HR.SubmitChanges();

            clearFields();
            g.ShowMessage(this.Page, "Leave applied successfully..");

        }
        else
        {
            g.ShowMessage(this.Page, "Leave Application Details Already Sent for This Period...");
        }
    }

    private void clearFields()
    {
        ClearInputs(Page.Controls);
      
    }

    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();

            ClearInputs(ctrl.Controls);
        }
    }
    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        CalculateDays();

        double applyDays = Convert.ToDouble(txtDuration.Text);
        double lDays = GetPendingLeaves();
        if (applyDays > lDays && ddlLeaveType.SelectedIndex>0)
        {
            g.ShowMessage(this.Page, "Leave Balance Not Enough for Selected Leave Name..");
            btnSubmit.Enabled = false;
        }
        else if (ddlLeaveType.SelectedIndex == 0)
        {
            g.ShowMessage(this.Page, "Leave will be apply under Loss of Pay criteria");
            btnSubmit.Enabled = true;
        }
        else
        {
            btnSubmit.Enabled = true;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();
    }

    private string DayName(string DayName)
    {
        DataSet ds = g.ReturnData1(@"Declare @startdate datetime, @enddate datetime set @startdate = '" + txtStartDate.Text + "' set @EndDate = '" + txtEndDate.Text + "' ;with mycte as (select  @startdate DateValue union all select DateValue + 1 from mycte where DateValue + 1 <= @enddate) SELECT COUNT(*) as NumOfSunday FROM mycte WHERE DATENAME(weekday,dateValue)='" + DayName + "'");
        //if (ds.Tables[0].Rows.Count>0)
        //{
        return ds.Tables[0].Rows[0][0].ToString();

    }
    private void CalculateDays()
    {
        DateTime enddateTime, startdateTime;
        DateTime.TryParse(txtStartDate.Text, out startdateTime);
        if (DateTime.TryParse(txtEndDate.Text, out enddateTime))
        {
            if (startdateTime > enddateTime)
            {
                g.ShowMessage(this.Page, "start date should be less than end date.");
            }
            else
            {
                btnSubmit.Enabled = true;

                double days = (enddateTime - startdateTime).TotalDays;

                if (rbFromFirst.Checked && rbToFirst.Checked)
                {
                    days = days + (0.5);
                }
                else if (rbFromFirst.Checked && rbToSecond.Checked)
                {
                    days = days + 1;
                }
                else if (rbFromSecond.Checked && rbToFirst.Checked) { days = days + (0.5); }
                else if (rbFromSecond.Checked && rbToSecond.Checked)
                {
                    days = days + (.50);
                }
                txtDuration.Text = days.ToString();
            }


        }
        else
        {
            g.ShowMessage(this.Page, "Unable to parse the specified date.");
        }

    }

    protected void rbToFirst_CheckedChanged(object sender, EventArgs e)
    {
        CalculateDays();
    }

    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        double pLeaves = GetPendingLeaves();
        if (pLeaves > 0)
        {
            btnSubmit.Enabled = true;
        }
        else if (ddlLeaveType.SelectedIndex == 0)
        {
            btnSubmit.Enabled = true;
        }
        else { btnSubmit.Enabled = false; }
    }
    private double GetPendingLeaves()
    {
        int empid = Convert.ToInt32(Session["UserId"]);
        double pendingLeaves = 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.LeaveAllocationTBs where d.LeaveID == Convert.ToInt32(ddlLeaveType.SelectedValue) && d.EmployeeID == empid select d).FirstOrDefault();
            pendingLeaves = data == null ? 0 : data.PendingLeaves.Value;

        }

        return pendingLeaves;
    }
}