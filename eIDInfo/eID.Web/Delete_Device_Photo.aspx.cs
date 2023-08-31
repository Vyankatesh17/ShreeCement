using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Delete_Device_Photo : System.Web.UI.Page
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
        BindDepartmentList();
        BindPagerList();
        //BindEmployeeList();
        //BindEmployeeGridView();
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
                            where  d.TenantId == Convert.ToString(Session["TenantId"])
                            select new { d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
                lstFruits.DataSource = data;
                lstFruits.DataTextField = "DeviceName";
                lstFruits.DataValueField = "DeviceAccountId";
                lstFruits.DataBind();
            }
        }

        BindPagerList();
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
            //if (ddlEmployee.SelectedIndex > 0)
            //{
            //    data = data.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue)).Distinct();
            //}
            //if (ddlFace.SelectedIndex > 0)
            //{
            //    string face = ddlFace.SelectedIndex == 1 ? "Yes" : "No";
            //    data = data.Where(d => d.IsFace == face).Distinct();
            //}

            //if (ddlGender.SelectedIndex > 0)
            //{
            //    string gender = ddlGender.SelectedIndex == 1 ? "Male" : "FeMale";
            //    data = data.Where(d => d.Gender == gender).Distinct();
            //}
            gvEmployeeList.DataSource = data;
            gvEmployeeList.DataBind();

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
            if (FaceCell.Text == "False")
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

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPagerList();
        //BindEmployeeGridView();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        int counter = 0, dCounter = 0;

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
                            string empNo = lblEmpNo.Text;
                            string empId = lblEmpId.Text;
                            string empName = gvr.Cells[1].Text;
                            string exDate = lblExDate.Text;
                            string userType = "";// gvr.Cells[3].Text;
                            DeleteDevicePhotosData(empName, empNo, empId, userType, item.Value, exDate, lblAccessCardNo.Text, lblFace.Text);
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
                gen.ShowMessage(this.Page, counter + " Delete employees Photo in " + dCounter + " devices");
            }
        }

    }


    private void DeleteDevicePhotosData(string empName, string empNo, string empId, string userType, string deviceId, string exDate, string accessCardNo, string Face)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var empData = db.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(empId)).FirstOrDefault();

            //List<EventInfo> loglist = new List<EventInfo>();
            try
            {
                bool deviceStat = false;

                #region Device Status
                var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == deviceId).FirstOrDefault();
                MatchlistDevice device = new MatchlistDevice();
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


                    #region New Code


                    string strName = empName;
                    string strUserType = "normal";

                    string strReq = "{\"UserInfo\" : [{\"employeeNo\": \"" + empNo + "\",\"name\": \"" + strName + "\",\"userType\": \"" + strUserType + "\",\"Valid\" : {\"enable\": true,\"beginTime\": \"" + beginTime + "\",\"endTime\": \"" + endTime + "\",\"timeType\" : \"local\"}}]}";
                    string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/UserInfo/Record?format=json&devIndex=" + index;
                    string strRsp = string.Empty;

                    clienthttp http = new clienthttp();
                    int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "POST", strReq, ref strRsp);
                    string apiStatus = "failed";
                    if (iRet == (int)HttpStatus.Http200)
                    {
                        apiStatus = "success";                       

                        strReq = "{\"CardInfo\":{\"employeeNo\":\"" + empNo + "\",\"cardNo\":\"" + accessCardNo + "\",\"cardType\":\"normalCard\"}}";
                        strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/CardInfo/Record?format=json&devIndex=" + index;
                        strRsp = string.Empty;

                        http = new clienthttp();
                        iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "POST", strReq, ref strRsp);

                        if (iRet == (int)HttpStatus.Http200)
                        {
                        }

                        #region Delete Face

                        if (Face == "Yes")
                        {
                            string folderPath = Server.MapPath("~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/");
                            string filePath = System.IO.Path.Combine(folderPath, empData.EmployeeId + "_" + empData.FName.Trim() + "_" + empData.Lname.Trim() + ".jpg");
                            string strEmployeeID = empNo;                           
                            strReq = "{ \"FaceInfoDelCond\": { \"EmployeeNoList\": [{\"employeeNo\": \"" + strEmployeeID + "\"}]}}";

                            strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/Intelligent/FDLib/FDSearch/Delete?format=json&devIndex=" + index;                           
                            strRsp = string.Empty;

                            http = new clienthttp();
                            iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "PUT", strReq, ref strRsp);
                            apiStatus = "failed";
                            if (iRet == (int)HttpStatus.Http200)
                            {                               
                                apiStatus = "success";
                                File.Delete(Server.MapPath("~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/" + empData.Photo));

                                empData.Photo = null;
                                empData.IsFacePresent = false;
                                db.SubmitChanges();

                            }

                          
                        }
                        #endregion
                    }
                    Genreal.AuditApi(strUrl, "UserInfo", "Record", apiStatus, "Delete_Device_Photo", Session["DisplayName"].ToString(), "import employee " + empNo, ddlCompany.SelectedValue, Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                    #endregion

                }
                else
                {
                    gen.ShowMessage(this.Page, "No devices found of selected company..");
                }
            }
            catch (Exception ex) { Console.Write(ex.Message); }
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
                         E.personalEmail, E.CompanyId, E.DeptId, E.DesgId, E.MachineID, E.DExpiryDate As ExDate,  E.IsFingerPrintPresent
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



}