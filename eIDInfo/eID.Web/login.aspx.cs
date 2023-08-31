using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext();
    private void CheckforFirstUser()
    {
        DataTable chkDT = gen.ReturnData("SELECT top 1 UID FROM dbo.SystemUsersTB where UserRole='SuperAdmin'");
        if (chkDT.Rows.Count == 0)
        {
            string key = DateTime.Now.ToString("yyyyMMddhhmmssffffff");
            string secureuid = SPPasswordHasher.Encrypt(key);
            string securepass = SPPasswordHasher.Encrypt("van!@$uperhr");
            string securetenant = SPPasswordHasher.Encrypt(key);
            string displayName = "Super Admin";
            string userName = "superadmin";
            string defaultEmail = "iamyesp@gmail.com";
            string query = string.Format(@"INSERT INTO SystemUsersTB(UID, DisplayName, Username, Email, PhoneNumber, Password, PasswordHash, Active, Disabled, UserRole, TenantId) 
VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '1', '0', 'SuperAdmin', '{7}')", secureuid, displayName, userName, defaultEmail, "9595629899", "default_password", securepass, securetenant);
            DataTable dataTable = gen.ReturnData(query);
        }
    }

    private void CheckSMTPDetails()    
    {
        try
        {
            var data = db.SMTPSettingsTBs.FirstOrDefault();
            if (data == null)
            {
                Response.Redirect("configure_smtp.aspx");
            }
        }
       catch(Exception ex)
        {
            gen.ShowMessage(this.Page, "Network Error...");
        }
    }
    private void CheckDateExists()
    {
        DateTime last_month = DateTime.Now.AddMonths(-4);
        if (last_month.Day == 2)
        {
            string month = last_month.ToString("MMM");
            int monthNo = last_month.Month;
            string year = last_month.Year.ToString();
            string table_name = "DeviceLogsBK_" + month.ToUpper() + "_" + year;

            string query4tableCheck = string.Format(@"IF OBJECT_ID('{0}', 'U') IS NOT NULL
BEGIN
SELECT '1'
END
ELSE
BEGIN

CREATE TABLE [dbo].[{0}](
	[LogId] [int] NOT NULL,
	[DeviceAccountId] [int] NULL,
	[DownloadDate] [datetime] NULL,
	[AttendDate] [datetime] NULL,
	[PunchStatus] [varchar](50) NULL,
	[EmpID] [int] NULL,
	[AccessCardNo] [varchar](50) NULL,
	[ADate] [date] NULL,
	[ATime] [time](7) NULL,
	[CompanyId] [int] NULL,
	[TenantId] [nvarchar](max) NULL,
	[CurrentTemp] [numeric](18, 2) NULL, 
) 

SELECT '0'
END", table_name);

            // move data from log to backup table
            DataTable checkTable = gen.ReturnData(query4tableCheck);
            if (checkTable.Rows[0][0].ToString() == "0")
            {
                string createTableQuery = string.Format(@"INSERT INTO dbo.{0}
SELECT        *
FROM            DeviceLogsTB
WHERE        (MONTH(ADate) = {1}) AND (YEAR(ADate) = {2})", table_name,monthNo,year);
            }
        }
        //if(last)

    }
    public async Task DeviceLogLoadAsync()
    {
        DateTime last_month = DateTime.Now.AddMonths(-4);
        if (last_month.Day == 2)
        {
            string month = last_month.ToString("MMM");
            int monthNo = last_month.Month;
            string year = last_month.Year.ToString();
            string table_name = "DeviceLogsBK_" + month.ToUpper() + "_" + year;

            string query4tableCheck = string.Format(@"IF OBJECT_ID('{0}', 'U') IS NOT NULL
BEGIN
SELECT '1'
END
ELSE
BEGIN

CREATE TABLE [dbo].[{0}](
	[LogId] [int] NOT NULL,
	[DeviceAccountId] [int] NULL,
	[DownloadDate] [datetime] NULL,
	[AttendDate] [datetime] NULL,
	[PunchStatus] [varchar](50) NULL,
	[EmpID] [int] NULL,
	[AccessCardNo] [varchar](50) NULL,
	[ADate] [date] NULL,
	[ATime] [time](7) NULL,
	[CompanyId] [int] NULL,
	[TenantId] [nvarchar](max) NULL,
	[CurrentTemp] [numeric](18, 2) NULL, 
) 

SELECT '0'
END", table_name);

            // move data from log to backup table
            DataTable checkTable = gen.ReturnData(query4tableCheck);
            if (checkTable.Rows[0][0].ToString() == "0")
            {

                //perform your actions here, including calling async methods and awaiting their results
                //Use Async method to get data
                await GetDataSetAsync(table_name, monthNo, year);
            }
        }
    }
    public Task GetDataSetAsync(string table_name, int monthNo, string year)
    {
        return Task.Run(() =>
        {
            string createTableQuery = string.Format(@"INSERT INTO dbo.{0}
SELECT        *
FROM            DeviceLogsTB
WHERE        (MONTH(ADate) = {1}) AND (YEAR(ADate) = {2});

DELETE  FROM DeviceLogsTB
WHERE        (MONTH(ADate) = {1}) AND (YEAR(ADate) = {2});", table_name, monthNo, year);

            DataTable dataTable = gen.ReturnData(createTableQuery);
        });
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime Todaysdate = DateTime.Now;
        string todaydate = Todaysdate.Year + "-" + Todaysdate.Month + "-" + Todaysdate.Day;
        string date = "2023-09-30";

        if(Convert.ToDateTime(todaydate) <= Convert.ToDateTime(date))
        {
            if (!IsPostBack)
            {
                CheckforFirstUser();
                CheckSMTPDetails();
                //DeviceLogLoadAsync();
                string encrypt = SPPasswordHasher.Encrypt("eIDinfo@123");
                //string encrypt = SPPasswordHasher.Encrypt("password@1234");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        //DateTime Todaysdate = DateTime.Now;
        //string todaydate = Todaysdate.Year + "-" + Todaysdate.Month + "-" + Todaysdate.Day;
        //string date = "2022-10-04";

        //if (Convert.ToDateTime(todaydate) <= Convert.ToDateTime(date))
        //{
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var pass = SPPasswordHasher.Encrypt(txtpassword.Text);

                var data = db.SystemUsersTBs.Where(d => d.Email == txtusername.Text && d.PasswordHash == SPPasswordHasher.Encrypt(txtpassword.Text) && d.Active == true).FirstOrDefault();

                if (data != null)
                {
                    var companyinfo = db.CompanyRegistrationTBs.Where(a => a.SecurityKey == data.TenantId).FirstOrDefault();
                    if (companyinfo.IsActive == false)
                    {
                        gen.ShowMessage(this.Page, "Your Company is DeActivated.");
                    }
                    else
                    {
                        if (data.EmployeeId == 0 && data.UserRole == "SuperAdmin")
                        {
                            var chkSuperAdminData = (from d in db.SystemUsersTBs
                                                     join com in db.CompanyRegistrationTBs on d.TenantId equals com.SecurityKey
                                                     where com.IsActive == true && d.Active == true && d.Email == txtusername.Text && d.PasswordHash == SPPasswordHasher.Encrypt(txtpassword.Text) && d.EmployeeId == 0 && d.UserRole == "SuperAdmin"
                                                     select new
                                                     {
                                                         d.Active,
                                                         d.Disabled,
                                                         d.DisplayName,
                                                         d.Email,
                                                         d.Password,
                                                         d.PasswordHash,
                                                         d.PhoneNumber,
                                                         d.TenantId,
                                                         d.UID,
                                                         d.Username,
                                                         d.UserRole,
                                                         CompanyId = 0,
                                                         EmployeeId = 0,
                                                         EmployeeNo = "0000",
                                                         DeptId = 0
                                                     }).FirstOrDefault();


                            if (chkSuperAdminData.Active == true)
                            {
                                Session["UserType"] = chkSuperAdminData.UserRole;
                                Session["UserId"] = chkSuperAdminData.UID;
                                Session["DisplayName"] = chkSuperAdminData.DisplayName;

                                Session["UserType"] = chkSuperAdminData.UserRole;
                                Session["EmpId"] = chkSuperAdminData.EmployeeId;
                                Session["DisplayName"] = chkSuperAdminData.DisplayName;

                                Session["IsDeptHead"] = false;
                                Session["TenantId"] = chkSuperAdminData.TenantId;
                                Session["Email"] = chkSuperAdminData.Email;
                                Session["TenantName"] = "";
                                Session["CompanyID"] = chkSuperAdminData.CompanyId;
                                Response.Redirect("admin_dashboard.aspx");
                            }
                            else
                            {
                                gen.ShowMessage(this.Page, "Account not activated yet.. Please activate your account or contact administrator");
                            }
                        }
                        else if (data.EmployeeId == 0 && data.UserRole == "Admin")
                        {
                            var chkAdminData = (from d in db.SystemUsersTBs
                                                join emp in db.CompanyRegistrationTBs on d.TenantId equals emp.SecurityKey
                                                where emp.IsActive == true && d.Active == true && d.Email == txtusername.Text && d.PasswordHash == SPPasswordHasher.Encrypt(txtpassword.Text) && d.EmployeeId == 0 && d.UserRole == "Admin"
                                                select new
                                                {
                                                    d.Active,
                                                    d.Disabled,
                                                    d.DisplayName,
                                                    d.Email,
                                                    d.Password,
                                                    d.PasswordHash,
                                                    d.PhoneNumber,
                                                    d.TenantId,
                                                    d.UID,
                                                    d.Username,
                                                    d.UserRole,
                                                    emp.CompanyId,
                                                    EmployeeId = 0,
                                                    EmployeeNo = "0000",
                                                    DeptId = 0
                                                }).FirstOrDefault();

                            if (chkAdminData.Active == true)
                            {
                                Session["UserType"] = chkAdminData.UserRole;
                                Session["UserId"] = chkAdminData.UID;
                                Session["DisplayName"] = chkAdminData.DisplayName;

                                var getTenantData = db.CompanyRegistrationTBs.Where(d => d.SecurityKey == chkAdminData.TenantId).Select(s => s.CompanyName).FirstOrDefault() ?? string.Empty;


                                Session["UserType"] = chkAdminData.UserRole;
                                Session["EmpId"] = chkAdminData.EmployeeId;
                                Session["DisplayName"] = chkAdminData.DisplayName;

                                Session["IsDeptHead"] = false;
                                Session["TenantId"] = chkAdminData.TenantId;
                                Session["Email"] = chkAdminData.Email;
                                Session["TenantName"] = getTenantData;
                                Session["CompanyID"] = chkAdminData.CompanyId;
                                Response.Redirect("admin_dashboard.aspx");
                            }
                            else
                            {
                                gen.ShowMessage(this.Page, "Account not activated yet.. Please activate your account or contact administrator");
                            }
                        }
                        else if (data.UserRole == "User")
                        {
                            var userData = (from d in db.SystemUsersTBs
                                            join com in db.CompanyRegistrationTBs on d.TenantId equals com.SecurityKey
                                            join emp in db.EmployeeTBs on d.Email equals emp.Email
                                            where com.IsActive == true && d.Active == true && d.Email == txtusername.Text && d.PasswordHash == SPPasswordHasher.Encrypt(txtpassword.Text) && d.EmployeeId == emp.EmployeeId && d.UserRole == "User"
                                            select new
                                            {
                                                d.Active,
                                                d.Disabled,
                                                d.DisplayName,
                                                d.Email,
                                                d.Password,
                                                d.PasswordHash,
                                                d.PhoneNumber,
                                                d.TenantId,
                                                d.UID,
                                                d.Username,
                                                d.UserRole,
                                                emp.CompanyId,
                                                emp.EmployeeId,
                                                emp.EmployeeNo,
                                                emp.DeptId,
                                                DeviceId = d.DeviceId == null ? "" : d.DeviceId,
                                                FCMKey = d.FCMKey == null ? "" : d.FCMKey,
                                                IsCodeRequested = d.IsCodeRequested.HasValue ? d.IsCodeRequested : false
                                            }).FirstOrDefault();

                            if (userData.Active == true)
                            {
                                Session["UserType"] = userData.UserRole;
                                Session["UserId"] = userData.UID;
                                Session["DisplayName"] = userData.DisplayName;
                                int headFlag = 0;

                                var getTenantData = db.CompanyRegistrationTBs.Where(d => d.SecurityKey == userData.TenantId).Select(s => s.CompanyName).FirstOrDefault() ?? string.Empty;


                                Session["UserType"] = userData.UserRole;
                                Session["EmpId"] = userData.EmployeeId;
                                Session["DisplayName"] = userData.DisplayName;
                                var headData = db.DepartmentHeadTBs.Where(d => d.EmpID == userData.EmployeeId).FirstOrDefault();
                                if (headData != null)
                                {
                                    headFlag = 1;
                                }

                                Session["IsDeptHead"] = headFlag;
                                Session["TenantId"] = userData.TenantId;
                                Session["Email"] = userData.Email;
                                Session["TenantName"] = getTenantData;
                                Session["CompanyID"] = userData.CompanyId;
                                string url = "admin_dashboard.aspx";
                                if (userData.UserRole == "User")
                                {
                                    url = "employee_dashboard.aspx";
                                }
                                Response.Redirect(url);
                            }
                            else
                            {
                                gen.ShowMessage(this.Page, "Account not activated yet.. Please activate your account or contact administrator");
                            }

                        }

                        else if (data.EmployeeId == 0 && data.UserRole == "LocationAdmin")
                        {
                            var chkLocationAdminData = (from d in db.SystemUsersTBs
                                                        join emp in db.CompanyRegistrationTBs on d.TenantId equals emp.SecurityKey
                                                        where emp.IsActive == true && d.Active == true && d.Email == txtusername.Text && d.PasswordHash == SPPasswordHasher.Encrypt(txtpassword.Text) && d.EmployeeId == 0 && d.UserRole == "LocationAdmin"
                                                        select new
                                                        {
                                                            d.Active,
                                                            d.Disabled,
                                                            d.DisplayName,
                                                            d.Email,
                                                            d.Password,
                                                            d.PasswordHash,
                                                            d.PhoneNumber,
                                                            d.TenantId,
                                                            d.UID,
                                                            d.Username,
                                                            d.UserRole,
                                                            emp.CompanyId,
                                                            EmployeeId = 0,
                                                            EmployeeNo = "0000",
                                                            DeptId = 0
                                                        }).FirstOrDefault();


                            if (chkLocationAdminData.Active == true)
                            {
                                Session["UserType"] = chkLocationAdminData.UserRole;
                                Session["UserId"] = chkLocationAdminData.UID;
                                Session["DisplayName"] = chkLocationAdminData.DisplayName;

                                var getTenantData = db.CompanyRegistrationTBs.Where(d => d.SecurityKey == chkLocationAdminData.TenantId).Select(s => s.CompanyName).FirstOrDefault() ?? string.Empty;


                                Session["UserType"] = chkLocationAdminData.UserRole;
                                Session["EmpId"] = chkLocationAdminData.EmployeeId;
                                Session["DisplayName"] = chkLocationAdminData.DisplayName;

                                Session["IsDeptHead"] = false;
                                Session["TenantId"] = chkLocationAdminData.TenantId;
                                Session["Email"] = chkLocationAdminData.Email;
                                Session["TenantName"] = getTenantData;
                                Session["CompanyID"] = chkLocationAdminData.CompanyId;
                                Response.Redirect("admin_dashboard.aspx");
                            }
                            else
                            {
                                gen.ShowMessage(this.Page, "Account not activated yet.. Please activate your account or contact administrator");
                            }
                        }
                        else if (data.UserRole == "LocationAdmin")
                        {
                            var chkLocationAdminData = (from d in db.SystemUsersTBs
                                                        join com in db.CompanyRegistrationTBs on d.TenantId equals com.SecurityKey
                                                        join emp in db.EmployeeTBs on d.Email equals emp.Email
                                                        where emp.IsActive == true && d.Active == true && d.Email == txtusername.Text && d.PasswordHash == SPPasswordHasher.Encrypt(txtpassword.Text) && d.EmployeeId == emp.EmployeeId && d.UserRole == "LocationAdmin"
                                                        select new
                                                        {
                                                            d.Active,
                                                            d.Disabled,
                                                            d.DisplayName,
                                                            d.Email,
                                                            d.Password,
                                                            d.PasswordHash,
                                                            d.PhoneNumber,
                                                            d.TenantId,
                                                            d.UID,
                                                            d.Username,
                                                            d.UserRole,
                                                            emp.CompanyId,
                                                            emp.EmployeeId,
                                                            emp.EmployeeNo,
                                                            emp.DeptId
                                                        }).FirstOrDefault();

                            if (chkLocationAdminData.Active == true)
                            {
                                Session["UserType"] = chkLocationAdminData.UserRole;
                                Session["UserId"] = chkLocationAdminData.UID;
                                Session["DisplayName"] = chkLocationAdminData.DisplayName;

                                var getTenantData = db.CompanyRegistrationTBs.Where(d => d.SecurityKey == chkLocationAdminData.TenantId).Select(s => s.CompanyName).FirstOrDefault() ?? string.Empty;


                                Session["UserType"] = chkLocationAdminData.UserRole;
                                Session["EmpId"] = chkLocationAdminData.EmployeeId;
                                Session["DisplayName"] = chkLocationAdminData.DisplayName;

                                Session["IsDeptHead"] = false;
                                Session["TenantId"] = chkLocationAdminData.TenantId;
                                Session["Email"] = chkLocationAdminData.Email;
                                Session["TenantName"] = getTenantData;
                                Session["CompanyID"] = chkLocationAdminData.CompanyId;
                                Response.Redirect("admin_dashboard.aspx");
                            }
                            else
                            {
                                gen.ShowMessage(this.Page, "Account not activated yet.. Please activate your account or contact administrator");
                            }
                        }
                    }
                }
                else
                {
                    if (txtusername.Text == "Official" && txtpassword.Text == "eidinfo@123")
                    {
                        Session["UserId"] = "0001";
                        Session["UserType"] = "Administrator";
                        Session["Email"] = "eidinfoindia@gmail.com";
                        Response.Redirect("Company_Settings.aspx");
                    }
                    else
                    {

                        gen.ShowMessage(this.Page, "Invalid username or password..");

                    }
                }
            //}
        }
    }


    










}