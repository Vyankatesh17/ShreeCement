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

public partial class Import_Leave_Balance : System.Web.UI.Page
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
            Response.Redirect("Leave_Summary.aspx");            
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
                string excelPath = string.Concat(Server.MapPath("~/Attachments/LeaveBalance/" + FileUpload1.PostedFile.FileName));
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

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] WHERE CODE IS NOT NULL", excel_con))
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
                            string EmpNo = Convert.ToString(dtExcelData.Rows[i]["CODE"]);
                            string EmpName = Convert.ToString(dtExcelData.Rows[i]["NAME"]);
                            string Designation = Convert.ToString(dtExcelData.Rows[i]["DESIGN"]);
                            string CLLeave = Convert.ToString(dtExcelData.Rows[i]["CL"]);
                            string SLLeave = Convert.ToString(dtExcelData.Rows[i]["SL"]);
                            string ELLeave = Convert.ToString(dtExcelData.Rows[i]["EL"]);
                            string LWPLeave = Convert.ToString(dtExcelData.Rows[i]["LWP"]);


                           List<masterLeavesTB> LeaveListData = new List<masterLeavesTB>();
                            masterLeavesTB CLLeaveData = new masterLeavesTB();
                            masterLeavesTB SLLeaveData = new masterLeavesTB();
                            masterLeavesTB ELLeaveData = new masterLeavesTB();
                            masterLeavesTB LWPLeaveData = new masterLeavesTB();
                            try
                            {
                                bool isPresentComp = false; bool isPresentEmp = false;
                                int companyId = 0; int EmpId = 0;
                                #region get Employee data
                                var empData = db.EmployeeTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.EmployeeNo == EmpNo);
                                EmpId = empData != null ? empData.EmployeeId : 0;
                                isPresentEmp = empData != null ? true : false;
                                #endregion

                                if (empData != null)
                                {
                                    #region get company data
                                    var compData = db.CompanyInfoTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.CompanyId == empData.CompanyId);
                                    companyId = compData != null ? compData.CompanyId : 0;
                                    isPresentComp = compData != null ? true : false;
                                    #endregion

                                     LeaveListData = db.masterLeavesTBs.Where(a => a.CompanyId == empData.CompanyId).ToList();

                                     CLLeaveData = LeaveListData.Where(a => a.LeaveName == "CL").FirstOrDefault();
                                     SLLeaveData = LeaveListData.Where(a => a.LeaveName == "SL").FirstOrDefault();
                                     ELLeaveData = LeaveListData.Where(a => a.LeaveName == "EL").FirstOrDefault();
                                     LWPLeaveData = LeaveListData.Where(a => a.LeaveName == "LWP").FirstOrDefault();
                                }

                                if (isPresentComp && isPresentEmp)
                                { 
                                    var Empleavedata = db.LeaveAllocationTBs.Where(a => a.EmployeeID == empData.EmployeeId && a.CompanyId == companyId && a.TenantId == Convert.ToString(hfTenant.Value)).ToList();
                                    if (Empleavedata.Count == 0)
                                    {
                                        foreach (var item1 in LeaveListData)
                                        {
                                            #region Create Emp Leave Balance Data
                                            LeaveAllocationTB leaveallocation = new LeaveAllocationTB();
                                            leaveallocation.CompanyId = companyId;
                                            leaveallocation.TenantId = Convert.ToString(hfTenant.Value);
                                            leaveallocation.EmployeeID = empData.EmployeeId;
                                            leaveallocation.FromDateAllocation = new DateTime(DateTime.Now.Year, 1, 1);
                                            leaveallocation.ToDateAllocation = new DateTime(DateTime.Now.Year, 12, 31);

                                            leaveallocation.LeaveID = item1.LeaveID;
                                            if (item1.LeaveName == CLLeaveData.LeaveName)
                                            {
                                                leaveallocation.TotalAllocatedLeaves = Convert.ToDouble(CLLeave);
                                                leaveallocation.PendingLeaves = Convert.ToDouble(CLLeave);
                                            }
                                            else if (item1.LeaveName == SLLeaveData.LeaveName)
                                            {
                                                leaveallocation.TotalAllocatedLeaves = Convert.ToDouble(SLLeave);
                                                leaveallocation.PendingLeaves = Convert.ToDouble(SLLeave);
                                            }
                                            else if (item1.LeaveName == ELLeaveData.LeaveName)
                                            {
                                                leaveallocation.TotalAllocatedLeaves = Convert.ToDouble(ELLeave);
                                                leaveallocation.PendingLeaves = Convert.ToDouble(ELLeave);
                                            }
                                            else if (item1.LeaveName == LWPLeaveData.LeaveName)
                                            {
                                                leaveallocation.TotalAllocatedLeaves = Convert.ToDouble(LWPLeave);
                                                leaveallocation.PendingLeaves = Convert.ToDouble(LWPLeave);
                                            }

                                            leaveallocation.AllocationStatus = 1;

                                            db.LeaveAllocationTBs.InsertOnSubmit(leaveallocation);
                                            db.SubmitChanges();
                                            #endregion
                                        }
                                 
                                    }                                    
                                    else
                                    {
                                        foreach (var item in Empleavedata)
                                        {
                                            #region Update Emp Leave Balance Data                                           
                                            item.CompanyId = companyId;
                                            item.TenantId = Convert.ToString(hfTenant.Value);
                                            item.EmployeeID = empData.EmployeeId;
                                            item.FromDateAllocation = new DateTime(DateTime.Now.Year, 1, 1);
                                            item.ToDateAllocation = new DateTime(DateTime.Now.Year, 12, 31);

                                            var leavedata = db.masterLeavesTBs.Where(a => a.LeaveID == item.LeaveID).FirstOrDefault();
                                            if (leavedata != null)
                                            {
                                                item.LeaveID = item.LeaveID;
                                                if (leavedata.LeaveName == CLLeaveData.LeaveName)
                                                {
                                                    item.TotalAllocatedLeaves = Convert.ToDouble(CLLeave);
                                                    item.PendingLeaves = Convert.ToDouble(CLLeave);
                                                }
                                                else if (leavedata.LeaveName == SLLeaveData.LeaveName)
                                                {
                                                    item.TotalAllocatedLeaves = Convert.ToDouble(SLLeave);
                                                    item.PendingLeaves = Convert.ToDouble(SLLeave);
                                                }
                                                else if (leavedata.LeaveName == ELLeaveData.LeaveName)
                                                {
                                                    item.TotalAllocatedLeaves = Convert.ToDouble(ELLeave);
                                                    item.PendingLeaves = Convert.ToDouble(ELLeave);
                                                }
                                                else if (leavedata.LeaveName == LWPLeaveData.LeaveName)
                                                {
                                                    item.TotalAllocatedLeaves = Convert.ToDouble(LWPLeave);
                                                    item.PendingLeaves = Convert.ToDouble(LWPLeave);
                                                }
                                            }

                                            item.AllocationStatus = 1;                                            
                                            db.SubmitChanges();
                                            #endregion

                                        }
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
                        gen.ShowMessage(this.Page, counter + " Employee Leave Balance imported successfully out of : " + dtExcelData.Rows.Count);
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