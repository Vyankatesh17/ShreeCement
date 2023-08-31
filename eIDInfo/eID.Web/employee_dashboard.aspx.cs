using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class employee_dashboard : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCounts();
            BindDeviceList();
        }
    }
    private void BindCounts()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            DateTime dateTime = DateTime.Now;
            var empData = db.EmployeeTBs.Where(d => d.EmployeeId ==  Convert.ToInt32(Session["EmpId"])).FirstOrDefault();
            if (empData != null)
            {
                hfCompanyId.Value = empData.CompanyId.Value.ToString();


                var leaveCount = db.LeaveApplicationsTBs.Where(d =>d.EmployeeId==Convert.ToInt32(Session["EmpId"])&& d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(hfCompanyId.Value)
                && d.LeaveStartDate.Value.Date >= dateTime.Date && d.LeaveEndDate.Value.Date <= dateTime.Date).Count();

                var presentCount = db.DeviceLogsTBs.Where(d => d.EmpID == Convert.ToInt32(Session["EmpId"]) && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(hfCompanyId.Value) && d.ADate.Value.Date == dateTime.Date).Select(d => d.EmpID).Distinct().Count();
                var mobileCount = (from d in db.AppAttendanceTBs
                                   join e in db.EmployeeTBs on d.EmpId equals e.EmployeeId
                                   where d.EmpId == Convert.ToInt32(Session["EmpId"]) && e.TenantId == Convert.ToString(Session["TenantId"]) && e.CompanyId == Convert.ToInt32(hfCompanyId.Value) && d.PunchDate.Value.Date == dateTime.Date
                                   select new { d.EmpId }).Distinct().Count();

                var birthdayCount = db.EmployeeTBs.Where(d => d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(hfCompanyId.Value) && d.BirthDate.Value.Date == dateTime.Date).Count();
                var absentCount = 1 - presentCount - mobileCount - leaveCount;
                lblAbsentCount.Text = absentCount.ToString();
                lblBirthdayCount.Text = birthdayCount.ToString();
                lblPresentCount.Text = presentCount.ToString();
            }
            else
            {
                hfCompanyId.Value = "0";
            }
        }
    }
    protected void ddlDevice_SelectedIndexChanged(object sender, EventArgs e)
    {
        string devStatus = "offline";
        string devIcoon = "bg-green";
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = db.DevicesTBs.Where(d => d.DeviceAccountId == ddlDevice.SelectedValue).FirstOrDefault();
                if (data != null)
                {
                    string password = string.IsNullOrEmpty(data.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : data.Password;
                    string userName = string.IsNullOrEmpty(data.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : data.UserName;
                    string deviceStatUrl = "http://" + data.IPAddress + ":" + data.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                    string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":150}}";

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
                            var dev = dr.SearchResult.MatchList.FirstOrDefault(d => d.Device.EhomeParams.EhomeID == ddlDevice.SelectedValue).Device.devStatus;

                            devStatus = string.IsNullOrEmpty(dev) ? "Offline" : dev;
                            devIcoon = string.IsNullOrEmpty(dev) ? "bg-red" : "bg-green";
                            lblDeviceStatus.Text = devStatus;

                            divStat.Visible = true;
                            stat.InnerHtml = @"<label style='padding:5px;' class='btn btn-default btn-sm text-uppercase " + devIcoon + "'> &nbsp;&nbsp; " + devStatus + " &nbsp;&nbsp; </button>";
                        }
                        apiStatus = "success";
                    }
                    Genreal.AuditApi(deviceStatUrl, "DeviceMgmt", "GetDeviceStatus", apiStatus, "Dashboard", Session["DisplayName"].ToString(), "show device status", hfCompanyId.Value, Session["TenantId"].ToString(), data.IPAddress, data.DeviceSerialNo, data.DeviceAccountId);
                }
            }
        }
        catch (Exception ex) { lblDeviceStatus.Text = "Offline"; divStat.Visible = false; stat.InnerHtml = @"<label style='padding:5px;' class='btn btn-default btn-sm text-uppercase bg-red'> &nbsp;&nbsp; " + devStatus + " &nbsp;&nbsp; </button>"; }

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

                        string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":150}}";

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
                                        Genreal.AuditApi(deviceStatUrl, "RemoteControl", "Open door", apiStatus, "dashboard", Session["DisplayName"].ToString(), "open door command", hfCompanyId.Value, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
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

                        string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":150}}";

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
                                        Genreal.AuditApi(deviceStatUrl, "RemoteControl", "Close door", apiStatus, "dashboard", Session["DisplayName"].ToString(), "open door command", hfCompanyId.Value, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
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
    private void BindDeviceList()
    {
        try
        {
            ddlDevice.Items.Clear();
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.DevicesTBs
                            where d.CompanyId == Convert.ToInt32(hfCompanyId.Value) && d.TenantId == Convert.ToString(Session["TenantId"])
                            select new { d.DeviceSerialNo, d.DeviceAccountId }).Distinct();

                ddlDevice.DataSource = data;
                ddlDevice.DataTextField = "DeviceSerialNo";
                ddlDevice.DataValueField = "DeviceAccountId";
                ddlDevice.DataBind();
            }
            ddlDevice.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex) { }
    }
    private void GetDeviceStatuses()
    {
        //<li><a href='#'>Projects <span class='pull-right badge bg-blue'>31</span></a></li>
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int devCount = 0, activeCount = 0;
            string devStatus = "offline";
            string devIcoon = "bg-green";
            var dData = (from d in db.DevicesTBs where d.CompanyId == Convert.ToInt32(hfCompanyId.Value) select d).Distinct();
            foreach (var item in dData)
            {
                devCount++;

                string password = string.IsNullOrEmpty(item.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : item.Password;
                string userName = string.IsNullOrEmpty(item.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : item.UserName;
                string deviceStatUrl = "http://" + item.IPAddress + ":" + item.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":150}}";

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
                        var dev = dr.SearchResult.MatchList.FirstOrDefault(d => d.Device.EhomeParams.EhomeID == item.DeviceAccountId).Device.devStatus;

                        if (!string.IsNullOrEmpty(dev))
                            activeCount++;
                    }
                    apiStatus = "success";
                }
                Genreal.AuditApi(deviceStatUrl, "DeviceMgmt", "GetDeviceStatus", apiStatus, "Dashboard", Session["DisplayName"].ToString(), "show device status", hfCompanyId.Value, Session["TenantId"].ToString(), item.IPAddress, item.DeviceSerialNo, item.DeviceAccountId);
            }
            /*
              <div class='progress'>
                        <div class='progress-bar' style='width: 70%'></div>
                    </div>
                    <span class='progress-description'>70% online out of 
                    </span>

             <a href='#' onclick='showDeviceStatus(0);' class='small-box-footer'>More info <i class='fas fa-arrow-circle-right'></i></a>
             */
             
        }
    }
}