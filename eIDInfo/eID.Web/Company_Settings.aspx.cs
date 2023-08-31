using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_Sttings : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        BindDataGridViewList();
        BindJqFunctions();
    }


    private void BindDataGridViewList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from r in db.CompanyRegistrationTBs                       
                        select new
                        {
                            r.CompanyId,
                            r.CompanyName,                           
                            r.ContactNo,
                            r.Email,
                            Status = r.IsActive == true ? "Active" : "In Active"                          
                        }).Distinct().OrderByDescending(d => d.CompanyId);
            gvDataList.DataSource = data;
            gvDataList.DataBind();
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        bool statusdata = ddlstatus.SelectedValue == "Active" ? true : false;

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from r in db.CompanyRegistrationTBs 
                        where r.IsActive == statusdata
                        select new
                        {
                            r.CompanyId,
                            r.CompanyName,
                            r.ContactNo,
                            r.Email,
                            Status = r.IsActive == true ? "Active" : "In Active"
                        }).Distinct().OrderByDescending(d => d.CompanyId);
            gvDataList.DataSource = data;
            gvDataList.DataBind();
        }
    }


    protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void lbtnActive_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton company = (LinkButton)sender;
            int CompanyID = Convert.ToInt32(company.CommandArgument);
            var data = HR.CompanyRegistrationTBs.Where(a => a.CompanyId == CompanyID).FirstOrDefault();

            data.IsActive = true;
            data.IsDisabled = false;
            HR.SubmitChanges();
            BindDataGridViewList();

            #region Email send after company Active
            SMTPSettingsTB smtpData = HR.SMTPSettingsTBs.FirstOrDefault();

            MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "Activation Email");
            MailAddress toAddress = new MailAddress(data.Email);
            //MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
            string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);              //"support@1234";
            string subject = "Activation of services";

            string body = string.Format(@"Hello, </br> <p>Greeting for the day!</p> </br> <p> Thank you for making the payment!</p> </br> <p> We've reinstated the services for your account with us.</p> 
                        </br> <p> We highly appreciate your business with us.</p> </br> <p> We look Forward to serve you better.</p> </br></br> <p> Thanks and regards,<br> Admin </br></p>");

            SmtpClient client = new SmtpClient();
            client.Host = smtpData.smtpAddress;
            client.Port = smtpData.portNo.Value;
            client.EnableSsl = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);

            MailMessage message = new MailMessage(fromAddress, toAddress);
            //message.Bcc.Add(bccAddress);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;
            try
            {
                client.Send(message);
            }
            catch (Exception ex) { }
            #endregion
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lbtnDeActive_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton company = (LinkButton)sender;
            int CompanyID = Convert.ToInt32(company.CommandArgument);
            var data = HR.CompanyRegistrationTBs.Where(a => a.CompanyId == CompanyID).FirstOrDefault();

            data.IsActive = false;
            data.IsDisabled = true;
            HR.SubmitChanges();
            BindDataGridViewList();

            #region Email send after company InActive
            SMTPSettingsTB smtpData = HR.SMTPSettingsTBs.FirstOrDefault();

            MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "Deactivation Email");
            MailAddress toAddress = new MailAddress(data.Email);
            //MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
            string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);              //"support@1234";
            string subject = "Deactivation of Services";

            string body = string.Format(@"Hello, </br> <p>Greeting for the day!</p> </br> <p> This is payment remainder for the services we have been providing to your organization.</p> </br> <p>We've sorry to inform you that the services for the - eidinfo hrms will be deactivated due to bill due on your account with us.</p> 
                        </br> <p> We would highly appreciate if you could make the payment and we'll be happy to reinstate the services.</p> </br> <p> We look Forward for your response.</p> </br> <p> Please get in touch with as early as possible.</p>
                         </br></br> <p> Thanks and regards, <br> Admin</br></p>");

            SmtpClient client = new SmtpClient();
            client.Host = smtpData.smtpAddress;
            client.Port = smtpData.portNo.Value;
            client.EnableSsl = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);

            MailMessage message = new MailMessage(fromAddress, toAddress);
            //message.Bcc.Add(bccAddress);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;
            try
            {
                client.Send(message);
            }
            catch (Exception ex) { }
            #endregion




        }
        catch (Exception ex)
        {
            throw;
        }
    }



    protected void btnActive_Click(object sender, EventArgs e)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            foreach (GridViewRow gvr in gvDataList.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect1")).Checked == true)
                {
                    Label lblEmpNo = (Label)gvr.FindControl("lblCompanyId");
                    Label lblEmail = (Label)gvr.FindControl("lblEmail");
                    string comid = lblEmpNo.Text;
                    string email = lblEmail.Text;

                    var data = HR.CompanyRegistrationTBs.Where(a => a.CompanyId == Convert.ToInt32(comid)).FirstOrDefault();

                    data.IsActive = true;
                    data.IsDisabled = false;
                    HR.SubmitChanges();
                    BindDataGridViewList();

                    #region Email send after company Active
                    SMTPSettingsTB smtpData = db.SMTPSettingsTBs.FirstOrDefault();

                    MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "eIDInfo Registration");
                    MailAddress toAddress = new MailAddress(email);
                    //MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
                    string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);              //"support@1234";
                    string subject = "Company Activation";

                    string body = string.Format(@"Your Company is Activated");

                    SmtpClient client = new SmtpClient();
                    client.Host = smtpData.smtpAddress;
                    client.Port = smtpData.portNo.Value;
                    client.EnableSsl = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);

                    MailMessage message = new MailMessage(fromAddress, toAddress);
                    //message.Bcc.Add(bccAddress);
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.Subject = subject;
                    message.Body = body;
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex) { }
                    #endregion



                }
            }
        }
    }


    protected void btnInActive_Click(object sender, EventArgs e)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            foreach (GridViewRow gvr in gvDataList.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect1")).Checked == true)
                {
                    Label lblEmpNo = (Label)gvr.FindControl("lblCompanyId");
                    Label lblEmail = (Label)gvr.FindControl("lblEmail");
                    string comid = lblEmpNo.Text;
                    string email = lblEmail.Text;                  

                    var data = HR.CompanyRegistrationTBs.Where(a => a.CompanyId == Convert.ToInt32(comid)).FirstOrDefault();

                    data.IsActive = false;
                    data.IsDisabled = true;
                    HR.SubmitChanges();
                    BindDataGridViewList();

                    #region Email send after company InActive
                    SMTPSettingsTB smtpData = db.SMTPSettingsTBs.FirstOrDefault();

                    MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "eIDInfo Registration");
                    MailAddress toAddress = new MailAddress(email);
                    //MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
                    string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);              //"support@1234";
                    string subject = "Company Activation";

                    string body = string.Format(@"Your Company is De-Activated");

                    SmtpClient client = new SmtpClient();
                    client.Host = smtpData.smtpAddress;
                    client.Port = smtpData.portNo.Value;
                    client.EnableSsl = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);

                    MailMessage message = new MailMessage(fromAddress, toAddress);
                    //message.Bcc.Add(bccAddress);
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.Subject = subject;
                    message.Body = body;
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex) { }
                    #endregion

                }
            }
        }
    }

}