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

public partial class Import_EmployeeWise_WeeklyOff : System.Web.UI.Page
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
            Response.Redirect("mst_weekly_off_settings.aspx");
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
                string excelPath = string.Concat(Server.MapPath("~/Attachments/WeeklyOffFiles/" + FileUpload1.PostedFile.FileName));
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


                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] WHERE EMPNO IS NOT NULL", excel_con))
                    {
                        oda.Fill(dtExcelData);
                    }
                    excel_con.Close();

                    #region Add Excel In Database
                    int counter = 0;

                    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                    {                       
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {                            
                            string EmpNo = Convert.ToString(dtExcelData.Rows[i]["EMPNO"]);
                            string EmpName = Convert.ToString(dtExcelData.Rows[i]["EMP_NAME"]);
                            string DayId = Convert.ToString(dtExcelData.Rows[i]["DAYID"]);
                            string Day = Convert.ToString(dtExcelData.Rows[i]["W_OFF_DAY"]);                           

                            try
                            {
                                bool isPresentComp = false; bool isPresentEmp = false;
                                int companyId = 0; int EmpId = 0;
                                #region get Employee data
                                var empData = db.EmployeeTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.EmployeeNo == EmpNo);
                                EmpId = empData != null ? empData.EmployeeId : 0;
                                isPresentEmp = empData != null ? true : false;
                                #endregion

                                if(empData != null)
                                {
                                    #region get company data
                                    var compData = db.CompanyInfoTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.CompanyId == empData.CompanyId);
                                    companyId = compData != null ? compData.CompanyId : 0;
                                    isPresentComp = compData != null ? true : false;
                                    #endregion
                                }



                                if (isPresentComp && isPresentEmp)
                                {
                                    var EmpWeeklydata = db.EmployeeWeeklyOffTBs.Where(a => a.EmployeeNo == EmpNo && a.CompanyId == companyId && a.TenantId == Convert.ToString(hfTenant.Value)).FirstOrDefault();
                                    if (EmpWeeklydata == null)
                                    {
                                        #region Create Emp Weekly Data
                                        EmployeeWeeklyOffTB empweekoff = new EmployeeWeeklyOffTB();
                                        empweekoff.CompanyId = companyId;
                                        empweekoff.EmployeeNo = EmpNo;
                                        empweekoff.Days = Day;
                                        empweekoff.UserId = 0;                                       
                                        empweekoff.TenantId = Convert.ToString(hfTenant.Value);
                                        empweekoff.DayId = Convert.ToInt32(DayId);                                       
                                        db.EmployeeWeeklyOffTBs.InsertOnSubmit(empweekoff);
                                        db.SubmitChanges();
                                        #endregion                                       
                                    }
                                    else
                                    {
                                        #region Update Emp Weekly Data
                                        EmpWeeklydata.CompanyId = companyId;
                                        EmpWeeklydata.EmployeeNo = EmpNo;
                                        EmpWeeklydata.Days = Day;
                                        EmpWeeklydata.UserId = 0;
                                        EmpWeeklydata.TenantId = Convert.ToString(hfTenant.Value);
                                        EmpWeeklydata.DayId = Convert.ToInt32(DayId);                                         
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
                        gen.ShowMessage(this.Page, counter + " Employee Weekly Off imported successfully out of : " + dtExcelData.Rows.Count);
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