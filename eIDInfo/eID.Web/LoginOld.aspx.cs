using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;

public partial class LoginOld : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    int logincnt;

    private void CheckforFirstUser()
    {
        DataTable chkDT = g.ReturnData("SELECT COUNT(*) CNT FROM SystemUsersTB");
        if(Convert.ToInt32(chkDT.Rows[0]["CNT"].ToString()) == 0)
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
          
            DataTable dataTable = g.ReturnData(query);

        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //CheckforFirstUser();

        txtusername.Focus();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

      

    }

    protected void btnlogin_Click(object sender, EventArgs e)
    {
        
       



        var LoginData = from d in HR.RegistrationTBs
                        join emp in HR.EmployeeTBs on d.EmployeeId equals emp.EmployeeId
                        where d.UserName == txtusername.Text &&
                            d.Password == txtpassword.Text && d.Status == 0
                        select new
                        {
                            d.EmployeeId,
                            d.UserType,
                            d.Flag,

                        };
        if (LoginData.Count() > 0)
        {

            foreach (var item in LoginData)
            {
                Session["UserId"] = item.EmployeeId;
                if (item.EmployeeId == 1)
                {
                    Session["IsSuperAdmin"] = "Yes";
                }
                else
                {
                    Session["IsSuperAdmin"] = "No";
                }
                Session["UserType"] = item.UserType;

                /******  Flag For Login Count *****/
                Session["LoginCnt"] = item.Flag;
                logincnt = (int)(Session["LoginCnt"]);
                logincnt++;

                var empdata = from dd in HR.EmployeeTBs
                              where dd.EmployeeId == item.EmployeeId
                              select new
                              {
                                  dd.EmployeeFlag,
                              };
                foreach (var empdata1 in empdata)
                {
                    Session["EmployeeFlag"] = empdata1.EmployeeFlag;
                    break;
                }

                if (item.UserType == "Admin")
                {
                   
                    Response.Redirect("admin_dashboard.aspx");
                }
                else
                {
                    var RegisterEmpData = from dd in HR.EmployeeTBs
                                          join dt in HR.RegistrationTBs on dd.EmployeeId equals dt.EmployeeId
                                          where dd.EmployeeId == item.EmployeeId && dd.CompanyId == null
                                          select new
                                          {
                                              dd.EmployeeId,
                                          };
                    if (RegisterEmpData.Count() > 0)
                    {
                        foreach (var item1 in RegisterEmpData)
                        {
                            Response.Redirect("employee_dashboard.aspx?Empid=" + item1.EmployeeId);
                        }

                    }
                    else
                    {
                        Response.Redirect("EmployeeDetails.aspx");
                    }
                }

                // Response.Redirect("admin_dashboard.aspx");


            }

        }
         else 
         {
             // Check Agency Login

             var LoginDataAgency = from d in HR.AgencyMasterTBs
                                   where d.EmailId == txtusername.Text &&
                                       d.Password == txtpassword.Text && d.Status == 0
                                   select new
                                   {
                                       d.AgencyId,

                                   };
             if (LoginDataAgency.Count() > 0)
             {

                 foreach (var item in LoginDataAgency)
                 {
                     Session["UserId"] = item.AgencyId;
                     Session["UserType"] = "Agency";

                 }
                 Response.Redirect("DefaultAgency.aspx");
             }
             else             
        {

            g.ShowMessage(this.Page, "Enter Correct Credentials");
            txtusername.Text = txtpassword.Text = string.Empty;
            txtusername.Focus();

        }


         }
    }
    protected void btnForgotpass_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgotPass.aspx");


    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("Add_newuser.aspx");
    }
}