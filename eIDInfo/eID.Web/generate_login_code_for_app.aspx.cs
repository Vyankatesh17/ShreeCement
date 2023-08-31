using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class generate_login_code_for_app : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindEmployeeList();
            }
        }
        if (gvPendingLeaves.Rows.Count > 0)
            gvPendingLeaves.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    protected void lbtnApprove_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                Random random = new Random();
                int randNum = random.Next(1000000);
                string sixDigitNumber = randNum.ToString("D6");

                SystemUsersTB systemUsers = db.SystemUsersTBs.Where(d => d.Email == linkButton.CommandArgument && d.Active == true).FirstOrDefault();
                if (systemUsers != null)
                {
                    systemUsers.IsCodeRequested = true;
                    systemUsers.RequestCode = sixDigitNumber;
                    db.SubmitChanges();
                    #region Email send after company registration
                    SMTPSettingsTB smtpData = db.SMTPSettingsTBs.FirstOrDefault();
                    MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "eIDInfo Registration");
                    MailAddress toAddress = new MailAddress(systemUsers.Email);
                   // MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
                    string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);              //"support@1234";
                    string subject = "eIDInfo OTP";


                    string body = string.Format(@"<p>Dear {0} </p> <p>Your Activation Code is {1}
                        <br/><br/><strong> Regards</strong></br><p>eIDInfo Team </p>",
                            systemUsers.DisplayName, sixDigitNumber);

                    MailMessage message = new MailMessage(fromAddress, toAddress);
                    //message.Bcc.Add(bccAddress);
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
                    #endregion

                    gen.ShowMessage(this.Page, string.Format("Login code generated and send on email successfully.. please check email for code.."));
                }
            }
        }
        catch (Exception ex) { }
        
    }
    
    private void BindEmployeeList()
    {
        try
        {
            string query = string.Format(@"SELECT DISTINCT E.Email, E.FName + ' ' + E.Lname AS EmpName, E.EmployeeNo, E.ContactNo, C.CompanyName, D.DeptName, D1.DesigName
FROM            EmployeeTB AS E INNER JOIN
                         CompanyInfoTB AS C ON E.CompanyId = C.CompanyId INNER JOIN
                         MasterDeptTB AS D ON E.DeptId = D.DeptID INNER JOIN
                         MasterDesgTB AS D1 ON E.DesgId = D1.DesigID WHERE E.IsActive = 1 AND E.TenantId='{0}'", Session["TenantId"]);

            DataTable data = gen.ReturnData(query);

            gvPendingLeaves.DataSource = data;
            gvPendingLeaves.DataBind();
        }
        catch (Exception ex) { }
    }
    
}