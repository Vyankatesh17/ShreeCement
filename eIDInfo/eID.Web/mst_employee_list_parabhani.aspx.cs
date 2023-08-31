using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
//using ServiceReference1;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using System.Drawing;
using System.Threading;

public partial class mst_employee_list_parabhani : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    DataTable Dt = new DataTable();
    DataTable DtExperience = new DataTable();
    string AttachPath;
    Genreal g = new Genreal();
    int ii;
    
    protected void Page_Load(object sender, EventArgs e)
     {
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                //BindAllEmp();
                BindPagerList();
                BindDeviceList();
                FillCompanyList();
                FillShiftGroupList();
                fillReportingHead();
                string empid11 = Request.QueryString["Empid"];
                if (Request.QueryString["Empid"] != null)
                {
                    MultiView1.ActiveViewIndex = 1;
                    editemp(int.Parse(empid11));
                }
                else
                {
                    DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
                    DataColumn Location = DtExperience.Columns.Add("Location");
                    DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
                    DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
                    DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
                    DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
                    DataColumn Department = DtExperience.Columns.Add("Department");
                    DataColumn Designation = DtExperience.Columns.Add("Designation");
                }
            }
            else
            {
                var empdata = HR.EmployeeTBs.Where(a => a.EmployeeNo == txtBioId.Text).FirstOrDefault();
                if(empdata != null)
                {               
                    string cname = ddlCompany.SelectedIndex > 0 ? ddlCompany.SelectedItem.Text : "";
                    string path = Server.MapPath("~/EmployeeImages/Faces/" + cname +"/");
                    string FolderPath = path;
                    MakeDirectoryIfExist(FolderPath);
                    if (FileUpload1.HasFile)
                    {
                        string filename = empdata.EmployeeId + "_" + empdata.FName + "_" + empdata.Lname;

                        //string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        string ext = Path.GetExtension(FileUpload1.PostedFile.FileName);


                               
                        AttachPath = filename + ext;
                        FileUpload1.SaveAs(path + AttachPath);
                        //FileUpload1.SaveAs(Server.MapPath(path + AttachPath));

                        image1.ImageUrl = path + AttachPath;
                        lblAttachPath.Text = AttachPath;
                        Session["fileName"] = AttachPath;
                        image1.Visible = true;
                    }
                }
            }
        }
            if (grd_Emp.Rows.Count > 0)
                grd_Emp.HeaderRow.TableSection = TableRowSection.TableHeader;
            BindJqFunctions();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindDepartment(Convert.ToInt32(ddlCompany.SelectedValue));
            fillReportingHead();
            BindDeviceList();
            FillShiftGroupList();
        }
        catch
        {

        }
    }
    protected void dddept_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDesignation(Convert.ToInt32(ddldept.SelectedValue));
    }
   
    protected void btnconP1_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            lblSummary.Visible = false;
            #region Save Employee Profile..

           

            var chkExists = (from d in HR.EmployeeTBs where d.EmployeeNo == txtEmpCode.Text && d.CompanyId==Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId==Convert.ToString(Session["TenantId"]) select d).Distinct();
            var shiftData = HR.MasterShiftTBs.Where(d => d.IsDefault == true && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();

            if (shiftData != null)
            {
                var emailExists = HR.SystemUsersTBs.Where(d => d.Username == txtEmail.Text.Trim()||d.Email==txtEmail.Text.Trim() && d.Active == true).Distinct();

                if (btnconP1.Text == "Save")
                {
                    if (emailExists.Count() > 0)
                    {
                        g.ShowMessage(this.Page, "Email id already present.. Try another email..");
                    }
                    else
                    {
                        if (chkExists.Count() > 0) { g.ShowMessage(this.Page, "Employee code already present.. Try another code.."); }
                        else
                        {
                            //personal Information

                            EmployeeTB emp = new EmployeeTB();
                            emp.EmployeeNo = txtEmpCode.Text;// here employee no is used as machines employee id
                            emp.MachineID = txtBioId.Text;// machine code is used as employee code
                            if (!string.IsNullOrEmpty(Convert.ToString(Session["fileName"])))
                            {
                                emp.Photo = Session["fileName"].ToString();
                                emp.IsFacePresent = true;
                            }
                            else if (string.IsNullOrEmpty(lblAttachPath.Text))
                            {
                                emp.Photo = lblAttachPath.Text;
                                emp.IsFacePresent = false;
                            }
                            else
                            {
                                emp.Photo = lblAttachPath.Text;
                                emp.IsFacePresent = true;
                            }

                            // emp.EmployeeId =int.Parse(txtempid.Text);
                            //  emp.EmployeeNo = txtmachinid.Text;
                            emp.Solitude = ddlsalitude.SelectedIndex;
                            emp.FName = txtfname.Text;
                            emp.MName = txtmname.Text;
                            emp.Lname = txtlname.Text;
                            emp.Gender = RbGender.SelectedValue;
                            //emp.MaritalStaus = rbMaritalStatus.SelectedValue;
                            //emp.BloodGroup = ddlBloodGroup.SelectedItem.Text;
                            if (!string.IsNullOrEmpty(txtbirtdate.Text))
                                emp.BirthDate = DateTime.Parse(txtbirtdate.Text);
                            emp.CompanyId = int.Parse(ddlCompany.SelectedValue);
                            emp.DeptId = int.Parse(ddldept.SelectedValue);
                            emp.DesgId = int.Parse(dddesg.SelectedValue);
                            emp.ContactNo = txtcontactno.Text;
                            emp.AltContactNo = txtaltcontact.Text;
                            emp.Email = txtEmail.Text;
                            emp.EmailId = txtEmail.Text;

                            if (ddlEmpType.SelectedIndex > 0)
                            {
                                emp.Grade = ddlEmpType.SelectedValue;// here grade used as employee type
                            }
                            if (ddlGrade.SelectedIndex > 0)
                            {
                                emp.Grade1 = ddlGrade.SelectedValue;// here grade1 used as employee Grade
                            }
                            emp.Grade_Sequence_No = Convert.ToInt32(string.IsNullOrEmpty(txtsequenceNo.Text) ? null : txtsequenceNo.Text);

                            //emp.EmpCardID = txtCardNo.Text;
                            emp.personalEmail = txtpersonalEmail.Text;
                            //emp.PanNo = txtpannumber.Text;
                            //emp.PassportNo = txtpassportnumber.Text;
                            emp.MachineID = txtBioId.Text;// machine code is used as employee code
                            if (!string.IsNullOrEmpty(txtcurrentjoiningdate.Text))
                                emp.DOJ = DateTime.Parse(txtcurrentjoiningdate.Text);



                            if (!string.IsNullOrEmpty(txtDExDate.Text))
                            {
                                emp.DExpiryDate = Convert.ToDateTime(txtDExDate.Text);//= Convert.ToString(.Value);
                            }

                            emp.WorkLocation = string.IsNullOrEmpty(txtWorkLocation.Text) ? "NA" : txtWorkLocation.Text;

                            emp.Password = string.IsNullOrEmpty(txtemppassword.Text) ? null : txtemppassword.Text;

                            

                            //emp.CurAddress = string.IsNullOrEmpty(txtAddress.Text) ? null : txtAddress.Text;
                            //emp.ProbationPeriod = txtprobation.Text;
                            //if (txtcnfrmdate.Text == null)
                            //{
                            //    emp.ConfirmDate = null;
                            //}
                            //else
                            //{
                            //    emp.ConfirmDate = DateTime.Parse(txtcnfrmdate.Text);
                            //}
                            //emp.NetSalary = txtsalary.Text;
                            //account details
                            emp.PFAccountNo = txtpfaccount.Text;
                            emp.ESICAccountNo = txtesicaccnumber.Text;
                            emp.SalaryAccountNo = txtaccount.Text;
                            emp.BankName = txtbankname.Text;
                            emp.Salarytype = ddlsalarytype.SelectedValue.ToString();
                            emp.ManagerID = ddlReportingHead.SelectedIndex > 0 ? Convert.ToInt32(ddlReportingHead.SelectedValue) : 0;
                            emp.ReportingStatus = ddlReportingHead.SelectedIndex > 0 ? Convert.ToInt32(ddlReportingHead.SelectedValue) : 0;
                            if (shiftData != null)
                            {
                                emp.CurrentShiftId = shiftData.ShiftID;
                            }
                            if(int.Parse(ddlShiftGroup.SelectedValue) > 0)
                            {
                                emp.ShiftGroupId = int.Parse(ddlShiftGroup.SelectedValue);
                            }
                            
                            emp.TenantId = Convert.ToString(Session["TenantId"]);
                            emp.RelivingStatus = 0;
                            emp.IsActive = true;
                            emp.AccessCardNo = txtAccessCard.Text;
                            emp.CurPin = txtPincode.Text;
                            emp.CurAddress = txtAddress.Text;
                            HR.EmployeeTBs.InsertOnSubmit(emp);
                            HR.SubmitChanges();
                            #region Create User Login Credentials
                            SystemUsersTB usersTB = new SystemUsersTB();
                            string key = DateTime.Now.ToString("yyyyMMddhhmmssffffff");
                            string secureuid = SPPasswordHasher.Encrypt(key);
                            string genPass = txtfname.Text.Trim() + "@123";
                            string securepass = SPPasswordHasher.Encrypt(genPass);
                            string displayName = txtfname.Text + " " + txtlname.Text;
                            string userName = txtEmail.Text;
                            string userEmail = txtEmail.Text;
                            usersTB.Active = true;
                            usersTB.Disabled = false;
                            usersTB.DisplayName = displayName;
                            usersTB.Email = userEmail;
                            usersTB.Password = genPass;
                            usersTB.PasswordHash = securepass;
                            usersTB.PhoneNumber = txtcontactno.Text;
                            usersTB.TenantId = Convert.ToString(Session["TenantId"]);
                            usersTB.UID = secureuid;
                            usersTB.Username = userName;
                            usersTB.UserRole = "User";
                            usersTB.EmployeeId = emp.EmployeeId;
                            HR.SystemUsersTBs.InsertOnSubmit(usersTB);
                            HR.SubmitChanges();


                            string dQuery = string.Format(@"insert into UserAssignRollHRMSTB(MenuId,UserId,Assignby,EmployeeId) select M.MenuId,{0} AS UserId,{1} AS AssignBy,{0} AS EmployeeId from MasterMenuHRMSTB M where M.menuid in (4,6,42,44,114,169,172,173,176,180,181,182,184,188,190,193,29,30,118,177)",
                                emp.EmployeeId, Session["EmpId"], emp.EmployeeId);
                            try
                            {
                                DataTable dtD = g.ReturnData(dQuery);
                            }
                            catch(Exception ex) { }
                            #endregion

                            //Educational information
                            DataTable dttt = (DataTable)ViewState["DT"];
                            if (dttt != null)
                            {
                                for (int i = 0; i < dttt.Rows.Count; i++)
                                {
                                    EmpQualificationTB EmpQ = new EmpQualificationTB();
                                    EmpQ.EmployeeId = emp.EmployeeId;
                                    EmpQ.EducationId = int.Parse(dttt.Rows[i]["EducationId"].ToString());
                                    EmpQ.College_School = dttt.Rows[i]["College"].ToString();
                                    EmpQ.University = dttt.Rows[i]["University"].ToString();
                                    EmpQ.YearofPassing = int.Parse(dttt.Rows[i]["YearofPassing"].ToString());
                                    EmpQ.Perc = float.Parse(dttt.Rows[i]["ObtainPercent"].ToString());

                                    HR.EmpQualificationTBs.InsertOnSubmit(EmpQ);
                                    HR.SubmitChanges();

                                }
                            }
                            //Experiance information
                            DataTable dttt1 = (DataTable)ViewState["DtExperience"];
                            if (dttt1 != null)
                            {
                                for (int i = 0; i < dttt1.Rows.Count; i++)
                                {
                                    EmpExprienceTB EmpQ = new EmpExprienceTB();
                                    EmpQ.EmployeeId = emp.EmployeeId;
                                    EmpQ.CompanyName = dttt1.Rows[i]["CompanyName"].ToString();
                                    EmpQ.CompanyAddress = dttt1.Rows[i]["Location"].ToString();
                                    EmpQ.JoiningDate = DateTime.Parse(dttt1.Rows[i]["JoiningDate"].ToString());
                                    EmpQ.RelvDate = DateTime.Parse(dttt1.Rows[i]["RelivingDate"].ToString());
                                    EmpQ.RefPerson = dttt1.Rows[i]["RefPerson"].ToString();

                                    EmpQ.refContactNo = dttt1.Rows[i]["ContactNo"].ToString();
                                    EmpQ.Department = dttt1.Rows[i]["Department"].ToString();
                                    EmpQ.Designation = dttt1.Rows[i]["Designation"].ToString();

                                    HR.EmpExprienceTBs.InsertOnSubmit(EmpQ);
                                    HR.SubmitChanges();
                                    Clear1();

                                }
                            }
                            g.ShowMessage(this.Page, "Employee added  Successfully");
                        }
                    }
                }
                else
                {
                    emailExists = emailExists.Where(d => d.EmployeeId != int.Parse(lblempid.Text)).Distinct();
                    chkExists = chkExists.Where(d => d.EmployeeId != int.Parse(lblempid.Text)).Distinct();
                    if (emailExists.Count() > 0) { g.ShowMessage(this.Page, "Email id is already present.. Try another email id.."); }
                    else
                    {
                        if (chkExists.Count() > 0) { g.ShowMessage(this.Page, "Employee coode already present.. Try another code.."); }
                        else
                        {
                            #region Update Profile......
                            EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == int.Parse(lblempid.Text)).First();
                            MT.EmployeeNo = txtEmpCode.Text;
                            if (!string.IsNullOrEmpty(Convert.ToString(Session["fileName"])))
                            {                          
                                MT.Photo = Session["fileName"].ToString();
                                MT.IsFacePresent = true;
                            }
                            else if (string.IsNullOrEmpty(lblAttachPath.Text))
                            {
                                MT.Photo = lblAttachPath.Text;
                                MT.IsFacePresent = false;
                            }
                            else
                            {
                                MT.Photo = lblAttachPath.Text;
                                MT.IsFacePresent = true;
                            }
                            MT.Solitude = Convert.ToInt32(ddlsalitude.SelectedIndex);
                            MT.MachineID = txtBioId.Text;// machine code is used as employee code
                            if (MT.FName == "")
                            {
                                MT.FName = txtfname.Text;
                            }
                            else
                            {
                                if (MT.FName != null)
                                {
                                    MT.FName = txtfname.Text;
                                }
                                else
                                {
                                    txtfname.Text = MT.FName;
                                }
                            }
                            if (MT.MName == "")
                            {
                                MT.MName = txtmname.Text;
                            }
                            else
                            {
                                if (MT.MName != null)
                                {
                                    MT.MName = txtmname.Text;
                                }
                                else
                                {
                                    txtmname.Text = MT.MName;
                                }
                            }
                            if (MT.Lname == "")
                            {
                                MT.Lname = txtlname.Text;
                            }
                            else
                            {
                                if (MT.Lname != null)
                                {
                                    MT.Lname = txtlname.Text;
                                }
                                else
                                {
                                    txtlname.Text = MT.Lname;
                                }
                            }
                            if (!string.IsNullOrEmpty(txtbirtdate.Text))
                            {
                                MT.BirthDate = Convert.ToDateTime(txtbirtdate.Text);
                            }
                            MT.Gender = RbGender.SelectedValue;

                            if (MT.Email == "")
                            {
                                MT.Email = txtEmail.Text;
                                MT.EmailId = txtEmail.Text;
                            }
                            else
                            {
                                if (MT.Email != "")
                                {
                                    MT.Email = txtEmail.Text;
                                }
                                else
                                {
                                    txtEmail.Text = MT.Email;
                                }
                            }
                            // for Reporting Head
                            MT.ManagerID = ddlReportingHead.SelectedIndex > 0 ? Convert.ToInt32(ddlReportingHead.SelectedValue) : 0;
                            MT.ReportingStatus = ddlReportingHead.SelectedIndex > 0 ? Convert.ToInt32(ddlReportingHead.SelectedValue) : 0;

                            //if (!string.IsNullOrEmpty(MT.Grade))
                            //{
                            //    ddlEmpType.SelectedValue=MT.Grade;// here grade used as employee type
                            //}


                            MT.WorkLocation = string.IsNullOrEmpty(txtWorkLocation.Text) ? "NA" : txtWorkLocation.Text;
                            MT.Password = string.IsNullOrEmpty(txtemppassword.Text) ? null : txtemppassword.Text;

                            if (MT.CurAddress == "")
                            {
                                MT.CurAddress = txtAddress.Text;                               
                            }
                            else
                            {
                                if (MT.CurAddress != "")
                                {
                                    MT.CurAddress = txtAddress.Text;
                                }
                                else
                                {
                                    txtAddress.Text = MT.CurAddress;
                                }
                            }

                            if (MT.Grade == "")
                            {
                                MT.Grade = ddlEmpType.SelectedValue;
                            }
                            else
                            {
                                if (MT.Grade != "")
                                {
                                    MT.Grade = ddlEmpType.SelectedValue;
                                }
                                else
                                {
                                    ddlEmpType.SelectedValue = MT.Grade;
                                }
                            }

                            if (MT.Grade1 == "")
                            {
                                MT.Grade1 = ddlGrade.SelectedValue;
                            }
                            else
                            {
                                if (MT.Grade1 != "")
                                {
                                    MT.Grade1 = ddlGrade.SelectedValue;
                                }
                                else
                                {
                                    ddlGrade.SelectedValue = MT.Grade1;
                                }
                            }

                            MT.Grade_Sequence_No = Convert.ToInt32(string.IsNullOrEmpty(txtsequenceNo.Text) ? null : txtsequenceNo.Text);



                            if (MT.personalEmail == "")
                            {
                                MT.personalEmail = txtpersonalEmail.Text;
                            }
                            else
                            {
                                if (MT.personalEmail != "")
                                {
                                    MT.personalEmail = txtpersonalEmail.Text;
                                }
                                else
                                {
                                    txtpersonalEmail.Text = MT.personalEmail;
                                }
                            }

                            if (MT.CompanyId.ToString() == "")
                            {
                                MT.CompanyId = int.Parse(ddlCompany.SelectedValue);
                            }
                            else
                            {
                                if (MT.CompanyId.ToString() != "")
                                {
                                    MT.CompanyId = int.Parse(ddlCompany.SelectedValue);
                                }
                                else
                                {
                                    ddlCompany.SelectedValue = MT.CompanyId.ToString();
                                }
                            }

                            //fillDept(int.Parse(MT.CompanyId.ToString()));

                            if (MT.DeptId.ToString() == "")
                            {
                                MT.DeptId = int.Parse(ddldept.SelectedValue);
                            }
                            else
                            {
                                if (MT.DeptId.ToString() != "")
                                {
                                    MT.DeptId = int.Parse(ddldept.SelectedValue);
                                }
                                else
                                {
                                    ddldept.SelectedValue = MT.DeptId.ToString();
                                }
                            }



                            if (MT.DesgId.ToString() == "")
                            {
                                if (dddesg.SelectedIndex > 0)
                                    MT.DesgId = Convert.ToInt32(dddesg.SelectedValue);
                            }
                            else
                            {
                                if (MT.DesgId.ToString() != "")
                                {
                                    if (dddesg.SelectedIndex > 0)
                                        MT.DesgId = Convert.ToInt32(dddesg.SelectedValue);
                                }
                                else
                                {
                                    dddesg.SelectedValue = MT.DesgId.ToString();
                                }
                            }


                            if (MT.ContactNo == "")
                            {
                                MT.ContactNo = txtcontactno.Text;
                            }
                            else
                            {
                                if (MT.ContactNo != "")
                                {
                                    MT.ContactNo = txtcontactno.Text;
                                }
                                else
                                {
                                    txtcontactno.Text = MT.ContactNo;
                                }
                            }

                            if (MT.AltContactNo == "")
                            {
                                MT.AltContactNo = txtaltcontact.Text;
                            }
                            else
                            {
                                if (MT.AltContactNo != "")
                                {
                                    MT.AltContactNo = txtaltcontact.Text;
                                }
                                else
                                {
                                    txtaltcontact.Text = MT.AltContactNo;
                                }
                            }
                            if (!string.IsNullOrEmpty(txtcurrentjoiningdate.Text))
                            {
                                MT.DOJ = Convert.ToDateTime(txtcurrentjoiningdate.Text);
                            }
                            if (!string.IsNullOrEmpty(txtDExDate.Text))
                            {
                                MT.DExpiryDate = Convert.ToDateTime(txtDExDate.Text);//= Convert.ToString(.Value);
                            }

                            if (!string.IsNullOrEmpty(txtrelivingDate.Text))
                            {
                                MT.RelivingDate = Convert.ToDateTime(txtrelivingDate.Text);//= Convert.ToString(.Value);
                            }

                            if(ddlRelevingstatus.SelectedValue != null || ddlRelevingstatus.SelectedValue != "")
                            {
                                MT.RelivingStatus =  Convert.ToInt32(ddlRelevingstatus.SelectedValue);
                            }

                            MT.WorkLocation = string.IsNullOrEmpty(txtWorkLocation.Text) ? "NA" : txtWorkLocation.Text;

                            //account details
                            if (MT.PFAccountNo == "")
                            {
                                MT.PFAccountNo = txtpfaccount.Text;
                            }
                            else
                            {
                                if (MT.PFAccountNo != "")
                                {
                                    MT.PFAccountNo = txtpfaccount.Text;
                                }
                                else
                                {
                                    txtpfaccount.Text = MT.PFAccountNo;
                                }
                            }
                            if (MT.ESICAccountNo == "")
                            {
                                MT.ESICAccountNo = txtesicaccnumber.Text;
                            }
                            else
                            {
                                if (MT.ESICAccountNo != "")
                                {
                                    MT.ESICAccountNo = txtesicaccnumber.Text;
                                }
                                else
                                {
                                    txtesicaccnumber.Text = MT.ESICAccountNo;
                                }
                            }

                            if (MT.SalaryAccountNo == "")
                            {
                                MT.SalaryAccountNo = txtaccount.Text;
                            }
                            else
                            {
                                if (MT.SalaryAccountNo != "")
                                {
                                    MT.SalaryAccountNo = txtaccount.Text;
                                }
                                else
                                {
                                    txtaccount.Text = MT.SalaryAccountNo;
                                }
                            }
                            if (MT.BankName == "")
                            {
                                MT.BankName = txtbankname.Text;
                            }
                            else
                            {
                                if (MT.BankName != "")
                                {
                                    MT.BankName = txtbankname.Text;
                                }
                                else
                                {
                                    txtbankname.Text = MT.BankName;
                                }
                            }
                            if ((MT.Salarytype == null) || (MT.Salarytype == "--Select--"))
                            {
                                MT.Salarytype = "--";
                            }
                            else
                            {
                                if ((MT.Salarytype != null) || (MT.Salarytype == "--Select--"))
                                {
                                    MT.Salarytype = "--";
                                }
                                else
                                {
                                    ddlsalarytype.Text = MT.Salarytype;
                                }
                            }

                            if (shiftData != null && MT.CurrentShiftId == null)
                            {
                                MT.CurrentShiftId = shiftData.ShiftID;
                            }

                            if (MT.ShiftGroupId.ToString() == "")
                            {
                                MT.ShiftGroupId = int.Parse(ddlShiftGroup.SelectedValue);
                            }
                            else
                            {
                                if (MT.ShiftGroupId.ToString() != "")
                                {
                                    MT.ShiftGroupId = int.Parse(ddlShiftGroup.SelectedValue);
                                }
                                else
                                {
                                    ddlShiftGroup.SelectedValue = MT.ShiftGroupId.ToString();
                                }
                            }


                            MT.AccessCardNo = txtAccessCard.Text;


                            if (MT.CurPin == "")
                            {
                                MT.CurPin = txtPincode.Text;                                
                            }
                            else
                            {
                                if (MT.CurPin != "")
                                {
                                    MT.CurPin = txtPincode.Text;
                                }
                                else
                                {
                                    txtPincode.Text = MT.CurPin;
                                }
                            }

                            HR.SubmitChanges();


                            #region Update User Login Credentials
                            var systemdata = HR.SystemUsersTBs.Where(a => a.EmployeeId == MT.EmployeeId && a.Active == true).FirstOrDefault();

                            if(systemdata == null)
                            {
                                SystemUsersTB usersTB = new SystemUsersTB();
                                string key = DateTime.Now.ToString("yyyyMMddhhmmssffffff");
                                string secureuid = SPPasswordHasher.Encrypt(key);
                                string genPass = "";
                                if (txtemppassword.Text == "")
                                {
                                     genPass = txtfname.Text.Trim() + "@123";
                                }
                                else
                                {
                                    genPass = txtemppassword.Text;
                                }
                                
                                string securepass = SPPasswordHasher.Encrypt(genPass);
                                string displayName = txtfname.Text + " " + txtlname.Text;
                                string userName = txtEmail.Text;
                                string userEmail = txtEmail.Text;
                                usersTB.Active = true;
                                usersTB.Disabled = false;
                                usersTB.DisplayName = displayName;
                                usersTB.Email = userEmail;
                                usersTB.Password = genPass;
                                usersTB.PasswordHash = securepass;
                                usersTB.PhoneNumber = txtcontactno.Text;
                                usersTB.TenantId = Convert.ToString(Session["TenantId"]);
                                usersTB.UID = secureuid;
                                usersTB.Username = userName;
                                usersTB.UserRole = "User";
                                usersTB.EmployeeId = MT.EmployeeId;
                                HR.SystemUsersTBs.InsertOnSubmit(usersTB);
                                HR.SubmitChanges();
                            }
                            else
                            {
                                systemdata.Email = txtEmail.Text;
                                systemdata.Username = txtEmail.Text;
                                systemdata.Password = txtemppassword.Text;
                                string genPass = txtemppassword.Text;
                                string securepass = SPPasswordHasher.Encrypt(genPass);
                                systemdata.PasswordHash = securepass;
                                systemdata.PhoneNumber = txtcontactno.Text;
                                HR.SubmitChanges();
                            }
                            
                            #endregion


                            if (ViewState["DtExperience"] == null)
                            {
                                DtExperience = new DataTable();
                                DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
                                DataColumn Location = DtExperience.Columns.Add("Location");
                                DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
                                DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
                                DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
                                DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
                                DataColumn Department = DtExperience.Columns.Add("Department");
                                DataColumn Designation = DtExperience.Columns.Add("Designation");
                            }
                            else
                            {
                                DtExperience = (DataTable)ViewState["DtExperience"];
                                DataTable ds = g.ReturnData("delete from EmpExprienceTB where EmployeeId='" + lblempid.Text + "'");
                                for (int i = 0; i < DtExperience.Rows.Count; i++)
                                {
                                    EmpExprienceTB exp = new EmpExprienceTB();
                                    exp.EmployeeId = Convert.ToInt32(lblempid.Text);

                                    exp.CompanyName = DtExperience.Rows[i]["CompanyName"].ToString();
                                    exp.CompanyAddress = DtExperience.Rows[i]["Location"].ToString();
                                    exp.JoiningDate = DateTime.Parse(DtExperience.Rows[i]["JoiningDate"].ToString());
                                    exp.RelvDate = DateTime.Parse(DtExperience.Rows[i]["RelivingDate"].ToString());
                                    exp.RefPerson = DtExperience.Rows[i]["RefPerson"].ToString();

                                    exp.refContactNo = DtExperience.Rows[i]["ContactNo"].ToString();
                                    exp.Department = DtExperience.Rows[i]["Department"].ToString();
                                    exp.Designation = DtExperience.Rows[i]["Designation"].ToString();

                                    HR.EmpExprienceTBs.InsertOnSubmit(exp);
                                    HR.SubmitChanges();
                                    Clear1();

                                }

                            }



                            grdExperience.DataSource = DtExperience;
                            grdExperience.DataBind();

                            ViewState["DT"] = Dt;
                            ViewState["DtExperience"] = DtExperience;
                            //}


                            btnconP1.Text = btnconP2.Text = "Update";
                            BindAllEmp();
                            Clear1();
                            MultiView1.ActiveViewIndex = 0;

                            g.ShowMessage(this.Page, "Employee Updateded  Successfully.");
                            #endregion End Update Profile..
                        }
                    }
                }
            }
            else
            {
                g.ShowMessage(this.Page, "Default shift not present in database against selected company.. Please add default shift first..");
            }
                #endregion End Employee Add....
        }
        else
        {
            lblSummary.Visible = true;
        }
    }

    private void Clear1()
    {
        txtEmployeeCode.Text=txtAccessCard.Text = lblAttachPath.Text = txtfname.Text = txtmname.Text = txtlname.Text = txtbirtdate.Text = txtcontactno.Text = txtaltcontact.Text = txtEmail.Text = txtpersonalEmail.Text = txtcurrentjoiningdate.Text = txtpfaccount.Text = txtesicaccnumber.Text = txtaccount.Text = txtbankname.Text = string.Empty;
        ddldept.SelectedIndex = ddlCompany.SelectedIndex = RbGender.SelectedIndex = dddesg.SelectedIndex = ddlsalarytype.SelectedIndex = -1;

        //BindAllEmp();
        BindPagerList();
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnaddexp_Click(object sender, EventArgs e)
    {
        int cnt = 0;
        if (ViewState["DtExperience"] != null)
        {
            DtExperience = (DataTable)ViewState["DtExperience"];
        }
        else
        {
            DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
            DataColumn Location = DtExperience.Columns.Add("Location");
            DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
            DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
            DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
            DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
            DataColumn Department = DtExperience.Columns.Add("Department");
            DataColumn Designation = DtExperience.Columns.Add("Designation");
        }
        DataRow dr = DtExperience.NewRow();
        dr[0] = txtcompanyname.Text;
        dr[1] = "";// txtlocation.Text; ;
        dr[2] = txtjoiningdate.Text;
        dr[3] = txtreldate.Text;
        dr[4] = txtref.Text;
        dr[5] = txtrefcontactno.Text;
        dr[6] =  txtDept.Text;
        dr[7] = txtDesig.Text;
        if (DtExperience.Rows.Count > 0)
        {
            for (int f = 0; f < DtExperience.Rows.Count; f++)
            {
                string u2 = DtExperience.Rows[f][0].ToString();
                if (u2 == txtcompanyname.Text)
                {
                    cnt++;
                }
            }
            if (cnt > 0)
            {
                g.ShowMessage(this.Page, "Already Exist");
            }
            else
            {
                DtExperience.Rows.Add(dr);
                clearExpirience();
            }
        }
        else
        {
            DtExperience.Rows.Add(dr);
            clearExpirience();
        }
        ViewState["DtExperience"] = DtExperience;

        grdExperience.DataSource = DtExperience;
        grdExperience.DataBind();
    }
    private void clearExpirience()
    {
        txtcompanyname.Text =  txtlocation.Text =   txtjoiningdate.Text =  txtreldate.Text =  txtref.Text =   txtrefcontactno.Text =  txtDept.Text =  txtDesig.Text = string.Empty;
    }

    protected void imgeditExpr_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        DtExperience = (DataTable)ViewState["DtExperience"];
        foreach (DataRow d in DtExperience.Rows)
        {
            if (d[0].ToString() == imgedit.CommandArgument)
            {
                txtcompanyname.Text = d["CompanyName"].ToString();
                txtlocation.Text = d["Location"].ToString();                
                txtreldate.Text = d["RelivingDate"].ToString();
                txtreldate.Text = txtreldate.Text.Replace(" 12:00:00 AM", " ");
                txtjoiningdate.Text = d["JoiningDate"].ToString();
                txtjoiningdate.Text = txtjoiningdate.Text.Replace(" 12:00:00 AM", " ");
                txtref.Text = d["RefPerson"].ToString();
                txtrefcontactno.Text = d["ContactNo"].ToString();
                txtDept.Text = d["Department"].ToString();
                txtDesig.Text = d["Designation"].ToString();
                d.Delete();
                DtExperience.AcceptChanges();
                break;
            }
        }

        grdExperience.DataSource = DtExperience;
        grdExperience.DataBind();
    }
    protected void imgdeleteExpr_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        DtExperience = (DataTable)ViewState["DtExperience"];
        foreach (DataRow d in DtExperience.Rows)
        {
            if (d[0].ToString() == imgdelete.CommandArgument)
            {
                d.Delete();
                DtExperience.AcceptChanges();
                break;
            }
        }
        grdExperience.DataSource = DtExperience;
        grdExperience.DataBind();
        clearExpirience();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        txtEmail.Attributes.Remove("readonly");
        txtpersonalEmail.Attributes.Remove("readonly");
        txtEmpCode.Attributes.Remove("readonly");
        MultiView1.ActiveViewIndex = 1;
        txtWorkLocation.Text = "NA";
    }
    public void editemp(int empid)
    {
        string EmployeeId = Convert.ToString(empid);
        lblempid.Text = EmployeeId;
        EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == int.Parse(EmployeeId)).First();
        if (MT.Solitude == 0)
        {
            ddlsalitude.SelectedIndex = 0;
        }
        if (MT.Solitude == 1)
        {
            ddlsalitude.SelectedIndex = 1;
        }
        if (MT.Solitude == 2)
        {
            ddlsalitude.SelectedIndex = 2;
        }
        if (MT.Solitude == 3)
        {
            ddlsalitude.SelectedIndex = 3;
        }
        //ddlsalitude.SelectedValue =Convert.ToString(MT.Solitude);
        txtfname.Text = MT.FName;
        txtmname.Text = MT.MName;
        txtlname.Text = MT.Lname;
        if (MT.BirthDate != null)
        {
            if (!string.IsNullOrEmpty(MT.BirthDate.Value.ToString()))
            {
                txtbirtdate.Text = MT.BirthDate.Value.ToShortDateString();
            }
        }
        RbGender.Text = MT.Gender;
        lblAttachPath.Text = MT.Photo;
        if (MT.CompanyId != null)
        {
            ddlCompany.SelectedValue = MT.CompanyId.ToString();
            BindDepartment(int.Parse(MT.CompanyId.ToString()));
            ddldept.SelectedValue = MT.DeptId.ToString();
            fillDesignation(int.Parse(MT.DeptId.ToString()));
            BindDeviceList();
        }
        fillReportingHead();
        ddlReportingHead.SelectedValue = MT.ManagerID.HasValue ? Convert.ToString(MT.ManagerID) : "0";
       
        if(MT.Photo != null)
        {
            image1.ImageUrl = "~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/" + lblAttachPath.Text;
            image1.Visible = true;
        }
        else
        {
            image1.Visible = false;
            btnRemoveFace.Visible = false;
        }
       
        txtEmail.Text = MT.Email;
        txtPincode.Text = MT.CurPin;
        txtpersonalEmail.Text = MT.EmailId;
        txtpersonalEmail.Attributes.Add("readonly", "readonly");
        txtAccessCard.Text = MT.AccessCardNo;
        txtBioId.Text = MT.MachineID;
        if (!string.IsNullOrEmpty(MT.MachineID))
            txtEmpCode.Attributes.Add("readonly", "readonly");
        txtcurrentjoiningdate.Attributes.Add("readonly", "readonly");
        dddesg.SelectedValue = MT.DesgId.ToString();
        txtcontactno.Text = MT.ContactNo;
        txtaltcontact.Text = MT.AltContactNo;
        if (MT.DOJ != null)
        {
            txtcurrentjoiningdate.Text = MT.DOJ.Value.ToShortDateString();
        }
        if (MT.DExpiryDate != null)
        {
            txtDExDate.Text = Convert.ToString(MT.DExpiryDate.Value);
        }
        //account details
        txtpfaccount.Text = MT.PFAccountNo;
        txtesicaccnumber.Text = MT.ESICAccountNo;
        txtaccount.Text = MT.SalaryAccountNo;
        txtbankname.Text = MT.BankName;
        if ((MT.Salarytype == "") || (MT.Salarytype == "--Select--") || (MT.Salarytype == "--"))
        {
            MT.Salarytype = "--";
        }
        else
        {
            ddlsalarytype.Text = MT.Salarytype;
        }
        //current Adderess
        
        var Exp = (from m in HR.EmpExprienceTBs
                   where m.EmployeeId == int.Parse(EmployeeId)
                   select new
                   {
                       m.CompanyName,
                       Location = m.CompanyAddress,
                       JoiningDate = m.JoiningDate.Value,
                       RelivingDate = m.RelvDate,
                       RefPerson = m.RefPerson,
                       ContactNo = m.refContactNo,
                       Department = m.Department,
                       Designation = m.Designation

                   }).ToList();
        if (Exp.Count() > 0)
        {

            grdExperience.DataSource = Exp;
            grdExperience.DataBind();
            // ViewState["DtExperience"] = Exp;
            foreach (var item in Exp)
            {
                //Dt = (DataTable)ViewState["DT"];
                if (ViewState["DtExperience"] == null)
                {
                    Dt = new DataTable();
                    DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
                    DataColumn Location = DtExperience.Columns.Add("Location");
                    DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
                    DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
                    DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
                    DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
                    DataColumn Department = DtExperience.Columns.Add("Department");
                    DataColumn Designation = DtExperience.Columns.Add("Designation");
                }
                else
                {
                    Dt = (DataTable)ViewState["DtExperience"];
                }
                DataTable ds1 = g.ReturnData("select CompanyName,CompanyAddress as Location, JoiningDate,RelvDate as RelivingDate, RefPerson,refContactNo as ContactNo, Department,Designation from  EmpExprienceTB where EmpExprienceTB.EmployeeId = '" + EmployeeId + "'");
                DataRow dr = ds1.NewRow();

                dr[0] = ds1.Rows[0]["CompanyName"].ToString();
                dr[1] = ds1.Rows[0]["Location"].ToString();
                dr[2] = ds1.Rows[0]["JoiningDate"].ToString();
                dr[3] = ds1.Rows[0]["RelivingDate"].ToString();
                dr[4] = ds1.Rows[0]["RefPerson"].ToString();
                dr[5] = ds1.Rows[0]["ContactNo"].ToString();
                dr[6] = ds1.Rows[0]["Department"].ToString();
                dr[7] = ds1.Rows[0]["Designation"].ToString();

                //ds.Rows.Add(dr);
                ViewState["DtExperience"] = ds1;
            }
        }

        btnconP1.Text = btnconP2.Text = "Update";
        //BindAllEmp();
        MultiView1.ActiveViewIndex = 1;
        

    }

    protected void btnRemoveFace_Click(object sender, EventArgs e)
    {
        EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == int.Parse(lblempid.Text)).First();

        if(MT.Photo != "")
        {
            File.Delete(Server.MapPath("~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/" + MT.Photo));

        }

        MT.Photo = null;
        MT.IsFacePresent = false;
        HR.SubmitChanges();

        image1.Visible = false;
        btnRemoveFace.Visible = false;

        g.ShowMessage(this.Page, "Employee Face Remove Sucessfully");
              

    }



   

    //protected void btnUpload_Click(object sender, EventArgs e)
    //{

    //    string cname = ddlCompany.SelectedIndex > 0 ? ddlCompany.SelectedItem.Text : "";
    //    string path = Server.MapPath("~/EmployeeImages/Faces/" + cname + "/");
    //    string FolderPath = path;
    //    MakeDirectoryIfExist(FolderPath);
    //    if (FileUpload1.HasFile)
    //    {
    //        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
    //        string ext = Path.GetExtension(FileUpload1.PostedFile.FileName);
    //        FileUpload1.SaveAs(Server.MapPath(path + filename));
    //        AttachPath = filename;
    //        image1.ImageUrl = path + AttachPath;
    //        lblAttachPath.Text = AttachPath;
    //        image1.Visible = true;

    //        EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == int.Parse(lblempid.Text)).First();

    //        MT.Photo = filename;
    //        MT.IsFacePresent = true;
    //        HR.SubmitChanges();

    //        g.ShowMessage(this.Page, "Employee Face Uploaded Sucessfully");
    //    }
    //}

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        //BindAllEmp();
        BindPagerList();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear1();
    }
    
    protected void btnaddexp_Click1(object sender, EventArgs e)
    {
        int cnt = 0;
        if (ViewState["DtExperience"] != null)
        {
            DtExperience = (DataTable)ViewState["DtExperience"];
        }
        else
        {
            DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
            DataColumn Location = DtExperience.Columns.Add("Location");
            DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
            DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
            DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
            DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
            DataColumn Department = DtExperience.Columns.Add("Department");
            DataColumn Designation = DtExperience.Columns.Add("Designation");
        }
        DataRow dr = DtExperience.NewRow();
        dr[0] = txtcompanyname.Text;
        dr[1] =  txtlocation.Text; ;
        dr[2] = txtjoiningdate.Text;
        dr[3] = txtreldate.Text;
        dr[4] = txtref.Text;
        dr[5] = txtrefcontactno.Text;
        dr[6] = txtDept.Text;
        dr[7] = txtDesig.Text;
        if (DtExperience.Rows.Count > 0)
        {
            for (int f = 0; f < DtExperience.Rows.Count; f++)
            {
                string u2 = DtExperience.Rows[f][0].ToString();
                if (u2 == txtcompanyname.Text)
                {
                    cnt++;
                }
            }
            if (cnt > 0)
            {
                g.ShowMessage(this.Page, "Already Exist");
            }
            else
            {
                DtExperience.Rows.Add(dr);
                clearExpirience();
            }
        }
        else
        {
            DtExperience.Rows.Add(dr);
            clearExpirience();
        }
        ViewState["DtExperience"] = DtExperience;

        grdExperience.DataSource = DtExperience;
        grdExperience.DataBind();
    }
    
    protected void btnpopsave_Click1(object sender, EventArgs e)
    {
        EmployeeTB emp = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmployeeId3"])).First();
        emp.RelivingDate = g.GetCurrentDateTime();
        emp.RelivingStatus = 0;
        HR.SubmitChanges();
        g.ShowMessage(this.Page, "Employee relieved Details Save Sucessfully");
        Response.Redirect("Employee.aspx");
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void grd_Emp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Emp.Rows.Count > 0)
        {
            grd_Emp.UseAccessibleHeader = true;
            grd_Emp.HeaderRow.TableSection =TableRowSection.TableHeader;
        }
    }
    protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentSearch(Convert.ToInt32(ddlCompanyList.SelectedValue));
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlCompanyList.SelectedIndex = 0;
        ddlDepartment.Items.Clear();
        ddlEmployeeList.Items.Clear();
        //BindAllEmp();
        BindPagerList();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployeeList();
    }
    protected void lbtnView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton imgemp = (LinkButton)sender;
            int EmployeeID = Convert.ToInt32(imgemp.CommandArgument);
            Session["EmpBasicId"] = EmployeeID;
            Response.Redirect("EmployeeDetails.aspx?id=" + EmployeeID.ToString());
        }
        catch (Exception exc)
        {
            throw;
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        Session["EditPage"] = "3";
        LinkButton Lnk = (LinkButton)sender;
        string EmployeeId = Lnk.CommandArgument;
        lblempid.Text = EmployeeId;

        
        EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == int.Parse(EmployeeId)).First();
        txtEmpCode.Text = MT.EmployeeNo;
        if (MT.Solitude == 0)
        {
            ddlsalitude.SelectedIndex = 0;
        }
        if (MT.Solitude == 1)
        {
            ddlsalitude.SelectedIndex = 1;
        }
        if (MT.Solitude == 2)
        {
            ddlsalitude.SelectedIndex = 2;
        }
        if (MT.Solitude == 3)
        {
            ddlsalitude.SelectedIndex = 3;
        }
        txtempID.Value = MT.EmployeeId.ToString();
        txtfname.Text = MT.FName;
        txtmname.Text = MT.MName;
        txtlname.Text = MT.Lname;
        
        txtAddress.Text = MT.CurAddress;
        RbGender.SelectedIndex = string.IsNullOrEmpty(MT.Gender) ? 0 : MT.Gender.ToUpper().Equals("MALE") ? 0 : 1;
        //image1.Visible = true;
        txtEmail.Text = MT.Email;
        //txtEmail.Attributes.Add("readonly", "readonly");
        txtpersonalEmail.Text = MT.personalEmail;
        //txtpersonalEmail.Attributes.Add("readonly", "readonly");
        txtBioId.Text = MT.MachineID;
        txtEmpCode.Attributes.Add("readonly", "readonly");
        txtAccessCard.Text = MT.AccessCardNo;

        if(!string.IsNullOrEmpty(MT.WorkLocation))
        {
            txtWorkLocation.Text = MT.WorkLocation;
        }
        else
        {
            txtWorkLocation.Text = "NA";
        }

        if (!string.IsNullOrEmpty(MT.CurPin))
        {
            txtPincode.Text = MT.CurPin;
        }
        else
        {
            txtPincode.Text = null;
        }

        if (MT.BirthDate != null)
        {
            txtbirtdate.Text = Convert.ToString(MT.BirthDate.Value.ToString("yyyy-MM-dd"));
        }

        if(MT.Grade != null && MT.Grade != "None")
        {
            ddlEmpType.SelectedValue = MT.Grade;
        }

        if (MT.Grade1 != null && MT.Grade1 != "None")
        {
            ddlGrade.SelectedValue = MT.Grade1;
        }

        if(MT.Grade_Sequence_No != null)
        {
            txtsequenceNo.Text = MT.Grade_Sequence_No.ToString();
        }
        //txtbirtdate.Text = Convert.ToDateTime(MT.BirthDate).ToString("dd-MM-yyyy");

        //if (!string.IsNullOrEmpty(MT.BirthDate.ToString()))
        //{
        //    txtbirtdate.Text = MT.BirthDate;          
        //    txtbirtdate.Attributes.Add("value", MT.BirthDate);
        //}
        //else
        //{
        //    txtbirtdate.Text = null;
        //}


        if (!string.IsNullOrEmpty(MT.Password))
        {
            txtemppassword.Text = MT.Password;
            //txtemppassword.Attributes["type"] = "password";
            
            //txtemppassword.Attributes.Add("value", MT.Password);
        }
        else
        {
            txtemppassword.Text = null;
        }

        if(MT.IsFingerPrintPresent == true)
        {
            divFingerprint.Visible = true;
            image2.ImageUrl = "~/Images/Fingerprint.jfif";

            var fingerdata = HR.EmployeeFingerprintTBs.Where(a => a.Employee_Id == int.Parse(EmployeeId)).ToList();
            txtFingerprintqty.Text = fingerdata[0].Finger_Quality;
            ddlfingerNo.SelectedValue = "1";
        }

        

        if (!string.IsNullOrEmpty(MT.Grade))
        {
            ddlEmpType.SelectedValue = MT.Grade;
        }
        if (MT.DExpiryDate.HasValue)
        {
            txtDExDate.Text = Convert.ToString(MT.DExpiryDate.Value.ToString("yyyy-MM-dd"));
        }
        if (MT.RelivingDate.HasValue)
        {
            txtrelivingDate.Text = Convert.ToString(MT.RelivingDate.Value.ToString("yyyy-MM-dd"));
        }
        if(MT.RelivingStatus != null)
        {
            ddlRelevingstatus.SelectedValue = MT.RelivingStatus.ToString();
        }


        try
        {
            ddlCompany.SelectedValue = MT.CompanyId.ToString();
            BindDepartment(int.Parse(MT.CompanyId.ToString()));
            ddldept.SelectedValue = MT.DeptId.ToString();
            BindShiftGroup(int.Parse(MT.CompanyId.ToString()));
            if (MT.ShiftGroupId != null || MT.ShiftGroupId != 0)
            {
                ddlShiftGroup.SelectedValue = MT.ShiftGroupId.ToString();
            }
            else
            {
                FillShiftGroupList();
            }
            fillDesignation(int.Parse(MT.DeptId.ToString()));
            BindDeviceList();
        }
        catch (Exception ex) { }
        lblAttachPath.Text = MT.Photo;

        if (MT.Photo != null)
        {
            image1.ImageUrl = "~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/" + lblAttachPath.Text;
            image1.Visible = true;
            btnRemoveFace.Visible = true;
        }
        else
        {
            image1.Visible = false;
            btnRemoveFace.Visible = false;
        }
        //image1.ImageUrl = "~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/" + lblAttachPath.Text;
        if (MT.ReportingStatus == null || MT.ReportingStatus.ToString() == "")
        {
            fillReportingHead();
        }
        else
        {
            try
            {
                fillReportingHead();
                ddlReportingHead.SelectedValue = Convert.ToString(MT.ManagerID);
            }
            catch (Exception)
            {
                fillReportingHead();
            }
        }
        if (MT.DesgId != null)
        {
            dddesg.SelectedValue = Convert.ToString(MT.DesgId);
        }
        if (MT.DExpiryDate != null)
        {
            txtDExDate.Text = MT.DExpiryDate.Value.ToString("yyyy-MM-dd");// Convert.ToString(MT.DExpiryDate.Value);
        }
        txtcontactno.Text = MT.ContactNo;
        txtaltcontact.Text = MT.AltContactNo;
        if (MT.DOJ != null)
        {
            txtcurrentjoiningdate.Text = Convert.ToString(MT.DOJ.Value.ToString("yyyy-MM-dd"));
        }
        //txtcurrentjoiningdate.Text = Convert.ToDateTime(MT.DOJ).ToShortDateString();
        //account details
        txtpfaccount.Text = MT.PFAccountNo;
        txtesicaccnumber.Text = MT.ESICAccountNo;
        txtaccount.Text = MT.SalaryAccountNo;
        txtbankname.Text = MT.BankName;
        if ((MT.Salarytype == "") || (MT.Salarytype == "--Select--") || (MT.Salarytype == "--"))
        {
            MT.Salarytype = "--";
        }
        else
        {
            ddlsalarytype.Text = MT.Salarytype;
        }
        var Exp = (from m in HR.EmpExprienceTBs
                   where m.EmployeeId == int.Parse(EmployeeId)
                   select new
                   {
                       m.CompanyName,
                       Location = m.CompanyAddress,
                       JoiningDate = m.JoiningDate.Value,
                       RelivingDate = m.RelvDate,
                       RefPerson = m.RefPerson,
                       ContactNo = m.refContactNo,
                       Department = m.Department,
                       Designation = m.Designation

                   }).ToList();
        grdExperience.DataSource = Exp;
        grdExperience.DataBind();

        foreach (var item in Exp)
        {
            if (ViewState["DtExperience"] == null)
            {
                Dt = new DataTable();
                DataColumn CompanyName = DtExperience.Columns.Add("CompanyName");
                DataColumn Location = DtExperience.Columns.Add("Location");
                DataColumn JoiningDate = DtExperience.Columns.Add("JoiningDate");
                DataColumn RelivingDate = DtExperience.Columns.Add("RelivingDate");
                DataColumn RefPerson = DtExperience.Columns.Add("RefPerson");
                DataColumn ContactNo = DtExperience.Columns.Add("ContactNo");
                DataColumn Department = DtExperience.Columns.Add("Department");
                DataColumn Designation = DtExperience.Columns.Add("Designation");
            }
            else
            {
                Dt = (DataTable)ViewState["DtExperience"];
            }
            DataTable ds1 = g.ReturnData("select CompanyName,CompanyAddress as Location, JoiningDate,RelvDate as RelivingDate, RefPerson,refContactNo as ContactNo, Department,Designation from  EmpExprienceTB where EmpExprienceTB.EmployeeId = '" + EmployeeId + "'");
            DataRow dr = ds1.NewRow();

            dr[0] = ds1.Rows[0]["CompanyName"].ToString();
            dr[1] = ds1.Rows[0]["Location"].ToString();
            dr[2] = ds1.Rows[0]["JoiningDate"].ToString();
            dr[3] = ds1.Rows[0]["RelivingDate"].ToString();
            dr[4] = ds1.Rows[0]["RefPerson"].ToString();
            dr[5] = ds1.Rows[0]["ContactNo"].ToString();
            dr[6] = ds1.Rows[0]["Department"].ToString();
            dr[7] = ds1.Rows[0]["Designation"].ToString();
            
            ViewState["DtExperience"] = ds1;
        }
        btnconP1.Text = btnconP2.Text = "Update";
        MultiView1.ActiveViewIndex = 1;

        //btnRemoveFace.Visible = true;
    }

    protected void lbtnRelease_Click(object sender, EventArgs e)
    {
        LinkButton lnkReliveDate = (LinkButton)sender;
        int EmployeeId = Convert.ToInt32(lnkReliveDate.CommandArgument);
        Session["EmployeeId3"] = EmployeeId;

        //tblReliveingEmployee.Visible = true;
        //modRelive.Show();
    }
    #region Events

    #endregion
    #region Methods
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    private void FillCompanyList()
    {

        ddlCompanyList.Items.Clear();
        ddlCompany.Items.Clear();
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
        if (data != null && data.Count() > 0)
        {
            ddlCompanyList.DataSource = data;
            ddlCompanyList.DataTextField = "CompanyName";
            ddlCompanyList.DataValueField = "CompanyId";
            ddlCompanyList.DataBind();
            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
        }
        ddlCompanyList.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    private void FillShiftGroupList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlShiftGroup.Items.Clear();       
        List<ShiftGroupTB> data = HR.ShiftGroupTBs.Where(a => a.CompanyId == cId).ToList();
        if (data != null && data.Count() > 0)
        {
            ddlShiftGroup.DataSource = data;
            ddlShiftGroup.DataTextField = "ShiftGroupName";
            ddlShiftGroup.DataValueField = "ShiftGroupId";
            ddlShiftGroup.DataBind();            
        }
        ddlShiftGroup.Items.Insert(0, new ListItem("--Select--", "0"));       
    }

    private void BindShiftGroup(int companyid)
    {
        ddlShiftGroup.Items.Clear();
        var data = (from dt in HR.ShiftGroupTBs
                    where dt.CompanyId == companyid
                    select dt).OrderBy(d => d.ShiftGroupName);
        if (data != null && data.Count() > 0)
        {

            ddlShiftGroup.DataSource = data;
            ddlShiftGroup.DataTextField = "ShiftGroupName";
            ddlShiftGroup.DataValueField = "ShiftGroupId";
            ddlShiftGroup.DataBind();
        }
        ddlShiftGroup.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void BindDeviceList()
    {
        ddlDevice.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            var data = (from d in db.DevicesTBs
                        where d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
            ddlDevice.DataSource = data;
            ddlDevice.DataTextField = "DeviceName";
            ddlDevice.DataValueField = "DeviceAccountId";
            ddlDevice.DataBind();
        }
        ddlDevice.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void BindDepartment(int companyid)
    {
        ddldept.Items.Clear();
        var data = (from dt in HR.MasterDeptTBs
                    where dt.Status == 1 && dt.CompanyId == companyid
                    select dt).OrderBy(d => d.DeptName);
        if (data != null && data.Count() > 0)
        {

            ddldept.DataSource = data;
            ddldept.DataTextField = "DeptName";
            ddldept.DataValueField = "DeptID";
            ddldept.DataBind();
        }
        ddldept.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindDepartmentSearch(int companyid)
    {
        ddlDepartment.Items.Clear();
        var data = (from dt in HR.MasterDeptTBs
                    where dt.Status == 1 && dt.CompanyId == companyid
                    select dt).OrderBy(d => d.DeptName);
        if (data != null && data.Count() > 0)
        {

            ddlDepartment.DataSource = data;
            ddlDepartment.DataTextField = "DeptName";
            ddlDepartment.DataValueField = "DeptID";
            ddlDepartment.DataBind();
        }
        ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void FillEmployeeList()
    {
        try
        {
            ddlEmployeeList.Items.Clear();
            int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from d in HR.EmployeeTBs
                        where (d.RelivingStatus == null||d.RelivingStatus==0) && (d.IsActive == null || d.IsActive==true)
                        select new
                        {
                            d.EmployeeId,
                            Name = d.FName + ' ' + d.Lname,
                            d.CompanyId,
                            d.DeptId
                        }).OrderBy(d => d.Name).Distinct();
            if (cId > 0)
            {
                data = data.Where(d => d.CompanyId == cId).Distinct();
            }
            if (dId > 0)
            {
                data = data.Where(d => d.DeptId == dId).Distinct();
            }

            if (data != null && data.Count() > 0)
            {
                ddlEmployeeList.DataSource = data;
                ddlEmployeeList.DataTextField = "Name";
                ddlEmployeeList.DataValueField = "EmployeeId";
                ddlEmployeeList.DataBind();
            }
            ddlEmployeeList.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        catch (Exception ex) { }
    }
    private void fillReportingHead()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlReportingHead.Items.Clear();
        var data = (from d in HR.EmployeeTBs
                    where d.IsActive == true && d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                    select new { d.EmployeeId, EmpName = d.FName + " " + d.Lname }).ToList();
        if (data != null && data.Count() > 0)
        {
            ddlReportingHead.DataSource = data;
            ddlReportingHead.DataTextField = "EmpName";
            ddlReportingHead.DataValueField = "EmployeeId";
            ddlReportingHead.DataBind();
        }
        ddlReportingHead.Items.Insert(0, new ListItem("Admin", "0"));
    }
    private void fillDesignation(int deptid)
    {
        dddesg.Items.Clear();
        var data = (from dt in HR.MasterDesgTBs
                    where dt.Status == 1 && dt.DeptID == deptid
                    select dt).OrderBy(d => d.DesigName);
        if (data != null && data.Count() > 0)
        {
            dddesg.DataSource = data;
            dddesg.DataTextField = "DesigName";
            dddesg.DataValueField = "DesigID";
            dddesg.DataBind();            
        }
        dddesg.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void MakeDirectoryIfExist(string NewPath)
    {
        if (!Directory.Exists(NewPath))
        {
            Directory.CreateDirectory(NewPath);
        }
    }
    public void BindAllEmp()
    {
        //bool Status = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        bool Status = g.CheckSuperAdmin(Convert.ToString(Session["UserId"]));

        string query = string.Format(@"SELECT   TOP 5000     E.FName, E.Lname, E.EmployeeId, E.BirthDate, E.Email, E.DOJ AS DOJ1, E.PanNo, E.ContactNo, E.PassportNo, D.DeptName, C.CompanyName, E.FName + ' ' + E.Lname AS EmpName, E.RelivingDate, E.EmployeeNo, 
                         E.personalEmail, E.CompanyId, E.DeptId, E.DesgId, E.MachineID, E.DExpiryDate
FROM            EmployeeTB AS E INNER JOIN
                         MasterDeptTB AS D ON E.DeptId = D.DeptID INNER JOIN
                         CompanyInfoTB AS C ON E.CompanyId = C.CompanyId WHERE E.IsActive=1 AND C.TenantId='{0}'", Convert.ToString(Session["TenantId"]));

        //var EmpData = (from d in HR.EmployeeTBs
        //               join n in HR.MasterDeptTBs on d.DeptId equals n.DeptID
        //               join c in HR.CompanyInfoTBs on d.CompanyId equals c.CompanyId
        //               where c.TenantId == Convert.ToString(Session["TenantId"]) && d.IsActive==true
        //               select new
        //               {
        //                   Name = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
        //                   Name1 = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
        //                   d.FName,
        //                   d.Lname,
        //                   d.EmployeeId,
        //                   d.BirthDate,
        //                   d.Email,
        //                   DOJ1 = d.DOJ,
        //                   d.PanNo,
        //                   d.ContactNo,
        //                   d.PassportNo,
        //                   n.DeptName,
        //                   c.CompanyName,
        //                   EmpName = d.FName + " " + d.Lname,
        //                   d.RelivingDate,
        //                   d.EmployeeNo,
        //                   d.personalEmail,
        //                   d.CompanyId,
        //                   d.DeptId,
        //                   d.DesgId,
        //                   d.MachineID,
        //                   d.DExpiryDate
        //               }).ToList();

        //if (Status == true)
        //{
        //    btnadd.Visible = true;

        //    if (ddlCompanyList.SelectedIndex > 0)
        //    {
        //        EmpData = EmpData.Where(d => d.CompanyId == Convert.ToInt32(ddlCompanyList.SelectedValue)).ToList();
        //    }
        //    if (ddlDepartment.SelectedIndex > 0)
        //    {
        //        EmpData = EmpData.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)).ToList();
        //    }
        //    if (ddlEmployeeList.SelectedIndex > 0)
        //    {
        //        EmpData = EmpData.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployeeList.SelectedValue)).ToList();
        //    }

        //    if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
        //    {
        //        EmpData = EmpData.Where(d => d.EmployeeNo.Contains(txtEmployeeCode.Text.Trim())).ToList();
        //    }
        //    grd_Emp.DataSource = EmpData;
        //    grd_Emp.DataBind();
        //}
        //else
        //{
        //    btnadd.Visible = true;
        //    if (ddlCompanyList.SelectedIndex > 0)
        //    {
        //        EmpData = EmpData.Where(d => d.CompanyId == Convert.ToInt32(ddlCompanyList.SelectedValue)).ToList();
        //    }
        //    if (ddlDepartment.SelectedIndex > 0)
        //    {
        //        EmpData = EmpData.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)).ToList();
        //    }
        //    if (ddlEmployeeList.SelectedIndex > 0)
        //    {
        //        EmpData = EmpData.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployeeList.SelectedValue)).ToList();
        //    }

        //    if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
        //    {
        //        EmpData = EmpData.Where(d => d.EmployeeNo.Contains(txtEmployeeCode.Text.Trim())).ToList();
        //    }

        //    grd_Emp.DataSource = EmpData;
        //    grd_Emp.DataBind();
        //}

        if (ddlCompanyList.SelectedIndex > 0)
        {
            query += " AND C.CompanyId=" + ddlCompanyList.SelectedValue;
        }
        if (ddlDepartment.SelectedIndex > 0)
        {
            query += " AND D.DeptId=" + ddlDepartment.SelectedValue;
        }
        if (ddlEmployeeList.SelectedIndex > 0)
        {
            query += " AND E.EmployeeId=" + ddlEmployeeList.SelectedValue;
        }

        if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
        {
            query += " AND E.EmployeeNo LIKE '%" + txtEmployeeCode.Text + "%'";
        }

        DataTable dataTable = g.ReturnData(query);

        grd_Emp.DataSource = dataTable;
            grd_Emp.DataBind();
    }

    public void BindPagerList()
    {
        string query = "select count(employeeid) from employeetb where isactive=1";
        DataTable data = g.ReturnData(query);
        int recordCount = data.Rows.Count > 0 ? Convert.ToInt32(data.Rows[0][0]) : 0;
        GetPageData(1);
        this.PopulatePager(recordCount, 1);
    }

    public void GetPageData(int pageNo)
    {
        int companyid = Convert.ToInt32(Session["CompanyID"]);
        string query = string.Format(@"SELECT    E.FName, E.Lname, E.EmployeeId, E.BirthDate, E.Email, E.DOJ AS DOJ1, E.PanNo, E.ContactNo, E.PassportNo, D.DeptName, C.CompanyName, E.FName + ' ' + E.Lname AS EmpName, E.RelivingDate, E.EmployeeNo, 
                         E.personalEmail, E.CompanyId, E.DeptId, E.DesgId, E.MachineID, E.DExpiryDate,E.RelivingStatus,E.Grade1
FROM            EmployeeTB AS E INNER JOIN
                         MasterDeptTB AS D ON E.DeptId = D.DeptID INNER JOIN
                         CompanyInfoTB AS C ON E.CompanyId = C.CompanyId WHERE E.IsActive=1 AND E.RelivingStatus = 0 AND C.TenantId='{0}'", Convert.ToString(Session["TenantId"]));

        if (ddlCompanyList.SelectedIndex > 0)
        {
            query += " AND C.CompanyId=" + ddlCompanyList.SelectedValue;
        }
        if (ddlDepartment.SelectedIndex > 0)
        {
            query += " AND D.DeptId=" + ddlDepartment.SelectedValue;
        }
        if (ddlEmployeeList.SelectedIndex > 0)
        {
            query += " AND E.EmployeeId=" + ddlEmployeeList.SelectedValue;
        }

        if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
        {
            query += " AND E.EmployeeNo LIKE '%" + txtEmployeeCode.Text + "%'";
        }
        if (Session["UserType"].ToString() == "LocationAdmin")
        {
            query = query + " AND E.CompanyId='" + companyid + "'";
        }

        query += string.Format(@" ORDER BY E.EmployeeId OFFSET        (({0} - 1) * 1000) ROWS FETCH NEXT 1000 ROWS ONLY", pageNo);
       
        DataTable dataTable = g.ReturnData(query);

        dataTable.DefaultView.Sort = "DeptName ASC, EmpName ASC, Grade1 ASC";

        grd_Emp.DataSource = dataTable;
        grd_Emp.DataBind();

        lblPageNo.Text = pageNo.ToString();
    }
    private void PopulatePager(Int32 TotalRecords, Int32 PageNumber)
    {
        Int32 intIndex;
        Int32 intPageNumber;
        
        intPageNumber = 1;

        double dblPageCount = (double)((decimal)TotalRecords / 1000);
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", PageNumber > 1));
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != PageNumber));
            }
            pages.Add(new ListItem("Last", pageCount.ToString(), PageNumber < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();

        
    }
    #endregion

    protected void btnImport_Click(object sender, EventArgs e)
    {
        string skey = Session["TenantId"].ToString().Replace("+", "key_plus");
        Response.Redirect("mst_employees_import.aspx?key=" + skey);
    }

    protected void lbtnFaceDownload_Click(object sender, EventArgs e)
    {
        LinkButton lnkReliveDate = (LinkButton)sender;
        int EmployeeId = Convert.ToInt32(lnkReliveDate.CommandArgument);

        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            EmployeeTB employee = db.EmployeeTBs.Where(d => d.EmployeeId == EmployeeId).FirstOrDefault();
            string name = employee.FName + " " + employee.Lname;
            Response.Redirect("device_download_persons_face.aspx?id=" + EmployeeId + "&name=" + name + "&cid=" + employee.CompanyId);
        }

    }

    protected void btnDisable_Click(object sender, EventArgs e)
    {
        try {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                LinkButton button = (LinkButton)sender;
                int eId = Convert.ToInt32(button.CommandArgument);
                EmployeeTB employee = db.EmployeeTBs.Where(d => d.EmployeeId == eId).FirstOrDefault();
                employee.IsActive = false;
                //db.SubmitChanges();
                SystemUsersTB usersTB = db.SystemUsersTBs.Where(d => d.EmployeeId == eId && d.Email == employee.Email && d.Active == true).FirstOrDefault();
                if (usersTB != null)
                {
                    usersTB.Disabled = true;
                    usersTB.Active = false;
                }
                db.SubmitChanges();

                g.ShowMessage(this.Page, "Employee deleted successfully");
            }
        }
        catch(Exception ex)
        {
            g.ShowMessage(this.Page, "Something wrong happen while delete record");
        }
        finally
        {
            //BindAllEmp();
            BindPagerList();
        }
    }

   
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetPageData(pageIndex);
        
    }

    protected void ddlfingerNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        var emp = HR.EmployeeTBs.Where(a => a.EmployeeId == int.Parse(lblempid.Text) && a.IsFingerPrintPresent == true).FirstOrDefault();
        if (emp != null)
        {
            var fingerdata = HR.EmployeeFingerprintTBs.Where(a => a.Employee_Id == emp.EmployeeId && a.Finger_Id == Convert.ToInt32(ddlfingerNo.SelectedValue)).FirstOrDefault();
            if (fingerdata != null)
            {
                divFingerprint.Visible = true;
                txtFingerprintqty.Text = fingerdata.Finger_Quality;
            }
            else
            {
                divFingerprint.Visible = false;
            }

        }

    }

    protected void btnfingerprint_Click(object sender, EventArgs e)
    {

        bool deviceStat = false;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            string folderPath = Server.MapPath("~/EmployeeImages/Fingerprint/" + ddlCompany.SelectedItem.Text + "/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            clienthttp clnt = new clienthttp();
            var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == ddlDevice.SelectedValue).FirstOrDefault();
            MatchlistDevice device = new MatchlistDevice();
            string userName = string.IsNullOrEmpty(deviceData.UserName) ? ConfigurationManager.AppSettings["DeviceUserName"] : deviceData.UserName;
            string password = string.IsNullOrEmpty(deviceData.Password) ? ConfigurationManager.AppSettings["DevicePassword"] : deviceData.Password;

            if (deviceData != null)
            {
                string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                string devReq = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":5000}}";

                string devResp = string.Empty;
                string strDevMatchNum = string.Empty;

                int devR = clnt.HttpRequest(userName, password, deviceStatUrl, "POST", devReq, ref devResp);
                if (devR == (int)HttpStatus.Http200)
                {
                    DeviceSearchRoot dr = JsonConvert.DeserializeObject<DeviceSearchRoot>(devResp);
                    strDevMatchNum = Convert.ToString(dr.SearchResult.numOfMatches);
                    if ("0" != strDevMatchNum)
                    {
                        //var dev = dr.SearchResult.MatchList.Select(d => d.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId).FirstOrDefault();
                        foreach (var item in dr.SearchResult.MatchList)
                        {
                            if (item.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId)
                            {
                                device = item.Device;
                                if (item.Device.devStatus == "online")
                                {
                                    deviceStat = true;
                                }
                            }
                        }
                    }
                }
            }



            if (deviceStat == false)
            {
                g.ShowMessage(this.Page, "Device is offline, cant download device log..");
            }
            else if (device == null)
            {
                g.ShowMessage(this.Page, "Device data not found..");
            }
            else if (deviceData != null)
            {
                int counter = 0;
                var devicedt = device;
                string index = devicedt.devIndex;

                string fingerprinturl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/CaptureFingerPrint?format=json&devIndex=" + index;

                clienthttp http = new clienthttp();
                int fingerno = Convert.ToInt32(ddlfingerNo.SelectedValue);

                string strFingerprintReq = "{\"CaptureFingerPrintCond\" : {\"fingerNo\":" + fingerno +"}}";
                string strFingerprintResp = string.Empty;
                string strfingerdata = string.Empty;
                int ietfingerprint = http.HttpRequest(userName, password, fingerprinturl, "POST", strFingerprintReq, ref strFingerprintResp);

                string apiStatus = "failed";

                if (ietfingerprint == (int)HttpStatus.Http200)
                {

                    EmployeeTB employee = db.EmployeeTBs.Where(d => d.EmployeeId == int.Parse(lblempid.Text)).FirstOrDefault();

                    DeviceFingerprintRoot dr = JsonConvert.DeserializeObject<DeviceFingerprintRoot>(strFingerprintResp);
                    strfingerdata = Convert.ToString(dr.CaptureFingerPrint.fingerData);

                    EmployeeFingerprintTB empfinger = new EmployeeFingerprintTB();
                    empfinger.Employee_Id = employee.EmployeeId;
                    empfinger.Company_Id = employee.CompanyId;
                    empfinger.Finger_Id = dr.CaptureFingerPrint.fingerNo;
                    empfinger.Finger_Data = dr.CaptureFingerPrint.fingerData;
                    empfinger.Finger_Quality = dr.CaptureFingerPrint.fingerPrintQuality.ToString();

                    db.EmployeeFingerprintTBs.InsertOnSubmit(empfinger);
                    db.SubmitChanges();

                    employee.IsFingerPrintPresent = true;
                    db.SubmitChanges();

                    counter++;
                    apiStatus = "success";

                    divFingerprint.Visible = true;
                    image2.ImageUrl = "~/Images/Fingerprint.jfif";
                }
            }
        }
    }


   


    protected void btnCatureDevicePicture_Click(object sender, EventArgs e)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            try
            {
                bool deviceStat = false;
                var empdata = HR.EmployeeTBs.Where(a => a.EmployeeNo == txtBioId.Text && a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).FirstOrDefault();
                #region Device Status
                var deviceData = db.DevicesTBs.Where(d => d.DeviceAccountId == ddlDevice.SelectedValue).FirstOrDefault();
                MatchlistDevice device = new MatchlistDevice();
                if (deviceData != null)
                {
                    string deviceStatUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/ContentMgmt/DeviceMgmt/deviceList?format=json";

                    string req = "{\"SearchDescription\" : {\"position\":0,\"maxResult\":500}}";

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
                            foreach (var item in dr.SearchResult.MatchList)
                            {
                                if (item.Device.EhomeParams.EhomeID == deviceData.DeviceAccountId)
                                {
                                    device = item.Device;
                                    if (item.Device.devStatus == "online")
                                    {
                                        deviceStat = true;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                if (deviceStat == false)
                {
                    g.ShowMessage(this.Page, "Device is offline, cant download device log..");
                }
                else if (device == null)
                {
                    g.ShowMessage(this.Page, "Device data not found..");
                }
                else if (deviceData != null)
                {
                    int counter = 0;
                    string strMatchNum = string.Empty;
                    var devicedt = device;

                    string index = devicedt.devIndex;
                    
                    string strUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/CaptureFaceData?devIndex=" + index;
                    string strReq = "<CaptureFaceDataCond version=\"2.0\" xmlns=\"http://www.isapi.org/ver20/XMLSchema\"><dataType>url</dataType></CaptureFaceDataCond>";

                    string strRsp = string.Empty;
                    clienthttp http = new clienthttp();
                    int iRet = http.HttpRequest(deviceData.UserName, deviceData.Password, strUrl, "POST", strReq, ref strRsp);

                    string apiStatus = "failed";
                    if (iRet == (int)HttpStatus.Http200)
                    {
                        Thread.Sleep(15000);
                        string strcaptureUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/ISAPI/AccessControl/CaptureFaceData/Progress?devIndex=" + index;
                        string strcaptureresp = string.Empty;

                        int captureface = http.HttpRequest(deviceData.UserName, deviceData.Password, strcaptureUrl, "GET","", ref strcaptureresp);
                        if (captureface == (int)HttpStatus.Http200)
                        {
                            //Thread.Sleep(5000);
                            var parts = strcaptureresp?.Split(new string[] { "<faceDataUrl>", "</faceDataUrl>" }, StringSplitOptions.None);
                                                        
                                string strpicUrl = "http://" + deviceData.IPAddress + ":" + deviceData.PortNo + "/HikGatewayStorage" + parts[1];
                                string strpicresp = string.Empty;

                                int picdata = http.HttpRequest(deviceData.UserName, deviceData.Password, strpicUrl, "GET", "", ref strpicresp);
                                if (picdata == (int)HttpStatus.Http200)
                                {
                                    apiStatus = "success";
                                var file_full_path = parts[1].Split('?');

                                string sourcePath = "C:\\DeviceGatewayStorage\\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + DateTime.Now.ToString("HH") + @"\" + file_full_path[1];

                                string filename = empdata.EmployeeId + "_" + empdata.FName + "_" + empdata.Lname;
                                string copyToPath = Server.MapPath("~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/") + filename + ".jpg";

                               
                                //string copyToPath = Server.MapPath("~/EmployeeImages/Faces/" + ddlCompany.SelectedItem.Text + "/") + file_full_path[1];

                                File.Copy(sourcePath, copyToPath);

                                empdata.Photo = filename + ".jpg";
                                empdata.IsFacePresent = true;
                                HR.SubmitChanges();

                                g.ShowMessage(this.Page, "Face Add successfully");
                                Response.Redirect("mst_employee_list_parabhani.aspx");
                            }                           
                        }

                    }
                }
            }
            catch (Exception ex) { Console.Write(ex.Message); }
        }
    }


















}
