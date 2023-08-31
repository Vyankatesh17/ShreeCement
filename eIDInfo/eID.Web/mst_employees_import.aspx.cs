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

public partial class mst_employees_import : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["key"])))
            {
                GetRegistrationDetails();
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
            else
            {
                Response.Redirect("mst_employee_list.aspx");
                // Response.Redirect("login.aspx");

            }
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
               

                string excelPath = string.Concat(Server.MapPath("~/Attachments/EmployeeFiles/" + FileUpload1.PostedFile.FileName));
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


                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] WHERE DeviceCode IS NOT NULL", excel_con))
                    {
                        oda.Fill(dtExcelData);
                    }
                    excel_con.Close();

                    #region Add Excel In Database
                    litErrors.Text = @"<thead><tr><th colspan='6'>Starting to import</th><tr>
<tr><th>Name</th><th>Device Code</th><th>Company</th><th>Email</th><th>Head</th><th>Status</th></tr>
</thead>
<tbody>";
                    int counter = 0;

                    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                    {
                        
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            string EmployeeCode = Convert.ToString(dtExcelData.Rows[i]["EmployeeCode"]);
                            string DeviceCode = Convert.ToString(dtExcelData.Rows[i]["DeviceCode"]);
                            string Company = Convert.ToString(dtExcelData.Rows[i]["Company"]);
                            string FirstName = Convert.ToString(dtExcelData.Rows[i]["FirstName"]);
                            string LastName = Convert.ToString(dtExcelData.Rows[i]["LastName"]);
                            string Department = Convert.ToString(dtExcelData.Rows[i]["Department"]);
                            string Designation = Convert.ToString(dtExcelData.Rows[i]["Designation"]);
                            string ReportingHead = Convert.ToString(dtExcelData.Rows[i]["ReportingHead"]);
                            string Mobile = Convert.ToString(dtExcelData.Rows[i]["Mobile"]);
                            string AlternateMobile = Convert.ToString(dtExcelData.Rows[i]["AlternateMobile"]);
                            string WorkEmail = Convert.ToString(dtExcelData.Rows[i]["WorkEmail"]);
                            string PersonalEmail = Convert.ToString(dtExcelData.Rows[i]["PersonalEmail"]);
                            string Gender = Convert.ToString(dtExcelData.Rows[i]["Gender"]);
                            string CardNo = Convert.ToString(dtExcelData.Rows[i]["CardNo"]);
                            string PANNo = Convert.ToString(dtExcelData.Rows[i]["PANNo"]);
                            string WorkLocation = Convert.ToString(dtExcelData.Rows[i]["WorkLocation"]);
                            string PinCode = Convert.ToString(dtExcelData.Rows[i]["PinCode"]);
                           
                            DateTime DOJ = DateTime.Now;
                            DateTime DOC = DateTime.Now;
                            DateTime DOB = DateTime.Now;
                            bool isDateformatDOJ = false;
                            try
                            {                               
                                DOJ = string.IsNullOrEmpty(Convert.ToString(dtExcelData.Rows[i]["DOJ"])) ? DateTime.Now : Convert.ToDateTime(dtExcelData.Rows[i]["DOJ"]);                                                    
                                 DOC = string.IsNullOrEmpty(Convert.ToString(dtExcelData.Rows[i]["DOC"]))?DateTime.Now:Convert.ToDateTime(dtExcelData.Rows[i]["DOC"]);
                                 DOB =string.IsNullOrEmpty(Convert.ToString(dtExcelData.Rows[i]["DOB"]))?DateTime.Now: Convert.ToDateTime(dtExcelData.Rows[i]["DOB"]);

                                 isDateformatDOJ = true;
                            }
                            catch(Exception ex)
                            {                               
                               
                            }
                            string ShiftGroup = Convert.ToString(dtExcelData.Rows[i]["ShiftGroup"]);
                            string Employeetype = Convert.ToString(dtExcelData.Rows[i]["EmployeeType"]);
                            try
                            {
                                bool isPresentDC = false;
                                bool isPresentComp = false;
                                //bool isPresentShift = false;
                                bool isPresentShiftGroup = false;
                                bool isPresentDept = false;
                                bool isPresentDesig = false;
                                bool isPresentMobile = false;
                                bool isPresentWork = false;
                                bool isPresentPersonalEmail = false;
                                bool alreadyPresent = false;
                                bool alreadyPresentDevice = false;
                                int companyId = 0; int deptId = 0; int desigId = 0; int deptHeadId = 0; int shiftgroupId = 0;


                                #region get company data
                                var compData = db.CompanyInfoTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.CompanyName == Company);
                                companyId = compData != null ? compData.CompanyId : 0;
                                isPresentComp = compData != null ? true : false;                                
                                #endregion

                                #region get Shift Group data
                                var shiftgroupData = db.ShiftGroupTBs.FirstOrDefault(d => d.CompanyId == companyId && d.ShiftGroupName == ShiftGroup);
                                shiftgroupId = shiftgroupData != null ? shiftgroupData.ShiftGroupId : 0;
                                isPresentShiftGroup = shiftgroupData != null ? true : false;
                                #endregion
                               
                                #region get department data
                                    var deptData = db.MasterDeptTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.CompanyId == companyId && d.DeptName == Department);

                                    if (deptData == null)
                                    {
                                        MasterDeptTB dept = new MasterDeptTB();
                                        dept.DeptName = Department;
                                        dept.Status = 1;
                                        dept.CompanyId = companyId;
                                        dept.TenantId = hfTenant.Value;
                                        db.MasterDeptTBs.InsertOnSubmit(dept);
                                        db.SubmitChanges();

                                        var deptsecData = db.MasterDeptTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.CompanyId == companyId && d.DeptName == Department);
                                        deptId = deptsecData.DeptID;
                                        isPresentDept = true;
                                    }
                                    else
                                    {
                                        deptId = deptData.DeptID;
                                        isPresentDept = true;
                                    }                                  
                                    #endregion

                                #region get designation data
                                    var desigData = db.MasterDesgTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.CompanyId == companyId && d.DeptID == deptId && d.DesigName == Designation);
                                if (desigData == null)
                                {
                                    MasterDesgTB desg = new MasterDesgTB();
                                    desg.DesigName = Designation;
                                    desg.Status = 1;
                                    desg.DeptID = deptId;
                                    desg.CompanyId = companyId;
                                    desg.TenantId = hfTenant.Value;
                                    db.MasterDesgTBs.InsertOnSubmit(desg);
                                    db.SubmitChanges();

                                    var desigsecData = db.MasterDesgTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.CompanyId == companyId && d.DeptID == deptId && d.DesigName == Designation);
                                    desigId = desigsecData.DesigID;
                                    isPresentDesig =  true;
                                }
                                else
                                {
                                    desigId = desigData.DesigID;
                                    isPresentDesig = true;
                                }


                                //desigId = desigData != null ? desigData.DesigID : 0;
                                    //isPresentDesig = desigData != null ? true : false;
                                    #endregion

                                #region get reporting head data
                                    var empHeadData = db.EmployeeTBs.FirstOrDefault(d => d.TenantId == hfTenant.Value && d.CompanyId == companyId && d.DeptId == deptId && d.FName + " " + d.Lname == ReportingHead);
                                    deptHeadId = empHeadData != null ? empHeadData.EmployeeId : 0;
                                    isPresentDC = string.IsNullOrEmpty(DeviceCode) ? false : true;
                                    isPresentMobile = string.IsNullOrEmpty(Mobile) ? false : true;
                                    isPresentWork = string.IsNullOrEmpty(WorkEmail) ? false : true;
                                    isPresentPersonalEmail = string.IsNullOrEmpty(PersonalEmail) ? false : true;

                                #endregion
                               
                                if (isPresentComp && isPresentDC && isPresentMobile && isPresentPersonalEmail && isPresentWork && isPresentShiftGroup && isDateformatDOJ)
                                        {
                                            var empdata = db.EmployeeTBs.Where(a => a.EmployeeNo == EmployeeCode && a.CompanyId == companyId && a.TenantId == Convert.ToString(hfTenant.Value)).FirstOrDefault();
                                            if(empdata == null)
                                            {
                                                #region Create Employee Data
                                            EmployeeTB employee = new EmployeeTB();
                                                employee.IsActive = true;
                                                employee.RelivingStatus = 0;
                                                employee.CompanyId = companyId;
                                                employee.ContactNo = Mobile;
                                                employee.AltContactNo = AlternateMobile;
                                                employee.DeptId = deptId;
                                                employee.Email = WorkEmail;
                                                employee.EmployeeNo = EmployeeCode;
                                                employee.FName = FirstName;
                                                employee.Lname = LastName;
                                                employee.MachineID = DeviceCode;
                                                employee.ManagerID = deptHeadId;
                                                employee.personalEmail = PersonalEmail;
                                                employee.TenantId = Convert.ToString(hfTenant.Value);
                                                employee.DesgId = desigId;
                                                employee.Gender = Gender;
                                                employee.PanNo = PANNo;
                                                employee.AccessCardNo = CardNo;
                                                employee.DOJ = DOJ;
                                                employee.BirthDate = DOB;
                                                employee.ConfirmDate = DOC;                                          
                                                if (shiftgroupData != null)
                                                {
                                                    employee.ShiftGroupId = shiftgroupData.ShiftGroupId;
                                                }

                                                if (WorkLocation != "")
                                                {
                                                    employee.WorkLocation = WorkLocation;
                                                }
                                                else
                                                {
                                                    employee.WorkLocation = "NA";
                                                }

                                                if(Employeetype != null || Employeetype != "")
                                                {
                                                    employee.Grade = Employeetype;
                                                }
                                                employee.CurPin = PinCode;
                                                db.EmployeeTBs.InsertOnSubmit(employee);
                                                db.SubmitChanges();
                                            #endregion

                                                #region Create User Login Credentials
                                                SystemUsersTB usersTB = new SystemUsersTB();
                                                string key = DateTime.Now.ToString("yyyyMMddhhmmssffffff");
                                                string secureuid = SPPasswordHasher.Encrypt(key);
                                                string genPass = FirstName.Trim() + "@123";
                                                string securepass = SPPasswordHasher.Encrypt(genPass);
                                                string displayName = FirstName + " " + LastName;
                                                string userName = WorkEmail;
                                                string userEmail = WorkEmail;
                                                usersTB.Active = true;
                                                usersTB.Disabled = false;
                                                usersTB.DisplayName = displayName;
                                                usersTB.Email = userEmail;
                                                usersTB.Password = genPass;
                                                usersTB.PasswordHash = securepass;
                                                usersTB.PhoneNumber = Mobile;
                                                usersTB.TenantId = Convert.ToString(Session["TenantId"]);
                                                usersTB.UID = secureuid;
                                                usersTB.Username = userName;
                                                usersTB.UserRole = "User";
                                                usersTB.EmployeeId = employee.EmployeeId;
                                                db.SystemUsersTBs.InsertOnSubmit(usersTB);
                                                db.SubmitChanges();
                                                #endregion
                                            }
                                            else
                                            {
                                                #region Update Employee Data
                                            empdata.ContactNo = Mobile;
                                                empdata.AltContactNo = AlternateMobile;
                                                empdata.DeptId = deptId;
                                                empdata.Email = WorkEmail;
                                                empdata.FName = FirstName;
                                                empdata.Lname = LastName;
                                                empdata.ManagerID = deptHeadId;
                                                empdata.personalEmail = PersonalEmail;
                                                empdata.DesgId = desigId;
                                                empdata.Gender = Gender;
                                                empdata.PanNo = PANNo;
                                                empdata.AccessCardNo = CardNo;
                                                empdata.DOJ = DOJ;
                                                empdata.BirthDate = DOB;
                                                empdata.ConfirmDate = DOC;
                                                if (shiftgroupData != null)
                                                {
                                                    empdata.ShiftGroupId = shiftgroupData.ShiftGroupId;
                                                }

                                                if (WorkLocation != "")
                                                {
                                                    empdata.WorkLocation = WorkLocation;
                                                }
                                                else
                                                {
                                                    empdata.WorkLocation = "NA";
                                                }
                                                if (Employeetype != null || Employeetype != "")
                                                {
                                                    empdata.Grade = Employeetype;
                                                }
                                                empdata.CurPin = PinCode;
                                                db.SubmitChanges();
                                            #endregion

                                                #region Update User Login Credentials
                                            var systemuserdata = db.SystemUsersTBs.Where(a => a.Username == WorkEmail && a.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
                                          
                                            if(systemuserdata != null)
                                            {                      
                                                if(systemuserdata.UserRole == "User")
                                                {
                                                    string genPass = FirstName.Trim() + "@123";
                                                    string securepass = SPPasswordHasher.Encrypt(genPass);
                                                    string displayName = FirstName + " " + LastName;
                                                    string userName = WorkEmail;
                                                    string userEmail = WorkEmail;
                                                    systemuserdata.DisplayName = displayName;
                                                    systemuserdata.Email = userEmail;
                                                    systemuserdata.Password = genPass;
                                                    systemuserdata.PasswordHash = securepass;
                                                    systemuserdata.PhoneNumber = Mobile;
                                                    systemuserdata.Username = userName;
                                                    db.SubmitChanges();
                                                }                                               
                                            }
                                            else
                                            {
                                                SystemUsersTB usersTB = new SystemUsersTB();
                                                string key = DateTime.Now.ToString("yyyyMMddhhmmssffffff");
                                                string secureuid = SPPasswordHasher.Encrypt(key);
                                                string genPass = FirstName.Trim() + "@123";
                                                string securepass = SPPasswordHasher.Encrypt(genPass);
                                                string displayName = FirstName + " " + LastName;
                                                string userName = WorkEmail;
                                                string userEmail = WorkEmail;
                                                usersTB.Active = true;
                                                usersTB.Disabled = false;
                                                usersTB.DisplayName = displayName;
                                                usersTB.Email = userEmail;
                                                usersTB.Password = genPass;
                                                usersTB.PasswordHash = securepass;
                                                usersTB.PhoneNumber = Mobile;
                                                usersTB.TenantId = Convert.ToString(Session["TenantId"]);
                                                usersTB.UID = secureuid;
                                                usersTB.Username = userName;
                                                usersTB.UserRole = "User";
                                                usersTB.EmployeeId = empdata.EmployeeId;
                                                db.SystemUsersTBs.InsertOnSubmit(usersTB);
                                                db.SubmitChanges();
                                            }
                                            #endregion
                                            }

                                        counter++;
                                        }                                       
                                        else if (!isPresentComp)
                                        {
                                            litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                                             FirstName + " " + LastName, DeviceCode, Company, WorkEmail, ReportingHead, "Not imported due to Company not Found");
                                        }
                                        else if (!isPresentMobile)
                                        {
                                            litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                                             FirstName + " " + LastName, DeviceCode, Company, WorkEmail, ReportingHead, "Not imported due to Mobile No. not Found");
                                        }
                                        else if (!isPresentPersonalEmail)
                                        {
                                            litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                                             FirstName + " " + LastName, DeviceCode, Company, WorkEmail, ReportingHead, "Not imported due to Personal Mail Id not Found");
                                        }
                                        else if (!isPresentWork)
                                        {
                                            litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                                             FirstName + " " + LastName, DeviceCode, Company, WorkEmail, ReportingHead, "Not imported due to Work Mail Id not Found");
                                        }
                                        else if(!isPresentShiftGroup)
                                        {
                                                    litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                                             FirstName + " " + LastName, DeviceCode, Company, WorkEmail, ReportingHead, "Not imported due to default shift Group not found in database");
                                        }
                                        else if (!isDateformatDOJ)
                                        {
                                            litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                                                    FirstName + " " + LastName, DeviceCode, Company, WorkEmail, ReportingHead, "Not imported due to DOJ/DOC/DOB Date Format Mismatch.");
                                        }

                            }
                            catch (Exception ex)
                            {
                                litErrors.Text += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                                          FirstName + " " + LastName, DeviceCode, Company, WorkEmail, ReportingHead, ex.Message);
                            }
                        }
                    }
                    if (counter > 0)
                    {
                        gen.ShowMessage(this.Page, counter + " Employee imported successfully out of : " + dtExcelData.Rows.Count);
                    }
                    litErrors.Text += "</tbody>";
                    #endregion
                }
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}