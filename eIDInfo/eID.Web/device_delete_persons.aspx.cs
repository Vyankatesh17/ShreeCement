using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class device_delete_persons : System.Web.UI.Page
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
                BindPagerList();
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
        BindJqFunctions();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDeviceList();
        BindDepartmentList();
        BindPagerList();
        // BindEmployeeList();
        // BindEmployeeGridView();
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

        }
        ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPagerList();
        BindEmployeeList();
        //BindEmployeeGridView();
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPagerList();
        //BindEmployeeGridView();
    }
    private void BindEmployeeList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        ddlEmployee.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && (d.RelivingStatus == null || d.RelivingStatus == 0) && d.DeptId != null
                        && d.CompanyId == cId & d.DeptId == dId
                        select new
                        {
                            d.EmployeeId,
                            EmpName = d.FName + " " + d.Lname
                        }).Distinct();

            ddlEmployee.DataSource = data;
            ddlEmployee.DataTextField = "EmpName";
            ddlEmployee.DataValueField = "EmployeeId";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, "--Select--");
        }
    }
    private void BindEmployeeGridView()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.IsActive == true && (d.RelivingStatus == null || d.RelivingStatus == 0)
                        select new
                        {
                            d.DeptId,
                            d.EmployeeId,
                            d.EmployeeNo,
                            EmpName = d.FName + " " + d.Lname,
                            d.Email,
                            d.Gender,
                            d.ContactNo,
                            IsFace = d.IsFacePresent.HasValue == true ? "Yes" : "No",
                            IsAccessCard = d.IsAccessCardPresent.HasValue == true ? "Yes" : "No"
                        }).Distinct();
            if (ddlDepartment.SelectedIndex > 0)
            {
                data = data.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)).Distinct();
            }
            if (ddlEmployee.SelectedIndex > 0)
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue)).Distinct();
            }
            gvEmployeeList.DataSource = data;
            gvEmployeeList.DataBind();

        }
    }
    private void GetDevicePersonsData()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            List<EventInfo> loglist = new List<EventInfo>();
            try
            {
                bool deviceStat = false;

                #region Device Status
                var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == "").FirstOrDefault();
                MatchlistDevice device = new MatchlistDevice();
                if (deviceData != null)
                {
                    string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                    string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":150}}";

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
                    int counter = 0;
                    string strMatchNum = string.Empty;
                    var devicedt = device;

                    string index = devicedt.devIndex;
                    string strReq = "{\"UserInfoSearchCond\" : {\"searchID\":\"0\",\"searchResultPosition\":0,\"maxResults\":500}}";
                    string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/UserInfo/Search?format=json&devIndex=" + index;
                    string strRsp = string.Empty;

                    clienthttp http = new clienthttp();
                    int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "POST", strReq, ref strRsp);
                        string apiStatus = "failed";
                    if (iRet == (int)HttpStatus.Http200)
                    {
                        RootUser drnew = JsonConvert.DeserializeObject<RootUser>(strRsp);
                        strMatchNum = drnew.UserInfoSearch.numOfMatches.ToString();
                        if ("0" != strMatchNum)
                        {
                            gvDataList.DataSource = drnew.UserInfoSearch.UserInfo;
                            gvDataList.DataBind();

                            foreach (var logitem in drnew.UserInfoSearch.UserInfo)
                            {
                                //var empData = db.EmployeeTBs.Where(d => d.MachineID == logitem.employeeNoString).FirstOrDefault();
                                //if (empData != null)
                                //{
                                //    DevicePunchesTB info = new DevicePunchesTB();
                                //    info.major = logitem.major;
                                //    info.minor = logitem.minor;
                                //    info.time = Convert.ToDateTime(logitem.time);
                                //    info.DeviceAccountId = deviceData.DeviceAccountId;
                                //    info.employeeNoString = logitem.employeeNoString != null ? logitem.employeeNoString : "";
                                //    info.cardNo = logitem.cardNo != null ? logitem.cardNo : "";
                                //    info.cardReaderNo = logitem.cardReaderNo != null ? logitem.cardReaderNo : "";
                                //    info.doorNo = logitem.doorNo != null ? logitem.doorNo : "";
                                //    info.currentVerifyMode = logitem.currentVerifyMode;
                                //    info.serialNo = logitem.serialNo;
                                //    info.type = logitem.type;
                                //    info.mask = logitem.mask;
                                //    info.name = logitem.name != null ? logitem.name : "";
                                //    info.userType = logitem.userType != null ? logitem.userType : "";
                                //    db.DevicePunchesTBs.InsertOnSubmit(info);
                                //    db.SubmitChanges();
                                //    DeviceLogsTB dlog = new DeviceLogsTB();
                                //    dlog.DeviceAccountId = Convert.ToInt32(deviceData.DeviceAccountId);
                                //    dlog.AttendDate = info.time;
                                //    dlog.EmpID = empData.EmployeeId;
                                //    dlog.AccessCardNo = info.cardNo;
                                //    dlog.ADate = Convert.ToDateTime(info.time);
                                //    TimeSpan timedt = Convert.ToDateTime(info.time).TimeOfDay;
                                //    dlog.ATime = timedt;
                                //    dlog.TenantId = Convert.ToString(Session["TenantId"]);
                                //    dlog.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                //    db.DeviceLogsTBs.InsertOnSubmit(dlog);
                                //    db.SubmitChanges();
                                //    EventInfo dt = new EventInfo();
                                //    dt.major = logitem.major;
                                //    dt.minor = logitem.minor;
                                //    dt.time = logitem.time;
                                //    dt.Atime = Convert.ToDateTime(logitem.time).ToString("hh:mm:ss");
                                //    dt.ADate = Convert.ToDateTime(logitem.time).ToString("dd/MM/yy");
                                //    dt.employeeNoString = logitem.employeeNoString != null ? logitem.employeeNoString : "";
                                //    dt.cardNo = logitem.cardNo != null ? logitem.cardNo : "";
                                //    dt.cardReaderNo = logitem.cardReaderNo != null ? logitem.cardReaderNo : "";
                                //    dt.doorNo = logitem.doorNo != null ? logitem.doorNo : "";
                                //    dt.currentVerifyMode = logitem.currentVerifyMode;
                                //    dt.serialNo = logitem.serialNo;
                                //    dt.type = logitem.type;
                                //    dt.mask = logitem.mask;
                                //    dt.name = logitem.name != null ? logitem.name : ""; ;
                                //    dt.userType = logitem.userType != null ? logitem.userType : "";
                                //    dt.CompanyName = ddlCompany.Text;
                                //    dt.DeviceAccountId = info.DeviceAccountId;
                                //    loglist.Add(dt);

                                //    counter++;
                                //}
                            }
                        }
                        apiStatus = "success";
                        //gen.ShowMessage(this.Page, "Log downloaded of " + counter + " employees..");
                    }
                    Genreal.AuditApi(strUrl, "RemoteControl", "Close door", apiStatus, "dashboard", Session["DisplayName"].ToString(), "open door command", ddlCompany.SelectedValue, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                }
                else
                {
                    gen.ShowMessage(this.Page, "No devices found of selected company..");
                }
            }
            catch (Exception ex) { Console.Write(ex.Message); }
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //GetDevicePersonsData();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        int counter = 0,dCounter=0;

        if (IsValid)
        {
            string list = "";
            foreach (ListItem item in lstFruits.Items)
            {
                if (item.Selected)
                {
                    list += item.Text + " " + item.Value + ",";
                    counter = 0;
                    foreach (GridViewRow gvr in gvEmployeeList.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                        {
                            Label lblEmpNo = (Label)gvr.FindControl("lblEmployeeNo");
                            Label lblEmpId = (Label)gvr.FindControl("lblEmpId");
                            //Label lblAccessCardNo = (Label)gvr.FindControl("lblAccessCardNo");
                            //Label lblFace = (Label)gvr.FindControl("lblFace");
                            //Label lblFingerprint = (Label)gvr.FindControl("lblFingerprint");
                            string empNo = lblEmpNo.Text;
                            string empId = lblEmpId.Text;
                            string empName = gvr.Cells[1].Text;
                            string userType = "";// gvr.Cells[3].Text;
                            AddDevicePersonsData(empName, empNo,empId, userType, item.Value);
                            //do something
                            counter++;
                        }
                    }
                    dCounter++;
                }
            }
            if (counter > 0)
            {
                BindPagerList();
                gen.ShowMessage(this.Page, counter + " employees delete in " + dCounter + " devices");
            }
        }

