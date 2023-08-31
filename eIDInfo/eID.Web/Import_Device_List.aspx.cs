using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Import_Device_List : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["key"])))
        {
            GetRegistrationDetails();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        else
        {
            Response.Redirect("add_device.aspx");
            // Response.Redirect("login.aspx");

        }
    }

    private void GetRegistrationDetails()
    {
        string key = Request.QueryString["key"].ToString().Trim();
        key = key.Replace("key_plus", "+");
        hfTenant.Value = key;
    }

    

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.PostedFile != null)
            {
                string excelPath = string.Concat(Server.MapPath("~/Attachments/DeviceFiles/" + FileUpload1.PostedFile.FileName));
                FileUpload1.SaveAs(excelPath);

                string conString = string.Empty;
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;

                }
                conString = string.Format(conString, excelPath, "YES");
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();


                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] WHERE DeviceSerialNo IS NOT NULL", excel_con))
                    {
                        oda.Fill(dtExcelData);
                    }
                    excel_con.Close();

                    #region Add Excel In Database
                    int counter = 0;

                    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                    {
                       
                            var data = db.DevicesTBs.OrderByDescending(u => u.DeviceId).FirstOrDefault();
                            int maxId = data == null ? 1 : data.DeviceId + 1;
                            string DeviceAccId = String.Format("{0,7:100000}", maxId).Trim();
                        


                        string pass = ConfigurationManager.AppSettings["DevicePassword"];
                        string username = ConfigurationManager.AppSettings["DeviceUserName"];
                        int AccountId = Convert.ToInt32(DeviceAccId);

                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            int DevAccountId = AccountId + i;
                            string Company = Convert.ToString(dtExcelData.Rows[i]["Company"]);
                            string DeviceName = Convert.ToString(dtExcelData.Rows[i]["DeviceName"]);
                            string DeviceSerialNo = Convert.ToString(dtExcelData.Rows[i]["DeviceSerialNo"]);                            
                            string IPAddress = Convert.ToString(dtExcelData.Rows[i]["IPAddress"]);
                            string DeviceLocation = Convert.ToString(dtExcelData.Rows[i]["DeviceLocation"]);
                            string DeviceDirection = Convert.ToString(dtExcelData.Rows[i]["DeviceDirection"]);
                            string PortNo = Convert.ToString(dtExcelData.Rows[i]["PortNo"]);
                            string DeviceModel = Convert.ToString(dtExcelData.Rows[i]["DeviceModel"]);                         
                           
                            try
                            {                                
                                bool isPresentComp = false;                                                               
                                                                
                               
                                int companyId = 0; 


                                #region get company data
                                var compData = db.CompanyInfoTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.CompanyName == Company);
                                companyId = compData != null ? compData.CompanyId : 0;
                                isPresentComp = compData != null ? true : false;
                                #endregion
                               

                                if (isPresentComp)
                                {
                                    var Devicedata = db.DevicesTBs.Where(a => a.DeviceSerialNo == DeviceSerialNo && a.CompanyId == companyId && a.TenantId == Convert.ToString(hfTenant.Value)).FirstOrDefault();
                                    if (Devicedata == null)
                                    {
                                        #region Create Device Data
                                        DevicesTB device = new DevicesTB();
                                        device.DeviceName = DeviceName;
                                        device.DeviceSerialNo = DeviceSerialNo;
                                        device.DeviceIp = IPAddress;
                                        device.DeviceLocation = DeviceLocation;
                                        device.DeviceAccountId = DevAccountId.ToString();
                                        device.DeviceDirection = DeviceDirection;
                                        device.CompanyId = companyId;
                                        device.TenantId = Convert.ToString(hfTenant.Value);
                                        device.PortNo = Convert.ToInt32(PortNo);
                                        device.UserName = username;
                                        device.Password = pass;
                                        device.DeviceModel = DeviceModel;                                        
                                        db.DevicesTBs.InsertOnSubmit(device);
                                        db.SubmitChanges();
                                        #endregion                                       
                                    }
                                    else
                                    {
                                        #region Update Device Data
                                        Devicedata.DeviceName = DeviceName;
                                        Devicedata.DeviceSerialNo = DeviceSerialNo;
                                        Devicedata.DeviceIp = IPAddress;
                                        Devicedata.DeviceLocation = DeviceLocation;                                        
                                        Devicedata.DeviceDirection = DeviceDirection;
                                        Devicedata.CompanyId = companyId;
                                        Devicedata.TenantId = Convert.ToString(hfTenant.Value);
                                        Devicedata.PortNo = Convert.ToInt32(PortNo);                                        
                                        Devicedata.DeviceModel = DeviceModel;                                       
                                       
                                        db.SubmitChanges();
                                        #endregion                                        
                                    }

                                    counter++;
                                }
                                

                            }
                            catch (Exception ex)
                            {
                                
                            }
                        }
                    }
                    if (counter > 0)
                    {
                        gen.ShowMessage(this.Page, counter + " Device imported successfully out of : " + dtExcelData.Rows.Count);
                    }
                    litErrors.Text += "</tbody>";
                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }





}