using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmpTrainingRequest : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            CompareValidator2.ValueToCompare = DateTime.Now.ToShortDateString();
            if (!Page.IsPostBack)
            {
                txtDate.Text = g.GetCurrentDateTime().ToShortDateString();
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void Message(string msg)
    {
        string rmsg = "<script language='javascript'>window.alert('" + msg + "')</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", rmsg, false);
    }
    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();

            ClearInputs(ctrl.Controls);
        }
    }
    private void clearFields()
    {
        ClearInputs(Page.Controls);
    }
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        var data = from d in HR.EmployeeTBs
                   where d.EmployeeId == Convert.ToInt32(Session["UserId"])
                   select new {d.CompanyId,d.DeptId };

        var dt = from p in HR.TrainingRequestTBs.Where(d => d.Emp_ID == Convert.ToInt32(Session["UserId"]) && d.Request_Date==Convert.ToDateTime(txtDate.Text) && d.Training_Topic==txtSubject.Text ) select p;
        if (dt.Count() > 0)
        {
             Message("Today This Topic Already Requested");

        }
        else
        {
            TrainingRequestTB tr = new TrainingRequestTB();

            tr.Emp_ID = Convert.ToInt32(Session["UserId"]);
            foreach (var item in data)
            {
                tr.CompanyID = item.CompanyId;
                tr.Department_ID = item.DeptId;
            }
            tr.Request_Date = Convert.ToDateTime(txtDate.Text);
            tr.Training_Topic = txtSubject.Text;
            tr.Status = 0;
            HR.TrainingRequestTBs.InsertOnSubmit(tr);
            HR.SubmitChanges();
            clearFields();
            Message("Data Saved Successfully");
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmpTrainingRequest.aspx");
    }
}