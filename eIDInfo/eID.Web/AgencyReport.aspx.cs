using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Recruitment_AgencyReport : System.Web.UI.Page
{
    /// <summary>
    /// Agency Report
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

                txtTodate.Attributes.Add("readonly", "readonly");
                txtfromdate.Attributes.Add("readonly", "readonly");
                DateTime dtt = DateTime.Now.Date;
                txtfromdate.Text = g.GetStartOfMonth(dtt).ToShortDateString();
                txtTodate.Text = g.EndOfMonth(dtt).ToShortDateString();
             
                
                BindAgency();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    
    private void BindAgency()
    {
        //DataSet dsagency = g.ReturnData1("select distinct ITB.CompanyID,ITB.VacancyId,A.AgencyName,VancancyTB.Title as position,(select count(SD.VacancyId) from ScheduleDetailsTB SD where SD.VacancyId=ITB.VacancyId and SD.CandidateID=ITB.CandidateID and convert(date,SC.Date,101) between  convert(date,'" + Convert.ToDateTime(txtfromdate.Text) + "',101) and convert(date,'" + txtTodate.Text + "',101)) as ScheduleCnt,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=ITB.VacancyId and IT.CandidateID=ITB.CandidateID and SC.Date between '" + txtfromdate.Text + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Selected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Rejected' and IT.VacancyId=ITB.VacancyId and IT.CandidateID=ITB.CandidateID and convert(date,SC.Date,101) between convert(date,'" + txtfromdate.Text + "',101) and convert(date,'" + txtTodate.Text + "',101)) as Rejected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Hold' and IT.VacancyId=ITB.VacancyId and IT.CandidateID=ITB.CandidateID and convert(date,SC.Date,101) between convert(date,'" + txtfromdate.Text + "',101)  and convert(date,'" + txtTodate.Text + "',101)) as Hold,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Offer' and IT.VacancyId=ITB.VacancyId and IT.CandidateID=ITB.CandidateID and convert(date, SC.Date,101)  between convert(date,'" + txtfromdate.Text + "',101) and convert(date,'" + txtTodate.Text + "',101)) as Offer from InterviewResultTB ITB  left outer join VancancyTB on ITB.VacancyId=VancancyTB.VacancyID  left outer join ScheduleTB SC on ITB.ScheduleId=SC.ScheduleId Left outer join CompanyInfoTB CT on ITB.CompanyID=CT.CompanyId  Left outer join CandidateTB C  on ITB.CandidateId=C.Candidate_ID left outer join AgencyMasterTB A on C.AgencyId=A.AgencyId where C.AgencyId  is not null and convert(date,SC.Date,101)  between convert(date,'" + txtfromdate.Text + "',101) and convert(date,'" + txtTodate.Text + "',101) order by AgencyName,Title");
        DataSet dsagency = g.ReturnData1("select distinct ITB.CompanyID,ITB.VacancyId,A.AgencyName,VancancyTB.Title as position,(select count(SD.VacancyId) from ScheduleDetailsTB SD where SD.VacancyId=ITB.VacancyId and SD.CandidateID=ITB.CandidateID and convert(date,SC.Date,101) between  convert(date,'" + Convert.ToDateTime(txtfromdate.Text) + "',101) and convert(date,'" + txtTodate.Text + "',101)) as ScheduleCnt,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=ITB.VacancyId and IT.CandidateID=ITB.CandidateID and SC.Date between '" + txtfromdate.Text + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Selected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Rejected' and IT.VacancyId=ITB.VacancyId and IT.CandidateID=ITB.CandidateID and convert(date,SC.Date,101) between convert(date,'" + txtfromdate.Text + "',101) and convert(date,'" + txtTodate.Text + "',101)) as Rejected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Hold' and IT.VacancyId=ITB.VacancyId and IT.CandidateID=ITB.CandidateID and convert(date,SC.Date,101) between convert(date,'" + txtfromdate.Text + "',101)  and convert(date,'" + txtTodate.Text + "',101)) as Hold,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Offer' and IT.VacancyId=ITB.VacancyId and IT.CandidateID=ITB.CandidateID and convert(date, SC.Date,101)  between convert(date,'" + txtfromdate.Text + "',101) and convert(date,'" + txtTodate.Text + "',101)) as Offer from InterviewResultTB ITB  left outer join VancancyTB on ITB.VacancyId=VancancyTB.VacancyID  left outer join ScheduleTB SC on ITB.ScheduleId=SC.ScheduleId Left outer join CompanyInfoTB CT on ITB.CompanyID=CT.CompanyId  Left outer join CandidateTB C  on ITB.CandidateId=C.Candidate_ID left outer join AgencyMasterTB A on C.AgencyId=A.AgencyId where C.AgencyId  is not null and convert(date,SC.Date,101)  between convert(date,'" + txtfromdate.Text + "',101) and convert(date,'" + txtTodate.Text + "',101) order by AgencyName,Title");
        if (dsagency.Tables[0].Rows.Count==0)
        {
            grdAgencyReport.DataSource = null;
            grdAgencyReport.DataBind();
            lblcount.Text = "0";
        }
        else
        {
            grdAgencyReport.DataSource = dsagency.Tables[0];
            grdAgencyReport.DataBind();
            lblcount.Text = "0";
        }
      
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAgency();
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DateTime dtt = DateTime.Now.Date;
        txtfromdate.Text = g.GetStartOfMonth(dtt).ToShortDateString();
        txtTodate.Text = g.EndOfMonth(dtt).ToShortDateString();
             
        BindAgency();
    }
    protected void grdAgencyReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int rowIndex = grdAgencyReport.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvrow = grdAgencyReport.Rows[rowIndex];
            GridViewRow gvprivous = grdAgencyReport.Rows[rowIndex + 1];

            for (int cellCount = 0; cellCount < gvrow.Cells.Count; cellCount++)
            {
                if (cellCount == 0)
                {
                    if (gvrow.Cells[cellCount].Text == gvprivous.Cells[cellCount].Text)
                    {
                        if (gvprivous.Cells[cellCount].RowSpan < 2)
                        {
                            gvrow.Cells[cellCount].RowSpan = 2;
                        }

                        else
                        {
                            gvrow.Cells[cellCount].RowSpan = gvprivous.Cells[cellCount].RowSpan + 1;
                        }
                        gvprivous.Cells[cellCount].Visible = false;
                    }
                }


             


               
               
            }
        }

    }
    protected void grdAgencyReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAgencyReport.PageIndex = e.NewPageIndex;
        BindAgency();
    }
}