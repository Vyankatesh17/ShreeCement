using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddCandidateForEmployee : System.Web.UI.Page
{
    /// <summary>
    /// Add Candidate
    /// Created By : Abdul Rahman
    /// Created Date : 25/11/2014
    /// </summary>
    #region Declear Variable
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    string AttachPath;
    string path = "";
    DataTable DTEducation = new DataTable();
    DataTable DTExperience = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                EMPNO();
                // fillvacancy();
                fillstate();
                bindcandidatelist();
                bindCompany();
                fillreferenceName();
                txtdob.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void bindCompany()
    {
        try
        {
            var dt = from p in hr.CompanyInfoTBs
                     select new { p.CompanyName, p.CompanyId };
            if (dt.Count() > 0)
            {
                ddlComp.DataSource = dt;
                ddlComp.DataTextField = "CompanyName";
                ddlComp.DataValueField = "CompanyId";
                ddlComp.DataBind();
                //ddlComp.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlComp.Items.Clear();
                ddlComp.DataSource = null;
                ddlComp.DataBind();

                ddlvaccancy.Items.Clear();
                ddlvaccancy.DataSource = null;
                ddlvaccancy.DataBind();
                // ddlComp.Items.Insert(0, "--Select--");
            }

            if (ddlComp.SelectedIndex != -1)
            {

                var dtdept = from p in hr.VancancyTBs
                             where p.VacancyStatus == "Approved" && p.CompanyID == Convert.ToInt32(ddlComp.SelectedValue)
                             select new { p.Title, p.VacancyID };
                if (dt.Count() > 0)
                {
                    ddlvaccancy.DataSource = dtdept;
                    ddlvaccancy.DataTextField = "Title";
                    ddlvaccancy.DataValueField = "VacancyID";
                    ddlvaccancy.DataBind();
                    ddlvaccancy.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddlvaccancy.Items.Clear();
                    ddlvaccancy.DataSource = null;
                    ddlvaccancy.DataBind();

                }

            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }


    protected void ddlComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillvacancy();
    }
    private void bindcandidatelist()
    {
        try
        {
            var registratindata = from d in hr.RegistrationTBs
                                  where d.EmployeeId == Convert.ToInt32(Session["UserId"]) && d.UserType == "Admin"
                                  select new { d.EmployeeId };
            if (registratindata.Count() > 0)
            {
                DataTable dtfetchcandidatelist = g.ReturnData("Select c.Candidate_ID, CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID ");
                if (dtfetchcandidatelist.Rows.Count > 0)
                {
                    grdcandidatelist.DataSource = dtfetchcandidatelist;
                    grdcandidatelist.DataBind();
                    lblcount.Text = dtfetchcandidatelist.Rows.Count.ToString();
                }
                else
                {
                    grdcandidatelist.DataSource = null;
                    grdcandidatelist.DataBind();
                    lblcount.Text = "0";
                }
            }

            var registratindataEmp = from d in hr.RegistrationTBs
                                     where d.EmployeeId == Convert.ToInt32(Session["UserId"]) && d.UserType != "Admin"
                                     select new { d.EmployeeId };
            if (registratindataEmp.Count() > 0)
            {
                DataTable dtfetchcandidatelistemp = g.ReturnData("Select c.Candidate_ID, CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Where EmployeeID='" + Convert.ToInt32(Session["UserId"]) + "' And Refrence_By='Employee' ");
                if (dtfetchcandidatelistemp.Rows.Count > 0)
                {
                    grdcandidatelist.DataSource = dtfetchcandidatelistemp;
                    grdcandidatelist.DataBind();
                    lblcount.Text = dtfetchcandidatelistemp.Rows.Count.ToString();
                }
                else
                {
                    grdcandidatelist.DataSource = null;
                    grdcandidatelist.DataBind();
                    lblcount.Text = "0";
                }
            }

            var agencydata = from d in hr.AgencyMasterTBs
                             where d.AgencyId == Convert.ToInt32(Session["UserId"]) && Session["UserType"] == "Agency"
                             select new { d.AgencyId };
            if (agencydata.Count() > 0)
            {
                DataTable dtfetchcandidatelist = g.ReturnData("Select c.Candidate_ID, CONVERT(varchar,c.Entry_Date,101) AS Entry_Date, VancancyTB.Title As Vacancy, c.Nationality,  c.Name ,  c.CandidateId_No, CONVERT(varchar,c.DOB,101) AS DOB,  c.Religion, c.Name, c.Contact_No, c.Email_Address from CandidateTB c Left outer join VancancyTB on c.Vaccancy_ID=VancancyTB.VacancyID Where AgencyId='" + Convert.ToInt32(Session["UserId"]) + "' And Refrence_By='Agency'  ");
                if (dtfetchcandidatelist.Rows.Count > 0)
                {
                    grdcandidatelist.DataSource = dtfetchcandidatelist;
                    grdcandidatelist.DataBind();
                    lblcount.Text = dtfetchcandidatelist.Rows.Count.ToString();
                }
                else
                {
                    grdcandidatelist.DataSource = null;
                    grdcandidatelist.DataBind();
                    lblcount.Text = "0";
                }
            }

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void fillstate()
    {
        try
        {
            var statedata = from dt in hr.StateTBs
                            select new { dt.StateId, dt.StateName };
            if (statedata != null && statedata.Count() > 0)
            {

                ddlCsate.DataSource = statedata;
                ddlCsate.DataTextField = "StateName";
                ddlCsate.DataValueField = "StateId";
                ddlCsate.DataBind();

                ddlPstatte.DataSource = statedata;
                ddlPstatte.DataTextField = "StateName";
                ddlPstatte.DataValueField = "StateId";
                ddlPstatte.DataBind();

                //ddlCsate.Items.Insert(1, "");
                FillCity(ddlCsate.SelectedValue);
                FillPCity(ddlPstatte.SelectedValue);

            }
            else
            {
                ddlCsate.DataSource = null;
                ddlCsate.DataBind();
                ddlCsate.Items.Clear();
                ddlCcity.Items.Clear();

            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void FillPCity(string p)
    {
        try
        {
            var citydata = from dt in hr.CityTBs
                           where dt.StateId == Convert.ToInt32(p)
                           select new { dt.CityId, dt.CityName };




            ddlPcity.DataSource = citydata;
            ddlPcity.DataTextField = "CityName";
            ddlPcity.DataValueField = "CityId";
            ddlPcity.DataBind();



        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void FillCity(string p)
    {
        try
        {
            var citydata = from dt in hr.CityTBs
                           where dt.StateId == Convert.ToInt32(p)
                           select new { dt.CityId, dt.CityName };


            ddlCcity.DataSource = citydata;
            ddlCcity.DataTextField = "CityName";
            ddlCcity.DataValueField = "CityId";
            ddlCcity.DataBind();



        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void fillvacancy()
    {
        try
        {
            if (ddlComp.SelectedIndex != -1)
            {
                var vacancydata = from dt in hr.VancancyTBs
                                  where dt.VacancyStatus == "Approved" && dt.CompanyID == Convert.ToInt32(ddlComp.SelectedValue)
                                  select new { dt.VacancyID, dt.Title };
                ddlvaccancy.DataSource = vacancydata;
                ddlvaccancy.DataTextField = "Title";
                ddlvaccancy.DataValueField = "VacancyID";
                ddlvaccancy.DataBind();
                ddlvaccancy.Items.Insert(0, "--Select--");
            }

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }


    #region Auto Increment CandidateIDNO.....

    private void EMPNO()
    {
        string s = DateTime.Now.ToString("dd-yyyy");
        try
        {

            var data = (from dt in hr.CandidateTBs
                        select dt.Candidate_ID).Max();
            if (data != null)
            {

                txtcandidateId.Text = data + 1 + "-EMP/" + DateTime.Now.ToString("dd/MM/yyyy");

            }
            else
            {
                txtcandidateId.Text = 1 + "-EMP/" + DateTime.Now.ToString("dd/MM/yyyy");

            }
        }
        catch
        {
            txtcandidateId.Text = 1 + "-EMP/" + DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    #endregion
    protected void ddlCsate_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCity(ddlCsate.SelectedValue);

    }

    //private void fillCcity()
    //{
    //    try
    //    {
    //        var citydata = from dt in hr.CityTBs
    //                       where dt.StateId==Convert.ToInt32(ddlCsate.SelectedValue)
    //                          select new { dt.CityId, dt.CityName };


    //        ddlCcity.DataSource = citydata;
    //        ddlCcity.DataTextField = "CityName";
    //        ddlCcity.DataValueField = "CityId";
    //        ddlCcity.DataBind();
    //     }
    //    catch (Exception ex)
    //    {

    //        g.ShowMessage(this.Page, ex.Message);
    //    }
    //}
    protected void ddlPstatte_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillPCity(ddlPstatte.SelectedValue);
    }

    //private void fillPcity()
    //{
    //    try
    //    {
    //        var citydata = from dt in hr.CityTBs
    //                       where dt.StateId == Convert.ToInt32(ddlPstatte.SelectedValue)
    //                       select new { dt.CityId, dt.CityName };
    //        ddlPcity.DataSource = citydata;
    //        ddlPcity.DataTextField = "CityName";
    //        ddlPcity.DataValueField = "CityId";
    //        ddlPcity.DataBind();

    //    }
    //    catch (Exception ex)
    //    {

    //        g.ShowMessage(this.Page, ex.Message);
    //    }
    //}

    #region add education qualification
    protected void btnaddedu_Click(object sender, EventArgs e)
    {

        int cnt = 0;
        if (ViewState["DTEducation"] != null)
        {
            DTEducation = (DataTable)ViewState["DTEducation"];
        }
        else
        {
            DataColumn EducationName = DTEducation.Columns.Add("EducationName");
            DataColumn University = DTEducation.Columns.Add("University");
            DataColumn YearOfPassing = DTEducation.Columns.Add("YearOfPassing");
            DataColumn ObtainPercent = DTEducation.Columns.Add("ObtainPercent");
        }

        DataRow dr = DTEducation.NewRow();
        dr[0] = txteduname.Text;
        dr[1] = txtuniversity.Text;
        dr[2] = txtpassingyear.Text;
        dr[3] = txtpercentage.Text;

        if (DTEducation.Rows.Count > 0)
        {
            for (int f = 0; f < DTEducation.Rows.Count; f++)
            {

                string u2 = DTEducation.Rows[f][0].ToString();
                if (u2 == txteduname.Text)
                {
                    cnt++;

                }
                else
                {

                }
            }
            if (cnt > 0)
            {
                g.ShowMessage(this.Page, "This Education Details Already Exist");
                // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Already Exist');", true);
            }
            else
            {
                DTEducation.Rows.Add(dr);
                ClearTextBoxOFEducationDetails();
            }
        }
        else
        {
            DTEducation.Rows.Add(dr);
            ClearTextBoxOFEducationDetails();
        }

        ViewState["DTEducation"] = DTEducation;

        grdeducation.DataSource = DTEducation;
        grdeducation.DataBind();
        grdeducation.Visible = true;
    }
    private void ClearTextBoxOFEducationDetails()
    {
        txteduname.Text = "";
        txtuniversity.Text = "";
        txtpassingyear.Text = "";
        txtpercentage.Text = "";
    }
    #endregion .....End Add Education


    #region Edit Education Details In DataTable
    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        DTEducation = (DataTable)ViewState["DTEducation"];
        foreach (DataRow d in DTEducation.Rows)
        {
            if (d[0].ToString() == imgedit.CommandArgument)
            {
                txteduname.Text = d["EducationName"].ToString();
                txtpassingyear.Text = d["YearOfPassing"].ToString();
                txtuniversity.Text = d["University"].ToString();
                txtpercentage.Text = d["ObtainPercent"].ToString();
                d.Delete();
                DTEducation.AcceptChanges();
                break;
            }
        }

        grdeducation.DataSource = DTEducation;
        grdeducation.DataBind();
        ViewState["DTEducation"] = DTEducation;

    }
    #endregion... End Edit Education Details

    #region Delete Education Details In DataTable
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        DTEducation = (DataTable)ViewState["DTEducation"];

        foreach (DataRow d in DTEducation.Rows)
        {
            if (d[0].ToString() == imgdelete.CommandArgument)
            {
                d.Delete();
                DTEducation.AcceptChanges();
                break;
            }
        }

        grdeducation.DataSource = DTEducation;
        grdeducation.DataBind();
    }
    #endregion... End Delete Education Details

    protected void btnuploadresume_Click(object sender, EventArgs e)
    {
        string FolderPath = Server.MapPath("~/Recruitment/CandidateResume");
        MakeDirectoryIfExist(FolderPath);
        if (FileUpload1.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string ext = Path.GetExtension(FileUpload1.PostedFile.FileName);
                //  FileUpload1.SaveAs(Server.MapPath("Attachments/" + filename));
                if (File.Exists(Server.MapPath("~/Recruitment/CandidateResume/" + filename)))
                {
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','This Image Name Already Exists');", true);
                    g.ShowMessage(this.Page, "This Resume Already Exists");
                }
                else
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Recruitment/CandidateResume/" + filename));
                    AttachPath = filename;
                    lblresumepath.Text = AttachPath;

                }
            }
            catch (Exception ex)
            {
                g.ShowMessage(this.Page, "" + ex.Message + "");
                // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','" + ex.Message + "');", true);
            }
        }
        else
        {
            g.ShowMessage(this.Page, "Please Choose file");
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Please Upload Image');", true);

        }
    }

    private void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (ViewState["DTEducation"] != null)
        {
            if (btnsave.Text == "Save")
            {
                #region save code

                var checkpanNo = from p in hr.CandidateTBs.Where(d => d.Pancardno == txtpanno.Text) select p;
                if (checkpanNo.Count() > 0)
                {
                    g.ShowMessage(this.Page, "PAN Number Already Exist");
                }
                else
                {
                    CandidateTB E = new CandidateTB();
                    E.Vaccancy_ID = Convert.ToInt32(ddlvaccancy.SelectedValue);
                    E.Entry_Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    E.Pancardno = txtpanno.Text;
                    E.Company_ID = Convert.ToInt32(ddlComp.SelectedValue);
                    if (chkpassport.Checked == true)
                    {
                        E.Passportstatus = 0; // yes passport
                        E.Passportno = txtpassport.Text;
                    }
                    else
                    {
                        E.Passportstatus = 1;  // No passport

                    }


                    E.CandidateId_No = txtcandidateId.Text;
                    E.Name = txtname.Text;
                    E.Marital_Status = rd_maritalstatus.SelectedIndex;
                    E.DOB = Convert.ToDateTime(txtdob.Text);
                    E.Gender = rd_maritalstatus.SelectedIndex;
                    E.Religion = ddlreligion.SelectedItem.Text;
                    E.Nationality = ddlnational.SelectedItem.Text;
                    E.Contact_No = txtcontactno.Text;
                    E.AltContactNo = txtaltcontact.Text;
                    E.Email_Address = txtemail.Text;
                    E.C_State = Convert.ToInt32(ddlCsate.SelectedValue);
                    E.C_City = Convert.ToInt32(ddlCcity.SelectedValue);
                    E.C_ZipCode = txtCzipcode.Text;
                    E.C_Landmark = txtClandmark.Text;

                    E.P_State = Convert.ToInt32(ddlPstatte.SelectedValue);
                    E.P_City = Convert.ToInt32(ddlPcity.SelectedValue);
                    E.P_ZipCode = txtPzipcode.Text;
                    E.P_Landmark = txtPlandmark.Text;

                    E.Total_Expereince = ddlyear.SelectedItem.Text;
                    E.Total_Month_Exp = ddlmonth.SelectedItem.Text;
                    E.Relevent_Expereince = ddlrelyear.SelectedItem.Text;
                    E.Relevent_Month_Exp = ddlrelmont.SelectedItem.Text;


                    E.NoticePeriod = txtnoticeperiod.Text;
                    E.Skills = txtskliis.Text;
                    E.CTC = txtCTC.Text;
                    E.ExpectedCTC = txtExpectedCTC.Text;

                    E.Refrence_By = ddlrefrenceby.SelectedItem.Text;
                    if (ddlrefrenceby.SelectedIndex == 0)
                    {
                        E.AgencyId = null;
                        E.EmployeeID = null;
                    }
                    if (ddlrefrenceby.SelectedIndex == 1)
                    {
                        if (ddlrefNameid.SelectedIndex != -1)
                        {
                            E.AgencyId = Convert.ToInt32(ddlrefNameid.SelectedValue);
                        }
                        else
                        {
                            E.AgencyId = null;
                        }

                    }

                    if (ddlrefrenceby.SelectedIndex == 2)
                    {
                        if (ddlrefNameid.SelectedIndex != -1)
                        {
                            E.EmployeeID = Convert.ToInt32(ddlrefNameid.SelectedValue);
                        }
                        else
                        {
                            E.EmployeeID = null;
                        }

                    }


                    E.Resume_Path = lblresumepath.Text;
                    hr.CandidateTBs.InsertOnSubmit(E);
                    hr.SubmitChanges();

                    if (ViewState["DTEducation"] != null)
                    {
                        DTEducation = ViewState["DTEducation"] as DataTable;
                        for (int i = 0; i < DTEducation.Rows.Count; i++)
                        {
                            CandidateEduTB BT = new CandidateEduTB();
                            BT.Candidate_ID = E.Candidate_ID;
                            BT.Education_Name = DTEducation.Rows[i][0].ToString();
                            BT.Passing_Year = DTEducation.Rows[i][1].ToString();
                            BT.University = DTEducation.Rows[i][2].ToString();
                            BT.Percentage = DTEducation.Rows[i][3].ToString();
                            hr.CandidateEduTBs.InsertOnSubmit(BT);
                            hr.SubmitChanges();
                        }
                        ViewState["DTEducation"] = null;
                        grdeducation.DataSource = null;
                        grdeducation.DataBind();
                    }

                    if (ViewState["DTExperience"] != null)
                    {
                        DTExperience = ViewState["DTExperience"] as DataTable;
                        for (int i = 0; i < DTExperience.Rows.Count; i++)
                        {
                            CandidateExperienceTB BT = new CandidateExperienceTB();
                            BT.CandidateID = E.Candidate_ID;
                            BT.CompanyName = DTExperience.Rows[i][0].ToString();
                            BT.Designation = DTExperience.Rows[i][1].ToString();
                            BT.FromMonth = DTExperience.Rows[i][3].ToString();
                            BT.FromYear = DTExperience.Rows[i][4].ToString();
                            BT.ToMonth = DTExperience.Rows[i][6].ToString();
                            BT.ToYear = DTExperience.Rows[i][7].ToString();
                            BT.CompanyAddress = DTExperience.Rows[i][8].ToString();
                            hr.CandidateExperienceTBs.InsertOnSubmit(BT);
                            hr.SubmitChanges();
                        }
                        ViewState["DTExperience"] = null;
                        grdexperience.DataSource = null;
                        grdexperience.DataBind();
                    }


                    g.ShowMessage(this.Page, "Candidate Details Saved Successfully");
                    clearallfields();
                }
                #endregion

            }
            else
            {
                #region Update
                var checkpanNo = from p in hr.CandidateTBs.Where(d => d.Pancardno == txtpanno.Text && d.Candidate_ID == Convert.ToInt32(lblcandidateID.Text)) select p;
                if (checkpanNo.Count() > 0)
                {
                    updateFun();

                }
                else
                {
                    var checkpanNo1 = from p in hr.CandidateTBs.Where(d => d.Pancardno == txtpanno.Text && d.Candidate_ID != Convert.ToInt32(lblcandidateID.Text)) select p;
                    if (checkpanNo1.Count() > 0)
                    {
                        g.ShowMessage(this.Page, "PAN Number Already Exist");

                    }
                    else
                    {
                        updateFun();
                    }

                }
                #endregion
            }
        }
        else
        {
            g.ShowMessage(this.Page, "Add Education Details");
        }
    }



    private void updateFun()
    {
        CandidateTB E = hr.CandidateTBs.Where(d => d.Candidate_ID == Convert.ToInt32(lblcandidateID.Text)).First();
        E.Vaccancy_ID = Convert.ToInt32(ddlvaccancy.SelectedValue);
        E.Name = txtname.Text;
        E.Marital_Status = rd_maritalstatus.SelectedIndex;

        E.Pancardno = txtpanno.Text;
        E.Company_ID = Convert.ToInt32(ddlComp.SelectedValue);
        if (chkpassport.Checked == true)
        {
            E.Passportstatus = 0; // yes passport
            E.Passportno = txtpassport.Text;
        }
        else
        {
            E.Passportstatus = 1;  // No passport

        }

        E.DOB = Convert.ToDateTime(txtdob.Text);
        E.Gender = rd_maritalstatus.SelectedIndex;
        E.Religion = ddlreligion.SelectedItem.Text;
        E.Nationality = ddlnational.SelectedItem.Text;
        E.Contact_No = txtcontactno.Text;
        E.AltContactNo = txtaltcontact.Text;
        E.Email_Address = txtemail.Text;
        E.C_State = Convert.ToInt32(ddlCsate.SelectedValue);
        E.C_City = Convert.ToInt32(ddlCcity.SelectedValue);
        E.C_ZipCode = txtCzipcode.Text;
        E.C_Landmark = txtClandmark.Text;

        E.P_State = Convert.ToInt32(ddlPstatte.SelectedValue);
        E.P_City = Convert.ToInt32(ddlPcity.SelectedValue);
        E.P_ZipCode = txtPzipcode.Text;
        E.P_Landmark = txtPlandmark.Text;
        E.Total_Expereince = ddlyear.SelectedItem.Text;
        E.Total_Month_Exp = ddlmonth.SelectedItem.Text;
        E.Relevent_Expereince = ddlrelyear.SelectedItem.Text;
        E.Relevent_Month_Exp = ddlrelmont.SelectedItem.Text;
        E.NoticePeriod = txtnoticeperiod.Text;
        E.Skills = txtskliis.Text;
        E.CTC = txtCTC.Text;
        E.ExpectedCTC = txtExpectedCTC.Text;
        E.Refrence_By = ddlrefrenceby.SelectedItem.Text;
        if (ddlrefrenceby.SelectedIndex == 0)
        {
            E.AgencyId = null;
            E.EmployeeID = null;
        }
        if (ddlrefrenceby.SelectedIndex == 1 || ddlrefrenceby.SelectedItem.Text == "Agency")
        {
            if (ddlrefNameid.SelectedIndex != -1)
            {
                E.AgencyId = Convert.ToInt32(ddlrefNameid.SelectedValue);
            }
            else
            {
                E.AgencyId = null;
            }

        }

        if (ddlrefrenceby.SelectedIndex == 2 || ddlrefrenceby.SelectedItem.Text == "Employee")
        {
            if (ddlrefNameid.SelectedIndex != -1)
            {
                E.EmployeeID = Convert.ToInt32(ddlrefNameid.SelectedValue);
            }
            else
            {
                E.EmployeeID = null;
            }

        }

        E.Resume_Path = lblresumepath.Text;
        hr.SubmitChanges();
        string itemdelet = "delete from CandidateEduTB where Candidate_ID='" + Convert.ToInt32(lblcandidateID.Text) + "'";
        DataSet ds = g.ReturnData1(itemdelet);
        if (ViewState["DTEducation"] != null)
        {
            DTEducation = ViewState["DTEducation"] as DataTable;
            for (int i = 0; i < DTEducation.Rows.Count; i++)
            {
                CandidateEduTB BT = new CandidateEduTB();
                BT.Candidate_ID = E.Candidate_ID;
                BT.Education_Name = DTEducation.Rows[i][0].ToString();
                BT.Passing_Year = DTEducation.Rows[i][1].ToString();
                BT.University = DTEducation.Rows[i][2].ToString();
                BT.Percentage = DTEducation.Rows[i][3].ToString();
                hr.CandidateEduTBs.InsertOnSubmit(BT);
                hr.SubmitChanges();
            }
            ViewState["DTEducation"] = null;
            ViewState["DTEducation"] = "";
            grdeducation.DataSource = null;
            grdeducation.DataBind();
        }

        string itemdeletexper = "delete from CandidateExperienceTB where CandidateID='" + Convert.ToInt32(lblcandidateID.Text) + "'";
        DataSet dsexp = g.ReturnData1(itemdeletexper);
        if (ViewState["DTExperience"] != null)
        {
            DTExperience = ViewState["DTExperience"] as DataTable;
            for (int i = 0; i < DTExperience.Rows.Count; i++)
            {
                CandidateExperienceTB BT = new CandidateExperienceTB();
                BT.CandidateID = E.Candidate_ID;
                BT.CompanyName = DTExperience.Rows[i][0].ToString();
                BT.Designation = DTExperience.Rows[i][1].ToString();
                BT.FromMonth = DTExperience.Rows[i][3].ToString();
                BT.FromYear = DTExperience.Rows[i][4].ToString();
                BT.ToMonth = DTExperience.Rows[i][6].ToString();
                BT.ToYear = DTExperience.Rows[i][7].ToString();
                BT.CompanyAddress = DTExperience.Rows[i][8].ToString();
                hr.CandidateExperienceTBs.InsertOnSubmit(BT);
                hr.SubmitChanges();
            }
            ViewState["DTExperience"] = null;
            grdexperience.DataSource = null;
            grdexperience.DataBind();
        }


        g.ShowMessage(this.Page, "Candidate Details Updated Successfully");
        clearallfields();
    }

    private void clearallfields()
    {
        bindcandidatelist();
        EMPNO();
        fillvacancy();
        fillstate();
        txtname.Text = "";
        rd_maritalstatus.SelectedIndex = 0;
        txtdob.Text = "";
        ddlreligion.SelectedIndex = 0;
        rdgender.SelectedIndex = 0;
        ddlnational.SelectedIndex = 0;
        txtcontactno.Text = "";
        txtemail.Text = "";
        txtCzipcode.Text = "";
        txtClandmark.Text = "";
        txtPzipcode.Text = "";
        txtPlandmark.Text = "";
        ddlyear.SelectedIndex = 0;
        ddlmonth.SelectedIndex = 0;
        ddlrelyear.SelectedIndex = 0;
        ddlrelmont.SelectedIndex = 0;
        txtnoticeperiod.Text = "";
        txtskliis.Text = "";
        txtCTC.Text = "";
        txtExpectedCTC.Text = "";
        ddlrefrenceby.SelectedIndex = 0;
        lblresumepath.Text = "";
        txteduname.Text = "";
        txtpassingyear.Text = "";
        txtpercentage.Text = "";
        txtuniversity.Text = "";
        DTEducation = null;
        ViewState["DTEducation"] = null;
        MultiView1.ActiveViewIndex = 0;
        grdeducation.DataSource = null;
        grdeducation.DataBind();
        txtaltcontact.Text = "";
        txtpanno.Text = "";
        txtpassport.Text = "";
        chkpassport.Checked = false;
        chksame.Checked = false;
        trVcodedate.Visible = false;
        lblcode.Text = "";
        lblDate.Text = "";
    }
    protected void chksame_CheckedChanged(object sender, EventArgs e)
    {
        //if (ddlCsate.SelectedIndex > 0 && ddlCsate.SelectedIndex != -1 && ddlCcity.SelectedIndex > 0 && ddlCcity.SelectedIndex != -1)
        //{

        if (chksame.Checked == true)
        {
            // txtAddressP.Text = txtAddress.Text;
            ddlPstatte.SelectedValue = ddlCsate.SelectedValue;
            FillPCity(ddlPstatte.SelectedValue);
            ddlPcity.SelectedValue = ddlCcity.SelectedValue;
            txtPzipcode.Text = txtCzipcode.Text;
            txtPlandmark.Text = txtClandmark.Text;
        }
        else
        {
            txtPzipcode.Text = "";
            txtPlandmark.Text = "";

        }
        //}
        //else
        //{
        //    g.ShowMessage(this.Page, "Select All Current Address Field");
        //    chksame.Checked = false;

        //}
    }
    protected void txtpassingyear_TextChanged(object sender, EventArgs e)
    {
        CheckYearPassing();
    }


    private void CheckYearPassing()
    {
        //string URL = "http://www.google.com";
        //System.Net.HttpWebRequest rq2 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
        //System.Net.HttpWebResponse res2 = (System.Net.HttpWebResponse)rq2.GetResponse();
        //DateTime Date = DateTime.Parse(res2.Headers["Date"]);


        DateTime Date = DateTime.Now;
        string date1 = Date.ToString("yyyy");
        int dae2 = Convert.ToInt32(date1);
        if (txtpassingyear.Text != "")
        {

            int year = Convert.ToInt32(txtpassingyear.Text);


            if (year > dae2)
            {
                g.ShowMessage(this.Page, "Enter Valid Year");
                txtpassingyear.Text = "";
            }
            else
            {

            }

        }
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        var registratindata = from d in hr.RegistrationTBs
                              where d.EmployeeId == Convert.ToInt32(Session["UserId"]) && d.UserType == "Admin"
                              select new { d.EmployeeId };
        if (registratindata.Count() > 0)
        {
            ddlrefrenceby.SelectedIndex = 0;
            trrefren.Visible = false;
            ddlrefNameid.Enabled = true;
            ddlrefrenceby.Enabled = true;
        }
        else
        {
            var registratindata1 = from d in hr.RegistrationTBs
                                   where d.EmployeeId == Convert.ToInt32(Session["UserId"]) && d.UserType != "Admin"
                                   select new { d.EmployeeId };
            if (registratindata1.Count() > 0)
            {
                ddlrefrenceby.SelectedIndex = 2;
                fillreferenceName();
                ddlrefNameid.SelectedValue = Convert.ToString(Session["UserId"]);
                ddlrefNameid.Enabled = false;
                ddlrefrenceby.Enabled = false;
            }
            else
            {
                var agencydata = from d in hr.AgencyMasterTBs
                                 where d.AgencyId == Convert.ToInt32(Session["UserId"]) && Session["UserType"] == "Agency"
                                 select new { d.AgencyId };
                if (agencydata.Count() > 0)
                {
                    ddlrefrenceby.SelectedIndex = 1;
                    fillreferenceName();
                    ddlrefNameid.SelectedValue = Convert.ToString(Session["UserId"]);
                    ddlrefNameid.Enabled = false;
                    ddlrefrenceby.Enabled = false;
                }
            }

        }
    }

    #region fetch all value for edit
    protected void ImageEditCanditList_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 1;
            ImageButton imge = (ImageButton)sender;
            lblcandidateID.Text = imge.CommandArgument;

            CandidateTB t = hr.CandidateTBs.Where(d => d.Candidate_ID == Convert.ToInt32(lblcandidateID.Text)).First();
            ddlvaccancy.SelectedValue = Convert.ToString(t.Vaccancy_ID);
            //DataTable dtcom = g.ReturnData("select CompanyID from VancancyTB where VacancyID='" + t.Vaccancy_ID + "'");
            //lblcompanyId.Text = dtcom.Rows[0]["CompanyID"].ToString();
            bindCompany();
            ddlComp.SelectedValue = Convert.ToInt32(t.Company_ID).ToString();
            txtpanno.Text = t.Pancardno;
            txtcandidateId.Text = t.CandidateId_No;
            txtname.Text = t.Name;
            rd_maritalstatus.SelectedIndex = Convert.ToInt32(t.Marital_Status);
            rdgender.SelectedIndex = Convert.ToInt32(t.Gender);
            DateTime date1 = Convert.ToDateTime(t.DOB);
            txtdob.Text = date1.ToString("MM/dd/yyyy");
            ddlreligion.SelectedItem.Text = t.Religion;
            ddlnational.SelectedItem.Text = t.Nationality;
            txtcontactno.Text = t.Contact_No;
            txtaltcontact.Text = t.AltContactNo;
            txtpanno.Text = t.Pancardno;
            if (t.Passportstatus == 0)
            {
                chkpassport.Checked = true;
                txtpassport.ReadOnly = false;
                txtpassport.Text = t.Passportno;
            }
            else
            {
                chkpassport.Checked = false;
                txtpassport.ReadOnly = true;
                txtpassport.Text = t.Passportno;
            }

            txtemail.Text = t.Email_Address;
            fillstate();

            ddlCsate.SelectedValue = Convert.ToString(t.C_State);
            FillCity(Convert.ToInt32(ddlCsate.SelectedValue).ToString());
            ddlCcity.SelectedValue = Convert.ToString(t.C_City);
            txtCzipcode.Text = t.C_ZipCode;
            txtClandmark.Text = t.C_Landmark;

            ddlPstatte.SelectedValue = Convert.ToString(t.P_State);
            FillPCity(Convert.ToInt32(ddlPstatte.SelectedValue).ToString());
            ddlPcity.SelectedValue = Convert.ToString(t.P_City);
            txtPzipcode.Text = t.P_ZipCode;
            txtPlandmark.Text = t.P_Landmark;

            //txttotalexp.Text = t.Total_Expereince;
            //txtrelevent.Text = t.Relevent_Expereince;
            ddlyear.SelectedValue = t.Total_Expereince;
            ddlmonth.SelectedValue = t.Total_Month_Exp;
            ddlrelyear.SelectedValue = t.Relevent_Expereince;
            ddlrelmont.SelectedValue = t.Relevent_Month_Exp;


            txtnoticeperiod.Text = t.NoticePeriod;
            txtskliis.Text = t.Skills;
            txtCTC.Text = t.CTC;
            txtExpectedCTC.Text = t.ExpectedCTC;


            ddlrefrenceby.SelectedItem.Text = t.Refrence_By;
            fillreferenceName();
            if (ddlrefrenceby.SelectedItem.Text == "--Select--")
            {
                trrefren.Visible = false;
            }
            if (ddlrefrenceby.SelectedItem.Text == "Agency")
            {
                trrefren.Visible = true;
                fillreferenceName();
                ddlrefNameid.SelectedValue = Convert.ToString(t.AgencyId).ToString();
            }
            if (ddlrefrenceby.SelectedItem.Text == "Employee")
            {
                trrefren.Visible = true;
                fillreferenceName();
                ddlrefNameid.SelectedValue = Convert.ToString(t.EmployeeID).ToString();
            }
            lblresumepath.Text = t.Resume_Path;

            // Education Details............
            DataTable datat1 = g.ReturnData("Select Education_Name AS EducationName , University AS University, Passing_Year AS YearOfPassing,  Percentage AS ObtainPercent from CandidateEduTB WHERE Candidate_ID = '" + Convert.ToInt32(lblcandidateID.Text) + "'");
            if (datat1.Rows.Count > 0)
            {
                ViewState["DTEducation"] = datat1;
                grdeducation.DataSource = datat1;
                grdeducation.DataBind();
            }
            else
            {
                ViewState["DTEducation"] = null;
                grdeducation.DataSource = null;
                grdeducation.DataBind();
            }

            // Experiance Details............

            DataTable dataExpe = g.ReturnData("Select CompanyName , Designation,case when FromMonth='1' then 'January' when FromMonth='2' then 'Feburary' when FromMonth='3' then 'March' when FromMonth='4' then 'April' when FromMonth='5' then 'May' when FromMonth='6' then 'June' when FromMonth='7' then 'July' when FromMonth='8' then 'August' when FromMonth='9' then 'September' when FromMonth='10' then 'October' when FromMonth='11' then 'November' when FromMonth='12' then 'December' end as FromMonth,FromMonth as FromMonthID,FromYear,case when ToMonth='1' then 'January' when ToMonth='2' then 'Feburary' when ToMonth='3' then 'March' when ToMonth='4' then 'April' when ToMonth='5' then 'May' when ToMonth='6' then 'June' when ToMonth='7' then 'July' when ToMonth='8' then 'August' when ToMonth='9' then 'September' when ToMonth='10' then 'October' when ToMonth='11' then 'November' when ToMonth='12' then 'December' end as ToMonth,ToMonth as ToMonthID,ToYear,CompanyAddress AS Address  from CandidateExperienceTB where CandidateID= '" + Convert.ToInt32(lblcandidateID.Text) + "'");
            if (dataExpe.Rows.Count > 0)
            {
                ViewState["DTExperience"] = dataExpe;
                grdexperience.DataSource = dataExpe;
                grdexperience.DataBind();
            }
            else
            {
                ViewState["DTExperience"] = null;
                grdexperience.DataSource = null;
                grdexperience.DataBind();
            }
            btnsave.Text = "Update";
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }


    }
    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearallfields();
    }

    protected void ddlvaccancy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlvaccancy.SelectedIndex > 0)
            //{
            //     var vacancycompanyId = from dt in hr.VancancyTBs
            //                  where dt.VacancyID==Convert.ToInt32(ddlvaccancy.SelectedValue)
            //                  select new { dt.CompanyID};
            //     if (vacancycompanyId.Count() > 0)
            //     {
            //     foreach (var item in vacancycompanyId)
            //     {
            //         lblcompanyId.Text = item.CompanyID.ToString();
            //     }
            //     }
            //     else
            //     {
            //         lblcompanyId.Text = "";
            //     }
            //}
            //else
            //{
            //    lblcompanyId.Text = "";
            //}



            if (ddlvaccancy.SelectedIndex > 0)
            {
                var fetchVcode = from d in hr.VancancyTBs
                                 where d.VacancyID == Convert.ToInt32(ddlvaccancy.SelectedValue)
                                 select new { d.Code, d.VacancyDate };
                if (fetchVcode.Count() > 0)
                {
                    trVcodedate.Visible = true;
                    lblvCodedate.Text = "Vacancy Code/Date :";
                    foreach (var item in fetchVcode)
                    {
                        lblcode.Text = item.Code;
                        DateTime date = Convert.ToDateTime(item.VacancyDate);
                        lblDate.Text = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
                    }
                }
                else
                {
                    trVcodedate.Visible = false;
                    lblcode.Text = "";
                    lblvCodedate.Text = "";
                    lblDate.Text = "";
                }
            }
            else
            {
                trVcodedate.Visible = false;
                lblcode.Text = "";
                lblvCodedate.Text = "";
                lblDate.Text = "";
            }


        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    protected void chkpassport_CheckedChanged(object sender, EventArgs e)
    {

        if (chkpassport.Checked == true)
        {
            txtpassport.ReadOnly = false;
            txtpassport.Focus();
            RequiredFieldValidator19.Enabled = true;
        }
        else
        {
            txtpassport.ReadOnly = true;
            txtpassport.Text = "";
            RequiredFieldValidator19.Enabled = false;
        }
    }
    protected void ddlrefrenceby_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillreferenceName();
    }

    private void fillreferenceName()
    {
        try
        {
            if (ddlrefrenceby.SelectedIndex == 0)
            {
                trrefren.Visible = false;
            }

            if (ddlrefrenceby.SelectedIndex == 1 || ddlrefrenceby.SelectedItem.Text == "Agency")
            {
                trrefren.Visible = true;
                lblrefnameby.Text = "Reference Name";
                var fetchagency = from d in hr.AgencyMasterTBs
                                  select new { d.AgencyId, d.AgencyName };
                if (fetchagency.Count() > 0)
                {
                    ddlrefNameid.DataSource = fetchagency;
                    ddlrefNameid.DataTextField = "AgencyName";
                    ddlrefNameid.DataValueField = "AgencyId";
                    ddlrefNameid.DataBind();

                }
                else
                {
                    ddlrefNameid.Items.Clear();
                    ddlrefNameid.DataSource = null;
                    ddlrefNameid.DataBind();
                }
            }
            if (ddlrefrenceby.SelectedIndex == 2 || ddlrefrenceby.SelectedItem.Text == "Employee")
            {
                trrefren.Visible = true;
                lblrefnameby.Text = "Reference Name";
                var dt = from p in hr.EmployeeTBs
                         where p.RelivingStatus == null
                         select new { Name = p.FName + ' ' + p.MName + ' ' + p.Lname, p.EmployeeId };
                if (dt.Count() > 0)
                {
                    ddlrefNameid.DataSource = dt;
                    ddlrefNameid.DataTextField = "Name";
                    ddlrefNameid.DataValueField = "EmployeeId";
                    ddlrefNameid.DataBind();

                }
                else
                {
                    ddlrefNameid.Items.Clear();
                    ddlrefNameid.DataSource = null;
                    ddlrefNameid.DataBind();

                }

            }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }


    #region Add Experience Details Edit & delete runtime
    protected void btnaddExperience_Click(object sender, EventArgs e)
    {
        int cnt = 0;
        if (ViewState["DTExperience"] != null)
        {
            DTExperience = (DataTable)ViewState["DTExperience"];
        }
        else
        {
            DataColumn CompanyName = DTExperience.Columns.Add("CompanyName");
            DataColumn Designation = DTExperience.Columns.Add("Designation");
            DataColumn FromMonth = DTExperience.Columns.Add("FromMonth");
            DataColumn FromMonthID = DTExperience.Columns.Add("FromMonthID");
            DataColumn FromYear = DTExperience.Columns.Add("FromYear");
            DataColumn ToMonth = DTExperience.Columns.Add("ToMonth");
            DataColumn ToMonthID = DTExperience.Columns.Add("ToMonthID");
            DataColumn ToYear = DTExperience.Columns.Add("ToYear");
            DataColumn CompanyAddress = DTExperience.Columns.Add("Address");
        }

        DataRow dr = DTExperience.NewRow();
        dr[0] = txtcompanyName.Text;
        dr[1] = txtdesignation.Text;
        dr[2] = ddlFrommonth.SelectedItem.Text;
        dr[3] = ddlFrommonth.SelectedValue;
        dr[4] = txtfromyear.Text;
        dr[5] = ddltommonth.SelectedItem.Text;
        dr[6] = ddltommonth.SelectedValue;
        dr[7] = txttoyear.Text;
        dr[8] = txtcompanyAddress.Text;


        if (DTExperience.Rows.Count > 0)
        {
            for (int f = 0; f < DTExperience.Rows.Count; f++)
            {

                string u2 = DTExperience.Rows[f][0].ToString();
                if (u2 == txtcompanyName.Text)
                {
                    cnt++;

                }
                else
                {

                }
            }
            if (cnt > 0)
            {
                g.ShowMessage(this.Page, "This Company Details Already Exist");
                // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Already Exist');", true);
            }
            else
            {
                DTExperience.Rows.Add(dr);
                ClearTextBoxOFExpDetails();
            }
        }
        else
        {
            DTExperience.Rows.Add(dr);
            ClearTextBoxOFExpDetails();
        }

        ViewState["DTExperience"] = DTExperience;

        grdexperience.DataSource = DTExperience;
        grdexperience.DataBind();
        grdexperience.Visible = true;
    }

    private void ClearTextBoxOFExpDetails()
    {
        txtcompanyName.Text = "";
        txtdesignation.Text = "";
        ddlFrommonth.SelectedIndex = 0;
        txtfromyear.Text = "";
        ddltommonth.SelectedIndex = 0;
        txttoyear.Text = "";
        txtcompanyAddress.Text = "";
    }
    protected void imgExpedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        DTExperience = (DataTable)ViewState["DTExperience"];
        foreach (DataRow d in DTExperience.Rows)
        {
            if (d[0].ToString() == imgedit.CommandArgument)
            {
                txtcompanyName.Text = d["CompanyName"].ToString(); ;
                txtdesignation.Text = d["Designation"].ToString();
                ddlFrommonth.SelectedValue = d["FromMonthID"].ToString();
                txtfromyear.Text = d["FromYear"].ToString();
                ddltommonth.SelectedValue = d["ToMonthID"].ToString();
                txttoyear.Text = d["ToYear"].ToString();
                txtcompanyAddress.Text = d["Address"].ToString();
                d.Delete();
                DTExperience.AcceptChanges();
                break;
            }
        }

        grdexperience.DataSource = DTExperience;
        grdexperience.DataBind();
        ViewState["DTExperience"] = DTExperience;
    }
    protected void imgExpdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        DTExperience = (DataTable)ViewState["DTExperience"];

        foreach (DataRow d in DTExperience.Rows)
        {
            if (d[0].ToString() == imgdelete.CommandArgument)
            {
                d.Delete();
                DTExperience.AcceptChanges();
                break;
            }
        }

        grdexperience.DataSource = DTExperience;
        grdexperience.DataBind();
    }
    #endregion

    protected void txtfromyear_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime Date = DateTime.Now;
            string date1 = Date.ToString("yyyy");
            int dae2 = Convert.ToInt32(date1);
            if (txtfromyear.Text != "")
            {

                int year = Convert.ToInt32(txtfromyear.Text);


                if (year > dae2)
                {
                    g.ShowMessage(this.Page, "Enter Valid Year");
                    txtfromyear.Text = "";
                    txttoyear.Text = "";
                }
                else
                {

                }

            }
            checkmonthyearOfExp();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void txttoyear_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime Date = DateTime.Now;
            string date1 = Date.ToString("yyyy");
            int dae2 = Convert.ToInt32(date1);
            if (txtfromyear.Text != "" && txttoyear.Text != "")
            {

                int year = Convert.ToInt32(txtfromyear.Text);
                int year2 = Convert.ToInt32(txttoyear.Text);
                if (year2 < year)
                {
                    g.ShowMessage(this.Page, "Enter Valid Year");
                    txttoyear.Text = "";
                }
                else
                {
                    if (year2 >= year)
                    {
                        if (year2 > dae2)
                        {
                            g.ShowMessage(this.Page, "Enter Valid Year");
                            txttoyear.Text = "";
                        }
                        else
                        {
                            //g.ShowMessage(this.Page, "Enter Valid Year");
                            //txttoyear.Text = "";
                        }

                    }
                    else
                    {
                        g.ShowMessage(this.Page, "Enter Valid Year");
                        txttoyear.Text = "";
                    }

                }


            }
            checkmonthyearOfExp();
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void checkmonthyearOfExp()
    {
        if (txtfromyear.Text != "" && txttoyear.Text != "")
        {
            if (txtfromyear.Text == txttoyear.Text)
            {
                int fromMon = Convert.ToInt32(ddlFrommonth.SelectedValue);
                int toMon = Convert.ToInt32(ddltommonth.SelectedValue);
                if (fromMon <= toMon)
                {

                }
                else
                {
                    g.ShowMessage(this.Page, "Please Check Valid  Month & Year ");
                    txttoyear.Text = "";
                }
            }
        }
    }



    protected void ddlFrommonth_SelectedIndexChanged(object sender, EventArgs e)
    {

        checkmonthyearOfExp();
    }
    protected void ddltommonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        checkmonthyearOfExp();
    }


    protected void txtdob_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (txtdob.Text != "")
            {
                //string URL = "http://www.google.com";
                //System.Net.HttpWebRequest rq2 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
                //System.Net.HttpWebResponse res2 = (System.Net.HttpWebResponse)rq2.GetResponse();
                //DateTime Date = DateTime.Parse(res2.Headers["Date"]);
                //string Servedate = Date.ToString("MM/dd/yyyy");
                DateTime Date = DateTime.Now;
                string DOBDate = Convert.ToDateTime(txtdob.Text).ToString();
                DateTime dt = DateTime.Parse(txtdob.Text);
                DateTime dtpr = Date.AddYears(-18);
                if (dt.Date >= dtpr.Date)
                {
                    g.ShowMessage(this.Page, "Verify Age. Not Allow Under 18 Years Old");
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "sticky('notice','Not Allow Under 14 Years Old');", true);
                    txtdob.Text = "";
                    txtdob.Focus();
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            g.ShowMessage(this.Page, "" + ex.Message + "");
        }
    }
}