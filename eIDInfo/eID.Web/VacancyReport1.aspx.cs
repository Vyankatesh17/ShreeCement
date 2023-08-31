using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI; 
using System.Web.UI.WebControls;

public partial class Recruitment_VacancyReport1 : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable dtSelected = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                txtfromdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtTodate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtfromdate.Attributes.Add("readonly", "readonly");
                txtTodate.Attributes.Add("readonly", "readonly");
            }

        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }

    private void bindgrid()
    {
        DataTable dtgriddata = new DataTable();
        if (txtcompany.Text == "" && txtposition.Text == "")
        {
            dtgriddata = g.ReturnData("select distinct ITB.CompanyID,ITB.VacancyId,CT.CompanyName,VancancyTB.Title as position,VancancyTB.Quota,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Selected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Rejected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Rejected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Hold' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Hold,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Offer' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as Offer,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') as InterviewDone,((select count(SD.VacancyId) from ScheduleDetailsTB SD where  SD.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') -(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') ) as InterviewLineup  from InterviewResultTB ITB left outer join VancancyTB on ITB.VacancyId=VancancyTB.VacancyID left outer join ScheduleTB SC on ITB.ScheduleId=SC.ScheduleId Left outer join CompanyInfoTB CT on ITB.CompanyID=CT.CompanyId where SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "'");
        }
        if (txtcompany.Text != "" && txtposition.Text == "")
        {
            dtgriddata = g.ReturnData("select distinct ITB.CompanyID,ITB.VacancyId,CT.CompanyName,VancancyTB.Title as position,VancancyTB.Quota,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "') as Selected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Rejected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "') as Rejected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Hold' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "') as Hold,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Offer' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "') as Offer,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "') as InterviewDone,((select count(SD.VacancyId) from ScheduleDetailsTB SD where  SD.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "')-(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "') ) as InterviewLineup  from InterviewResultTB ITB left outer join VancancyTB on ITB.VacancyId=VancancyTB.VacancyID left outer join ScheduleTB SC on ITB.ScheduleId=SC.ScheduleId Left outer join CompanyInfoTB CT on ITB.CompanyID=CT.CompanyId where SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "'");
        }
        if (txtcompany.Text == "" && txtposition.Text != "")
        {
            dtgriddata = g.ReturnData("select distinct ITB.CompanyID,ITB.VacancyId,CT.CompanyName,VancancyTB.Title as position,VancancyTB.Quota,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as Selected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Rejected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as Rejected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Hold' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as Hold,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Offer' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as Offer,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as InterviewDone,((select count(SD.VacancyId) from ScheduleDetailsTB SD where  SD.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') -(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') ) as InterviewLineup  from InterviewResultTB ITB left outer join VancancyTB on ITB.VacancyId=VancancyTB.VacancyID left outer join ScheduleTB SC on ITB.ScheduleId=SC.ScheduleId Left outer join CompanyInfoTB CT on ITB.CompanyID=CT.CompanyId where SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "'");
        }
        if (txtcompany.Text != "" && txtposition.Text != "")
        {
            dtgriddata = g.ReturnData("select distinct ITB.CompanyID,ITB.VacancyId,CT.CompanyName,VancancyTB.Title as position,VancancyTB.Quota,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Selected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as Selected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Rejected' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as Rejected,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Hold' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as Hold,(select count(IT.VacancyId) from InterviewResultTB IT where InterviewStatus='Offer' and IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as Offer,(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') as InterviewDone,((select count(SD.VacancyId) from ScheduleDetailsTB SD where  SD.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') -(select count(IT.VacancyId) from InterviewResultTB IT where  IT.VacancyId=ITB.VacancyId and SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "') ) as InterviewLineup  from InterviewResultTB ITB left outer join VancancyTB on ITB.VacancyId=VancancyTB.VacancyID left outer join ScheduleTB SC on ITB.ScheduleId=SC.ScheduleId Left outer join CompanyInfoTB CT on ITB.CompanyID=CT.CompanyId where SC.Date between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "' and ITB.CompanyID='" + Convert.ToInt32(lblcompaniId.Text) + "' and ITB.VacancyID='" + Convert.ToInt32(lblvacancyID.Text) + "'");
        }
        if (dtgriddata.Rows.Count > 0)
        {
            grdVacancy.DataSource = dtgriddata;
            grdVacancy.DataBind();
        }
        else
        {
            grdVacancy.DataSource = null;
            grdVacancy.DataBind();
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

            LinkButton lblSelect = (LinkButton)grdVacancy.Rows[i].FindControl("lblselected");
            decimal totsel = Convert.ToDecimal(lblSelect.Text);
            totSelected += totsel;

            LinkButton lblReject = (LinkButton)grdVacancy.Rows[i].FindControl("lblRejected");
            decimal totRej = Convert.ToDecimal(lblReject.Text);
            totRejected += totRej;

            LinkButton lblHold = (LinkButton)grdVacancy.Rows[i].FindControl("lblHold");
            decimal totH = Convert.ToDecimal(lblHold.Text);
            totHold += totH;

            LinkButton lblOffer = (LinkButton)grdVacancy.Rows[i].FindControl("lblOffer");
            decimal totoff = Convert.ToDecimal(lblOffer.Text);
            totoffer += totoff;

            LinkButton lblInterviewSchedule = (LinkButton)grdVacancy.Rows[i].FindControl("lblIntSched");
            decimal totsche = Convert.ToDecimal(lblInterviewSchedule.Text);
            totIntschedule += totsche;

            LinkButton lblTotalInterview = (LinkButton)grdVacancy.Rows[i].FindControl("lblTotInt");
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
        }
    }
    protected void txtcompany_TextChanged(object sender, EventArgs e)
    {
        var companydata = from d in HR.CompanyInfoTBs
                          where d.CompanyName == txtcompany.Text
                          select new { d.CompanyId };
        foreach (var item in companydata)
        {
            lblcompaniId.Text = Convert.ToString(item.CompanyId);
            AutoCompleteExtenderPosition.ContextKey = lblcompaniId.Text;
        }
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
                                  .Where(r => r.Title.Contains(prefixText) && r.CompanyID == Convert.ToInt32(contextKey))
                                 select d.Title).Distinct().ToList();
        return position;
    }

    protected void txtposition_TextChanged(object sender, EventArgs e)
    {
        var positiondata = from d in HR.VancancyTBs
                           where d.CompanyID == Convert.ToInt32(lblcompaniId.Text) && d.Title == txtposition.Text
                           select new { d.VacancyID };
        foreach (var item in positiondata)
        {
            lblvacancyID.Text = Convert.ToString(item.VacancyID);
        }
        bindgrid();
    }
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        LinkButton lnkselect = (LinkButton)sender;
        string stselect = lnkselect.CommandArgument;
        string[] stselect1 = stselect.Split(',');

        DataTable dtdata = g.ReturnData("Select CandidateId from InterviewResultTB where CompanyID='" + Convert.ToInt32(stselect1[0].ToString()) + "' and VacancyId='" + Convert.ToInt32(stselect1[1].ToString()) + "'  and InterviewStatus='Selected'");
        for (int i = 0; i < dtdata.Rows.Count; i++)
        {
            DataTable dtcandidate = g.ReturnData("select Name,VancancyTB.Title as Position,convert(varchar,DOB,101) as DOB,Contact_No,Email_Address,'Selected' as InterviewStatus from CandidateTB Left outer join VancancyTB on CandidateTB.Vaccancy_ID=VancancyTB.VacancyID  where Candidate_ID='" + dtdata.Rows[i]["CandidateId"].ToString() + "'");

            if (ViewState["dtSelected"] != null)
            {
                dtSelected = (DataTable)ViewState["dtSelected"];
            }
            else
            {
                DataColumn Name = dtSelected.Columns.Add("Name");
                DataColumn Position = dtSelected.Columns.Add("Position");
                DataColumn DOB = dtSelected.Columns.Add("DOB");
                DataColumn ContactNo = dtSelected.Columns.Add("Contact_No");
                DataColumn Email_Address = dtSelected.Columns.Add("Email_Address");
                DataColumn InterviewStatus = dtSelected.Columns.Add("InterviewStatus");
            }

            DataRow dr = dtSelected.NewRow();
            dr[0] = dtcandidate.Rows[0]["Name"].ToString();
            dr[1] = dtcandidate.Rows[0]["Position"].ToString();
            dr[2] = dtcandidate.Rows[0]["DOB"].ToString();
            dr[3] = dtcandidate.Rows[0]["Contact_No"].ToString();
            dr[4] = dtcandidate.Rows[0]["Email_Address"].ToString();
            dr[5] = dtcandidate.Rows[0]["InterviewStatus"].ToString();
            dtSelected.Rows.Add(dr);
            ViewState["dtSelected"] = dtSelected;

        }
        if (dtSelected.Rows.Count > 0)
        {
            grdcandidate.DataSource = dtSelected;
            grdcandidate.DataBind();
        }
        else
        {
            grdcandidate.DataSource = null;
            grdcandidate.DataBind();
        }

        lbltitle.Text = "Selected Candidate Details";
        modnopo.Show();
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["dtSelected"] = null;
        dtSelected = null;
    }
    protected void lnkRejected_Click(object sender, EventArgs e)
    {
        LinkButton lnkreject = (LinkButton)sender;
        string strej = lnkreject.CommandArgument;
        string[] strej1 = strej.Split(',');

        DataTable dtdata = g.ReturnData("Select CandidateId from InterviewResultTB where CompanyID='" + Convert.ToInt32(strej1[0].ToString()) + "' and VacancyId='" + Convert.ToInt32(strej1[1].ToString()) + "'  and InterviewStatus='Rejected'");
        for (int i = 0; i < dtdata.Rows.Count; i++)
        {
            DataTable dtcandidate = g.ReturnData("select Name,VancancyTB.Title as Position,convert(varchar,DOB,101) as DOB,Contact_No,Email_Address,'Rejected' as InterviewStatus from CandidateTB Left outer join VancancyTB on CandidateTB.Vaccancy_ID=VancancyTB.VacancyID  where Candidate_ID='" + dtdata.Rows[i]["CandidateId"].ToString() + "'");

            if (ViewState["dtSelected"] != null)
            {
                dtSelected = (DataTable)ViewState["dtSelected"];
            }
            else
            {
                DataColumn Name = dtSelected.Columns.Add("Name");
                DataColumn Position = dtSelected.Columns.Add("Position");
                DataColumn DOB = dtSelected.Columns.Add("DOB");
                DataColumn ContactNo = dtSelected.Columns.Add("Contact_No");
                DataColumn Email_Address = dtSelected.Columns.Add("Email_Address");
                DataColumn InterviewStatus = dtSelected.Columns.Add("InterviewStatus");
            }

            DataRow dr = dtSelected.NewRow();
            dr[0] = dtcandidate.Rows[0]["Name"].ToString();
            dr[1] = dtcandidate.Rows[0]["Position"].ToString();
            dr[2] = dtcandidate.Rows[0]["DOB"].ToString();
            dr[3] = dtcandidate.Rows[0]["Contact_No"].ToString();
            dr[4] = dtcandidate.Rows[0]["Email_Address"].ToString();
            dr[5] = dtcandidate.Rows[0]["InterviewStatus"].ToString();
            dtSelected.Rows.Add(dr);
            ViewState["dtSelected"] = dtSelected;

        }
        if (dtSelected.Rows.Count > 0)
        {
            grdcandidate.DataSource = dtSelected;
            grdcandidate.DataBind();
        }
        else
        {
            grdcandidate.DataSource = null;
            grdcandidate.DataBind();
        }

        lbltitle.Text = "Rejected Candidate Details";
        modnopo.Show();
    }
    protected void lnkHold_Click(object sender, EventArgs e)
    {
        LinkButton lnkhold = (LinkButton)sender;
        string sthold = lnkhold.CommandArgument;
        string[] sthold1 = sthold.Split(',');

        DataTable dtdata = g.ReturnData("Select CandidateId from InterviewResultTB where CompanyID='" + Convert.ToInt32(sthold1[0].ToString()) + "' and VacancyId='" + Convert.ToInt32(sthold1[1].ToString()) + "'  and InterviewStatus='Hold'");
        for (int i = 0; i < dtdata.Rows.Count; i++)
        {
            DataTable dtcandidate = g.ReturnData("select Name,VancancyTB.Title as Position,convert(varchar,DOB,101) as DOB,Contact_No,Email_Address,'Hold' as InterviewStatus from CandidateTB Left outer join VancancyTB on CandidateTB.Vaccancy_ID=VancancyTB.VacancyID  where Candidate_ID='" + dtdata.Rows[i]["CandidateId"].ToString() + "'");

            if (ViewState["dtSelected"] != null)
            {
                dtSelected = (DataTable)ViewState["dtSelected"];
            }
            else
            {
                DataColumn Name = dtSelected.Columns.Add("Name");
                DataColumn Position = dtSelected.Columns.Add("Position");
                DataColumn DOB = dtSelected.Columns.Add("DOB");
                DataColumn ContactNo = dtSelected.Columns.Add("Contact_No");
                DataColumn Email_Address = dtSelected.Columns.Add("Email_Address");
                DataColumn InterviewStatus = dtSelected.Columns.Add("InterviewStatus");
            }

            DataRow dr = dtSelected.NewRow();
            dr[0] = dtcandidate.Rows[0]["Name"].ToString();
            dr[1] = dtcandidate.Rows[0]["Position"].ToString();
            dr[2] = dtcandidate.Rows[0]["DOB"].ToString();
            dr[3] = dtcandidate.Rows[0]["Contact_No"].ToString();
            dr[4] = dtcandidate.Rows[0]["Email_Address"].ToString();
            dr[5] = dtcandidate.Rows[0]["InterviewStatus"].ToString();
            dtSelected.Rows.Add(dr);
            ViewState["dtSelected"] = dtSelected;

        }
        if (dtSelected.Rows.Count > 0)
        {
            grdcandidate.DataSource = dtSelected;
            grdcandidate.DataBind();
        }
        else
        {
            grdcandidate.DataSource = null;
            grdcandidate.DataBind();
        }

        lbltitle.Text = "Hold Candidate Details";
        modnopo.Show();
    }
    protected void lnkOffer_Click(object sender, EventArgs e)
    {
        LinkButton lnkOffer = (LinkButton)sender;
        string stOffer = lnkOffer.CommandArgument;
        string[] stOffer1 = stOffer.Split(',');

        DataTable dtdata = g.ReturnData("Select CandidateId from InterviewResultTB where CompanyID='" + Convert.ToInt32(stOffer1[0].ToString()) + "' and VacancyId='" + Convert.ToInt32(stOffer1[1].ToString()) + "'  and InterviewStatus='Offer'");
        for (int i = 0; i < dtdata.Rows.Count; i++)
        {
            DataTable dtcandidate = g.ReturnData("select Name,VancancyTB.Title as Position,convert(varchar,DOB,101) as DOB,Contact_No,Email_Address,'Offer' as InterviewStatus from CandidateTB Left outer join VancancyTB on CandidateTB.Vaccancy_ID=VancancyTB.VacancyID  where Candidate_ID='" + dtdata.Rows[i]["CandidateId"].ToString() + "'");

            if (ViewState["dtSelected"] != null)
            {
                dtSelected = (DataTable)ViewState["dtSelected"];
            }
            else
            {
                DataColumn Name = dtSelected.Columns.Add("Name");
                DataColumn Position = dtSelected.Columns.Add("Position");
                DataColumn DOB = dtSelected.Columns.Add("DOB");
                DataColumn ContactNo = dtSelected.Columns.Add("Contact_No");
                DataColumn Email_Address = dtSelected.Columns.Add("Email_Address");
                DataColumn InterviewStatus = dtSelected.Columns.Add("InterviewStatus");
            }

            DataRow dr = dtSelected.NewRow();
            dr[0] = dtcandidate.Rows[0]["Name"].ToString();
            dr[1] = dtcandidate.Rows[0]["Position"].ToString();
            dr[2] = dtcandidate.Rows[0]["DOB"].ToString();
            dr[3] = dtcandidate.Rows[0]["Contact_No"].ToString();
            dr[4] = dtcandidate.Rows[0]["Email_Address"].ToString();
            dr[5] = dtcandidate.Rows[0]["InterviewStatus"].ToString();
            dtSelected.Rows.Add(dr);
            ViewState["dtSelected"] = dtSelected;

        }
        if (dtSelected.Rows.Count > 0)
        {
            grdcandidate.DataSource = dtSelected;
            grdcandidate.DataBind();
        }
        else
        {
            grdcandidate.DataSource = null;
            grdcandidate.DataBind();
        }

        lbltitle.Text = "Offer Candidate Details";
        modnopo.Show();
    }
    protected void lnkInterviewDone_Click(object sender, EventArgs e)
    {
        LinkButton lnkIntDone = (LinkButton)sender;
        string stIntDone = lnkIntDone.CommandArgument;
        string[] stIntDone1 = stIntDone.Split(',');

        DataTable dtdata = g.ReturnData("Select CandidateId from InterviewResultTB where CompanyID='" + Convert.ToInt32(stIntDone1[0].ToString()) + "' and VacancyId='" + Convert.ToInt32(stIntDone1[1].ToString()) + "'");
        for (int i = 0; i < dtdata.Rows.Count; i++)
        {
            DataTable dtcandidate = g.ReturnData("select Name,VancancyTB.Title as Position,convert(varchar,DOB,101) as DOB,Contact_No,Email_Address,'Done' as InterviewStatus from CandidateTB Left outer join VancancyTB on CandidateTB.Vaccancy_ID=VancancyTB.VacancyID  where Candidate_ID='" + dtdata.Rows[i]["CandidateId"].ToString() + "'");

            if (ViewState["dtSelected"] != null)
            {
                dtSelected = (DataTable)ViewState["dtSelected"];
            }
            else
            {
                DataColumn Name = dtSelected.Columns.Add("Name");
                DataColumn Position = dtSelected.Columns.Add("Position");
                DataColumn DOB = dtSelected.Columns.Add("DOB");
                DataColumn ContactNo = dtSelected.Columns.Add("Contact_No");
                DataColumn Email_Address = dtSelected.Columns.Add("Email_Address");
                DataColumn InterviewStatus = dtSelected.Columns.Add("InterviewStatus");
            }

            DataRow dr = dtSelected.NewRow();
            dr[0] = dtcandidate.Rows[0]["Name"].ToString();
            dr[1] = dtcandidate.Rows[0]["Position"].ToString();
            dr[2] = dtcandidate.Rows[0]["DOB"].ToString();
            dr[3] = dtcandidate.Rows[0]["Contact_No"].ToString();
            dr[4] = dtcandidate.Rows[0]["Email_Address"].ToString();
            dr[5] = dtcandidate.Rows[0]["InterviewStatus"].ToString();
            dtSelected.Rows.Add(dr);
            ViewState["dtSelected"] = dtSelected;

        }
        if (dtSelected.Rows.Count > 0)
        {
            grdcandidate.DataSource = dtSelected;
            grdcandidate.DataBind();
        }
        else
        {
            grdcandidate.DataSource = null;
            grdcandidate.DataBind();
        }

        lbltitle.Text = "Interview Done Candidate Details";
        modnopo.Show();
    }
    protected void lnkInterviewLineup_Click(object sender, EventArgs e)
    {
        LinkButton lnkIntlineup = (LinkButton)sender;
        string stIntlineup = lnkIntlineup.CommandArgument;
        string[] stIntlineup1 = stIntlineup.Split(',');

        DataTable dtdata = g.ReturnData("Select CandidateId from InterviewResultTB where CompanyID='" + Convert.ToInt32(stIntlineup1[0].ToString()) + "' and VacancyId='" + Convert.ToInt32(stIntlineup1[1].ToString()) + "'  and InterviewStatus='Offer'");
        for (int i = 0; i < dtdata.Rows.Count; i++)
        {
            DataTable dtcandidate = g.ReturnData("select Name,VancancyTB.Title as Position,convert(varchar,DOB,101) as DOB,Contact_No,Email_Address,'Offer' as InterviewStatus from CandidateTB Left outer join VancancyTB on CandidateTB.Vaccancy_ID=VancancyTB.VacancyID  where Candidate_ID='" + dtdata.Rows[i]["CandidateId"].ToString() + "'");

            if (ViewState["dtSelected"] != null)
            {
                dtSelected = (DataTable)ViewState["dtSelected"];
            }
            else
            {
                DataColumn Name = dtSelected.Columns.Add("Name");
                DataColumn Position = dtSelected.Columns.Add("Position");
                DataColumn DOB = dtSelected.Columns.Add("DOB");
                DataColumn ContactNo = dtSelected.Columns.Add("Contact_No");
                DataColumn Email_Address = dtSelected.Columns.Add("Email_Address");
                DataColumn InterviewStatus = dtSelected.Columns.Add("InterviewStatus");
            }

            DataRow dr = dtSelected.NewRow();
            dr[0] = dtcandidate.Rows[0]["Name"].ToString();
            dr[1] = dtcandidate.Rows[0]["Position"].ToString();
            dr[2] = dtcandidate.Rows[0]["DOB"].ToString();
            dr[3] = dtcandidate.Rows[0]["Contact_No"].ToString();
            dr[4] = dtcandidate.Rows[0]["Email_Address"].ToString();
            dr[5] = dtcandidate.Rows[0]["InterviewStatus"].ToString();
            dtSelected.Rows.Add(dr);
            ViewState["dtSelected"] = dtSelected;

        }
        if (dtSelected.Rows.Count > 0)
        {
            grdcandidate.DataSource = dtSelected;
            grdcandidate.DataBind();
        }
        else
        {
            grdcandidate.DataSource = null;
            grdcandidate.DataBind();
        }

        lbltitle.Text = "InterviewLineup Candidate Details";
        modnopo.Show();
    }
}