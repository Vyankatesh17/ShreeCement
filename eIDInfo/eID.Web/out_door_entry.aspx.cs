using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class out_door_entry : System.Web.UI.Page
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
                BindOutDoorList();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        if (gvDataList.Rows.Count > 0)
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    DateTime sDate = Genreal.GetDate(txtFromDate.Text);
                    DateTime eDate = Genreal.GetDate(txtToDate.Text);

                    OutDoorEntryTB outDoor = new OutDoorEntryTB();
                    outDoor.Description = txtDetails.Text;
                    outDoor.HrStatus = "Pending";
                    outDoor.ManagerStatus = "Pending";
                    outDoor.FromDate = Convert.ToDateTime(txtFromDate.Text);
                    outDoor.ToDate = Convert.ToDateTime(txtToDate.Text);
                    outDoor.TravelPlace = txtTravelPlace.Text;
                    outDoor.TravelReason = txtTravelReason.Text;
                    outDoor.EmployeeId =Session["UserType"].ToString()=="User"? Convert.ToInt32(Convert.ToString(Session["EmpId"])):Convert.ToInt32(ddlEmployee.SelectedValue);
                    outDoor.FromTime = TimeSpan.Parse(txtFromTime.Text);
                    outDoor.ToTime = TimeSpan.Parse(txtToTime.Text);
                    outDoor.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    outDoor.TenantId = Convert.ToString(Session["TenantId"]);
                    db.OutDoorEntryTBs.InsertOnSubmit(outDoor);
                    db.SubmitChanges();

                    for (var day = sDate.Date; day.Date <= eDate.Date; day = day.AddDays(1))
                    {
                        OutDoorEntryDetailsTB detailsTB = new OutDoorEntryDetailsTB();
                        detailsTB.OutDoorEntry_Id = outDoor.OddId;
                        detailsTB.Employee_Id = outDoor.EmployeeId;
                        detailsTB.OD_Date = day;
                        detailsTB.ODFrom_Time = outDoor.FromTime;
                        detailsTB.ODTo_Time = outDoor.ToTime;
                        detailsTB.Entry_Date = DateTime.Now;
                        detailsTB.Status = true;

                        db.OutDoorEntryDetailsTBs.InsertOnSubmit(detailsTB);
                        db.SubmitChanges();
                    }


                    #region Send mail to department head
                    SendMail(outDoor.EmployeeId.Value, outDoor.FromDate.Value.ToShortDateString(), outDoor.ToDate.Value.ToShortDateString(), outDoor.FromTime.Value.ToString(), outDoor.ToTime.Value.ToString(), outDoor.TravelReason,outDoor.TravelPlace);
                    #endregion

                    gen.ShowMessage(this.Page, "OD entry saved successfully..");
                    txtDetails.Text = txtFromDate.Text = txtToDate.Text = txtTravelPlace.Text = txtTravelReason.Text = txtFromTime.Text = txtToTime.Text = "";
                    ddlCompany.SelectedIndex = 0;
                    ddlEmployee.SelectedIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                }
            }
            catch (Exception ex) { }
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

    private void BindOutDoorList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.OutDoorEntryTBs
                        join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                        where  d.TenantId==Convert.ToString(Session["TenantId"])
                        select new
                        {
                            d.Description,
                            d.HrRemark,
                            d.HrStatus,
                            d.ManagerRemark,
                            d.ManagerStatus,
                            d.OddId,
                            d.FromDate,
                            d.ToDate,
                            d.TravelPlace,
                            d.TravelReason,
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
    protected void lbtnEvent_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        hfKey.Value = linkButton.CommandArgument;

        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            OutDoorEntryTB outDoorEntryTB = db.OutDoorEntryTBs.Where(d => d.OddId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
            db.OutDoorEntryTBs.DeleteOnSubmit(outDoorEntryTB);
            db.SubmitChanges();

            List<OutDoorEntryDetailsTB> odentrydetails = db.OutDoorEntryDetailsTBs.Where(a => a.OutDoorEntry_Id == Convert.ToInt32(hfKey.Value)).ToList();
            db.OutDoorEntryDetailsTBs.DeleteAllOnSubmit(odentrydetails);
            db.SubmitChanges();

            gen.ShowMessage(this.Page, "OD Entry deleted..");
            BindOutDoorList();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
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
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            DateTime todaysdt = DateTime.Now;
            var month = todaysdt.Month;
            ddlEmployee.Items.Clear();

            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.RelivingDate, dt.RelivingStatus }).OrderBy(d => d.name);
            List<EmployeeTB> emplist = new List<EmployeeTB>();

            if (data != null && data.Count() > 0)
            {
                foreach (var item in data)
                {
                    EmployeeTB emp = new EmployeeTB();
                    if (item.RelivingStatus == 1)
                    {
                        DateTime relivingdate = Convert.ToDateTime(item.RelivingDate);
                        var relivingmonth = relivingdate.Month;

                        if (relivingmonth == month)
                        {
                            emp.EmployeeId = item.EmployeeId;
                            emp.FName = item.name;

                            emplist.Add(emp);
                        }
                    }
                    else
                    {
                        emp.EmployeeId = item.EmployeeId;
                        emp.FName = item.name;
                        emplist.Add(emp);
                    }
                }

                ddlEmployee.DataSource = emplist;
                ddlEmployee.DataTextField = "FName";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
    }

    //private     void BindEmployeeList()
    //{
    //    ddlEmployee.Items.Clear();
    //    int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
    //    using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
    //    {
    //        var data = (from dt in db.EmployeeTBs
    //                    where dt.IsActive == true && (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.CompanyId == cId
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

    public void SendMail(int empId, string fromDate, string toDate, string startTime, string endTime, string reason,string place)
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
                    MailAddress toAddress = new MailAddress(adminMail.Email);
                    //MailAddress cc1 = new MailAddress(empData.Email);
                    string url = "http://eidinfo.in/hrms/out_door_approve_hr.aspx";

                    string body = string.Format(@"<p>Dear, </p> <p>Your have a new outdoor visit request from {0}, 
                        OD details are as bellow <br/><br/> 
                        From Date : {1} <BR/> 
                        To Date : {2} <BR/> 
                        FromTime : {3}</br>
                        End Time : {4} <br/> 
                        Reason : {5} <br/> 
                        Place : {6} <br/> 
                        <a href='{7}' title='Approve or Reject Leave'>Approve or Reject</a>
                        <br/><br/><strong> Regards</strong></br><p>eIDInfo Team </p>",
                            empData.FName + " " + empData.Lname, fromDate, toDate, startTime, endTime, reason,place,url);
                    //MailAddress bccAddress = new MailAddress("shrikantpatil12345@gmail.com");
                    string fromPassword = SPPasswordHasher.Decrypt(smtpData.emailFromPassword);              //"support@1234";
                    string subject = "OD Application - " + empData.FName + " " + empData.Lname;

                    SmtpClient client = new SmtpClient();
                    client.Host = smtpData.smtpAddress;
                    client.Port = smtpData.portNo.Value;
                    client.EnableSsl = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(fromAddress.Address, smtpData.SMTPPassword);

                    MailMessage message = new MailMessage(fromAddress, toAddress);
                    //message.Bcc.Add(bccAddress);
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
                    gen.ShowMessage(this, "Mail send unsuccessfull, due to config settings, Please contact administrator.");
                }
            }
        }
        catch (Exception ex) { }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }
}