using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class leave_approve_hr : System.Web.UI.Page
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
                fillcompany();
                BindPendingLeavesList();
            }
        }
        if (gvPendingLeaves.Rows.Count > 0)
            gvPendingLeaves.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    protected void lbtnApprove_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        hfKey.Value = linkButton.CommandArgument;
        MultiView1.ActiveViewIndex = 1;

        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data  =(from d in  db.LeaveApplicationsTBs
                            join emp in db.EmployeeTBs on d.EmployeeId equals emp.EmployeeId
                            join l in db.masterLeavesTBs on d.LeaveTypeId equals l.LeaveID into dleave
                            from dl in dleave.DefaultIfEmpty()
                            where d.LeaveApplicationId == Convert.ToInt32(linkButton.CommandArgument)
                            select new
                            {
                                d.Duration,
                                d.LeaveEndDate,
                                d.LeaveReason,
                                d.LeaveStartDate,
                                LeaveType = d.LeaveTypeId == 0 ? "Loss of Pay" : dl.LeaveName,
                                EmpName=emp.FName+ " "+emp.Lname,
                                d.ManRemark
                            }).FirstOrDefault();

                lblEmpName.Text = data.EmpName;
                lblFromdate.Text = data.LeaveStartDate.Value.Date.ToString("MM/dd/yyyy");
                lblLeaveType.Text = data.LeaveType;
                lblReason.Text = data.LeaveReason;
                lblToDate.Text = data.LeaveEndDate.Value.ToString("MM/dd/yyyy");
                lblTotaDays.Text = data.Duration.Value.ToString();
                lblManagerComment.Text = data.ManRemark;
            }
        }
        catch (Exception ex) { }

        //try
        //{
        //    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        //    {
        //        LeaveApplicationsTB applicationsTB = db.LeaveApplicationsTBs.Where(d => d.LeaveApplicationId == Convert.ToInt32(linkButton.CommandArgument)).FirstOrDefault();
        //        applicationsTB.HrStatus = "Approve";
        //        db.SubmitChanges();
        //        if (applicationsTB.LeaveTypeId > 0)
        //        {
        //            DateTime dateTime = applicationsTB.ApplicationDate.Value;
        //            LeaveAllocationTB allocationTB = db.LeaveAllocationTBs.Where(d => d.EmployeeID == applicationsTB.EmployeeId &&
        //            dateTime.Date >= d.FromDateAllocation.Value.Date && dateTime.Date <= d.ToDateAllocation.Value
        //            && d.LeaveID == applicationsTB.LeaveTypeId).FirstOrDefault();
        //            double duration = applicationsTB.Duration.Value;
        //            double pendingLeaves = allocationTB.PendingLeaves.Value;

        //            allocationTB.PendingLeaves = (pendingLeaves - duration);
        //            db.SubmitChanges();
        //        }

        //        gen.ShowMessage(this.Page, "Leave Approved Successfully..");

        //        // Send mail to employee code here

        //        BindPendingLeavesList();
        //    }
        //}
        //catch(Exception ex) { }
    }

    protected void lbtnReject_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                LeaveApplicationsTB applicationsTB = db.LeaveApplicationsTBs.Where(d => d.LeaveApplicationId == Convert.ToInt32(linkButton.CommandArgument)).FirstOrDefault();
                applicationsTB.HrStatus = "Reject";
                db.SubmitChanges();
                
                gen.ShowMessage(this.Page, "Leave Rejected..");

                // Send mail to employee code here

                BindPendingLeavesList();
            }
        }
        catch (Exception ex) { }
    }

    private void BindPendingLeavesList()
    {
        try {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.LeaveApplicationsTBs
                            join l in db.masterLeavesTBs on d.LeaveTypeId equals l.LeaveID into dleave
                            from dl in dleave.DefaultIfEmpty()
                            join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                            where e.IsActive == true && d.ManagerStatus == "Approve" && d.HrStatus=="Pending" && e.CompanyId==Convert.ToInt32(ddlCompany.SelectedValue)&& d.TenantId == Convert.ToString(Session["TenantId"])
                            select new
                            {
                                d.LeaveApplicationId,
                                d.ManagerStatus,
                                d.ApplicationDate,
                                d.Duration,
                                d.IsLossofPay,
                                d.LeaveStartDate,
                                d.LeaveEndDate,
                                d.LeaveReason,
                                LeaveType = d.LeaveTypeId == 0 ? "Loss of Pay" : dl.LeaveName,
                                EmpName = e.FName + " " + e.Lname,
                                e.EmployeeNo,
                                e.MachineID
                            }).Distinct();

                gvPendingLeaves.DataSource = data;
                gvPendingLeaves.DataBind();
            }
        }
        catch(Exception ex) { }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                LeaveApplicationsTB applicationsTB = db.LeaveApplicationsTBs.Where(d => d.LeaveApplicationId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.HrStatus = "Approve";
                applicationsTB.HrRemark = txtReason.Text;
                applicationsTB.ApproveDate = gen.GetCurrentDateTime();
                db.SubmitChanges();

                LeaveAppTB leaveApp = db.LeaveAppTBs.Where(d => d.LeaveAppID == applicationsTB.LeaveApplicationId).FirstOrDefault();
                if (leaveApp != null)
                {
                    leaveApp.HRStatus = "Approve";
                    leaveApp.ManagerStatus = "Approve";
                    db.SubmitChanges();
                }

               
                if (applicationsTB.LeaveTypeId > 0)
                {
                    DateTime dateTime = applicationsTB.ApplicationDate.Value;
                    LeaveAllocationTB allocationTB = db.LeaveAllocationTBs.Where(d => d.EmployeeID == applicationsTB.EmployeeId &&
                    dateTime.Date >= d.FromDateAllocation.Value.Date && dateTime.Date <= d.ToDateAllocation.Value
                    && d.LeaveID == applicationsTB.LeaveTypeId).FirstOrDefault();
                    double duration = applicationsTB.Duration.Value;
                    double pendingLeaves = allocationTB.PendingLeaves.Value;

                    allocationTB.PendingLeaves = (pendingLeaves - duration);
                    db.SubmitChanges();
                }

                SendMail(applicationsTB.EmployeeId.Value, applicationsTB.ApplicationDate.Value.ToShortDateString(), applicationsTB.LeaveStartDate.Value.ToShortDateString(), applicationsTB.LeaveEndDate.Value.ToShortDateString(), txtReason.Text, "Approve");
                // Send mail to employee code here
                MultiView1.ActiveViewIndex = 0;
                BindPendingLeavesList();
                gen.ShowMessage(this.Page, "Leave Approved Successfully..");
            }
        }
        catch (Exception ex) { }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                LeaveApplicationsTB applicationsTB = db.LeaveApplicationsTBs.Where(d => d.LeaveApplicationId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.HrStatus = "Reject";
                applicationsTB.HrRemark = txtReason.Text;
                applicationsTB.ApproveDate = gen.GetCurrentDateTime();
                db.SubmitChanges();

                SendMail(applicationsTB.EmployeeId.Value, applicationsTB.ApplicationDate.Value.ToShortDateString(), applicationsTB.LeaveStartDate.Value.ToShortDateString(), applicationsTB.LeaveEndDate.Value.ToShortDateString(), txtReason.Text,"Reject");

                gen.ShowMessage(this.Page, "Leave Rejected..");

                // Send mail to employee code here
                MultiView1.ActiveViewIndex = 0;
                BindPendingLeavesList();
            }
        }
        catch (Exception ex) { }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPendingLeavesList();
    }
    private void fillcompany()
    {
        ddlCompany.Items.Clear();
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
        }
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void SendMail(int empId, string travelDate, string startTime, string endTime, string reason, string status)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var empData = db.EmployeeTBs.Where(d => d.EmployeeId == empId).FirstOrDefault();
                var adminMail = db.CompanyRegistrationTBs.Where(d => d.SecurityKey == empData.TenantId).FirstOrDefault();
                var reportingHead = db.EmployeeTBs.Where(d => d.EmployeeId == empData.ManagerID).FirstOrDefault();
                SMTPSettingsTB smtpData = db.SMTPSettingsTBs.FirstOrDefault();
                if (smtpData != null)
                {
                    string deptHeadMail = "";
                    MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "eIDInfo Leave Management");
                    MailAddress cc1 = new MailAddress(adminMail.Email);
                    MailAddress toAddress = new MailAddress(empData.Email);


                    string body = string.Format(@"<p>Dear, </p> <p>Your leave has been {0}, 
                        Leave details are as bellow <br/><br/> 
                        Application Date : {1} <BR/> 
                        Leave Start : {2}</br>
                        Leave End : {3} <br/> 
                        Reason : {4} 
                        <br/><br/><strong> Regards</strong></br><p>eIDInfo Team </p>",
                            status, travelDate, startTime, endTime, reason);
                    MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
                    string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);              //"support@1234";
                    string subject = "Leave Application - " + empData.FName + " " + empData.Lname;

                    SmtpClient client = new SmtpClient();
                    client.Host = smtpData.smtpAddress;
                    client.Port = smtpData.portNo.Value;
                    client.EnableSsl = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);

                    MailMessage message = new MailMessage(fromAddress, toAddress);
                    message.Bcc.Add(bccAddress);
                    message.CC.Add(cc1);

                    if (reportingHead != null)
                    {
                        MailAddress cc2 = new MailAddress(reportingHead.Email);
                        message.CC.Add(cc2);
                    }
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.Subject = subject;
                    message.Body = body;
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex) { }

                }
                else
                {
                    gen.ShowMessage(this, "Mail send unsuccessfull, due to config settings, Please contact administrator.");
                }
            }
        }
        catch (Exception ex) { }
    }
}