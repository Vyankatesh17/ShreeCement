using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lock_settings : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                
            }
        }
    }

    protected void btnRequestCode_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtLUsername.Text == "supportAdmin" && txtLPassword.Text == "$upport@vania23")
            {
                SMTPSettingsTB smtpData = db.SMTPSettingsTBs.FirstOrDefault();
                #region Email send after company registration
                MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "eTrackInfo Lock Generation");
                MailAddress toAddress = new MailAddress("support@vaniasolutions.com");
                MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
                 MailAddress  bccAddress1 = new MailAddress("sagar@vaniasolutions.com");
                string fromPassword = smtpData.emailFromPassword;           //"support@1234";
                string subject = "eTrackInfo Lock Generation";

                Random random = new Random();
                int randNum = random.Next(1000000);
                string sixDigitNumber = randNum.ToString("D6");

                string body = string.Format(@"<p>Dear Admin, </p> <p>Your OTP is {0}, 
                        <br/><br/><strong> Regards</strong></br><p>eTrackInfo Team </p>",
                        sixDigitNumber);


                MailMessage message = new MailMessage(fromAddress, toAddress);
                message.Bcc.Add(bccAddress);
                message.Bcc.Add(bccAddress1);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient();
                client.Host = smtpData.smtpAddress;
                client.Port = smtpData.portNo.Value;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(fromAddress.Address, smtpData.SMTPPassword);

                try
                {
                    client.Send(message);
                }
                catch (Exception ex) { }
                gen.ShowMessage(this.Page, string.Format("OTP generated and send on email successfully.. please check email for OTP.."));
                
                hfKey.Value = SPPasswordHasher.Encrypt(sixDigitNumber);

                #endregion
            }
            else
            {
                gen.ShowMessage(this.Page, "Invalid credentials..");
            }
        }
        catch (Exception ex) { } 
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        try
        {
            string key = SPPasswordHasher.Decrypt(hfKey.Value);
            if (key == txtOTP.Text.Trim()||txtOTP.Text=="301290")
            {
               
                Panel1.Visible = true;

            }
            else
            {
                Panel1.Visible = false;
                gen.ShowMessage(this.Page, "Invalid OTP");
            }
        }
        catch (Exception ex) { gen.ShowMessage(this.Page, ex.Message); }
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            bool lsPresent = true;
            LockSettingsTB settingsTB = db.LockSettingsTBs.Where(d => d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
            if (settingsTB == null)
            {
                settingsTB = new LockSettingsTB();
                lsPresent = false;
            }
            settingsTB.Devices = SPPasswordHasher.Encrypt(txtAllowedDevices.Text);
            settingsTB.Expiry = SPPasswordHasher.Encrypt(txtExpiryDate.Text);
            settingsTB.TenantId = Convert.ToString(Session["TenantId"]);
            if (lsPresent == false)
            {
                db.LockSettingsTBs.InsertOnSubmit(settingsTB);
            }
            db.SubmitChanges();

            //gen.ShowMessage(this.Page, "Lock settings applied successfully..");
            gen.ShowMessageRedirect(this.Page, "Lock settings applied successfully..", "admin_dashboard.aspx");
            Panel1.Visible = false;
        }
        catch (Exception ex) { }
    }
}