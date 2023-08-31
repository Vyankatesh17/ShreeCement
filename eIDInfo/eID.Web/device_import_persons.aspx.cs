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

public partial class device_import_persons : System.Web.UI.Page
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
        //BindEmployeeList();
        //BindEmployeeGridView();
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
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.IsActive == true && (d.RelivingStatus == null || d.RelivingStatus == 0)
                        select new
                        {
                            d.DeptId,
                            d.EmployeeId,
                            d.EmployeeNo,
                            EmpName = d.FName + " " + d.Lname,
                            d.AccessCardNo,
                            d.Email,
                            d.Gender,
                            d.ContactNo,
                            d.IsFacePresent,
                            IsFace = d.IsFacePresent.HasValue == true ? "Yes" : "No",
                            IsAccessCard = d.IsAccessCardPresent.HasValue == true ? "Yes" : "No",
                            ExDate = d.DExpiryDate.HasValue ? d.DExpiryDate.Value : DateTime.Now.AddYears(10)
                        }).Distinct();

            if (ddlDepartment.SelectedIndex > 0)
            {
                data = data.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)).Distinct();
            }
            if (ddlEmployee.SelectedIndex > 0)
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue)).Distinct();
            }
            if (ddlFace.SelectedIndex > 0)
            {
                string face = ddlFace.SelectedIndex == 1 ? "Yes" : "No";
                data = data.Where(d => d.IsFace == face).Distinct();
            }

            if(ddlGender.SelectedIndex > 0)
            {
                string gender = ddlGender.SelectedIndex == 1 ? "Male" : "FeMale";
                data = data.Where(d => d.Gender == gender).Distinct();
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

                        //gen.ShowMessage(this.Page, "Log downloaded of " + counter + " employees..");
                    }
                   
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
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int counter = 0, dCounter = 0;
            db.CommandTimeout = 8 * 60;
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
                                Label lblAccessCardNo = (Label)gvr.FindControl("lblAccessCardNo");
                                Label lblEmpId = (Label)gvr.FindControl("lblEmpId");
                                Label lblExDate = (Label)gvr.FindControl("lblExDate");
                                Label lblFace = (Label)gvr.FindControl("lblFace");
                                Label lblFingerprint = (Label)gvr.FindControl("lblFingerprint");
                                string empNo = lblEmpNo.Text;
                                string empId = lblEmpId.Text;
                                string empName = gvr.Cells[1].Text;
                                string exDate = lblExDate.Text;
                                string userType = "";// gvr.Cells[3].Text;
                                AddDevicePersonsData(empName, empNo, empId, userType, item.Value, exDate, lblAccessCardNo.Text, lblFace.Text, lblFingerprint.Text);
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
                    gen.ShowMessage(this.Page, counter + " employees import in " + dCounter + " devices");
                }
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

    private void AddDevicePersonsData(string empName,string empNo,string empId,string userType,string deviceId,string exDate,string accessCardNo, string Face, string fingerprint)
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



                        DateTime dateStart = DateTime.Now;
                        DateTime dateEnd = string.IsNullOrEmpty(exDate) ? dateStart.AddYears(10) : Convert.ToDateTime(exDate);

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

                        string strReq = "{\"UserInfo\" : [{\"employeeNo\": \"" + empNo + "\",\"name\": \"" + strName + "\",\"userType\": \"" + strUserType + "\",\"Valid\" : {\"enable\": true,\"beginTime\": \"" + beginTime + "\",\"endTime\": \"" + endTime + "\",\"timeType\" : \"local\"}, \"password\":\"123456\"}]}";
                        string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/UserInfo/Record?format=json&devIndex=" + index;
                        string strRsp = string.Empty;

                        clienthttp http = new clienthttp();
                        int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "POST", strReq, ref strRsp);
                        string apiStatus = "failed";
                        if (iRet == (int)HttpStatus.Http200)
                        {
                            apiStatus = "success";
                            //ApiMonitorTB at = new ApiMonitorTB();
                            //at.Action = "Add Employee";
                            //at.Page = "Employee";
                            //at.time = DateTime.Now;
                            //db.ApiMonitorTBs.Add(at);
                            //db.SaveChanges();
                            //for add card no

                            strReq = "{\"CardInfo\":{\"employeeNo\":\"" + empNo + "\",\"cardNo\":\"" + accessCardNo + "\",\"cardType\":\"normalCard\"}}";
                            strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/CardInfo/Record?format=json&devIndex=" + index;
                            strRsp = string.Empty;

                            http = new clienthttp();
                            iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "POST", strReq, ref strRsp);

                            if (iRet == (int)HttpStatus.Http200)
                            {
                            }

                            #region Upload Face
                            if (Face == "True")
                            {
                                string folderPath = Server.MapPath("~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/");
                                string filePath = System.IO.Path.Combine(folderPath, empData.EmployeeId + "_" + empData.FName.Trim() + "_" + empData.Lname.Trim() + ".jpg");
                                string strEmployeeID = empNo;
                                strReq = "{ \"FaceInfo\": {\"employeeNo\": \"" + strEmployeeID + "\",\"faceLibType\": \"blackFD\" }}";
                                strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/Intelligent/FDLib/FaceDataRecord?format=json&devIndex=" + index;
                                strRsp = string.Empty;

                                string fileKeyName = "FaceImage";
                                NameValueCollection stringDict = new NameValueCollection();
                                stringDict.Add("FaceDataRecord", strReq);

                                http = new clienthttp();
                                iRet = http.HttpPostData(deviceData.UserName, deviceData.Password, strUrl, fileKeyName, filePath, stringDict, ref strRsp);
                                if (iRet != (int)HttpStatus.Http200)
                                {
                                    //at = new ApiMonitorTB();
                                    //at.Action = "Upload Face";
                                    //at.Page = "Employee";
                                    //at.time = DateTime.Now;
                                    //db.ApiMonitorTBs.Add(at);
                                    //db.SaveChanges();
                                }
                            }
                            #endregion

                            #region Upload Fingerprint
                            if (fingerprint == "True")
                            {
                                string folderPath = Server.MapPath("~/EmployeeImages/Fingerprint/" + ddlCompany.SelectedItem.Text + "/");
                                string filePath = System.IO.Path.Combine(folderPath, empData.EmployeeId + "_" + empData.FName.Trim() + "_" + empData.Lname.Trim() + ".jpg");
                                string strEmployeeID = empNo;

                                var fingerinfo = db.EmployeeFingerprintTBs.Where(a => a.Employee_Id == Convert.ToInt32(empId)).ToList();

                                foreach (var item in fingerinfo)
                                {
                                    strReq = "{ \"FingerPrintCfg\": {\"employeeNo\": \"" + strEmployeeID + "\",\"fingerPrintID\": " + item.Finger_Id + ",\"fingerData\": \"" + item.Finger_Data + "\" }}";
                                    strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/FingerPrintDownload?format=json&devIndex=" + index;
                                    strRsp = string.Empty;

                                    http = new clienthttp();

                                    iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "POST", strReq, ref strRsp);
                                    if (iRet == (int)HttpStatus.Http200)
                                    {

                                    }
                                }
                            }
                            #endregion


                        }
                        Genreal.AuditApi(strUrl, "UserInfo", "Record", apiStatus, "device_import_persons", Session["DisplayName"].ToString(), "import employee " + empNo, ddlCompany.SelectedValue, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                        #endregion

                    }
                    else
                    {
                        gen.ShowMessage(this.Page, "No devices found of selected company..");
                    }
                    #endregion
                }
                else if (deviceData.DeviceModel == "Others")
                {
                    #region SOAP API                  

                    clienthttp Soapclnt = new clienthttp();
                    string userna = "admin";
                    string userpwd = "Admin@1234";

                    //Calling CreateSOAPWebRequest method  
                    HttpWebRequest request = Soapclnt.CreateSOAPWebRequest("AddEmployee");
                    XmlDocument SOAPReqBody = new XmlDocument();
                    //SOAP Body Request  
                    SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>  
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                            <soap:Body>
                            <AddEmployee xmlns=""http://tempuri.org/"">                         
                              <EmployeeCode>" + empNo + @"</EmployeeCode>
                              <EmployeeName>" + empName + @"</EmployeeName>
                              <CardNumber>" + accessCardNo + @"</CardNumber>
                              <SerialNumber>" + deviceData.DeviceSerialNo + @"</SerialNumber>
                              <UserName>" + userna + @"</UserName>
                              <UserPassword>" + userpwd + @"</UserPassword>                         
                            </AddEmployee>
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
                            var ServiceResult = rd.ReadToEnd();
                        }
                    }
                    #endregion

                    #region Fingerprint
                    if (fingerprint == "True")
                    {
                        using (EliteDataDataContext elite = new EliteDataDataContext())
                        {
                            var empdata = elite.Employees.Where(a => a.EmployeeCode == empNo).FirstOrDefault();
                            if (empdata != null)
                            {
                                var employeebio = elite.EmployeesBios.Where(a => a.EmployeeId == empdata.EmployeeId).ToList();
                                if (employeebio != null)
                                {
                                    foreach (var item in employeebio)
                                    {
                                        DeviceCommand devicecommand = new DeviceCommand();
                                        devicecommand.Title = "Add User " + empNo + " Fingerprint " + item.BioId;
                                        devicecommand.DeviceCommand1 = "C:UniqueId:DATA FP PIN=" + empNo + " FID=" + item.BioId + " Valid=1 Size=1504 TMP=" + item.Bio;
                                        devicecommand.SerialNumber = deviceData.DeviceSerialNo;
                                        devicecommand.Status = "Success";
                                        devicecommand.Type = "Add User Fingerprint";
                                        devicecommand.CreationDate = DateTime.Now;
                                        devicecommand.ExecutionDate = DateTime.Now;

                                        elite.DeviceCommands.InsertOnSubmit(devicecommand);
                                    }
                                }
                            }
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
            TableCell FaceCell = e.Row.Cells[4];         
            if (FaceCell.Text == "True")
            {
                FaceCell.Text = "Yes";
            }
            if (FaceCell.Text == "False" || FaceCell.Text == "&nbsp;")
            {
                FaceCell.Text = "No";
            }


            TableCell cardCell = e.Row.Cells[5];
            if (cardCell.Text == "True")
            {
                cardCell.Text = "Yes";
            }
            if (cardCell.Text == "False" || cardCell.Text == "&nbsp;")
            {
                cardCell.Text = "No";
            }

            TableCell fingerprintCell = e.Row.Cells[6];
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

    protected void ddlFace_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPagerList();
        //BindEmployeeGridView();
    }

    protected void ddlCard_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BindPagerList();
    }

    protected void ddlFingerprint_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPagerList();
    }

    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPagerList();
        //BindEmployeeGridView();
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
                         E.personalEmail, E.CompanyId, E.DeptId, E.DesgId, E.MachineID, E.DExpiryDate As ExDate, E.IsFingerPrintPresent, E.RelivingDate
