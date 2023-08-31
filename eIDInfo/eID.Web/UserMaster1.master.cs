using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserMaster1 : System.Web.UI.MasterPage
{
    HrPortalDtaClassDataContext HDC = new HrPortalDtaClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                EmployeeTB CTB = HDC.EmployeeTBs.Where(d =>d.Email==Session["Email"].ToString()).FirstOrDefault();
                if (CTB != null)
                {
                    //lblloginname.Text = Convert.ToString(CTB.FName + " " + CTB.Lname);
                    //lblloginname1.Text = "Welcome   " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    lblloginname.Text = Session["DisplayName"].ToString();
                    lblloginname1.Text = "Welcome   " + Session["DisplayName"].ToString();
                    // lblnote.Text = "Welcome " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    // BindMenu();
               
                    string title=lblTitle.Text=lblActivePage.Text = Page.Page.Title;
                }
                else if (Session["UserType"].ToString().Equals("Admin"))
                {
                    lblloginname.Text = "Administrator";
                    lblloginname1.Text = "Welcome " + Session["TenantName"] + " Admin";
                    // lblnote.Text = "Welcome " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    // BindMenu();
                  

                    string title = lblTitle.Text = lblActivePage.Text = Page.Page.Title;
                }
                else if (Session["UserType"].ToString().Equals("SuperAdmin"))
                {
                    lblloginname.Text = "Super Admin";
                    lblloginname1.Text = "Welcome   Super Admin" ;
                    // lblnote.Text = "Welcome " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    // BindMenu();
                 

                    string title = lblTitle.Text = lblActivePage.Text = Page.Page.Title;
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        
    }
    
    
    Genreal gen = new Genreal();
   
    protected void btnlogout_Click(object sender, EventArgs e)
    {
        Session["UserId"] = null;

        Response.Redirect("Login.aspx");
    }
    protected void btnProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeDetails.aspx");
    }
}
