using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePassword : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

    Genreal g = new Genreal();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] != null)
        {
            MultiView1.ActiveViewIndex = 0;
            fillQuestion();
        }
        else
        {
            Response.Redirect("login.aspx");
        }

    }

    private void fillQuestion()
    {
         string s = Session["UserId"].ToString();
         RegistrationTB rg = HR.RegistrationTBs.Where(d => d.EmployeeId == int.Parse(s)).First();
         txtQuestion.Text = rg.SecurityQuestion;
     }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
       
       
        string s = Session["UserId"].ToString();
        bool AdminStatus = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));
        var data = from dt in HR.RegistrationTBs
                   where dt.EmployeeId == Convert.ToInt32(s)
                   select dt;

        if (AdminStatus == false)
        {
            data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["UserId"].ToString()));
        }
        if (data.Count() > 0)
        {
            RegistrationTB mtb = HR.RegistrationTBs.Where(d => d.EmployeeId == Convert.ToInt32(Session["UserId"])).First();
            mtb.Password = TextBox6.Text;
            HR.SubmitChanges();
            Clear();
            g.ShowMessage(this.Page, "Updated Successfully");
            Response.Redirect("HomePage.aspx");
           
        }
        else {
            g.ShowMessage(this.Page, "Please Enter Correct Password");
            //modpop.Message = "Please Enter Correct Password";
            //modpop.ShowPopUp();
        }

      
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
       TextBox5.Text = null;
       TextBox6.Text = null;
       TextBox7.Text = null;
    }

    private void Clear()
    {
        txtQuestion.Text = null;
        txtAnswer.Text = null;
      
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string s = Session["UserId"].ToString();
        RegistrationTB rg = HR.RegistrationTBs.Where(d => d.EmployeeId == int.Parse(s)).First();
        if (txtAnswer.Text == null)
        {
            g.ShowMessage(this.Page, "Please Enter Your Answer ");
        }
        else
        {
            if (txtAnswer.Text == rg.SecurityAnswer)
            {
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                g.ShowMessage(this.Page, "Please Enter Correct Answer ");

            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        txtAnswer.Text = null;
        Response.Redirect("HomePage.aspx");
    }
}