FROM            EmployeeTB AS E INNER JOIN
                         MasterDeptTB AS D ON E.DeptId = D.DeptID INNER JOIN
                         CompanyInfoTB AS C ON E.CompanyId = C.CompanyId WHERE E.IsActive=1 AND E.RelivingStatus = 0 AND C.TenantId='{0}'", Convert.ToString(Session["TenantId"]));

       

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
        if (ddlFace.SelectedIndex > 0)
        {
            //int face = ddlFace.SelectedValue == "Yes" ? 1 : 0;
            //query += " AND E.IsFacePresent=" + face;

            if (ddlFace.SelectedIndex == 1)
            {
                query += " AND E.IsFacePresent=1";
            }
            else
            {
                query += " AND (E.IsFacePresent Is NULL or E.IsFacePresent=0)";
            }
        }
        if (ddlCard.SelectedIndex > 0)
        {
            if(ddlCard.SelectedIndex == 1)
            {
                query += " AND E.IsAccessCardPresent=1";
            }
            else
            {
                query += " AND (E.IsAccessCardPresent Is NULL or E.IsAccessCardPresent=0)";
            }           
        }
        if (ddlFingerprint.SelectedIndex > 0)
        {
            if (ddlFingerprint.SelectedIndex == 1)
            {
                query += " AND E.IsFingerPrintPresent=1";
            }
            else
            {
                query += " AND (E.IsFingerPrintPresent Is NULL or E.IsFingerPrintPresent=0)";
            }
        }
        if (ddlGender.SelectedIndex > 0)
        {
            string gender = ddlGender.SelectedValue == "FeMale" ? "Female" : "male";
            query += string.Format(" AND E.Gender='{0}'", Convert.ToString(gender));           
        }

        int companyid = Convert.ToInt32(Session["CompanyID"]);
        if (Session["UserType"].ToString() == "LocationAdmin")
        {
            query += " AND C.CompanyId=" + companyid;
        }


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


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int counter = 0, dCounter = 0;
            db.CommandTimeout = 8 * 60;
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
                                Label lblAccessCardNo = (Label)gvr.FindControl("lblAccessCardNo");
                                Label lblEmpId = (Label)gvr.FindControl("lblEmpId");
                                Label lblExDate = (Label)gvr.FindControl("lblExDate");
                                Label lblFace = (Label)gvr.FindControl("lblFace");
                                Label lblFingerprint = (Label)gvr.FindControl("lblFingerprint");
                                string empNo = lblEmpNo.Text;
                                string empId = lblEmpId.Text;
                                string empName = gvr.Cells[1].Text;
                                string exDate = lblExDate.Text;
                                UpdateDevicePersonsData(empName, empNo, empId, item.Value, exDate);
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
                    gen.ShowMessage(this.Page, counter + " employees Update in " + dCounter + " devices");
                }
            }
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