//        if (FileUpload1.PostedFile != null)
//        {
//            string excelPath = string.Concat(Server.MapPath("~/Attachments/EmployeeFiles/" + FileUpload1.PostedFile.FileName));
//            FileUpload1.SaveAs(excelPath);

//            string conString = string.Empty;
//            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
//            switch (extension)
//            {
//                case ".xls": //Excel 97-03
//                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
//                    break;
//                case ".xlsx": //Excel 07 or higher
//                    conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
//                    break;

//            }
//            conString = string.Format(conString, excelPath, "YES");

//            using (OleDbConnection excel_con = new OleDbConnection(conString))
//            {
//                excel_con.Open();
//                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
//                DataTable dtExcelData = new DataTable();


//                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] WHERE EmployeeNo IS NOT NULL", excel_con))
//                {
//                    oda.Fill(dtExcelData);
//                }
//                excel_con.Close();


//                #region Add Excel In Database
//                litErrors.Text = @"<thead><tr><th colspan='4'>Starting to import</th><tr>
//<tr><th>Name</th><th>Employee Code</th><th>User Type</th><th>Status</th></tr>
//</thead>
//<tbody>";
//                int counter = 0;
//                for (int i = 0; i < dtExcelData.Rows.Count; i++)
//                {
//                    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
//                    {
//                        string EmployeeNo = Convert.ToString(dtExcelData.Rows[i]["EmployeeNo"]);
//                        string EmployeeName = Convert.ToString(dtExcelData.Rows[i]["EmployeeName"]);
//                        string UserType = Convert.ToString(dtExcelData.Rows[i]["UserType"]);
//                        string DeviceAccountId = ddlDevice.SelectedValue;
                        
