using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Recruitment_ScheduleInterview : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable dtemp = new DataTable();
    DataTable dtposition = new DataTable();
    static DataTable tblsendCandidate = null;
    static DataTable tblsendInterviewer = null;
    DataRow drCandidate = null;
    DataRow drInterviewer = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                fillcompany();
                bindgrid();
               
                txtdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
            #region DataTableToadd candidate column to send mail To Interviewer
            tblsendInterviewer = new DataTable();
            tblsendInterviewer.Columns.Add(new DataColumn("Candidate Name", typeof(string)));
            tblsendInterviewer.Columns.Add(new DataColumn("Company Name", typeof(string)));
            tblsendInterviewer.Columns.Add(new DataColumn("Position", typeof(string)));
            tblsendInterviewer.Columns.Add(new DataColumn("Interview Date", typeof(string)));
            tblsendInterviewer.Columns.Add(new DataColumn("Interviewer Name", typeof(string)));
            tblsendInterviewer.Columns.Add(new DataColumn("Venu", typeof(string)));
            tblsendInterviewer.Columns.Add(new DataColumn("Time From", typeof(string)));
            tblsendInterviewer.Columns.Add(new DataColumn("Time To", typeof(string)));
            #endregion
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
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid ");  
            }
            if (ddlsortby.SelectedIndex==1 && txtcompanywise.Text !="")
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid Where ST.CompanyId='"+Convert.ToInt32(lblcompID.Text) +"' ");  
            }
            if (ddlsortby.SelectedIndex == 2 && txtpositionwise.Text !="")
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid Where VancancyTB.Title = '" +txtpositionwise.Text +"' ");
            }
            if (ddlsortby.SelectedIndex == 3 && txtcandidatewise.Text !="")
            {
                dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid Where CandidateTB.Name='" +txtcandidatewise.Text +"'");
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
                 dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid where (Select EmployeeId from SchedulePanelTB where SchedulePanelTB.ScheduleId=ST.ScheduleId and EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "')='" + Convert.ToInt32(Session["UserId"]) + "' ");
             }
            if (ddlsortby.SelectedIndex == 1 && txtcompanywise.Text != "")
             {
                 dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid where (Select EmployeeId from SchedulePanelTB where SchedulePanelTB.ScheduleId=ST.ScheduleId and EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "')='" + Convert.ToInt32(Session["UserId"]) + "' and ST.CompanyId='" + Convert.ToInt32(lblcompID.Text) + "' ");
             }
            if (ddlsortby.SelectedIndex == 2 && txtpositionwise.Text != "")
             {
                 dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid where (Select EmployeeId from SchedulePanelTB where SchedulePanelTB.ScheduleId=ST.ScheduleId and EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "')='" + Convert.ToInt32(Session["UserId"]) + "' And VancancyTB.Title = '" + txtpositionwise.Text + "' ");
             }
            if (ddlsortby.SelectedIndex == 3 && txtcandidatewise.Text != "")
             {
                 dtgrid = g.ReturnData("select ST.ScheduleId,ScheduleDetailsTB.ScheduleDetailsId,CompanyInfoTB.CompanyName,VancancyTB.Title as position,CandidateTB.Name as candidatename,ST.Date,ScheduleDetailsTB.FromTime,ScheduleDetailsTB.ToTime,ST.Vanue,ScheduleDetailsTB.Status from ScheduleTB ST left outer join ScheduleDetailsTB on ST.ScheduleId=ScheduleDetailsTB.ScheduleID left outer join CompanyInfoTB ON ST.CompanyId=CompanyInfoTB.CompanyId left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID left outer join VancancyTB on ScheduleDetailsTB.VacancyId=VancancyTB.vacancyid where (Select EmployeeId from SchedulePanelTB where SchedulePanelTB.ScheduleId=ST.ScheduleId and EmployeeId='" + Convert.ToInt32(Session["UserId"]) + "')='" + Convert.ToInt32(Session["UserId"]) + "' And CandidateTB.Name='" + txtcandidatewise.Text + "'");
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
        for (int i = 0; i < grdschedule.Rows.Count; i++)
        {
            ImageButton imgedit = (ImageButton)grdschedule.Rows[i].FindControl("imgedit");
            if (grdschedule.Rows[i].Cells[8].Text != "Scheduled")
            {
                imgedit.Enabled = false;
            }
            else
            {
                imgedit.Enabled = true;
            }
        }
    }

    private void fillcandidate()
    {
        try
        {
         if (ddlcompany.SelectedIndex > 0 && ddlposition.SelectedIndex > 0)
           {

            DataTable dtcandi = g.ReturnData("Select CandidateTB.Candidate_ID,Name  from CandidateTB where Company_ID='" + Convert.ToInt32(ddlcompany.SelectedValue) + "' and Vaccancy_ID='" + Convert.ToInt32(ddlposition.SelectedValue) + "' and CandidateTB.Candidate_ID not in (Select ScheduleDetailsTB.CandidateID   from ScheduleDetailsTB)");

            if (dtcandi.Rows.Count > 0)
            {
                ddlcandidate.DataSource = dtcandi;
                ddlcandidate.DataTextField = "Name";
                ddlcandidate.DataValueField = "Candidate_ID";
                ddlcandidate.DataBind();
                ddlcandidate.Items.Insert(0, "--Select Candidate--");
            }
            else
            {
                ddlcandidate.Items.Clear();
            }
        }
        else
        {
            ddlcandidate.Items.Clear();
        }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void fillcompany()
    {
        var companydata = from d in HR.CompanyInfoTBs
                          select new { d.CompanyId,d.CompanyName};
        if (companydata.Count()>0)
        {
            ddlcompany.DataSource = companydata;
            ddlcompany.DataTextField = "CompanyName";
            ddlcompany.DataValueField = "CompanyId";
            ddlcompany.DataBind();
            ddlcompany.Items.Insert(0, "--Select--");
        }
    }

    private void fillposition(string p)
    {
        try
        {
        var positiondata = from d in HR.VancancyTBs
                           where d.CompanyID==Convert.ToInt32(p)
                           select new { d.VacancyID,d.Title};
        if (positiondata.Count()>0)
        {
            ddlposition.DataSource = positiondata;
            ddlposition.DataTextField = "Title";
            ddlposition.DataValueField = "VacancyID";
            ddlposition.DataBind();
            ddlposition.Items.Insert(0, "--Select--");
            
        }
        else
        {
            ddlposition.Items.Clear();
        }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ddlcandidate.Enabled = true;
        ddlcandidate.Items.Clear();
        fillcandidate();
        Session["checkid"] = "1";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
        Clear();
    }

    private void Clear()
    {
        Session["checkid"] = "0";
        MultiView1.ActiveViewIndex = 0;
        ddlcompany.Enabled = true;
        ddlposition.Enabled = true;
        ddlcandidate.Enabled = true;
        ddlcompany.SelectedIndex = 0;
        ddlposition.Items.Clear();
        ddlemployee.Items.Clear();
        ddhh.SelectedIndex = 0;
        ddmm.SelectedIndex = 0;
        ddampm.SelectedIndex = 0;
        ddhhT.SelectedIndex = 0;
        ddmmT.SelectedIndex = 0;
        ddampmT.SelectedIndex = 0;
        ddlcandidate.Items.Clear();
        txtdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        txtvanue.Text = "";
        ViewState["dtemp"]=null;
        dtemp = null;
        listemp.Items.Clear();
        listemp.Visible = false;
        btnRemoveEmp.Visible = false;
        btnsave.Text = "Save";
        ViewState["dtposition"] = null;
        dtposition = null;
        grdCan.DataSource = null;
        grdCan.DataBind();

    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedIndex!=0)
        {
            fillemployee(ddlcompany.SelectedValue);
            fillposition(ddlcompany.SelectedValue);
            fillcandidate();
        }
        else
        {
            ddlemployee.Items.Clear();
            ddlposition.Items.Clear();
        }
       
    }

    private void fillemployee(string p)
    {
        try
        {

       
        var empdata = from d in HR.EmployeeTBs
                      where d.CompanyId == Convert.ToInt32(p) && d.RelivingStatus==null
                      select new { d.EmployeeId,EmpName=d.FName+" "+d.Lname};
        if (empdata.Count()>0)
        {
            ddlemployee.DataSource = empdata;
            ddlemployee.DataTextField = "EmpName";
            ddlemployee.DataValueField = "EmployeeId";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, "--Select--");

        }
        else
        {
            ddlemployee.Items.Clear();
        }
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void btnAddemp_Click(object sender, EventArgs e)
    {
        listemp.Visible = true;
        btnRemoveEmp.Visible = true;
        int cnt = 0;
        if (ViewState["dtemp"] != null)
        {
            dtemp = (DataTable)ViewState["dtemp"];
        }
        else
        {
            dtemp = new DataTable();
            DataColumn CatId = dtemp.Columns.Add("empid");
            DataColumn CatName = dtemp.Columns.Add("empname");
        }
        DataRow dr = dtemp.NewRow();
        dr[0] = ddlemployee.SelectedValue;
        dr[1] = ddlemployee.SelectedItem;
        if (dtemp.Rows.Count > 0)
        {
            for (int f = 0; f < dtemp.Rows.Count; f++)
            {

                string u2 = dtemp.Rows[f][0].ToString();
                if (u2 == ddlemployee.SelectedValue)
                {
                    cnt++;

                }
            }

            if (cnt > 0)
            {
                g.ShowMessage(this.Page, "Employee already added");
            }
            else
            {

                dtemp.Rows.Add(dr);

            }
        }
        else
        {
            dtemp.Rows.Add(dr);

        }
        listemp.DataSource = dtemp;
        for (int f = 0; f < dtemp.Rows.Count; f++)
        {
            listemp.DataTextField = "empname";
            listemp.DataValueField = "empid";
            listemp.DataBind();
        }


       
        ViewState["dtemp"] = dtemp;
       
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (listemp.Visible == false)
        {
            g.ShowMessage(this.Page, "Please Add Employee");
        }
        else
        {
            if (ViewState["dtposition"] == null)
            {
                g.ShowMessage(this.Page, "Please add Candidate for position");
            }
            else
            {

                if (btnsave.Text == "Save")
                {
                    string st = Session["checkid"].ToString(); 
                    if (Session["checkid"] == "1")
                    {

                        ScheduleTB SC = new ScheduleTB();

                        SC.CompanyId = Convert.ToInt32(ddlcompany.SelectedValue);
                        SC.Vanue = txtvanue.Text;
                        SC.Status = "Scheduled";
                        SC.Date = Convert.ToDateTime(txtdate.Text);

                        HR.ScheduleTBs.InsertOnSubmit(SC);
                        HR.SubmitChanges();
                        for (int i = 0; i < listemp.Items.Count; i++)
                        {
                            SchedulePanelTB SCP = new SchedulePanelTB();
                            SCP.ScheduleId = SC.ScheduleId;
                            // SCP.CandidateId = Convert.ToInt32(ddlcandidate.SelectedValue);
                            SCP.EmployeeId = Convert.ToInt32(listemp.Items[i].Value);
                            SCP.Status = "Scheduled";
                            HR.SchedulePanelTBs.InsertOnSubmit(SCP);
                            HR.SubmitChanges();

                        }
                        dtposition = (DataTable)ViewState["dtposition"];
                        for (int j = 0; j < dtposition.Rows.Count; j++)
                        {
                            ScheduleDetailsTB SCD = new ScheduleDetailsTB();
                            SCD.ScheduleId = SC.ScheduleId;
                            SCD.CompanyId = Convert.ToInt32(ddlcompany.SelectedValue);
                            SCD.VacancyID = Convert.ToInt32(dtposition.Rows[j][0].ToString());
                            SCD.CandidateID = Convert.ToInt32(dtposition.Rows[j][2].ToString());
                            SCD.FromTime = dtposition.Rows[j][4].ToString();
                            SCD.ToTime = dtposition.Rows[j][5].ToString();
                            SCD.Status = "Scheduled";
                            HR.ScheduleDetailsTBs.InsertOnSubmit(SCD);
                            HR.SubmitChanges();

                            HiddenFieldScheduledDetailsId.Value = Convert.ToString(SCD.ScheduleDetailsId);
                            HiddenScheduledID.Value = Convert.ToString(SC.ScheduleId);
                            //HiddenFieldCandidateID.Value = Convert.ToString(SCD.CandidateID);
                            SendEmailToCandidate();
                        }
                        Session["checkid"] = "0";
                    
                        sendMailToInterViewer();
                        g.ShowMessage(this.Page, "Submitted Successfully");
                    }
                }
                else
                {
                    #region
                    ScheduleTB SC = HR.ScheduleTBs.Where(d => d.ScheduleId == Convert.ToInt32(lblschedulid.Text)).First();
                    SC.CompanyId = Convert.ToInt32(ddlcompany.SelectedValue);
                    SC.Vanue = txtvanue.Text;
                    SC.Date = Convert.ToDateTime(txtdate.Text);
                   
                    HR.SubmitChanges();
                    DataTable dtdelete = g.ReturnData("delete from SchedulePanelTB where ScheduleId='" + Convert.ToInt32(lblschedulid.Text) + "'");
                    for (int i = 0; i < listemp.Items.Count; i++)
                    {
                        SchedulePanelTB SCP = new SchedulePanelTB();
                        SCP.ScheduleId = SC.ScheduleId;
                       //SCP.CandidateId = Convert.ToInt32(ddlcandidate.SelectedValue);
                        SCP.EmployeeId = Convert.ToInt32(listemp.Items[i].Value);
                        SCP.Status = "Scheduled";
                        HR.SchedulePanelTBs.InsertOnSubmit(SCP);
                        HR.SubmitChanges();
                    }
                    DataTable dtdeleteschDetails = g.ReturnData("delete from ScheduleDetailsTB where ScheduleID='" + Convert.ToInt32(lblschedulid.Text) + "'");
                    dtposition = (DataTable)ViewState["dtposition"];
                    for (int j = 0; j < dtposition.Rows.Count; j++)
                    {
                        ScheduleDetailsTB SCD = new ScheduleDetailsTB();
                        SCD.ScheduleId = Convert.ToInt32(lblschedulid.Text);
                        SCD.CompanyId = Convert.ToInt32(ddlcompany.SelectedValue);
                        SCD.VacancyID = Convert.ToInt32(dtposition.Rows[j][0].ToString());
                        SCD.CandidateID = Convert.ToInt32(dtposition.Rows[j][2].ToString());
                        SCD.FromTime = dtposition.Rows[j][4].ToString();
                        SCD.ToTime = dtposition.Rows[j][5].ToString();
                        SCD.Status = "Scheduled";
                        HR.ScheduleDetailsTBs.InsertOnSubmit(SCD);
                        HR.SubmitChanges();
                        
                    }
                    g.ShowMessage(this.Page, "Updated Successfully");
                    #endregion
                }

                Clear();
                bindgrid();
            }
        }

    }

    private void sendMailToInterViewer()
    {
        try
        {

            var empdata = from d in HR.SchedulePanelTBs
                          where d.ScheduleId == Convert.ToInt32(HiddenScheduledID.Value)
                          select new { d.EmployeeId };

            foreach (var item in empdata)
            {
                grdSendmailtoInterviewer.DataSource = null;
                grdSendmailtoInterviewer.DataBind();
                    var findMailidInterviewer = from d in HR.EmployeeTBs
                                            where d.EmployeeId == Convert.ToInt32(item.EmployeeId)
                                            select new { d.EmailId };
                if (findMailidInterviewer.Count() > 0)
                {
                    string str1 = Convert.ToString(findMailidInterviewer.Select(d => d.EmailId).First().ToString()).Trim();
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.To.Add(str1);
                    mail.From = new MailAddress("excellence.usatest@gmail.com", "Interview Details");
                    //mail.From = new MailAddress(SMTP_Settings.Email_smtp);
                    mail.Subject = "Interview Candidate Details";

                    grdSendmailtoInterviewer.DataSource = tblsendInterviewer;
                    //mail.Body = GridViewToHtml(grddisp);
                    BindMailGrdToInterviewer();
                    //string body = "Dear  " + lblname.Text + "," + "<br/>" + "<br/>" + "  Your offer details as follow :" + "<br/>" + "<br/>" + GetGridviewData();
                    string body = "  Hi , <br/> Interview Details are as  follows :<br/><br/> <br/><br/>" + GetGridviewData1();
                    mail.Body = body;

                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = Encoding.UTF8;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    //smtp.Host = SMTP_Settings.smtp_servername;
                    //smtp.Credentials = new System.Net.NetworkCredential("joseph.hernandez@nextsteperp.com", "josephg0lf"); 
                    smtp.Credentials = new System.Net.NetworkCredential("excellence.usatest@gmail.com", "excellence@#123");
                    //smtp.Credentials = new System.Net.NetworkCredential(SMTP_Settings.Email_smtp, SMTP_Settings.Password_smtp);
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    //smtp.Port = int.Parse(SMTP_Settings.Port);
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //smtp.UseDefaultCredentials = false;
                    smtp.Send(mail);
                    // g.ShowMessage(this.Page, "Your password sent to your email..");
                }
            }

           

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private string GetGridviewData1()
    {

        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        if (tblsendCandidate != null)
        {
            grdSendmailtoInterviewer.DataSource = tblsendInterviewer;
            grdSendmailtoInterviewer.DataBind();
        }
        else
        {
            BindMailGrdToInterviewer();
        }
        grdSendmailtoInterviewer.AllowPaging = false;
        grdSendmailtoInterviewer.RenderControl(hw);

        return sb.ToString();

    }

    private void BindMailGrdToInterviewer()
    {
        try
        {
            //DataSet dt = g.ReturnData1("Select c.Name, cp.CompanyName ,  v.Title, CONVERT(varchar,s.Date, 101) AS Date, s.Vanue, Sd.FromTime, Sd.ToTime from ScheduleDetailsTB Sd Left outer Join CandidateTB c on Sd.CandidateID=c.Candidate_ID Left Outer Join CompanyInfoTB cp on Sd.CompanyID=cp.CompanyId Left Outer Join VancancyTB v on Sd.VacancyID=v.VacancyID Left outer Join ScheduleTB s on Sd.ScheduleId=s.ScheduleId  Where Sd.ScheduleDetailsId='" +Convert.ToInt32(HiddenFieldScheduledDetailsId.Value) +"' And Sd.ScheduleID='" +Convert.ToInt32(HiddenScheduledID.Value) +"' ");
            DataSet dt = g.ReturnData1("SELECT c.Name, cp.CompanyName, v.Title, CONVERT(varchar, s.Date, 101) AS Date, s.Vanue, Sd.FromTime, Sd.ToTime,'" + g.Getempname(Convert.ToInt32(HiddenScheduledID.Value)) + "' as empname  FROM  ScheduleDetailsTB AS Sd LEFT OUTER JOIN CandidateTB AS c ON Sd.CandidateID = c.Candidate_ID LEFT OUTER JOIN  CompanyInfoTB AS cp ON Sd.CompanyId = cp.CompanyId LEFT OUTER JOIN VancancyTB AS v ON Sd.VacancyID = v.VacancyID LEFT OUTER JOIN  ScheduleTB AS s ON Sd.ScheduleId = s.ScheduleId Where  Sd.ScheduleID='" + Convert.ToInt32(HiddenScheduledID.Value) + "'");
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {


                if (dt.Tables[0].Rows.Count == 0)
                {

                }
                else
                {
                    drInterviewer = tblsendInterviewer.NewRow();
                    drInterviewer[0] = dt.Tables[0].Rows[i]["Name"];
                    drInterviewer[1] = dt.Tables[0].Rows[i]["CompanyName"];
                    drInterviewer[2] = dt.Tables[0].Rows[i]["Title"];
                    drInterviewer[3] = dt.Tables[0].Rows[i]["Date"]; // Interviewer Date
                    drInterviewer[4] = dt.Tables[0].Rows[i]["empname"]; // Interviewer name
                    drInterviewer[5] = dt.Tables[0].Rows[i]["Vanue"];
                    drInterviewer[6] = dt.Tables[0].Rows[i]["FromTime"];
                    drInterviewer[7] = dt.Tables[0].Rows[i]["ToTime"];

                    tblsendInterviewer.Rows.Add(drInterviewer);
                    //Session["id"] = tblsendInterviewer;
                    // new grid for Mail

                }
            }
            grdSendmailtoInterviewer.DataSource = tblsendInterviewer;
            grdSendmailtoInterviewer.DataBind();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void SendEmailToCandidate()
    {
        try
        {
            var candata = from dt in HR.ScheduleDetailsTBs
                          where dt.ScheduleId == Convert.ToInt32(HiddenScheduledID.Value)
                          select new { dt.CandidateID};
            foreach (var item in candata)
            {
                HiddenFieldCandidateID.Value = Convert.ToString(item.CandidateID);
                var findMailid = from d in HR.CandidateTBs
                                 where d.Candidate_ID == Convert.ToInt32(item.CandidateID)
                                 select new { d.Email_Address };
                if (findMailid.Count() > 0)
                {
                    string str1 = Convert.ToString(findMailid.Select(d => d.Email_Address).First().ToString()).Trim();
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.To.Add(str1);
                    mail.From = new MailAddress("excellence.usatest@gmail.com", "Interview Details");
                    //mail.From = new MailAddress(SMTP_Settings.Email_smtp);
                    mail.Subject = "Interview Details";

                    grddisp.DataSource = tblsendCandidate;
                    //mail.Body = GridViewToHtml(grddisp);
                    BindMailGrdToCandidate();
                    //string body = "Dear  " + lblname.Text + "," + "<br/>" + "<br/>" + "  Your offer details as follow :" + "<br/>" + "<br/>" + GetGridviewData();
                    string body = "   Hi , <br/> Your Interview Details are as  follows :<br/><br/> <br/><br/>" + GetGridviewData();
                    mail.Body = body;

                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = Encoding.UTF8;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    //smtp.Host = SMTP_Settings.smtp_servername;
                    //smtp.Credentials = new System.Net.NetworkCredential("joseph.hernandez@nextsteperp.com", "josephg0lf"); 
                    smtp.Credentials = new System.Net.NetworkCredential("excellence.usatest@gmail.com", "excellence@#123");
                    //smtp.Credentials = new System.Net.NetworkCredential(SMTP_Settings.Email_smtp, SMTP_Settings.Password_smtp);
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    //smtp.Port = int.Parse(SMTP_Settings.Port);
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //smtp.UseDefaultCredentials = false;
                    smtp.Send(mail);
                    // g.ShowMessage(this.Page, "Your password sent to your email..");
                }
            }
        

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private string GetGridviewData()
    {
       
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        if (tblsendCandidate != null)
        {
            grddisp.DataSource = tblsendCandidate;
            grddisp.DataBind();
        }
        else
        {
            BindMailGrdToCandidate();
        }
        grddisp.AllowPaging = false;
        grddisp.RenderControl(hw);

        return sb.ToString();

    }

    private void BindMailGrdToCandidate()
    {
        try
        {
            tblsendCandidate = null;
            grddisp.DataSource = null;
            grddisp.DataBind();
            //DataSet dt = g.ReturnData1("Select c.Name, cp.CompanyName ,  v.Title, CONVERT(varchar,s.Date, 101) AS Date, s.Vanue, Sd.FromTime, Sd.ToTime from ScheduleDetailsTB Sd Left outer Join CandidateTB c on Sd.CandidateID=c.Candidate_ID Left Outer Join CompanyInfoTB cp on Sd.CompanyID=cp.CompanyId Left Outer Join VancancyTB v on Sd.VacancyID=v.VacancyID Left outer Join ScheduleTB s on Sd.ScheduleId=s.ScheduleId  Where Sd.ScheduleDetailsId='" +Convert.ToInt32(HiddenFieldScheduledDetailsId.Value) +"' And Sd.ScheduleID='" +Convert.ToInt32(HiddenScheduledID.Value) +"' ");
            DataSet dt = g.ReturnData1("SELECT  c.Name, cp.CompanyName, v.Title, CONVERT(varchar, s.Date, 101) AS Date, s.Vanue, Sd.FromTime, Sd.ToTime,'" + g.Getempname(Convert.ToInt32(HiddenScheduledID.Value)) + "' as empname  FROM  ScheduleDetailsTB AS Sd LEFT OUTER JOIN CandidateTB AS c ON Sd.CandidateID = c.Candidate_ID LEFT OUTER JOIN  CompanyInfoTB AS cp ON Sd.CompanyId = cp.CompanyId LEFT OUTER JOIN VancancyTB AS v ON Sd.VacancyID = v.VacancyID LEFT OUTER JOIN  ScheduleTB AS s ON Sd.ScheduleId = s.ScheduleId Where  Sd.ScheduleID='" + Convert.ToInt32(HiddenScheduledID.Value) + "' and Candidate_ID='"+Convert.ToInt32(HiddenFieldCandidateID.Value)+"'");
            if (dt.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                if (tblsendCandidate == null)
                {
                    #region DataTableToadd candidate column to send mail To Candidate
                    tblsendCandidate = new DataTable();
                    tblsendCandidate.Columns.Add(new DataColumn("Candidate Name", typeof(string)));
                    tblsendCandidate.Columns.Add(new DataColumn("Company Name", typeof(string)));
                    tblsendCandidate.Columns.Add(new DataColumn("Position", typeof(string)));
                    tblsendCandidate.Columns.Add(new DataColumn("Interview Date", typeof(string)));
                    tblsendCandidate.Columns.Add(new DataColumn("Interviewer Name", typeof(string)));
                    tblsendCandidate.Columns.Add(new DataColumn("Venu", typeof(string)));
                    tblsendCandidate.Columns.Add(new DataColumn("Time From", typeof(string)));
                    tblsendCandidate.Columns.Add(new DataColumn("Time To", typeof(string)));
                    #endregion
                }
                drCandidate = tblsendCandidate.NewRow();
                drCandidate[0] = dt.Tables[0].Rows[0]["Name"];
                drCandidate[1] = dt.Tables[0].Rows[0]["CompanyName"];
                drCandidate[2] = dt.Tables[0].Rows[0]["Title"];
                drCandidate[3] = dt.Tables[0].Rows[0]["Date"];
                drCandidate[4] = dt.Tables[0].Rows[0]["empname"];
                drCandidate[5] = dt.Tables[0].Rows[0]["Vanue"];
                drCandidate[6] = dt.Tables[0].Rows[0]["FromTime"];
                drCandidate[7] = dt.Tables[0].Rows[0]["ToTime"];

                tblsendCandidate.Rows.Add(drCandidate);
                //Session["id"] = tblsendCandidate;
                //// new grid for Mail

            }
            grddisp.DataSource = tblsendCandidate;
            grddisp.DataBind();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        string scheduleid = imgbtn.CommandArgument;
        lblschedulid.Text = scheduleid;
        var scheduledata=from d in HR.ScheduleTBs
                         where d.ScheduleId==Convert.ToInt32(scheduleid)
                         select new{d.CompanyId,d.Date,d.Vanue};
        foreach (var item in scheduledata)
	{
        DateTime dtdate =Convert.ToDateTime(item.Date);
        txtdate.Text = dtdate.ToString("MM/dd/yyyy");
        txtvanue.Text = item.Vanue;
        ddlcompany.SelectedValue = Convert.ToString(item.CompanyId);
        ddlcompany.Enabled = false;
        fillposition(ddlcompany.SelectedValue);
        ddlposition.Enabled = false;
        DataTable dtcandi = g.ReturnData("select distinct CandidateTB.Name,CandidateTB.Candidate_ID from ScheduleDetailsTB left outer join CandidateTB on ScheduleDetailsTB.CandidateId=CandidateTB.Candidate_ID where ScheduleID='" + Convert.ToInt32(scheduleid) + "' ");
        ddlcandidate.DataSource = dtcandi;
        ddlcandidate.DataTextField = "Name";
        ddlcandidate.DataValueField = "Candidate_ID";
        ddlcandidate.DataBind();
        ddlcandidate.Items.Insert(0,"--Select--");
        ddlcandidate.SelectedIndex = -1;
        ddlcandidate.Enabled = false;
       
       
	}
        DataTable dtCandidate = g.ReturnData("Select SC.VacancyID,VancancyTB.Title as VacancyName,SC.CandidateID,CandidateTB.Name as CandidateName,SC.FromTime,SC.ToTime from ScheduleDetailsTB SC  left outer join VancancyTB on SC.VacancyID=VancancyTB.VacancyID left outer join CandidateTB on SC.CandidateID=CandidateTB.Candidate_ID where ScheduleId='" + Convert.ToInt32(scheduleid) + "'");
        if (dtCandidate.Rows.Count>0)
        {
            ViewState["dtposition"] = dtCandidate;
            grdCan.DataSource = dtCandidate;
            grdCan.DataBind();
        }
        else
        {
            ViewState["dtposition"] = null;
            grdCan.DataSource = null;
            grdCan.DataBind();
        }
        fillemployee(ddlcompany.SelectedValue);
        DataTable dtemp = g.ReturnData("select SchedulePanelTB.EmployeeId as empid,EmployeeTB.Fname+' '+EmployeeTB.Lname as empname from SchedulePanelTB left outer join EmployeeTB on SchedulePanelTB.EmployeeId=EmployeeTB.EmployeeId where ScheduleId='" + Convert.ToInt32(scheduleid) + "'");
        listemp.Visible = true;
        btnRemoveEmp.Visible = true;
        listemp.DataSource = dtemp;
        listemp.DataTextField = "empname";
        listemp.DataValueField = "empid";
        listemp.DataBind();
        ViewState["dtemp"]=dtemp;
        MultiView1.ActiveViewIndex = 1;

        btnsave.Text = "Update";

    }
    protected void txtdate_TextChanged(object sender, EventArgs e)
    {
        if (txtdate.Text != "")
        {
            if (Convert.ToDateTime(txtdate.Text) <Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")))
            {
                
                g.ShowMessage(this.Page, "You can not enter Interview date less than Today Date");
                txtdate.Text = "";
            }
        }
    }
    protected void btnRemoveEmp_Click(object sender, EventArgs e)
    {
        if (listemp.SelectedItem != null)
        {
           
            dtemp = (DataTable)ViewState["dtemp"];

            foreach (DataRow d in dtemp.Rows)
            {
                if (d[0].ToString() == listemp.SelectedValue)
                {

                    d.Delete();
                    dtemp.AcceptChanges();
                    listemp.Items.RemoveAt(listemp.SelectedIndex);
                    break;
                }
            }
           
            ViewState["dtemp"] = dtemp;
        }
        if (listemp.Items.Count == 0)
        {
            listemp.Visible = false;
            btnRemoveEmp.Visible = false;            
        }
    }
    protected void imgeditmult_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        string positionId = imgedit.CommandArgument;
        string[] positionId1 = positionId.Split(',');
        dtposition = (DataTable)ViewState["dtposition"];
        foreach (DataRow d in dtposition.Rows)
        {
            if (d[0].ToString() == positionId1[0].ToString() && d[2].ToString()==positionId1[1].ToString())
            {
                ddlposition.SelectedValue = d["VacancyId"].ToString();
                ddlcandidate.SelectedValue = d["CandidateId"].ToString();
                string fromtime = d["FromTime"].ToString();
                string []fromtime1=fromtime.Split(' ');
                ddhh.SelectedValue = fromtime1[0].ToString();
                ddmm.SelectedValue = fromtime1[2].ToString();
                ddampm.SelectedValue = fromtime1[3].ToString();
                string totime = d["ToTime"].ToString();
                string []totime1=totime.Split(' ');
                ddhhT.SelectedValue = totime1[0].ToString();
                ddmmT.SelectedValue = totime1[2].ToString();
                ddampmT.SelectedValue = totime1[3].ToString();
                d.Delete();
                dtposition.AcceptChanges();
                break;
            }
        }

        grdCan.DataSource = dtposition;
        grdCan.DataBind();
        ViewState["dtposition"] = dtposition;
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgdelete = (ImageButton)sender;
        string positionId = imgdelete.CommandArgument;
        string[] positionId1 = positionId.Split(',');
        dtposition = (DataTable)ViewState["dtposition"];
        foreach (DataRow d in dtposition.Rows)
        {
            if (d[0].ToString() == positionId1[0].ToString() && d[2].ToString() == positionId1[1].ToString())
            {
                d.Delete();
                dtposition.AcceptChanges();
                break;
            }
        }

        grdCan.DataSource = dtposition;
        grdCan.DataBind();
        ViewState["dtposition"] = dtposition;
    }
    protected void btnaddcandidate_Click(object sender, EventArgs e)
    {
        int cnt = 0;
        if (ViewState["dtposition"] != null)
        {
            dtposition = (DataTable)ViewState["dtposition"];
        }
        else
        {
            dtposition = new DataTable();
            DataColumn VacancyId = dtposition.Columns.Add("VacancyId");
            DataColumn VacancyName = dtposition.Columns.Add("VacancyName");
            DataColumn CandidateId = dtposition.Columns.Add("CandidateId");
            DataColumn CandidateName = dtposition.Columns.Add("CandidateName");
            DataColumn FromTime = dtposition.Columns.Add("FromTime");
            DataColumn ToTime = dtposition.Columns.Add("ToTime");
        }
        DataRow dr = dtposition.NewRow();
        dr[0] = ddlposition.SelectedValue;
        dr[1] = ddlposition.SelectedItem.Text;
        dr[2] = ddlcandidate.SelectedValue;
        dr[3] = ddlcandidate.SelectedItem.Text;
        dr[4] = ddhh.SelectedValue + " " + ":" + " " + ddmm.SelectedValue + " " + ddampm.SelectedValue;
        dr[5] = ddhhT.SelectedValue + " " + ":" + " " + ddmmT.SelectedValue + " " + ddampmT.SelectedValue;
        if (dtposition.Rows.Count > 0)
        {
            for (int f = 0; f < dtposition.Rows.Count; f++)
            {

                string u2 = dtposition.Rows[f][0].ToString();
                if (u2 == ddlposition.SelectedValue && dtposition.Rows[f][2].ToString() == ddlcandidate.SelectedValue)
                {
                    cnt++;

                }
            }

            if (cnt > 0)
            {
                g.ShowMessage(this.Page, "Position already added");
            }
            else
            {

                dtposition.Rows.Add(dr);

            }
        }
        else
        {
            dtposition.Rows.Add(dr);

        }
        grdCan.DataSource = dtposition;
        grdCan.DataBind();
        clearCandidate();

        ViewState["dtposition"] = dtposition;
    }
    private void clearCandidate()
    {
        ddlposition.SelectedIndex = 0;
        ddlcandidate.Items.Clear();
        ddhh.SelectedIndex = 0;
        ddmm.SelectedIndex = 0;
        ddampm.SelectedIndex = 0;
        ddhhT.SelectedIndex = 0;
        ddmmT.SelectedIndex = 0;
        ddampmT.SelectedIndex = 0;
    }
    protected void ddlposition_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlposition.SelectedIndex!=0)
        {
            fillcandidate();
        }
        
        
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
        if (ddlsortby.SelectedIndex ==0)
        {
            lblname.Visible = false;
            lblname.Text = "";
            txtcompanywise.Visible = false;
            txtpositionwise.Visible = false;
            txtcandidatewise.Visible = false;
            bindgrid();
        }
        if (ddlsortby.SelectedIndex ==1)
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
        if (txtcompanywise.Text !="")
        {
            var compId = from d in HR.CompanyInfoTBs
                         where d.CompanyName == txtcompanywise.Text
                         select new {d.CompanyId };
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