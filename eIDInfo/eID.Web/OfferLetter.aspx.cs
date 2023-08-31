using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Text;
  
public partial class Recruitment_OfferLetter : System.Web.UI.Page
{
    /// <summary>
    /// offer letter form
    /// Created By : Abdul Rahman
    /// Created Date : 14/11/2014
    /// </summary>
    #region Declear Variable
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    static DataTable tbloffer = null;
    DataRow dr = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["UserId"] != null)
        {
        if (!IsPostBack)
        {
            //fillposition();
            bindselectedcandidategrd();
           bindoffergrd();
            txtJoiningdate.Text = DateTime.Now.ToShortDateString();
            txtjoindate.Attributes.Add("readonly", "readonly");
        }
          #region
        tbloffer = new DataTable();
        tbloffer.Columns.Add(new DataColumn("Name", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Company Name", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Position", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Contact No", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Interview Date", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Offer Date", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Joining Date", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Probation Period", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Department", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Designation", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Reporting", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("E-mail", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Salary", typeof(string)));
        tbloffer.Columns.Add(new DataColumn("Remark", typeof(string)));
       
        #endregion
        }
         else
         {
             Response.Redirect("Login.aspx");
         }
       
    }

    private void fillposition()
    {
        try
        {
            if (lblcompaniId.Text !="")
            {
             var fillpositiodata = from d in hr.VancancyTBs
                                   where d.CompanyID == Convert.ToInt32(lblcompaniId.Text)
                                  select new { d.VacancyID, d.Title };
            ddlposition.DataSource = fillpositiodata;
            ddlposition.DataTextField = "Title";
            ddlposition.DataValueField = "VacancyID";
            ddlposition.DataBind();
            ddlposition.Items.Insert(0, "All");
            }
           

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    private void bindoffergrd()
    {
        try
        {
            //if (ddlposition.SelectedIndex != -1)
            //{
                #region Seardching All position & company,Candidate, & Skills wise search
            if (txtcompany.Text == "" && ddlposition.SelectedIndex == -1 && txtcandidatename.Text == "" && txtskills.Text == "")
                {
                    DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID, f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' ");
                    if (dtfetchofferddata.Rows.Count > 0)
                    {
                        grdoffered.DataSource = dtfetchofferddata;
                        grdoffered.DataBind();
                    }
                    else
                    {
                        grdoffered.DataSource = null;
                        grdoffered.DataBind();
                    }
                }

            if (txtcompany.Text != "" && ddlposition.SelectedIndex == -1 && txtcandidatename.Text == "" && txtskills.Text == "")
                {

                    DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID, f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' And f.company_Id='" + Convert.ToInt32(lblcompaniId.Text) + "' ");
                    if (dtfetchofferddata.Rows.Count > 0)
                    {
                        grdoffered.DataSource = dtfetchofferddata;
                        grdoffered.DataBind();
                    }
                    else
                    {
                        grdoffered.DataSource = null;
                        grdoffered.DataBind();
                    }

                }

            if (txtcompany.Text != "" && ddlposition.SelectedIndex == 0 && txtcandidatename.Text == "" && txtskills.Text == "")
            {

                DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID, f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' And f.company_Id='" + Convert.ToInt32(lblcompaniId.Text) + "' ");
                if (dtfetchofferddata.Rows.Count > 0)
                {
                    grdoffered.DataSource = dtfetchofferddata;
                    grdoffered.DataBind();
                }
                else
                {
                    grdoffered.DataSource = null;
                    grdoffered.DataBind();
                }

            }


                 if (txtcompany.Text != "" && ddlposition.SelectedIndex > 0 && txtcandidatename.Text == "" && txtskills.Text == "")
                    {

                        DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID, f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' And f.company_Id='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' ");
                        if (dtfetchofferddata.Rows.Count > 0)
                        {
                            grdoffered.DataSource = dtfetchofferddata;
                            grdoffered.DataBind();
                        }
                        else
                        {
                            grdoffered.DataSource = null;
                            grdoffered.DataBind();
                        }
                    }

                 if (txtcompany.Text != "" && ddlposition.SelectedIndex > 0 && txtcandidatename.Text != "" && txtskills.Text == "")
                 {

                     DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID, f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' And f.company_Id='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And c.Name='" + txtcandidatename.Text + "' ");
                     if (dtfetchofferddata.Rows.Count > 0)
                     {
                         grdoffered.DataSource = dtfetchofferddata;
                         grdoffered.DataBind();
                     }
                     else
                     {
                         grdoffered.DataSource = null;
                         grdoffered.DataBind();
                     }
                 }

                 if (txtcompany.Text != "" && ddlposition.SelectedIndex > 0 && txtcandidatename.Text != "" && txtskills.Text != "")
                    {
                        DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID, f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' And f.company_Id='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And c.Name='" + txtcandidatename.Text + "' And c.skills='" + txtskills.Text + "' ");
                        if (dtfetchofferddata.Rows.Count > 0)
                        {
                            grdoffered.DataSource = dtfetchofferddata;
                            grdoffered.DataBind();
                        }
                        else
                        {
                            grdoffered.DataSource = null;
                            grdoffered.DataBind();
                        }
                    }
                #endregion


                #region Position wise 
                //    if (ddlposition.SelectedIndex > 0 && txtcompany.Text == "" && txtcandidatename.Text == "" && txtskills.Text == "")
                //    {
                //        DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID, f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' And c.Vaccancy_ID ='" + Convert.ToInt32(ddlposition.SelectedValue) + "'  ");
                //        if (dtfetchofferddata.Rows.Count > 0)
                //        {
                //            grdoffered.DataSource = dtfetchofferddata;
                //            grdoffered.DataBind();
                //        }
                //        else
                //        {
                //            grdoffered.DataSource = null;
                //            grdoffered.DataBind();
                //        }
                //    }

                //    if (ddlposition.SelectedIndex > 0 && txtcompany.Text != "" && txtcandidatename.Text == "" && txtskills.Text == "")
                //    {

                //        DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID, f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' And f.company_Id='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Vaccancy_ID ='" + Convert.ToInt32(ddlposition.SelectedValue) + "' ");
                //        if (dtfetchofferddata.Rows.Count > 0)
                //        {
                //            grdoffered.DataSource = dtfetchofferddata;
                //            grdoffered.DataBind();
                //        }
                //        else
                //        {
                //            grdoffered.DataSource = null;
                //            grdoffered.DataBind();
                //        }

                //    }

                //    if (ddlposition.SelectedIndex > 0 && txtcompany.Text != "" && txtcandidatename.Text != "" && txtskills.Text == "")
                //    {

                //        DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID,  f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' And f.company_Id='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Name='" + txtcandidatename.Text + "' And c.Vaccancy_ID ='" + Convert.ToInt32(ddlposition.SelectedValue) + "' ");
                //        if (dtfetchofferddata.Rows.Count > 0)
                //        {
                //            grdoffered.DataSource = dtfetchofferddata;
                //            grdoffered.DataBind();
                //        }
                //        else
                //        {
                //            grdoffered.DataSource = null;
                //            grdoffered.DataBind();
                //        }
                //    }

                //    if (ddlposition.SelectedIndex > 0 && txtcompany.Text != "" && txtcandidatename.Text != "" && txtskills.Text != "")
                //    {
                //        DataTable dtfetchofferddata = g.ReturnData("Select f.CadidateOffer_Id, f.ScheduleDetailsID, f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID where f.Status='Offer' And f.company_Id='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Name='" + txtcandidatename.Text + "' And c.skills='" + txtskills.Text + "' And c.Vaccancy_ID ='" + Convert.ToInt32(ddlposition.SelectedValue) + "' ");
                //        if (dtfetchofferddata.Rows.Count > 0)
                //        {
                //            grdoffered.DataSource = dtfetchofferddata;
                //            grdoffered.DataBind();
                //        }
                //        else
                //        {
                //            grdoffered.DataSource = null;
                //            grdoffered.DataBind();
                //        }
                //    }


                #endregion


                //}
                //else
                //{
                //    g.ShowMessage(this.Page, "Position is copmulsory");
                //}
            
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    
    private void bindselectedcandidategrd()
    {
        try
        {
            string query = @"Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected'";
            if(!string.IsNullOrEmpty(txtcompany.Text))
            {
                query += " and co.CompanyName like '%" + txtcompany.Text + "%'";
            }
            if (ddlposition.SelectedIndex>0)
            {
                query += " and v.VacancyID =" + ddlposition.SelectedValue + "";
            } 
            if (!string.IsNullOrEmpty(txtcandidatename.Text))
            {
                query += " and c.Name like '%" + txtcandidatename.Text + "%'";
            } 
            if (!string.IsNullOrEmpty(txtskills.Text))
            {
                query += " and c.Skills like '%" + txtskills.Text + "%'";
            }
            DataTable fetchselectedcandidate = g.ReturnData(query);
            if (fetchselectedcandidate.Rows.Count > 0)
            {
                grdselectedcandidate.DataSource = fetchselectedcandidate;
                grdselectedcandidate.DataBind();
            }
            else
            {
                grdselectedcandidate.DataSource = null;
                grdselectedcandidate.DataBind();
            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    private void bindselectedcandidategrdOld()
    {
        try
        {
            //if (txtcompany.Text !="")
            //{
                #region searching in All position & with Company, Candidate Name & Skills
            if (txtcompany.Text == "" && ddlposition.SelectedIndex == -1  && txtcandidatename.Text == "" && txtskills.Text == "")
            {
                DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected' ");
                if (fetchselectedcandidate.Rows.Count > 0)
                {
                    grdselectedcandidate.DataSource = fetchselectedcandidate;
                    grdselectedcandidate.DataBind();
                }
                else
                {
                    grdselectedcandidate.DataSource = null;
                    grdselectedcandidate.DataBind();
                }
            }
            if (txtcompany.Text != "" && ddlposition.SelectedIndex== -1 && txtcandidatename.Text == "" && txtskills.Text == "")
            {
                DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected'  AND s.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "'  ");
                if (fetchselectedcandidate.Rows.Count > 0)
                {
                    grdselectedcandidate.DataSource = fetchselectedcandidate;
                    grdselectedcandidate.DataBind();
                }
                else
                {
                    grdselectedcandidate.DataSource = null;
                    grdselectedcandidate.DataBind();
                }
            }

            if (txtcompany.Text != "" && ddlposition.SelectedIndex == 0 && txtcandidatename.Text == "" && txtskills.Text == "")
            {
                DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected'  AND s.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "'  ");
                if (fetchselectedcandidate.Rows.Count > 0)
                {
                    grdselectedcandidate.DataSource = fetchselectedcandidate;
                    grdselectedcandidate.DataBind();
                }
                else
                {
                    grdselectedcandidate.DataSource = null;
                    grdselectedcandidate.DataBind();
                }
            }




                if (txtcompany.Text != "" && ddlposition.SelectedIndex > 0 && txtcandidatename.Text == "" && txtskills.Text == "")
                {
                    DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected' AND s.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "'");
                    if (fetchselectedcandidate.Rows.Count > 0)
                    {
                        grdselectedcandidate.DataSource = fetchselectedcandidate;
                        grdselectedcandidate.DataBind();
                    }
                    else
                    {
                        grdselectedcandidate.DataSource = null;
                        grdselectedcandidate.DataBind();
                    }
                }




                if (txtcompany.Text != "" && ddlposition.SelectedIndex > 0 && txtcandidatename.Text != "" && txtskills.Text == "")
                {
                    DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected' AND s.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And c.Name='" + (txtcandidatename.Text) + "'");
                    if (fetchselectedcandidate.Rows.Count > 0)
                    {
                        grdselectedcandidate.DataSource = fetchselectedcandidate;
                        grdselectedcandidate.DataBind();
                    }
                    else
                    {
                        grdselectedcandidate.DataSource = null;
                        grdselectedcandidate.DataBind();
                    }
                }

                if (txtcompany.Text != "" && ddlposition.SelectedIndex > 0 && txtcandidatename.Text != "" && txtskills.Text != "")
                {
                    DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected'  AND s.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And c.Name='" + (txtcandidatename.Text) + "' And c.Skills='" + (txtskills.Text) + "'  ");
                    if (fetchselectedcandidate.Rows.Count > 0)
                    {
                        grdselectedcandidate.DataSource = fetchselectedcandidate;
                        grdselectedcandidate.DataBind();
                    }
                    else
                    {
                        grdselectedcandidate.DataSource = null;
                        grdselectedcandidate.DataBind();
                    }
                }

                #endregion

                //#region searching in  position Wise & with Company, Candidate Name & Skills
                //if (ddlposition.SelectedIndex > 0 && txtcompany.Text == "" && txtcandidatename.Text == "" && txtskills.Text == "")
                //{
                //    DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected'  AND st.VacancyId='" + Convert.ToInt32(ddlposition.SelectedValue) + "' ");
                //    if (fetchselectedcandidate.Rows.Count > 0)
                //    {
                //        grdselectedcandidate.DataSource = fetchselectedcandidate;
                //        grdselectedcandidate.DataBind();
                //    }
                //    else
                //    {
                //        grdselectedcandidate.DataSource = null;
                //        grdselectedcandidate.DataBind();
                //    }
                //}
                //if (ddlposition.SelectedIndex > 0 && txtcompany.Text != "" && txtcandidatename.Text == "" && txtskills.Text == "")
                //{
                //    DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected'  AND st.VacancyId='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And s.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' ");
                //    if (fetchselectedcandidate.Rows.Count > 0)
                //    {
                //        grdselectedcandidate.DataSource = fetchselectedcandidate;
                //        grdselectedcandidate.DataBind();
                //    }
                //    else
                //    {
                //        grdselectedcandidate.DataSource = null;
                //        grdselectedcandidate.DataBind();
                //    }
                //}

                //if (ddlposition.SelectedIndex > 0 && txtcompany.Text != "" && txtcandidatename.Text != "" && txtskills.Text == "")
                //{
                //    DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected'  AND st.VacancyId='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And s.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Name='" + (txtcandidatename.Text) + "'");
                //    if (fetchselectedcandidate.Rows.Count > 0)
                //    {
                //        grdselectedcandidate.DataSource = fetchselectedcandidate;
                //        grdselectedcandidate.DataBind();
                //    }
                //    else
                //    {
                //        grdselectedcandidate.DataSource = null;
                //        grdselectedcandidate.DataBind();
                //    }
                //}

                //if (ddlposition.SelectedIndex > 0 && txtcompany.Text != "" && txtcandidatename.Text != "" && txtskills.Text != "")
                //{
                //    DataTable fetchselectedcandidate = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.Status='Selected'  AND st.VacancyId='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And s.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' And c.Name='" + (txtcandidatename.Text) + "' And c.Skills='" + (txtskills.Text) + "'  ");
                //    if (fetchselectedcandidate.Rows.Count > 0)
                //    {
                //        grdselectedcandidate.DataSource = fetchselectedcandidate;
                //        grdselectedcandidate.DataBind();
                //    }
                //    else
                //    {
                //        grdselectedcandidate.DataSource = null;
                //        grdselectedcandidate.DataBind();
                //    }
                //}

                //#endregion


            //  }
            //else
            //{
            //    g.ShowMessage(this.Page, "Position is copmulsory");
            //}
            
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (tabco.ActiveTabIndex==0)
        {
            bindselectedcandidategrd();
        }
        if (tabco.ActiveTabIndex==1)
        {
            bindoffergrd();
        }

        
    }


    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

        modnopo.Hide();
    }

    protected void lnkSendOffer_Click(object sender, EventArgs e)
    {
        modnopo.Show();
        LinkButton lnkid = (LinkButton)sender;
        lblscheduleid.Text = lnkid.CommandArgument;
        fillcandidatedetailsAddofferdetails();
        txtJoiningdate.Attributes.Add("raedonly", "readonly");
    }

    private void fillcandidatedetailsAddofferdetails()
    {
        try
        {
            DataTable dtfetchcandidatvalue = g.ReturnData("Select s.ScheduleDetailsId,s.ScheduleId, s.CandidateID, s.VacancyID, s.CompanyId , co.CompanyName, v.Title AS Position , c.Name,c.Email_Address , c.Contact_No, CONVERT(varchar, st.Date , 101) AS Date, s.FromTime,s.ToTime ,  st.Vanue  , s.Status from ScheduleDetailsTB s Left outer join ScheduleTB st on s.ScheduleId=st.ScheduleId Left outer join CandidateTB c on s.CandidateId=c.Candidate_ID Left outer join VancancyTB v on s.VacancyId=v.VacancyID left outer join CompanyInfoTB co on s.CompanyId=co.CompanyId where s.ScheduleDetailsId='" + Convert.ToInt32(lblscheduleid.Text) + "'");
            if (dtfetchcandidatvalue.Rows.Count > 0)
            {
                lblcompanyname.Text=dtfetchcandidatvalue.Rows[0]["CompanyName"].ToString();
                lblname.Text = dtfetchcandidatvalue.Rows[0]["Name"].ToString();
                lblcontactno.Text = dtfetchcandidatvalue.Rows[0]["Contact_No"].ToString();
                lblinterviewdate.Text = dtfetchcandidatvalue.Rows[0]["Date"].ToString();
                lblposition.Text = dtfetchcandidatvalue.Rows[0]["Position"].ToString();
                lblemail.Text = dtfetchcandidatvalue.Rows[0]["Email_Address"].ToString();
                lblSid.Text = dtfetchcandidatvalue.Rows[0]["ScheduleId"].ToString();
                lblcompany_ID.Text = dtfetchcandidatvalue.Rows[0]["CompanyId"].ToString();
                lblcandidateIdtosave.Text = dtfetchcandidatvalue.Rows[0]["CandidateId"].ToString();
                lblvacancyid.Text = dtfetchcandidatvalue.Rows[0]["VacancyID"].ToString();

                //select 0  as DeptID, '--Select--' as DeptName union all Select distinct v.DeptID, dp.DeptName from VancancyTB v left outer join MasterDeptTB dp on v.DeptID=dp.DeptID left outer join MasterDesgTB ds on v.DesignID=ds.DesigID
               // DataTable dtfilldepart = g.ReturnData("Select distinct v.CompanyID, v.DeptID, v.DesignID, dp.DeptName , ds.DesigName from VancancyTB v left outer join MasterDeptTB dp on v.DeptID=dp.DeptID left outer join MasterDesgTB ds on v.DesignID=ds.DesigID where v.CompanyID='" + Convert.ToInt32(lblcompany_ID.Text) + "'");
               // DataTable dtfilldepart = g.ReturnData("select 0  as DeptID, '--Select--' as DeptName union all Select distinct v.DeptID, dp.DeptName from VancancyTB v left outer join MasterDeptTB dp on v.DeptID=dp.DeptID left outer join MasterDesgTB ds on v.DesignID=ds.DesigID where v.CompanyID='" + Convert.ToInt32(lblcompany_ID.Text) + "'");
                DataTable dtfilldepart = g.ReturnData("Select distinct v.DeptID, dp.DeptName from VancancyTB v left outer join MasterDeptTB dp on v.DeptID=dp.DeptID left outer join MasterDesgTB ds on v.DesignID=ds.DesigID where v.CompanyID='" + Convert.ToInt32(lblcompany_ID.Text) + "'");
               
                
                //if (dtfilldepart.Rows.Count > 0)
                //{
                    ddldepartment.DataSource = dtfilldepart;
                    ddldepartment.DataTextField = "DeptName";
                    ddldepartment.DataValueField = "DeptID";
                    ddldepartment.DataBind();


                    if (ddldepartment.SelectedIndex != -1)
                    {
                        // DataTable filldesignation = g.ReturnData("select 0  as DesignID, '--Select--' as DesigName union all Select distinct  v.DesignID,  ds.DesigName from VancancyTB v left outer join MasterDeptTB dp on v.DeptID=dp.DeptID left outer join MasterDesgTB ds on v.DesignID=ds.DesigID where dp.DeptID ='" + Convert.ToInt32(ddldepartment.SelectedValue) + "'");
                        DataTable filldesignation = g.ReturnData("Select distinct  v.DesignID,  ds.DesigName from VancancyTB v left outer join MasterDeptTB dp on v.DeptID=dp.DeptID left outer join MasterDesgTB ds on v.DesignID=ds.DesigID where dp.DeptID ='" + Convert.ToInt32(ddldepartment.SelectedValue) + "'");
                        if (filldesignation.Rows.Count > 0)
                        {
                            ddldesig.DataSource = filldesignation;
                            ddldesig.DataTextField = "DesigName";
                            ddldesig.DataValueField = "DesignID";
                            ddldesig.DataBind();
                        }
                        else
                        {
                            ddldesig.DataSource = null;
                            ddldesig.DataBind();
                        }

                    }
                //}
                //else
                //{
                //    ddldepartment.DataSource = null;
                //    ddldepartment.DataBind();
                //}

                 //  DataTable dtfillreportingemp = g.ReturnData("select 0  as EmployeeId, '--Select--' as EmployeeName union all Select  EmployeeId,  FName +' '+  MName +' '+ Lname AS EmployeeName from EmployeeTB where CompanyId='" + Convert.ToInt32(lblcompany_ID.Text) + "'");
                    DataTable dtfillreportingemp = g.ReturnData("Select  EmployeeId,  FName +' '+  MName +' '+ Lname AS EmployeeName from EmployeeTB where CompanyId='" + Convert.ToInt32(lblcompany_ID.Text) + "' AND RelivingStatus IS NULL");
                
                if (dtfillreportingemp.Rows.Count > 0)
                {
                    ddlreporting.DataSource = dtfillreportingemp;
                    ddlreporting.DataTextField = "EmployeeName";
                    ddlreporting.DataValueField = "EmployeeId";
                    ddlreporting.DataBind();
                }
                else
                {
                    ddlreporting.DataSource = null;
                    ddlreporting.DataBind();
                }
            }
            else
            {
                lblcompanyname.Text="--------------";
                lblname.Text = "--------------";
                lblcontactno.Text ="--------------";
                lblcontactno.Text ="--------------";
                lblinterviewdate.Text ="--------------";
                lblposition.Text = "--------------";
                lblemail.Text = "--------------";
                lblcompany_ID.Text = "";
                lblcandidateIdtosave.Text = "";
            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldepartment.SelectedIndex !=-1)
        {
           // DataTable filldesignation = g.ReturnData("select 0  as DesignID, '--Select--' as DesigName union all Select distinct  v.DesignID,  ds.DesigName from VancancyTB v left outer join MasterDeptTB dp on v.DeptID=dp.DeptID left outer join MasterDesgTB ds on v.DesignID=ds.DesigID where dp.DeptID ='" + Convert.ToInt32(ddldepartment.SelectedValue) + "'");
            DataTable filldesignation = g.ReturnData("Select distinct  v.DesignID,  ds.DesigName from VancancyTB v left outer join MasterDeptTB dp on v.DeptID=dp.DeptID left outer join MasterDesgTB ds on v.DesignID=ds.DesigID where dp.DeptID ='" + Convert.ToInt32(ddldepartment.SelectedValue) + "'");
            if (filldesignation.Rows.Count > 0)
            {
                    ddldesig.DataSource = filldesignation;
                    ddldesig.DataTextField = "DesigName";
                    ddldesig.DataValueField = "DesignID";
                    ddldesig.DataBind();
                }
                else
                {
                    ddldesig.DataSource = null;
                    ddldesig.DataBind();
                }
        
        }
        modnopo.Show();
    }

    protected void btnsendoffer_Click(object sender, EventArgs e)
    {
        try
        {
            CandidateOfferTB t = new CandidateOfferTB();
            t.Candidate_Id = Convert.ToInt32(lblcandidateIdtosave.Text);
            t.Schedule_Id = Convert.ToInt32(lblSid.Text);
            t.ScheduleDetailsID = Convert.ToInt32(lblscheduleid.Text);
            t.Offer_Date =Convert.ToDateTime(DateTime.UtcNow.ToShortDateString());
            t.Company_Id = Convert.ToInt32(lblcompany_ID.Text);
            t.Department_Id = Convert.ToInt32(ddldepartment.SelectedValue);
            t.Designation_Id = Convert.ToInt32(ddldesig.SelectedValue);
            t.Joining_Date =Convert.ToDateTime(txtJoiningdate.Text);
            t.Reporting_Id = Convert.ToInt32(ddlreporting.SelectedValue);
            t.Salary = Convert.ToDecimal(txtsalary.Text);
            t.ProbetionPeriod = ddlprobetion.SelectedValue;
            t.Remark = txtremark.Text;
            t.User_Id = Convert.ToInt32(Session["UserId"]);
            t.Status = "Offer";
            hr.CandidateOfferTBs.InsertOnSubmit(t);
            hr.SubmitChanges();

            ScheduleTB c = hr.ScheduleTBs.Where(s => s.ScheduleId == Convert.ToInt32(lblSid.Text)).First();

            c.Status = "Offer";
            hr.SubmitChanges();
            ScheduleDetailsTB Sc = hr.ScheduleDetailsTBs.Where(f => f.ScheduleDetailsId == Convert.ToInt32(lblscheduleid.Text)).First();
            Sc.Status = "Offer";
            hr.SubmitChanges();

            CandidateTB d = hr.CandidateTBs.Where(s => s.Candidate_ID == Convert.ToInt32(lblcandidateIdtosave.Text)).First();

            d.Status = "Offer";
            hr.SubmitChanges();

            InterviewResultTB n=hr.InterviewResultTBs.Where(s=>s.ScheduleDetailsID==Convert.ToInt32(lblscheduleid.Text)).First();
            n.InterviewStatus = "Offer";
            hr.SubmitChanges();

            InterviewResultDetailsTB r = new InterviewResultDetailsTB();
            r.InterviewResultId = n.InterviewResultId;
            r.ScheduleId = Convert.ToInt32(lblSid.Text);
            r.ScheduleDetailsID = Convert.ToInt32(lblscheduleid.Text);
            r.CompanyID = Convert.ToInt32(lblcompany_ID.Text);
            r.CandidateId = Convert.ToInt32(lblcandidateIdtosave.Text);
            r.VacancyId = Convert.ToInt32(lblvacancyid.Text);
            r.InterviewStatus = "Offer";
            r.Description = txtremark.Text;
            r.EntryBy = Convert.ToInt32(Session["UserId"]);
            hr.InterviewResultDetailsTBs.InsertOnSubmit(r);
            hr.SubmitChanges();


            lblidviewoffer.Text = t.CadidateOffer_Id.ToString();
            SendMail();
            g.ShowMessage(this.Page, "Offer Send Successfully");
            bindselectedcandidategrd();
            bindoffergrd();
            cleartexfields();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void cleartexfields()
    {
        txtsalary.Text = "";
        txtremark.Text = "";
        ddlprobetion.SelectedIndex = 0;
    }

    protected void imgVeiw_Click(object sender, ImageClickEventArgs e)
    {
        modpopup2.Show();
        ImageButton imgid = (ImageButton)sender;
        lblidviewoffer.Text = imgid.CommandArgument;
        viewofferdetails();
       
    }

    private void viewofferdetails()
    {
        try
        {
            DataTable dtbindofferdetails = g.ReturnData("Select f.CadidateOffer_Id,f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date, c.Email_Address, c.Contact_No, convert(varchar,s.Date,101) As InterviewDate, dp.DeptName, ds.DesigName, f.Salary, f.ProbetionPeriod, e.FName +' '+ e.MName +' '+ e.Lname AS ReportingName,  f.Remark from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID Left outer join ScheduleTB s on f.Schedule_Id=s.ScheduleId Left outer join MasterDeptTB dp on f.Department_Id=dp.DeptID Left outer join MasterDesgTB ds on f.Designation_Id=ds.DesigID Left outer join EmployeeTB e on f.Reporting_Id=e.EmployeeId where f.CadidateOffer_Id='" + Convert.ToInt32(lblidviewoffer.Text) + "' ");
            if (dtbindofferdetails.Rows.Count > 0)
            {
                lbloffercandidatename.Text = dtbindofferdetails.Rows[0]["Name"].ToString();
                lblofferoffercompany.Text = dtbindofferdetails.Rows[0]["CompanyName"].ToString();
                lbloffercontactno.Text = dtbindofferdetails.Rows[0]["Contact_No"].ToString();
                lblofferinterViewdate.Text = dtbindofferdetails.Rows[0]["InterviewDate"].ToString();
                lblofferposition.Text = dtbindofferdetails.Rows[0]["Title"].ToString();
                lblofferemail.Text = dtbindofferdetails.Rows[0]["Email_Address"].ToString();
                lblDept.Text = dtbindofferdetails.Rows[0]["DeptName"].ToString();
                lblDesig.Text = dtbindofferdetails.Rows[0]["DesigName"].ToString();
                lblofferdate.Text = dtbindofferdetails.Rows[0]["offer_Date"].ToString();
                lbljoiningDate.Text = dtbindofferdetails.Rows[0]["Joining_Date"].ToString();
                lblreporting.Text = dtbindofferdetails.Rows[0]["ReportingName"].ToString();
                lblsalary.Text = dtbindofferdetails.Rows[0]["Salary"].ToString();
                lblprobationperiod.Text = dtbindofferdetails.Rows[0]["ProbetionPeriod"].ToString();
                lblremark.Text = dtbindofferdetails.Rows[0]["Remark"].ToString();
            }
            else
            {
                lbloffercandidatename.Text = "---------------";
                lblofferoffercompany.Text = "---------------";
                lbloffercontactno.Text = "---------------";
                lblofferinterViewdate.Text = "---------------";
                lblofferposition.Text = "---------------";
                lblofferemail.Text = "---------------";
                lblDept.Text = "---------------";
                lblDesig.Text = "---------------";
                lblofferdate.Text = "---------------";
                lbljoiningDate.Text = "---------------";
                lblreporting.Text = "---------------";
                lblsalary.Text = "---------------";
                lblprobationperiod.Text = "---------------";
                lblremark.Text = "---------------";
            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    #region Mail after new employee registration
    private void SendMail()
    {
        try
        {

            string EM1 = lblemail.Text;
            string[] tos = EM1.Split('@');
            for (int i = 0; i < tos.Length; i++)
            {
                if (tos[i].ToString() == "rediffmail.com")
                {
                    SmtpRediffMail();
                }
                if (tos[i].ToString() == "gmail.com")
                {
                    SmtpGmail();
                }
                if (tos[i].ToString() == "yahoo.com")
                {
                    SmtpYahoo();
                }
                if (tos[i].ToString() == "hotmail.com")
                {
                    SmtpHotMail();
                }
                if (tos[i].ToString() == "excellenceit.in")
                {
                    SmtpExcellence();
                }

            }

        }
        catch (Exception ex)
        {
            string str = "Send Email Failed." + ex.Message;
        }
    }

    private void SmtpExcellence()
    {
        try
        {
            SMTPSettingsTB smtpData = hr.SMTPSettingsTBs.FirstOrDefault();
            // DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblemail.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress(smtpData.emailFromAddress);
            mail.Subject = "Offer Details";
            grddisp.DataSource = tbloffer;
            //mail.Body = GridViewToHtml(grddisp);
            BindMailGrd();
            mail.Body = "Dear  " + lblname.Text + "," + "<br/>" + "<br/>" + "  Your offer details as follow :" + "<br/>" + "<br/>" + GetGridviewData();
           
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();   smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential(smtpData.emailFromAddress,smtpData.emailFromPassword);

            smtp.EnableSsl = true;
         
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            string str = "Send Email Failed." + ex.Message;
        }
    }

    private void SmtpHotMail()
    {
        try
        {
            SMTPSettingsTB smtpData = hr.SMTPSettingsTBs.FirstOrDefault();
            // DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblemail.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress(smtpData.emailFromAddress);
            grddisp.DataSource = tbloffer;
            //mail.Body = GridViewToHtml(grddisp);
            BindMailGrd();
            mail.Body = "Dear  " + lblname.Text + "," + "<br/>" + "<br/>" + "  Your offer details as follow :" + "<br/>" + "<br/>" + GetGridviewData();
           
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.live.com";
            smtp.Credentials = new System.Net.NetworkCredential(smtpData.emailFromAddress,smtpData.emailFromPassword);

            smtp.EnableSsl = true;
            smtp.Port = 25;
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            string str = "Send Email Failed." + ex.Message;
        }
    }

    private void SmtpYahoo()
    {
        try
        {
            SMTPSettingsTB smtpData = hr.SMTPSettingsTBs.FirstOrDefault();
            // DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblemail.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress(smtpData.emailFromAddress);
            grddisp.DataSource = tbloffer;
            //mail.Body = GridViewToHtml(grddisp);
            BindMailGrd();
            mail.Body = "Dear  " + lblname.Text + "," + "<br/>" + "<br/>" + "  Your offer details as follow :" + "<br/>" + "<br/>" + GetGridviewData();
           
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.mail.yahoo.com";
            smtp.Credentials = new System.Net.NetworkCredential(smtpData.emailFromAddress,smtpData.emailFromPassword);

            smtp.EnableSsl = true;
            smtp.Port = 25;
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            string str = "Send Email Failed." + ex.Message;
        }
    }

    private void SmtpGmail()
    {
        try
        {
            SMTPSettingsTB smtpData = hr.SMTPSettingsTBs.FirstOrDefault();
            //DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblemail.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress(smtpData.emailFromAddress);
            mail.Subject = "Offer Details";
            grddisp.DataSource = tbloffer;
            //mail.Body = GridViewToHtml(grddisp);
            BindMailGrd();
            mail.Body = "Dear  " + lblname.Text + "," + "<br/>" + "<br/>" + "  Your offer details as follow :" + "<br/>" + "<br/>"+ GetGridviewData();
           
            mail.IsBodyHtml = true;
            

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential(smtpData.emailFromAddress,smtpData.emailFromPassword);

            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            string str = "Send Email Failed." + ex.Message;
        }
    }
    private void SmtpRediffMail()
    {
        try
        {
            SMTPSettingsTB smtpData = hr.SMTPSettingsTBs.FirstOrDefault();
            //DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblemail.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress(smtpData.emailFromAddress);
            grddisp.DataSource = tbloffer;
            //mail.Body = GridViewToHtml(grddisp);
            BindMailGrd();
            mail.Body = "Dear  " + lblname.Text + "," + "<br/>" + "<br/>" + "  Your offer details as follow :" + "<br/>" + "<br/>" + GetGridviewData();
           
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.mail.rediffmail.com";
            smtp.Credentials = new System.Net.NetworkCredential(smtpData.emailFromAddress,smtpData.emailFromPassword);

            smtp.EnableSsl = true;
            smtp.Port = 25;
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            string str = "Send Email Failed." + ex.Message;
        }
    }

    private string GetGridviewData()
    {
        //try
        //{
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        if (tbloffer != null)
        {
            grddisp.DataSource = tbloffer;
            grddisp.DataBind();
        }
        else
        {
            BindMailGrd();
        }
        //BindMailGrd();
        grddisp.AllowPaging = false;
        grddisp.RenderControl(hw);

        return sb.ToString();
        //}
        //catch (Exception ex)
        //{

        //    g.ShowMessage(this.Page, ex.Message);
        //}
    }

    private void BindMailGrd()
    {
        try
        {
        
            DataSet dt = g.ReturnData1("Select f.CadidateOffer_Id,f.Schedule_Id  ,co.CompanyName, v.Title, c.Name, convert(varchar, f.Joining_Date,101) AS Joining_Date, CONVERT(varchar, f.offer_Date, 101) AS offer_Date, c.Email_Address, c.Contact_No, convert(varchar,s.Date,101) As InterviewDate, dp.DeptName, ds.DesigName, f.Salary, f.ProbetionPeriod, e.FName +' '+ e.MName +' '+ e.Lname AS ReportingName,  f.Remark from CandidateOfferTB f Left outer join CandidateTB c on f.Candidate_Id=c.Candidate_Id Left outer join CompanyInfoTB co on f.Company_Id=co.CompanyId Left outer join VancancyTB v on c.Vaccancy_ID=v.VacancyID Left outer join ScheduleTB s on f.Schedule_Id=s.ScheduleId Left outer join MasterDeptTB dp on f.Department_Id=dp.DeptID Left outer join MasterDesgTB ds on f.Designation_Id=ds.DesigID Left outer join EmployeeTB e on f.Reporting_Id=e.EmployeeId where f.CadidateOffer_Id='" + Convert.ToInt32(lblidviewoffer.Text) + "' ");
                        if (dt.Tables[0].Rows.Count==0)
                        {
                            
                        }
                        else
                        {
                        dr = tbloffer.NewRow();
                        dr[0] = dt.Tables[0].Rows[0]["Name"];
                        dr[1] = dt.Tables[0].Rows[0]["CompanyName"];
                        dr[2] = dt.Tables[0].Rows[0]["Title"];
                        dr[3] = dt.Tables[0].Rows[0]["Contact_No"];
                        dr[4] = dt.Tables[0].Rows[0]["InterviewDate"];
                        dr[5] = dt.Tables[0].Rows[0]["offer_Date"];
                        dr[6] = dt.Tables[0].Rows[0]["Joining_Date"];
                        dr[7] = dt.Tables[0].Rows[0]["ProbetionPeriod"];
                        dr[8] = dt.Tables[0].Rows[0]["DeptName"];
                        dr[9] = dt.Tables[0].Rows[0]["DesigName"];
                        dr[10] = dt.Tables[0].Rows[0]["ReportingName"];
                        dr[11] = dt.Tables[0].Rows[0]["Email_Address"];
                        dr[12] = dt.Tables[0].Rows[0]["Salary"];
                        dr[13] = dt.Tables[0].Rows[0]["Remark"];
                       

                        tbloffer.Rows.Add(dr);
                        Session["id"] = tbloffer;
                        // new grid for Mail
                        grddisp.DataSource = tbloffer;
                        grddisp.DataBind();
                        }
                  }
        catch (Exception)
        {
            
            throw;
        }
    }

    
    #endregion mail function


    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }



    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        modpopup2.Hide();
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
    public static List<string> GetCompletionListSkills(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> skills = (from d in hr.CandidateTBs
                                  .Where(r => r.Skills.Contains(prefixText))
                               select d.Skills).Distinct().ToList();
        return skills;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListCandidateName(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        string st = contextKey;
        //string[] str = st.Split(',');

        //List<string> candidateName = (from d in hr.CandidateTBs
        //                          .Where(r => r.Name.Contains(prefixText) && r.Company_ID==Convert.ToInt32(str[0].ToString())  && r.Vaccancy_ID==Convert.ToInt32(str[1].ToString()))
        //                       select d.Name).Distinct().ToList();


        List<string> candidateName = (from d in hr.CandidateTBs
                                 .Where(r => r.Name.Contains(prefixText) )
                                      select d.Name).Distinct().ToList();
        return candidateName;
    }

    protected void txtcompany_TextChanged(object sender, EventArgs e)
    {
        if (txtcompany.Text != "")
        {
            var findcompanyID = from d in hr.CompanyInfoTBs
                                where d.CompanyName == txtcompany.Text
                                select new { d.CompanyId };
            if (findcompanyID.Count() > 0)
            {
                foreach (var item in findcompanyID)
                {
                    lblcompaniId.Text = item.CompanyId.ToString();
                }
                fillposition();
            }
            else
            {
                lblcompaniId.Text = "";
            }

        }
    }



    protected void grdselectedcandidate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdselectedcandidate.PageIndex = e.NewPageIndex;
        bindselectedcandidategrd();
    }
    protected void grdoffered_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdoffered.PageIndex = e.NewPageIndex;
        bindoffergrd();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cleartexfields();
        modnopo.Hide();
    }

    protected void txtJoiningdate_TextChanged(object sender, EventArgs e)
    {
        if (txtJoiningdate.Text != "" && lblinterviewdate.Text != "")
        {
            //string URL = "http://www.google.com";
            //System.Net.HttpWebRequest rq2 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
            //System.Net.HttpWebResponse res2 = (System.Net.HttpWebResponse)rq2.GetResponse();
            //DateTime Date = DateTime.Parse(res2.Headers["Date"]);
            //string Servedate = Date.ToString("MM/dd/yyyy");
            DateTime IntervDate = DateTime.Parse(lblinterviewdate.Text);
            DateTime Date = DateTime.Now;
            string JONDate = Convert.ToDateTime(txtJoiningdate.Text).ToString();
            DateTime dt = DateTime.Parse(txtJoiningdate.Text);
            if (IntervDate > dt)
            {
                txtJoiningdate.Text = "";
                g.ShowMessage(this.Page, "Compare Interview Date and Joining Date");
            }

            else
            {

                //if (Date > dt)
                //{
                //    txtJoiningdate.Text = "";
                //    g.ShowMessage(this.Page, "Joining Date is Greater Than Current Date");
                //}
                //else
                //{

                //}
            }
            modnopo.Show();
        }
    }


    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupRejectoffer.Hide();
        txtrejectRemark.Text = "";
        lblrejectDate.Text = "";
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupJoin.Hide();
        txtjoinremark.Text = "";
        txtJoiningdate.Text = "";
    }


    protected void lnkRejectOffer_Click(object sender, EventArgs e)
    {
        ModalPopupRejectoffer.Show();
        LinkButton lnkid = (LinkButton)sender;
        lblidviewoffer.Text = lnkid.CommandArgument;
        findschIdDetailID();
        lblrejectDate.Text = DateTime.Now.ToShortDateString();
        
    }

    private void findschIdDetailID()
    {
        try
        {
            var findschedulDetailsID = from d in hr.CandidateOfferTBs
                                       where d.CadidateOffer_Id == Convert.ToInt32(lblidviewoffer.Text)
                                       select new { d.Schedule_Id, d.ScheduleDetailsID,d.Candidate_Id ,d.Company_Id};
            if (findschedulDetailsID.Count() > 0)
            {
                foreach (var item in findschedulDetailsID)
                {
                    HiddenFieldSchedulID.Value = item.Schedule_Id.ToString();
                    HiddenFieldSchedulDetailsID.Value = item.ScheduleDetailsID.ToString();
                    HiddenFieldCandidateId.Value = item.Candidate_Id.ToString();
                    HiddenFieldCompanyID.Value = item.Company_Id.ToString();
                }

            }

            if (HiddenFieldSchedulDetailsID.Value !="")
            {
                var findvacancyid = from d in hr.CandidateTBs
                                    where d.Candidate_ID == Convert.ToInt32(HiddenFieldCandidateId.Value)
                                    select new { d.Vaccancy_ID};
                if (findvacancyid.Count() > 0)
                {
                    foreach (var item in findvacancyid)
                    {
                        HiddenFieldVacancyID.Value = item.Vaccancy_ID.ToString();
                    }
                }

            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void lnkJoin_Click(object sender, EventArgs e)
    {
        try
        {
         ModalPopupJoin.Show();
        LinkButton lnkid = (LinkButton)sender;
        lblidviewoffer.Text = lnkid.CommandArgument;
        txtjoindate.Attributes.Add("readonly", "readonly");
        findschIdDetailID();

        var offerdattedata = from d in hr.CandidateOfferTBs
                             where d.CadidateOffer_Id == Convert.ToInt32(lblidviewoffer.Text)
                             select new {d.Offer_Date };
        if (offerdattedata.Count() > 0)
        {
            foreach (var item in offerdattedata)
            {
                HiddenFieldofferDate.Value = item.Offer_Date.ToString();
            }
        }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    protected void btnreject_Click(object sender, EventArgs e)
    {
        try
        {
            CandidateOfferTB t = hr.CandidateOfferTBs.Where(s => s.CadidateOffer_Id == Convert.ToInt32(lblidviewoffer.Text)).First();
            t.Reject_Remarks = txtrejectRemark.Text;
            t.Reject_Date =Convert.ToDateTime(DateTime.Now.ToShortDateString());
            t.Status = "Offer Rejected";
            t.User_Id = Convert.ToInt32(Session["UserId"]);
            hr.SubmitChanges();

            ScheduleTB c = hr.ScheduleTBs.Where(s => s.ScheduleId == Convert.ToInt32(HiddenFieldSchedulID.Value)).First();
            c.Status = "Offer Rejected";
            hr.SubmitChanges();

            ScheduleDetailsTB Sc = hr.ScheduleDetailsTBs.Where(f => f.ScheduleDetailsId == Convert.ToInt32(HiddenFieldSchedulDetailsID.Value)).First();
            Sc.Status = "Offer Rejected";
            hr.SubmitChanges();

            CandidateTB d = hr.CandidateTBs.Where(s => s.Candidate_ID == Convert.ToInt32(HiddenFieldCandidateId.Value)).First();

            d.Status = "Offer Rejected";
            hr.SubmitChanges();

            InterviewResultTB n = hr.InterviewResultTBs.Where(s => s.ScheduleDetailsID == Convert.ToInt32(HiddenFieldSchedulDetailsID.Value)).First();
            n.InterviewStatus = "Offer Rejected";
            hr.SubmitChanges();

            InterviewResultDetailsTB r = new InterviewResultDetailsTB();
            r.InterviewResultId = n.InterviewResultId;
            r.ScheduleId = Convert.ToInt32(HiddenFieldSchedulID.Value);
            r.ScheduleDetailsID = Convert.ToInt32(HiddenFieldSchedulDetailsID.Value);

            r.CompanyID = Convert.ToInt32(HiddenFieldCompanyID.Value);
            r.CandidateId = Convert.ToInt32(HiddenFieldCandidateId.Value);

            r.VacancyId = Convert.ToInt32(HiddenFieldVacancyID.Value);

            r.InterviewStatus = "Offer Rejected";

            r.Description = txtremark.Text;
            r.EntryBy = Convert.ToInt32(Session["UserId"]);
            hr.InterviewResultDetailsTBs.InsertOnSubmit(r);
            hr.SubmitChanges();
            g.ShowMessage(this.Page, "Offer Rejected Successfully");
            bindselectedcandidategrd();
            bindoffergrd();
            txtrejectRemark.Text = "";
            lblrejectDate.Text = "";
            ModalPopupRejectoffer.Hide();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }


    protected void btnjoin_Click(object sender, EventArgs e)
    {
         try
        {
            CandidateOfferTB t = hr.CandidateOfferTBs.Where(s => s.CadidateOffer_Id == Convert.ToInt32(lblidviewoffer.Text)).First();
            t.JoinRemark = txtjoinremark.Text;
            t.CandidateJoin_Date =Convert.ToDateTime(txtjoindate.Text);
            t.Status = "Join";
            t.User_Id = Convert.ToInt32(Session["UserId"]);
            hr.SubmitChanges();

            ScheduleTB c = hr.ScheduleTBs.Where(s => s.ScheduleId == Convert.ToInt32(HiddenFieldSchedulID.Value)).First();
            c.Status = "Join";
            hr.SubmitChanges();

            ScheduleDetailsTB Sc = hr.ScheduleDetailsTBs.Where(f => f.ScheduleDetailsId == Convert.ToInt32(HiddenFieldSchedulDetailsID.Value)).First();
            Sc.Status = "Join";
            hr.SubmitChanges();

            CandidateTB d = hr.CandidateTBs.Where(s => s.Candidate_ID == Convert.ToInt32(HiddenFieldCandidateId.Value)).First();

            d.Status = "Join";
            hr.SubmitChanges();

            InterviewResultTB n = hr.InterviewResultTBs.Where(s => s.ScheduleDetailsID == Convert.ToInt32(HiddenFieldSchedulDetailsID.Value)).First();
            n.InterviewStatus = "Join";
            hr.SubmitChanges();

            InterviewResultDetailsTB r = new InterviewResultDetailsTB();
            r.InterviewResultId = n.InterviewResultId;
            r.ScheduleId = Convert.ToInt32(HiddenFieldSchedulID.Value);
            r.ScheduleDetailsID = Convert.ToInt32(HiddenFieldSchedulDetailsID.Value);

            r.CompanyID = Convert.ToInt32(HiddenFieldCompanyID.Value);
            r.CandidateId = Convert.ToInt32(HiddenFieldCandidateId.Value);

            r.VacancyId = Convert.ToInt32(HiddenFieldVacancyID.Value);

            r.InterviewStatus = "Join";

            r.Description = txtremark.Text;
            r.EntryBy = Convert.ToInt32(Session["UserId"]);
            hr.InterviewResultDetailsTBs.InsertOnSubmit(r);
            hr.SubmitChanges();
            g.ShowMessage(this.Page, "Candidate Joined Successfully");
            bindselectedcandidategrd();
            bindoffergrd();
            txtjoindate.Text = "";
            txtjoinremark.Text = "";
            ModalPopupJoin.Hide();
        }
         catch (Exception ex)
         {

             g.ShowMessage(this.Page, ex.Message);
         }

    }
    protected void btncancelreject_Click(object sender, EventArgs e)
    {

        ModalPopupRejectoffer.Hide();
        txtrejectRemark.Text = "";
        lblrejectDate.Text = "";

       
    }
   
    protected void btnjoincancel_Click(object sender, EventArgs e)
    {
        ModalPopupJoin.Hide();
        txtjoinremark.Text = "";
        txtJoiningdate.Text = "";
    }

    protected void txtjoindate_TextChanged(object sender, EventArgs e)
    {
        ModalPopupJoin.Show();
        if ( HiddenFieldofferDate.Value !="" && txtjoindate.Text !="")
        {
            DateTime offerDat = Convert.ToDateTime(HiddenFieldofferDate.Value);
            DateTime joinDat = Convert.ToDateTime(txtjoindate.Text);
            if (offerDat > joinDat)
            {
                txtjoindate.Text = "";
                g.ShowMessage(this.Page, "Compare Offer Date and Joining Date");
            }
        }
    }

    protected void ddlposition_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlposition.SelectedIndex > 0)
        {
            txtcandidatename_AutoCompleteExtender.ContextKey =lblcompaniId.Text+"," + ddlposition.SelectedValue;
        }
    }
}