//                        try
//                        {
//                            AddDevicePersonsData(EmployeeName, EmployeeNo, UserType);
//                        }
//                        catch (Exception ex)
//                        {
//                            litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
//                                      EmployeeNo , EmployeeName, UserType, ex.Message);
//                        }
//                    }
//                }
//                if (counter > 0)
//                {
//                    gen.ShowMessage(this.Page, counter + " Employee imported successfully out of : " + dtExcelData.Rows.Count);
//                }
//                litErrors.Text += "</tbody>";
//                #endregion
//            }
//        }
    }

    private void AddDevicePersonsData(string empName,string empNo,string empId,string userType,string deviceId)
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

                        string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":150}}";

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

                    int counter = 0;
                    if (deviceStat == false)
                    {
                        gen.ShowMessage(this.Page, "Device is offline, cant delete employee..");
                    }
                    else if (device == null)
                    {
                        gen.ShowMessage(this.Page, "Device data not found..");
                    }
                    else if (deviceData != null)
                    {

                        string strMatchNum = string.Empty;
                        var devicedt = device;



                        DateTime dateStart = DateTime.Now;
                        DateTime dateEnd = dateStart.AddYears(10);

                        string beginTime = dateStart.Year + "-" + dateStart.Month.ToString("d2") + "-" + dateStart.Day.ToString("d2") + "T00:00:00";
                        string endTime = dateEnd.Year + "-" + dateEnd.Month.ToString("d2") + "-" + dateEnd.Day.ToString("d2") + "T23:00:00";

                        string index = devicedt.devIndex;
                        #region old code
                        /*
                         \"doorRight\": \"1\",\"RightPlan\" : [{\"doorNo\": 1,\"planTemplateNo\": \"1\"}]
                         */

                        //string strReq = "{\"UserInfo\" : [{\"employeeNo\":\"" + empNo + "\",\"name\":\"" + empName + "\",\"userType\":\"" + userType + "\",\"Valid\":{\"enable\":0,\"beginTime\": \"" + beginTime + "\",\"endTime\": \"" + endTime + "\",\"timeType\":\"local\"},\"doorRight\": \"1\",\"RightPlan\" : [{\"doorNo\": 1,\"planTemplateNo\": \"1\"}]}]}";
                        //string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/UserInfo/Record?format=json&devIndex=" + index;
                        //string strRsp = string.Empty;

                        //clienthttp http = new clienthttp();
                        //int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "POST", strReq, ref strRsp);
                        //if (iRet == (int)HttpStatus.Http200)
                        //{
                        //UserInfoCount drnew = JsonConvert.DeserializeObject<UserInfoCount>(strRsp);
                        //strMatchNum = drnew.UserInfoSearch.numOfMatches.ToString();
                        //if ("0" != strMatchNum)
                        //{
                        //    gvDataList.DataSource = drnew.UserInfoSearch.UserInfo;
                        //    gvDataList.DataBind();

                        //    foreach (var logitem in drnew.UserInfoSearch.UserInfo)
                        //    {
                        //        //var empData = db.EmployeeTBs.Where(d => d.MachineID == logitem.employeeNoString).FirstOrDefault();
                        //        //if (empData != null)
                        //        //{
                        //        //    DevicePunchesTB info = new DevicePunchesTB();
                        //        //    info.major = logitem.major;
                        //        //    info.minor = logitem.minor;
                        //        //    info.time = Convert.ToDateTime(logitem.time);
                        //        //    info.DeviceAccountId = deviceData.DeviceAccountId;
                        //        //    info.employeeNoString = logitem.employeeNoString != null ? logitem.employeeNoString : "";
                        //        //    info.cardNo = logitem.cardNo != null ? logitem.cardNo : "";
                        //        //    info.cardReaderNo = logitem.cardReaderNo != null ? logitem.cardReaderNo : "";
                        //        //    info.doorNo = logitem.doorNo != null ? logitem.doorNo : "";
                        //        //    info.currentVerifyMode = logitem.currentVerifyMode;
                        //        //    info.serialNo = logitem.serialNo;
                        //        //    info.type = logitem.type;
                        //        //    info.mask = logitem.mask;
                        //        //    info.name = logitem.name != null ? logitem.name : "";
                        //        //    info.userType = logitem.userType != null ? logitem.userType : "";
                        //        //    db.DevicePunchesTBs.InsertOnSubmit(info);
                        //        //    db.SubmitChanges();
                        //        //    DeviceLogsTB dlog = new DeviceLogsTB();
                        //        //    dlog.DeviceAccountId = Convert.ToInt32(deviceData.DeviceAccountId);
                        //        //    dlog.AttendDate = info.time;
                        //        //    dlog.EmpID = empData.EmployeeId;
                        //        //    dlog.AccessCardNo = info.cardNo;
                        //        //    dlog.ADate = Convert.ToDateTime(info.time);
                        //        //    TimeSpan timedt = Convert.ToDateTime(info.time).TimeOfDay;
                        //        //    dlog.ATime = timedt;
                        //        //    dlog.TenantId = Convert.ToString(Session["TenantId"]);
                        //        //    dlog.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        //        //    db.DeviceLogsTBs.InsertOnSubmit(dlog);
                        //        //    db.SubmitChanges();
                        //        //    EventInfo dt = new EventInfo();
                        //        //    dt.major = logitem.major;
                        //        //    dt.minor = logitem.minor;
                        //        //    dt.time = logitem.time;
                        //        //    dt.Atime = Convert.ToDateTime(logitem.time).ToString("hh:mm:ss");
                        //        //    dt.ADate = Convert.ToDateTime(logitem.time).ToString("dd/MM/yy");
                        //        //    dt.employeeNoString = logitem.employeeNoString != null ? logitem.employeeNoString : "";
                        //        //    dt.cardNo = logitem.cardNo != null ? logitem.cardNo : "";
                        //        //    dt.cardReaderNo = logitem.cardReaderNo != null ? logitem.cardReaderNo : "";
                        //        //    dt.doorNo = logitem.doorNo != null ? logitem.doorNo : "";
                        //        //    dt.currentVerifyMode = logitem.currentVerifyMode;
                        //        //    dt.serialNo = logitem.serialNo;
                        //        //    dt.type = logitem.type;
                        //        //    dt.mask = logitem.mask;
                        //        //    dt.name = logitem.name != null ? logitem.name : ""; ;
                        //        //    dt.userType = logitem.userType != null ? logitem.userType : "";
                        //        //    dt.CompanyName = ddlCompany.Text;
                        //        //    dt.DeviceAccountId = info.DeviceAccountId;
                        //        //    loglist.Add(dt);

                        //        //    counter++;
                        //        //}
                        //    }
                        //}

                        //gen.ShowMessage(this.Page, "Log downloaded of " + counter + " employees..");
                        //}
                        #endregion

                        #region New Code


                        string strName = empName;
                        string strUserType = "normal";

                        string strReq = "{\"UserInfoDetail\": {\"mode\": \"byEmployeeNo\",\"EmployeeNoList\": [{\"employeeNo\": \"" + empNo + "\"}]}}";
                        string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/UserInfoDetail/Delete?format=json&devIndex=" + index;
                        string strRsp = string.Empty;

                        clienthttp http = new clienthttp();
                        int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "PUT", strReq, ref strRsp);
                        string apiStatus = "failed";
                        if (iRet == (int)HttpStatus.Http200)
                        {
                            counter++;
                            apiStatus = "success";
                        }

                        #region delete Fingerprint

                        var fingerinfo = db.EmployeeFingerprintTBs.Where(a => a.Employee_Id == Convert.ToInt32(empId)).ToList();

                        string fingerno = "";
                        foreach (var item in fingerinfo)
                        {
                            fingerno += item.Finger_Id + ",";
                        }


                        string strfingerprintReq = "{\"FingerPrintDelete\": {\"EmployeeNoDetail\": {\"employeeNo\" :\"" + empNo + "\",\"fingerPrintID\": [" + fingerno + "]}}}";
                        string strfingerprintUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/FingerPrint/Delete?format=json&devIndex=" + index;
                        string strfingerprintRsp = string.Empty;


                        int ifingerprintRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strfingerprintUrl, "PUT", strfingerprintReq, ref strfingerprintRsp);

                        if (ifingerprintRet == (int)HttpStatus.Http200)
                        {
                            empData.IsFingerPrintPresent = false;
                            db.SubmitChanges();
                            apiStatus = "success";
                        }

                        #endregion

                        Genreal.AuditApi(strUrl, "UserInfoDetail", "Delete", apiStatus, "device_delete_persons", Session["DisplayName"].ToString(), "delete employee command for " + empNo, ddlCompany.SelectedValue, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                        #endregion
                        //gen.ShowMessage(this.Page, string.Format("Deleted {0} employees  from device", counter));
                    }
                    else
                    {
                        gen.ShowMessage(this.Page, "No devices found of selected company..");
                    }
                    #endregion
                }
                else if(deviceData.DeviceModel == "Others")
                {
                    #region SOAP API
                    clienthttp Soapclnt = new clienthttp();
                    //string IPAddress = "103.240.91.206";                            
                    string userna = "admin";
                    string userpwd = "Admin@1234";

                    //Calling CreateSOAPWebRequest method  
                    HttpWebRequest request = Soapclnt.CreateSOAPWebRequest("DeleteUser");
                    XmlDocument SOAPReqBody = new XmlDocument();
                    //SOAP Body Request  
                    SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>  
                        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                        <soap:Body>
                        <DeleteUser xmlns=""http://tempuri.org/"">                          
                          <EmployeeCode>" + empNo + @"</EmployeeCode>
                          <SerialNumber>" + deviceData.DeviceSerialNo + @"</SerialNumber>
                        <UserName>" + userna + @"</UserName>
                          <UserPassword>" + userpwd + @"</UserPassword>                           
                        </DeleteUser>                  
                      </soap:Body>
                    </soap:Envelope>");


                    using (Stream stream = request.GetRequestStream())
                    {
                        SOAPReqBody.Save(stream);
                    }
                    //Geting response from request  
                    using (WebResponse Serviceres = request.GetResponse())
                    {
                        using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                        {
                            //reading stream  
                            var ServiceResult = rd.ReadToEnd();

                        }
                    }
                    #endregion
                }

            }
            catch (Exception ex) { Console.Write(ex.Message); }
        }
    }


    protected void gvEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvEmployeeList.Rows.Count > 0)
        {
            gvEmployeeList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvEmployeeList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell FaceCell = e.Row.Cells[3];
            if (FaceCell.Text == "True")
            {
                FaceCell.Text = "Yes";
            }
            if (FaceCell.Text == "False" || FaceCell.Text == "&nbsp;")
            {
                FaceCell.Text = "No";
            }


            TableCell cardCell = e.Row.Cells[4];
            if (cardCell.Text == "True")
            {
                cardCell.Text = "Yes";
            }
            if (cardCell.Text == "False" || cardCell.Text == "&nbsp;")
            {
                cardCell.Text = "No";
            }

            TableCell fingerprintCell = e.Row.Cells[5];
            if (fingerprintCell.Text == "True")
            {
                fingerprintCell.Text = "Yes";
            }
            if (fingerprintCell.Text == "False" || fingerprintCell.Text == "&nbsp;")
            {
                fingerprintCell.Text = "No";
            }
        }
    }

    private void PopulatePager(Int32 TotalRecords, Int32 PageNumber)
    {
        Int32 intIndex;
        Int32 intPageNumber;

        intPageNumber = 1;

        double dblPageCount = (double)((decimal)TotalRecords / 1000);
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", PageNumber > 1));
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != PageNumber));
            }
            pages.Add(new ListItem("Last", pageCount.ToString(), PageNumber < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();


    }




    public void GetPageData(int pageNo)
    {

        string query = string.Format(@"SELECT    E.FName, E.Lname, E.EmployeeId, E.BirthDate, E.Email, E.DOJ AS DOJ1,E.AccessCardNo, E.IsAccessCardPresent As IsAccessCard, E.Gender, E.IsFacePresent As IsFace, E.PanNo, E.ContactNo, E.PassportNo, D.DeptName, C.CompanyName, E.FName + ' ' + E.Lname AS EmpName, E.RelivingDate, E.EmployeeNo, 
                         E.personalEmail, E.CompanyId, E.DeptId, E.DesgId, E.MachineID, E.DExpiryDate As ExDate,  E.IsFingerPrintPresent,E.RelivingStatus
FROM            EmployeeTB AS E INNER JOIN
                         MasterDeptTB AS D ON E.DeptId = D.DeptID INNER JOIN
                         CompanyInfoTB AS C ON E.CompanyId = C.CompanyId WHERE E.IsActive=1  AND C.TenantId='{0}'", Convert.ToString(Session["TenantId"]));

        if (ddlCompany.SelectedIndex > 0)
        {
            query += " AND C.CompanyId=" + ddlCompany.SelectedValue;
        }
        if (ddlDepartment.SelectedIndex > 0)
        {
            query += " AND D.DeptId=" + ddlDepartment.SelectedValue;
        }
        if (ddlEmployee.SelectedIndex > 0)
        {
            query += " AND E.EmployeeId=" + ddlEmployee.SelectedValue;
        }
        if (ddlrelivingstatus.SelectedIndex > 0)
        {
            query += " AND E.RelivingStatus=" + ddlrelivingstatus.SelectedValue;
        }
        int companyid = Convert.ToInt32(Session["CompanyID"]);
        if (Session["UserType"].ToString() == "LocationAdmin")
        {
            query += " AND C.CompanyId=" + companyid;
        }


        //if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
        //{
        //    query += " AND E.EmployeeNo LIKE '%" + txtEmployeeCode.Text + "%'";
        //}

        query += string.Format(@" ORDER BY E.EmployeeId OFFSET        (({0} - 1) * 1000) ROWS FETCH NEXT 1000 ROWS ONLY", pageNo);




        DataTable dataTable = gen.ReturnData(query);

        gvEmployeeList.DataSource = dataTable;
        gvEmployeeList.DataBind();

        lblPageNo.Text = pageNo.ToString();
    }

    public void BindPagerList()
    {
        string query = "select count(employeeid) from employeetb where isactive=1";
        DataTable data = gen.ReturnData(query);
        int recordCount = data.Rows.Count > 0 ? Convert.ToInt32(data.Rows[0][0]) : 0;
        GetPageData(1);
        this.PopulatePager(recordCount, 1);
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetPageData(pageIndex);

    }



    protected void ddlrelivingstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPagerList();
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
        BindPagerList();
    }











}