using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class out_door_approve_hr : System.Web.UI.Page
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
                var data  =(from d in  db.OutDoorEntryTBs
                            join emp in db.EmployeeTBs on d.EmployeeId equals emp.EmployeeId
                            where d.OddId == Convert.ToInt32(linkButton.CommandArgument)
                            select new
                            {
                                d.OddId,
                                d.ManagerStatus,
                                d.FromDate,
                                d.ToDate,
                                d.TravelPlace,
                                d.TravelReason,
                                d.Description,
                                EmpName =emp.FName+ " "+emp.Lname,
                                d.ManagerRemark
                            }).FirstOrDefault();

                lblEmpName.Text = data.EmpName;
                lblFromdate.Text = data.FromDate.Value.Date.ToString("MM/dd/yyyy");
                lblLeaveType.Text = data.TravelPlace;
                lblReason.Text = data.TravelReason;
                lblManagerComment.Text = data.ManagerRemark;
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
                var data = (from d in db.OutDoorEntryTBs
                            join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                            where e.IsActive == true && d.ManagerStatus == "Approve" && d.HrStatus== "Pending" && e.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                            select new
                            {
                                d.OddId,
                                d.ManagerStatus,
                                d.FromDate,
                                d.ToDate,
                                d.TravelPlace,
                                d.TravelReason,
                                d.Description,
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
                OutDoorEntryTB applicationsTB = db.OutDoorEntryTBs.Where(d => d.OddId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.HrStatus = "Approve";
                applicationsTB.HrRemark = txtReason.Text;
                db.SubmitChanges();

                //#region Send mail to department head
                //SendMail(applicationsTB.EmployeeId.Value, applicationsTB.FromDate.Value.ToShortDateString(), applicationsTB.ToDate.Value.ToShortDateString(), applicationsTB.FromTime.Value.ToString(), applicationsTB.ToTime.Value.ToString(), applicationsTB.TravelReason, applicationsTB.TravelPlace,"Approve");
                //#endregion
                // Send mail to employee code here
                MultiView1.ActiveViewIndex = 0;
                BindPendingLeavesList();
                gen.ShowMessage(this.Page, "OD Approved Successfully..");
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
                OutDoorEntryTB applicationsTB = db.OutDoorEntryTBs.Where(d => d.OddId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.HrStatus = "Reject";
                applicationsTB.HrRemark = txtReason.Text;
                db.SubmitChanges();
                //#region Send mail to department head
                //SendMail(applicationsTB.EmployeeId.Value, applicationsTB.FromDate.Value.ToShortDateString(), applicationsTB.ToDate.Value.ToShortDateString(), applicationsTB.FromTime.Value.ToString(), applicationsTB.ToTime.Value.ToString(), applicationsTB.TravelReason, applicationsTB.TravelPlace, "Rejected");
                //#endregion
                gen.ShowMessage(this.Page, "OD Rejected..");

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

        ddlCompany.SelectedIndex = 1;
    }

    public void SendMail(int empId, string fromDate, string toDate, string startTime, string endTime, string reason, string place,string status)
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
                    MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "eIDInfo OD Management");
                    MailAddress cc1 = new MailAddress(adminMail.Email);
                    MailAddress toAddress = new MailAddress(empData.Email);


                    string body = string.Format(@"<p>Dear, </p> <p>Your outdoor visit request has beed {0}, 
                        OD details are as bellow <br/><br/> 
                        From Date : {1} <BR/> 
                        To Date : {2} <BR/>
                        FromTime : {3}</br>
                        End Time : {4} <br/> 
                        Reason : {5} <br/> 
                        Place : {6}
                        <br/><br/><strong> Regards</strong></br><p>eIDInfo Team </p>",
                            status, fromDate, toDate, startTime, endTime, reason, place);
                    MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
                    string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);              //"support@1234";
                    string subject = "OD Application - " + empData.FName + " " + empData.Lname;

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