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

public partial class mst_roster_import : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
            }
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.PostedFile != null)
            {
                string path = Server.MapPath("~/Attachments/RosterFiles/" );
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string excelPath = string.Concat(Server.MapPath("~/Attachments/RosterFiles/" + FileUpload1.PostedFile.FileName));
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
                DataTable dtExcelData = new DataTable();

                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();


                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] WHERE EmpNo IS NOT NULL", excel_con))
                    {
                        oda.Fill(dtExcelData);
                    }
                    excel_con.Close();
                }
                #region Add Excel In Database
                litErrors.Text = @"<thead><tr><th colspan='6'>Starting to import</th><tr>
<tr><th>Employee Name</th><th>Employee No</th><th>Shift</th><th>From Date</th><th>To Date</th><th>Status</th></tr>
</thead>
<tbody>";
                int counter = 0;
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    db.CommandTimeout = 5 * 60;
                    for (int i = 0; i < dtExcelData.Rows.Count; i++)
                    {
                        string EmpName = Convert.ToString(dtExcelData.Rows[i]["EmpName"]);
                        string EmpNo = Convert.ToString(dtExcelData.Rows[i]["EmpNo"]);
                        string Shift = Convert.ToString(dtExcelData.Rows[i]["Shift"]);
                        string fromDate = Convert.ToString(dtExcelData.Rows[i]["FromDate"]);
                        string toDate = Convert.ToString(dtExcelData.Rows[i]["ToDate"]);

                        try
                        {
                            int companyId = Convert.ToInt32(ddlCompany.SelectedValue);
                            #region get employee data
                            var empData = (from d in db.EmployeeTBs
                                           where d.IsActive == true && d.EmployeeNo == EmpNo && d.CompanyId == companyId && d.TenantId == Convert.ToString(Session["TenantId"])
                                           select new { d.EmployeeNo, d.EmployeeId, d.CompanyId, d.DeptId, d.DesgId }).FirstOrDefault();
                            #endregion
                            if (empData != null)
                            {
                                companyId = empData.CompanyId.Value;
                                var sData = db.MasterShiftTBs.Where(d => d.Shift == Shift && d.CompanyId == companyId && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
                                if (sData != null)
                                {
                                    DateTime startDate = Convert.ToDateTime(fromDate);
                                    DateTime endDate = Convert.ToDateTime(toDate);
                                    try
                                    {
                                        RosterMasterTB roster = new RosterMasterTB();
                                        roster.CompanyId = companyId;
                                        roster.DesgId = empData.DesgId.HasValue ? empData.DesgId.Value : 0;
                                        roster.DeptId = empData.DeptId;
                                        roster.EmpID = empData.EmployeeId;
                                        roster.FromDate = startDate;
                                        roster.Month = startDate.ToString("MMMM");
                                        roster.MonthID = startDate.Month;
                                        roster.TenantId = Convert.ToString(Session["TenantId"]);
                                        roster.Year = startDate.Year.ToString();
                                        roster.ToDate = endDate;
                                        db.RosterMasterTBs.InsertOnSubmit(roster);

                                        for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
                                        {
                                            RosterDetailsTB detailsTB = new RosterDetailsTB();
                                            detailsTB.CompanyId = companyId;
                                            detailsTB.Date = day;
                                            detailsTB.Day = day.Day;
                                            detailsTB.EmpID = empData.EmployeeId;
                                            detailsTB.ShiftId = sData.ShiftID;
                                            detailsTB.RosterId = roster.RosterId;
                                            detailsTB.TenantId = Convert.ToString(Session["TenantId"]);
                                            detailsTB.Type = Shift;
                                            db.RosterDetailsTBs.InsertOnSubmit(detailsTB);
                                            db.SubmitChanges();
                                        }
                                        counter++;
                                        litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><span class='text-green'>{5}</span></td></tr>",
                                     EmpName, EmpNo, Shift, fromDate, toDate, "Success");
                                    }
                                    catch (Exception ex)
                                    {
                                        litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><span class='text-danger'>{5}</span></td></tr>",
                                     EmpName, EmpNo, Shift, fromDate, toDate, ex.Message);
                                    }
                                }
                                else
                                {
                                    litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><span class='text-danger'>{5}</span></td></tr>",
                                     EmpName, EmpNo, Shift, fromDate, toDate, "Shift not found..");
                                }
                            }
                            else
                            {
                                litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><span class='text-danger'>{5}</span></td></tr>",
                                     EmpName, EmpNo, Shift, fromDate, toDate, "Employee not found..");
                            }
                        }
                        catch (Exception ex)
                        {
                            litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><span class='text-danger'>{5}</span></td></tr>",
                                      EmpName, EmpNo, Shift, fromDate, toDate, ex.Message);
                        }
                    }
                }
                if (counter > 0)
                {
                    gen.ShowMessage(this.Page, counter + " Roster imported successfully out of : " + dtExcelData.Rows.Count);
                }
                litErrors.Text += "</tbody>";
                #endregion
            }
        }
        catch (Exception ex)
        {
            litErrors.Text += ex.Message;
            gen.ShowMessage(this.Page, ex.Message);
        }
    }
}