using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class import_employees_from_devices : System.Web.UI.Page
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

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDeviceList();
    }

    #region Download Employee

    private void DownloadEmployeeData( string deviceId)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            db.CommandTimeout = 5 * 60;
            int counter = 0;
            var shiftData = db.MasterShiftTBs.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();

            var desigData = db.MasterDesgTBs.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();

            bool deviceStat = false;

            var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == deviceId).FirstOrDefault();
            MatchlistDevice device = new MatchlistDevice();

            #region Get Device Data
            if (deviceData != null)
            {
                string deviceApiUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";
                string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":500}}";

                string reps = string.Empty;
                string strMatchNum = string.Empty;
                clienthttp clnt = new clienthttp();
                int iet = clnt.HttpRequest(deviceData.UserName, deviceData.Password, deviceApiUrl, "POST", req, ref reps);
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
                gen.ShowMessage(this.Page, "Device is offline, can not enable to download employees");
            }
            else if (device == null)
            {
                gen.ShowMessage(this.Page, "Device not found");
            }
            else if (deviceData != null)
            {
                string strMatchNum = string.Empty;
                var devicedt = device;

                string devIndex = device.devIndex;

                string personsApiUrl = string.Format(@"http://{0}:{1}/ISAPI/AccessControl/UserInfo/Search?format=json&devIndex={2}", deviceData.IPAddress, deviceData.PortNo, devIndex);
                string personsApiRequest = "{\"UserInfoSearchCond\": {\"searchID\": \"C7E71364-4560-0001-6EDD-16ED17B01CCD\",\"searchResultPosition\": 0,\"maxResults\": 5000}}";

                string personsApiResponse = string.Empty;
                clienthttp http = new clienthttp();
                int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, personsApiUrl, "POST", personsApiRequest, ref personsApiResponse);
                if (iRet == (int)HttpStatus.Http200)
                {
                    EmployeeDeserializedClass.EmployeeDeserializedRoot searchRow = JsonConvert.DeserializeObject<EmployeeDeserializedClass.EmployeeDeserializedRoot>(personsApiResponse);

                    strMatchNum = searchRow.UserInfoSearch.numOfMatches.ToString();
                    if ("0" != strMatchNum)
                    {
                        int totMatches = searchRow.UserInfoSearch.totalMatches;

                        int rows = totMatches / 30;
                        for (int i = 0; i <= rows; i++)
                        {
                            int searchPoss = i * 30;
                            string perApiRequest = "{\"UserInfoSearchCond\": {\"searchID\": \"C7E71364-4560-0001-6EDD-16ED17B01CCD\",\"searchResultPosition\": " + searchPoss + ",\"maxResults\": 5000}}";

                            string perApiResponse = string.Empty;
                            clienthttp httpP = new clienthttp();
                            int iRetP = http.HttpRequest(deviceData.UserName, deviceData.Password, personsApiUrl, "POST", perApiRequest, ref perApiResponse);
                            if (iRetP == (int)HttpStatus.Http200)
                            {
                                searchRow = JsonConvert.DeserializeObject<EmployeeDeserializedClass.EmployeeDeserializedRoot>(perApiResponse);
                                //List<UserInfo> users=se
                                foreach (var item in searchRow.UserInfoSearch.UserInfo)
                                {
                                    // check employee exists in database
                                    var empData = db.EmployeeTBs.Where(d => d.EmployeeNo == item.employeeNo && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
                                    if (empData != null)
                                    {

                                    }
                                    else
                                    {
                                        string[] fullName = item.name.Split(' ');
                                        EmployeeDeserializedClass.Valid valid = item.Valid;
                                        EmployeeTB employee = new EmployeeTB();
                                        employee.AccessCardNo = item.employeeNo;
                                        employee.MachineID = item.employeeNo;
                                        employee.EmployeeNo = item.employeeNo;
                                        employee.Gender = item.gender;
                                        employee.DOJ = valid.beginTime;
                                        employee.DExpiryDate = valid.endTime;
                                        //employee.

                                        employee.FName = fullName[0];
                                        employee.Lname = fullName.Count() > 1 ? fullName[1] : "";

                                        employee.CurrentShiftId = shiftData != null ? shiftData.ShiftID : 1;

                                        employee.RelivingStatus = 0;
                                        employee.ManagerID = 0;
                                        if (desigData != null)
                                        {
                                            employee.CompanyId = desigData.CompanyId;// Convert.ToInt32(ddlCompany.SelectedValue);
                                            employee.TenantId = desigData.TenantId;// Convert.ToString(Session["TenantId"]);
                                            employee.DeptId = desigData.DeptID;
                                            employee.DesgId = desigData.DesigID;
                                        }
                                        employee.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                        employee.TenantId = Convert.ToString(Session["TenantId"]);
                                        employee.IsActive = true;

                                        db.EmployeeTBs.InsertOnSubmit(employee);
                                        db.SubmitChanges();
                                        counter++;
                                    }
                                }
                            }
                        }
                    }
                }


                if (counter > 0)
                {
                    gen.ShowMessage(this.Page, counter + " employees uploaded in system from device");
                }
                else
                {
                    gen.ShowMessage(this.Page, "Employees not uploaded");
                }
            }

        }
    }


        protected void btnDownloadEmployees_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListItem item in lstFruits.Items)
            {
                if (item.Selected)
                {
                    DownloadEmployeeData(item.Value);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Download Card

    private void DownloadCardData(string deviceId)
    {
        bool deviceStat = false;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            clienthttp clnt = new clienthttp();
            var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == deviceId).FirstOrDefault();
            MatchlistDevice device = new MatchlistDevice();
            string userName = string.IsNullOrEmpty(deviceData.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : deviceData.UserName;
            string password = string.IsNullOrEmpty(deviceData.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : deviceData.Password;

            if (deviceData != null)
            {
                string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                string devReq = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":5000}}";

                string devResp = string.Empty;
                string strDevMatchNum = string.Empty;

                int devR = clnt.HttpRequest(userName, password, deviceStatUrl, "POST", devReq, ref devResp);
                if (devR == (int)HttpStatus.Http200)
                {
                    DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(devResp);
                    strDevMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                    if ("0" != strDevMatchNum)
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

            if (deviceStat == false)
            {
                gen.ShowMessage(this.Page, "Device is offline, cant download device Cards..");
            }
            else if (device == null)
            {
                gen.ShowMessage(this.Page, "Device data not found..");
            }
            else if (deviceData != null)
            {
                string strMatchNum = string.Empty;
                int counter = 0;
                var devicedt = device;
                var empData = (from d in db.EmployeeTBs where d.IsActive == true && (d.IsAccessCardPresent == null || d.IsAccessCardPresent != true) && d.RelivingStatus == 0 && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select d).Distinct();

                string index = devicedt.devIndex;
                string Cardurl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/CardInfo/Search?format=json&devIndex=" + index;
                string strcardReq = "{\"CardInfoSearchCond\" : {\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":500,\"CardNoList\":[]}}";
                string strcardResp = string.Empty;

                clienthttp http = new clienthttp();

                int cardiet = http.HttpRequest(userName, password, Cardurl, "POST", strcardReq, ref strcardResp);
                if (cardiet == (int)HttpStatus.Http200)
                {
                    CardrRoot dr = JsonConvert.DeserializeObject<CardrRoot>(strcardResp);
                    strMatchNum = dr.CardInfoSearch.numOfMatche.ToString();
                   
                        int totMatches = dr.CardInfoSearch.totalMatches;

                    int rows = totMatches / 30;
                        for (int i = 0; i <= rows; i++)
                        {
                            int searchPoss = i * 30;

                            string strcardApiReq = "{\"CardInfoSearchCond\" : {\"searchID\":\"1\",\"searchResultPosition\":" + searchPoss + ",\"maxResults\":500,\"CardNoList\":[]}}";
                            string strcardApiResp = string.Empty;

                            clienthttp httpapi = new clienthttp();

                            int cardapiiet = http.HttpRequest(userName, password, Cardurl, "POST", strcardApiReq, ref strcardApiResp);
                            if (cardapiiet == (int)HttpStatus.Http200)
                            {
                                CardrRoot carddr = JsonConvert.DeserializeObject<CardrRoot>(strcardApiResp);

                                if (carddr.CardInfoSearch.responseStatusStrg == "OK" || carddr.CardInfoSearch.responseStatusStrg == "MORE")
                                {
                                    foreach (var item in empData)
                                    {
                                        var carddata = carddr.CardInfoSearch.CardInfo;
                                        foreach (var card in carddata)
                                        {
                                            if (item.EmployeeNo == card.employeeNo)
                                            {
                                                item.AccessCardNo = card.cardNo;
                                                item.IsAccessCardPresent = true;
                                                db.SubmitChanges();

                                                counter++;
                                            }
                                        }
                                    }
                                }
                            }
                        
                    }
                }
                if (counter > 0)
                {
                    gen.ShowMessage(this.Page, counter + " Cards downloaded..");
                }
            }
            else
            {
                gen.ShowMessage(this.Page, "No devices found of selected company..");
            }
        }
    }
        protected void btnDownloadEmployeesCard_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListItem item in lstFruits.Items)
            {
                if (item.Selected)
                {
                    DownloadCardData(item.Value);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Download Fingerprint

    private void DownloadFingerprintData(string deviceId)
    {
        bool deviceStat = false;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            clienthttp clnt = new clienthttp();
            var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == deviceId).FirstOrDefault();
            MatchlistDevice device = new MatchlistDevice();
            string userName = string.IsNullOrEmpty(deviceData.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : deviceData.UserName;
            string password = string.IsNullOrEmpty(deviceData.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : deviceData.Password;

            if (deviceData != null)
            {
                string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                string devReq = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":5000}}";

                string devResp = string.Empty;
                string strDevMatchNum = string.Empty;

                int devR = clnt.HttpRequest(userName, password, deviceStatUrl, "POST", devReq, ref devResp);
                if (devR == (int)HttpStatus.Http200)
                {
                    DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(devResp);
                    strDevMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                    if ("0" != strDevMatchNum)
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

            if (deviceStat == false)
            {
                gen.ShowMessage(this.Page, "Device is offline, cant download device FingerPrint..");
            }
            else if (device == null)
            {
                gen.ShowMessage(this.Page, "Device data not found..");
            }
            else if (deviceData != null)
            {
                int counter = 0;
                var devicedt = device;
                var empData = (from d in db.EmployeeTBs where d.IsActive == true && (d.IsFingerPrintPresent == null || d.IsFingerPrintPresent != true) && d.RelivingStatus == 0 && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select d).Distinct();

                string index = devicedt.devIndex;
                string fingerurl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/FingerPrintUpload?format=json&devIndex=" + index;

                foreach (var item in empData)
                {
                    string strFingerReq = "{\"FingerPrintCond\" : {\"searchID\":\"" + item.EmployeeNo + "\",\"employeeNo\": \"" + item.EmployeeNo + "\"}}";
                    string strFingerResp = string.Empty;
                    clienthttp http = new clienthttp();
                    int fingeriet = http.HttpRequest(userName, password, fingerurl, "POST", strFingerReq, ref strFingerResp);


                    if (fingeriet == (int)HttpStatus.Http200)
                    {
                        FingerRoot dr = JsonConvert.DeserializeObject<FingerRoot>(strFingerResp);

                        if (dr.FingerPrintInfo.status == "OK")
                        {
                            var empfingerinfo = db.EmployeeFingerprintTBs.Where(a => a.Employee_Id == item.EmployeeId).FirstOrDefault();
                            if (empfingerinfo == null)
                            {
                                EmployeeFingerprintTB empfingerdata = new EmployeeFingerprintTB();
                                empfingerdata.Employee_Id = item.EmployeeId;
                                empfingerdata.Finger_Id = dr.FingerPrintInfo.FingerPrintList[0].fingerPrintID;
                                empfingerdata.Company_Id = Convert.ToInt32(ddlCompany.SelectedValue);
                                empfingerdata.Finger_Data = dr.FingerPrintInfo.FingerPrintList[0].fingerData;

                                db.EmployeeFingerprintTBs.InsertOnSubmit(empfingerdata);
                                db.SubmitChanges();

                                item.IsFingerPrintPresent = true;
                                db.SubmitChanges();

                                counter++;
                            }
                        }

                    }
                }
                if (counter > 0)
                {
                    gen.ShowMessage(this.Page, counter + " Fingerprints downloaded..");
                }
            }
            else
            {
                gen.ShowMessage(this.Page, "No devices found of selected company..");
            }
        }
    }


        protected void btnDownloadEmployeesFingerprint_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListItem item in lstFruits.Items)
            {
                if (item.Selected)
                {
                    DownloadFingerprintData(item.Value);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    #region Download Face

    private void DownloadFaceData(string deviceId)
    {
        bool deviceStat = false;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            string folderPath = Server.MapPath("~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }


            clienthttp clnt = new clienthttp();
            var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == deviceId).FirstOrDefault();
            MatchlistDevice device = new MatchlistDevice();
            string userName = string.IsNullOrEmpty(deviceData.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : deviceData.UserName;
            string password = string.IsNullOrEmpty(deviceData.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : deviceData.Password;

            if (deviceData != null)
            {
                string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                string devReq = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":5000}}";

                string devResp = string.Empty;
                string strDevMatchNum = string.Empty;

                int devR = clnt.HttpRequest(userName, password, deviceStatUrl, "POST", devReq, ref devResp);
                if (devR == (int)HttpStatus.Http200)
                {
                    DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(devResp);
                    strDevMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                    if ("0" != strDevMatchNum)
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

            if (deviceStat == false)
            {
                gen.ShowMessage(this.Page, "Device is offline, cant download device Face..");
            }
            else if (device == null)
            {
                gen.ShowMessage(this.Page, "Device data not found..");
            }
            else if (deviceData != null)
            {
                int counter = 0;
                var devicedt = device;
                var empData = (from d in db.EmployeeTBs where d.IsActive == true && (d.IsFacePresent == null || d.IsFacePresent != true) && d.RelivingStatus == 0 && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select d).Distinct();

                string index = devicedt.devIndex;
                string faceurl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/Intelligent/FDLib/FDSearch?format=json&devIndex=" + index;
                clienthttp http = new clienthttp();
                foreach (var item in empData)
                {
                    string strFaceReq = "{\"FaceInfoSearchCond\" : {\"searchID\":\"" + item.EmployeeNo + "\",\"searchResultPosition\":1,\"maxResults\":10,\"employeeNo\": \"" + item.EmployeeNo + "\",\"faceLibType\": \"blackFD\"}}";
                    string strFaceResp = string.Empty;

                    int iet = http.HttpRequest(userName, password, faceurl, "POST", strFaceReq, ref strFaceResp);
                    if (iet == (int)HttpStatus.Http200)
                    {
                        FaceDownRoot dr = JsonConvert.DeserializeObject<FaceDownRoot>(strFaceResp);
                        string strMatchNum = Convert.ToString(dr.FaceInfoSearch.numOfMatches);
                        if ("0" != strMatchNum)
                        {
                            foreach (var fitem in dr.FaceInfoSearch.FaceInfo)
                            {
                                string fdurl = @"http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "" + fitem.faceURL;
                                string fdresp = string.Empty;
                                //litMessage.Text += "<br>" + fdurl;
                                string imgPath = folderPath + item.EmployeeId + "_" + item.FName.Trim() + "_" + item.Lname.Trim() + ".jpg";
                                int read = clnt.DownloadFile(userName, password, fdurl, "GET", imgPath);
                                Console.WriteLine("{0} bytes written", read);
                                string apiStatus = "failed";
                                if (iet == (int)HttpStatus.Http200)
                                {
                                    EmployeeTB employee = db.EmployeeTBs.Where(d => d.EmployeeId == item.EmployeeId).FirstOrDefault();
                                    employee.IsFacePresent = true;
                                    employee.Photo = item.EmployeeId + "_" + item.FName.Trim() + "_" + item.Lname.Trim() + ".jpg";
                                    db.SubmitChanges();

                                    counter++;
                                    apiStatus = "success";
                                }
                                Genreal.AuditApi(faceurl, "FDLib", "FDSearch", apiStatus, "device_download_persons_face", Session["DisplayName"].ToString(), "download employee face of " + item.EmployeeNo, ddlCompany.SelectedValue, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                            }
                        }
                    }
                }
                if (counter > 0)
                {
                    gen.ShowMessage(this.Page, counter + " Faces downloaded..");
                }
            }
            else
            {
                gen.ShowMessage(this.Page, "No devices found of selected company..");
            }
        }
    }

        protected void btnDownloadEmployeesFace_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListItem item in lstFruits.Items)
            {
                if (item.Selected)
                {
                    DownloadFaceData(item.Value);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    #region Download Password
    private void DownloadPasswordData(string deviceId)
    {
        bool deviceStat = false;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            clienthttp clnt = new clienthttp();
            var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == deviceId).FirstOrDefault();
            MatchlistDevice device = new MatchlistDevice();
            string userName = string.IsNullOrEmpty(deviceData.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : deviceData.UserName;
            string password = string.IsNullOrEmpty(deviceData.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : deviceData.Password;

            if (deviceData != null)
            {
                string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                string devReq = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":5000}}";

                string devResp = string.Empty;
                string strDevMatchNum = string.Empty;

                int devR = clnt.HttpRequest(userName, password, deviceStatUrl, "POST", devReq, ref devResp);
                if (devR == (int)HttpStatus.Http200)
                {
                    DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(devResp);
                    strDevMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                    if ("0" != strDevMatchNum)
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

            if (deviceStat == false)
            {
                gen.ShowMessage(this.Page, "Device is offline, cant download device Password..");
            }
            else if (device == null)
            {
                gen.ShowMessage(this.Page, "Device data not found..");
            }
            else if (deviceData != null)
            {
                int counter = 0;
                var devicedt = device;
                var empData = (from d in db.EmployeeTBs where d.IsActive == true && (d.Password == null || d.Password == "") && d.RelivingStatus == 0 && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select d).Distinct();

                string index = devicedt.devIndex;
                string Passwordurl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/UserInfo/Search?format=json&devIndex=" + index;
                string strpasswordReq = "{\"UserInfoSearchCond\" : {\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[]}}";
                string strpasswordResp = string.Empty;

                clienthttp http = new clienthttp();

                int passwordiet = http.HttpRequest(userName, password, Passwordurl, "POST", strpasswordReq, ref strpasswordResp);
                if (passwordiet == (int)HttpStatus.Http200)
                {
                    passwordRoot dr = JsonConvert.DeserializeObject<passwordRoot>(strpasswordResp);

                    if (dr.UserInfoSearch.responseStatusStrg == "OK")
                    {
                        foreach (var item in empData)
                        {
                            var passworddata = dr.UserInfoSearch.UserInfo;
                            foreach (var pass in passworddata)
                            {
                                if (item.EmployeeNo == pass.employeeNo)
                                {
                                    item.Password = pass.password;
                                    db.SubmitChanges();

                                    counter++;
                                }
                            }
                        }
                    }
                }
                if (counter > 0)
                {
                    gen.ShowMessage(this.Page, counter + " Passwords downloaded..");
                }
            }
            else
            {
                gen.ShowMessage(this.Page, "No devices found of selected company..");
            }
        }
    }



        protected void btnDownloadEmployeespassword_Click(object sender, EventArgs e)
        {
        try
        {
            foreach (ListItem item in lstFruits.Items)
            {
                if (item.Selected)
                {
                    DownloadPasswordData(item.Value);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Bind Drowpdowns
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
    //private void BindDeviceList()
    //{
    //    ddlDevice.Items.Clear();
    //    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
    //    {
    //        var data = (from d in db.DevicesTBs
    //                    where d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
    //                    select new { d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
    //        ddlDevice.DataSource = data;
    //        ddlDevice.DataTextField = "DeviceName";
    //        ddlDevice.DataValueField = "DeviceAccountId";
    //        ddlDevice.DataBind();
    //    }
    //    ddlDevice.Items.Insert(0, new ListItem("--Select--", "0"));
    //}

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

    #endregion

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