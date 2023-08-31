using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_newuser : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    int logincnt;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

        }
    }
    public void Clear()
    {
        txtPassword1.Text = null;
        txtusername1.Text = null;
        ddlsecurity.SelectedIndex = 0;
        txtempcode.Text = null;
        rd_status.SelectedIndex = 0;
        btnsubmit.Text = "Submit";

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            String EmpCode = txtempcode.Text.Replace("100", "");
            int Empid = Convert.ToInt32(EmpCode);

            var CompanyData = from d in HR.RegistrationTBs
                              where d.UserName == txtusername1.Text && d.Password == txtPassword1.Text
                              select new { d.UserName, d.Password, d.EmployeeId };
            if (CompanyData.Count() == 0)
            {
                if (btnsubmit.Text == "Save" || btnsubmit.Text == "Submit")
                {
                    var CompanyData1 = from d in HR.RegistrationTBs
                                       where d.EmployeeId == Empid //&& d.UserName == null && d.Password == null 
                                       select new { d.UserName, d.Password, d.EmployeeId };
                    if (CompanyData1.Count() > 0)
                    {

                        RegistrationTB rg = HR.RegistrationTBs.Where(d => d.EmployeeId == Convert.ToInt32(Empid)).First();
                        rg.UserName = txtusername1.Text;
                        rg.Password = txtPassword1.Text;
                        rg.ReenterPassword = txtPassword1.Text;
                        rg.SecurityQuestion = ddlsecurity.SelectedValue;
                        rg.SecurityAnswer = txtSecurity.Text;
                        rg.EmployeeId = Convert.ToInt32(Empid);
                        rg.Flag = 1;
                        Session["flag"] = 0;
                        rg.Status = rd_status.SelectedIndex;
                        g.ShowMessage(this.Page, "Your Registration Done Successfully, Please Login with your UserName and Password ...");
                        HR.SubmitChanges();
                        EmployeeTB emp = new EmployeeTB();
                        emp.EmployeeFlag = 0;
                        HR.SubmitChanges();
                        Clear();
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        g.ShowMessage(this.Page, "Invalid UserName or Password.");
                        txtPassword1.Focus();
                    }
                }
            }
            else
            {
                g.ShowMessage(this.Page, "UserName or Password Details Already Exists.");
            }
        }
        catch (Exception e1)
        {
            g.ShowMessage(this.Page, e1.Message);
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
}
