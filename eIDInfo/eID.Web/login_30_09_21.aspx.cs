using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login_30_09_21 : System.Web.UI.Page
{
    Genreal gen = new Genreal();
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        CheckforFirstUser();

    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var pass = SPPasswordHasher.Encrypt(txtpassword.Text);
            var userData = (from d in db.SystemUsersTBs
                            where d.Email==txtusername.Text&& d.PasswordHash == SPPasswordHasher.Encrypt(txtpassword.Text)
                            select new { d.DisplayName, d.Email, d.UserRole, d.UID, d.TenantId,d.Active }).FirstOrDefault();
            if (userData != null)
            {
                if (userData.Active == true)
                {
                    var getTenantData = db.CompanyRegistrationTBs.Where(d => d.SecurityKey == userData.TenantId).Select(s => s.CompanyName).FirstOrDefault() ?? string.Empty;

                    var empData = db.EmployeeTBs.Where(d => d.Email == txtusername.Text).DefaultIfEmpty().FirstOrDefault();
                    if (empData != null)
                    {
                        Session["EmpId"] = empData.EmployeeId;
                    }
                    Session["UserType"] = userData.UserRole;
                    Session["UserId"] = userData.UID;
                    Session["DisplayName"] = userData.DisplayName;
                    Session["TenantId"] = userData.TenantId;
                    Session["Email"] = userData.Email;
                    Session["TenantName"] = getTenantData;

                    Response.Redirect("admin_dashboard.aspx");
                }
                else
                {
                    gen.ShowMessage(this.Page, "Account not activated yet.. Please activate your account or contact administrator");
                }
            }
            else
            {
                gen.ShowMessage(this.Page, "Invalid username or password..");
            }
        }
        
    }
}