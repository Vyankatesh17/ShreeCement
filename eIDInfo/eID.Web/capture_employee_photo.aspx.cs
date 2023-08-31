using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class capture_employee_photo : System.Web.UI.Page
{

    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfCompany.Value = Request.QueryString["cId"];
            hfemployeecode.Value = Request.QueryString["EmployeeId"];
        }
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat =
    System.Web.Script.Services.ResponseFormat.Json,
    UseHttpGet = false)]
    public static bool SaveCapturedImage(string data, string Company, string EmployeeCode)
    {

        //var empdata = HR.EmployeeTBs.Where(a => a.EmployeeNo == EmployeeCode).FirstOrDefault();
        //string fileName = DateTime.Now.ToString("yyyyMMddhhmmss");
        

        string fileName = EmployeeCode;

        //Convert Base64 Encoded string to Byte Array.
        byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);

       
        //string path = "EmployeeImages/Faces/" + company; //"EmployeeImages/Faces/" + cId;
        string path = "EmployeeImages/Faces/" + Company;  //"EmployeeImages/Faces/" + cId;

        //Save the Byte Array as Image File.
        string filePath = HttpContext.Current.Server.MapPath(string.Format("~/{1}/{0}.jpg", fileName, path));
        File.WriteAllBytes(filePath, imageBytes);
        HttpContext.Current.Session["fileName"] = string.Format("{0}.jpg", fileName);

        return true;
    }

    public void GetEmployee(string EmployeeCode)
    {
       


    }
}