using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Recruitment_SearchCandidateList : System.Web.UI.Page
{
    /// <summary>
    /// Searching Candidate List Form
    /// Created By Abdul Rahman
    /// Created Date 13/11/2014
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region Declear Variable
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable DTEducation = new DataTable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["UserId"] != null)
        {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            fillposition();
            if (ddlposition.SelectedIndex==0)
            {
                bindcandidategrd();
            }
        }
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
            var fillpositiodata = from d in hr.VancancyTBs
                                  select new {d.VacancyID, d.Title };
            ddlposition.DataSource = fillpositiodata;
            ddlposition.DataTextField = "Title";
            ddlposition.DataValueField = "VacancyID";
            ddlposition.DataBind();
            ddlposition.Items.Insert(0,"All");
            
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }


    protected void btnsearch_Click(object sender, EventArgs e)
    {
        bindcandidategrd();
    }

    private void bindcandidategrd()
    {
        try
        {
            DataTable dtsearchcadidate = new DataTable();


            if (ddlposition.SelectedIndex !=-1)
            {


                #region display all record and all sort by with all position
            if (ddlposition.SelectedIndex ==0 && txtexpereince.Text=="" && txtsalary.Text=="" && txtskills.Text=="" && txtcompany.Text=="")
            {
                dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId ");
            
            }
                // All with expereince
            if (ddlposition.SelectedIndex == 0 && txtexpereince.Text != "" && txtsalary.Text == "" && txtskills.Text == "" && txtcompany.Text == "")
            {
                dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Total_Expereince='" + txtexpereince.Text + "' ");
               
            }

            // All with only salary
            if (ddlposition.SelectedIndex == 0 && txtexpereince.Text == "" && txtsalary.Text != "" && txtskills.Text == "" && txtcompany.Text == "")
            {
                dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.CTC='" + txtsalary.Text + "' ");

            }

            // All with only skills
            if (ddlposition.SelectedIndex == 0 && txtexpereince.Text == "" && txtsalary.Text == "" && txtskills.Text != "" && txtcompany.Text == "")
            {
                dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Skills='" + txtskills.Text + "' ");

            }
            // All with only Company
            if (ddlposition.SelectedIndex == 0 && txtexpereince.Text == "" && txtsalary.Text == "" && txtskills.Text == "" && txtcompany.Text != "")
            {
                dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Company_ID='" + Convert.ToInt32(lblcompaniId.Text) + "' ");

            }




             // All with expereince and salary
            if (ddlposition.SelectedIndex == 0 && txtexpereince.Text != "" && txtsalary.Text != "" && txtskills.Text == "" && txtcompany.Text == "")
            {
                dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Total_Expereince='" + txtexpereince.Text + "' And c.CTC='" + txtsalary.Text + "' ");
               
            }
            // All with expereince, salary  and skills
            if (ddlposition.SelectedIndex == 0 && txtexpereince.Text != "" && txtsalary.Text != "" && txtskills.Text != "" && txtcompany.Text == "")
            {
                dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Total_Expereince='" + txtexpereince.Text + "' And c.CTC='" + txtsalary.Text + "' And c.Skills='" + txtskills.Text + "' ");
               
            }
            // All with expereince, salary, skills and company
            if (ddlposition.SelectedIndex == 0 && txtexpereince.Text != "" && txtsalary.Text != "" && txtskills.Text != "" && txtcompany.Text != "")
            {
                dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Total_Expereince='" + txtexpereince.Text + "' And c.CTC='" + txtsalary.Text + "' And c.Skills='" + txtskills.Text + "' And c.Company_ID='" + Convert.ToInt32(lblcompaniId.Text) + "' ");
                
            }
            if (dtsearchcadidate.Rows.Count > 0)
            {
                grdcandidatelist.DataSource = dtsearchcadidate;
                grdcandidatelist.DataBind();
                lblcount.Text = dtsearchcadidate.Rows.Count.ToString();
            }
            else
            {
                grdcandidatelist.DataSource = null;
                grdcandidatelist.DataBind();
                lblcount.Text = "0";

            }
                #endregion

                #region position wise
                if (ddlposition.SelectedIndex > 0 && txtexpereince.Text == "" && txtsalary.Text == "" && txtskills.Text == "" && txtcompany.Text == "")
                {
                dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' ");
                
                }
              

                if (ddlposition.SelectedIndex > 0 && txtexpereince.Text != "" && txtsalary.Text == "" && txtskills.Text == "" && txtcompany.Text == "")
                {
                    // position & expereince wise search
                    dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And c.Total_Expereince='" + txtexpereince.Text + "' ");
                    
                }

               // All with only salary
                if (ddlposition.SelectedIndex > 0 && txtexpereince.Text == "" && txtsalary.Text != "" && txtskills.Text == "" && txtcompany.Text == "")
                {
                    dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' and c.CTC='" + txtsalary.Text + "' ");

                }

                // All with only skills
                if (ddlposition.SelectedIndex > 0 && txtexpereince.Text == "" && txtsalary.Text == "" && txtskills.Text != "" && txtcompany.Text == "")
                {
                    dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' and c.Skills='" + txtskills.Text + "' ");

                }
                // All with only Company
                if (ddlposition.SelectedIndex > 0 && txtexpereince.Text == "" && txtsalary.Text == "" && txtskills.Text == "" && txtcompany.Text != "")
                {
                    dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' and c.Company_ID='" + Convert.ToInt32(lblcompaniId.Text) + "' ");

                }



                if (ddlposition.SelectedIndex > 0 && txtexpereince.Text != "" && txtsalary.Text != "" && txtskills.Text == "" && txtcompany.Text == "")
                {
                    // position & expereince And salary wise search
                    dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And c.Total_Expereince='" + txtexpereince.Text + "' and c.CTC='" + txtsalary.Text + "' ");
                   
                }
                if (ddlposition.SelectedIndex > 0 && txtexpereince.Text != "" && txtsalary.Text != "" && txtskills.Text != "" && txtcompany.Text == "")
                {
                    // position & expereince,salary and Skills wise search
                    dtsearchcadidate = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And c.Total_Expereince='" + txtexpereince.Text + "' and c.CTC='" + txtsalary.Text + "' And c.Skills='" + txtskills.Text + "' ");
                   
                }

                if (ddlposition.SelectedIndex > 0 && txtexpereince.Text != "" && txtsalary.Text != "" && txtskills.Text != "" && txtcompany.Text != "")
                {
                    // position & expereince,salary and Skills wise search
                    DataTable dtsearchwithPosition = g.ReturnData("Select  c.Candidate_ID,CompanyInfoTB.CompanyName , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId where c.Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' And c.Total_Expereince='" + txtexpereince.Text + "' and c.CTC='" + txtsalary.Text + "' And c.Skills='" + txtskills.Text + "' And c.Company_ID='" + Convert.ToInt32(lblcompaniId.Text) + "' ");
                    
                }

                if (dtsearchcadidate.Rows.Count > 0)
                {
                    grdcandidatelist.DataSource = dtsearchcadidate;
                    grdcandidatelist.DataBind();
                    lblcount.Text = dtsearchcadidate.Rows.Count.ToString();
                }
                else
                {
                    grdcandidatelist.DataSource = null;
                    grdcandidatelist.DataBind();
                    lblcount.Text = "0";

                }
                #endregion

                


            }
            else
            {
                g.ShowMessage(this.Page, "Position is copmulsory");
            }
           
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionList(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> Expereince = (from d in hr.CandidateTBs
                                        .Where(r => r.Total_Expereince.Contains(prefixText))
                                     select d.Total_Expereince).Distinct().ToList();
        return Expereince;

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListsalary(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> salary = (from d in hr.CandidateTBs
                                       .Where(r => r.CTC.Contains(prefixText))
                                   select d.CTC).Distinct().ToList();
        return salary;
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
    public static List<string> GetCompletionListCompany(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> company = (from d in hr.CompanyInfoTBs
                                  .Where(r => r.CompanyName.Contains(prefixText))
                               select d.CompanyName).Distinct().ToList();
        return company;
    }

    protected void txtcompany_TextChanged(object sender, EventArgs e)
    {
        if (txtcompany.Text != "")
        {
            var findcompanyID = from d in hr.CompanyInfoTBs
                                where d.CompanyName == txtcompany.Text
                                select new {d.CompanyId };
            if (findcompanyID.Count() > 0)
            {
                foreach (var item in findcompanyID)
                {
                    lblcompaniId.Text = item.CompanyId.ToString();
                }
            }
            else
            {
                lblcompaniId.Text = "";
            }

        }
    }
    protected void Imagedownloadfile_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

      
        ImageButton lnkbtn = sender as ImageButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        string filePath = grdcandidatelist.DataKeys[gvrow.RowIndex].Value.ToString();

        var dowlloaddata = from dt in hr.CandidateTBs
                   where dt.Candidate_ID == Convert.ToInt32(filePath)
                   select new { dt.Resume_Path };
        foreach (var item in dowlloaddata)
        {
            lbup.Text = item.Resume_Path;
        }
        if (lbup.Text!="")
        {
        
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + lbup.Text);
        Response.TransmitFile(Server.MapPath("CandidateResume/" + lbup.Text));
        Response.End();

        }
        else
        {
            g.ShowMessage(this.Page, "There is no file to download");
        }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    #region View all details of candidate
    protected void Imageviewmoredetails_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgviewId = sender as ImageButton;
        lblviewId.Text = imgviewId.CommandArgument;
        modnopo.Show();
        tbl1.Attributes.Add("display","block");
        viewFun();
    }

    private void viewFun()
    {
        try
        {
            DataTable fetchallfields = g.ReturnData("Select  c.Candidate_ID, c.CandidateId_No , CompanyInfoTB.CompanyName, Cs.StateName AS Cstate, Cct.CityName As Ccity ,Ps.StateName As PState ,Pct.CityName AS PCity , CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB, c.Religion, c.Name, case when c.Gender=0 then 'Male' else 'Female' end AS Gender, case when c.Marital_Status=0 then 'Single' else 'Married' end AS Marital_Status, c.CTC, c.ExpectedCTC, c.Total_Expereince, c.Total_Month_Exp , c.Relevent_Expereince , c.Relevent_Month_Exp, c.NoticePeriod, c.Skills, c.Refrence_By, c.C_zipcode ,c.C_Landmark, c.P_ZipCode , c.P_Landmark , c.Contact_No,c.Email_Address from CandidateTB c  Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID  Left outer join CompanyInfoTB on c.Company_ID=CompanyInfoTB.CompanyId Left outer join StateTB Cs on c.C_State=Cs.StateId Left outer join CityTB Cct on c.C_City=Cct.CityId Left outer join StateTB Ps on c.P_State=Ps.StateId Left outer join CityTB Pct on c.P_City=Pct.CityId where c.Candidate_ID='" + Convert.ToInt32(lblviewId.Text) + "'");
            if (fetchallfields.Rows.Count > 0)
            {
                lblcompanyname.Text = fetchallfields.Rows[0]["CompanyName"].ToString();
                lblposition.Text = fetchallfields.Rows[0]["Vacancy"].ToString();
                lblcandidateidview.Text = fetchallfields.Rows[0]["CandidateId_No"].ToString();
                lblname.Text = fetchallfields.Rows[0]["Name"].ToString();
                lblmeritalstatus.Text = fetchallfields.Rows[0]["Marital_Status"].ToString();
                lblDob.Text = fetchallfields.Rows[0]["DOB"].ToString();
                lblreligion.Text = fetchallfields.Rows[0]["Religion"].ToString();
                lblgender.Text = fetchallfields.Rows[0]["Gender"].ToString();
                lblnationality.Text = fetchallfields.Rows[0]["Nationality"].ToString();
                lblcontactno.Text = fetchallfields.Rows[0]["Contact_No"].ToString();

                lblemail.Text = fetchallfields.Rows[0]["Email_Address"].ToString();
                lblCstate.Text = fetchallfields.Rows[0]["Cstate"].ToString();
                lblCcity.Text = fetchallfields.Rows[0]["Ccity"].ToString();
                lblCzipcode.Text = fetchallfields.Rows[0]["C_zipcode"].ToString();
                lblClandmark.Text = fetchallfields.Rows[0]["C_Landmark"].ToString();

                lblPstate.Text = fetchallfields.Rows[0]["PState"].ToString();
                lblPcity.Text = fetchallfields.Rows[0]["PCity"].ToString();
                lblPzipcode.Text = fetchallfields.Rows[0]["P_ZipCode"].ToString();
                lblPlandmark.Text = fetchallfields.Rows[0]["P_Landmark"].ToString();

                lblyearexp.Text = fetchallfields.Rows[0]["Total_Expereince"].ToString();
                lblmonthexp.Text = fetchallfields.Rows[0]["Total_Month_Exp"].ToString();
                lblrelYear.Text = fetchallfields.Rows[0]["Relevent_Expereince"].ToString();
                lblrelmonth.Text = fetchallfields.Rows[0]["Relevent_Month_Exp"].ToString();

                lblnoticeperiod.Text = fetchallfields.Rows[0]["NoticePeriod"].ToString();
                lblskills.Text = fetchallfields.Rows[0]["Skills"].ToString();
                lblCTC.Text = fetchallfields.Rows[0]["CTC"].ToString();
                lblexpectedctc.Text = fetchallfields.Rows[0]["ExpectedCTC"].ToString();
                lblrefrenceby.Text = fetchallfields.Rows[0]["Refrence_By"].ToString();
                
               

            }
            if (lblviewId.Text !="")
            {
                var fetchdocumentdata = from d in hr.CandidateEduTBs
                                        where d.Candidate_ID == Convert.ToInt32(lblviewId.Text)
                                        select new 
                                        {
                                            d.Candidate_ID,   
                                            d.Education_Name,
                                            d.Passing_Year, 
                                            d.Percentage, 
                                            d.University,
                                            d.Education_ID 
                                        };
                if (fetchdocumentdata.Count() > 0)
                {
                    grdeducationview.DataSource = fetchdocumentdata;
                    grdeducationview.DataBind();
                }
                else
                {
                    grdeducationview.DataSource = null;
                    grdeducationview.DataBind();
                }
            }



        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    #endregion
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        modnopo.Hide();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtexpereince.Text = txtsalary.Text = txtskills.Text = txtcompany.Text = string.Empty;
    }
    protected void grdcandidatelist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcandidatelist.PageIndex = e.NewPageIndex;
        bindcandidategrd();
    }
}