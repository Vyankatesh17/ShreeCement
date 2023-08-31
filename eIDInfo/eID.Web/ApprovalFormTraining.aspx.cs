using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class ApprovalFormTraining : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!Page.IsPostBack)
            {
                notraining();
               

            }
        }
    }

    public void training()
    {

        var CompanyData = from r in HR.tblTrainings join m in HR.MasterDeptTBs on r.deptID equals m.DeptID 
                          join k in HR.EmployeeTBs on r.EmpID equals k.EmployeeId
                          where r.Status == "Approved"
                          select new { name = concat(k.FName, k.MName, k.Lname), m.DeptName, r.TrainingDate, r.TrainingTime, r.TrainingSubject, r.Trainer, r.trainID, r.Status };
        if (CompanyData.Count() > 0)
        {
            grd_Training.Columns[7].Visible = false;
            grd_Training.DataSource = CompanyData;
            grd_Training.DataBind();
            lblcnt.Text = CompanyData.Count().ToString();

        }
        else
        {
            grd_Training.DataSource = null;
            grd_Training.DataBind();
            lblcnt.Text = CompanyData.Count().ToString();
        }
    }
    public void notraining()
    {

        var CompanyData = from r in HR.tblTrainings 
                          join m in HR.MasterDeptTBs on r.deptID equals m.DeptID 
                          join k in HR.EmployeeTBs on r.EmpID equals k.EmployeeId
                          where r.Status == "pending"
                          select new 
                          { name = concat(k.FName, k.MName, k.Lname), m.DeptName, r.TrainingDate, r.TrainingTime, r.TrainingSubject, r.Trainer, r.trainID,r.Status };

        if (CompanyData.Count() > 0)
        {
            grd_Training.Columns[7].Visible = true;
            grd_Training.DataSource = CompanyData;
            grd_Training.DataBind();
            lblcnt.Text = CompanyData.Count().ToString();

        }
        else
        {
            grd_Training.DataSource = null;
            grd_Training.DataBind();
            lblcnt.Text = CompanyData.Count().ToString();
        
        }
    }
    public string concat(string fname, string mname, string lname)
    {
        return (fname + ' ' + mname + ' ' + lname);
    }
    public string MDYToDMY(string input)
    {
        return Convert.ToString(Regex.Replace(input,
            "\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b",
            "${day}/${month}/${year}"));
    }

    protected void OnClick_Edit(object sender, EventArgs e)
    {
        LinkButton b = (LinkButton)sender;
        string str = b.CommandArgument;
        var r = (from y in HR.tblTrainings where y.trainID == Convert.ToInt32(str) select y).Single();
        r.Status="Approved";
        HR.SubmitChanges();
        notraining();
    }

    //protected void rbNotApproved_CheckedChanged(object sender, EventArgs e)
    //{
    //    notraining();
    //}
    //protected void rbApproved_CheckedChanged(object sender, EventArgs e)
    //{
    //    training();
    //}
    protected void LeaveDenie_Click(object sender, EventArgs e)
    {
        LinkButton b = (LinkButton)sender;
        string str = b.CommandArgument;
        var r = (from y in HR.tblTrainings where y.trainID == Convert.ToInt32(str) select y).Single();
        r.Status = "Not Approved";
        HR.SubmitChanges();
        notraining();
    }
    protected void rdpending_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdpending.SelectedIndex == 0)
        {
            notraining();
        }
        else
        {
            training();
        }
    }
}