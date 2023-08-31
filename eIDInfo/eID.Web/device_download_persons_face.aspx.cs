using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class device_download_persons_face : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindDeviceList();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/EmployeeImages/Faces/");

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        clienthttp clnt = new clienthttp();

        string userName =  ConfigurationManager.AppSettings["DeviceUserName"];
        string password = ConfigurationManager.AppSettings["DevicePassword"];
       
        string faceurl = "http://103.240.91.206:9099/ISAPI/Intelligent/FDLib/FDSearch?format=json&devIndex=7D657AA1-C6DE-4572-B7EB-2BF544465B0D";
        string strReq = "{\"FaceInfoSearchCond\" : {\"searchID\":\"123\",\"searchResultPosition\":1,\"maxResults\":500,\"employeeNo\": \"999\",\"faceLibType\": \"blackFD\"}}";
        string strRsp = string.Empty;

        int iet = clnt.HttpRequest(userName, password, faceurl, "POST", strReq, ref strRsp);

        if (iet == (int)HttpStatus.Http200)
        {
            FaceDownRoot dr = JsonConvert.DeserializeObject<FaceDownRoot>(strRsp);
            string strMatchNum = Convert.ToString(dr.FaceInfoSearch.numOfMatches);
            if ("0" != strMatchNum)
            {

                foreach (var item in dr.FaceInfoSearch.FaceInfo)
                {

                    string fdurl = @"http://103.240.91.206:9099" + item.faceURL;
                    string fdresp = string.Empty;

                    int read = clnt.DownloadFile(userName, password, fdurl, "GET", 
                        "E:test.jpg");
                    Console.WriteLine("{0} bytes written", read);
                    int df = clnt.HttpRequest(userName, password, fdurl, "GET", "fdimage.jpeg", ref fdresp);
                    if (df == (int)HttpStatus.Http200) { }
                }
            }
            //at = new ApiMonitorTB();
            //at.Action = "Upload Face";
            //at.Page = "Employee";
            //at.time = DateTime.Now;
            //db.ApiMonitorTBs.Add(at);
            //db.SaveChanges();
        }
    }


    protected void btnDownloadFace_Click(object sender, EventArgs e)
    {
        try
        {
            litMessage.Text = "";
            bool deviceStat = false;
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                string folderPath = Server.MapPath("~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text+"/");

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }

                clienthttp clnt = new clienthttp();
                var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == ddlDevice.SelectedValue).FirstOrDefault();
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
                    gen.ShowMessage(this.Page, "Device is offline, cant download device log..");
                }
                else if (device == null)
                {
                    gen.ShowMessage(this.Page, "Device data not found..");
                }
                else if (deviceData != null)
                {
                    int counter = 0;
                    var devicedt = device;
                    var empData = (from d in db.EmployeeTBs where d.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select d).Distinct();

                    string index = devicedt.devIndex;
                    string faceurl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/Intelligent/FDLib/FDSearch?format=json&devIndex=" + index;

                    string fingerurl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/FingerPrintUpload?format=json&devIndex=" + index;
                    clienthttp http = new clienthttp();
                    foreach (var item in empData)
                    {
                        if(item.IsFacePresent == null || item.IsFacePresent != true)
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
                                        litMessage.Text += "<br>" + fdurl;
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

                        if (item.IsFingerPrintPresent == null || item.IsFingerPrintPresent != true)
                        {
                            string strFingerReq = "{\"FingerPrintCond\" : {\"searchID\":\"" + item.EmployeeNo + "\",\"employeeNo\": \"" + item.EmployeeNo + "\"}}";
                            string strFingerResp = string.Empty;

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
                                    }
                                }

                            }
                        }


                    }
                    if (counter > 0)
                    {
                        gen.ShowMessage(this.Page, counter + " faces downloaded..");
                    }
                }
                else
                {
                    gen.ShowMessage(this.Page, "No devices found of selected company..");
                }
            }
        }
        catch(Exception ex)
        {
            litMessage.Text += ex.Message;
        }
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
        ddlDevice.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int cId = ddlCompany.SelectedIndex>0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            var data = (from d in db.DevicesTBs
                        where d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
            ddlDevice.DataSource = data;
            ddlDevice.DataTextField = "DeviceName";
            ddlDevice.DataValueField = "DeviceAccountId";
            ddlDevice.DataBind();
        }
        ddlDevice.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDeviceList();
    }
}