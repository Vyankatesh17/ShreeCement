using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class company_register : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnlogin_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
            {

                //var chkExists = db.CompanyRegistrationTBs.Where(d => d.Email == txtEmail.Text).Distinct();
                var chkExists = db.CompanyRegistrationTBs.Where(d => d.Email == txtEmail.Text).Distinct();
                if (chkExists.Count() > 0)
                {
                    gen.ShowMessage(this.Page, "Company already registered with provided email address.. Please try another email id..");
                }
                else
                {
                    SMTPSettingsTB smtpData = db.SMTPSettingsTBs.FirstOrDefault();
                    string key = DateTime.Now.ToString("yyyyMMddhhmmssffffff");
                    string secureuid = SPPasswordHasher.Encrypt(key);
                    string securepass = SPPasswordHasher.Encrypt("Adm!n@123");
                    CompanyRegistrationTB company = new CompanyRegistrationTB();
                    company.Address = txtAddress.Text;
                    company.CompanyName = txtCompanyName.Text;
                    company.ContactNo = txtContactNo.Text;
                    company.ContactPerson = txtContactPerson.Text;
                    company.Email = txtEmail.Text;
                    company.IsActive = false;
                    company.IsDisabled = true;
                    company.RegDate = DateTime.Now;
                    company.SecurityKey = secureuid;
                    company.PasswordPlain = "Adm!n@123";
                    company.PasswordHash = securepass;
                    db.CompanyRegistrationTBs.InsertOnSubmit(company);
                    db.SubmitChanges();

                    #region Email send after company registration
                    MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "eTrackInfo Registration");
                    MailAddress toAddress = new MailAddress(txtEmail.Text);
                    MailAddress bccAddress = new MailAddress("sagar.kate3@gmail.com");
                    string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);              //"support@1234";
                    string subject = "eTrackInfo Registration";

                    string skey = company.SecurityKey.Replace("+", "key_plus");
                    string link = string.Format(@"<a class='btn btn-default' href=" + "http://68.178.168.27/Vania/activate_account.aspx?key={0}&pass={1}&email={2}> Activate</a>", skey,
                        securepass, company.Email);

                    string body = string.Format(@"<p>Dear {0} </p> <p>Your Activation Code is {1}, 
                        please follow the following steps to activate account on eTrackInfo Portal <br/><br/> 
                        1. Please click on activate button to activate your account. <BR/> 
                        2. Please note down the security key : {1} and login mail : {3} & password {4} which will be ask for activate account.</br>
                        3. You can change password once account is activated. <br/> 
                        {2}
                        <br/><br/><strong> Regards</strong></br><p>eTrackInfo Team </p>",
                            company.ContactPerson, key, link, company.Email, "Adm!n@123");

                    MailMessage message = new MailMessage(fromAddress, toAddress);
                    //message.CC.Add(new MailAddress("Sagar@vaniasolutions.com"));
                    message.Bcc.Add(bccAddress);
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.Subject = subject;
                    message.Body = body;                    

                    SmtpClient client = new SmtpClient();
                    client.Host = smtpData.smtpAddress;
                    client.Port = smtpData.portNo.Value;
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;                         
                    client.Credentials = new System.Net.NetworkCredential(fromAddress.Address, smtpData.SMTPPassword);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex) { }
                    #endregion                   

                    gen.ShowMessage(this.Page, string.Format("Company registered successfully.. please verify email"));

                    txtAddress.Text = txtCompanyName.Text = txtContactNo.Text = txtContactPerson.Text = txtEmail.Text = "";
                }
            }
        }
    }
}