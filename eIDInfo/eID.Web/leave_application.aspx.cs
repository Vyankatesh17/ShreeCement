using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class leave_application : System.Web.UI.Page
{


    #region Variable Declaration
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    int totalSunday = 0, TotalHolidays = 0, TotalSaturDays = 0;
    #endregion
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {

                //txtStartDate.Attributes.Add("Readonly", "Readonly");
                //txtEndDate.Attributes.Add("Readonly", "Readonly");
                fillcompany();
                fillpendingLeaves();
                FillLeaveType();
                BindLeaveApplicationsList();
                if (grd_Leavependings.Rows.Count == 0)
                {
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (grd_Leavependings.Rows.Count > 0)
            grd_Leavependings.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    public void FillLeaveType()
    {

        var LeaveTypeData = (from d in HR.masterLeavesTBs
                             join k in HR.LeaveAllocationTBs on d.LeaveID equals k.LeaveID
                             where d.Status == 1 && d.CompanyId==Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId==Convert.ToString(Session["TenantId"])
                             select new
                             {
                                 d.LeaveID,
                                 d.LeaveName
                             }).Distinct();
        if (LeaveTypeData.Count() > 0)
        {
            ddlLeaveType.DataSource = LeaveTypeData;
            ddlLeaveType.DataTextField = "LeaveName";
            ddlLeaveType.DataValueField = "LeaveID";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("Loss of Pay", "0"));
        }
        else
        {
            ddlLeaveType.Items.Clear();
            ddlLeaveType.Items.Insert(0, new ListItem("Loss of Pay", "0"));
        }
    }

    private void fillpendingLeaves()
    { // For Binding pending Leaves to Grid
        int empid = Session["UserType"].ToString().Equals("User") ? Convert.ToInt32(Session["EmpId"]) : lstFruits.SelectedIndex > 0 ? Convert.ToInt32(lstFruits.SelectedValue) : 0;
        var pendingLeaves = (from m in HR.LeaveAllocationTBs
                             join n in HR.masterLeavesTBs on m.LeaveID equals n.LeaveID
                             where m.EmployeeID == empid
                             && ((DateTime)m.FromDateAllocation).Year == DateTime.Now.Year
                              && (((DateTime)m.ToDateAllocation).Year == DateTime.Now.Year|| ((DateTime)m.ToDateAllocation).Year == DateTime.Now.Year+1)
                              && m.PendingLeaves != 0
                             select new
                             {
                                 LeaveName = n.LeaveName,
                                 m.LeaveID,
                                 LeaveBalance = m.PendingLeaves,
                                 EligibleLeaves = m.TotalAllocatedLeaves,
                                 TakenLeaves = m.TotalAllocatedLeaves - m.PendingLeaves
                             }).Distinct();
        if (pendingLeaves.Count() > 0)
        {
            grd_Leavependings.DataSource = pendingLeaves;
            grd_Leavependings.DataBind();
        }
        else
        {
            grd_Leavependings.DataSource = null;
            grd_Leavependings.DataBind();
            btnSubmit.Enabled = false;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //CHeck Exists...

        int empid = Convert.ToInt32(lstFruits.SelectedValue);
        if (Session["UserType"].ToString().Equals("User"))
        {
            empid = Convert.ToInt32(Session["EmpId"].ToString());
        }
            DateTime sDate = Genreal.GetDate(txtStartDate.Text);
        DateTime eDate = Genreal.GetDate(txtEndDate.Text);

        var ExistData = from d in HR.LeaveApplicationsTBs
                        where d.LeaveStartDate == sDate.Date
                            && d.LeaveEndDate == eDate.Date
                            && d.EmployeeId == empid
                        select d;
        if (ExistData.Count() == 0)
        {
            LeaveApplicationsTB leaveApplicationsTB = new LeaveApplicationsTB();
            leaveApplicationsTB.AddedBy = empid;
            leaveApplicationsTB.AddedDate = g.GetCurrentDateTime();
            leaveApplicationsTB.ApplicationDate = g.GetCurrentDateTime();
            leaveApplicationsTB.Duration = Convert.ToDouble(txtDuration.Text);
            leaveApplicationsTB.EmployeeId = empid;
            leaveApplicationsTB.EndHalf = rbToFirst.Checked ? "First" : "Second";           
            leaveApplicationsTB.LeaveStartDate = new DateTime(sDate.Year, sDate.Month, sDate.Day);
            leaveApplicationsTB.LeaveReason = txtPurpose.Text;
            leaveApplicationsTB.LeaveEndDate = new DateTime(eDate.Year, eDate.Month, eDate.Day);
            leaveApplicationsTB.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);            
            leaveApplicationsTB.StartHalf = rbFromFirst.Checked ? "First" : "Second";
            leaveApplicationsTB.IsLossofPay = ddlLeaveType.SelectedIndex == 0 ? 1 : 0;//
            leaveApplicationsTB.TenantId = Convert.ToString(Session["TenantId"]);
            leaveApplicationsTB.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

            if(Session["UserType"].ToString() == "Admin" || Session["UserType"].ToString() == "LocationAdmin")
            {
                leaveApplicationsTB.HrStatus = "Approve";
                leaveApplicationsTB.ManagerStatus = "Approve";
            }
            else
            {
                leaveApplicationsTB.HrStatus = "Pending";
                leaveApplicationsTB.ManagerStatus = "Pending";
            }


            HR.LeaveApplicationsTBs.InsertOnSubmit(leaveApplicationsTB);
            HR.SubmitChanges();

            LeaveAppTB app = new LeaveAppTB();
            app.LeaveAppID = leaveApplicationsTB.LeaveApplicationId;
            if (Session["UserType"].ToString() == "Admin" || Session["UserType"].ToString() == "LocationAdmin")
            {
                app.HRStatus = "Approve";
                app.ManagerStatus = "Approve";
            }
            else
            {
                app.HRStatus = "Pending";
                app.ManagerStatus = "Pending";
            }
            app.ApproveDays = 0;
            app.TenantId = Convert.ToString(Session["TenantId"]);
            HR.LeaveAppTBs.InsertOnSubmit(app);
            HR.SubmitChanges();
            
            for (var day = sDate.Date; day.Date <= eDate.Date; day = day.AddDays(1))
            {
                LeaveApplicationDetailsTB detailsTB = new LeaveApplicationDetailsTB();
                detailsTB.EmpId = leaveApplicationsTB.EmployeeId;
                detailsTB.EntryBy =empid;
                detailsTB.EntryDate = DateTime.Now;
                detailsTB.LeaveApplicationId = leaveApplicationsTB.LeaveApplicationId;
                detailsTB.LeaveDate = day;
                detailsTB.Status = true;
                if (day == sDate.Date)
                {
                    string workStat = "";
                    if (rbFromFirst.Checked && rbToFirst.Checked) { workStat = "First"; }
                    else if (rbFromFirst.Checked && rbToSecond.Checked) { workStat = "Both"; }
                    else if (rbFromSecond.Checked && rbToSecond.Checked) { workStat = "Second"; }
                    detailsTB.DayWorkStatus = workStat;
                }
                else if (day == eDate.Date)
                {
                    string workStat = "";
                    if (rbFromFirst.Checked && rbToFirst.Checked) { workStat = "First"; }
                    else if (rbFromFirst.Checked && rbToSecond.Checked) { workStat = "Both"; }
                    else if (rbFromSecond.Checked && rbToSecond.Checked) { workStat = "Second"; }
                    detailsTB.DayWorkStatus = workStat;
                }
                HR.LeaveApplicationDetailsTBs.InsertOnSubmit(detailsTB);
                HR.SubmitChanges();
            }

            //var leaveapplicationdata = (from d in HR.LeaveAllocationTBs where d.LeaveID == Convert.ToInt32(ddlLeaveType.SelectedValue) && d.EmployeeID == empid select d).FirstOrDefault();
            //if (leaveapplicationdata != null)
            //{
            //    var pendleave = leaveapplicationdata.PendingLeaves - Convert.ToDouble(txtDuration.Text);
            //    leaveapplicationdata.PendingLeaves = pendleave;
            //    HR.SubmitChanges();
            //}

            #region Send mail to department head
            SendMail(leaveApplicationsTB.EmployeeId.Value,leaveApplicationsTB.LeaveStartDate.Value.ToShortDateString(),leaveApplicationsTB.LeaveEndDate.Value.ToShortDateString(), leaveApplicationsTB.Duration.Value.ToString(),leaveApplicationsTB.LeaveReason);
            #endregion

            #region Send SMS
            //SendSMS(leaveApplicationsTB.EmployeeId.Value);
            #endregion

            clearFields();
            g.ShowMessage(this.Page, "Leave applied successfully..");

        }
        else
        {
            g.ShowMessage(this.Page, "Leave Application Details Already Sent for This Period...");
        }
    }

    private void clearFields()
    {
        ClearInputs(Page.Controls);

    }

    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();

            ClearInputs(ctrl.Controls);
        }
    }
    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        CalculateDays();

        double applyDays =string.IsNullOrEmpty(txtDuration.Text)?0: Convert.ToDouble(txtDuration.Text);
        double lDays = GetPendingLeaves();
        if (applyDays > lDays && ddlLeaveType.SelectedIndex > 0)
        {
            g.ShowMessage(this.Page, "Leave Balance Not Enough for Selected Leave Name..");
            btnSubmit.Enabled = false;
        }
        else if (ddlLeaveType.SelectedIndex == 0)
        {
            g.ShowMessage(this.Page, "Leave will be apply under Loss of Pay criteria");
            btnSubmit.Enabled = true;
        }
        else
        {
            btnSubmit.Enabled = true;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();
    }

    private string DayName(string DayName)
    {
        DataSet ds = g.ReturnData1(@"Declare @startdate datetime, @enddate datetime set @startdate = '" + txtStartDate.Text + "' set @EndDate = '" + txtEndDate.Text + "' ;with mycte as (select  @startdate DateValue union all select DateValue + 1 from mycte where DateValue + 1 <= @enddate) SELECT COUNT(*) as NumOfSunday FROM mycte WHERE DATENAME(weekday,dateValue)='" + DayName + "'");
        //if (ds.Tables[0].Rows.Count>0)
        //{
        return ds.Tables[0].Rows[0][0].ToString();

    }
    private void CalculateDays()
    {
        DateTime enddateTime = Genreal.GetDate(txtEndDate.Text);
        DateTime startdateTime = Genreal.GetDate(txtStartDate.Text);

        try
        {
            if (startdateTime > enddateTime)
            {
                g.ShowMessage(this.Page, "start date should be less than end date.");
            }
            else
            {
                btnSubmit.Enabled = true;

                double days = (enddateTime - startdateTime).TotalDays;

                if (rbFromFirst.Checked && rbToFirst.Checked)
                {
                    days = days + (0.5);
                }
                else if (rbFromFirst.Checked && rbToSecond.Checked)
                {
                    days = days + 1;
                }
                else if (rbFromSecond.Checked && rbToFirst.Checked) { days = days + (0.5); }
                else if (rbFromSecond.Checked && rbToSecond.Checked)
                {
                    days = days + (.50);
                }
                txtDuration.Text = days.ToString();
            }

        }
        catch(Exception ex)
        {
            g.ShowMessage(this.Page, ex.Message);
        }
    }

    protected void rbToFirst_CheckedChanged(object sender, EventArgs e)
    {
        CalculateDays();
    }

    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        double pLeaves = GetPendingLeaves();
        if (pLeaves > 0)
        {
            btnSubmit.Enabled = true;
        }
        else if (ddlLeaveType.SelectedIndex == 0)
        {
            btnSubmit.Enabled = true;
        }
        else { btnSubmit.Enabled = false; }
    }
    private double GetPendingLeaves()
    {
        int empid = lstFruits.SelectedIndex > 0 ? Convert.ToInt32(lstFruits.SelectedValue): Convert.ToInt32(Session["EmpId"]);
        double pendingLeaves = 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.LeaveAllocationTBs where d.LeaveID == Convert.ToInt32(ddlLeaveType.SelectedValue) && d.EmployeeID == empid select d).FirstOrDefault();
            pendingLeaves = data == null ? 0 : data.PendingLeaves.Value;
        }
        return pendingLeaves;
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
        BindDepartmentList();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        //BindEmployeeList();
        FillLeaveType();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
        FillLeaveType();
    }

    private void BindDepartmentList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlDepartment.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterDeptTBs
                        where d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeptName, d.DeptID }).Distinct();
            if (data != null && data.Count() > 0)
            {
                ddlDepartment.DataSource = data;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }

            ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));

           
        }
    }


    private void BindEmployeeList()
    {
        lstFruits.Items.Clear();
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true && (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { EmpName = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.ManagerID }).Distinct();
            if (Session["UserType"].ToString().Equals("User"))
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            if (data != null && data.Count() > 0)
            {
                if (Convert.ToInt32(Session["IsDeptHead"]) == 1)
                {
                    int manId = Convert.ToInt32(Session["EmpId"]);
                    data = data.Where(d => d.ManagerID == manId || d.EmployeeId == manId).Distinct();
                }
                lstFruits.DataSource = data;
                lstFruits.DataTextField = "EmpName";
                lstFruits.DataValueField = "EmployeeId";
                lstFruits.DataBind();
            }
            lstFruits.Items.Insert(0, new ListItem("--Select--", "0"));
            if (Session["UserType"].ToString().Equals("User"))
            {
                lstFruits.SelectedValue = Session["EmpId"].ToString();
            }
        }
    }



    //private void BindEmployeeList()
    //{
    //    ddlEmployee.Items.Clear();
    //    int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
    //    int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
    //    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
    //    {
    //        var data = (from dt in db.EmployeeTBs
    //                    where dt.IsActive == true && (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.CompanyId == cId && dt.DeptId == dId
    //                    select new { EmpName = dt.FName + ' ' + dt.Lname, dt.EmployeeId,dt.ManagerID }).Distinct();
    //        if (Session["UserType"].ToString().Equals("User"))
    //        {
    //            data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
    //        }
    //        if (data != null && data.Count() > 0)
    //        {
    //            if (Convert.ToInt32(Session["IsDeptHead"]) == 1)
    //            {
    //                int manId = Convert.ToInt32(Session["EmpId"]);
    //                data = data.Where(d => d.ManagerID == manId || d.EmployeeId == manId).Distinct();
    //            }
    //            ddlEmployee.DataSource = data;
    //            ddlEmployee.DataTextField = "EmpName";
    //            ddlEmployee.DataValueField = "EmployeeId";
    //            ddlEmployee.DataBind();
    //        }
    //        ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
    //        if (Session["UserType"].ToString().Equals("User"))
    //        {
    //            ddlEmployee.SelectedValue = Session["EmpId"].ToString();
    //        }
    //    }
    //}

    protected void lbtnEvent_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        hfKey.Value = linkButton.CommandArgument;

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            LeaveApplicationsTB outDoorEntryTB = db.LeaveApplicationsTBs.Where(d => d.LeaveApplicationId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
            db.LeaveApplicationsTBs.DeleteOnSubmit(outDoorEntryTB);

            db.LeaveApplicationDetailsTBs.Where(d => d.LeaveApplicationId == Convert.ToInt32(hfKey.Value)).ToList().ForEach(d=>d.Status=false);
            db.SubmitChanges();
            g.ShowMessage(this.Page, "Leave Entry deleted..");
            BindLeaveApplicationsList();
        }
    }
    protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvDataList.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    private void BindLeaveApplicationsList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.LeaveApplicationsTBs
                        join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                        join l in db.masterLeavesTBs on d.LeaveTypeId equals l.LeaveID into leave
                        from ml in leave.DefaultIfEmpty()
                        where e.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new
                        {
                            d.ApplicationDate,
                            d.Duration,
                            d.IsLossofPay,
                            d.LeaveReason,
                            d.LeaveTypeId,
                            ml.LeaveName,
                            LeaveType=d.IsLossofPay==1?"Loss of Pay":    ml.LeaveTypeSName,
                            d.HrRemark,
                            d.HrStatus,
                            d.ManRemark,
                            d.ManagerStatus,
                            d.LeaveApplicationId,
                            d.LeaveStartDate,
                            d.LeaveEndDate,
                            d.StartHalf,
                            d.EndHalf,
                            d.EmployeeId,
                            e.EmployeeNo,
                            EmpName = e.FName + " " + e.Lname
                        });
            if (Session["UserType"].ToString().Equals("User"))
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"].ToString())).Distinct();
            }
            gvDataList.DataSource = data;
            gvDataList.DataBind();
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpendingLeaves();
    }

    #region Send Mail
    public void SendMail(int empId,string startDate,string endDate,string duration, string reason)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                string url = "http://eidinfo.in/hrms/leave_approve_hr.aspx";
                var empData = db.EmployeeTBs.Where(d => d.EmployeeId == empId).FirstOrDefault();
                var adminMail = db.CompanyRegistrationTBs.Where(d => d.SecurityKey == empData.TenantId).FirstOrDefault();
                var reportingHead = db.EmployeeTBs.Where(d => d.EmployeeId == empData.ManagerID).FirstOrDefault();
                SMTPSettingsTB smtpData = db.SMTPSettingsTBs.FirstOrDefault();
                if (smtpData != null)
                {
                    string deptHeadMail = "";
                    MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "eIDInfo Leave Management");
                    MailAddress toAddress = new MailAddress(adminMail.Email);
                    //MailAddress cc1 = new MailAddress(empData.Email);


                    string body = string.Format(@"<p>Dear, </p> <p>Your have a new leave application request from {0}, 
                        Leave details are as bellow <br/><br/> 
                        Start Date : {1} <BR/> 
                        End Date : {2}</br>
                        Duration : {3} <br/> 
                        Reason : {4} <br/> 
                        <a href='{5}' title='Approve or Reject Leave'>Approve or Reject</a>
                        <br/><br/><strong> Regards</strong></br><p>eIDInfo Team </p>",
                            empData.FName+" "+empData.Lname, startDate, endDate, duration, reason,url);
                    MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
                    string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);           //"support@1234";
                    string subject = "Leave Application";

                    SmtpClient client = new SmtpClient();
                    client.Host = smtpData.smtpAddress;
                    client.Port = smtpData.portNo.Value;
                    client.EnableSsl = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);

                    MailMessage message = new MailMessage(fromAddress, toAddress);
                    message.Bcc.Add(bccAddress);
                    //message.CC.Add(cc1);

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
                    g.ShowMessage(this, "Mail send unsuccessfull, dur to config settings, Please contact administrator.");
                }
            }
        }
        catch (Exception ex) { }
    }
    #endregion

    #region send SMS
    //public void SendSMS (int empid)
    //{
    //    try
    //    {
    //        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
    //        {
    //            var empData = db.EmployeeTBs.Where(d => d.EmployeeId == empid).FirstOrDefault();
    //            tbl_SMSsettingTB smsData = db.tbl_SMSsettingTBs.Where(a => a.CompanyId == empData.CompanyId).FirstOrDefault();
               
    //            if (smsData != null)
    //            {
    //                string strUrl = smsData.URL +"user=" + smsData.UserName+"&pass="+ smsData.Password + "&sender=" + smsData.Sender + "&phone="+ empData.ContactNo + "&text=api%20test%20-%20BHASHSMS&priority=ndnd&stype=normal";
    //                // Create a request object  
    //                WebRequest request = HttpWebRequest.Create(strUrl);
    //                // Get the response back  
    //                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //                Stream s = (Stream)response.GetResponseStream();
    //                StreamReader readStream = new StreamReader(s);
    //                string dataString = readStream.ReadToEnd();
    //                response.Close();
    //                s.Close();
    //                readStream.Close();

    //                Genreal.SMSAuditApi(Convert.ToInt32(empData.EmployeeNo), empData.FName + " "+ empData.Lname, empData.ContactNo, "Success");
    //            }
    //            else
    //            {
    //                g.ShowMessage(this, "SMS send unsuccessfull, dur to config settings, Please contact administrator.");
    //            }
    //        }
    //    }
    //    catch (Exception ex) { }
    //}
    #endregion


}






