using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dash_device_status : System.Web.UI.Page
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
                if (!string.IsNullOrEmpty(Request.QueryString["cId"]))
                    BindDataGridViewList();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        BindJqFunctions();
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
    private void BindDataGridViewList()
    {
        var data = (from d in db.DevicesTBs
                    join c in db.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                    select new
                    {
                        d.CompanyId,
                        d.DeviceAccountId,
                        d.DeviceDirection,
                        d.DeviceId,
                        d.DeviceLocation,
                        d.DeviceName,
                        d.DeviceSerialNo,
                        d.DeviceStatus,
                        d.DownloadDate,
                        d.IPAddress,
                        d.TenantId,
                        c.CompanyName,
                        d.PortNo,
                        d.UserName,
                        d.Password,
                        Status = GetDeviceStatus(d.DeviceAccountId, d.DeviceSerialNo, d.IPAddress, d.Password, d.PortNo, d.UserName, d.TenantId)
                    }).ToList();
        data = data.Where(d => d.TenantId == Convert.ToString(Session["TenantId"])&& d.CompanyId==Convert.ToInt32(Request.QueryString["cId"])).ToList();
        gvList.DataSource = data;
        gvList.DataBind();
    }

    private string GetDeviceStatus(string deviceAccountId, string deviceSerialNo, string iPAddress, string password, int? portNo, string userName, string tenantId)
    {
        string devStatus = "offline";
        try
        {
            if (tenantId.Equals(Convert.ToString(Session["TenantId"])))
            {
                password = string.IsNullOrEmpty(password) ? ConfigurationManager.AppSettings["DevicePassword"] : password;
                userName = string.IsNullOrEmpty(userName) ? ConfigurationManager.AppSettings["DeviceUserName"] : userName;
                string deviceStatUrl = "http://" + iPAddress + ":" + portNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":50}}";

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
                        var dev = dr.SearchResult.MatchList.FirstOrDefault(d => d.Device.EhomeParams.EhomeID == deviceAccountId).Device.devStatus;

                        devStatus = string.IsNullOrEmpty(dev) ? "Offline" : dev;
                    }
                    apiStatus = "success";
                }
                Genreal.AuditApi(deviceStatUrl, "DeviceMgmt", "GetDeviceStatus", apiStatus, "Add Device", Session["DisplayName"].ToString(), "show device status on device list", Request.QueryString["cid"].ToString(), Session["TenantId"].ToString(), iPAddress, deviceSerialNo, deviceAccountId);
            }
        }
        catch (Exception ex) { }
        return devStatus;
    }

}