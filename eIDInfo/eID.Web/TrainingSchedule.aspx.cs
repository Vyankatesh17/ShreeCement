using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// Page Craete by : Excellence IT Solutions Pvt Ltd.
/// Description: Page craete by the Add Training Schedule & Request Training Schedule From Employee detail and display on  Gridview.
/// Date : 17 Dec 2014 02:00PM Allocate Hour 3 hrs 30 min
/// Auther : Shivaji Gaikwad
///  

public partial class TrainingSchedule : System.Web.UI.Page
{
    #region Global Declaraction Varable.
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindRequstTraining();
                BindAllEmployee();
                BindScheduleTrainingList();
                txtTrainingDate.Attributes.Add("readonly", "readonly");
                txtStartDate.Attributes.Add("readonly", "readonly");
                txtEndDate.Attributes.Add("readonly", "readonly");
                txtTrainingDate.Text = g.GetCurrentDateTime().ToShortDateString();
                MultViewTraining.ActiveViewIndex = 0;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindScheduleTrainingList()
    {
        #region Bind All Training Schedule List From  HR.
        bool admin = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        if (admin)
        {
            var dt = (from p in HR.TrainingTBs
                      join k in HR.EmployeeTBs on p.EmpID equals k.EmployeeId
                      where p.RHApproval == 2
                      select new { p.TraingID, p.Trainer, p.ReqDate, p.Type, p.RHApproval, p.Title, p.CourseContent, p.Description, p.StartDate, p.EndDate, p.StartTime, p.EndTime, Name = k.FName + " " + k.MName + " " + k.Lname }).OrderByDescending(d => d.ReqDate);
            if (dt.Count() > 0)
            {

                grdScheduleTraining.DataSource = dt;
                grdScheduleTraining.DataBind();
                lbScheduleTrainingCnt.Text = dt.Count().ToString();
            }
            else
            {
                lbScheduleTrainingCnt.Text = "0";
                grdScheduleTraining.DataSource = null;
                grdScheduleTraining.DataBind();
            }

        }
        else
        {
            var dt = (from p in HR.TrainingTBs
                      join k in HR.EmployeeTBs on p.EmpID equals k.EmployeeId
                      where p.RHApproval == 2 && k.ReportingStatus == Convert.ToInt32(Session["UserId"])
                      select new { p.TraingID, p.Trainer, p.ReqDate, p.Type, p.RHApproval, p.Title, p.CourseContent, p.Description, p.StartDate, p.EndDate, p.StartTime, p.EndTime, Name = k.FName + " " + k.MName + " " + k.Lname }).OrderByDescending(d => d.ReqDate);
            if (dt.Count() > 0)
            {

                grdScheduleTraining.DataSource = dt;
                grdScheduleTraining.DataBind();
                lbScheduleTrainingCnt.Text = dt.Count().ToString();
            }
            else
            {

                lbScheduleTrainingCnt.Text = "0";
                grdScheduleTraining.DataSource = null;
                grdScheduleTraining.DataBind();
            }
        }
        #endregion
    }

