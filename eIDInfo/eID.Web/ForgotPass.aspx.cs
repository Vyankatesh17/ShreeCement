using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;

public partial class ForgotPass : System.Web.UI.Page
{
    HrPortalDtaClassDataContext EX = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtemail.Text))
        {
            var LoginData = from d in EX.EmployeeTBs where d.EmailId == txtemail.Text select d;
            if (LoginData.Count() > 0)
            {
              
                string str1 = Convert.ToString(LoginData.Select(d => d.EmailId).First().ToString()).Trim();

                RegistrationTB CTB = EX.RegistrationTBs.Where(d => d.EmployeeId == LoginData.First().EmployeeId).First();
                string password = Convert.ToString(CTB.Password);
               string UserName = Convert.ToString(CTB.UserName);


                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.To.Add(str1);
                    mail.From = new MailAddress("excellence.usatest@gmail.com","LogIn Details");
                    //mail.From = new MailAddress(SMTP_Settings.Email_smtp);
                    mail.Subject = "Login Details";



                    string body = "   Hi , Your Login Details are as  follows<br/><br/>  <br/>Your User Name is  : " + UserName + "<br/> Your password is :" + password + " <br/><br/>Thank you,";
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
                    g.ShowMessage(this.Page, "Your password sent to your email..");

                    txtemail.Text = null;
                  
            }
            else
            {
                g.ShowMessage(this.Page, "The username that was entered was not found.");
            }

        }
        else
        {
            g.ShowMessage(this.Page, "Enter  UserName");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("login.aspx");


    
    }
}