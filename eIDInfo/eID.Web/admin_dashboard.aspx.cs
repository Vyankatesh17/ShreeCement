using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChartJsHelper;
using Newtonsoft.Json;

public partial class admin_dashboard : System.Web.UI.Page
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
                txtDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                BindCompanyList();
                BindCountsAllCompany();
                //Chk15DaysPresent();
                if (Session["UserType"].Equals("LocationAdmin"))
                {                    
                    Mobilepunch.Visible = false;
                }

            }
            if (Session["UserType"].Equals("User"))
            {
                Response.Redirect("employee_dashboard.aspx");
            }
        }

        BindJqFunctions();
    }

    private void BindCountsAllCompany()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                DateTime dateTime = string.IsNullOrEmpty(txtDate.Text) ? DateTime.Now : Convert.ToDateTime(txtDate.Text);
                var leaveCount = (from d in db.LeaveApplicationDetailsTBs
                                  join l in db.LeaveApplicationsTBs on d.LeaveApplicationId equals l.LeaveApplicationId
                                  where d.LeaveDate.Value.Date == dateTime.Date 
                                  && l.TenantId == Convert.ToString(Session["TenantId"])
                                  select new
                                  {
                                      d.LeaveApplicationId,
                                      d.Id
                                  }).Count();

                var empCount = 0;
                int companyid = Convert.ToInt32(Session["CompanyID"]);
                if (Session["UserType"].Equals("LocationAdmin"))
                {
                     empCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == companyid && d.RelivingStatus == 0).Count();
                }
                else
                {
                     empCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.RelivingStatus == 0).Count();
                }

                    
                var presentCount = 0;
                
                if (Convert.ToString(Session["CompanyID"]) == "224")
                {
                    //presentCount = db.DeviceLogsTBs.Where(d => d.TenantId == Convert.ToString(Session["TenantId"]) && d.PunchStatus == "In" && d.ADate.Value.Date == dateTime.Date).Select(d => d.EmpID).Distinct().Count();

                    presentCount = (from d in db.DeviceLogsTBs
                                    join e in db.EmployeeTBs on d.EmpID equals e.EmployeeId
                                    where e.IsActive == true && e.TenantId == Convert.ToString(Session["TenantId"]) && d.ADate.Value.Date == dateTime.Date && d.PunchStatus == "In"
                                    select new { d.EmpID }).Distinct().Count();


                }
                else
                {
                    if (Session["UserType"].Equals("LocationAdmin"))
                    {
                        //presentCount = db.DeviceLogsTBs.Where(d => d.TenantId == Convert.ToString(Session["TenantId"]) && d.ADate.Value.Date == dateTime.Date && d.CompanyId == companyid).Select(d => d.EmpID).Distinct().Count();

                        presentCount = (from d in db.DeviceLogsTBs
                                        join e in db.EmployeeTBs on d.EmpID equals e.EmployeeId
                                        where e.IsActive == true && e.RelivingStatus == 0 && e.TenantId == Convert.ToString(Session["TenantId"]) && d.ADate.Value.Date == dateTime.Date && d.CompanyId == companyid
                                        select new { d.EmpID }).Distinct().Count();


                    }
                    else
                    {                       

                        presentCount = (from d in db.DeviceLogsTBs
                                        join e in db.EmployeeTBs on d.EmpID equals e.EmployeeId
                                        where e.IsActive == true && e.RelivingStatus == 0 && e.TenantId == Convert.ToString(Session["TenantId"]) && d.ADate.Value.Date == dateTime.Date
                                        select new { d.EmpID }).Distinct().Count();
                    }
                        
                }
                    
                var mobileCount = (from d in db.AppAttendanceTBs
                                   join e in db.EmployeeTBs on d.EmpId equals e.EmployeeId
                                   where e.IsActive == true && e.TenantId == Convert.ToString(Session["TenantId"]) && d.PunchDate.Value.Date == dateTime.Date
                                   select new { d.EmpId }).Distinct().Count();

                var birthdayCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"])  && d.BirthDate.Value.Date == dateTime.Date).Count();
                var absentCount = empCount - presentCount - leaveCount;
                lblAbsentCount.Text = absentCount.ToString();
                lblBirthdayCount.Text = birthdayCount.ToString();
                lblPresentCount.Text = (presentCount).ToString();
                lblTotalEmployees.Text = empCount.ToString();
                lblOnLeaveCount.Text = leaveCount.ToString();
                lblMobileCount.Text = mobileCount.ToString();

                #region Face, Finger and Card Counts
                /// date : 2022-03-03
                /// dev : Shrikant Patil
                /// desc : 
                /// 
                var faceCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.IsFacePresent == true && d.TenantId == Convert.ToString(Session["TenantId"]) ).Count();
                var fingerCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.IsFingerPrintPresent == true && d.TenantId == Convert.ToString(Session["TenantId"]) ).Count();
                var cardCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.IsAccessCardPresent == true && d.TenantId == Convert.ToString(Session["TenantId"]) ).Count();

                lblFaces.Text = faceCount.ToString();
                lblFingers.Text = fingerCount.ToString();
                lblCards.Text = cardCount.ToString();
                #endregion
            }
        }
        catch (Exception ex) { }
    }


    private void Chk15DaysPresent()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {

                DateTime todaysdate = DateTime.Now;
                DateTime onedaybeforedate = todaysdate.Date.AddDays(-1);
                DateTime beforethirteendaysdate = todaysdate.Date.AddDays(-13);
                DateTime beforefifteendaysdate = todaysdate.Date.AddDays(-15);
                DateTime twodayafterdate = todaysdate.Date.AddDays(2);
                int companyid = Convert.ToInt32(Session["CompanyID"]);
                var empData = (from d in db.EmployeeTBs
                               where d.IsActive == true && d.CompanyId == companyid
                               select d
                                 ).Distinct();                
                
                foreach (var item in empData)
                {
                    int EmpAbsent15Count = 0; int EmpAbsent13Count = 0;
                    for (var day = beforefifteendaysdate.Date; day.Date <= todaysdate.Date; day = day.AddDays(1))
                    {
                        var devicelogdata = db.DeviceLogsTBs.Where(a => a.ADate == day.Date && a.EmpID == item.EmployeeId && a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).ToList();
                        if (devicelogdata.Count == 0)
                        {
                            EmpAbsent15Count++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (var day = beforethirteendaysdate.Date; day.Date <= todaysdate.Date; day = day.AddDays(1))
                    {
                        var devicelogdata = db.DeviceLogsTBs.Where(a => a.ADate == day.Date && a.EmpID == item.EmployeeId && a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).ToList();
                        if (devicelogdata.Count == 0)
                        {
                            EmpAbsent13Count++;
                        }
                        else
                        {
                            break;
                        }
                    }


                    if (EmpAbsent13Count == 13 && item.RelivingStatus == 0)
                    {
                        SMTPSettingsTB smtpData = db.SMTPSettingsTBs.FirstOrDefault();

                        MailAddress fromAddress = new MailAddress(smtpData.emailFromAddress, "Face Deactivate.");
                        MailAddress toAddress = new MailAddress(item.Email);                      
                                
                        string subject = "Face Deactivate.";

                        string body = string.Format(@"<p>Dear {0}, </p> <br/><br/>
                            <p> Your face is Deactivated {1} from face devices please contact to admin.</p>
                                
                        <br/><br/><strong> Regards</strong></br><p>eTrackInfo Team.</p>",
                            item.FName + " " + item.Lname, twodayafterdate);

                        MailMessage message = new MailMessage(fromAddress, toAddress);
                        message.CC.Add(new MailAddress("Sagar@vaniasolutions.com"));
                        //message.Bcc.Add(bccAddress);
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
                    }
                     else if (EmpAbsent15Count >= 15 && item.RelivingStatus == 0)
                    {
                        var empdata = db.EmployeeTBs.Where(a => a.EmployeeId == item.EmployeeId).FirstOrDefault();                       

                        empdata.DExpiryDate = onedaybeforedate;                      
                        empdata.RelivingStatus = 1;
                        db.SubmitChanges();


                        string list = "";
                        foreach (ListItem devicelist in ddlDevice.Items)
                        {
                            if (devicelist.Selected)
                            {
                                list += devicelist.Text + " " + devicelist.Value + ",";

                                UpdateDevicePersonsData(empdata.FName + " " + empdata.Lname, empdata.EmployeeNo, empdata.EmployeeId.ToString(), devicelist.Value, onedaybeforedate.Date.ToString());

                            }
                        }
                    }                    
                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    private void UpdateDevicePersonsData(string empName, string empNo, string empId, string deviceId, string exDate)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var empData = db.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(empId)).FirstOrDefault();

            //List<EventInfo> loglist = new List<EventInfo>();
            try
            {
                bool deviceStat = false;


                var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == deviceId).FirstOrDefault();

                if (deviceData.DeviceModel == "Hikvision")
                {
                    #region REST Api
                    MatchlistDevice device = new MatchlistDevice();
                    #region Device Status
                    if (deviceData != null)
                    {
                        string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                        string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":1150}}";

                        string reps = string.Empty;
                        string strMatchNum = string.Empty;
                        clienthttp clnt = new clienthttp();
                        int iet = clnt.HttpRequest(deviceData.UserName, deviceData.Password, deviceStatUrl, "POST", req, ref reps);
                        if (iet == (int)HttpStatus.Http200)
                        {
                            DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(reps);
                            strMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                            if ("0" != strMatchNum)
                            {
                                //var dev = dr.SearchResult.MatchList.Select(d => d.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId).FirstOrDefault();
                                foreach (var item in dr.SearchResult.MatchList)
                                {
                                    if (item.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId)
                                    {
                                        device = item.Device;
                                        if (item.Device.devStatus == "online")
                                        {
                                            deviceStat = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion


                    if (deviceStat == false)
                    {
                        gen.ShowMessage(this.Page, "Device is offline, cant download device log..");
                    }
                    else if (device == null)
                    {
                        gen.ShowMessage(this.Page, "Device data not found..");
                    }
                    else if (deviceData != null)
                    {

                        string strMatchNum = string.Empty;
                        var devicedt = device;



                        DateTime dateStart = Convert.ToDateTime(empData.DOJ);
                        DateTime dateEnd = string.IsNullOrEmpty(exDate) ? dateStart.AddYears(10) : Convert.ToDateTime(exDate);

                        string beginTime = dateStart.Year + "-" + dateStart.Month.ToString("d2") + "-" + dateStart.Day.ToString("d2") + "T00:00:00";
                        string endTime = dateEnd.Year + "-" + dateEnd.Month.ToString("d2") + "-" + dateEnd.Day.ToString("d2") + "T23:00:00";

                        string index = devicedt.devIndex;



                        #region Update Code


                        string strName = empName;
                        string strUserType = "normal";

                        string strReq = "{\"UserInfo\" : {\"employeeNo\": \"" + empNo + "\",\"name\": \"" + strName + "\",\"Valid\" : {\"beginTime\": \"" + beginTime + "\",\"endTime\": \"" + endTime + "\"}}}";
                        string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/UserInfo/Modify?format=json&devIndex=" + index;
                        string strRsp = string.Empty;

                        clienthttp http = new clienthttp();
                        int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "PUT", strReq, ref strRsp);
                        string apiStatus = "failed";
                        if (iRet == (int)HttpStatus.Http200)
                        {
                            apiStatus = "success";

                        }
                        Genreal.AuditApi(strUrl, "UserInfo", "Modify", apiStatus, "device_import_persons", Session["DisplayName"].ToString(), "Update employee " + empNo, ddlCompany.SelectedValue, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                        #endregion

                    }
                    else
                    {
                        gen.ShowMessage(this.Page, "No devices found of selected company..");
                    }
                    #endregion
                }
            }
            catch (Exception ex) { Console.Write(ex.Message); }
        }
    }



    private void BindCounts()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                if(Convert.ToInt32(ddlCompany.SelectedValue) == 0)
                {
                    BindCountsAllCompany();
                }
                else
                {
                    DateTime dateTime = string.IsNullOrEmpty(txtDate.Text) ? DateTime.Now : Convert.ToDateTime(txtDate.Text);
                    var leaveCount = (from d in db.LeaveApplicationDetailsTBs
                                      join l in db.LeaveApplicationsTBs on d.LeaveApplicationId equals l.LeaveApplicationId
                                      where d.LeaveDate.Value.Date == dateTime.Date && l.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
                                      && l.TenantId == Convert.ToString(Session["TenantId"])
                                      select new
                                      {
                                          d.LeaveApplicationId,
                                          d.Id
                                      }).Count();


                    var empCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).Count();
                    var presentCount = 0;
                    if (Convert.ToString(Session["CompanyID"]) == "224")
                    {
                        presentCount = db.DeviceLogsTBs.Where(d => d.TenantId == Convert.ToString(Session["TenantId"]) && d.PunchStatus == "In" && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.ADate.Value.Date == dateTime.Date).Select(d => d.EmpID).Distinct().Count();
                    }
                    else
                    {
                        presentCount = db.DeviceLogsTBs.Where(d => d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.ADate.Value.Date == dateTime.Date).Select(d => d.EmpID).Distinct().Count();
                    }

                    var mobileCount = (from d in db.AppAttendanceTBs
                                       join e in db.EmployeeTBs on d.EmpId equals e.EmployeeId
                                       where e.IsActive == true && e.TenantId == Convert.ToString(Session["TenantId"]) && e.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.PunchDate.Value.Date == dateTime.Date
                                       select new { d.EmpId }).Distinct().Count();

                    var birthdayCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.BirthDate.Value.Date == dateTime.Date).Count();
                    var absentCount = empCount - presentCount - leaveCount;
                    lblAbsentCount.Text = absentCount.ToString();
                    lblBirthdayCount.Text = birthdayCount.ToString();
                    lblPresentCount.Text = (presentCount).ToString();
                    lblTotalEmployees.Text = empCount.ToString();
                    lblOnLeaveCount.Text = leaveCount.ToString();
                    lblMobileCount.Text = mobileCount.ToString();

                    #region Face, Finger and Card Counts
                    /// date : 2022-03-03
                    /// dev : Shrikant Patil
                    /// desc : 
                    /// 
                    var faceCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.IsFacePresent == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).Count();
                    var fingerCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.IsFingerPrintPresent == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).Count();
                    var cardCount = db.EmployeeTBs.Where(d => d.IsActive == true && d.IsAccessCardPresent == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).Count();

                    lblFaces.Text = faceCount.ToString();
                    lblFingers.Text = fingerCount.ToString();
                    lblCards.Text = cardCount.ToString();
                    #endregion
                }
            }
        }
        catch (Exception ex) { }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCounts(); BindDeviceList();
        //GetDeviceStatuses();
    }
    private void BindCompanyList()
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
        ddlCompany.Items.Insert(0, new ListItem("All", "0"));
    }




    private void BindDeviceList()
    {
        try
        {
            ddlDevice.Items.Clear();
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.DevicesTBs
                            where d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                            select new { d.DeviceSerialNo, d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();

                ddlDevice.DataSource = data;
                ddlDevice.DataTextField = "DeviceName";
                ddlDevice.DataValueField = "DeviceAccountId";
                ddlDevice.DataBind();
            }
            ddlDevice.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex) { }
    }


    [WebMethod]
    public static List<chartSourceData> getTrafficSourceData(string companyId, string date)
    {

        //DateTime dateTime = string.IsNullOrEmpty(date) ? DateTime.Now:date=="undefined"?DateTime.Now: Genreal.GetDate(date);

        List<chartSourceData> t = new List<chartSourceData>();

        try
        {


            string[] arrColor = new string[] { "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850"};

            Genreal gen = new Genreal();
            string query = "";

            if (companyId == "0")
            {
                query = string.Format(@"SELECT        COUNT(DISTINCT E.EmployeeId) AS Cnt, 'Total' AS Type
                        FROM            EmployeeTB AS E
                        WHERE         (E.TenantId = '{0}') 
                        UNION ALL
                        SELECT         COUNT(DISTINCT E.EmployeeId)  AS Cnt, 'Presents' AS Type
                        FROM            EmployeeTB AS E LEFT OUTER JOIN
                                                 DeviceLogsTB AS D ON D.EmpID = E.EmployeeId LEFT OUTER JOIN
                                                 AppAttendanceTB AS A ON E.EmployeeId = A.EmpId
                        WHERE        (CONVERT(date, D.AttendDate) = CONVERT(date, '{1}') OR CONVERT(date, A.PunchDate) = CONVERT(date, '{1}'))  AND (E.TenantId = '{0}') 
                        UNION ALL
                        SELECT        COUNT(EmployeeId) AS Cnt, 'Absents' AS Type
                        FROM            EmployeeTB AS E
                        WHERE        (EmployeeId NOT IN
                                                     (SELECT        EmpID
                                                       FROM            DeviceLogsTB
                                                       WHERE        (CONVERT(date, AttendDate) = CONVERT(date, '{1}')) AND (TenantId = '{0}') )) AND (EmployeeId NOT IN
                                                     (SELECT        EmpId
                                                       FROM            AppAttendanceTB
                                                       WHERE        (CONVERT(date, PunchDate) = CONVERT(date, '{1}')) AND (TenantId = '{0}') )) AND (TenantId = '{0}') 
                        UNION ALL
                        SELECT        COUNT(E.EmployeeId)  AS Cnt, 'OnLeaves' AS Type
                        FROM            LeaveApplicationsTB AS E 
                        WHERE        (CONVERT(date,'{1}') BETWEEN CONVERT(date, E.LeaveStartDate) AND CONVERT(date, E.LeaveEndDate)) AND (E.TenantId = '{0}') ", HttpContext.Current.Session["TenantId"].ToString(), date);


            }
            else
            {
                query = string.Format(@"SELECT        COUNT(DISTINCT E.EmployeeId) AS Cnt, 'Total' AS Type
                    FROM            EmployeeTB AS E
                    WHERE         (E.TenantId = '{0}') AND (E.CompanyId = '{1}')
                    UNION ALL
                    SELECT         COUNT(DISTINCT E.EmployeeId)  AS Cnt, 'Presents' AS Type
                    FROM            EmployeeTB AS E LEFT OUTER JOIN
                                             DeviceLogsTB AS D ON D.EmpID = E.EmployeeId LEFT OUTER JOIN
                                             AppAttendanceTB AS A ON E.EmployeeId = A.EmpId
                    WHERE        (CONVERT(date, D.AttendDate) = CONVERT(date, '{2}') OR CONVERT(date, A.PunchDate) = CONVERT(date, '{2}'))  AND (E.TenantId = '{0}') AND (E.CompanyId = '{1}')
                    UNION ALL
                    SELECT        COUNT(EmployeeId) AS Cnt, 'Absents' AS Type
                    FROM            EmployeeTB AS E
                    WHERE        (EmployeeId NOT IN
                                                 (SELECT        EmpID
                                                   FROM            DeviceLogsTB
                                                   WHERE        (CONVERT(date, AttendDate) = CONVERT(date, '{2}')) AND (TenantId = '{0}') AND (CompanyId = '{1}'))) AND (EmployeeId NOT IN
                                                 (SELECT        EmpId
                                                   FROM            AppAttendanceTB
                                                   WHERE        (CONVERT(date, PunchDate) = CONVERT(date, '{2}')) AND (TenantId = '{0}') AND (CompanyId = '{1}'))) AND (TenantId = '{0}') AND (CompanyId = '{1}')
                    UNION ALL
                    SELECT        COUNT(E.EmployeeId)  AS Cnt, 'OnLeaves' AS Type
                    FROM            LeaveApplicationsTB AS E 
                    WHERE        (CONVERT(date,'{2}') BETWEEN CONVERT(date, E.LeaveStartDate) AND CONVERT(date, E.LeaveEndDate)) AND (E.TenantId = '{0}') AND (E.CompanyId = '{1}')", HttpContext.Current.Session["TenantId"].ToString(), companyId, date);

            }

            DataTable data = gen.ReturnData(query);
            int counter = 0;
            foreach (DataRow item in data.Rows)
            {
                chartSourceData tsData = new chartSourceData();
                //tsData.companyid = Convert.ToInt32(comid);
                tsData.value = item["Cnt"].ToString();
                tsData.label = item["Type"].ToString();
                tsData.color = arrColor[counter];
                tsData.hightlight = date;
                t.Add(tsData);
                counter++;
            }
           


        }


        catch (Exception ex) { }
        return t;
    }
    public class chartSourceData
    {
        //public int companyid { get; set; }
        public string label { get; set; }
        public string value { get; set; }
        public string color { get; set; }
        public string hightlight { get; set; }
    }

    protected void ddlDevice_SelectedIndexChanged(object sender, EventArgs e)
    {
        string devStatus = "offline";
        string devIcoon = "bg-red";
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = db.DevicesTBs.Where(d => d.DeviceAccountId == ddlDevice.SelectedValue).FirstOrDefault();
                if (data != null)
                {
                    if(data.DeviceModel == "Hikvision")
                    { 
                    string password = string.IsNullOrEmpty(data.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : data.Password;
                    string userName = string.IsNullOrEmpty(data.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : data.UserName;
                    string deviceStatUrl = "http://" + data.IPAddress + ":" + data.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                    string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":700}}";

                    string reps = string.Empty;
                    string strMatchNum = string.Empty;
                    clienthttp clnt = new clienthttp();
                    int iet = clnt.HttpRequest(userName, password, deviceStatUrl, "POST", req, ref reps);

                    string apiStatus = "failed";
                    string devSerialNo = "";

                    if (iet == (int)HttpStatus.Http200)
                    {
                        DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(reps);
                        strMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                        if ("0" != strMatchNum)
                        {
                            var dev = dr.SearchResult.MatchList.Where(d => d.Device.EhomeParams.EhomeID == ddlDevice.SelectedValue).FirstOrDefault();//.Device.devStatus;
                            if (dev != null)
                            {
                                devStatus = string.IsNullOrEmpty(dev.Device.devStatus) ? "Offline" : dev.Device.devStatus;
                                    if(devStatus == "offline")
                                    {
                                        devIcoon = "bg-red";
                                    }
                                    else
                                    {
                                        devIcoon = "bg-green";
                                    }
                                //devIcoon = string.IsNullOrEmpty(dev.Device.devStatus) ? "bg-red" : "bg-green";
                                lblDeviceStatus.Text = devStatus;
                            }
                            divStat.Visible = true;
                            stat.InnerHtml = @"<label style='padding:5px;' class='btn btn-default btn-sm text-uppercase " + devIcoon + "'> &nbsp;&nbsp; " + devStatus + " &nbsp;&nbsp; </button>";
                        }
                        apiStatus = "success";
                    }
                    Genreal.AuditApi(deviceStatUrl, "DeviceMgmt", "GetDeviceStatus", apiStatus, "Dashboard", Session["DisplayName"].ToString(), "show device status", ddlCompany.SelectedValue, Session["TenantId"].ToString(), data.IPAddress, data.DeviceSerialNo, data.DeviceAccountId);
                }
                    else
                    {
                        var devstatusinfo = gen.GetesslDevice(data.DeviceAccountId);
                        if(devstatusinfo == "Online")
                        {
                            devStatus = "online";
                            devIcoon = string.IsNullOrEmpty(devStatus) ? "bg-red" : "bg-green";
                            lblDeviceStatus.Text = devStatus;
                            divStat.Visible = true;
                            stat.InnerHtml = @"<label style='padding:5px;' class='btn btn-default btn-sm text-uppercase " + devIcoon + "'> &nbsp;&nbsp; " + devStatus + " &nbsp;&nbsp; </button>";
                        }                       
                    }
            }
            }
        }
        catch (Exception ex) { lblDeviceStatus.Text = "Offline"; divStat.Visible = false; stat.InnerHtml = @"<label style='padding:5px;' class='btn btn-default btn-sm text-uppercase bg-red'> &nbsp;&nbsp; " + devStatus + " &nbsp;&nbsp; </button>"; }

    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        BindCounts(); BindDeviceList();
    }

    protected void lbtnDoorOpen_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDevice.SelectedIndex > 0 && lblDeviceStatus.Text == "online")
            {
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    string type = "open";
                    var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == ddlDevice.SelectedValue).FirstOrDefault();
                    if (deviceData != null)
                    {
                        string password = string.IsNullOrEmpty(deviceData.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : deviceData.Password;
                        string userName = string.IsNullOrEmpty(deviceData.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : deviceData.UserName;
                        string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                        string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":700}}";

                        string reps = string.Empty;
                        string strMatchNum = string.Empty;
                        clienthttp clnt = new clienthttp();
                        int iet = clnt.HttpRequest(userName, password, deviceStatUrl, "POST", req, ref reps);
                        string apiStatus = "failed";
                        if (iet == (int)HttpStatus.Http200)
                        {
                            DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(reps);
                            strMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                            if ("0" != strMatchNum)
                            {
                                //var dev = dr.SearchResult.MatchList.Select(d => d.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId).FirstOrDefault();
                                foreach (var item in dr.SearchResult.MatchList)
                                {
                                    if (item.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId)
                                    {
                                        var devicedt = item.Device;

                                        string index = devicedt.devIndex;
                                        string strReq = "{\"RemoteControlDoor\": {\"cmd\":\"" + type + "\"}}";
                                        string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/RemoteControl/door/1?format=json&devIndex=" + index;
                                        string strRsp = string.Empty;

                                        clienthttp http = new clienthttp();
                                        int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "PUT", strReq, ref strRsp);
                                        if (iRet == (int)HttpStatus.Http200)
                                        {
                                            gen.ShowMessage(this.Page, "Door opened..");

                                            apiStatus = "success";
                                        }
                                        Genreal.AuditApi(deviceStatUrl, "RemoteControl", "Open door", apiStatus, "dashboard", Session["DisplayName"].ToString(), "open door command", ddlCompany.SelectedValue, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                                    }
                                }
                            }
                        }

                    }

                }
            }
        }
        catch (Exception ex) { }
    }

    protected void lbtnDoorClose_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDevice.SelectedIndex > 0 && lblDeviceStatus.Text == "online")
            {
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    string type = "close";
                    var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == ddlDevice.SelectedValue).FirstOrDefault();
                    if (deviceData != null)
                    {
                        string password = string.IsNullOrEmpty(deviceData.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : deviceData.Password;
                        string userName = string.IsNullOrEmpty(deviceData.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : deviceData.UserName;
                        string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                        string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":700}}";

                        string reps = string.Empty;
                        string strMatchNum = string.Empty;
                        clienthttp clnt = new clienthttp();
                        int iet = clnt.HttpRequest(userName, password, deviceStatUrl, "POST", req, ref reps);
                        if (iet == (int)HttpStatus.Http200)
                        {
                            DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(reps);
                            strMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                            if ("0" != strMatchNum)
                            {
                                //var dev = dr.SearchResult.MatchList.Select(d => d.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId).FirstOrDefault();
                                foreach (var item in dr.SearchResult.MatchList)
                                {
                                    if (item.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId)
                                    {
                                        var devicedt = item.Device;

                                        string index = devicedt.devIndex;
                                        string strReq = "{\"RemoteControlDoor\": {\"cmd\":\"" + type + "\"}}";
                                        string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/RemoteControl/door/1?format=json&devIndex=" + index;
                                        string strRsp = string.Empty;

                                        clienthttp http = new clienthttp();
                                        int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "PUT", strReq, ref strRsp);
                                        string apiStatus = "failed";
                                        if (iRet == (int)HttpStatus.Http200)
                                        {
                                            gen.ShowMessage(this.Page, "Door closed..");
                                            apiStatus = "success";
                                        }
                                        Genreal.AuditApi(deviceStatUrl, "RemoteControl", "Close door", apiStatus, "dashboard", Session["DisplayName"].ToString(), "open door command", ddlCompany.SelectedValue, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        catch (Exception ex) { }
    }

    private void GetDeviceStatuses()
    {
        try
        {
            //<li><a href='#'>Projects <span class='pull-right badge bg-blue'>31</span></a></li>
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                int devCount = 0, activeCount = 0;
                string devStatus = "offline";
                string devIcoon = "bg-green";
                var dData = (from d in db.DevicesTBs where d.DeviceName != "ME" select d).Distinct();
                foreach (var item in dData)
                {
                    devCount++;
                    if (item.DeviceModel == "Hikvision")
                    {
                        string password = string.IsNullOrEmpty(item.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : item.Password;
                        string userName = string.IsNullOrEmpty(item.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : item.UserName;
                        string deviceStatUrl = "http://" + item.IPAddress + ":" + item.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                        string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":700}}";

                        string reps = string.Empty;
                        string strMatchNum = string.Empty;
                        clienthttp clnt = new clienthttp();
                        int iet = clnt.HttpRequest(userName, password, deviceStatUrl, "POST", req, ref reps);

                        string apiStatus = "failed";

                        if (iet == (int)HttpStatus.Http200)
                        {
                            DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(reps);
                            strMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                            if ("0" != strMatchNum)
                            {
                                var dev = dr.SearchResult.MatchList.Where(d => d.Device.EhomeParams.EhomeID == item.DeviceAccountId).FirstOrDefault();//.Device.devStatus;
                                if (dev != null)
                                {
                                    if (dev.Device.devStatus != "offline")
                                    {
                                        activeCount++;
                                    }

                                }
                            }
                            apiStatus = "success";
                        }
                        Genreal.AuditApi(deviceStatUrl, "DeviceMgmt", "GetDeviceStatus", apiStatus, "Dashboard", Session["DisplayName"].ToString(), "show device status", ddlCompany.SelectedValue, Session["TenantId"].ToString(), item.IPAddress, item.DeviceSerialNo, item.DeviceAccountId);
                    }
                    else
                    {
                        var devstatusinfo = gen.GetesslDevice(item.DeviceAccountId);
                        if (devstatusinfo == "Online")
                        {
                            activeCount++;
                        }
                    }
                }
                /*
                  <div class='progress'>
                            <div class='progress-bar' style='width: 70%'></div>
                        </div>
                        <span class='progress-description'>70% online out of 
                        </span>

                 <a href='#' onclick='showDeviceStatus(0);' class='small-box-footer'>More info <i class='fas fa-arrow-circle-right'></i></a>
                 */

                int percentComplete = (int)Math.Round((double)(100 * activeCount) / devCount);
                litStat.Text = string.Format(@" <div class='progress'>
                        <div class='progress-bar' style='width: {0}%'></div>
                    </div>
                    <span class='progress-description'>{1} online out of {2} <a href='dash_device_status.aspx?cId={3}' class='small-box-footer text-white pull-right' style='color:#f6f6f6;'>View <i class='fas fa-arrow-circle-right'></i></a>
                    </span>", percentComplete, activeCount, devCount, ddlCompany.SelectedValue);

                lblTotalDeviceCount.Text = devCount.ToString();

                lblOnlineDevices.Text = activeCount.ToString();
                lblOfflineDevices.Text = (devCount - activeCount).ToString();
            }
        }
        catch(Exception ex)
        {

        }
        
    }
}