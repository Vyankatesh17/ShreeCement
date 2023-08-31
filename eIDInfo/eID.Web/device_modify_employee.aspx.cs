using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class device_modify_employee : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["empId"])))
            {
                txtEmpName.Attributes.Add("readonly", "readonly");
                txtEmployeeNo.Attributes.Add("readonly", "readonly");
                GetEmployeeDetails();
            }
            else
            {
                Response.Redirect("mst_employee_list.aspx");
            }
        }
        BindJqFunctions();
    }
    /*
     * api :  http://103.240.91.206:9099/ISAPI/AccessControl/UserInfo/Modify?format=json&devIndex=<uuid>
     * 
     {"UserInfo": {"employeeNo": "123456","name": "test","Valid": {"beginTime": "2017-08-01T17:30:08","endTime": "2027-08-01T17:30:08"}}}
     */

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string ip=string.Empty, acid=string.Empty, srno=string.Empty;
        string strUrl = string.Empty;string apiStatus = string.Empty;
        try {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                DateTime start = Convert.ToDateTime(txtStartTime.Text);
                DateTime end = Convert.ToDateTime(txtEndTime.Text);
                DateTime startT = Convert.ToDateTime(txtStartT.Text);
                DateTime endT = Convert.ToDateTime(txtEndT.Text);
                String stime = startT.ToString("HH:mm:ss");
                String etime = endT.ToString("HH:mm:ss");
                string beginTime1 = start.Year + "-" + start.Month.ToString("d2") + "-" + start.Day.ToString("d2") + "T" + stime;
                string startTime1 = txtStartT.Text;
                string endTime1 = end.Year + "-" + end.Month.ToString("d2") + "-" + end.Day.ToString("d2") + "T" + etime;
                #region Device Status
                int counter = 0;
                foreach (ListItem item in lstFruits.Items)
                {
                    if (item.Selected)
                    {
                        bool deviceStat = false;
                        var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == item.Value).FirstOrDefault();
                        ip = deviceData.IPAddress;
                        acid = deviceData.DeviceAccountId;
                        srno = deviceData.DeviceSerialNo;
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
                                    foreach (var ditem in dr.SearchResult.MatchList)
                                    {
                                        if (ditem.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId)
                                        {
                                            device = ditem.Device;
                                            #region Modify Employee Time
                                            string strName = txtEmpName.Text;
                                            string employeeNo = hfDeviceCode.Value;
                                            string beginTime = beginTime1;// txtStartTime.Text;
                                            string endTime = endTime1;// txtEndTime.Text;

                                            
                                            string index = device.devIndex;

                                            string strReq = "{\"UserInfo\" : {\"name\": \"" + strName + "\",\"employeeNo\": \"" + employeeNo + "\",\"Valid\" : {\"beginTime\": \"" + beginTime + "\",\"endTime\": \"" + endTime + "\"}}}";

                                            strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/UserInfo/Modify?format=json&devIndex=" + index;
                                            string strRsp = string.Empty;

                                            clienthttp http = new clienthttp();
                                            int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "PUT", strReq, ref strRsp);
                                             apiStatus = "failed";

                                            if (iRet == (int)HttpStatus.Http200)
                                            {
                                                apiStatus = "success";
                                                counter++;

                                                EmployeeTB employee = db.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(hfEmpId.Value)).FirstOrDefault();
                                                employee.DExpiryDate = end;
                                                db.SubmitChanges();
                                            }
                                            Genreal.AuditApi(strUrl, "UserInfo", "Record_Modify", apiStatus, "device_modify_employees", txtEmpName.Text, "modify employee " + txtEmployeeNo.Text, "", Session["TenantId"].ToString(), deviceData.IPAddress, deviceData.DeviceSerialNo, deviceData.DeviceAccountId);
                                            #endregion

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (counter > 0)
                {
                    gen.ShowMessage(this.Page, "Employee time modified in device successfully..");
                }
                else
                {
                    gen.ShowMessage(this.Page, "Employee not found in any device..");
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            Genreal.AuditApi(strUrl, "UserInfo", "Record_Modify", apiStatus, "device_modify_employees", txtEmpName.Text, "modify employee " + txtEmployeeNo.Text, "", Session["TenantId"].ToString(), ip, srno, acid);
        }
    }

    private void GetEmployeeDetails()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            var data = db.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(Request.QueryString["empId"])).FirstOrDefault();
            if (data != null)
            {
                hfEmpId.Value = data.EmployeeId.ToString();
                hfDeviceCode.Value = data.EmployeeNo;
                txtEmployeeNo.Text = data.MachineID.ToString();
                txtEmpName.Text = data.FName + " " + data.Lname;

                BindDeviceList(data.CompanyId.Value);
            }
        }
    }
    private void BindDeviceList(int cId)
    {

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.DevicesTBs
                        where d.CompanyId ==cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
            lstFruits.DataSource = data;
            lstFruits.DataTextField = "DeviceName";
            lstFruits.DataValueField = "DeviceAccountId";
            lstFruits.DataBind();
        }

    }
}