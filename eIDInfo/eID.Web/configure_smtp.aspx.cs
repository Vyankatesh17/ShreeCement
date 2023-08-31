using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class configure_smtp : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    
    protected void btnConfigure_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {

                //var chkExists = db.CompanyRegistrationTBs.Where(d => d.Email == txtEmail.Text).Distinct();
                //var chkExists = db.CompanyRegistrationTBs.Where(d => d.Email == txtEmail.Text).Distinct();
                //if (chkExists.Count() > 0)
                //{
                //    gen.ShowMessage(this.Page, "Company already registered with provided email address.. Please try another email id..");
                //}
                //else
                //{
                    SMTPSettingsTB smtpData = new SMTPSettingsTB();
                    string securepass = SPPasswordHasher.Encrypt(txtPassword.Text);
                    smtpData.CompanyId = 0;
                    smtpData.emailFromAddress = txtEmail.Text;
                    smtpData.emailFromPassword = securepass;
                    smtpData.enableSSL = true;
                    smtpData.portNo = Convert.ToInt32(txtPortNo.Text);
                    smtpData.smtpAddress = txtSmtpAddress.Text;
                    smtpData.SMTPPassword = txtSMTPPassword.Text;
                    db.SMTPSettingsTBs.InsertOnSubmit(smtpData);
                    db.SubmitChanges();



                    gen.ShowMessageRedirect(this.Page, string.Format("SMTP Configured successfully"), "login.aspx");

                    txtEmail.Text = txtPassword.Text = txtPortNo.Text = txtSmtpAddress.Text = "";
                //}
            }
        }
    }
}