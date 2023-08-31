using DocumentFormat.OpenXml.EMMA;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class download_device_log : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["TenantId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindDeviceList();
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
        BindJqFunctions();
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
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void BindDeviceList()
    {

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.DevicesTBs
                        where d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
            lstFruits.DataSource = data;
            lstFruits.DataTextField = "DeviceName";
            lstFruits.DataValueField = "DeviceAccountId";
            lstFruits.DataBind();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            
            db.CommandTimeout = 8 * 60;                          
                foreach (ListItem item in lstFruits.Items)
                {
                    if (item.Selected)
                    {                                            
                        GetDeviceLogData(item.Value);
                    }
                }
            
        }

        //BindDataList();
    }
    private void GetDeviceLogData(string deviceId)
    {

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            List<EventInfo> loglist = new List<EventInfo>();
            try
            {
                bool deviceStat = false;

                #region Device Status
                var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == deviceId).FirstOrDefault();
                MatchlistDevice device = new MatchlistDevice();
                if (deviceData != null)
                {
                    string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                    string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":500}}";

                    string reps = string.Empty;
                    string strMatchNum = string.Empty;
                    clienthttp clnt = new clienthttp();

                    string userName = string.IsNullOrEmpty(deviceData.UserName)? ConfigurationManager.AppSettings["DeviceUserName"] : deviceData.UserName;
                    string password = string.IsNullOrEmpty(deviceData.Password) ?ConfigurationManager.AppSettings["DevicePassword"] : deviceData.Password;

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


                if (deviceStat == false) {
                    gen.ShowMessage(this.Page, "Device is offline, cant download device log..");
                }
                else if(device == null)
                {
                    gen.ShowMessage(this.Page, "Device data not found..");
                }
                else if (deviceData != null)
                {
                    int counter = 0;
                    string strMatchNum = string.Empty;
                    var devicedt = device;

                    DateTime fdate = Genreal.GetDate(txtFromDate.Text);// Convert.ToDateTime(deviceLog.fromdate);
                    string BeginTime = fdate.Year + "-" + fdate.Month.ToString("d2") + "-" + fdate.Day.ToString("d2") + "T00:00:00+05:30";

                    DateTime tdate = Genreal.GetDate(txtToDate.Text);// Convert.ToDateTime(deviceLog.todate);
                    string endTime = tdate.Year + "-" + tdate.Month.ToString("d2") + "-" + tdate.Day.ToString("d2") + "T23:00:00+05:30";

                    //var empData = (from d in db.EmployeeTBs
                    //               where d.IsActive == true && (d.RelivingStatus == 0 || d.RelivingStatus == null) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                    //               select d
                    //             ).Distinct();

                    //foreach (var empItem in empData)
                    //{
                        string userName = string.IsNullOrEmpty(deviceData.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : deviceData.UserName;
                        string password = string.IsNullOrEmpty(deviceData.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : deviceData.Password;


                        string index = devicedt.devIndex;
                        //string strReq = "{\"AcsEventSearchDescription\" : {\"searchID\":\"0\",\"searchResultPosition\":0,\"maxResults\":500,\"AcsEventFilter\": {\"major\":0,\"minor\":0,\"startTime\": \"" + BeginTime + "\",\"endTime\": \"" + endTime + "\"}}}";
                        string strReq = "{\"AcsEventSearchDescription\" : {\"searchID\":\"0\",\"searchResultPosition\":0,\"maxResults\":5000,\"AcsEventFilter\": {\"major\":0,\"minor\":0,\"startTime\": \"" + BeginTime + "\",\"endTime\": \"" + endTime + "\",\"picEnable\":true}}}";
                        string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/AcsEvent?format=json&devIndex=" + index;
                        string strRsp = string.Empty;

                        clienthttp http = new clienthttp();
                        int iRet = http.HttpRequest(userName, password, strUrl, "POST", strReq, ref strRsp);
                        string apiStatus = "failed";
                        if (iRet == (int)HttpStatus.Http200)
                        {
                            EventSearchRoot drnew = JsonConvert.DeserializeObject<EventSearchRoot>(strRsp);
                            strMatchNum = drnew.AcsEventSearchResult.numOfMatches;
                            if ("0" != strMatchNum)
                            {
                                int totMatches = Convert.ToInt32(drnew.AcsEventSearchResult.totalMatches);

                                int rows = totMatches / 30;
                                for (int i = 0; i <= rows; i++)
                                {
                                    int searchPoss = i * 30;
                                    string perApiRequest = "{\"AcsEventSearchDescription\" : {\"searchID\":\"0\",\"searchResultPosition\": " + searchPoss + ",\"maxResults\":5000,\"AcsEventFilter\": {\"major\":0,\"minor\":0,\"startTime\": \"" + BeginTime + "\",\"endTime\": \"" + endTime + "\",\"picEnable\":true}}}";
                                    //string perApiRequest = "{\"UserInfoSearchCond\": {\"searchID\": \"C7E71364-4560-0001-6EDD-16ED17B01CCD\",\"searchResultPosition\": " + searchPoss + ",\"maxResults\": 5000}}";

                                    string perApiResponse = string.Empty;
                                    clienthttp httpP = new clienthttp();
                                    int iRetP = http.HttpRequest(userName, password, strUrl, "POST", perApiRequest, ref perApiResponse);

                                if (iRetP == (int)HttpStatus.Http200)
                                {
                                    drnew = JsonConvert.DeserializeObject<EventSearchRoot>(perApiResponse);

                                    if(drnew.AcsEventSearchResult.MatchList != null)
                                    { 
                                    foreach (var logitem in drnew.AcsEventSearchResult.MatchList)
                                    {
                                        if (logitem.employeeNoString != "" && logitem.employeeNoString != null)
                                        {
                                                DevicePunchesTB info = new DevicePunchesTB();
                                                info.major = logitem.major;
                                                info.minor = logitem.minor;
                                                info.time = Convert.ToDateTime(logitem.time);
                                                info.DeviceAccountId = deviceData.DeviceAccountId;
                                                info.employeeNoString = logitem.employeeNoString != null ? logitem.employeeNoString : "";
                                                info.cardNo = logitem.cardNo != null ? logitem.cardNo : "";
                                                info.cardReaderNo = logitem.cardReaderNo != null ? logitem.cardReaderNo : "";
                                                info.doorNo = logitem.doorNo != null ? logitem.doorNo : "";
                                                info.currentVerifyMode = logitem.currentVerifyMode;
                                                info.serialNo = logitem.serialNo;
                                                info.type = logitem.type;
                                                info.mask = logitem.mask;
                                                info.name = logitem.name != null ? logitem.name : "";
                                                info.userType = logitem.userType != null ? logitem.userType : "";
                                                db.DevicePunchesTBs.InsertOnSubmit(info);
                                                db.SubmitChanges();


                                                var empdata = db.EmployeeTBs.Where(a => a.EmployeeNo == logitem.employeeNoString && a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && a.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();

                                            if (empdata != null)
                                            {                                                   

                                                    DateTime logdt = Convert.ToDateTime(logitem.time);
                                                    string cardNumber = logitem.cardNo != null ? logitem.cardNo : "";
                                                    var devicelogdata = db.DeviceLogsTBs.Where(a => a.EmpID == empdata.EmployeeId && a.AttendDate == logdt && a.ATime == Convert.ToDateTime(logdt).TimeOfDay).FirstOrDefault();
                                                    TimeSpan timedt = Convert.ToDateTime(logdt).TimeOfDay;
                                                    
                                                    if (devicelogdata == null)
                                                    {
                                                        DeviceLogsTB dlog = new DeviceLogsTB();
                                                        dlog.DeviceAccountId = Convert.ToInt32(deviceData.DeviceAccountId);
                                                        dlog.AttendDate = logdt;
                                                        dlog.EmpID = empdata.EmployeeId;
                                                        dlog.AccessCardNo = cardNumber;
                                                        dlog.DownloadDate = DateTime.Now;
                                                        dlog.ADate = logdt;
                                                        dlog.ATime = timedt;
                                                        dlog.TenantId = Convert.ToString(Session["TenantId"]);
                                                        dlog.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                                        dlog.Calculationflag = 0;
                                                        dlog.PunchStatus = deviceData.DeviceDirection;
                                                        db.DeviceLogsTBs.InsertOnSubmit(dlog);
                                                        db.SubmitChanges();
                                                    }

                                                    TimeSpan oneHours = TimeSpan.Parse("01:00");
                                                    TimeSpan starttime = timedt.Subtract(oneHours);

                                                    var Shreecementdata = db.ShriCementPunchDatas.Where(a => a.EMP_CODE == empdata.EmployeeNo && a.PUNCH_TIME >= starttime && a.PUNCH_TIME <= timedt && a.DIRECTION == deviceData.DeviceDirection).ToList();

                                                    if (Shreecementdata.Count == 0)
                                                    {
                                                        ShriCementPunchData shridata = new ShriCementPunchData();
                                                        shridata.EMP_CODE = empdata.EmployeeNo;
                                                        shridata.EMP_NAME = empdata.FName + " " + empdata.Lname;
                                                        shridata.PUNCH_TIME = timedt;
                                                        shridata.PUNCH_DATE_TIME = logdt;
                                                        shridata.RECORD_RECEIVE_TIME = logdt.ToString();
                                                        shridata.DEVICE_IP = deviceData.DeviceIp;
                                                        shridata.DEVICE_ID = Convert.ToInt32(deviceId);
                                                        shridata.DEVICE_SLNO = deviceData.DeviceSerialNo;
                                                        shridata.MACHINE_MAKE = deviceData.DeviceModel;
                                                        shridata.DEVICE_NAME = deviceData.DeviceName;
                                                        shridata.DIRECTION = deviceData.DeviceDirection;
                                                        db.ShriCementPunchDatas.InsertOnSubmit(shridata);
                                                        db.SubmitChanges();
                                                    }


                                                    //EventInfo dt = new EventInfo();
                                                    //dt.major = logitem.major;
                                                    //dt.minor = logitem.minor;
                                                    //dt.time = logitem.time;
                                                    //dt.Atime = Convert.ToDateTime(logitem.time).ToString("hh:mm:ss");
                                                    //dt.ADate = Convert.ToDateTime(logitem.time).ToString("dd/MM/yy");
                                                    //dt.employeeNoString = logitem.employeeNoString != null ? logitem.employeeNoString : "";
                                                    //dt.cardNo = logitem.cardNo != null ? logitem.cardNo : "";
                                                    //dt.cardReaderNo = logitem.cardReaderNo != null ? logitem.cardReaderNo : "";
                                                    //dt.doorNo = logitem.doorNo != null ? logitem.doorNo : "";
                                                    //dt.currentVerifyMode = logitem.currentVerifyMode;
                                                    //dt.serialNo = logitem.serialNo;
                                                    //dt.type = logitem.type;
                                                    //dt.mask = logitem.mask;
                                                    //dt.name = logitem.name != null ? logitem.name : ""; ;
                                                    //dt.userType = logitem.userType != null ? logitem.userType : "";
                                                    //dt.CompanyName = ddlCompany.Text;
                                                    //dt.DeviceAccountId = info.DeviceAccountId;
                                                    //loglist.Add(dt);

                                                    counter++;
                                            }
                                        }
                                    }
                                }
                                    }
                                }
                                apiStatus = "success";
                            }
                            gen.ShowMessage(this.Page, "Log downloaded of " + counter + " employees..");
                        //}
                        Genreal.AuditApi(strUrl, "AccessControl", "AcsEvent", apiStatus, "download_device_log", Session["DisplayName"].ToString(), "download device log of " + index, ddlCompany.SelectedValue, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                    }
                    BindDataList();
                }               
                else
                {
                    gen.ShowMessage(this.Page, "No devices found of selected company..");
                }
            }
            catch (Exception ex) { Console.Write(ex.Message); }
        }
    }
    

    private void BindDataList()
    {
            try
            {
            
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {              
                        db.CommandTimeout = 5 * 60;
                        DateTime fdate = Genreal.GetDate(txtFromDate.Text);// Convert.ToDateTime(deviceLog.fromdate);

                        DateTime tdate = Genreal.GetDate(txtToDate.Text);// Convert.ToDateTime(deviceLog.todate);

                var data = (from d in db.DevicePunchesTBs
                            join emp in db.EmployeeTBs on d.employeeNoString equals emp.EmployeeNo
                            join c in db.CompanyInfoTBs on emp.CompanyId equals c.CompanyId
                            where c.TenantId == Convert.ToString(Session["TenantId"]) && c.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.time >= fdate && d.time < tdate
                            select new
                            {
                                CompanyName = c.CompanyName,
                                EmpName = emp.FName + " " + emp.Lname,
                                employeeNoString = d.employeeNoString,
                                cardNo = d.cardNo,
                                cardReaderNo = d.cardReaderNo,
                                DeviceAccountId = d.DeviceAccountId,
                                time = Convert.ToDateTime(d.time),
                                type = d.type,
                                userType = d.userType,
                                serialNo = d.serialNo
                            }).Distinct();

                //                DataTable dataTable = new DataTable();


                //                foreach (ListItem item in lstFruits.Items)
                //                {
                //                    if (item.Selected)
                //                    {

                //                        string devicelogQuery = string.Format(@"select * from DevicePunchesTB as d
                //inner join EmployeeTB as emp on emp.EmployeeNo = d.employeeNoString
                //inner join CompanyInfoTB as c on c.CompanyId = emp.CompanyId
                //where c.TenantId == " + Convert.ToString(Session["TenantId"]) + "&& c.CompanyId ==" + Convert.ToInt32(ddlCompany.SelectedValue) + "&& d.time >=" + fdate + "&& d.time <" + tdate + "&& d.DeviceAccountId = "+ item.Value);


                // dataTable = gen.ReturnData(devicelogQuery);

                //                    }
                //                }

                gvDataList.DataSource = data;
                gvDataList.DataBind();
            }
            }
            catch (Exception ex) { }
       
    }

    public class DeviceLogInfo
    {
        public string CompanyName;
        public string EmpName;
        public string employeeNoString;
        public string cardNo;
        public string cardReaderNo;
        public string DeviceAccountId;
        public DateTime time;
        public string type;
        public string userType;
        public string serialNo;
       
    }

    
    public class EventInfo
    {
        public string major;
        public string minor;
        public string time;
        public DateTime eventdate;
        public string Atime;
        public string ADate;
        public string employeeNoString;
        public string cardNo;
        public string cardReaderNo;
        public string doorNo;
        public string currentVerifyMode;
        public string serialNo;
        public string type;
        public string mask;
        public string name;
        public string userType;
        public string DeviceAccountId;
        public string CompanyName;
    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDeviceList();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindDataList();
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstFruits.Items.Clear();
        if (ddlLocation.SelectedIndex > 0)
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.DevicesTBs
                            where d.TenantId == Convert.ToString(Session["TenantId"]) && d.DeviceLocation == ddlLocation.SelectedValue
                            select new { d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
                lstFruits.DataSource = data;
                lstFruits.DataTextField = "DeviceName";
                lstFruits.DataValueField = "DeviceAccountId";
                lstFruits.DataBind();
            }
        }
        else
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.DevicesTBs
                            where d.TenantId == Convert.ToString(Session["TenantId"])
                            select new { d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
                lstFruits.DataSource = data;
                lstFruits.DataTextField = "DeviceName";
                lstFruits.DataValueField = "DeviceAccountId";
                lstFruits.DataBind();
            }
        }
    }




}