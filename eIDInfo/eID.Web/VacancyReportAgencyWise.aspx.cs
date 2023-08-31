using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Recruitment_VacancyReportAgencyWise : System.Web.UI.Page
{
    /// <summary>
    /// Vacancy Report Agency Wise  
    /// Created Date : 25/11/2014
    ///   Created By Abdul Rahman
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region declearation
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                var registratindata = from d in HR.RegistrationTBs
                                      where d.EmployeeId == Convert.ToInt32(Session["UserId"]) && d.UserType == "Admin"
                                      select new { d.EmployeeId };
                if (registratindata.Count() > 0)
                {
                    tragency.Visible = true;
                }
                else
                {
                    var agencydata = from d in HR.AgencyMasterTBs
                                     where d.AgencyId == Convert.ToInt32(Session["UserId"]) && Session["UserType"] == "Agency"
                                     select new { d.AgencyId };
                    if (agencydata.Count() > 0)
                    {
                        tragency.Visible = false;
                    }
                }
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                txtfromdate.Text = DateTime.Now.ToShortDateString();
                txtfromdate.Attributes.Add("readonly", "readonly");
                txtTodate.Text = DateTime.Now.ToShortDateString();
                txtTodate.Attributes.Add("readonly", "readonly");
                DateTime dtt = DateTime.Now.Date;
                txtfromdate.Text = g.GetStartOfMonth(dtt).ToShortDateString();
                txtTodate.Text = g.EndOfMonth(dtt).ToShortDateString();

                grdVacancy.DataSource = null;
                grdVacancy.DataBind();
                lblcount.Text = "0";
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtagencywise.Text != "")
        {
            var findAgencyID = from d in HR.AgencyMasterTBs
                               where d.AgencyName == txtagencywise.Text
                               select new { d.AgencyId };
            if (findAgencyID.Count() > 0)
            {
                foreach (var item in findAgencyID)
                {
                    lblagencyId.Text = item.AgencyId.ToString();
                    bindgrid();
                }
            }
            else
            {
                lblagencyId.Text = "";
            }

        }
        else
        {
            bindgrid();
        }
        
    }
    private void bindgrid()
    {

        DataTable dtgriddata = new DataTable();
        var registratindata = from d in HR.RegistrationTBs
                                  where d.EmployeeId == Convert.ToInt32(Session["UserId"]) && d.UserType == "Admin"
                                  select new { d.EmployeeId };
        if (registratindata.Count() > 0)
        {
            if (txtfromdate.Text != "" && txtTodate.Text != "" && txtagencywise.Text == "")
            {
                dtgriddata = g.ReturnData("select distinct ITB.CompanyID,ITB.VacancyId,CT.CompanyName,VancancyTB.Title as position,VancancyTB.Quota,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Selected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Rejected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Rejected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Hold' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Hold,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Offer' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Offer,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as InterviewDone,((select count(SD.VacancyId) from ScheduleDetailsTB SD where  SD.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') -(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') ) as InterviewLineup  from InterviewResultTB ITB left outer join VancancyTB on ITB.VacancyId=VancancyTB.VacancyID left outer join ScheduleTB SC on ITB.ScheduleId=SC.ScheduleId Left outer join CompanyInfoTB CT on ITB.CompanyID=CT.CompanyId where SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' order by CompanyName,Title");

            }
            if (txtfromdate.Text != "" && txtTodate.Text != "" && lblagencyId.Text != "" && txtagencywise.Text != "")
            {
                dtgriddata = g.ReturnData("select distinct ITB.CompanyID,ITB.VacancyId,CT.CompanyName,VancancyTB.Title as position,VancancyTB.Quota,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as Selected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Rejected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as Rejected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Hold' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as Hold,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Offer' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as Offer,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as InterviewDone,((select count(SD.VacancyId) from ScheduleDetailsTB SD where  SD.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') -(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') ) as InterviewLineup  from InterviewResultTB ITB left outer join VancancyTB on ITB.VacancyId=VancancyTB.VacancyID left outer join ScheduleTB SC on ITB.ScheduleId=SC.ScheduleId Left outer join CompanyInfoTB CT on ITB.CompanyID=CT.CompanyId  Left outer Join CandidateTB ca on ITB.CandidateId=ca.Candidate_ID  where SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "' order by CompanyName,Title");
            }

        }
          var agencydata = from d in HR.AgencyMasterTBs
                             where d.AgencyId == Convert.ToInt32(Session["UserId"]) && Session["UserType"] == "Agency"
                             select new { d.AgencyId };
          if (agencydata.Count() > 0)
          {
              foreach (var item in agencydata)
	            {
		            lblagencyId.Text= item.AgencyId.ToString();
	            }
              
              if (txtfromdate.Text != "" && txtTodate.Text != "")
              {
                  dtgriddata = g.ReturnData("select distinct ITB.CompanyID,ITB.VacancyId,CT.CompanyName,VancancyTB.Title as position,VancancyTB.Quota,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as Selected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Rejected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as Rejected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Hold' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as Hold,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Offer' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as Offer,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') as InterviewDone,((select count(SD.VacancyId) from ScheduleDetailsTB SD where  SD.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') -(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "') ) as InterviewLineup  from InterviewResultTB ITB left outer join VancancyTB on ITB.VacancyId=VancancyTB.VacancyID left outer join ScheduleTB SC on ITB.ScheduleId=SC.ScheduleId Left outer join CompanyInfoTB CT on ITB.CompanyID=CT.CompanyId  Left outer Join CandidateTB ca on ITB.CandidateId=ca.Candidate_ID  where SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ca.AgencyId='" + Convert.ToInt32(lblagencyId.Text) + "' order by CompanyName,Title");
              }
          }
        if (dtgriddata.Rows.Count > 0)
        {
            grdVacancy.DataSource = dtgriddata;
            grdVacancy.DataBind();
            lblcount.Text = dtgriddata.Rows.Count.ToString();
        }
        else
        {
            grdVacancy.DataSource = null;
            grdVacancy.DataBind();
            lblcount.Text ="0";
        }
    }
    protected void txtfromdate_TextChanged(object sender, EventArgs e)
    {
        if (txtfromdate.Text != "" && txtTodate.Text != "")
        {

            if (Convert.ToDateTime(txtfromdate.Text) > Convert.ToDateTime(txtTodate.Text))
            {
                g.ShowMessage(this, "You can not select From Date greater than To Date");
                txtfromdate.Text = "";
            }
        }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        if (txtfromdate.Text != "" && txtTodate.Text != "")
        {
            if (Convert.ToDateTime(txtfromdate.Text) > Convert.ToDateTime(txtTodate.Text))
            {
                g.ShowMessage(this, "You can not select To Date Less than From Date");
                txtTodate.Text = "";
            }
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionList(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> AgencyName = (from d in hr.AgencyMasterTBs
                                  .Where(r => r.AgencyName.Contains(prefixText))
                                   select d.AgencyName).Distinct().ToList();
        return AgencyName;
    }
    protected void txtagencywise_TextChanged(object sender, EventArgs e)
    {
        if (txtagencywise.Text != "")
        {
            var findAgencyID = from d in HR.AgencyMasterTBs
                               where d.AgencyName == txtagencywise.Text
                               select new { d.AgencyId };
            if (findAgencyID.Count() > 0)
            {
                foreach (var item in findAgencyID)
                {
                    lblagencyId.Text = item.AgencyId.ToString();
                   
                }
            }
            else
            {
                lblagencyId.Text = "";
            }

        }
    }

    protected void grdVacancy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        decimal totQuota = 0;
        decimal totSelected = 0;
        decimal totRejected = 0;
        decimal totHold = 0;
        decimal totoffer = 0;
        decimal totIntschedule = 0;
        decimal totalInterview = 0;

        for (int i = 0; i < grdVacancy.Rows.Count; i++)
        {
            Label lblquota = (Label)grdVacancy.Rows[i].FindControl("lblQuota");
            decimal totqot = Convert.ToDecimal(lblquota.Text);
            totQuota += totqot;

            Label lblSelect = (Label)grdVacancy.Rows[i].FindControl("lblselected");
            decimal totsel = Convert.ToDecimal(lblSelect.Text);
            totSelected += totsel;

            Label lblReject = (Label)grdVacancy.Rows[i].FindControl("lblRejected");
            decimal totRej = Convert.ToDecimal(lblReject.Text);
            totRejected += totRej;

            Label lblHold = (Label)grdVacancy.Rows[i].FindControl("lblHold");
            decimal totH = Convert.ToDecimal(lblHold.Text);
            totHold += totH;

            Label lblOffer = (Label)grdVacancy.Rows[i].FindControl("lblOffer");
            decimal totoff = Convert.ToDecimal(lblOffer.Text);
            totoffer += totoff;

            Label lblInterviewSchedule = (Label)grdVacancy.Rows[i].FindControl("lblIntSched");
            decimal totsche = Convert.ToDecimal(lblInterviewSchedule.Text);
            totIntschedule += totsche;

            Label lblTotalInterview = (Label)grdVacancy.Rows[i].FindControl("lblTotInt");
            decimal totinter = Convert.ToDecimal(lblTotalInterview.Text);
            totalInterview += totinter;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total :";
            e.Row.Cells[3].Text = totQuota.ToString();
            e.Row.Cells[4].Text = totSelected.ToString();
            e.Row.Cells[5].Text = totRejected.ToString();
            e.Row.Cells[6].Text = totHold.ToString();
            e.Row.Cells[7].Text = totoffer.ToString();
            e.Row.Cells[8].Text = totIntschedule.ToString();
            e.Row.Cells[9].Text = totalInterview.ToString();
            e.Row.BackColor = Color.LightGray;
        }
    }

    protected void grdVacancy_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdVacancy.PageIndex = e.NewPageIndex;
        bindgrid();
    }
}