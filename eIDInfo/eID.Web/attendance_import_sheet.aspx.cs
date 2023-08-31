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

public partial class attendance_import_sheet : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindYear();
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                if (FileUpload1.PostedFile!=null)
                {
                    string excelPath = string.Concat(Server.MapPath("~/Attachments/AttendanceFiles/" + FileUpload1.PostedFile.FileName));
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
                    int counter = 0;
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] WHERE DeviceCode IS NOT NULL", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] WHERE EmployeeNo IS NOT NULL", excel_con))
                        //{
                        //    oda.Fill(dtExcelData);
                        //}
                        excel_con.Close();
                        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                        {
                            db.CommandTimeout = 5 * 60;
                            #region Add data in sql database
                            for (int i = 0; i < dtExcelData.Rows.Count; i++)
                            {
                                string empNo = Convert.ToString(dtExcelData.Rows[i]["EmployeeNo"]);
                                string devNo = Convert.ToString(dtExcelData.Rows[i]["DeviceCode"]);
                                string empName = Convert.ToString(dtExcelData.Rows[i]["EmpName"]);
                                string AttDate = Convert.ToString(dtExcelData.Rows[i]["Date"]);
                                string InTime = Convert.ToString(dtExcelData.Rows[i]["InTime"]);
                                string OutTime = Convert.ToString(dtExcelData.Rows[i]["OutTime"]);
                                string Duration = Convert.ToString(dtExcelData.Rows[i]["Duration"]);
                                string LateBy = Convert.ToString(dtExcelData.Rows[i]["LateBy"]);
                                string EarlyBy = Convert.ToString(dtExcelData.Rows[i]["EarlyBy"]);
                                string OverTime = Convert.ToString(dtExcelData.Rows[i]["OverTime"]);
                                string Status = Convert.ToString(dtExcelData.Rows[i]["Status"]);
                                string Remarks = Convert.ToString(dtExcelData.Rows[i]["Remarks"]);

                                

                                var empData = db.EmployeeTBs.Where(d =>d.IsActive==true && d.MachineID == empNo && d.EmployeeNo == devNo&&d.CompanyId==Convert.ToInt32(ddlCompany.SelectedValue)&&d.TenantId==Convert.ToString(Session["TenantId"])).FirstOrDefault();
                                if (empData != null)
                                {

                                    var checkExists = db.AttendaceLogTBs.Where(d => d.EmployeeId == empData.EmployeeId && d.AttendanceDate == Convert.ToDateTime(AttDate)).FirstOrDefault();
                                    AttendaceLogTB logTB = new AttendaceLogTB();
                                    if (checkExists != null)
                                    {
                                        logTB= db.AttendaceLogTBs.Where(d => d.EmployeeId == empData.EmployeeId && d.AttendanceDate == Convert.ToDateTime(AttDate)).FirstOrDefault();
                                    }
                                    logTB.AttendanceDate = Convert.ToDateTime(AttDate);
                                    logTB.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                    DateTime duration = Convert.ToDateTime(Duration);
                                    TimeSpan dur = duration.TimeOfDay;
                                    Double totaldur = dur.TotalHours;
                                    logTB.Duration =string.IsNullOrEmpty(Duration)?0: totaldur;
                                    logTB.EarlyBy=string.IsNullOrEmpty(EarlyBy)?0: Convert.ToInt32(EarlyBy);
                                    logTB.EmployeeId = empData.EmployeeId;
                                    DateTime intime = Convert.ToDateTime(InTime);
                                    logTB.InTime = intime.TimeOfDay.ToString();
                                    logTB.LateBy=string.IsNullOrEmpty(LateBy)?0: Convert.ToInt32(LateBy);
                                    DateTime outtime = Convert.ToDateTime(OutTime);
                                    logTB.OutTime = outtime.TimeOfDay.ToString();
                                    if(OverTime != "")
                                    {
                                        double overtime = Convert.ToDouble(OverTime);
                                        var hours = Convert.ToInt32(overtime);
                                        var mins = 60 * (overtime - hours);
                                        int totalminute = Convert.ToInt32((hours * 60) + mins);
                                        logTB.OverTime = totalminute;
                                    }
                                    else
                                    {
                                        logTB.OverTime = 0;
                                    }

                                    
                                    logTB.Remarks = Remarks;
                                    logTB.Status = Status;
                                    logTB.TenantId = Convert.ToString(Session["TenantId"]);
                                    if (checkExists == null)
                                    {
                                        db.AttendaceLogTBs.InsertOnSubmit(logTB);
                                    }
                                    db.SubmitChanges();
                                    counter++;
                                }

                            }
                            #endregion
                        }
                    }
                    if (counter > 0)
                    {
                        gen.ShowMessage(this.Page, "Attendance sheet uploaded successfully..");
                    }
                }
                else
                {
                    gen.ShowMessage(this.Page, "Please select file for upload..");
                }
            }
        }
        catch (Exception ex) { gen.ShowMessage(this.Page, ex.Message);litErrorMessage.Text = ex.Message + "\n Inner : " + ex.InnerException; }
    }
    private void BindCompanyList()
    {
        try
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
        catch (Exception ex) { }
    }
    private void BindYear()
    {
        ddlYear.Items.Clear();
        int year = DateTime.Now.AddYears(-75).Year;
        for (int i = year; i < DateTime.Now.AddYears(2).Year; i++)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }
}