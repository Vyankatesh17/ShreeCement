using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class add_device : System.Web.UI.Page
{
    HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext();
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
                GetMaxNo();
                BindDataGridViewList();
                fillcompany();
                txtDeviceId.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        BindJqFunctions();
            checkLock();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            GetMaxNo();
            DevicesTB devices = new DevicesTB();
            string msg = "Device updated successfully..";
            var chk = (from d in db.DevicesTBs where d.DeviceSerialNo == txtSerialNo.Text || d.DeviceAccountId == txtDeviceId.Text select d).ToList();
            if (btnSave.Text != "Save")
            {
                devices = db.DevicesTBs.Where(d => d.DeviceId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                chk = chk.Where(d => d.DeviceId != Convert.ToInt32(hfKey.Value)).ToList();
            }
            if (chk.Count() > 0)
            {
                gen.ShowMessage(this.Page, string.Format("Device with serial no : {0} already present in database", txtSerialNo.Text));
            }
            else
            {
                string pass = ConfigurationManager.AppSettings["DevicePassword"];
                string username = ConfigurationManager.AppSettings["DeviceUserName"];

                devices.DeviceDirection = ddlDirection.SelectedValue;
                devices.DeviceLocation = txtLocation.Text;
                devices.DeviceName = txtDeviceName.Text;
                devices.DeviceSerialNo = txtSerialNo.Text;
                devices.IPAddress = txtIpAddress.Text;
                devices.PortNo = Convert.ToInt32(txtPortNo.Text);
                devices.UserName = username;
                devices.Password = pass;
                devices.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                devices.TenantId = Convert.ToString(Session["TenantId"]);
                devices.DeviceIp = txtDeviceIp.Text;
                devices.DeviceModel = ddldeviceModel.SelectedValue;
                if (btnSave.Text == "Save")
                {
                    devices.DeviceAccountId = txtDeviceId.Text;// DateTime.Now.ToString("ddMMyy");
                    db.DevicesTBs.InsertOnSubmit(devices);
                    msg = "Device added successfully..";
                }
                db.SubmitChanges();

                BindDataGridViewList();
                ClearFields();
                gen.ShowMessage(this.Page, msg);

            }


        }
        catch (Exception ex) { gen.ShowMessage(this.Page, ex.Message); }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        string skey = Session["TenantId"].ToString().Replace("+", "key_plus");
        Response.Redirect("Import_Device_List.aspx?key=" + skey);
    }

    protected void btnSetTime_Click(object sender, EventArgs e)
    {
        try
        {
            var data = db.DevicesTBs.Where(d => d.DeviceId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();

            if(data != null)
            {
                string password = string.IsNullOrEmpty(data.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : data.Password;
                string userName = string.IsNullOrEmpty(data.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : data.UserName;
                string deviceStatUrl = "http://" + data.IPAddress + ":" + data.PortNo + "/ISAPI/System/time?format=json&devIndex="+ data.devIndex;

                string setdate = txtsetdatefordevice.Text;
                string settime = txtsettimefordevice.Text;

                string setdatetime = setdate +"T" + settime + ":00+05:30";

                string req = "{\"Time\" : {\"timeMode\":\"manual\",\"localTime\":\""+ setdatetime + "\"}}";

                string reps = string.Empty;
                string strMatchNum = string.Empty;
                clienthttp clnt = new clienthttp();
                int iet = clnt.HttpRequest(userName, password, deviceStatUrl, "PUT", req, ref reps);
                string apiStatus = "failed";
                if (iet == (int)HttpStatus.Http200)
                {
                    Devicesettimeresult dr1 = JsonConvert.DeserializeObject<Devicesettimeresult>(reps);
                    strMatchNum = Convert.ToString(dr1.statusString);
                    if ("OK" == strMatchNum)
                    {
                        apiStatus = "success";
                        gen.ShowMessage(this.Page, "Device Set time Successfully");
                    }                    
                }
                Genreal.AuditApi(deviceStatUrl, "System", "btnSetTime_Click", apiStatus, "Add Device", Session["DisplayName"].ToString(), "Set Time on device", ddlCompany.SelectedValue, Session["TenantId"].ToString(), "", "", "");
                
            }
            

        }
        catch (Exception ex) { gen.ShowMessage(this.Page, ex.Message); }
    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        BindDataGridViewList();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        hfKey.Value = linkButton.CommandArgument;
        btnSave.Text = "Update";
        SetTime.Visible = true;
        try
        {
            using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
            {
                var data = db.DevicesTBs.Where(d => d.DeviceId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                txtDeviceId.Text = data.DeviceAccountId;
                txtDeviceName.Text = data.DeviceName;
                txtIpAddress.Text = data.IPAddress;
                txtLocation.Text = data.DeviceLocation;
                txtSerialNo.Text = data.DeviceSerialNo;
                ddlDirection.SelectedValue = data.DeviceDirection;
                ddlCompany.SelectedValue = data.CompanyId.Value.ToString();
                txtPortNo.Text = data.PortNo.Value.ToString();
                txtDeviceIp.Text = data.DeviceIp;
                ddldeviceModel.SelectedValue = data.DeviceModel;
                txtSerialNo.Enabled = false;
                txtDeviceId.Enabled = false;
                MultiView1.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex) { }
    }

    private void GetMaxNo()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            var data = db.DevicesTBs.OrderByDescending(u => u.DeviceId).FirstOrDefault();
            int maxId = data == null ? 1 : data.DeviceId + 1;

            txtDeviceId.Text = String.Format("{0,7:100000}", maxId).Trim();
        }
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
    }

    private void BindDataGridViewList()
        {
        var data = (from d in db.DevicesTBs                    
                     select new {
                        d.CompanyId,d.DeviceAccountId,d.DeviceDirection,d.DeviceId,d.DeviceLocation,d.DeviceName,d.DeviceSerialNo,d.DeviceStatus,d.DeviceModel,
                        d.DownloadDate,d.IPAddress,d.TenantId,d.PortNo,d.UserName,d.Password,
                        Status=GetDeviceStatus(d.DeviceAccountId,d.DeviceSerialNo,d.IPAddress,d.Password,d.PortNo,d.UserName,d.TenantId, d.DeviceModel)
                    }).ToList();
        data = data.Where(d => d.TenantId == Convert.ToString(Session["TenantId"])).ToList();
        gvList.DataSource = data;
        gvList.DataBind();
    }

    private string GetDeviceStatus(string deviceAccountId, string deviceSerialNo, string iPAddress, string password, int? portNo, string userName,string tenantId, string devicemodel)
    {
        string devStatus = "offline";
        try
        {

            if (devicemodel == "Hikvision")
            {
                if (tenantId.Equals(Convert.ToString(Session["TenantId"])))
                {
                    password = string.IsNullOrEmpty(password) ? ConfigurationManager.AppSettings["DevicePassword"] : password;
                    userName = string.IsNullOrEmpty(userName) ? ConfigurationManager.AppSettings["DeviceUserName"] : userName;
                    string deviceStatUrl = "http://" + iPAddress + ":" + portNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                    string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":5000}}";

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
                            var dev = dr.SearchResult.MatchList.Where(d => d.Device.EhomeParams.EhomeID == deviceAccountId).FirstOrDefault();
                            if (dev != null)
                                devStatus = string.IsNullOrEmpty(dev.Device.devStatus) ? "Offline" : dev.Device.devStatus;

                            DevicesTB devicesTB = db.DevicesTBs.Where(d => d.DeviceAccountId == deviceAccountId && d.DeviceSerialNo == deviceSerialNo).FirstOrDefault();
                            if (devicesTB != null)
                            {
                                if (dev!= null)
                                {
                                    devicesTB.devIndex = dev.Device.devIndex;
                                    db.SubmitChanges();
                                }                               
                            }

                        }
                        apiStatus = "success";
                    }
                    Genreal.AuditApi(deviceStatUrl, "DeviceMgmt", "GetDeviceStatus", apiStatus, "Add Device", Session["DisplayName"].ToString(), "show device status on device list", ddlCompany.SelectedValue, Session["TenantId"].ToString(), "", "", "");
                }
            }
            else
            {
                var devstatusinfo = gen.GetesslDevice(deviceAccountId);
                if (devstatusinfo == "Online")
                {
                    devStatus = "online";
                }                  
            }
        }
        catch(Exception ex) {  }
        return devStatus;
    }

    private void ClearFields()
    {
        ddlDirection.SelectedIndex = 0;
        txtDeviceId.Text = txtDeviceName.Text  = txtIpAddress.Text = txtLocation.Text = txtSerialNo.Text=txtPortNo.Text = "";
        hfKey.Value = "";
        ddlCompany.SelectedIndex = 0;
        txtSerialNo.Enabled = true;
        txtDeviceId.Enabled = true;
        GetMaxNo();
        btnSave.Text = "Save";
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvList.Rows.Count > 0)
        {
            gvList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    private void checkLock()
    {
        try {
            LockSettingsTB locks = db.LockSettingsTBs.Where(d => d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
            int lockCount = 0;
            if (locks != null)
            {
                lockCount = Convert.ToInt32(SPPasswordHasher.Decrypt(locks.Devices));
            }
            if (lockCount == 0)
            {
                btnAdd.Visible = true;
            }
            else if (gvList.Rows.Count >= lockCount)
            {
                btnAdd.Visible = false;
            }
            else
            {
                btnAdd.Visible = true;
            }
        }
        catch(Exception ex) { }
    }
}