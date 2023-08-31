using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Recruitment_AgencyMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex =0;
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                bindgrid();
                fillSpeciality();

            } 
            if (!(String.IsNullOrEmpty(txtpassword.Text.Trim())))
            {
                txtpassword.Attributes["value"] = txtpassword.Text;
            }            
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void fillSpeciality()
    {
        var specialitydata = from d in HR.SpecialityMasterTBs
                             select new { d.SpecialityId,d.SpecialityName};
        if (specialitydata.Count()>0)
        {
            ddlSpeciality.DataSource = specialitydata;
            ddlSpeciality.DataTextField = "SpecialityName";
            ddlSpeciality.DataValueField = "SpecialityId";
            ddlSpeciality.DataBind();
            ddlSpeciality.Items.Insert(0, "--Select--");
        }
    }

    private void bindgrid()
    {
        DataTable dtAgency = new DataTable();
        if (ddlsortby.SelectedIndex==0)
        {
          dtAgency = g.ReturnData("Select AgencyId,AgencyName,Address,MobNo,ContactPerson,EmailId,SpecialityMasterTB.SpecialityName from AgencyMasterTB left outer join SpecialityMasterTB on AgencyMasterTB.SpecialityId=SpecialityMasterTB.SpecialityId");
        
       }
        if (ddlsortby.SelectedIndex ==1 && txtagencywise.Text!="")
        {
            dtAgency = g.ReturnData("Select AgencyId,AgencyName,Address,MobNo,ContactPerson,EmailId,SpecialityMasterTB.SpecialityName from AgencyMasterTB left outer join SpecialityMasterTB on AgencyMasterTB.SpecialityId=SpecialityMasterTB.SpecialityId Where AgencyName='"+txtagencywise.Text+"'");
        }
        if (ddlsortby.SelectedIndex == 2 && txtcontactwise.Text != "")
        {
            dtAgency = g.ReturnData("Select AgencyId,AgencyName,Address,MobNo,ContactPerson,EmailId,SpecialityMasterTB.SpecialityName from AgencyMasterTB left outer join SpecialityMasterTB on AgencyMasterTB.SpecialityId=SpecialityMasterTB.SpecialityId Where ContactPerson='"+txtcontactwise.Text+"'");
        }

        if (ddlsortby.SelectedIndex == 3 && txtspecilitywise.Text != "")
        {
            dtAgency = g.ReturnData("Select AgencyId,AgencyName,Address,MobNo,ContactPerson,EmailId,SpecialityMasterTB.SpecialityName from AgencyMasterTB left outer join SpecialityMasterTB on AgencyMasterTB.SpecialityId=SpecialityMasterTB.SpecialityId Where AgencyMasterTB.SpecialityId='" + Convert.ToInt32(lblspecialitiID.Text) +"'");
        }

        if (dtAgency.Rows.Count > 0)
        {
            grdAgency.DataSource = dtAgency;
            grdAgency.DataBind();
            lblcount.Text = dtAgency.Rows.Count.ToString();
        }
        else
        {
            grdAgency.DataSource = null;
            grdAgency.DataBind();
            lblcount.Text = "0";
        }
    }



    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (btnsave.Text == "Save")
        {
            var existdata = from d in HR.AgencyMasterTBs where d.AgencyName == txtAgency.Text select d;
            if (existdata.Count() > 0)
            {
                g.ShowMessage(this.Page, "Agency Name already Exist");
            }
            else
            {
                AgencyMasterTB SP = new AgencyMasterTB();
                SP.AgencyName = txtAgency.Text;
                SP.Address = txtarea.Text;
                SP.MobNo = txtmob.Text;
                SP.AltMobNo = txtAltmob.Text;
                SP.EmailId = txtemailID.Text;
                SP.ContactPerson = txtContactP.Text;
                SP.Password = txtpassword.Text;
                if (txtpayment.Text!="")
                {
                    SP.Payment = Convert.ToDecimal(txtpayment.Text);
                }
               
                SP.SpecialityId = Convert.ToInt32(ddlSpeciality.SelectedValue);
                SP.AgreementPath = lblagreementpath.Text;
                SP.Status = 0; // By default save status is Zero(0).
                HR.AgencyMasterTBs.InsertOnSubmit(SP);
                HR.SubmitChanges();
                lblagencyidforsendmail.Text = SP.AgencyId.ToString();
                SendMail();
                g.ShowMessage(this.Page, "Agency Details Saved Successfully");
                bindgrid();
                Clear();
            }
        }
        else
        {
            var existdata = from d in HR.AgencyMasterTBs where d.AgencyName == txtAgency.Text && d.AgencyId != Convert.ToInt32(lblAgencyId.Text) select d;
            if (existdata.Count() > 0)
            {
                g.ShowMessage(this.Page, "Agency Name already Exist");
            }
            else
            {
                AgencyMasterTB SP = HR.AgencyMasterTBs.Where(d => d.AgencyId == Convert.ToInt32(lblAgencyId.Text)).First();
                SP.AgencyName = txtAgency.Text;
                SP.Address = txtarea.Text;
                SP.MobNo = txtmob.Text;
                SP.AltMobNo = txtAltmob.Text;
                SP.EmailId = txtemailID.Text;
                SP.ContactPerson = txtContactP.Text;
                SP.Password = txtpassword.Text;
                if (txtpayment.Text != "")
                {
                    SP.Payment = Convert.ToDecimal(txtpayment.Text);
                }
                SP.SpecialityId = Convert.ToInt32(ddlSpeciality.SelectedValue);
                SP.AgreementPath = lblagreementpath.Text;
                HR.SubmitChanges();
                g.ShowMessage(this.Page,"Agency Details Updated Successfully");
                //Response.Redirect("AgencyMaster.aspx");
                Clear();
                MultiView1.ActiveViewIndex = 0; 
                bindgrid();
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
       
        txtAgency.Text = "";
        txtmob.Text = "";
        txtAltmob.Text = "";
        txtarea.Text = "";
        txtemailID.Text = "";
        txtContactP.Text = "";
        txtpayment.Text = "";
        if (ddlSpeciality.Items.Count>0)
        {
            ddlSpeciality.SelectedIndex = 0;
        }
       
        lblagreementpath.Text = "";
        btnsave.Text = "Save";
        MultiView1.ActiveViewIndex = 0;
    }
    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        lblAgencyId.Text = imgbtn.CommandArgument;
        var agencydata = from d in HR.AgencyMasterTBs
                         where d.AgencyId==Convert.ToInt32(lblAgencyId.Text)
                          select new { d.AgencyName,d.Password, d.Address,d.MobNo,d.AltMobNo,d.EmailId,d.ContactPerson,d.Payment,d.AgreementPath,d.SpecialityId };
        foreach (var item in agencydata)
        {
            txtAgency.Text = item.AgencyName;
            txtmob.Text = item.MobNo;
            txtAltmob.Text = item.AltMobNo;
            txtarea.Text = item.Address;
            txtemailID.Text = item.EmailId;
            txtpassword.Text = item.Password;
            txtContactP.Text = item.ContactPerson;
            txtpayment.Text =Convert.ToString(item.Payment);
            ddlSpeciality.SelectedValue = Convert.ToString(item.SpecialityId);
            lblagreementpath.Text = item.AgreementPath;
        }
        MultiView1.ActiveViewIndex = 1;
        btnsave.Text = "Update";
    }
    protected void btnuploadresume_Click(object sender, EventArgs e)
    {
        string FolderPath = Server.MapPath("~/Recruitment/AgencyDoc");
        MakeDirectoryIfExist(FolderPath);
        if (FileUpload1.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string ext = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (File.Exists(Server.MapPath("~/Recruitment/AgencyDoc/" + filename)))
                {                   
                    g.ShowMessage(this.Page, "This Agreement Already Exists");
                }
                else
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Recruitment/AgencyDoc/" + filename));
                    string AttachPath = filename;
                    lblagreementpath.Text = AttachPath;

                }
            }
            catch (Exception ex)
            {
                g.ShowMessage(this.Page, "" + ex.Message + "");
                
            }
        }
        else
        {
            g.ShowMessage(this.Page, "Please Choose file");           

        }
    }
    private void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
    }

    #region Mail after new Agency registration
    private void SendMail()
    {
        try
        {
            string EM1 = "";
            DataTable dss = g.ReturnData("Select EmailId, Password,EmailID AS UserName  from AgencyMasterTB where AgencyId='" + Convert.ToInt32(lblagencyidforsendmail.Text) + "' ");
            if (dss.Rows.Count > 0)
            {
                lblmailid.Text = dss.Rows[0]["EmailId"].ToString();
                lblusername.Text = dss.Rows[0]["UserName"].ToString();
                lblpassword.Text = dss.Rows[0]["Password"].ToString();
            }
            else
            {
                lblmailid.Text = "";
            }
           EM1 = lblmailid.Text;
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
            SMTPSettingsTB smtpData = HR.SMTPSettingsTBs.FirstOrDefault();
            // DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblmailid.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress(smtpData.emailFromAddress);
            mail.Subject = "Your Registration Succefully";
            mail.Body = "Dear Your Login details as follow  " + "," + "<br/>" + "<br/>" + "User Name :  " + lblusername.Text + "<br/>" + "Password :" + lblpassword.Text + "<br> <a href='http://webtest.excellenceserver.com/hrportalglobal/recruitment/login.aspx'>Click here to login</a> <br>Thanks," + " ";

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
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
            SMTPSettingsTB smtpData = HR.SMTPSettingsTBs.FirstOrDefault();
            // DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblmailid.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress(smtpData.emailFromAddress);
            mail.Subject = "Your Registration Succefully";
            mail.Body = "Dear Your Login details as follow  " + "," + "<br/>" + "<br/>" + "User Name :  " + lblusername.Text + "<br/>" + "Password :" + lblpassword.Text + "<br> <a href='http://webtest.excellenceserver.com/hrportalglobal/recruitment/login.aspx'>Click here to login</a> <br>Thanks," + " ";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.live.com";
            smtp.Credentials = new System.Net.NetworkCredential(smtpData.emailFromAddress, smtpData.emailFromPassword);

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
            // DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblmailid.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress("excellence20130401@gmail.com");
            mail.Subject = "Your Registration Succefully";
            mail.Body = "Dear Your Login details as follow  " + "," + "<br/>" + "<br/>" + "User Name :  " + lblusername.Text + "<br/>" + "Password :" + lblpassword.Text + "<br> <a href='http://webtest.excellenceserver.com/hrportalglobal/recruitment/login.aspx'>Click here to login</a> <br>Thanks," + " ";

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.mail.yahoo.com";
            smtp.Credentials = new System.Net.NetworkCredential("excellence20130401@gmail.com", "sup123456");

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
            SMTPSettingsTB smtpData = HR.SMTPSettingsTBs.FirstOrDefault();
            //DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblmailid.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress(smtpData.emailFromAddress);
            mail.Subject = "Offer Details";

            mail.Subject = "Your Registration Succefully";
            mail.Body = "Dear Your Login details as follow  " + "," + "<br/>" + "<br/>" + "User Name :  " + lblusername.Text + "<br/>" + "Password :" + lblpassword.Text + "<br> <a href='http://webtest.excellenceserver.com/hrportalglobal/recruitment/login.aspx'>Click here to login</a> <br>Thanks," + " ";

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
            SMTPSettingsTB smtpData = HR.SMTPSettingsTBs.FirstOrDefault();
            //DataSet dss = g.ReturnData1("Select EmailID from EmployeeRegistrationTB where EmployeeId='" + Convert.ToInt32(Session["Empid"]) + "' ");
            string email = lblmailid.Text;
            //string email = "abdul.rahman@excellenceit.in";
            System.Web.Mail.MailMessage objMail = new System.Web.Mail.MailMessage();

            string str1 = email;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(str1);
            mail.From = new MailAddress(smtpData.emailFromAddress);
            mail.Subject = "Your Registration Succefully";
            mail.Body = "Dear Your Login details as follow  " + "," + "<br/>" + "<br/>" + "User Name :  " + lblusername.Text + "<br/>" + "Password :" + lblpassword.Text + "<br> <a href='http://webtest.excellenceserver.com/hrportalglobal/recruitment/login.aspx'>Click here to login</a> <br>Thanks," + " ";

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
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /*Verifies that the control is rendered */
    //}
  #endregion mail function

    protected void txtemailID_TextChanged(object sender, EventArgs e)
    {
        var checkmailinEmpTB = from x in HR.EmployeeTBs
                               where x.EmailId == txtemailID.Text || x.Email == txtemailID.Text || x.personalEmail == txtemailID.Text
                               select x;
        if (checkmailinEmpTB.Count() > 0)
        {
            txtemailID.Text = "";
            g.ShowMessage(this.Page, "This Email ID Already Exist in employee details");
            
        } 
    }
    protected void txtAgency_TextChanged(object sender, EventArgs e)
    {
        if (!txtAgency.Text.Any(char.IsLetter))
        {
            g.ShowMessage(this.Page, "Invalid Charcter");
            txtAgency.Focus();
            txtAgency.Text = "";
        }
      
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListAgency(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> Agency = (from d in hr.AgencyMasterTBs
                                        .Where(r => r.AgencyName.Contains(prefixText))
                               select d.AgencyName).Distinct().ToList();
        return Agency;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListcontact(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> contactP = (from d in hr.AgencyMasterTBs
                                        .Where(r => r.ContactPerson.Contains(prefixText))
                                 select d.ContactPerson).Distinct().ToList();
        return contactP;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionListSpeciality(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> speciality = (from d in hr.SpecialityMasterTBs
                                        .Where(r => r.SpecialityName.Contains(prefixText))
                                   select d.SpecialityName).Distinct().ToList();
        return speciality;
    }


    protected void ddlsortby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsortby.SelectedIndex==0)
        {
            txtagencywise.Visible = false;
            txtagencywise.Text = "";
            txtcontactwise.Visible = false;
            txtcontactwise.Text = "";
            txtspecilitywise.Visible = false;
            txtspecilitywise.Text = "";
            lblname.Text = "";
            lblname.Visible = false;
            bindgrid();
        }
        if (ddlsortby.SelectedIndex==1)
        {
            txtagencywise.Text = "";
            txtagencywise.Visible = true;

            txtcontactwise.Visible = false;
            txtspecilitywise.Visible = false;
            lblname.Text = "Agency Name";
            lblname.Visible = true;
        }

        if (ddlsortby.SelectedIndex == 2)
        {
            txtagencywise.Visible = false;
            txtcontactwise.Visible = true;
            txtcontactwise.Text = "";
            txtspecilitywise.Visible = false;
            lblname.Visible = true;
            lblname.Text = "Contact Person";
        }
        if (ddlsortby.SelectedIndex == 3)
        {
            txtagencywise.Visible = false;
            txtcontactwise.Visible = false;
            txtspecilitywise.Visible = true;
            txtspecilitywise.Text = "";
            lblname.Text = "Speciality";
            lblname.Visible = true;
        }
    }
    protected void txtspecilitywise_TextChanged(object sender, EventArgs e)
    {
        if (txtspecilitywise.Text !="")
        {
            var findspecislityId = from d in HR.SpecialityMasterTBs
                                   where d.SpecialityName == txtspecilitywise.Text
                                   select new {d.SpecialityId };
            if (findspecislityId.Count () > 0)
            {
                foreach (var item in findspecislityId)
                {
                    lblspecialitiID.Text = item.SpecialityId.ToString();
                }
            }
            else
            {
                lblspecialitiID.Text = "";
            }
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }
    protected void grdAgency_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAgency.PageIndex = e.NewPageIndex;
        bindgrid();
    }
}