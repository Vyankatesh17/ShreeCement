using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class activate_account : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["key"])))
            {
                txtEmail.Text = Convert.ToString(Request.QueryString["email"]);
                GetRegistrationDetails();

                txtCompanyName.Attributes.Add("readonly", "readonly");
                txtEmail.Attributes.Add("readonly", "readonly");
            }
            else
            {
               // Response.Redirect("login.aspx");

            }
        }
    }

    protected void btnActivate_Click(object sender, EventArgs e)
    {
        try {
            using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
            {
                string rkey = Request.QueryString["key"].ToString().Trim();
                rkey = rkey.Replace("key_plus", "+");
                CompanyRegistrationTB company = db.CompanyRegistrationTBs.Where(d => d.Email == txtEmail.Text &&
                  d.SecurityKey == rkey && d.PasswordHash == SPPasswordHasher.Encrypt(txtOldPassword.Text)).FirstOrDefault();

                if (company != null)
                {
                    string key = DateTime.Now.ToString("yyyyMMddhhmmssffffff");
                    string secureuid = SPPasswordHasher.Encrypt(key);
                    string hashPass = SPPasswordHasher.Encrypt(txtPassword.Text);
                    company.IsActive = true;
                    company.IsDisabled = false;
                    company.PasswordPlain = txtPassword.Text;
                    company.PasswordHash = hashPass;

                    var systemData = db.SystemUsersTBs.Where(d => d.TenantId == company.SecurityKey).Distinct();
                    SystemUsersTB usersTB = new SystemUsersTB();
                    if (systemData.Count() > 0)
                    {
                        usersTB = db.SystemUsersTBs.Where(d => d.TenantId == company.SecurityKey && d.Email == company.Email && d.Active == true).FirstOrDefault();
                    }
                    usersTB.Active = true;
                    usersTB.Disabled = false;
                    usersTB.DisplayName = company.ContactPerson;
                    usersTB.Email = company.Email;
                    usersTB.Password = txtPassword.Text;
                    usersTB.PasswordHash = hashPass;
                    usersTB.PhoneNumber = company.ContactNo;
                    usersTB.TenantId = company.SecurityKey;
                    usersTB.UID = secureuid;
                    usersTB.Username = company.Email;
                    usersTB.UserRole = "Admin";
                    usersTB.EmployeeId = 0;
                    if (systemData.Count() == 0)
                    {
                        db.SystemUsersTBs.InsertOnSubmit(usersTB);
                    }
                    db.SubmitChanges();

                    gen.ShowMessage(this.Page, "Account activated successfully..");
                    txtCompanyName.Text = txtConfirmPass.Text = txtEmail.Text = txtOldPassword.Text = txtPassword.Text = "";
                }
                else
                {
                    gen.ShowMessage(this.Page, "Something went arong..! \n Data not found for activation..");
                }
            }
        }
        catch(Exception ex) { }
    }

    private void GetRegistrationDetails()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            string key = Request.QueryString["key"].ToString().Trim() ;
            key = key.Replace("key_plus", "+");
            CompanyRegistrationTB company = db.CompanyRegistrationTBs.Where(d => d.Email == txtEmail.Text.Trim() && d.SecurityKey == key).FirstOrDefault();
            if (company != null)
            {
                txtCompanyName.Text = company.CompanyName;
                btnActivate.Visible = true;
            }
            else
            {
                gen.ShowMessage(this.Page, "Activation link not valid..");
                btnActivate.Visible = false;
            }
        }
    }
}