    private void BindAllEmployee()
    {
        bool Status = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));

        if (Status == true)
        {

            var EmpData = (from d in HR.EmployeeTBs
                           join n in HR.MasterDeptTBs on d.DeptId equals n.DeptID
                           join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                           select new
                           {
                               Name = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                               Name1 = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                               d.FName,
                               d.Lname,
                               d.EmployeeId,
                               d.BirthDate,
                               d.Email,
                               DOJ1 = d.DOJ,
                               d.PanNo,
                               d.ContactNo,
                               d.PassportNo,
                               n.DeptName,
                               n.CompanyId,
                               c.CompanyName,
                               emnae = d.FName + " " + d.Lname,
                               d.RelivingDate
                           });



            if (EmpData.Count() > 0)
            {
                if (txtCompanyName.Text != "")
                {
                    EmpData = EmpData.Where(d => d.CompanyName == txtCompanyName.Text);
                }

                if (txtDepartment.Text != "")
                {
                    EmpData = EmpData.Where(d => d.DeptName == txtDepartment.Text);
                }

                grdEmployeeList.DataSource = EmpData;
                grdEmployeeList.DataBind();
                lblcnt.Text = EmpData.Count().ToString();
            }

        }
    }


    private void BindRequstTraining()
    {
        #region Bind All Training Schedule List From  Managar.
        bool admin = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        if (admin)
        {
            var dt = (from p in HR.TrainingTBs
                      join k in HR.EmployeeTBs on p.EmpID equals k.EmployeeId
                      where p.RHApproval == 1
                      select new { p.TraingID, p.Trainer, p.ReqDate, p.Type, p.RHApproval, p.Title, p.CourseContent, p.Description, Name = k.FName + " " + k.MName + " " + k.Lname }).OrderByDescending(d => d.ReqDate);
            if (dt.Count() > 0)
            {
                grdTrainingDetail.DataSource = dt;
                grdTrainingDetail.DataBind();
                lblcnt1.Text = dt.Count().ToString();
            }
            else
            {
                lblcnt1.Text = "0";
                grdTrainingDetail.DataSource = null;
                grdTrainingDetail.DataBind();
            }

        }
        else
        {
            var dt = (from p in HR.TrainingTBs
                      join k in HR.EmployeeTBs on p.EmpID equals k.EmployeeId
                      where p.RHApproval == 1 && k.ReportingStatus == Convert.ToInt32(Session["UserId"])
                      select new { p.TraingID, p.Trainer, p.ReqDate, p.Type, p.RHApproval, p.Title, p.CourseContent, p.Description, Name = k.FName + " " + k.MName + " " + k.Lname }).OrderByDescending(d => d.ReqDate);
            if (dt.Count() > 0)
            {

                grdTrainingDetail.DataSource = dt;
                grdTrainingDetail.DataBind();
                lblcnt1.Text = dt.Count().ToString();
            }
            else
            {
                lblcnt1.Text = "0";
                grdTrainingDetail.DataSource = null;
                grdTrainingDetail.DataBind();
            }
        }
        #endregion
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultViewTraining.ActiveViewIndex = 1;
        ClearAllControl();
    }

    private void ClearAllControl()
    {
        txtTrainingTitle.Text = txtCource.Text = txtRequiredReading.Text = txtRecommand.Text = txtTeacherName.Text = txtLocation.Text = txtStartDate.Text = txtEndDate.Text = txtFromTime.Text = txtToTime.Text = string.Empty;
    }
    protected void btnAddEmployee_Click(object sender, EventArgs e)
    {
        if (divEmployeeList.Visible == true)
        {
            divEmployeeList.Visible = false;
        }
        else
        {
            divEmployeeList.Visible = true;
        }
    }
    protected void grdEmployeeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEmployeeList.PageIndex = e.NewPageIndex;
        BindAllEmployee();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            #region
            TrainingTB TRAN = new TrainingTB();
            TRAN.ReqDate = Convert.ToDateTime(txtTrainingDate.Text);
            TRAN.Title = txtTrainingTitle.Text;
            TRAN.CourseContent = txtCource.Text;
            TRAN.RecommandedReading = txtRecommand.Text;
            TRAN.RequiredReading = txtRequiredReading.Text;
            TRAN.Trainer = txtTeacherName.Text;
            TRAN.Location = txtLocation.Text;
            TRAN.StartDate = Convert.ToDateTime(txtStartDate.Text);
            TRAN.EndDate = Convert.ToDateTime(txtEndDate.Text);
            TRAN.StartTime = txtFromTime.Text;
            TRAN.RHApproval = 2;
            TRAN.EmpID = Convert.ToInt32(Session["UserId"].ToString());
            TRAN.EndTime = txtToTime.Text;
            HR.TrainingTBs.InsertOnSubmit(TRAN);
            HR.SubmitChanges();
            grdEmployeeList.AllowPaging = false;
            for (int i = 0; i < grdEmployeeList.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdEmployeeList.Rows[i].FindControl("chkEmployee");
                if (chk.Checked == true)
                {
                    TrainingScheduleEmpoyeeTB TRANEMP = new TrainingScheduleEmpoyeeTB();
                    TRANEMP.TraingID = TRAN.TraingID;
                    TRANEMP.EmployeeId = int.Parse(chk.ToolTip);
                    HR.TrainingScheduleEmpoyeeTBs.InsertOnSubmit(TRANEMP);
                    HR.SubmitChanges();
                }
            }
            grdEmployeeList.AllowPaging = true;
            g.ShowMessage(this.Page, "Trainning Schedule Successfully");
            MultViewTraining.ActiveViewIndex = 0;
            btnSave.Text = "Save";
            BindRequstTraining();
            #endregion
        }
        if (btnSave.Text == "Update")
        {
            #region
            TrainingTB t = HR.TrainingTBs.Where(d => d.TraingID == int.Parse(lbTrainingId.Text)).First();
            t.Trainer = txtTeacherName.Text;
            t.Title = txtTrainingTitle.Text;
            t.CourseContent = txtCource.Text;
            t.RequiredReading = txtRequiredReading.Text;
            t.RecommandedReading = txtRequiredReading.Text;
            t.Trainer = txtTeacherName.Text;
            t.Location = txtLocation.Text;
            t.StartDate = Convert.ToDateTime(txtStartDate.Text);
            t.EndDate = Convert.ToDateTime(txtEndDate.Text);
            t.StartTime = txtFromTime.Text;
            t.EndTime = txtToTime.Text;
            t.RHApproval = 2;
            t.EmpID = Convert.ToInt32(Session["UserId"].ToString());
            HR.SubmitChanges();
            grdEmployeeList.AllowPaging = false;

            for (int i = 0; i < grdEmployeeList.Rows.Count; i++)
            {


                CheckBox chk = (CheckBox)grdEmployeeList.Rows[i].FindControl("chkEmployee");
                if (chk.Checked == true)
                {
                    TrainingScheduleEmpoyeeTB TRANEMP = new TrainingScheduleEmpoyeeTB();
                    TRANEMP.TraingID = Convert.ToInt32(lbTrainingId.Text);
                    TRANEMP.EmployeeId = int.Parse(chk.ToolTip);
                    HR.TrainingScheduleEmpoyeeTBs.InsertOnSubmit(TRANEMP);
                    HR.SubmitChanges();
                }
            }
            grdEmployeeList.AllowPaging = true;
            g.ShowMessage(this.Page, "Trainning Schedule Successfully");
            MultViewTraining.ActiveViewIndex = 0;
            txtTeacherName.Text = txtStartDate.Text = txtEndDate.Text = txtFromTime.Text = txtToTime.Text = string.Empty;

            BindRequstTraining();
            BindScheduleTrainingList();

            #endregion
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        MultViewTraining.ActiveViewIndex = 0;
    }
    protected void imgApprove_Click(object sender, ImageClickEventArgs e)
    {
        MultViewTraining.ActiveViewIndex = 1;
        ImageButton TrainingID = (ImageButton)sender;
        TrainingTB t = HR.TrainingTBs.Where(d => d.TraingID == int.Parse(TrainingID.CommandArgument)).First();
        txtTrainingTitle.Text = t.Title;
        lbTrainingId.Text = Convert.ToString(t.TraingID);
        txtTrainingTitle.ReadOnly = true;
        t.RHApproval = 1;
        HR.SubmitChanges();
        btnSave.Text = "Update";
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListCompany(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> company = (from d in hr.CompanyInfoTBs
                                  .Where(r => r.CompanyName.Contains(prefixText))
                                select d.CompanyName).Distinct().ToList();
        return company;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListDepartment(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> Department = (from d in hr.MasterDeptTBs
                                  .Where(r => r.DeptName.Contains(prefixText))
                                   select d.DeptName).Distinct().ToList();
        return Department;
    }

    protected void btnSerchEmployee_Click(object sender, EventArgs e)
    {
        BindAllEmployee();
    }
}