using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Recruitment_InterviewResult : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                bindgrid();
                txtdate.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void bindgrid()
    {
        bool admin = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        DataTable dtgrid = new DataTable();
        if (admin == true)
        {
            if (ddlsortby.SelectedIndex==0)
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid  where  Candidate_ID not in(select CandidateId from InterviewResultTB where InterviewResultTB.InterviewStatus='Selected' or InterviewResultTB.InterviewStatus='Rejected' or InterviewResultTB.InterviewStatus='Offer' or InterviewResultTB.InterviewStatus='Offer Rejected' or InterviewResultTB.InterviewStatus='Join')");
            }

            if (ddlsortby.SelectedIndex ==1 && txtcompanywise.Text !="")
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid  where  Candidate_ID not in(select CandidateId from InterviewResultTB where InterviewResultTB.InterviewStatus='Selected' or InterviewResultTB.InterviewStatus='Rejected' or InterviewResultTB.InterviewStatus='Offer' or InterviewResultTB.InterviewStatus='Offer Rejected' or InterviewResultTB.InterviewStatus='Join') And ST.CompanyId='"+Convert.ToInt32(lblcompID.Text)+"'");
            }
            if (ddlsortby.SelectedIndex == 2 && txtpositionwise.Text != "")
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid  where  Candidate_ID not in(select CandidateId from InterviewResultTB where InterviewResultTB.InterviewStatus='Selected' or InterviewResultTB.InterviewStatus='Rejected' or InterviewResultTB.InterviewStatus='Offer' or InterviewResultTB.InterviewStatus='Offer Rejected' or InterviewResultTB.InterviewStatus='Join') And VancancyTB.Title='" +txtpositionwise.Text +"'");
            }
            if (ddlsortby.SelectedIndex == 3 && txtcandidatewise.Text != "")
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid  where  Candidate_ID not in(select CandidateId from InterviewResultTB where InterviewResultTB.InterviewStatus='Selected' or InterviewResultTB.InterviewStatus='Rejected' or InterviewResultTB.InterviewStatus='Offer' or InterviewResultTB.InterviewStatus='Offer Rejected' or InterviewResultTB.InterviewStatus='Join') And CandidateTB.Name='" +txtcandidatewise.Text +"'");
            }


            if (dtgrid.Rows.Count > 0)
            {
                grdschedule.DataSource = dtgrid;
                grdschedule.DataBind();
                lblcount.Text = dtgrid.Rows.Count.ToString();
            }
            else
            {
                grdschedule.DataSource = null;
                grdschedule.DataBind();
                lblcount.Text = "0";
            }
        }
        else
        {
            if (ddlsortby.SelectedIndex == 0)
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid  where ScheduleDetailsTB.ScheduleId in (Select ScheduleId from SchedulePanelTB where EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "') and Candidate_ID not in(select CandidateId from InterviewResultTB where InterviewResultTB.InterviewStatus='Selected' or InterviewResultTB.InterviewStatus='Rejected' or InterviewResultTB.InterviewStatus='Offer' or InterviewResultTB.InterviewStatus='Offer Rejected' or InterviewResultTB.InterviewStatus='Join')");
            }

            if (ddlsortby.SelectedIndex == 1 && txtcompanywise.Text != "")
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid  where ScheduleDetailsTB.ScheduleId in (Select ScheduleId from SchedulePanelTB where EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "') and Candidate_ID not in(select CandidateId from InterviewResultTB where InterviewResultTB.InterviewStatus='Selected' or InterviewResultTB.InterviewStatus='Rejected' or InterviewResultTB.InterviewStatus='Offer' or InterviewResultTB.InterviewStatus='Offer Rejected' or InterviewResultTB.InterviewStatus='Join') And ST.CompanyId='" + Convert.ToInt32(lblcompID.Text) + "' ");
            }
            if (ddlsortby.SelectedIndex == 2 && txtpositionwise.Text != "")
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid  where ScheduleDetailsTB.ScheduleId in (Select ScheduleId from SchedulePanelTB where EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "') and Candidate_ID not in(select CandidateId from InterviewResultTB where InterviewResultTB.InterviewStatus='Selected' or InterviewResultTB.InterviewStatus='Rejected' or InterviewResultTB.InterviewStatus='Offer' or InterviewResultTB.InterviewStatus='Offer Rejected' or InterviewResultTB.InterviewStatus='Join') And VancancyTB.Title='" + txtpositionwise.Text + "'");
            }
            if (ddlsortby.SelectedIndex == 3 && txtcandidatewise.Text != "")
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid  where ScheduleDetailsTB.ScheduleId in (Select ScheduleId from SchedulePanelTB where EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "') and Candidate_ID not in(select CandidateId from InterviewResultTB where InterviewResultTB.InterviewStatus='Selected' or InterviewResultTB.InterviewStatus='Rejected' or InterviewResultTB.InterviewStatus='Offer' or InterviewResultTB.InterviewStatus='Offer Rejected' or InterviewResultTB.InterviewStatus='Join') And CandidateTB.Name='" + txtcandidatewise.Text + "'");
            }

            if (dtgrid.Rows.Count > 0)
            {
                grdschedule.DataSource = dtgrid;
                grdschedule.DataBind();
                lblcount.Text = dtgrid.Rows.Count.ToString();
            }
            else
            {
                grdschedule.DataSource = null;
                grdschedule.DataBind();
                lblcount.Text = "0";
            }
        }
    }


    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        string stid = imgbtn.CommandArgument;
        string[] stid1 = stid.Split(',');
        lblschudleid.Text = stid1[0].ToString();
        lblschudleDetailsId.Text = stid1[1].ToString();
        fillcompany();
        var scheduledata = from d in HR.ScheduleDetailsTBs
                           where d.ScheduleDetailsId == Convert.ToInt32(lblschudleDetailsId.Text)
                           select new { d.CompanyId, d.CandidateID, d.VacancyID };
        foreach (var item in scheduledata)
        {
            ddlcompany.SelectedValue = Convert.ToString(item.CompanyId);
            fillcandidate();
            ddlCandidate.SelectedValue = Convert.ToString(item.CandidateID);
            fillvacancy(ddlcompany.SelectedValue);
            ddlvacancy.SelectedValue = Convert.ToString(item.VacancyID);
        }
        
        
        modnopo.Show();
    }
    private void fillvacancy(string p)
    {
        var positiondata = from d in HR.VancancyTBs
                           where d.CompanyID == Convert.ToInt32(p)
                           select new { d.VacancyID, d.Title };
        if (positiondata.Count() > 0)
        {
            ddlvacancy.DataSource = positiondata;
            ddlvacancy.DataTextField = "Title";
            ddlvacancy.DataValueField = "VacancyID";
            ddlvacancy.DataBind();
            ddlvacancy.Items.Insert(0, "--Select--");

        }
    }
    private void fillcandidate()
    {
        var candidatedata = from d in HR.CandidateTBs
                            select new { d.Candidate_ID, d.Name };
        if (candidatedata.Count() > 0)
        {
            ddlCandidate.DataSource = candidatedata;
            ddlCandidate.DataTextField = "Name";
            ddlCandidate.DataValueField = "Candidate_ID";
            ddlCandidate.DataBind();
            ddlCandidate.Items.Insert(0, "--Select Candidate--");
        }
    }

    private void fillcompany()
    {
        var companydata = from d in HR.CompanyInfoTBs
                          select new { d.CompanyId, d.CompanyName };
        if (companydata.Count() > 0)
        {
            ddlcompany.DataSource = companydata;
            ddlcompany.DataTextField = "CompanyName";
            ddlcompany.DataValueField = "CompanyId";
            ddlcompany.DataBind();
            ddlcompany.Items.Insert(0, "--Select--");
        }
    }
   
    protected void btnsave_Click(object sender, EventArgs e)
    {
        
        var checkscheduledetailsID = from p in HR.InterviewResultTBs.Where(d => d.ScheduleDetailsID == Convert.ToInt32(lblschudleDetailsId.Text)) select p;
        if (checkscheduledetailsID.Count() > 0)
          {
              InterviewResultTB INT = HR.InterviewResultTBs.Where(s => s.ScheduleDetailsID == Convert.ToInt32(lblschudleDetailsId.Text)).First();
              INT.InterviewStatus = ddlstatus.SelectedItem.Text;

              if (ddlstatus.SelectedIndex == 4)
              {
                  if (txtdate.Text != "")
                  {
                      INT.NextDate = Convert.ToDateTime(txtdate.Text);
                  }
                  if (ddlemployee.SelectedIndex != -1)
                  {
                      INT.NextInterviewEmpID = Convert.ToInt32(ddlemployee.SelectedValue);
                  }
                  if (trtime.Visible == true)
                  {
                      INT.Time = ddhh.SelectedValue + " " + ":" + " " + ddmm.SelectedValue + " " + ddampm.SelectedValue;
                  }
              }
              if (ddlstatus.SelectedIndex == 3)
              {
                  if (ddlpositionforRecoJob.SelectedIndex != -1)
                  {
                      INT.NextInterviewPosition = Convert.ToInt32(ddlpositionforRecoJob.SelectedValue);
                  }
              }

              INT.Description = txtdesc.Text;
              HR.SubmitChanges();
            lblInterviewResultID.Text=Convert.ToString(INT.InterviewResultId);

          }
          else
          {
          #region Insert in InterviewResultTB
        InterviewResultTB INT = new InterviewResultTB();
        INT.CompanyID =Convert.ToInt32(ddlcompany.SelectedValue);
        INT.ScheduleId = Convert.ToInt32(lblschudleid.Text);
        INT.CandidateId = Convert.ToInt32(ddlCandidate.SelectedValue);
        INT.VacancyId = Convert.ToInt32(ddlvacancy.SelectedValue);
        INT.InterviewStatus = ddlstatus.SelectedItem.Text;
       if (ddlstatus.SelectedIndex == 4)
        {
            if (txtdate.Text != "")
            {
                INT.NextDate = Convert.ToDateTime(txtdate.Text);
            }
            if (ddlemployee.SelectedIndex != -1)
            {
                INT.NextInterviewEmpID = Convert.ToInt32(ddlemployee.SelectedValue);
            }
            if (trtime.Visible == true)
            {
                INT.Time = ddhh.SelectedValue + " " + ":" + " " + ddmm.SelectedValue + " " + ddampm.SelectedValue;
            }
        }
        if (ddlstatus.SelectedIndex == 3)
        {
            if (ddlpositionforRecoJob.SelectedIndex != -1)
            {
                INT.NextInterviewPosition = Convert.ToInt32(ddlpositionforRecoJob.SelectedValue);
            }
        }
        INT.Description = txtdesc.Text;
        INT.EntryBy = Convert.ToInt32(Session["UserId"]);
        INT.ScheduleDetailsID = Convert.ToInt32(lblschudleDetailsId.Text);
        HR.InterviewResultTBs.InsertOnSubmit(INT);
        HR.SubmitChanges();
          lblInterviewResultID.Text=Convert.ToString(INT.InterviewResultId);
         #endregion
          }



        #region InterviewResultDetailsTB
        InterviewResultDetailsTB i = new InterviewResultDetailsTB();
        i.CompanyID = Convert.ToInt32(ddlcompany.SelectedValue);
        i.InterviewResultId = Convert.ToInt32(lblInterviewResultID.Text);
        i.ScheduleId = Convert.ToInt32(lblschudleid.Text);
        i.CandidateId = Convert.ToInt32(ddlCandidate.SelectedValue);
        i.VacancyId = Convert.ToInt32(ddlvacancy.SelectedValue);
        i.InterviewStatus = ddlstatus.SelectedItem.Text;
        if (ddlstatus.SelectedIndex == 4)
        {
            if (txtdate.Text != "")
            {
                i.NextDate = Convert.ToDateTime(txtdate.Text);
            }
            if (ddlemployee.SelectedIndex != -1)
            {
                i.NextInterviewEmpID = Convert.ToInt32(ddlemployee.SelectedValue);
            }
            if (trtime.Visible == true)
            {
                i.Time = ddhh.SelectedValue + " " + ":" + " " + ddmm.SelectedValue + " " + ddampm.SelectedValue;
            }
        }
        if (ddlstatus.SelectedIndex == 3)
        {
            if (ddlpositionforRecoJob.SelectedIndex != -1)
            {
                i.NextInterviewPosition = Convert.ToInt32(ddlpositionforRecoJob.SelectedValue);
            }
        }
        i.Description = txtdesc.Text;
        i.EntryBy = Convert.ToInt32(Session["UserId"]);
        i.ScheduleDetailsID = Convert.ToInt32(lblschudleDetailsId.Text);
        HR.InterviewResultDetailsTBs.InsertOnSubmit(i);
        HR.SubmitChanges();


        #endregion












        #region Update ScheduleTB
        ScheduleTB SC = HR.ScheduleTBs.Where(d => d.ScheduleId == Convert.ToInt32(lblschudleid.Text)).First();
        SC.Status = ddlstatus.SelectedItem.Text;
        HR.SubmitChanges();
        #endregion

        #region Update ScheduleDetailsTB
        ScheduleDetailsTB SCD = HR.ScheduleDetailsTBs.Where(d => d.ScheduleDetailsId == Convert.ToInt32(lblschudleDetailsId.Text)).First();
        SCD.Status = ddlstatus.SelectedItem.Text;
        HR.SubmitChanges();
       #endregion

        #region Update SchedulePanelTB
        SchedulePanelTB SCP = HR.SchedulePanelTBs.Where(d => d.ScheduleId == Convert.ToInt32(lblschudleid.Text)).First();
        SCP.Status = ddlstatus.SelectedItem.Text;
        HR.SubmitChanges();
        #endregion


        g.ShowMessage(this.Page, "Interview Result submitted successfully");
        Clear();
        bindgrid();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
       
    }

    private void Clear()
    {
        trdate.Visible = false;
        txtdesc.Text = "";
        txtdate.Text = "";
        ddlstatus.SelectedIndex = 0;
        trdate.Visible = false;
        trtime.Visible = false;
        tremp.Visible = false;
        trjobrecom.Visible = false;
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstatus.SelectedIndex==3)
        {
            trjobrecom.Visible = true;
            fillposition();
        }
        else
        {
            trjobrecom.Visible = false; 
        }
        if (ddlstatus.SelectedIndex==4)
        {
            txtdate.Text = "";
            trdate.Visible = true;
            tremp.Visible = true;
            trtime.Visible = true;
            fillEmployee();
        }
        else
        {
            trdate.Visible = false;
            tremp.Visible = false;
            trtime.Visible = false;
           
        }
        //if (ddlstatus.SelectedIndex==0 || ddlstatus.SelectedIndex==1 || ddlstatus.SelectedIndex==2)
        //{
            
        //}


        modnopo.Show();
    }

    private void fillposition()
    {
        try
        {
            var fillpositiondata = from d in HR.VancancyTBs
                           where d.CompanyID == Convert.ToInt32(ddlcompany.SelectedValue)
                           select new { d.VacancyID, d.Title };
        if (fillpositiondata.Count() > 0)
        {
            ddlpositionforRecoJob.DataSource = fillpositiondata;
            ddlpositionforRecoJob.DataTextField = "Title";
            ddlpositionforRecoJob.DataValueField = "VacancyID";
            ddlpositionforRecoJob.DataBind();
         }
        else
        {
            ddlpositionforRecoJob.DataSource = null;
            ddlpositionforRecoJob.DataBind();
        }
        }
        catch (Exception ex)
        {
            
                 g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void fillEmployee()
    {
        try
        {
            try
            {
                if (ddlcompany.SelectedIndex != -1)
                {
                    var dt = from p in HR.EmployeeTBs
                             where p.RelivingStatus == null && p.CompanyId == Convert.ToInt32(ddlcompany.SelectedValue)
                             select new { Name = p.FName + ' ' + p.MName + ' ' + p.Lname, p.EmployeeId };
                    if (dt.Count() > 0)
                    {
                        ddlemployee.DataSource = dt;
                        ddlemployee.DataTextField = "Name";
                        ddlemployee.DataValueField = "EmployeeId";
                        ddlemployee.DataBind();
                      }
                    else
                    {
                        ddlemployee.Items.Clear();
                        ddlemployee.DataSource = null;
                        ddlemployee.DataBind();
                     }

                }
            }
            catch (Exception ex)
            {

                g.ShowMessage(this.Page, ex.Message);
            }
        }

        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }
    protected void txtdate_TextChanged(object sender, EventArgs e)
    {
        if (txtdate.Text!="")
        {
            if (Convert.ToDateTime(txtdate.Text) < Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")))
            {
                txtdate.Text = "";
                g.ShowMessage(this.Page, "Invalid next date");
            }
        }
        modnopo.Show();
    }
    protected void grdschedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdschedule.PageIndex = e.NewPageIndex;
        bindgrid();
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
    public static List<string> GetCompletionListPosition(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> position = (from d in hr.VancancyTBs
                                        .Where(r => r.Title.Contains(prefixText))
                                 select d.Title).Distinct().ToList();
        return position;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListCandidate(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> candidatename = (from d in hr.CandidateTBs
                                        .Where(r => r.Name.Contains(prefixText))
                                      select d.Name).Distinct().ToList();
        return candidatename;
    }


    protected void btnsearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }
    protected void ddlsortby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsortby.SelectedIndex == 0)
        {
            lblname.Visible = false;
            lblname.Text = "";
            txtcompanywise.Visible = false;
            txtpositionwise.Visible = false;
            txtcandidatewise.Visible = false;
            bindgrid();
        }
        if (ddlsortby.SelectedIndex == 1)
        {
            lblname.Visible = true;
            lblname.Text = "Company";
            txtcompanywise.Text = "";
            txtcompanywise.Visible = true;
            txtpositionwise.Visible = false;
            txtcandidatewise.Visible = false;
        }

        if (ddlsortby.SelectedIndex == 2)
        {
            lblname.Visible = true;
            lblname.Text = "Position";

            txtcompanywise.Visible = false;
            txtpositionwise.Text = "";
            txtpositionwise.Visible = true;
            txtcandidatewise.Visible = false;
        }
        if (ddlsortby.SelectedIndex == 3)
        {
            lblname.Visible = true;
            lblname.Text = "Candidate";
            txtcompanywise.Visible = false;
            txtpositionwise.Visible = false;
            txtcandidatewise.Text = "";
            txtcandidatewise.Visible = true;
        }

    }

    protected void txtcompanywise_TextChanged(object sender, EventArgs e)
    {
        if (txtcompanywise.Text != "")
        {
            var compId = from d in HR.CompanyInfoTBs
                         where d.CompanyName == txtcompanywise.Text
                         select new { d.CompanyId };
            if (compId.Count() > 0)
            {
                foreach (var item in compId)
                {
                    lblcompID.Text = item.CompanyId.ToString();
                }
            }
            else
            {
                lblcompID.Text = "";
            }
        }
    }

}